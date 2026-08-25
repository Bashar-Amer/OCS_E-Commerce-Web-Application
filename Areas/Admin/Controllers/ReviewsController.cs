using CampTravelGear.Data;
using CampTravelGear.Helpers;
using CampTravelGear.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CampTravelGear.Areas.Admin.Controllers
{
    public class ReviewsController : BaseAdminController
    {
        public ReviewsController(ApplicationDbContext context) : base(context)
        {
        }

        // GET: /Admin/Reviews
        public IActionResult Index(int rPage = 1, int tPage = 1, string tab = "reviews")
        {
            int pageSize = 8;

            var query = _context.Reviews
                .Include(r => r.User)
                .Include(r => r.Product)
                    .ThenInclude(p => p.ProductImages)
                .OrderByDescending(r => r.CreatedAt);

            var query2 = _context.Testimonials
                .Include(r => r.User)
                .OrderByDescending(r => r.CreatedAt);

            var reviews = PaginatedList<Review>.Create(query, rPage, pageSize);
            var testimonials = PaginatedList<Testimonial>.Create(query2, tPage, pageSize);

            ViewBag.ActiveTab = tab;
            ViewBag.ReviewPage = rPage;
            ViewBag.TestimonialPage = tPage;

            return View((Reviews: reviews, Testimonials: testimonials));
        }

        // POST: /Admin/Reviews/ReviewEdit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ReviewEdit(int id, AdminResponse status)
        {
            var review = _context.Reviews.Find(id);
            if (review == null) return NotFound();

            review.Status = status.ToString();
            if (status == AdminResponse.Accepted) review.ApprovedAt = DateTime.UtcNow;
            _context.SaveChanges();

            TempData["Success"] = $"Review #{id} status updated to {status}!";
            return RedirectToAction(nameof(Index));
        }

        // POST: /Admin/Reviews/TestimonialsEdit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult TestimonialsEdit(int id, AdminResponse status)
        {
            var testimonial = _context.Testimonials.Find(id);
            if (testimonial == null) return NotFound();

            testimonial.Status = status.ToString();
            if (status == AdminResponse.Accepted) testimonial.ApprovedAt = DateTime.UtcNow;
            _context.SaveChanges();

            TempData["Success"] = $"Testimonial #{id} status updated to {status}!";
            return RedirectToAction(nameof(Index));
        }
    }
}
