using Microsoft.AspNetCore.Mvc;

namespace CampTravelGear.Controllers;

public class WishlistController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
