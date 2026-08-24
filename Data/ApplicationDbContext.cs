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
        const string customerUser4Id = "user-cust-004";

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
            FullName = "Rahhala Admin",
            PhoneNumber = "+962 6 500 2026",
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

        var customerUser4 = new ApplicationUser
        {
            Id = customerUser4Id,
            UserName = "omar.khalil@example.com",
            NormalizedUserName = "OMAR.KHALIL@EXAMPLE.COM",
            Email = "omar.khalil@example.com",
            NormalizedEmail = "OMAR.KHALIL@EXAMPLE.COM",
            EmailConfirmed = true,
            FullName = "Omar Al-Khalil",
            PhoneNumber = "+962 7 9876 5432",
            PasswordHash = customerPasswordHash,
            SecurityStamp = "sec-stamp-omar-004",
            ConcurrencyStamp = "con-stamp-omar-004"
        };

        builder.Entity<ApplicationUser>().HasData(adminUser, customerUser1, customerUser2, customerUser3, customerUser4);

        // --- Seed User Roles ---
        builder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string> { UserId = adminUserId, RoleId = adminRoleId },
            new IdentityUserRole<string> { UserId = customerUser1Id, RoleId = customerRoleId },
            new IdentityUserRole<string> { UserId = customerUser2Id, RoleId = customerRoleId },
            new IdentityUserRole<string> { UserId = customerUser3Id, RoleId = customerRoleId },
            new IdentityUserRole<string> { UserId = customerUser4Id, RoleId = customerRoleId }
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
                Name = "Rahhala Horizon 4-Person Geodesic Camping Tent",
                Description = "Engineered for rugged wilderness expeditions and weekend family getaways alike, the Rahhala Horizon 4-Person Tent delivers uncompromising storm protection and exceptional ventilation. Constructed from heavy-duty 210T ripstop polyester with a 3,500mm polyurethane waterproof coating, this tent keeps your camp completely dry even during continuous torrential downpours.\n\nThe freestanding geodesic frame uses 9.5mm aircraft-grade anodized aluminum poles that resist high mountain winds up to 60 km/h. Inside, dual oversized mesh doors and dual vestibules provide generous gear storage and cross-ventilation to prevent condensation. Features factory seam-taped bathtub floors, reflective guy lines, and internal organizer pockets for headlamps and maps.",
                Price = 189.00m,
                Stock = 28,
                IsActive = true,
                IsDeleted = false,
                CreatedAt = baseDate
            },
            new Product
            {
                Id = 2,
                CategoryId = 1,
                Name = "Alpine Apex 65L Internal Frame Mountain Expedition Backpack",
                Description = "Built for multi-day alpine traverses and rugged backcountry journeys, the Alpine Apex 65L backpack combines maximum load stability with breathable trail ergonomics. The dynamic torso adjustment system fits heights from 5'4\" to 6'4\", transferring 80% of pack weight smoothly to the multi-density contoured hip belt.\n\nFabricated from 420D high-tenacity Cordura nylon with abrasion-resistant Hypalon reinforcement panels, this pack easily withstands jagged rock scrambles. Includes a dedicated sleeping bag bottom compartment with internal divider, integrated 3L hydration bladder sleeve, dual trekking pole attachments, and a stowable high-visibility waterproof rain cover.",
                Price = 149.00m,
                Stock = 35,
                IsActive = true,
                IsDeleted = false,
                CreatedAt = baseDate
            },
            new Product
            {
                Id = 3,
                CategoryId = 2,
                Name = "Rahhala Trekker Ultra-Compact Aluminum Folding Camp Stool",
                Description = "Weighing merely 580 grams, the Rahhala Trekker Camp Stool is the ultimate lightweight seating solution for backpackers, trail runners, and campfire gatherings. Crafted with 7075 aviation-grade aluminum tubing with shock-corded assembly, it sets up in under 5 seconds and supports weights up to 130 kg (285 lbs).\n\nThe seat is woven from 600D water-repellent oxford canvas with reinforced bar-tack stitching at all stress points. Packs down into an ultra-compact cylinder that slips effortlessly into side water bottle pockets, making it an indispensable companion on every journey.",
                Price = 38.00m,
                Stock = 45,
                IsActive = true,
                IsDeleted = false,
                CreatedAt = baseDate
            },
            new Product
            {
                Id = 4,
                CategoryId = 2,
                Name = "GlacierPeak -10°C Ultralight Goose Down Mummy Sleeping Bag",
                Description = "Engineered for alpine bivouacs and cold mountain nights, the GlacierPeak sleeping bag delivers premium 750-fill-power hydrophobic goose down insulation rated for temperatures down to -10°C (14°F). The anatomically curved mummy profile minimizes dead air space for instant thermal retention.\n\nFeatures a 20D DWR-treated micro-ripstop shell that sheds condensation, a full-length insulated draft tube along the anti-snag YKK two-way zipper, and an ergonomic 3D footbox. Compresses down to the size of a water bottle in the included waterproof compression dry sack.",
                Price = 165.00m,
                Stock = 20,
                IsActive = true,
                IsDeleted = false,
                CreatedAt = baseDate
            },
            new Product
            {
                Id = 5,
                CategoryId = 3,
                Name = "TitanFlame Backcountry Micro Windproof Titanium Camp Stove",
                Description = "Crafted from precision-machined aerospace titanium, this ultralight 48g backpacking stove boils 1 liter of water in just 3.2 minutes while consuming minimal isobutane fuel. The concave burner head creates a localized micro-tornado flame that resists gusts up to 30 km/h without requiring a bulky windscreen.\n\nFoldable serrated pot supports accommodate cookware from small 500ml titanium mugs to 2-liter camp pots. Fits standard threaded Lindal valve fuel canisters and folds neatly into an included protective hard-shell case for effortless packing.",
                Price = 42.00m,
                Stock = 60,
                IsActive = true,
                IsDeleted = false,
                CreatedAt = baseDate
            },
            new Product
            {
                Id = 6,
                CategoryId = 5,
                Name = "TerraGrip Waterproof Ankle-Support Hiking Trail Boots",
                Description = "Conquer demanding rocky terrains, mud trails, and mountain summits with confidence. The TerraGrip boots combine waterproof full-grain nubuck leather with breathable HydroGuard waterproof membrane booties that keep feet completely dry while releasing internal perspiration.\n\nEquipped with custom deep-lug Vibram Megagrip rubber outsoles for exceptional braking power on steep descents. An injected EVA midsole and thermoplastic shank absorb trail shock and protect feet from sharp jagged rocks throughout long trekking days.",
                Price = 135.00m,
                Stock = 24,
                IsActive = true,
                IsDeleted = false,
                CreatedAt = baseDate
            },
            new Product
            {
                Id = 7,
                CategoryId = 4,
                Name = "Solaris 1200-Lumen Rechargeable LED Camp Lantern & Power Bank",
                Description = "Light up your entire campsite with 360-degree diffused illumination. The Solaris Lantern outputs up to 1,200 lumens with 4 versatile lighting modes: Warm Campfire Glow, Cool Daylight, Red Night-Vision, and Emergency SOS Flashing.\n\nPowered by an internal 5,200mAh rechargeable lithium battery that provides up to 70 hours of continuous runtime on low mode and doubles as an emergency USB power bank to charge smartphones on the trail. Features an IPX6 waterproof rating, magnetic base, and top carabiner hook.",
                Price = 39.00m,
                Stock = 55,
                IsActive = true,
                IsDeleted = false,
                CreatedAt = baseDate
            },
            new Product
            {
                Id = 8,
                CategoryId = 4,
                Name = "SwissForge 14-in-1 Tactical Stainless Outdoor Multi-Tool",
                Description = "Precision crafted from heat-treated 440C stainless steel, this heavy-duty multi-tool integrates 14 essential backcountry tools: spring-loaded needle-nose pliers, wire cutters, wood saw, serrated blade, can opener, bottle opener, Phillips and flat screwdrivers, and a ruler.\n\nEvery individual implement securely locks in place with a safety lever to prevent accidental closure during intense cutting or sawing tasks. Includes a heavy-duty ballistic nylon belt sheath.",
                Price = 49.00m,
                Stock = 40,
                IsActive = true,
                IsDeleted = false,
                CreatedAt = baseDate
            },
            new Product
            {
                Id = 9,
                CategoryId = 3,
                Name = "HydroShield Double-Wall Vacuum Insulated Camping Canteen 1.5L",
                Description = "Keep hydration icy cold for 36 hours or soup steaming hot for 18 hours. The HydroShield Canteen is manufactured from food-grade 18/8 kitchen stainless steel with TempLock double-wall vacuum insulation that never transfers flavor or creates outer condensation.\n\nFeatures a wide mouth for adding ice cubes, a leakproof twist-lock cap with an integrated stainless carrying handle, and a durable powder-coat exterior finish that resists scuffs and trail drops.",
                Price = 36.00m,
                Stock = 50,
                IsActive = true,
                IsDeleted = false,
                CreatedAt = baseDate
            },
            new Product
            {
                Id = 10,
                CategoryId = 4,
                Name = "ApexView 12x50 HD Waterproof Nitrogen-Purged Binoculars",
                Description = "Experience breathtaking clarity on wildlife spotting and trail reconnaissance. The ApexView features fully multi-coated FMC green broadband optics and premium BAK-4 roof prisms that deliver 99.5% light transmission even in low-light twilight conditions.\n\nO-ring sealed and nitrogen purged for 100% fogproof and waterproof performance in all climates. Rugged rubber armor absorbs shocks and provides a non-slip grip in wet weather. Includes neck strap, carrying case, and protective lens caps.",
                Price = 89.00m,
                Stock = 22,
                IsActive = true,
                IsDeleted = false,
                CreatedAt = baseDate
            },
            new Product
            {
                Id = 11,
                CategoryId = 2,
                Name = "CloudRest Self-Inflating 3-Inch Ergonomic Camp Sleeping Pad",
                Description = "Say goodbye to waking up on hard uneven ground. The CloudRest features 3 inches of high-density open-cell memory foam with an insulated R-value of 4.8, providing true 4-season thermal barrier from frozen ground.\n\nDual wide-mouth brass valves inflate the pad in under 60 seconds with zero pumping required. The top surface is soft-touch peached polyester that prevents sleeping bag rustle, while the bottom uses tough 75D puncture-resistant polyester with anti-slip silicone dots.",
                Price = 52.00m,
                Stock = 32,
                IsActive = true,
                IsDeleted = false,
                CreatedAt = baseDate
            },
            new Product
            {
                Id = 12,
                CategoryId = 5,
                Name = "TrailFlow Quick-Lock Carbon Fiber Trekking Poles (Pair)",
                Description = "Engineered with 100% 3K carbon fiber shafts, these trekking poles absorb trail vibrations and reduce knee strain by up to 25% on steep mountain descents. Weighs just 210 grams per pole.\n\nAluminum SpeedLock levers allow quick length adjustments from 65cm to 135cm even while wearing thick winter gloves. Features ergonomic natural cork grips that wick sweat, breathable padded wrist straps, carbide tungsten tips, and interchangeable mud and snow baskets.",
                Price = 64.00m,
                Stock = 30,
                IsActive = true,
                IsDeleted = false,
                CreatedAt = baseDate
            }
        );

        // --- Seed Product Images (4 structured images per product: /images/Products/{id}/img_{1..4}.jpg) ---
        var productImages = new List<ProductImage>();
        int imgIdCounter = 1;
        for (int pId = 1; pId <= 12; pId++)
        {
            for (int imgNum = 1; imgNum <= 4; imgNum++)
            {
                productImages.Add(new ProductImage
                {
                    Id = imgIdCounter++,
                    ProductId = pId,
                    ImageUrl = $"/images/Products/{pId}/img_{imgNum}.jpg",
                    IsMain = (imgNum == 1)
                });
            }
        }
        builder.Entity<ProductImage>().HasData(productImages);

        // --- Seed Addresses ---
        builder.Entity<Address>().HasData(
            new Address { Id = 1, UserId = customerUser1Id, FullAddress = "124 Mountain Trail Rd, Apt 4B", City = "Denver" },
            new Address { Id = 2, UserId = customerUser2Id, FullAddress = "582 Forest Pine Way", City = "Seattle" },
            new Address { Id = 3, UserId = customerUser3Id, FullAddress = "910 Alpine Ridge Ave", City = "Portland" },
            new Address { Id = 4, UserId = customerUser4Id, FullAddress = "Queen Rania St, Building 45", City = "Amman" }
        );

        // --- Seed Orders (Covering all OrderStatus values: Completed, Processing, Cancelled) ---
        var orderDate1 = new DateTime(2026, 8, 10, 14, 30, 0, DateTimeKind.Utc);
        var orderDate2 = new DateTime(2026, 8, 18, 9, 15, 0, DateTimeKind.Utc);
        var orderDate3 = new DateTime(2026, 8, 19, 16, 45, 0, DateTimeKind.Utc);
        var orderDate4 = new DateTime(2026, 8, 21, 11, 20, 0, DateTimeKind.Utc);
        var orderDate5 = new DateTime(2026, 8, 23, 13, 10, 0, DateTimeKind.Utc);
        var orderDate6 = new DateTime(2026, 8, 24, 15, 00, 0, DateTimeKind.Utc);

        builder.Entity<Order>().HasData(
            new Order
            {
                Id = 1,
                UserId = customerUser1Id,
                AddressId = 1,
                OrderDate = orderDate1,
                Status = OrderStatus.Completed.ToString(),
                TotalAmount = 338.00m,
                CreatedAt = orderDate1
            },
            new Order
            {
                Id = 2,
                UserId = customerUser2Id,
                AddressId = 2,
                OrderDate = orderDate2,
                Status = OrderStatus.Processing.ToString(),
                TotalAmount = 187.00m,
                CreatedAt = orderDate2
            },
            new Order
            {
                Id = 3,
                UserId = customerUser3Id,
                AddressId = 3,
                OrderDate = orderDate3,
                Status = OrderStatus.Cancelled.ToString(),
                TotalAmount = 103.00m,
                CreatedAt = orderDate3
            },
            new Order
            {
                Id = 4,
                UserId = customerUser4Id,
                AddressId = 4,
                OrderDate = orderDate4,
                Status = OrderStatus.Completed.ToString(),
                TotalAmount = 217.00m,
                CreatedAt = orderDate4
            },
            new Order
            {
                Id = 5,
                UserId = customerUser1Id,
                AddressId = 1,
                OrderDate = orderDate5,
                Status = OrderStatus.Processing.ToString(),
                TotalAmount = 171.00m,
                CreatedAt = orderDate5
            },
            new Order
            {
                Id = 6,
                UserId = customerUser2Id,
                AddressId = 2,
                OrderDate = orderDate6,
                Status = OrderStatus.Completed.ToString(),
                TotalAmount = 138.00m,
                CreatedAt = orderDate6
            }
        );

        // --- Seed Order Items ---
        builder.Entity<OrderItem>().HasData(
            new OrderItem { Id = 1, OrderId = 1, ProductId = 1, Quantity = 1, UnitPrice = 189.00m },
            new OrderItem { Id = 2, OrderId = 1, ProductId = 2, Quantity = 1, UnitPrice = 149.00m },
            new OrderItem { Id = 3, OrderId = 2, ProductId = 2, Quantity = 1, UnitPrice = 149.00m },
            new OrderItem { Id = 4, OrderId = 2, ProductId = 3, Quantity = 1, UnitPrice = 38.00m },
            new OrderItem { Id = 5, OrderId = 3, ProductId = 7, Quantity = 1, UnitPrice = 39.00m },
            new OrderItem { Id = 6, OrderId = 3, ProductId = 12, Quantity = 1, UnitPrice = 64.00m },
            new OrderItem { Id = 7, OrderId = 4, ProductId = 4, Quantity = 1, UnitPrice = 165.00m },
            new OrderItem { Id = 8, OrderId = 4, ProductId = 11, Quantity = 1, UnitPrice = 52.00m },
            new OrderItem { Id = 9, OrderId = 5, ProductId = 6, Quantity = 1, UnitPrice = 135.00m },
            new OrderItem { Id = 10, OrderId = 5, ProductId = 9, Quantity = 1, UnitPrice = 36.00m },
            new OrderItem { Id = 11, OrderId = 6, ProductId = 10, Quantity = 1, UnitPrice = 89.00m },
            new OrderItem { Id = 12, OrderId = 6, ProductId = 8, Quantity = 1, UnitPrice = 49.00m }
        );

        // --- Seed Payments (Covering multiple payment methods and statuses) ---
        builder.Entity<Payment>().HasData(
            new Payment
            {
                Id = 1,
                OrderId = 1,
                TransactionId = "TXN-849201",
                Amount = 338.00m,
                PaymentMethod = "Credit Card",
                Status = "Completed",
                PaidAt = new DateTime(2026, 8, 10, 14, 32, 0, DateTimeKind.Utc),
            },
            new Payment
            {
                Id = 2,
                OrderId = 2,
                TransactionId = "TXN-739102",
                Amount = 187.00m,
                PaymentMethod = "PayPal",
                Status = "Completed",
                PaidAt = new DateTime(2026, 8, 18, 9, 16, 0, DateTimeKind.Utc),
            },
            new Payment
            {
                Id = 3,
                OrderId = 3,
                TransactionId = "TXN-109483",
                Amount = 103.00m,
                PaymentMethod = "Credit Card",
                Status = "Failed",
                PaidAt = null,
            },
            new Payment
            {
                Id = 4,
                OrderId = 4,
                TransactionId = "TXN-559124",
                Amount = 217.00m,
                PaymentMethod = "Apple Pay",
                Status = "Completed",
                PaidAt = new DateTime(2026, 8, 21, 11, 22, 0, DateTimeKind.Utc),
            },
            new Payment
            {
                Id = 5,
                OrderId = 5,
                TransactionId = "TXN-662391",
                Amount = 171.00m,
                PaymentMethod = "Cash on Delivery",
                Status = "Pending",
                PaidAt = null,
            },
            new Payment
            {
                Id = 6,
                OrderId = 6,
                TransactionId = "TXN-773419",
                Amount = 138.00m,
                PaymentMethod = "Credit Card",
                Status = "Completed",
                PaidAt = new DateTime(2026, 8, 24, 15, 05, 0, DateTimeKind.Utc),
            }
        );

        // --- Seed Reviews (Covering all AdminResponse statuses: Accepted, Pending, Rejected) ---
        builder.Entity<Review>().HasData(
            new Review
            {
                Id = 1,
                UserId = customerUser1Id,
                ProductId = 1,
                Rating = 5,
                Comment = "Outstanding 4-person tent! Pitched it during heavy rain in the mountains and stayed bone dry. Setup took under 5 minutes.",
                Status = AdminResponse.Accepted.ToString(),
                CreatedAt = new DateTime(2026, 8, 12, 10, 0, 0, DateTimeKind.Utc),
                ApprovedAt = new DateTime(2026, 8, 12, 12, 0, 0, DateTimeKind.Utc)
            },
            new Review
            {
                Id = 2,
                UserId = customerUser2Id,
                ProductId = 2,
                Rating = 5,
                Comment = "The weight distribution on this 65L pack is incredible. The lumbar support saved my back on a 4-day ridge traverse.",
                Status = AdminResponse.Accepted.ToString(),
                CreatedAt = new DateTime(2026, 8, 15, 14, 0, 0, DateTimeKind.Utc),
                ApprovedAt = new DateTime(2026, 8, 15, 15, 0, 0, DateTimeKind.Utc)
            },
            new Review
            {
                Id = 3,
                UserId = customerUser3Id,
                ProductId = 3,
                Rating = 4,
                Comment = "Surprisingly sturdy for how small it folds down. Fits right into the bottle pocket of my backpack.",
                Status = AdminResponse.Accepted.ToString(),
                CreatedAt = new DateTime(2026, 8, 16, 11, 0, 0, DateTimeKind.Utc),
                ApprovedAt = new DateTime(2026, 8, 16, 12, 0, 0, DateTimeKind.Utc)
            },
            new Review
            {
                Id = 4,
                UserId = customerUser1Id,
                ProductId = 4,
                Rating = 5,
                Comment = "Super warm goose down sleeping bag for sub-zero mountain nights. Extremely compact when compressed.",
                Status = AdminResponse.Accepted.ToString(),
                CreatedAt = new DateTime(2026, 8, 20, 9, 30, 0, DateTimeKind.Utc),
                ApprovedAt = new DateTime(2026, 8, 20, 10, 0, 0, DateTimeKind.Utc)
            },
            new Review
            {
                Id = 5,
                UserId = customerUser4Id,
                ProductId = 6,
                Rating = 5,
                Comment = "Exceptional grip on rocky trails in Petra and Wadi Rum. Completely waterproof and comfortable all day.",
                Status = AdminResponse.Accepted.ToString(),
                CreatedAt = new DateTime(2026, 8, 22, 16, 0, 0, DateTimeKind.Utc),
                ApprovedAt = new DateTime(2026, 8, 22, 17, 0, 0, DateTimeKind.Utc)
            },
            new Review
            {
                Id = 6,
                UserId = customerUser1Id,
                ProductId = 7,
                Rating = 4,
                Comment = "Terrific camp lantern. The warm campfire mode is great inside the tent and it charged my phone on the trail.",
                Status = AdminResponse.Pending.ToString(),
                CreatedAt = new DateTime(2026, 8, 23, 18, 0, 0, DateTimeKind.Utc),
                ApprovedAt = null
            },
            new Review
            {
                Id = 7,
                UserId = customerUser2Id,
                ProductId = 8,
                Rating = 2,
                Comment = "The multi-tool is great quality, but the outer packaging was slightly squished during delivery.",
                Status = AdminResponse.Rejected.ToString(),
                CreatedAt = new DateTime(2026, 8, 23, 19, 0, 0, DateTimeKind.Utc),
                ApprovedAt = null
            },
            new Review
            {
                Id = 8,
                UserId = customerUser4Id,
                ProductId = 10,
                Rating = 5,
                Comment = "Crisp and clear BAK-4 optics for desert stargazing and mountain wildlife observation.",
                Status = AdminResponse.Pending.ToString(),
                CreatedAt = new DateTime(2026, 8, 24, 10, 0, 0, DateTimeKind.Utc),
                ApprovedAt = null
            }
        );

        // --- Seed Testimonials (Covering all AdminResponse statuses: Accepted, Pending, Rejected) ---
        builder.Entity<Testimonial>().HasData(
            new Testimonial
            {
                Id = 1,
                UserId = customerUser2Id,
                Name = "Sara G. Helvey",
                Content = "Rahhala gear survived our 10-day alpine trek in freezing storms. Best outdoor brand quality and fast delivery!",
                Status = AdminResponse.Accepted.ToString(),
                CreatedAt = new DateTime(2026, 7, 20, 9, 0, 0, DateTimeKind.Utc),
                ApprovedAt = new DateTime(2026, 7, 21, 10, 0, 0, DateTimeKind.Utc)
            },
            new Testimonial
            {
                Id = 2,
                UserId = customerUser1Id,
                Name = "John Doe",
                Content = "The most reliable camping gear store I have ever used. Authentic rugged equipment and top customer support.",
                Status = AdminResponse.Accepted.ToString(),
                CreatedAt = new DateTime(2026, 7, 25, 11, 0, 0, DateTimeKind.Utc),
                ApprovedAt = new DateTime(2026, 7, 26, 10, 0, 0, DateTimeKind.Utc)
            },
            new Testimonial
            {
                Id = 3,
                UserId = customerUser4Id,
                Name = "Omar Al-Khalil",
                Content = "The top outdoor camping outfitter in Jordan. Genuine technical gear and great customer care at their Amman center!",
                Status = AdminResponse.Accepted.ToString(),
                CreatedAt = new DateTime(2026, 7, 27, 14, 0, 0, DateTimeKind.Utc),
                ApprovedAt = new DateTime(2026, 7, 27, 16, 0, 0, DateTimeKind.Utc)
            },
            new Testimonial
            {
                Id = 4,
                UserId = customerUser3Id,
                Name = "Alex Hunter",
                Content = "Their geodesic tents and titanium stoves are elite level. Highly recommended to any wilderness adventurer.",
                Status = AdminResponse.Pending.ToString(),
                CreatedAt = new DateTime(2026, 7, 28, 16, 0, 0, DateTimeKind.Utc),
                ApprovedAt = null
            },
            new Testimonial
            {
                Id = 5,
                UserId = customerUser1Id,
                Name = "Spam User",
                Content = "Visit this promotional third-party spam link for discounts.",
                Status = AdminResponse.Rejected.ToString(),
                CreatedAt = new DateTime(2026, 7, 29, 8, 0, 0, DateTimeKind.Utc),
                ApprovedAt = null
            }
        );
    }
}
