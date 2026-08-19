using Microsoft.AspNetCore.Mvc;

namespace CampTravelGear.Controllers;

public class ShopController : Controller
{
    public IActionResult Index(string? category, string? search)
    {
        ViewBag.SelectedCategory = category;
        ViewBag.SearchQuery = search;
        return View();
    }

    public IActionResult Details(int? id)
    {
        ViewBag.ProductId = id ?? 1;
        return View();
    }
}
