using System.ComponentModel.DataAnnotations;

namespace CampTravelGear.DTOs
{
    public class CheckoutDto
    {
        [Required]
        [StringLength(255)]
        public required string FullAddress { get; set; }

        [Required]
        [StringLength(50)]
        public required string City { get; set; }
    }
}
