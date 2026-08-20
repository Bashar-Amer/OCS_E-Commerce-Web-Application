using System.ComponentModel.DataAnnotations;
using CampTravelGear.Data;

namespace CampTravelGear.Models;

public class Review
{
    public int Id { get; set; }

    [Required]
    [StringLength(450)]
    public required string UserId { get; set; }

    public ApplicationUser? User { get; set; }

    [Required]
    public int ProductId { get; set; }

    public Product? Product { get; set; }

    [Range(1, 5)]
    public int? Rating { get; set; }

    [StringLength(1000)]
    public string? Comment { get; set; }

    [StringLength(50)]
    public string Status { get; set; } = AdminResponse.Pending.ToString();

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? ApprovedAt { get; set; }
}
