using System.Diagnostics;
using System.Security.Claims;
using CampTravelGear.Data;
using CampTravelGear.Models;
using CampTravelGear.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CampTravelGear.Controllers;

public class HomeController : Controller
{
    private readonly ApplicationDbContext _context;

    public HomeController(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<IActionResult> Index()
    {
        var baseQuery = _context.Products
    .Where(p =>
        p.IsActive &&
        !p.IsDeleted &&
        p.Category != null &&
        !p.Category.IsDeleted
    );
        var vm = new HomeViewModel
        {
            // Genuinely correct: newest products by CreatedAt
            NewArrivals = await BuildProductCards(
                baseQuery.OrderByDescending(p => p.CreatedAt).Take(4)),

            Stats = new StoreStatsVM
            {
                TotalProducts = await baseQuery.CountAsync(),
                TotalCategories = await _context.Categories.CountAsync(c => !c.IsDeleted),
                HappyCustomers = await _context.Orders.Select(o => o.UserId).Distinct().CountAsync(),
            },

            Testimonials = await _context.Testimonials
             .Where(t => t.Status == "Accepted")
             .OrderByDescending(t => t.ApprovedAt)
              .Take(3)
              .Select(t => new TestimonialVM { Name = t.Name, Content = t.Content })
              .ToListAsync()
        };

        return View(vm);
    }

    private async Task<List<ProductCardVM>> BuildProductCards(IQueryable<Product> query)
    {
        return await query
            .Select(p => new ProductCardVM
            {
                Id = p.Id,
                Name = p.Name,
                CategoryName = p.Category != null ? p.Category.Name : "Outdoor Gear",
                Price = p.Price,
                ImageUrl = p.ProductImages
                    .Where(i => i.IsMain)
                    .Select(i => i.ImageUrl)
                    .FirstOrDefault(),
                ReviewCount = p.Reviews.Count(r => r.Status == "Accepted"),
                AverageRating = p.Reviews
                    .Where(r =>  r.Status == "Accepted" && r.Rating != null)
                    .Average(r => (double?)r.Rating)
            })
            .ToListAsync();
    }

    public IActionResult About()
    {
        return View();
    }

    public async Task<IActionResult> Contact()
    {
        // Check if user is logged in
        if (User.Identity?.IsAuthenticated == true)
        {
            var pendingName = HttpContext.Session.GetString(
                "PendingTestimonialName"
            );

            var pendingContent = HttpContext.Session.GetString(
                "PendingTestimonialContent"
            );

            // There is a pending testimonial
            if (!string.IsNullOrEmpty(pendingName) &&
                !string.IsNullOrEmpty(pendingContent))
            {
                var userId = User.FindFirstValue(
                    ClaimTypes.NameIdentifier
                );

                if (!string.IsNullOrEmpty(userId))
                {
                    var testimonial = new Testimonial
                    {
                        UserId = userId,
                        Name = pendingName,
                        Content = pendingContent,
                        Status = AdminResponse.Pending.ToString(),
                        CreatedAt = DateTime.UtcNow
                    };

                    _context.Testimonials.Add(testimonial);

                    await _context.SaveChangesAsync();

                    // Remove pending data after saving
                    HttpContext.Session.Remove(
                        "PendingTestimonialName"
                    );

                    HttpContext.Session.Remove(
                        "PendingTestimonialContent"
                    );

                    TempData["SuccessMessage"] =
                        "Thank you! Your experience has been submitted successfully.";
                }
            }
        }

        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateTestimonial(Testimonial testimonial)
    {
        // User must be logged in
        if (User.Identity?.IsAuthenticated != true)
        {
            return Unauthorized();
        }

        // Get UserId from Identity
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized();
        }

        // Assign UserId
        testimonial.UserId = userId;

        // Remove the validation error created during model binding
        ModelState.Remove(nameof(Testimonial.UserId));

        // Validate the model
        if (!ModelState.IsValid)
        {
            return View("Contact", testimonial);
        }

        // Set testimonial information
        testimonial.Status = AdminResponse.Pending.ToString();
        testimonial.CreatedAt = DateTime.UtcNow;

        // Save
        _context.Testimonials.Add(testimonial);

        await _context.SaveChangesAsync();

        TempData["SuccessMessage"] =
            "Thank you! Your experience has been submitted successfully.";

        return RedirectToAction("Contact");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error404()
    {
        return View();
    }
}
