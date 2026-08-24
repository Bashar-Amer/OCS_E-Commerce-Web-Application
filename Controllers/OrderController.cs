using CampTravelGear.Data;
using CampTravelGear.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using CampTravelGear.DTOs;

namespace CampTravelGear.Controllers;

[Authorize]
public class OrderController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ApplicationDbContext _dbContext;

    public OrderController(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
        _dbContext = dbContext;
    }


    private string CurrentUserId => _userManager.GetUserId(User)!;

    public async Task<IActionResult> Index() {
        var orders = await _dbContext.Orders
            .AsNoTracking()
            .Where(o => o.UserId == CurrentUserId)
            .Include(o => o.Address)
            .Include(o => o.OrderItems).ThenInclude(oi => oi.Product).ThenInclude(p => p!.ProductImages)
            .Include(o => o.OrderItems).ThenInclude(oi => oi.Product).ThenInclude(p => p!.Category)
            .Include(o => o.Payments)
            .OrderByDescending(o => o.OrderDate)
            .ToListAsync();

        return View(orders);
    }

    public async Task<IActionResult> Checkout()
    {
        var cart = await _dbContext.Carts
            .AsNoTracking()
            .Include(c => c.CartItems)
            .SingleOrDefaultAsync(c => c.UserId == CurrentUserId);

        if (cart == null || cart.CartItems.Count == 0)
            return RedirectToAction("Index", "Cart");

        if (TempData["CartIssues"] is List<string> issues && issues.Count > 0)
            ViewBag.CartIssues = issues;

        ViewBag.Total = cart.CartItems.Sum(ci => ci.UnitPrice * ci.Quantity);
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Checkout(CheckoutDto userData)
    {
        if (!ModelState.IsValid) return View(userData);

        var cart = await _dbContext.Carts
            .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Product)
            .SingleOrDefaultAsync(c => c.UserId == CurrentUserId);

        if (cart == null || cart.CartItems.Count == 0)
        {
            ModelState.AddModelError(string.Empty, "Your cart is empty.");
            return View(userData);
        }

        
        var validation = ValidateAndAdjustCart(cart);
        if (!validation.IsValid)
        {
            await _dbContext.SaveChangesAsync(); 
            TempData["CartIssues"] = validation.Issues;
            return RedirectToAction(nameof(Checkout));
        }

        await using var transaction = await _dbContext.Database.BeginTransactionAsync();

       
        foreach (var item in cart.CartItems)
        {
            var affected = await _dbContext.Products
                .Where(p => p.Id == item.ProductId && p.Stock >= item.Quantity)
                .ExecuteUpdateAsync(s => s.SetProperty(p => p.Stock, p => p.Stock - item.Quantity));

            if (affected == 0)
            {
                await transaction.RollbackAsync();
                TempData["CartIssues"] = new List<string>
                {
                    "One or more items sold out while you were checking out. Please review your cart."
                };
                return RedirectToAction(nameof(Checkout));
            }
        }

        var total = cart.CartItems.Sum(ci => ci.UnitPrice * ci.Quantity);

        var order = new Order
        {
            UserId = CurrentUserId,
            OrderDate = DateTime.UtcNow,
            Address = new Address
            {
                UserId = CurrentUserId,
                FullAddress = userData.FullAddress,
                City = userData.City
            },
            TotalAmount = total,
            OrderItems = cart.CartItems.Select(ci => new OrderItem
            {
                ProductId = ci.ProductId,
                Quantity = ci.Quantity,
                UnitPrice = ci.UnitPrice
            }).ToList()
        };

        _dbContext.Orders.Add(order);
        _dbContext.CartItems.RemoveRange(cart.CartItems);

        await _dbContext.SaveChangesAsync();
        await transaction.CommitAsync();

        return Ok(new { success = true, orderId = order.Id });
    }

    public async Task<IActionResult> Invoice(int id)
    {
        var order = await _dbContext.Orders
            .AsNoTracking()
            .Include(o => o.User)
            .Include(o => o.Address)
            .Include(o => o.OrderItems).ThenInclude(oi => oi.Product).ThenInclude(p => p!.ProductImages)
            .Include(o => o.OrderItems).ThenInclude(oi => oi.Product).ThenInclude(p => p!.Category)
            .Include(o => o.Payments)
            .FirstOrDefaultAsync(o => o.Id == id);

        if (order == null) return NotFound();
        if (order.UserId != CurrentUserId) return Forbid();

        ViewBag.OrderId = id.ToString();
        return View(order);
    }

    private CartValidationResult ValidateAndAdjustCart(Cart cart)
    {
        var issues = new List<string>();
        var toRemove = new List<CartItem>();

        foreach (var item in cart.CartItems)
        {
            if (item.Product is null || item.Product.Stock <= 0)
            {
                issues.Add($"\"{item.Product?.Name ?? "An item"}\" is out of stock and was removed from your cart.");
                toRemove.Add(item);
                continue;
            }

            if (item.Product.Stock < item.Quantity)
            {
                issues.Add($"Only {item.Product.Stock} left of \"{item.Product.Name}\" - quantity adjusted.");
                item.Quantity = item.Product.Stock;
            }
        }

        foreach (var item in toRemove)
        {
            cart.CartItems.Remove(item);
            _dbContext.CartItems.Remove(item);
        }

        return new CartValidationResult(issues.Count == 0, issues);
    }

    private readonly record struct CartValidationResult(bool IsValid, List<string> Issues);

}
