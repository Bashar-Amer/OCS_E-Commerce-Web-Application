namespace CampTravelGear.Models.ViewModels
{
    public class HomeViewModel
    {
        public List<ProductCardVM> NewArrivals { get; set; } = new();        // "New Equipment" (8)
        public StoreStatsVM Stats { get; set; } = new();
        public List<TestimonialVM> Testimonials { get; set; } = new();   // now a list of 3, not one
    }

    public class ProductCardVM
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string CategoryName { get; set; } = "";
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }
        public double? AverageRating { get; set; }   // null = no reviews yet, hide stars
        public int ReviewCount { get; set; }
    }

    public class TestimonialVM
    {
        public string Name { get; set; } = "";
        public string Content { get; set; } = "";
    }
    public class StoreStatsVM
    {
        public int TotalProducts { get; set; }
        public int TotalCategories { get; set; }
        public int HappyCustomers { get; set; }
        public double AverageRating { get; set; }
        public int TotalReviews { get; set; }
    }
}
