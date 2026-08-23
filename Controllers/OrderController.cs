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


    public async Task<IActionResult> Checkout()
    {
        string? userId = _userManager.GetUserId(User);
        if (userId != null)
        {
            var userCart = await _dbContext.Carts.SingleOrDefaultAsync(c => c.UserId == userId);
            if (userCart != null)
            {
                await _dbContext.Entry(userCart).Collection(c => c.CartItems).LoadAsync();

                var total = userCart.CartItems.Sum(item => item.UnitPrice * item.Quantity);
                ViewBag.Total = total;
                return View(null);
            }
        }
        return NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> Checkout(CheckoutDto userData)
    {
        
        if (ModelState.IsValid)
        {
            string? userId = _userManager.GetUserId(User);
            if (userId != null)
            {
                var userCart = await _dbContext.Carts.SingleOrDefaultAsync(c => c.UserId == userId);
                if (userCart != null)
                {
                    await _dbContext.Entry(userCart).Collection(c => c.CartItems).LoadAsync();

                    if (await CheckAndValidateCart(userCart.CartItems))
                        return RedirectToAction("Checkout");
                    
                    var total = userCart.CartItems.Sum(item => item.UnitPrice * item.Quantity);

                    var order = new Order
                    {
                        UserId = userId,
                        Address = new Address
                        {
                            UserId = userId,
                            FullAddress = userData.FullAddress,
                            City = userData.City
                        },
                        TotalAmount = total,
                        OrderItems = userCart.CartItems.Select(ci => new OrderItem
                        {
                            ProductId = ci.ProductId,
                            Quantity = ci.Quantity,
                            UnitPrice = ci.UnitPrice
                        }).ToList()
                    };

                    await _dbContext.Orders.AddAsync(order);
                    await _dbContext.SaveChangesAsync();

                    _dbContext.CartItems.RemoveRange(userCart.CartItems);
                    await _dbContext.SaveChangesAsync();

                    return Ok(order.Id);
                }
            }
        }
        return View(userData);
    }

    public IActionResult Invoice(int id)
    {
        ViewBag.OrderId = id.ToString();
        return View();
    }

    [NonAction]
    public async Task<bool> CheckAndValidateCart(ICollection<CartItem> items)
    {
        if (items.Count < 0)
            return false;
        
        foreach (var item in items)
        {
            await _dbContext.Entry(item).Reference(i => i.Product).LoadAsync();
            if(item!.Product.Stock < 1)
            {
                _dbContext.CartItems.Remove(item);
            }
        }
        return true;
    }
}
