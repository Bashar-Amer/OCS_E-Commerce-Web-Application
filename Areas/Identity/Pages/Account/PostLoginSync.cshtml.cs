using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CampTravelGear.Areas.Identity.Pages.Account
{
    public class PostLoginSyncModel : PageModel
    {
        public string ReturnUrl { get; set; }

        public IActionResult OnGet(string returnUrl = null)
        {
            if (string.IsNullOrEmpty(returnUrl) || !Url.IsLocalUrl(returnUrl))
            {
                returnUrl = Url.Content("~/");
            }

            ReturnUrl = returnUrl;
            return Page();
        }
    }
}
