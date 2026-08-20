using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CampTravelGear.Models;
public class ProductImage
{
    public int Id { get; set; }

    [Required]
    public int ProductId { get; set; }
    public Product? Product { get; set; }

    [Required]
    [StringLength(255)]
    public string ImageUrl { get; set; } = string.Empty;

    public bool IsMain { get; set; } = false;

    //public int SortOrder { get; set; } = 0;

  
}
