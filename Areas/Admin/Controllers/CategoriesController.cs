using CampTravelGear.Data;
using CampTravelGear.Helpers;
using CampTravelGear.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CampTravelGear.Areas.Admin.Controllers
{
    public class CategoriesController : BaseAdminController
    {
        public CategoriesController(ApplicationDbContext context) : base(context)
        {
        }

        // GET: /Admin/Categories
        public IActionResult Index(int page = 1)
        {
            int pageSize = 8;
            
            var query = _context.Categories.Where(c => !c.IsDeleted).OrderByDescending(c => c.Id);
            var categories = PaginatedList<Category>.Create(query, page, pageSize);
            return View(categories);
        }

        // GET: /Admin/Categories/Create
        public IActionResult Create()
        { 
            return View();
        }

        // POST: /Admin/Categories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid) {
                _context.Categories.Add(category);
                _context.SaveChanges();
                TempData["Success"] = "Category created successfully!";
                return RedirectToAction("Index");
            }
            return View(category);
        }

        // GET: /Admin/Categories/Edit/5
        public IActionResult Edit(int id)
        {
            var category = _context.Categories.Find(id);
            if (category == null) return NotFound();
            return View(category);
        }

        // POST: /Admin/Categories/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                _context.Categories.Update(category);
                _context.SaveChanges();
                TempData["Success"] = "Category updated successfully!";
                return RedirectToAction("Index");
            }
            return View(category);
        }

        // POST: /Admin/Categories/Remove/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Remove(int id) {
            var category = _context.Categories.Include(c => c.Products).FirstOrDefault(o => o.Id == id);
            if (category == null) return NotFound();
            
            category.IsDeleted = true;

            if(category.Products != null)
                foreach (Product product in category.Products)
                    product.IsDeleted = true;

            _context.Categories.Update(category);
            _context.SaveChanges();
            TempData["Success"] = "Category removed successfully!";
            return RedirectToAction("Index");
        }
    }
}
