using CampTravelGear.Data;
using CampTravelGear.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CampTravelGear.Areas.Admin.Controllers
{
    public class DashboardController : BaseAdminController
    {
        public DashboardController(ApplicationDbContext context) : base(context)
        {
        }

        public IActionResult Index()
        {
            var users = _context.Users
                .OrderBy(u => u.FullName ?? u.UserName)
                .ToList();

            var categories = _context.Categories
                .Where(c => !c.IsDeleted)
                .OrderByDescending(c => c.Id)
                .ToList();

            var products = _context.Products
                .Include(p => p.Category)
                .Include(p => p.ProductImages)
                .Where(p => !p.IsDeleted)
                .ToList();

            var orders = _context.Orders
                .Include(o => o.User)
                .Include(o => o.OrderItems)
                .OrderByDescending(o => o.Id)
                .ToList();

            return View((Users: users, Categories: categories, Products: products, Orders: orders));
        }
    }
}