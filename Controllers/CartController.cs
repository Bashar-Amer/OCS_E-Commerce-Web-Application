using Microsoft.AspNetCore.Mvc;

namespace CampTravelGear.Controllers;

public class CartController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
