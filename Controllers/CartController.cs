using CampTravelGear.Data;
using CampTravelGear.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CampTravelGear.Controllers;

public class CartController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ApplicationDbContext _dbContext;

    public CartController(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
        _dbContext = dbContext;
    }

    public async Task<IActionResult> Index()
    {
        string? userId = _userManager.GetUserId(User);
        if (userId != null)
        {
            var userCart = await _dbContext.Carts
                .Include(c => c.CartItems).ToListAsync();
            return View(userCart.Count);
        }
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Update([FromBody] UpdateCartItemDto updateDto)
    {
        string? userId = _userManager.GetUserId(User);

        if (userId == null)
            return Unauthorized();

        var item = await _dbContext.CartItems.FindAsync(updateDto.CartItemId);
        if (item == null)
            return NotFound();

        await _dbContext.Entry(item).Reference(i => i.Cart).LoadAsync();
        if (item?.Cart?.UserId != userId)
            return Forbid();

        item.Quantity = updateDto.Quantity;
        await _dbContext.SaveChangesAsync();

        return Ok();
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
}
