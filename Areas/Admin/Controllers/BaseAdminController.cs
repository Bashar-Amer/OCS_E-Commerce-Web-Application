using CampTravelGear.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CampTravelGear.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public abstract class BaseAdminController : Controller
    {
        protected readonly ApplicationDbContext _context;
        protected BaseAdminController(ApplicationDbContext context)
        {
            _context = context;
        }
    }
}
