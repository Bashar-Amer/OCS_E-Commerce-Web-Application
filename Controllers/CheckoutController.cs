using Microsoft.AspNetCore.Mvc;

namespace CampTravelGear.Controllers;

public class CheckoutController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
