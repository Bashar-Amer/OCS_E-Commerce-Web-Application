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

    private string? CurrentUserId => _userManager.GetUserId(User);

    [AllowAnonymous]
    public async Task<IActionResult> Index()
    {
        var cart = CurrentUserId is null
            ? null
            : await _dbContext.Carts
                .Include(c => c.CartItems)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.UserId == CurrentUserId);

        return View((id: cart?.Id ?? 0, count: cart?.CartItems.Count ?? 0));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateAll([FromBody] List<CartUpdateDto> updates)
    {
        if (updates is null || updates.Count == 0)
            return BadRequest("No items provided for update.");

        var ids = updates.Select(u => u.Id).ToList();

        var items = await _dbContext.CartItems
            .Where(ci => ids.Contains(ci.Id) && ci.Cart!.UserId == CurrentUserId)
            .ToListAsync();

        foreach (var item in items)
        {
            var newQty = updates.First(u => u.Id == item.Id).Quantity;
            if (newQty <= 0)
                _dbContext.CartItems.Remove(item);
            else
                item.Quantity = newQty;
        }

        await _dbContext.SaveChangesAsync();
        return Ok(new { success = true });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteItem(int id)
    {
        var item = await _dbContext.CartItems
            .Include(ci => ci.Cart)
            .FirstOrDefaultAsync(ci => ci.Id == id);

        if (item == null) return NotFound();
        if (item.Cart?.UserId != CurrentUserId) return Forbid();

        _dbContext.CartItems.Remove(item);
        await _dbContext.SaveChangesAsync();
        return Ok(new { success = true });
    }


    [HttpGet]
    public async Task<IActionResult> GetData()
    {
        var cart = await _dbContext.Carts
           .Include(c => c.CartItems)
               .ThenInclude(ci => ci.Product)
                   .ThenInclude(p => p!.ProductImages)
           .AsNoTracking()
           .FirstOrDefaultAsync(c => c.UserId == CurrentUserId);

        var dto = new CartDto
        {
            Id = cart?.Id ?? 0,
            CartItems = cart?.CartItems.Select(ci => new CartItemDto
            {
                Id = ci.Id,
                ProductId = ci.ProductId,
                ProductName = ci.Product?.Name,
                UnitPrice = ci.UnitPrice,
                Quantity = ci.Quantity,
                ImageUrl = ci.Product?.ProductImages?.FirstOrDefault(i => i.IsMain)?.ImageUrl
            }).ToList() ?? new List<CartItemDto>()
        };

        return Json(dto);
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddItem([FromForm] CartItemAddDto userData)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var product = await _dbContext.Products.FindAsync(userData.ProductId);
        if (product == null) return NotFound();

        var cart = await GetOrCreateCartAsync();

        await AddOrUpdateCartItemAsync(cart, product.Id, userData.Quantity, product.Price);

        await _dbContext.SaveChangesAsync();
        return Ok(new { success = true });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> MergeGuestCart([FromBody] List<CartItemAddDto> items)
    {
        if (items == null || !items.Any())
            return Ok(new { success = true });

        var cart = await GetOrCreateCartAsync();

        var productIds = items.Select(i => i.ProductId).Distinct().ToList();
        var products = await _dbContext.Products
            .Where(p => productIds.Contains(p.Id))
            .ToDictionaryAsync(p => p.Id);

        foreach (var userData in items)
        {
            if (!products.TryGetValue(userData.ProductId, out var product))
                continue;

            await AddOrUpdateCartItemAsync(cart, product.Id, userData.Quantity, product.Price);
        }

        await _dbContext.SaveChangesAsync();
        return Ok(new { success = true });
    }

    // Helper methods

    private async Task<Cart> GetOrCreateCartAsync()
    {
        var cart = await _dbContext.Carts.FirstOrDefaultAsync(c => c.UserId == CurrentUserId);
        if (cart == null)
        {
            cart = new Cart { UserId = CurrentUserId! };
            _dbContext.Carts.Add(cart);
            await _dbContext.SaveChangesAsync();
        }
        return cart;
    }

    

    private async Task AddOrUpdateCartItemAsync(Cart cart, int productId, int quantity, decimal unitPrice)
    {
        var item = await _dbContext.CartItems
            .FirstOrDefaultAsync(ci => ci.CartId == cart.Id && ci.ProductId == productId);

        if (item == null)
        {
            _dbContext.CartItems.Add(new CartItem
            {
                CartId = cart.Id,
                ProductId = productId,
                Quantity = quantity,
                UnitPrice = unitPrice
            });
        }
        else
        {
            item.Quantity += quantity;
        }
    }
}
