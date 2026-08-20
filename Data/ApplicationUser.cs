using Microsoft.AspNetCore.Identity;

namespace CampTravelGear.Data;

public class ApplicationUser : IdentityUser
{
    public string? FullName { get; set; }
}
