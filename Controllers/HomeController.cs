using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CampTravelGear.Models;

namespace CampTravelGear.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult About()
    {
        return View();
    }

    public IActionResult Contact()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error404()
    {
        return View();
    }
}
