using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CampTravelGear.Models;

namespace CampTravelGear.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<ProductImage> ProductImages { get; set; } = null!;
    public DbSet<Cart> Carts { get; set; } = null!;
    public DbSet<CartItem> CartItems { get; set; } = null!;
    public DbSet<Wishlist> Wishlists { get; set; } = null!;
    public DbSet<WishlistItem> WishlistItems { get; set; } = null!;
    public DbSet<Review> Reviews { get; set; } = null!;
    public DbSet<Testimonial> Testimonials { get; set; } = null!;
    public DbSet<Address> Addresses { get; set; } = null!;
    public DbSet<Order> Orders { get; set; } = null!;
    public DbSet<OrderItem> OrderItems { get; set; } = null!;
    public DbSet<Payment> Payments { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // ==========================================
        // 1. Index Constraints
        // ==========================================

        // Categories Index
        builder.Entity<Category>(entity =>
        {
            entity.HasIndex(c => c.Name).IsUnique();
        });

        // Products Indexes
        builder.Entity<Product>(entity =>
        {
            entity.HasIndex(p => p.Name);
            entity.HasIndex(p => p.CategoryId);
        });

        // Carts Index
        builder.Entity<Cart>(entity =>
        {
            entity.HasIndex(c => c.UserId).IsUnique();
        });

        // CartItems Unique Composite Index
        builder.Entity<CartItem>(entity =>
        {
            entity.HasIndex(ci => new { ci.CartId, ci.ProductId }).IsUnique();
        });

        // Wishlists Index
        builder.Entity<Wishlist>(entity =>
        {
            entity.HasIndex(w => w.UserId).IsUnique();
        });

        // WishlistItems Unique Composite Index
        builder.Entity<WishlistItem>(entity =>
        {
            entity.HasIndex(wi => new { wi.WishlistId, wi.ProductId }).IsUnique();
        });

        // ==========================================
        // 2. Database Seeds (Static & Deterministic)
        // ==========================================

        // --- Seed Roles ---
        const string adminRoleId = "role-admin-001";
        const string customerRoleId = "role-customer-002";

        builder.Entity<IdentityRole>().HasData(
            new IdentityRole
            {
                Id = adminRoleId,
                Name = "Admin",
                NormalizedName = "ADMIN",
                ConcurrencyStamp = "role-admin-stamp"
            },
            new IdentityRole
            {
                Id = customerRoleId,
                Name = "Customer",
                NormalizedName = "CUSTOMER",
                ConcurrencyStamp = "role-customer-stamp"
            }
        );

        // --- Seed Users ---
        const string adminUserId = "user-admin-001";
        const string customerUser1Id = "user-cust-001";
        const string customerUser2Id = "user-cust-002";
        const string customerUser3Id = "user-cust-003";

        // Precomputed static password hashes for Admin@123! and Customer@123!
        const string adminPasswordHash = "AQAAAAIAAYagAAAAEEdaqL9mVLSYIYDQnf3VbN26q7GMI+7eZ1KJyWghZois4i24uitPbuHClLflhAdeGA==";
        const string customerPasswordHash = "AQAAAAIAAYagAAAAEPvH9DFxtV1m7CiKbFG7DczGEaLFQzYgi5Ckg0OVEzpmEsDORBDjImwoH6IuqeZohA==";

        var adminUser = new ApplicationUser
        {
            Id = adminUserId,
            UserName = "admin@barrameru.com",
            NormalizedUserName = "ADMIN@BARRAMERU.COM",
            Email = "admin@barrameru.com",
            NormalizedEmail = "ADMIN@BARRAMERU.COM",
            EmailConfirmed = true,
            FullName = "Admin Administrator",
            PhoneNumber = "+1 (555) 019-2834",
            PasswordHash = adminPasswordHash,
            SecurityStamp = "sec-stamp-admin-001",
            ConcurrencyStamp = "con-stamp-admin-001"
        };

        var customerUser1 = new ApplicationUser
        {
            Id = customerUser1Id,
            UserName = "john.doe@example.com",
            NormalizedUserName = "JOHN.DOE@EXAMPLE.COM",
            Email = "john.doe@example.com",
            NormalizedEmail = "JOHN.DOE@EXAMPLE.COM",
            EmailConfirmed = true,
            FullName = "John Doe",
            PhoneNumber = "+1 (555) 342-8901",
            PasswordHash = customerPasswordHash,
            SecurityStamp = "sec-stamp-john-001",
            ConcurrencyStamp = "con-stamp-john-001"
        };

        var customerUser2 = new ApplicationUser
        {
            Id = customerUser2Id,
            UserName = "sara.helvey@example.com",
            NormalizedUserName = "SARA.HELVEY@EXAMPLE.COM",
            Email = "sara.helvey@example.com",
            NormalizedEmail = "SARA.HELVEY@EXAMPLE.COM",
            EmailConfirmed = true,
            FullName = "Sara G. Helvey",
            PhoneNumber = "+1 (555) 781-4329",
            PasswordHash = customerPasswordHash,
            SecurityStamp = "sec-stamp-sara-002",
            ConcurrencyStamp = "con-stamp-sara-002"
        };

        var customerUser3 = new ApplicationUser
        {
            Id = customerUser3Id,
            UserName = "alex.h@example.com",
            NormalizedUserName = "ALEX.H@EXAMPLE.COM",
            Email = "alex.h@example.com",
            NormalizedEmail = "ALEX.H@EXAMPLE.COM",
            EmailConfirmed = true,
            FullName = "Alex Hunter",
            PhoneNumber = "+1 (555) 629-1145",
            PasswordHash = customerPasswordHash,
            SecurityStamp = "sec-stamp-alex-003",
            ConcurrencyStamp = "con-stamp-alex-003"
        };

        builder.Entity<ApplicationUser>().HasData(adminUser, customerUser1, customerUser2, customerUser3);

        // --- Seed User Roles ---
        builder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string> { UserId = adminUserId, RoleId = adminRoleId },
            new IdentityUserRole<string> { UserId = customerUser1Id, RoleId = customerRoleId },
            new IdentityUserRole<string> { UserId = customerUser2Id, RoleId = customerRoleId },
            new IdentityUserRole<string> { UserId = customerUser3Id, RoleId = customerRoleId }
        );

        // --- Seed Categories ---
        var baseDate = new DateTime(2026, 1, 15, 8, 0, 0, DateTimeKind.Utc);
        builder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Bags & Pack", IsDeleted = false, CreatedAt = baseDate },
            new Category { Id = 2, Name = "Tent & Accessories", IsDeleted = false, CreatedAt = baseDate },
            new Category { Id = 3, Name = "Camp Kitchen", IsDeleted = false, CreatedAt = baseDate },
            new Category { Id = 4, Name = "Knives & Tools", IsDeleted = false, CreatedAt = baseDate },
            new Category { Id = 5, Name = "Climbing & Hiking", IsDeleted = false, CreatedAt = baseDate }
        );

        // --- Seed Products ---
        builder.Entity<Product>().HasData(
            new Product
            {
                Id = 1,
                CategoryId = 2,
                Name = "Black Folding Stool",
                Description = "Compact aluminum lightweight folding camp stool for hiking trails and campsite relaxation.",
                Price = 45.00m,
                Stock = 25,
                IsActive = true,
                IsDeleted = false,
                CreatedAt = baseDate
            },
            new Product
            {
                Id = 2,
                CategoryId = 2,
                Name = "Yellow 3-Person Tent",
                Description = "Double-layer waterproof 3-person dome camping tent with high-strength fiberglass poles and rainfly.",
                Price = 65.00m,
                Stock = 18,
                IsActive = true,
                IsDeleted = false,
                CreatedAt = baseDate
            },
            new Product
            {
                Id = 3,
                CategoryId = 2,
                Name = "Inflatable Camping Bed",
                Description = "Comfortable self-inflating sleeping pad with integrated air pillow and moisture barrier.",
                Price = 35.00m,
                Stock = 30,
                IsActive = true,
                IsDeleted = false,
                CreatedAt = baseDate
            },
            new Product
            {
                Id = 4,
                CategoryId = 2,
                Name = "Orange Ground Camping Mat",
                Description = "Closed-cell foam moisture-proof ground sleeping pad for rugged outdoor adventures.",
                Price = 20.00m,
                Stock = 40,
                IsActive = true,
                IsDeleted = false,
                CreatedAt = baseDate
            },
            new Product
            {
                Id = 5,
                CategoryId = 4,
                Name = "Multi-Function Folding Knife",
                Description = "High-grade stainless steel multi-tool tactical folding pocket knife with secure locking mechanism.",
                Price = 25.00m,
                Stock = 50,
                IsActive = true,
                IsDeleted = false,
                CreatedAt = baseDate
            },
            new Product
            {
                Id = 6,
                CategoryId = 4,
                Name = "Tactical LED Flashlight",
                Description = "1000-lumen ultra-bright rechargeable waterproof tactical torch with zoomable beam.",
                Price = 50.00m,
                Stock = 35,
                IsActive = true,
                IsDeleted = false,
                CreatedAt = baseDate
            },
            new Product
            {
                Id = 7,
                CategoryId = 4,
                Name = "Black HD Binoculars 10x42",
                Description = "High-definition compact roof prism binoculars with multi-coated anti-reflective lenses.",
                Price = 65.00m,
                Stock = 20,
                IsActive = true,
                IsDeleted = false,
                CreatedAt = baseDate
            },
            new Product
            {
                Id = 8,
                CategoryId = 4,
                Name = "Military Navigation Compass",
                Description = "Heavy-duty metal sighting navigation compass with fluorescent dial and clinometer.",
                Price = 50.00m,
                Stock = 45,
                IsActive = true,
                IsDeleted = false,
                CreatedAt = baseDate
            },
            new Product
            {
                Id = 9,
                CategoryId = 1,
                Name = "Trekking Expedition Backpack 65L",
                Description = "Heavy-duty ergonomic internal frame mountain hiking backpack with rain cover and hydration port.",
                Price = 150.00m,
                Stock = 15,
                IsActive = true,
                IsDeleted = false,
                CreatedAt = baseDate
            },
            new Product
            {
                Id = 10,
                CategoryId = 5,
                Name = "Brown Ankle-High Hiking Boots",
                Description = "Waterproof full-grain leather hiking boots with high-traction Vibram rubber outsoles.",
                Price = 125.00m,
                Stock = 22,
                IsActive = true,
                IsDeleted = false,
                CreatedAt = baseDate
            },
            new Product
            {
                Id = 11,
                CategoryId = 3,
                Name = "Insulated Metal Water Bottle 1L",
                Description = "Double-wall vacuum insulated stainless steel canteen keeping drinks hot for 12h or cold for 24h.",
                Price = 60.00m,
                Stock = 60,
                IsActive = true,
                IsDeleted = false,
                CreatedAt = baseDate
            },
            new Product
            {
                Id = 12,
                CategoryId = 3,
                Name = "Portable Camp Charcoal Grill",
                Description = "Foldable stainless steel tabletop barbecue grill for backcountry cooking and tailgating.",
                Price = 100.00m,
                Stock = 12,
                IsActive = true,
                IsDeleted = false,
                CreatedAt = baseDate
            },
            new Product
            {
                Id = 13,
                CategoryId = 5,
                Name = "Dynamic Climbing Ropes 60m",
                Description = "9.8mm dynamic UIAA certified climbing rope with dry treatment for alpine ascents.",
                Price = 80.00m,
                Stock = 25,
                IsActive = true,
                IsDeleted = false,
                CreatedAt = baseDate
            },
            new Product
            {
                Id = 14,
                CategoryId = 1,
                Name = "Camouflage Tactical Backpack 50L",
                Description = "Rugged military camo multi-compartment tactical pack with MOLLE webbing and padded harness.",
                Price = 140.00m,
                Stock = 18,
                IsActive = true,
                IsDeleted = false,
                CreatedAt = baseDate
            }
        );

        // --- Seed Product Images ---
        builder.Entity<ProductImage>().HasData(
            new ProductImage { Id = 1, ProductId = 1, ImageUrl = "/images/top-view-of-travel-equipment-for-a-mountain-trip-e1664201262219.jpg", IsMain = true },
            new ProductImage { Id = 2, ProductId = 2, ImageUrl = "/images/orange-tourist-tent-illuminated-from-inside-stands-in-mountains-above-clouds.jpg", IsMain = true },
            new ProductImage { Id = 3, ProductId = 3, ImageUrl = "/images/camping-in-steppe.jpg", IsMain = true },
            new ProductImage { Id = 4, ProductId = 4, ImageUrl = "/images/sandy-field-in-desert-of-egypt.jpg", IsMain = true },
            new ProductImage { Id = 5, ProductId = 5, ImageUrl = "/images/43.jpg", IsMain = true },
            new ProductImage { Id = 6, ProductId = 6, ImageUrl = "/images/panoramic-shot-of-two-young-tourists-looking-at-geographic-map-on-grey-background-e1664119398628.jpg", IsMain = true },
            new ProductImage { Id = 7, ProductId = 7, ImageUrl = "/images/high-angle-view-of-confident-couple-climbing-mountain-e1664201089286.jpg", IsMain = true },
            new ProductImage { Id = 8, ProductId = 8, ImageUrl = "/images/tourist-sitting-phu-sub-lek-viewpoint-sunset-lopburi-thailand-e1664089245105.jpg", IsMain = true },
            new ProductImage { Id = 9, ProductId = 9, ImageUrl = "/images/blue-hiking-backpack-with-fitness-mat-isolated-on-2021-09-03-13-40-43-utc-1.jpg", IsMain = true },
            new ProductImage { Id = 10, ProductId = 10, ImageUrl = "/images/tourist-with-a-backpack-against-the-sky-panorama.jpg", IsMain = true },
            new ProductImage { Id = 11, ProductId = 11, ImageUrl = "/images/spending-time-by-the-campfire.jpg", IsMain = true },
            new ProductImage { Id = 12, ProductId = 12, ImageUrl = "/images/paddling-kayaker-on-the-scenic-lake.jpg", IsMain = true },
            new ProductImage { Id = 13, ProductId = 13, ImageUrl = "/images/high-angle-view-of-confident-couple-climbing-mountain-e1664201089286.jpg", IsMain = true },
            new ProductImage { Id = 14, ProductId = 14, ImageUrl = "/images/group-of-friends-with-backpacks-doing-trekking-excursion-on-mountain.jpg", IsMain = true }
        );

        // --- Seed Addresses ---
        builder.Entity<Address>().HasData(
            new Address { Id = 1, UserId = customerUser1Id, FullAddress = "124 Mountain Trail Rd, Apt 4B", City = "Denver" },
            new Address { Id = 2, UserId = customerUser2Id, FullAddress = "582 Forest Pine Way", City = "Seattle" },
            new Address { Id = 3, UserId = customerUser3Id, FullAddress = "910 Alpine Ridge Ave", City = "Portland" }
        );

        // --- Seed Orders ---
        var orderDate1 = new DateTime(2026, 8, 10, 14, 30, 0, DateTimeKind.Utc);
        var orderDate2 = new DateTime(2026, 8, 18, 9, 15, 0, DateTimeKind.Utc);
        var orderDate3 = new DateTime(2026, 8, 19, 16, 45, 0, DateTimeKind.Utc);

        builder.Entity<Order>().HasData(
            new Order
            {
                Id = 1,
                UserId = customerUser1Id,
                AddressId = 1,
                OrderDate = orderDate1,
                Status = OrderStatus.Completed.ToString(),
                TotalAmount = 270.00m,
                CreatedAt = orderDate1
            },
            new Order
            {
                Id = 2,
                UserId = customerUser2Id,
                AddressId = 2,
                OrderDate = orderDate2,
                Status = OrderStatus.Processing.ToString(),
                TotalAmount = 185.00m,
                CreatedAt = orderDate2
            },
            new Order
            {
                Id = 3,
                UserId = customerUser3Id,
                AddressId = 3,
                OrderDate = orderDate3,
                Status = OrderStatus.Cancelled.ToString(),
                TotalAmount = 85.00m,
                CreatedAt = orderDate3
            }
        );

        // --- Seed Order Items ---
        builder.Entity<OrderItem>().HasData(
            new OrderItem { Id = 1, OrderId = 1, ProductId = 7, Quantity = 1, UnitPrice = 65.00m },
            new OrderItem { Id = 2, OrderId = 1, ProductId = 2, Quantity = 1, UnitPrice = 65.00m },
            new OrderItem { Id = 3, OrderId = 1, ProductId = 14, Quantity = 1, UnitPrice = 140.00m },
            new OrderItem { Id = 4, OrderId = 2, ProductId = 9, Quantity = 1, UnitPrice = 150.00m },
            new OrderItem { Id = 5, OrderId = 2, ProductId = 3, Quantity = 1, UnitPrice = 35.00m },
            new OrderItem { Id = 6, OrderId = 3, ProductId = 11, Quantity = 1, UnitPrice = 60.00m },
            new OrderItem { Id = 7, OrderId = 3, ProductId = 5, Quantity = 1, UnitPrice = 25.00m }
        );

        // --- Seed Payments ---
        builder.Entity<Payment>().HasData(
            new Payment
            {
                Id = 1,
                OrderId = 1,
                TransactionId = "TXN-849201",
                Amount = 270.00m,
                PaymentMethod = "Credit Card",
                Status = "Completed",
                PaidAt = new DateTime(2026, 8, 10, 14, 32, 0, DateTimeKind.Utc),
            },
            new Payment
            {
                Id = 2,
                OrderId = 2,
                TransactionId = "TXN-739102",
                Amount = 185.00m,
                PaymentMethod = "PayPal",
                Status = "Completed",
                PaidAt = new DateTime(2026, 8, 18, 9, 16, 0, DateTimeKind.Utc),
            },
            new Payment
            {
                Id = 3,
                OrderId = 3,
                TransactionId = "TXN-109483",
                Amount = 85.00m,
                PaymentMethod = "Credit Card",
                Status = "Pending",
                PaidAt = null,
            }
        );

        // --- Seed Reviews ---
        builder.Entity<Review>().HasData(
            new Review
            {
                Id = 1,
                UserId = customerUser1Id,
                ProductId = 2,
                Rating = 5,
                Comment = "Excellent camping tent, set up took 5 minutes and withstands wind easily.",
                Status = AdminResponse.Accepted.ToString(),
                CreatedAt = new DateTime(2026, 8, 12, 10, 0, 0, DateTimeKind.Utc),
                ApprovedAt = new DateTime(2026, 8, 12, 12, 0, 0, DateTimeKind.Utc)
            },
            new Review
            {
                Id = 2,
                UserId = customerUser2Id,
                ProductId = 9,
                Rating = 4,
                Comment = "Incredible build quality and very comfortable back padding for multi-day treks.",
                Status = AdminResponse.Pending.ToString(),
                CreatedAt = new DateTime(2026, 8, 15, 14, 0, 0, DateTimeKind.Utc),
                ApprovedAt = null
            },
            new Review
            {
                Id = 3,
                UserId = customerUser3Id,
                ProductId = 7,
                Rating = 2,
                Comment = "Arrived with a slight scratch on the outer lens cap.",
                Status = AdminResponse.Rejected.ToString(),
                CreatedAt = new DateTime(2026, 8, 16, 11, 0, 0, DateTimeKind.Utc),
                ApprovedAt = null
            },
            new Review
            {
                Id = 4,
                UserId = customerUser1Id,
                ProductId = 14,
                Rating = 5,
                Comment = "Super warm down sleeping bag for sub-zero mountain nights. Highly recommended!",
                Status = AdminResponse.Pending.ToString(),
                CreatedAt = new DateTime(2026, 8, 20, 9, 30, 0, DateTimeKind.Utc),
                ApprovedAt = null
            }
        );

        // --- Seed Testimonials ---
        builder.Entity<Testimonial>().HasData(
            new Testimonial
            {
                Id = 1,
                UserId = customerUser2Id,
                Name = "Sara G. Helvey",
                Content = "We've seen amazing results already. Barrameru should be nominated for outdoor brand of the year. Great customer service!",
                Status = AdminResponse.Accepted.ToString(),
                CreatedAt = new DateTime(2026, 7, 20, 9, 0, 0, DateTimeKind.Utc),
                ApprovedAt = new DateTime(2026, 7, 21, 10, 0, 0, DateTimeKind.Utc)
            },
            new Testimonial
            {
                Id = 2,
                UserId = customerUser1Id,
                Name = "John Doe",
                Content = "The best camping gear store I have ever used. Fast delivery and authentic rugged equipment.",
                Status = AdminResponse.Pending.ToString(),
                CreatedAt = new DateTime(2026, 7, 25, 11, 0, 0, DateTimeKind.Utc),
                ApprovedAt = null
            },
            new Testimonial
            {
                Id = 3,
                UserId = customerUser3Id,
                Name = "Alex Hunter",
                Content = "Spam test message or irrelevant feedback.",
                Status = AdminResponse.Rejected.ToString(),
                CreatedAt = new DateTime(2026, 7, 28, 16, 0, 0, DateTimeKind.Utc),
                ApprovedAt = null
            }
        );

        // --- Seed Carts & CartItems ---
        builder.Entity<Cart>().HasData(
            new Cart
            {
                Id = 1,
                UserId = customerUser1Id
            }
        );

        builder.Entity<CartItem>().HasData(
            new CartItem { Id = 1, CartId = 1, ProductId = 7, Quantity = 1, UnitPrice = 65.00m },
            new CartItem { Id = 2, CartId = 1, ProductId = 2, Quantity = 1, UnitPrice = 65.00m },
            new CartItem { Id = 3, CartId = 1, ProductId = 14, Quantity = 1, UnitPrice = 140.00m }
        );

        // --- Seed Wishlists & WishlistItems ---
        builder.Entity<Wishlist>().HasData(
            new Wishlist
            {
                Id = 1,
                UserId = customerUser1Id
            }
        );

        builder.Entity<WishlistItem>().HasData(
            new WishlistItem { Id = 1, WishlistId = 1, ProductId = 2, AddedAt = new DateTime(2026, 8, 1, 10, 0, 0, DateTimeKind.Utc) },
            new WishlistItem { Id = 2, WishlistId = 1, ProductId = 9, AddedAt = new DateTime(2026, 8, 2, 11, 0, 0, DateTimeKind.Utc) }
        );
    }
}
