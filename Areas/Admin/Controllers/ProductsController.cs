using CampTravelGear.Data;
using CampTravelGear.Helpers;
using CampTravelGear.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace CampTravelGear.Areas.Admin.Controllers
{
    public class ProductsController : BaseAdminController
    {
        public ProductsController(ApplicationDbContext context) : base(context)
        {
        }

        // GET: /Admin/Products
        public IActionResult Index(int page = 1, int? CategoryId = null)
        {
            int pageSize = 8;

            var query = _context.Products
                .Include(p => p.Category)
                .Include(p => p.ProductImages)
                .Where(p => !p.IsDeleted);
                

            if(CategoryId != null)
                query = query.Where(p => p.CategoryId == CategoryId.Value);

            query = query.OrderByDescending(p => p.Id);

            ViewBag.categories = _context.Categories.Where(c => !c.IsDeleted).ToList();
            ViewBag.selectedCategory = CategoryId;

            var products = PaginatedList<Product>.Create(query, page, pageSize);
            return View(products);
        }

        // GET: /Admin/Products/Create
        public IActionResult Create()
        {
            ViewBag.categories = _context.Categories.Where(c => !c.IsDeleted).ToList();
            return View();
        }

        // POST: /Admin/Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product product, List<IFormFile>? imageFiles , int main)
        {
            if (ModelState.IsValid)
            {
                _context.Products.Add(product);
                _context.SaveChanges();

                if (imageFiles != null && imageFiles.Count > 0)
                {
                    ImagesAddImages(product.Id, imageFiles, main);
                }
                TempData["Success"] = "Product created successfully!";
                return RedirectToAction("Index");
            }
            ViewBag.categories = _context.Categories.Where(c => !c.IsDeleted).ToList();
            return View(product);
        }

        // GET: /Admin/Products/Edit/5
        public IActionResult Edit(int id)
        {
            ViewBag.categories = _context.Categories.Where(c => !c.IsDeleted).ToList();
            var product = _context.Products.Include(p => p.ProductImages).FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound();
            return View(product);
        }

        // POST: /Admin/Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(
            Product product, 
            List<IFormFile>? imageFiles, 
            int? selectedExistingImageId, 
            int? selectedNewImageIndex)
        {
            if (ModelState.IsValid)
            {
                _context.Products.Update(product);
                _context.SaveChanges();

                if (selectedExistingImageId.HasValue)
                {
                    var existingImages = _context.ProductImages
                        .Where(pi => pi.ProductId == product.Id)
                        .ToList();

                    foreach (var img in existingImages)
                    {
                        img.IsMain = (img.Id == selectedExistingImageId.Value);
                    }
                    _context.SaveChanges();
                }

                if (imageFiles != null && imageFiles.Count > 0)
                {
                    if (selectedNewImageIndex.HasValue && selectedNewImageIndex.Value >= 0)
                    {
                        var existingImages = _context.ProductImages
                            .Where(pi => pi.ProductId == product.Id)
                            .ToList();

                        foreach (var img in existingImages)
                        {
                            img.IsMain = false;
                        }
                        _context.SaveChanges();
                    }

                    int newMain = selectedNewImageIndex ?? -1;
                    ImagesAddImages(product.Id, imageFiles, newMain);
                }

                TempData["Success"] = "Product updated successfully!";
                return RedirectToAction("Index");
            }
            ViewBag.categories = _context.Categories.Where(c => !c.IsDeleted).ToList();
            return View(product);
        }

        // POST: /Admin/Products/Remove/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Remove(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null) return NotFound();

            product.IsDeleted = true;
            _context.Products.Update(product);
            _context.SaveChanges();

            TempData["Success"] = "Product removed successfully!";
            return RedirectToAction("Index");
        }


        public IActionResult RemoveImage(int id)
        {
            var img = _context.ProductImages.Find(id);
            if (img == null) return NotFound();

            int productId = img.ProductId;
            var physicalPath = Path.Combine(
                        Directory.GetCurrentDirectory(),
                        "wwwroot",
                        img.ImageUrl.TrimStart('/')
                    );
            if (System.IO.File.Exists(physicalPath))
            {
                System.IO.File.Delete(physicalPath);
            }

            _context.ProductImages.Remove(img);
            _context.SaveChanges();
            TempData["Success"] = "Photo deleted successfully!";
            return RedirectToAction("Edit",new {id = productId});
        }


        private void ImagesAddImages(int productId, List<IFormFile> imagesFile, int main) {
            string wwwRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            string productImagesPath = Path.Combine(wwwRootPath, "images", "Products", productId.ToString());

            if (!Directory.Exists(productImagesPath))
            {
                Directory.CreateDirectory(productImagesPath);
            }

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".webp", ".gif" };
            int count = 0;
            foreach (IFormFile file in imagesFile)
            {
                var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
                if (!allowedExtensions.Contains(extension))
                {
                    continue; // Skip non-image files
                }

                string uniqueFileName = Guid.NewGuid().ToString() + extension;
                string filepath = Path.Combine(productImagesPath, uniqueFileName);

                using (var stream = new FileStream(filepath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                var image = new ProductImage
                {
                    ProductId = productId,
                    ImageUrl = $"/images/Products/{productId}/{uniqueFileName}",
                    IsMain = (count == main)
                };

                _context.ProductImages.Add(image);
                count++;
            }
            _context.SaveChanges();
        }

    }
}
