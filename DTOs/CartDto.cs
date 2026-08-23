using System.ComponentModel.DataAnnotations;

namespace CampTravelGear.DTOs
{
    public class CartDto
    {
        public int Id { get; set; }
        public List<CartItemDto> CartItems { get; set; }
    }

    public class CartItemDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; } = 1;
        public decimal UnitPrice { get; set; }
        public string ImageUrl { get; set; }
    }

    public class CartItemAddDto
    {
        [Required]
        public required int ProductId { get; set; }
        public int Quantity { get; set; } = 1;
    }

    public class CartUpdateDto
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
    }
}
