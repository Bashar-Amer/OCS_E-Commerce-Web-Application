namespace CampTravelGear.Models.ViewModels
{
    public class ShopProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public bool IsNew { get; set; }
        public double AverageRating { get; set; }
        public int ReviewCount { get; set; }
    }
}
