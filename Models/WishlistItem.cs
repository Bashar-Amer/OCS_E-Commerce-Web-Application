using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CampTravelGear.Models;

public class WishlistItem
{
    public int Id { get; set; }

    [Required]
    public int WishlistId { get; set; }

    public Wishlist? Wishlist { get; set; }

    [Required]
    public int ProductId { get; set; }

    public Product? Product { get; set; }

    public DateTime AddedAt { get; set; } = DateTime.UtcNow;
   
}
