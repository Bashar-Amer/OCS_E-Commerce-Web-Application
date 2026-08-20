using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CampTravelGear.Models;

public class Payment
{
    public int Id { get; set; }

    [Required]
    public int OrderId { get; set; }

    public Order? Order { get; set; }

    [Required]
    [StringLength(100)]
    public string TransactionId { get; set; } = string.Empty;

    [Column(TypeName = "decimal(18,2)")]
    public decimal Amount { get; set; }

    [Required]
    [StringLength(50)]
    public string PaymentMethod { get; set; } = string.Empty;

    [Required]
    [StringLength(50)]
    public string Status { get; set; } = "Pending";

    public DateTime? PaidAt { get; set; }

}
