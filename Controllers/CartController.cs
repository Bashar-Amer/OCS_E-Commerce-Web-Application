using CampTravelGear.Data;
using CampTravelGear.DTOs;
using CampTravelGear.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CampTravelGear.Controllers;

[Authorize]
public class CartController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ApplicationDbContext _dbContext;

    public CartController(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
        _dbContext = dbContext;
    }

    [AllowAnonymous]
    public async Task<IActionResult> Index()
    {
        string? userId = _userManager.GetUserId(User);
        if (userId != null)
        {
            var userCart = await _dbContext.Carts.FirstOrDefaultAsync(c=>c.UserId == userId);
            if (userCart != null)
            {
                await _dbContext.Entry(userCart).Collection(c => c.CartItems).LoadAsync();
                var data = (id: userCart.Id,count: userCart.CartItems.Count);
                return View(data);
            }
        }
        return View((id: 0, count: 0));
    }

    [HttpPost]
    public async Task<IActionResult> UpdateAll([FromBody] List<CartUpdateDto> updates)
    {
        if (updates == null || !updates.Any())
        {
            return BadRequest("No items provided for update.");
        }

        if (User.Identity.IsAuthenticated)
        {
            foreach (var update in updates)
            {
                var cartItem = await _dbContext.CartItems.FindAsync(update.Id);
                if (cartItem != null)
                {
                    if (update.Quantity <= 0)
                    {
                        _dbContext.CartItems.Remove(cartItem);
                    }
                    else
                    {
                        cartItem.Quantity = update.Quantity;
                    }
                }
            }
            await _dbContext.SaveChangesAsync();
        }
        return Ok(new { success = true });
    }

    [HttpGet]
    public async Task<IActionResult> DeleteItem(int id)
    {
        string? userId = _userManager.GetUserId(User);

        if (userId == null)
            return Unauthorized();

        var item = await _dbContext.CartItems.FindAsync(id);
        if (item == null)
            return NotFound();

        await _dbContext.Entry(item).Reference(i => i.Cart).LoadAsync();
        if (item?.Cart?.UserId != userId)
            return Forbid();

        _dbContext.CartItems.Remove(item);
        await _dbContext.SaveChangesAsync();

        return Ok();
    }

    public async Task<IActionResult> GetData()
    {
         string? userId = _userManager.GetUserId(User);
        if (userId != null)
        {
            var userCart = await _dbContext.Carts
                .Include(c => c.CartItems)
                .ThenInclude(item => item.Product)
                .ThenInclude(product=> product!.ProductImages)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (userCart == null) return NotFound();

            var cartDto = new CartDto
            {
                Id = userCart.Id,
                CartItems = userCart.CartItems.Select(ci => new CartItemDto
                {
                    Id = ci.Id,
                    ProductName = ci.Product?.Name,
                    UnitPrice = ci.UnitPrice,
                    Quantity = ci.Quantity,
                    ImageUrl = ci.Product?.ProductImages?.FirstOrDefault(image=>image.IsMain)?.ImageUrl

                }).ToList()
            };

            return Json(cartDto);
        }
        return NotFound();
    }

    public async Task<IActionResult> AddItem(CartItemAddDto userData)
    {
        string? userId = _userManager.GetUserId(User);
        if (userId == null) return Unauthorized();

        if (!ModelState.IsValid) return BadRequest();

        var cart = await _dbContext.Carts
            .Include(c => c.CartItems)
            .SingleOrDefaultAsync(c => c.UserId == userId);

        if (cart == null)
        {
            cart = new Cart { UserId = userId };
            _dbContext.Carts.Add(cart);
            await _dbContext.SaveChangesAsync();
        }

        var product = await _dbContext.Products.FindAsync(userData.ProductId);
        if (product == null) return NotFound();

        var item = cart.CartItems.FirstOrDefault(i => i.ProductId == product.Id);

        if (item == null)
        {
            var newItem = new CartItem
            {
                CartId = cart.Id,
                ProductId = product.Id,
                Quantity = userData.Quantity,
                UnitPrice = product.Price
            };
            _dbContext.CartItems.Add(newItem);
        }
        else
            item.Quantity += userData.Quantity;
        

        await _dbContext.SaveChangesAsync();
        return Ok(new { success = true });
    }
}
