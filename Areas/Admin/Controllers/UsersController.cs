using CampTravelGear.Data;
using CampTravelGear.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CampTravelGear.Areas.Admin.Controllers
{
    public class UsersController : BaseAdminController
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UsersController(ApplicationDbContext context, UserManager<ApplicationUser> userManager) : base(context)
        {
            _userManager = userManager;
        }

        // GET: /Admin/Users
        public IActionResult Index(int page = 1)
        {
            int pageSize = 8;

            var query = _context.Users
                .OrderBy(u => u.FullName ?? u.UserName);

            var users = PaginatedList<ApplicationUser>.Create(query, page, pageSize);
            return View(users);
        }
    }
}
