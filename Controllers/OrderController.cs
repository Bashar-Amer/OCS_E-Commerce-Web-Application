using Microsoft.AspNetCore.Mvc;

namespace CampTravelGear.Controllers;

public class OrderController : Controller
{
    public IActionResult Invoice(string? id)
    {
        ViewBag.OrderId = string.IsNullOrEmpty(id) ? "ORD-2026-8941" : id;
        return View();
    }
}
