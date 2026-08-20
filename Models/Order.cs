using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CampTravelGear.Data;

namespace CampTravelGear.Models;

public class Order
{
    public int Id { get; set; }

    [Required]
    [StringLength(450)]
    public required string UserId { get; set; }

    public ApplicationUser? User { get; set; }

    public int? AddressId { get; set; }

    public Address? Address { get; set; }

    public DateTime OrderDate { get; set; } = DateTime.UtcNow;

    [Required]
    [StringLength(50)]
    public string Status { get; set; } = OrderStatus.Pending.ToString();

    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalAmount { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
}
