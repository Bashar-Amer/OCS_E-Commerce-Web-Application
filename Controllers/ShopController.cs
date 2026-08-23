using CampTravelGear.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CampTravelGear.Models.ViewModels;

namespace CampTravelGear.Controllers;

public class ShopController : Controller
{
    private readonly ApplicationDbContext _context;
    private const int PageSize = 12;

    public ShopController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index(string? category, string? search, string? sort, int page = 1)
    {
        var categories = await _context.Categories
            .Where(c => !c.IsDeleted)
            .Select(c => new
            {
                c.Name,
                Count = c.Products.Count(p => p.IsActive && !p.IsDeleted)
            })
            .OrderBy(c => c.Name)
            .ToListAsync();

        ViewBag.Categories = categories
            .Select(c => (c.Name, c.Count))
            .ToList();

        // 2)  المنتجات
        var query = _context.Products
            .Include(p => p.Category)
            .Include(p => p.ProductImages)
            .Include(p => p.Reviews)
            .Where(p => p.IsActive && !p.IsDeleted)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(category) && category != "all")
        {
            query = query.Where(p => p.Category!.Name == category);
        }

        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(p => p.Name.Contains(search));
        }

        query = sort switch
        {
            "price-asc" => query.OrderBy(p => p.Price),
            "price-desc" => query.OrderByDescending(p => p.Price),
            "rating" => query.OrderByDescending(p =>
                                p.Reviews.Any() ? p.Reviews.Average(r => r.Rating) : 0),
            _ => query.OrderByDescending(p => p.CreatedAt)
        };

        var totalResults = await query.CountAsync();

        var products = await query
            .Skip((page - 1) * PageSize)
            .Take(PageSize)
            .ToListAsync();

        // 3) تجهيز الداتا للـ View (تفادي إرسال Entities تشيل كل الـ navigation properties)
        var productViewModels = products.Select(p => new ShopProductViewModel
        {
            Id = p.Id,
            Name = p.Name,
            Price = p.Price,
            ImageUrl = p.ProductImages.FirstOrDefault(img => img.IsMain)?.ImageUrl
           ?? p.ProductImages.FirstOrDefault()?.ImageUrl
           ?? "/images/placeholder.jpg",
            IsNew = p.CreatedAt >= DateTime.UtcNow.AddDays(-14),
            AverageRating = p.Reviews.Any(r => r.Rating.HasValue)
                ? Math.Round(p.Reviews.Where(r => r.Rating.HasValue).Average(r => r.Rating!.Value), 1)
                : 0,
            ReviewCount = p.Reviews.Count(r => r.Rating.HasValue)
        }).ToList();

        ViewBag.SelectedCategory = category;
        ViewBag.SearchQuery = search;
        ViewBag.SelectedSort = sort ?? "default";
        ViewBag.TotalResults = totalResults;
        ViewBag.PageStart = totalResults == 0 ? 0 : ((page - 1) * PageSize) + 1;
        ViewBag.PageEnd = Math.Min(page * PageSize, totalResults);
        ViewBag.CurrentPage = page;
        ViewBag.TotalPages = (int)Math.Ceiling(totalResults / (double)PageSize);

        return View(productViewModels);
    }

    public async Task<IActionResult> Details(int id)
    {
        var product = await _context.Products
            .Include(p => p.Category)
                .ThenInclude(c => c.Products)
                    .ThenInclude(p => p.ProductImages)

            .Include(p => p.ProductImages)

            .Include(p => p.Reviews)
                .ThenInclude(r => r.User)

            .FirstOrDefaultAsync(p =>
                p.Id == id &&
                p.IsActive &&
                !p.IsDeleted);

        if (product == null)
            return NotFound();

        return View(product);
    }

}
