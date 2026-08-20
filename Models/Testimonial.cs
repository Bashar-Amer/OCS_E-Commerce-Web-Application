using System.ComponentModel.DataAnnotations;
using CampTravelGear.Data;

namespace CampTravelGear.Models;

public class Testimonial
{
    public int Id { get; set; }

    [Required]
    [StringLength(450)]
    public required string UserId { get; set; }

    public ApplicationUser? User { get; set; }

    [Required]
    [StringLength(150)]
    public required string Name { get; set; }

    [Required]
    [StringLength(1000)]
    public required string Content { get; set; } 

    [StringLength(50)]
    public string Status { get; set; } = AdminResponse.Pending.ToString();

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? ApprovedAt { get; set; }
}
