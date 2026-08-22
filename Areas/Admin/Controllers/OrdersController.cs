using CampTravelGear.Data;
using CampTravelGear.Helpers;
using CampTravelGear.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CampTravelGear.Areas.Admin.Controllers
{
    public class OrdersController : BaseAdminController
    {
        public OrdersController(ApplicationDbContext context) : base(context)
        {
        }

        // GET: /Admin/Orders
        public IActionResult Index(int page = 1)
        {
            int pageSize = 8;

            var query = _context.Orders
                   .Include(o => o.User)
                   .Include(o => o.OrderItems)
                   .OrderByDescending(o => o.Id);
            var categories = PaginatedList<Order>.Create(query, page, pageSize);
            return View(categories);
        }

        public IActionResult Details(int id) {

            var order = _context.Orders
                    .Include(o => o.User)
                    .Include(o => o.Address)
                    .Include(o => o.OrderItems)
                        .ThenInclude(oi => oi.Product)
                            .ThenInclude(p => p.ProductImages)
                    .Include(o => o.Payments)
                    .FirstOrDefault(o => o.Id == id);

            if (order == null) return NotFound();
            return View(order);
        }


        // POST: /Admin/Orders/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, OrderStatus status)
        {
            var order = _context.Orders.Find(id);

            if (ModelState.IsValid)
            {

                if (order == null) return NotFound();


                order.Status = status.ToString();

                _context.SaveChanges();
                TempData["Success"] = "Order updated successfully!";
                return RedirectToAction("Index");
            }
            return View(order);
        }

       
    }
}
