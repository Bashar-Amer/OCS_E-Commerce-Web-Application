using System.ComponentModel.DataAnnotations;
using CampTravelGear.Data;

namespace CampTravelGear.Models;

public class Address 
{
    public int Id { get; set; }

    [Required]
    public required string UserId { get; set; }

    public ApplicationUser? User { get; set; }

    [Required]
    [StringLength(255)]
    public required string FullAddress { get; set; }

    [Required]
    [StringLength(100)]
    public required string City { get; set; }
}
