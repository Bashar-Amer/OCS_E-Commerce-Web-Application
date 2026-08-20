using System.ComponentModel.DataAnnotations;
using CampTravelGear.Data;

namespace CampTravelGear.Models;

public class Cart
{
    public int Id { get; set; }

    [Required]
    [StringLength(450)]
    public required string UserId { get; set; }

    public ApplicationUser? User { get; set; }

    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
}
