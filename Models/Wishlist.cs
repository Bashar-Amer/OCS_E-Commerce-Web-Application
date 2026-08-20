using System.ComponentModel.DataAnnotations;
using CampTravelGear.Data;

namespace CampTravelGear.Models;

public class Wishlist
{
    public int Id { get; set; }
    [Required]
    [StringLength(450)]
    public required string UserId { get; set; }
    public ApplicationUser? User { get; set; }

    public virtual ICollection<WishlistItem> WishlistItems { get; set; } = new List<WishlistItem>();
}
