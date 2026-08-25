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
            },
            new Product
            {
                Id = 13,
                CategoryId = 2,
                Name = "Rahhala Quick-Pitch 2-Person Pop-Up Blackout Camping Tent",
                Description = "Instant hydraulic pop-up camping tent designed for effortless 10-second setup. Engineered with 190T silver-coated blackout Oxford fabric providing UPF 50+ UV shielding and 2000mm water resistance. Dimensions: 190 x 120 x 100 cm (74.8 x 47.2 x 39.4 in), comfortably accommodating 1-2 persons with dual-layer mesh ventilation doors.",
                Price = 79.99m,
                Stock = 35,
                IsActive = true,
                IsDeleted = false,
                CreatedAt = baseDate
            },
            new Product
            {
                Id = 14,
                CategoryId = 3,
                Name = "CityFox 8L Collapsible Outdoor Water Bag with Spigot Dispenser",
                Description = "Heavy-duty 8-Liter (281 oz) collapsible camping water reservoir made from 100% food-grade, BPA-free PE/PA material. Features a 360-degree leakproof rotatable on/off spigot for easy handwashing, cooking, and hydration dispensing. Folds completely flat when empty and includes a reinforced carry and tree-hanging handle.",
                Price = 18.50m,
                Stock = 60,
                IsActive = true,
                IsDeleted = false,
                CreatedAt = baseDate
            },
            new Product
            {
                Id = 15,
                CategoryId = 2,
                Name = "UrbanWave Deluxe Padded Folding Camp Chair with Solid Beechwood Armrests",
                Description = "Premium ergonomic folding camp lounge chair featuring a removable high-density quilted cushion for all-season comfort. Built with natural solid beechwood armrests and a high-strength triangular carbon steel frame supporting up to 150 kg (330 lbs). Folds in 3 seconds into a slim, easy-carry profile.",
                Price = 68.00m,
                Stock = 25,
                IsActive = true,
                IsDeleted = false,
                CreatedAt = baseDate
            },
            new Product
            {
                Id = 16,
                CategoryId = 1,
                Name = "AeroTrail 40L Ultralight Packable Daypack with Separate Shoe & Wet Pocket",
                Description = "Ultralight 40-liter multi-compartment hiking backpack weighing only 1 lb (450g). Features an isolated bottom shoe compartment, waterproof wet gear pocket, breathable honeycomb mesh shoulder straps, dual water bottle mesh pockets, front bungee organizer, and trekking pole attachment loops.",
                Price = 45.00m,
                Stock = 40,
                IsActive = true,
                IsDeleted = false,
                CreatedAt = baseDate
            },
            new Product
            {
                Id = 17,
                CategoryId = 3,
                Name = "CampMaster 4-Person Stainless Steel Cutlery & Tableware Mess Kit (24-Piece)",
                Description = "Complete 24-piece outdoor dining kit crafted from mirror-polished food-grade 18/8 stainless steel. Includes 4 dinner knives, 4 dinner forks, 4 tablespoons, 4 dessert spoons, 4 pairs of chopsticks, 2 stainless straws with brush, 2 BBQ food tongs, 4 stainless plates, and 1 multi-opener, all organized in a heavy-duty zippered Oxford case.",
                Price = 34.00m,
                Stock = 50,
                IsActive = true,
                IsDeleted = false,
                CreatedAt = baseDate
            },
            new Product
            {
                Id = 18,
                CategoryId = 5,
                Name = "SaharaShield UPF 50+ Sun Protection Safari Boonie Hat with Detachable Neck & Face Flaps",
                Description = "Comprehensive 360-degree UPF 50+ sun protection outdoor hat featuring a wide 3.8-inch brim, removable breathable neck drape, and mesh face shield. Built with water-repellent quick-dry nylon, crown air circulation vents, and an adjustable windproof chin toggle drawstring.",
                Price = 22.00m,
                Stock = 70,
                IsActive = true,
                IsDeleted = false,
                CreatedAt = baseDate
            },
            new Product
            {
                Id = 19,
                CategoryId = 4,
                Name = "PathFinder Heritage Vintage Solid Brass Pocket Compass with 360 Luminous Dial",
                Description = "Classic survival navigation compass crafted from heavy-gauge polished solid brass with an engraved protective flip lid. Equipped with a 360-degree rotating azimuth bezel, rapid-settling magnetic needle, fluorescent glow-in-the-dark dial markings for night orienteering, and a solid lanyard ring.",
                Price = 26.00m,
                Stock = 55,
                IsActive = true,
                IsDeleted = false,
                CreatedAt = baseDate
            },
            new Product
            {
                Id = 20,
                CategoryId = 1,
                Name = "TacticalCook Heavy-Duty Modular Camping Gear Organizer Trunk 35L",
                Description = "Rugged 35-liter modular camp storage tote engineered from 900D water-resistant ballistic Oxford cloth with PE reinforced bottom panels. Includes movable padded Velcro dividers to securely store gas canisters, camping stoves, cookware, and lanterns without rattling. Features Molle loops and a padded shoulder strap.",
                Price = 52.00m,
                Stock = 30,
                IsActive = true,
                IsDeleted = false,
                CreatedAt = baseDate
            },
            new Product
            {
                Id = 21,
                CategoryId = 1,
                Name = "Vanguard Tactical MOLLE Hydration Waist Pack & Trail Fanny Bag",
                Description = "Tactical multi-pocket outdoor waist pack featuring a dedicated drawstring water bottle holster fitting up to 1L bottles. Built with 1000D abrasion-resistant nylon, dual front buckle compartments, laser-cut Molle attachment points, and an adjustable heavy-duty quick-release waist belt (28-50 inches).",
                Price = 29.50m,
                Stock = 45,
                IsActive = true,
                IsDeleted = false,
                CreatedAt = baseDate
            },
            new Product
            {
                Id = 22,
                CategoryId = 5,
                Name = "AeroCool UPF 50+ UV Compression Cooling Arm Sleeves with Thumb Holes",
                Description = "High-performance ice-silk cooling compression sleeves delivering UPF 50+ UV protection and immediate skin temperature reduction by 3-5 deg C. Features ergonomic thumb hole cuffs to prevent slippage, reflective safety accents for twilight visibility, and seamless 4-way stretch fabric for hiking and climbing.",
                Price = 14.00m,
                Stock = 80,
                IsActive = true,
                IsDeleted = false,
                CreatedAt = baseDate
            },
            new Product
            {
                Id = 23,
                CategoryId = 5,
                Name = "Namkabaw Quick-Deploy Tri-Fold Carbon-Alloy Trekking Poles with Natural Cork Grip",
                Description = "Ultra-compact tri-fold collapsible hiking poles folding down to just 36 cm (14 in). Features sweat-absorbing ergonomic natural cork handles, extended EVA choke grips, durable aluminum SpeedLock levers (110-130 cm adjustment), tungsten carbide tips, rubber walking feet, mud/snow baskets, and a ripstop carry bag.",
                Price = 58.00m,
                Stock = 35,
                IsActive = true,
                IsDeleted = false,
                CreatedAt = baseDate
            }
        );

        // --- Seed Product Images ---
        var productImages = new List<ProductImage>();
        int imgIdCounter = 1;
        
        // Products 1 to 12 (.jpg)
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

        // Products 13 to 23 (.png from drive download)
        var newProductImageCounts = new Dictionary<int, int>
        {
            { 13, 4 }, { 14, 4 }, { 15, 4 }, { 16, 4 },
            { 17, 3 }, { 18, 4 }, { 19, 3 }, { 20, 3 },
            { 21, 4 }, { 22, 3 }, { 23, 4 }
        };

        foreach (var kvp in newProductImageCounts)
        {
            int pId = kvp.Key;
            int count = kvp.Value;
            for (int imgNum = 1; imgNum <= count; imgNum++)
            {
                productImages.Add(new ProductImage
                {
                    Id = imgIdCounter++,
                    ProductId = pId,
                    ImageUrl = $"/images/Products/{pId}/img_{imgNum}.png",
                    IsMain = (imgNum == 1)
                });
            }
        }

        builder.Entity<ProductImage>().HasData(productImages);

                // --- Seed Addresses ---
        builder.Entity<Address>().HasData(
            new Address { Id = 1, UserId = customerUser1Id, FullAddress = "742 Evergreen Terrace", City = "Amman" },
            new Address { Id = 2, UserId = customerUser2Id, FullAddress = "1824 Mountain View Ridge", City = "Boulder" },
            new Address { Id = 3, UserId = customerUser3Id, FullAddress = "45 King Hussein Street", City = "Aqaba" },
            new Address { Id = 4, UserId = customerUser4Id, FullAddress = "89 Alpine Summit Blvd", City = "Salt Lake City" },
            new Address { Id = 5, UserId = customerUser1Id, FullAddress = "12 University Road", City = "Irbid" },
            new Address { Id = 6, UserId = customerUser2Id, FullAddress = "33 Panorama Way", City = "Denver" },
            new Address { Id = 7, UserId = customerUser3Id, FullAddress = "500 Sunset Strip", City = "Amman" },
            new Address { Id = 8, UserId = customerUser4Id, FullAddress = "104 Red Sea Marina Way", City = "Aqaba" }
        );

        // --- Seed Orders (66 Orders spanning 6 months with realistic revenue progression) ---
        builder.Entity<Order>().HasData(
            new Order
            {
                Id = 1,
                UserId = customerUser1Id,
                AddressId = 1,
                OrderDate = new DateTime(2026, 3, 5, 14, 20, 0, DateTimeKind.Utc),
                Status = OrderStatus.Completed.ToString(),
                TotalAmount = 227m,
                CreatedAt = new DateTime(2026, 3, 5, 14, 20, 0, DateTimeKind.Utc)
            },
            new Order
            {
                Id = 2,
                UserId = customerUser2Id,
                AddressId = 2,
                OrderDate = new DateTime(2026, 3, 10, 11, 15, 0, DateTimeKind.Utc),
                Status = OrderStatus.Completed.ToString(),
                TotalAmount = 227m,
                CreatedAt = new DateTime(2026, 3, 10, 11, 15, 0, DateTimeKind.Utc)
            },
            new Order
            {
                Id = 3,
                UserId = customerUser3Id,
                AddressId = 3,
                OrderDate = new DateTime(2026, 3, 16, 16, 40, 0, DateTimeKind.Utc),
                Status = OrderStatus.Completed.ToString(),
                TotalAmount = 217m,
                CreatedAt = new DateTime(2026, 3, 16, 16, 40, 0, DateTimeKind.Utc)
            },
            new Order
            {
                Id = 4,
                UserId = customerUser4Id,
                AddressId = 4,
                OrderDate = new DateTime(2026, 3, 21, 9, 30, 0, DateTimeKind.Utc),
                Status = OrderStatus.Completed.ToString(),
                TotalAmount = 221m,
                CreatedAt = new DateTime(2026, 3, 21, 9, 30, 0, DateTimeKind.Utc)
            },
            new Order
            {
                Id = 5,
                UserId = customerUser1Id,
                AddressId = 5,
                OrderDate = new DateTime(2026, 3, 26, 13, 0, 0, DateTimeKind.Utc),
                Status = OrderStatus.Completed.ToString(),
                TotalAmount = 177m,
                CreatedAt = new DateTime(2026, 3, 26, 13, 0, 0, DateTimeKind.Utc)
            },
            new Order
            {
                Id = 6,
                UserId = customerUser2Id,
                AddressId = 6,
                OrderDate = new DateTime(2026, 3, 29, 18, 10, 0, DateTimeKind.Utc),
                Status = OrderStatus.Cancelled.ToString(),
                TotalAmount = 188m,
                CreatedAt = new DateTime(2026, 3, 29, 18, 10, 0, DateTimeKind.Utc)
            },
            new Order
            {
                Id = 7,
                UserId = customerUser3Id,
                AddressId = 7,
                OrderDate = new DateTime(2026, 4, 3, 10, 0, 0, DateTimeKind.Utc),
                Status = OrderStatus.Completed.ToString(),
                TotalAmount = 262m,
                CreatedAt = new DateTime(2026, 4, 3, 10, 0, 0, DateTimeKind.Utc)
            },
            new Order
            {
                Id = 8,
                UserId = customerUser4Id,
                AddressId = 8,
                OrderDate = new DateTime(2026, 4, 7, 15, 25, 0, DateTimeKind.Utc),
                Status = OrderStatus.Completed.ToString(),
                TotalAmount = 284m,
                CreatedAt = new DateTime(2026, 4, 7, 15, 25, 0, DateTimeKind.Utc)
            },
            new Order
            {
                Id = 9,
                UserId = customerUser1Id,
                AddressId = 1,
                OrderDate = new DateTime(2026, 4, 12, 12, 10, 0, DateTimeKind.Utc),
                Status = OrderStatus.Completed.ToString(),
                TotalAmount = 241m,
                CreatedAt = new DateTime(2026, 4, 12, 12, 10, 0, DateTimeKind.Utc)
            },
            new Order
            {
                Id = 10,
                UserId = customerUser2Id,
                AddressId = 2,
                OrderDate = new DateTime(2026, 4, 16, 17, 45, 0, DateTimeKind.Utc),
                Status = OrderStatus.Completed.ToString(),
                TotalAmount = 181.99m,
                CreatedAt = new DateTime(2026, 4, 16, 17, 45, 0, DateTimeKind.Utc)
            },
            new Order
            {
                Id = 11,
                UserId = customerUser3Id,
                AddressId = 3,
                OrderDate = new DateTime(2026, 4, 20, 11, 30, 0, DateTimeKind.Utc),
                Status = OrderStatus.Completed.ToString(),
                TotalAmount = 164m,
                CreatedAt = new DateTime(2026, 4, 20, 11, 30, 0, DateTimeKind.Utc)
            },
            new Order
            {
                Id = 12,
                UserId = customerUser4Id,
                AddressId = 4,
                OrderDate = new DateTime(2026, 4, 23, 14, 0, 0, DateTimeKind.Utc),
                Status = OrderStatus.Completed.ToString(),
                TotalAmount = 181.5m,
                CreatedAt = new DateTime(2026, 4, 23, 14, 0, 0, DateTimeKind.Utc)
            },
            new Order
            {
                Id = 13,
                UserId = customerUser1Id,
                AddressId = 5,
                OrderDate = new DateTime(2026, 4, 27, 16, 20, 0, DateTimeKind.Utc),
                Status = OrderStatus.Completed.ToString(),
                TotalAmount = 158m,
                CreatedAt = new DateTime(2026, 4, 27, 16, 20, 0, DateTimeKind.Utc)
            },
            new Order
            {
                Id = 14,
                UserId = customerUser2Id,
                AddressId = 6,
                OrderDate = new DateTime(2026, 4, 29, 9, 50, 0, DateTimeKind.Utc),
                Status = OrderStatus.Completed.ToString(),
                TotalAmount = 187m,
                CreatedAt = new DateTime(2026, 4, 29, 9, 50, 0, DateTimeKind.Utc)
            },
            new Order
            {
                Id = 15,
                UserId = customerUser3Id,
                AddressId = 7,
                OrderDate = new DateTime(2026, 5, 2, 11, 0, 0, DateTimeKind.Utc),
                Status = OrderStatus.Completed.ToString(),
                TotalAmount = 430m,
                CreatedAt = new DateTime(2026, 5, 2, 11, 0, 0, DateTimeKind.Utc)
            },
            new Order
            {
                Id = 16,
                UserId = customerUser4Id,
                AddressId = 8,
                OrderDate = new DateTime(2026, 5, 5, 14, 30, 0, DateTimeKind.Utc),
                Status = OrderStatus.Completed.ToString(),
                TotalAmount = 302m,
                CreatedAt = new DateTime(2026, 5, 5, 14, 30, 0, DateTimeKind.Utc)
            },
            new Order
            {
                Id = 17,
                UserId = customerUser1Id,
                AddressId = 1,
                OrderDate = new DateTime(2026, 5, 9, 10, 15, 0, DateTimeKind.Utc),
                Status = OrderStatus.Completed.ToString(),
                TotalAmount = 315m,
                CreatedAt = new DateTime(2026, 5, 9, 10, 15, 0, DateTimeKind.Utc)
            },
            new Order
            {
                Id = 18,
                UserId = customerUser2Id,
                AddressId = 2,
                OrderDate = new DateTime(2026, 5, 13, 16, 0, 0, DateTimeKind.Utc),
                Status = OrderStatus.Completed.ToString(),
                TotalAmount = 209m,
                CreatedAt = new DateTime(2026, 5, 13, 16, 0, 0, DateTimeKind.Utc)
            },
            new Order
            {
                Id = 19,
                UserId = customerUser3Id,
                AddressId = 3,
                OrderDate = new DateTime(2026, 5, 16, 13, 40, 0, DateTimeKind.Utc),
                Status = OrderStatus.Completed.ToString(),
                TotalAmount = 178.48m,
                CreatedAt = new DateTime(2026, 5, 16, 13, 40, 0, DateTimeKind.Utc)
            },
            new Order
            {
                Id = 20,
                UserId = customerUser4Id,
                AddressId = 4,
                OrderDate = new DateTime(2026, 5, 20, 9, 20, 0, DateTimeKind.Utc),
                Status = OrderStatus.Completed.ToString(),
                TotalAmount = 196m,
                CreatedAt = new DateTime(2026, 5, 20, 9, 20, 0, DateTimeKind.Utc)
            },
            new Order
            {
                Id = 21,
                UserId = customerUser1Id,
                AddressId = 5,
                OrderDate = new DateTime(2026, 5, 23, 15, 10, 0, DateTimeKind.Utc),
                Status = OrderStatus.Completed.ToString(),
                TotalAmount = 174m,
                CreatedAt = new DateTime(2026, 5, 23, 15, 10, 0, DateTimeKind.Utc)
            },
            new Order
            {
                Id = 22,
                UserId = customerUser2Id,
                AddressId = 6,
                OrderDate = new DateTime(2026, 5, 26, 17, 35, 0, DateTimeKind.Utc),
                Status = OrderStatus.Completed.ToString(),
                TotalAmount = 131.5m,
                CreatedAt = new DateTime(2026, 5, 26, 17, 35, 0, DateTimeKind.Utc)
            },
            new Order
            {
                Id = 23,
                UserId = customerUser3Id,
                AddressId = 7,
                OrderDate = new DateTime(2026, 5, 28, 12, 0, 0, DateTimeKind.Utc),
                Status = OrderStatus.Completed.ToString(),
                TotalAmount = 269m,
                CreatedAt = new DateTime(2026, 5, 28, 12, 0, 0, DateTimeKind.Utc)
            },
            new Order
            {
                Id = 24,
                UserId = customerUser4Id,
                AddressId = 8,
                OrderDate = new DateTime(2026, 5, 31, 16, 50, 0, DateTimeKind.Utc),
                Status = OrderStatus.Cancelled.ToString(),
                TotalAmount = 338m,
                CreatedAt = new DateTime(2026, 5, 31, 16, 50, 0, DateTimeKind.Utc)
            },
            new Order
            {
                Id = 25,
                UserId = customerUser1Id,
                AddressId = 1,
                OrderDate = new DateTime(2026, 6, 3, 10, 10, 0, DateTimeKind.Utc),
                Status = OrderStatus.Completed.ToString(),
                TotalAmount = 493m,
                CreatedAt = new DateTime(2026, 6, 3, 10, 10, 0, DateTimeKind.Utc)
            },
            new Order
            {
                Id = 26,
                UserId = customerUser2Id,
                AddressId = 2,
                OrderDate = new DateTime(2026, 6, 6, 14, 40, 0, DateTimeKind.Utc),
                Status = OrderStatus.Completed.ToString(),
                TotalAmount = 497m,
                CreatedAt = new DateTime(2026, 6, 6, 14, 40, 0, DateTimeKind.Utc)
            },
            new Order
            {
                Id = 27,
                UserId = customerUser3Id,
                AddressId = 3,
                OrderDate = new DateTime(2026, 6, 9, 11, 25, 0, DateTimeKind.Utc),
                Status = OrderStatus.Completed.ToString(),
                TotalAmount = 303m,
                CreatedAt = new DateTime(2026, 6, 9, 11, 25, 0, DateTimeKind.Utc)
            },
            new Order
            {
                Id = 28,
                UserId = customerUser4Id,
                AddressId = 4,
                OrderDate = new DateTime(2026, 6, 12, 16, 0, 0, DateTimeKind.Utc),
                Status = OrderStatus.Completed.ToString(),
                TotalAmount = 295.98m,
                CreatedAt = new DateTime(2026, 6, 12, 16, 0, 0, DateTimeKind.Utc)
            },
            new Order
            {
                Id = 29,
                UserId = customerUser1Id,
                AddressId = 5,
                OrderDate = new DateTime(2026, 6, 15, 9, 30, 0, DateTimeKind.Utc),
                Status = OrderStatus.Completed.ToString(),
                TotalAmount = 171.5m,
                CreatedAt = new DateTime(2026, 6, 15, 9, 30, 0, DateTimeKind.Utc)
            },
            new Order
            {
                Id = 30,
                UserId = customerUser2Id,
                AddressId = 6,
                OrderDate = new DateTime(2026, 6, 18, 13, 50, 0, DateTimeKind.Utc),
                Status = OrderStatus.Completed.ToString(),
                TotalAmount = 272m,
                CreatedAt = new DateTime(2026, 6, 18, 13, 50, 0, DateTimeKind.Utc)
            },
            new Order
            {
                Id = 31,
                UserId = customerUser3Id,
                AddressId = 7,
                OrderDate = new DateTime(2026, 6, 21, 17, 15, 0, DateTimeKind.Utc),
                Status = OrderStatus.Completed.ToString(),
                TotalAmount = 283m,
                CreatedAt = new DateTime(2026, 6, 21, 17, 15, 0, DateTimeKind.Utc)
            },
            new Order
            {
                Id = 32,
                UserId = customerUser4Id,
                AddressId = 8,
                OrderDate = new DateTime(2026, 6, 24, 12, 20, 0, DateTimeKind.Utc),
                Status = OrderStatus.Completed.ToString(),
                TotalAmount = 272m,
                CreatedAt = new DateTime(2026, 6, 24, 12, 20, 0, DateTimeKind.Utc)
            },
            new Order
            {
                Id = 33,
                UserId = customerUser1Id,
                AddressId = 1,
                OrderDate = new DateTime(2026, 6, 26, 15, 45, 0, DateTimeKind.Utc),
                Status = OrderStatus.Completed.ToString(),
                TotalAmount = 372.5m,
                CreatedAt = new DateTime(2026, 6, 26, 15, 45, 0, DateTimeKind.Utc)
            },
            new Order
            {
                Id = 34,
                UserId = customerUser2Id,
                AddressId = 2,
                OrderDate = new DateTime(2026, 6, 28, 10, 0, 0, DateTimeKind.Utc),
                Status = OrderStatus.Completed.ToString(),
                TotalAmount = 285m,
                CreatedAt = new DateTime(2026, 6, 28, 10, 0, 0, DateTimeKind.Utc)
            },
            new Order
            {
                Id = 35,
                UserId = customerUser3Id,
                AddressId = 3,
                OrderDate = new DateTime(2026, 6, 29, 18, 30, 0, DateTimeKind.Utc),
                Status = OrderStatus.Completed.ToString(),
                TotalAmount = 272m,
                CreatedAt = new DateTime(2026, 6, 29, 18, 30, 0, DateTimeKind.Utc)
            },
            new Order
            {
                Id = 36,
                UserId = customerUser4Id,
                AddressId = 4,
                OrderDate = new DateTime(2026, 6, 30, 14, 15, 0, DateTimeKind.Utc),
                Status = OrderStatus.Completed.ToString(),
                TotalAmount = 189.99m,
                CreatedAt = new DateTime(2026, 6, 30, 14, 15, 0, DateTimeKind.Utc)
            },
            new Order
            {
                Id = 37,
                UserId = customerUser1Id,
                AddressId = 1,
                OrderDate = new DateTime(2026, 7, 2, 11, 20, 0, DateTimeKind.Utc),
                Status = OrderStatus.Completed.ToString(),
                TotalAmount = 676m,
                CreatedAt = new DateTime(2026, 7, 2, 11, 20, 0, DateTimeKind.Utc)
            },
            new Order
            {
                Id = 38,
                UserId = customerUser2Id,
                AddressId = 2,
                OrderDate = new DateTime(2026, 7, 5, 15, 0, 0, DateTimeKind.Utc),
                Status = OrderStatus.Completed.ToString(),
                TotalAmount = 600m,
                CreatedAt = new DateTime(2026, 7, 5, 15, 0, 0, DateTimeKind.Utc)
            },
            new Order
            {
                Id = 39,
                UserId = customerUser3Id,
                AddressId = 3,
                OrderDate = new DateTime(2026, 7, 8, 10, 45, 0, DateTimeKind.Utc),
                Status = OrderStatus.Completed.ToString(),
                TotalAmount = 382m,
                CreatedAt = new DateTime(2026, 7, 8, 10, 45, 0, DateTimeKind.Utc)
            },
            new Order
            {
                Id = 40,
                UserId = customerUser4Id,
                AddressId = 4,
                OrderDate = new DateTime(2026, 7, 11, 16, 30, 0, DateTimeKind.Utc),
                Status = OrderStatus.Completed.ToString(),
                TotalAmount = 375.97m,
                CreatedAt = new DateTime(2026, 7, 11, 16, 30, 0, DateTimeKind.Utc)
            },
            new Order
            {
                Id = 41,
                UserId = customerUser1Id,
                AddressId = 5,
                OrderDate = new DateTime(2026, 7, 14, 9, 15, 0, DateTimeKind.Utc),
                Status = OrderStatus.Completed.ToString(),
                TotalAmount = 290m,
                CreatedAt = new DateTime(2026, 7, 14, 9, 15, 0, DateTimeKind.Utc)
            },
            new Order
            {
                Id = 42,
                UserId = customerUser2Id,
                AddressId = 6,
                OrderDate = new DateTime(2026, 7, 17, 13, 40, 0, DateTimeKind.Utc),
                Status = OrderStatus.Completed.ToString(),
                TotalAmount = 270m,
                CreatedAt = new DateTime(2026, 7, 17, 13, 40, 0, DateTimeKind.Utc)
            },
            new Order
            {
                Id = 43,
                UserId = customerUser3Id,
                AddressId = 7,
                OrderDate = new DateTime(2026, 7, 19, 17, 50, 0, DateTimeKind.Utc),
                Status = OrderStatus.Completed.ToString(),
                TotalAmount = 437m,
                CreatedAt = new DateTime(2026, 7, 19, 17, 50, 0, DateTimeKind.Utc)
            },
            new Order
            {
                Id = 44,
                UserId = customerUser4Id,
                AddressId = 8,
                OrderDate = new DateTime(2026, 7, 22, 12, 0, 0, DateTimeKind.Utc),
                Status = OrderStatus.Completed.ToString(),
                TotalAmount = 371m,
                CreatedAt = new DateTime(2026, 7, 22, 12, 0, 0, DateTimeKind.Utc)
            },
            new Order
            {
                Id = 45,
                UserId = customerUser1Id,
                AddressId = 1,
                OrderDate = new DateTime(2026, 7, 24, 15, 25, 0, DateTimeKind.Utc),
                Status = OrderStatus.Completed.ToString(),
                TotalAmount = 358m,
                CreatedAt = new DateTime(2026, 7, 24, 15, 25, 0, DateTimeKind.Utc)
            },
            new Order
            {
                Id = 46,
                UserId = customerUser2Id,
                AddressId = 2,
                OrderDate = new DateTime(2026, 7, 26, 10, 10, 0, DateTimeKind.Utc),
                Status = OrderStatus.Completed.ToString(),
                TotalAmount = 197m,
                CreatedAt = new DateTime(2026, 7, 26, 10, 10, 0, DateTimeKind.Utc)
            },
            new Order
            {
                Id = 47,
                UserId = customerUser3Id,
                AddressId = 3,
                OrderDate = new DateTime(2026, 7, 28, 18, 0, 0, DateTimeKind.Utc),
                Status = OrderStatus.Completed.ToString(),
                TotalAmount = 252.99m,
                CreatedAt = new DateTime(2026, 7, 28, 18, 0, 0, DateTimeKind.Utc)
            },
            new Order
            {
                Id = 48,
                UserId = customerUser4Id,
                AddressId = 4,
                OrderDate = new DateTime(2026, 7, 29, 14, 30, 0, DateTimeKind.Utc),
                Status = OrderStatus.Completed.ToString(),
                TotalAmount = 244m,
                CreatedAt = new DateTime(2026, 7, 29, 14, 30, 0, DateTimeKind.Utc)
            },
            new Order
            {
                Id = 49,
                UserId = customerUser1Id,
                AddressId = 5,
                OrderDate = new DateTime(2026, 7, 30, 9, 40, 0, DateTimeKind.Utc),
                Status = OrderStatus.Completed.ToString(),
                TotalAmount = 229m,
                CreatedAt = new DateTime(2026, 7, 30, 9, 40, 0, DateTimeKind.Utc)
            },
            new Order
            {
                Id = 50,
                UserId = customerUser2Id,
                AddressId = 6,
                OrderDate = new DateTime(2026, 7, 31, 16, 15, 0, DateTimeKind.Utc),
                Status = OrderStatus.Completed.ToString(),
                TotalAmount = 345m,
                CreatedAt = new DateTime(2026, 7, 31, 16, 15, 0, DateTimeKind.Utc)
            },
            new Order
            {
                Id = 51,
                UserId = customerUser3Id,
                AddressId = 7,
                OrderDate = new DateTime(2026, 8, 2, 10, 0, 0, DateTimeKind.Utc),
                Status = OrderStatus.Completed.ToString(),
                TotalAmount = 708m,
                CreatedAt = new DateTime(2026, 8, 2, 10, 0, 0, DateTimeKind.Utc)
            },
            new Order
            {
                Id = 52,
                UserId = customerUser4Id,
                AddressId = 8,
                OrderDate = new DateTime(2026, 8, 4, 14, 15, 0, DateTimeKind.Utc),
                Status = OrderStatus.Completed.ToString(),
                TotalAmount = 657m,
                CreatedAt = new DateTime(2026, 8, 4, 14, 15, 0, DateTimeKind.Utc)
            },
            new Order
            {
                Id = 53,
                UserId = customerUser1Id,
                AddressId = 1,
                OrderDate = new DateTime(2026, 8, 7, 11, 30, 0, DateTimeKind.Utc),
                Status = OrderStatus.Completed.ToString(),
                TotalAmount = 409.98m,
                CreatedAt = new DateTime(2026, 8, 7, 11, 30, 0, DateTimeKind.Utc)
            },
            new Order
            {
                Id = 54,
                UserId = customerUser2Id,
                AddressId = 2,
                OrderDate = new DateTime(2026, 8, 9, 16, 50, 0, DateTimeKind.Utc),
                Status = OrderStatus.Completed.ToString(),
                TotalAmount = 348m,
                CreatedAt = new DateTime(2026, 8, 9, 16, 50, 0, DateTimeKind.Utc)
            },
            new Order
            {
                Id = 55,
                UserId = customerUser3Id,
                AddressId = 3,
                OrderDate = new DateTime(2026, 8, 11, 9, 20, 0, DateTimeKind.Utc),
                Status = OrderStatus.Completed.ToString(),
                TotalAmount = 256m,
                CreatedAt = new DateTime(2026, 8, 11, 9, 20, 0, DateTimeKind.Utc)
            },
            new Order
            {
                Id = 56,
                UserId = customerUser4Id,
                AddressId = 4,
                OrderDate = new DateTime(2026, 8, 13, 13, 45, 0, DateTimeKind.Utc),
                Status = OrderStatus.Completed.ToString(),
                TotalAmount = 473m,
                CreatedAt = new DateTime(2026, 8, 13, 13, 45, 0, DateTimeKind.Utc)
            },
            new Order
            {
                Id = 57,
                UserId = customerUser1Id,
                AddressId = 5,
                OrderDate = new DateTime(2026, 8, 15, 17, 10, 0, DateTimeKind.Utc),
                Status = OrderStatus.Completed.ToString(),
                TotalAmount = 512m,
                CreatedAt = new DateTime(2026, 8, 15, 17, 10, 0, DateTimeKind.Utc)
            },
            new Order
            {
                Id = 58,
                UserId = customerUser2Id,
                AddressId = 6,
                OrderDate = new DateTime(2026, 8, 17, 12, 0, 0, DateTimeKind.Utc),
                Status = OrderStatus.Completed.ToString(),
                TotalAmount = 303m,
                CreatedAt = new DateTime(2026, 8, 17, 12, 0, 0, DateTimeKind.Utc)
            },
            new Order
            {
                Id = 59,
                UserId = customerUser3Id,
                AddressId = 7,
                OrderDate = new DateTime(2026, 8, 18, 15, 30, 0, DateTimeKind.Utc),
                Status = OrderStatus.Completed.ToString(),
                TotalAmount = 244m,
                CreatedAt = new DateTime(2026, 8, 18, 15, 30, 0, DateTimeKind.Utc)
            },
            new Order
            {
                Id = 60,
                UserId = customerUser4Id,
                AddressId = 8,
                OrderDate = new DateTime(2026, 8, 20, 10, 15, 0, DateTimeKind.Utc),
                Status = OrderStatus.Completed.ToString(),
                TotalAmount = 350m,
                CreatedAt = new DateTime(2026, 8, 20, 10, 15, 0, DateTimeKind.Utc)
            },
            new Order
            {
                Id = 61,
                UserId = customerUser1Id,
                AddressId = 1,
                OrderDate = new DateTime(2026, 8, 21, 18, 0, 0, DateTimeKind.Utc),
                Status = OrderStatus.Completed.ToString(),
                TotalAmount = 249.48m,
                CreatedAt = new DateTime(2026, 8, 21, 18, 0, 0, DateTimeKind.Utc)
            },
            new Order
            {
                Id = 62,
                UserId = customerUser2Id,
                AddressId = 2,
                OrderDate = new DateTime(2026, 8, 22, 14, 20, 0, DateTimeKind.Utc),
                Status = OrderStatus.Completed.ToString(),
                TotalAmount = 348m,
                CreatedAt = new DateTime(2026, 8, 22, 14, 20, 0, DateTimeKind.Utc)
            },
            new Order
            {
                Id = 63,
                UserId = customerUser3Id,
                AddressId = 3,
                OrderDate = new DateTime(2026, 8, 23, 11, 10, 0, DateTimeKind.Utc),
                Status = OrderStatus.Processing.ToString(),
                TotalAmount = 228m,
                CreatedAt = new DateTime(2026, 8, 23, 11, 10, 0, DateTimeKind.Utc)
            },
            new Order
            {
                Id = 64,
                UserId = customerUser4Id,
                AddressId = 4,
                OrderDate = new DateTime(2026, 8, 24, 16, 40, 0, DateTimeKind.Utc),
                Status = OrderStatus.Processing.ToString(),
                TotalAmount = 146m,
                CreatedAt = new DateTime(2026, 8, 24, 16, 40, 0, DateTimeKind.Utc)
            },
            new Order
            {
                Id = 65,
                UserId = customerUser1Id,
                AddressId = 5,
                OrderDate = new DateTime(2026, 8, 25, 9, 30, 0, DateTimeKind.Utc),
                Status = OrderStatus.Processing.ToString(),
                TotalAmount = 254m,
                CreatedAt = new DateTime(2026, 8, 25, 9, 30, 0, DateTimeKind.Utc)
            },
            new Order
            {
                Id = 66,
                UserId = customerUser2Id,
                AddressId = 6,
                OrderDate = new DateTime(2026, 8, 25, 15, 0, 0, DateTimeKind.Utc),
                Status = OrderStatus.Processing.ToString(),
                TotalAmount = 162m,
                CreatedAt = new DateTime(2026, 8, 25, 15, 0, 0, DateTimeKind.Utc)
            }
        );

        // --- Seed Order Items ---
        builder.Entity<OrderItem>().HasData(
            new OrderItem { Id = 1, OrderId = 1, ProductId = 1, Quantity = 1, UnitPrice = 189m },
            new OrderItem { Id = 2, OrderId = 1, ProductId = 3, Quantity = 1, UnitPrice = 38m },
            new OrderItem { Id = 3, OrderId = 2, ProductId = 2, Quantity = 1, UnitPrice = 149m },
            new OrderItem { Id = 4, OrderId = 2, ProductId = 5, Quantity = 1, UnitPrice = 42m },
            new OrderItem { Id = 5, OrderId = 2, ProductId = 9, Quantity = 1, UnitPrice = 36m },
            new OrderItem { Id = 6, OrderId = 3, ProductId = 4, Quantity = 1, UnitPrice = 165m },
            new OrderItem { Id = 7, OrderId = 3, ProductId = 11, Quantity = 1, UnitPrice = 52m },
            new OrderItem { Id = 8, OrderId = 4, ProductId = 6, Quantity = 1, UnitPrice = 135m },
            new OrderItem { Id = 9, OrderId = 4, ProductId = 12, Quantity = 1, UnitPrice = 64m },
            new OrderItem { Id = 10, OrderId = 4, ProductId = 18, Quantity = 1, UnitPrice = 22m },
            new OrderItem { Id = 11, OrderId = 5, ProductId = 10, Quantity = 1, UnitPrice = 89m },
            new OrderItem { Id = 12, OrderId = 5, ProductId = 7, Quantity = 1, UnitPrice = 39m },
            new OrderItem { Id = 13, OrderId = 5, ProductId = 8, Quantity = 1, UnitPrice = 49m },
            new OrderItem { Id = 14, OrderId = 6, ProductId = 15, Quantity = 2, UnitPrice = 68m },
            new OrderItem { Id = 15, OrderId = 6, ProductId = 20, Quantity = 1, UnitPrice = 52m },
            new OrderItem { Id = 16, OrderId = 7, ProductId = 1, Quantity = 1, UnitPrice = 189m },
            new OrderItem { Id = 17, OrderId = 7, ProductId = 16, Quantity = 1, UnitPrice = 45m },
            new OrderItem { Id = 18, OrderId = 7, ProductId = 22, Quantity = 2, UnitPrice = 14m },
            new OrderItem { Id = 19, OrderId = 8, ProductId = 2, Quantity = 1, UnitPrice = 149m },
            new OrderItem { Id = 20, OrderId = 8, ProductId = 6, Quantity = 1, UnitPrice = 135m },
            new OrderItem { Id = 21, OrderId = 9, ProductId = 4, Quantity = 1, UnitPrice = 165m },
            new OrderItem { Id = 22, OrderId = 9, ProductId = 7, Quantity = 1, UnitPrice = 39m },
            new OrderItem { Id = 23, OrderId = 9, ProductId = 14, Quantity = 2, UnitPrice = 18.5m },
            new OrderItem { Id = 24, OrderId = 10, ProductId = 13, Quantity = 1, UnitPrice = 79.99m },
            new OrderItem { Id = 25, OrderId = 10, ProductId = 15, Quantity = 1, UnitPrice = 68m },
            new OrderItem { Id = 26, OrderId = 10, ProductId = 17, Quantity = 1, UnitPrice = 34m },
            new OrderItem { Id = 27, OrderId = 11, ProductId = 8, Quantity = 1, UnitPrice = 49m },
            new OrderItem { Id = 28, OrderId = 11, ProductId = 10, Quantity = 1, UnitPrice = 89m },
            new OrderItem { Id = 29, OrderId = 11, ProductId = 19, Quantity = 1, UnitPrice = 26m },
            new OrderItem { Id = 30, OrderId = 12, ProductId = 23, Quantity = 2, UnitPrice = 58m },
            new OrderItem { Id = 31, OrderId = 12, ProductId = 21, Quantity = 1, UnitPrice = 29.5m },
            new OrderItem { Id = 32, OrderId = 12, ProductId = 9, Quantity = 1, UnitPrice = 36m },
            new OrderItem { Id = 33, OrderId = 13, ProductId = 5, Quantity = 2, UnitPrice = 42m },
            new OrderItem { Id = 34, OrderId = 13, ProductId = 20, Quantity = 1, UnitPrice = 52m },
            new OrderItem { Id = 35, OrderId = 13, ProductId = 18, Quantity = 1, UnitPrice = 22m },
            new OrderItem { Id = 36, OrderId = 14, ProductId = 6, Quantity = 1, UnitPrice = 135m },
            new OrderItem { Id = 37, OrderId = 14, ProductId = 11, Quantity = 1, UnitPrice = 52m },
            new OrderItem { Id = 38, OrderId = 15, ProductId = 1, Quantity = 1, UnitPrice = 189m },
            new OrderItem { Id = 39, OrderId = 15, ProductId = 4, Quantity = 1, UnitPrice = 165m },
            new OrderItem { Id = 40, OrderId = 15, ProductId = 3, Quantity = 2, UnitPrice = 38m },
            new OrderItem { Id = 41, OrderId = 16, ProductId = 2, Quantity = 1, UnitPrice = 149m },
            new OrderItem { Id = 42, OrderId = 16, ProductId = 10, Quantity = 1, UnitPrice = 89m },
            new OrderItem { Id = 43, OrderId = 16, ProductId = 12, Quantity = 1, UnitPrice = 64m },
            new OrderItem { Id = 44, OrderId = 17, ProductId = 6, Quantity = 2, UnitPrice = 135m },
            new OrderItem { Id = 45, OrderId = 17, ProductId = 16, Quantity = 1, UnitPrice = 45m },
            new OrderItem { Id = 46, OrderId = 18, ProductId = 15, Quantity = 2, UnitPrice = 68m },
            new OrderItem { Id = 47, OrderId = 18, ProductId = 17, Quantity = 1, UnitPrice = 34m },
            new OrderItem { Id = 48, OrderId = 18, ProductId = 7, Quantity = 1, UnitPrice = 39m },
            new OrderItem { Id = 49, OrderId = 19, ProductId = 13, Quantity = 2, UnitPrice = 79.99m },
            new OrderItem { Id = 50, OrderId = 19, ProductId = 14, Quantity = 1, UnitPrice = 18.5m },
            new OrderItem { Id = 51, OrderId = 20, ProductId = 8, Quantity = 2, UnitPrice = 49m },
            new OrderItem { Id = 52, OrderId = 20, ProductId = 9, Quantity = 2, UnitPrice = 36m },
            new OrderItem { Id = 53, OrderId = 20, ProductId = 19, Quantity = 1, UnitPrice = 26m },
            new OrderItem { Id = 54, OrderId = 21, ProductId = 20, Quantity = 2, UnitPrice = 52m },
            new OrderItem { Id = 55, OrderId = 21, ProductId = 5, Quantity = 1, UnitPrice = 42m },
            new OrderItem { Id = 56, OrderId = 21, ProductId = 22, Quantity = 2, UnitPrice = 14m },
            new OrderItem { Id = 57, OrderId = 22, ProductId = 23, Quantity = 1, UnitPrice = 58m },
            new OrderItem { Id = 58, OrderId = 22, ProductId = 18, Quantity = 2, UnitPrice = 22m },
            new OrderItem { Id = 59, OrderId = 22, ProductId = 21, Quantity = 1, UnitPrice = 29.5m },
            new OrderItem { Id = 60, OrderId = 23, ProductId = 4, Quantity = 1, UnitPrice = 165m },
            new OrderItem { Id = 61, OrderId = 23, ProductId = 11, Quantity = 2, UnitPrice = 52m },
            new OrderItem { Id = 62, OrderId = 24, ProductId = 1, Quantity = 1, UnitPrice = 189m },
            new OrderItem { Id = 63, OrderId = 24, ProductId = 2, Quantity = 1, UnitPrice = 149m },
            new OrderItem { Id = 64, OrderId = 25, ProductId = 1, Quantity = 2, UnitPrice = 189m },
            new OrderItem { Id = 65, OrderId = 25, ProductId = 3, Quantity = 2, UnitPrice = 38m },
            new OrderItem { Id = 66, OrderId = 25, ProductId = 7, Quantity = 1, UnitPrice = 39m },
            new OrderItem { Id = 67, OrderId = 26, ProductId = 2, Quantity = 2, UnitPrice = 149m },
            new OrderItem { Id = 68, OrderId = 26, ProductId = 6, Quantity = 1, UnitPrice = 135m },
            new OrderItem { Id = 69, OrderId = 26, ProductId = 12, Quantity = 1, UnitPrice = 64m },
            new OrderItem { Id = 70, OrderId = 27, ProductId = 4, Quantity = 1, UnitPrice = 165m },
            new OrderItem { Id = 71, OrderId = 27, ProductId = 10, Quantity = 1, UnitPrice = 89m },
            new OrderItem { Id = 72, OrderId = 27, ProductId = 8, Quantity = 1, UnitPrice = 49m },
            new OrderItem { Id = 73, OrderId = 28, ProductId = 13, Quantity = 2, UnitPrice = 79.99m },
            new OrderItem { Id = 74, OrderId = 28, ProductId = 15, Quantity = 1, UnitPrice = 68m },
            new OrderItem { Id = 75, OrderId = 28, ProductId = 17, Quantity = 2, UnitPrice = 34m },
            new OrderItem { Id = 76, OrderId = 29, ProductId = 16, Quantity = 2, UnitPrice = 45m },
            new OrderItem { Id = 77, OrderId = 29, ProductId = 20, Quantity = 1, UnitPrice = 52m },
            new OrderItem { Id = 78, OrderId = 29, ProductId = 21, Quantity = 1, UnitPrice = 29.5m },
            new OrderItem { Id = 79, OrderId = 30, ProductId = 23, Quantity = 2, UnitPrice = 58m },
            new OrderItem { Id = 80, OrderId = 30, ProductId = 5, Quantity = 2, UnitPrice = 42m },
            new OrderItem { Id = 81, OrderId = 30, ProductId = 9, Quantity = 2, UnitPrice = 36m },
            new OrderItem { Id = 82, OrderId = 31, ProductId = 6, Quantity = 1, UnitPrice = 135m },
            new OrderItem { Id = 83, OrderId = 31, ProductId = 11, Quantity = 2, UnitPrice = 52m },
            new OrderItem { Id = 84, OrderId = 31, ProductId = 18, Quantity = 2, UnitPrice = 22m },
            new OrderItem { Id = 85, OrderId = 32, ProductId = 10, Quantity = 2, UnitPrice = 89m },
            new OrderItem { Id = 86, OrderId = 32, ProductId = 19, Quantity = 2, UnitPrice = 26m },
            new OrderItem { Id = 87, OrderId = 32, ProductId = 22, Quantity = 3, UnitPrice = 14m },
            new OrderItem { Id = 88, OrderId = 33, ProductId = 1, Quantity = 1, UnitPrice = 189m },
            new OrderItem { Id = 89, OrderId = 33, ProductId = 4, Quantity = 1, UnitPrice = 165m },
            new OrderItem { Id = 90, OrderId = 33, ProductId = 14, Quantity = 1, UnitPrice = 18.5m },
            new OrderItem { Id = 91, OrderId = 34, ProductId = 2, Quantity = 1, UnitPrice = 149m },
            new OrderItem { Id = 92, OrderId = 34, ProductId = 15, Quantity = 2, UnitPrice = 68m },
            new OrderItem { Id = 93, OrderId = 35, ProductId = 6, Quantity = 1, UnitPrice = 135m },
            new OrderItem { Id = 94, OrderId = 35, ProductId = 8, Quantity = 2, UnitPrice = 49m },
            new OrderItem { Id = 95, OrderId = 35, ProductId = 7, Quantity = 1, UnitPrice = 39m },
            new OrderItem { Id = 96, OrderId = 36, ProductId = 13, Quantity = 1, UnitPrice = 79.99m },
            new OrderItem { Id = 97, OrderId = 36, ProductId = 20, Quantity = 1, UnitPrice = 52m },
            new OrderItem { Id = 98, OrderId = 36, ProductId = 23, Quantity = 1, UnitPrice = 58m },
            new OrderItem { Id = 99, OrderId = 37, ProductId = 1, Quantity = 2, UnitPrice = 189m },
            new OrderItem { Id = 100, OrderId = 37, ProductId = 2, Quantity = 2, UnitPrice = 149m },
            new OrderItem { Id = 101, OrderId = 38, ProductId = 4, Quantity = 2, UnitPrice = 165m },
            new OrderItem { Id = 102, OrderId = 38, ProductId = 6, Quantity = 2, UnitPrice = 135m },
            new OrderItem { Id = 103, OrderId = 39, ProductId = 10, Quantity = 2, UnitPrice = 89m },
            new OrderItem { Id = 104, OrderId = 39, ProductId = 12, Quantity = 2, UnitPrice = 64m },
            new OrderItem { Id = 105, OrderId = 39, ProductId = 3, Quantity = 2, UnitPrice = 38m },
            new OrderItem { Id = 106, OrderId = 40, ProductId = 13, Quantity = 3, UnitPrice = 79.99m },
            new OrderItem { Id = 107, OrderId = 40, ProductId = 15, Quantity = 2, UnitPrice = 68m },
            new OrderItem { Id = 108, OrderId = 41, ProductId = 16, Quantity = 2, UnitPrice = 45m },
            new OrderItem { Id = 109, OrderId = 41, ProductId = 23, Quantity = 2, UnitPrice = 58m },
            new OrderItem { Id = 110, OrderId = 41, ProductId = 5, Quantity = 2, UnitPrice = 42m },
            new OrderItem { Id = 111, OrderId = 42, ProductId = 20, Quantity = 2, UnitPrice = 52m },
            new OrderItem { Id = 112, OrderId = 42, ProductId = 17, Quantity = 2, UnitPrice = 34m },
            new OrderItem { Id = 113, OrderId = 42, ProductId = 8, Quantity = 2, UnitPrice = 49m },
            new OrderItem { Id = 114, OrderId = 43, ProductId = 6, Quantity = 2, UnitPrice = 135m },
            new OrderItem { Id = 115, OrderId = 43, ProductId = 9, Quantity = 3, UnitPrice = 36m },
            new OrderItem { Id = 116, OrderId = 43, ProductId = 21, Quantity = 2, UnitPrice = 29.5m },
            new OrderItem { Id = 117, OrderId = 44, ProductId = 1, Quantity = 1, UnitPrice = 189m },
            new OrderItem { Id = 118, OrderId = 44, ProductId = 11, Quantity = 2, UnitPrice = 52m },
            new OrderItem { Id = 119, OrderId = 44, ProductId = 7, Quantity = 2, UnitPrice = 39m },
            new OrderItem { Id = 120, OrderId = 45, ProductId = 4, Quantity = 1, UnitPrice = 165m },
            new OrderItem { Id = 121, OrderId = 45, ProductId = 2, Quantity = 1, UnitPrice = 149m },
            new OrderItem { Id = 122, OrderId = 45, ProductId = 18, Quantity = 2, UnitPrice = 22m },
            new OrderItem { Id = 123, OrderId = 46, ProductId = 10, Quantity = 1, UnitPrice = 89m },
            new OrderItem { Id = 124, OrderId = 46, ProductId = 19, Quantity = 2, UnitPrice = 26m },
            new OrderItem { Id = 125, OrderId = 46, ProductId = 22, Quantity = 4, UnitPrice = 14m },
            new OrderItem { Id = 126, OrderId = 47, ProductId = 15, Quantity = 2, UnitPrice = 68m },
            new OrderItem { Id = 127, OrderId = 47, ProductId = 13, Quantity = 1, UnitPrice = 79.99m },
            new OrderItem { Id = 128, OrderId = 47, ProductId = 14, Quantity = 2, UnitPrice = 18.5m },
            new OrderItem { Id = 129, OrderId = 48, ProductId = 23, Quantity = 2, UnitPrice = 58m },
            new OrderItem { Id = 130, OrderId = 48, ProductId = 12, Quantity = 2, UnitPrice = 64m },
            new OrderItem { Id = 131, OrderId = 49, ProductId = 6, Quantity = 1, UnitPrice = 135m },
            new OrderItem { Id = 132, OrderId = 49, ProductId = 16, Quantity = 1, UnitPrice = 45m },
            new OrderItem { Id = 133, OrderId = 49, ProductId = 8, Quantity = 1, UnitPrice = 49m },
            new OrderItem { Id = 134, OrderId = 50, ProductId = 1, Quantity = 1, UnitPrice = 189m },
            new OrderItem { Id = 135, OrderId = 50, ProductId = 5, Quantity = 2, UnitPrice = 42m },
            new OrderItem { Id = 136, OrderId = 50, ProductId = 9, Quantity = 2, UnitPrice = 36m },
            new OrderItem { Id = 137, OrderId = 51, ProductId = 1, Quantity = 2, UnitPrice = 189m },
            new OrderItem { Id = 138, OrderId = 51, ProductId = 4, Quantity = 2, UnitPrice = 165m },
            new OrderItem { Id = 139, OrderId = 52, ProductId = 2, Quantity = 2, UnitPrice = 149m },
            new OrderItem { Id = 140, OrderId = 52, ProductId = 6, Quantity = 2, UnitPrice = 135m },
            new OrderItem { Id = 141, OrderId = 52, ProductId = 10, Quantity = 1, UnitPrice = 89m },
            new OrderItem { Id = 142, OrderId = 53, ProductId = 13, Quantity = 2, UnitPrice = 79.99m },
            new OrderItem { Id = 143, OrderId = 53, ProductId = 15, Quantity = 2, UnitPrice = 68m },
            new OrderItem { Id = 144, OrderId = 53, ProductId = 3, Quantity = 3, UnitPrice = 38m },
            new OrderItem { Id = 145, OrderId = 54, ProductId = 23, Quantity = 2, UnitPrice = 58m },
            new OrderItem { Id = 146, OrderId = 54, ProductId = 12, Quantity = 2, UnitPrice = 64m },
            new OrderItem { Id = 147, OrderId = 54, ProductId = 20, Quantity = 2, UnitPrice = 52m },
            new OrderItem { Id = 148, OrderId = 55, ProductId = 16, Quantity = 2, UnitPrice = 45m },
            new OrderItem { Id = 149, OrderId = 55, ProductId = 17, Quantity = 2, UnitPrice = 34m },
            new OrderItem { Id = 150, OrderId = 55, ProductId = 8, Quantity = 2, UnitPrice = 49m },
            new OrderItem { Id = 151, OrderId = 56, ProductId = 1, Quantity = 1, UnitPrice = 189m },
            new OrderItem { Id = 152, OrderId = 56, ProductId = 2, Quantity = 1, UnitPrice = 149m },
            new OrderItem { Id = 153, OrderId = 56, ProductId = 6, Quantity = 1, UnitPrice = 135m },
            new OrderItem { Id = 154, OrderId = 57, ProductId = 4, Quantity = 2, UnitPrice = 165m },
            new OrderItem { Id = 155, OrderId = 57, ProductId = 11, Quantity = 2, UnitPrice = 52m },
            new OrderItem { Id = 156, OrderId = 57, ProductId = 7, Quantity = 2, UnitPrice = 39m },
            new OrderItem { Id = 157, OrderId = 58, ProductId = 10, Quantity = 2, UnitPrice = 89m },
            new OrderItem { Id = 158, OrderId = 58, ProductId = 21, Quantity = 2, UnitPrice = 29.5m },
            new OrderItem { Id = 159, OrderId = 58, ProductId = 18, Quantity = 3, UnitPrice = 22m },
            new OrderItem { Id = 160, OrderId = 59, ProductId = 15, Quantity = 2, UnitPrice = 68m },
            new OrderItem { Id = 161, OrderId = 59, ProductId = 19, Quantity = 2, UnitPrice = 26m },
            new OrderItem { Id = 162, OrderId = 59, ProductId = 22, Quantity = 4, UnitPrice = 14m },
            new OrderItem { Id = 163, OrderId = 60, ProductId = 23, Quantity = 2, UnitPrice = 58m },
            new OrderItem { Id = 164, OrderId = 60, ProductId = 5, Quantity = 3, UnitPrice = 42m },
            new OrderItem { Id = 165, OrderId = 60, ProductId = 9, Quantity = 3, UnitPrice = 36m },
            new OrderItem { Id = 166, OrderId = 61, ProductId = 13, Quantity = 2, UnitPrice = 79.99m },
            new OrderItem { Id = 167, OrderId = 61, ProductId = 14, Quantity = 3, UnitPrice = 18.5m },
            new OrderItem { Id = 168, OrderId = 61, ProductId = 17, Quantity = 1, UnitPrice = 34m },
            new OrderItem { Id = 169, OrderId = 62, ProductId = 2, Quantity = 1, UnitPrice = 149m },
            new OrderItem { Id = 170, OrderId = 62, ProductId = 6, Quantity = 1, UnitPrice = 135m },
            new OrderItem { Id = 171, OrderId = 62, ProductId = 12, Quantity = 1, UnitPrice = 64m },
            new OrderItem { Id = 172, OrderId = 63, ProductId = 1, Quantity = 1, UnitPrice = 189m },
            new OrderItem { Id = 173, OrderId = 63, ProductId = 7, Quantity = 1, UnitPrice = 39m },
            new OrderItem { Id = 174, OrderId = 64, ProductId = 16, Quantity = 1, UnitPrice = 45m },
            new OrderItem { Id = 175, OrderId = 64, ProductId = 20, Quantity = 1, UnitPrice = 52m },
            new OrderItem { Id = 176, OrderId = 64, ProductId = 8, Quantity = 1, UnitPrice = 49m },
            new OrderItem { Id = 177, OrderId = 65, ProductId = 4, Quantity = 1, UnitPrice = 165m },
            new OrderItem { Id = 178, OrderId = 65, ProductId = 10, Quantity = 1, UnitPrice = 89m },
            new OrderItem { Id = 179, OrderId = 66, ProductId = 23, Quantity = 1, UnitPrice = 58m },
            new OrderItem { Id = 180, OrderId = 66, ProductId = 15, Quantity = 1, UnitPrice = 68m },
            new OrderItem { Id = 181, OrderId = 66, ProductId = 9, Quantity = 1, UnitPrice = 36m }
        );

        // --- Seed Payments ---
        builder.Entity<Payment>().HasData(
            new Payment
            {
                Id = 1,
                OrderId = 1,
                TransactionId = "TXN-100373",
                Amount = 227m,
                PaymentMethod = "Credit Card",
                Status = "Completed",
                PaidAt = new DateTime(2026, 3, 5, 14, 20, 0, DateTimeKind.Utc)
            },
            new Payment
            {
                Id = 2,
                OrderId = 2,
                TransactionId = "TXN-100746",
                Amount = 227m,
                PaymentMethod = "PayPal",
                Status = "Completed",
                PaidAt = new DateTime(2026, 3, 10, 11, 15, 0, DateTimeKind.Utc)
            },
            new Payment
            {
                Id = 3,
                OrderId = 3,
                TransactionId = "TXN-101119",
                Amount = 217m,
                PaymentMethod = "Credit Card",
                Status = "Completed",
                PaidAt = new DateTime(2026, 3, 16, 16, 40, 0, DateTimeKind.Utc)
            },
            new Payment
            {
                Id = 4,
                OrderId = 4,
                TransactionId = "TXN-101492",
                Amount = 221m,
                PaymentMethod = "Apple Pay",
                Status = "Completed",
                PaidAt = new DateTime(2026, 3, 21, 9, 30, 0, DateTimeKind.Utc)
            },
            new Payment
            {
                Id = 5,
                OrderId = 5,
                TransactionId = "TXN-101865",
                Amount = 177m,
                PaymentMethod = "Credit Card",
                Status = "Completed",
                PaidAt = new DateTime(2026, 3, 26, 13, 0, 0, DateTimeKind.Utc)
            },
            new Payment
            {
                Id = 6,
                OrderId = 6,
                TransactionId = "TXN-102238",
                Amount = 188m,
                PaymentMethod = "Credit Card",
                Status = "Failed",
                PaidAt = null
            },
            new Payment
            {
                Id = 7,
                OrderId = 7,
                TransactionId = "TXN-102611",
                Amount = 262m,
                PaymentMethod = "Credit Card",
                Status = "Completed",
                PaidAt = new DateTime(2026, 4, 3, 10, 0, 0, DateTimeKind.Utc)
            },
            new Payment
            {
                Id = 8,
                OrderId = 8,
                TransactionId = "TXN-102984",
                Amount = 284m,
                PaymentMethod = "PayPal",
                Status = "Completed",
                PaidAt = new DateTime(2026, 4, 7, 15, 25, 0, DateTimeKind.Utc)
            },
            new Payment
            {
                Id = 9,
                OrderId = 9,
                TransactionId = "TXN-103357",
                Amount = 241m,
                PaymentMethod = "Apple Pay",
                Status = "Completed",
                PaidAt = new DateTime(2026, 4, 12, 12, 10, 0, DateTimeKind.Utc)
            },
            new Payment
            {
                Id = 10,
                OrderId = 10,
                TransactionId = "TXN-103730",
                Amount = 181.99m,
                PaymentMethod = "Credit Card",
                Status = "Completed",
                PaidAt = new DateTime(2026, 4, 16, 17, 45, 0, DateTimeKind.Utc)
            },
            new Payment
            {
                Id = 11,
                OrderId = 11,
                TransactionId = "TXN-104103",
                Amount = 164m,
                PaymentMethod = "Cash on Delivery",
                Status = "Completed",
                PaidAt = new DateTime(2026, 4, 20, 11, 30, 0, DateTimeKind.Utc)
            },
            new Payment
            {
                Id = 12,
                OrderId = 12,
                TransactionId = "TXN-104476",
                Amount = 181.5m,
                PaymentMethod = "Credit Card",
                Status = "Completed",
                PaidAt = new DateTime(2026, 4, 23, 14, 0, 0, DateTimeKind.Utc)
            },
            new Payment
            {
                Id = 13,
                OrderId = 13,
                TransactionId = "TXN-104849",
                Amount = 158m,
                PaymentMethod = "PayPal",
                Status = "Completed",
                PaidAt = new DateTime(2026, 4, 27, 16, 20, 0, DateTimeKind.Utc)
            },
            new Payment
            {
                Id = 14,
                OrderId = 14,
                TransactionId = "TXN-105222",
                Amount = 187m,
                PaymentMethod = "Credit Card",
                Status = "Completed",
                PaidAt = new DateTime(2026, 4, 29, 9, 50, 0, DateTimeKind.Utc)
            },
            new Payment
            {
                Id = 15,
                OrderId = 15,
                TransactionId = "TXN-105595",
                Amount = 430m,
                PaymentMethod = "Credit Card",
                Status = "Completed",
                PaidAt = new DateTime(2026, 5, 2, 11, 0, 0, DateTimeKind.Utc)
            },
            new Payment
            {
                Id = 16,
                OrderId = 16,
                TransactionId = "TXN-105968",
                Amount = 302m,
                PaymentMethod = "Apple Pay",
                Status = "Completed",
                PaidAt = new DateTime(2026, 5, 5, 14, 30, 0, DateTimeKind.Utc)
            },
            new Payment
            {
                Id = 17,
                OrderId = 17,
                TransactionId = "TXN-106341",
                Amount = 315m,
                PaymentMethod = "Credit Card",
                Status = "Completed",
                PaidAt = new DateTime(2026, 5, 9, 10, 15, 0, DateTimeKind.Utc)
            },
            new Payment
            {
                Id = 18,
                OrderId = 18,
                TransactionId = "TXN-106714",
                Amount = 209m,
                PaymentMethod = "PayPal",
                Status = "Completed",
                PaidAt = new DateTime(2026, 5, 13, 16, 0, 0, DateTimeKind.Utc)
            },
            new Payment
            {
                Id = 19,
                OrderId = 19,
                TransactionId = "TXN-107087",
                Amount = 178.48m,
                PaymentMethod = "Credit Card",
                Status = "Completed",
                PaidAt = new DateTime(2026, 5, 16, 13, 40, 0, DateTimeKind.Utc)
            },
            new Payment
            {
                Id = 20,
                OrderId = 20,
                TransactionId = "TXN-107460",
                Amount = 196m,
                PaymentMethod = "Apple Pay",
                Status = "Completed",
                PaidAt = new DateTime(2026, 5, 20, 9, 20, 0, DateTimeKind.Utc)
            },
            new Payment
            {
                Id = 21,
                OrderId = 21,
                TransactionId = "TXN-107833",
                Amount = 174m,
                PaymentMethod = "Credit Card",
                Status = "Completed",
                PaidAt = new DateTime(2026, 5, 23, 15, 10, 0, DateTimeKind.Utc)
            },
            new Payment
            {
                Id = 22,
                OrderId = 22,
                TransactionId = "TXN-108206",
                Amount = 131.5m,
                PaymentMethod = "PayPal",
                Status = "Completed",
                PaidAt = new DateTime(2026, 5, 26, 17, 35, 0, DateTimeKind.Utc)
            },
            new Payment
            {
                Id = 23,
                OrderId = 23,
                TransactionId = "TXN-108579",
                Amount = 269m,
                PaymentMethod = "Credit Card",
                Status = "Completed",
                PaidAt = new DateTime(2026, 5, 28, 12, 0, 0, DateTimeKind.Utc)
            },
            new Payment
            {
                Id = 24,
                OrderId = 24,
                TransactionId = "TXN-108952",
                Amount = 338m,
                PaymentMethod = "Credit Card",
                Status = "Failed",
                PaidAt = null
            },
            new Payment
            {
                Id = 25,
                OrderId = 25,
                TransactionId = "TXN-109325",
                Amount = 493m,
                PaymentMethod = "Credit Card",
                Status = "Completed",
                PaidAt = new DateTime(2026, 6, 3, 10, 10, 0, DateTimeKind.Utc)
            },
            new Payment
            {
                Id = 26,
                OrderId = 26,
                TransactionId = "TXN-109698",
                Amount = 497m,
                PaymentMethod = "Apple Pay",
                Status = "Completed",
                PaidAt = new DateTime(2026, 6, 6, 14, 40, 0, DateTimeKind.Utc)
            },
            new Payment
            {
                Id = 27,
                OrderId = 27,
                TransactionId = "TXN-110071",
                Amount = 303m,
                PaymentMethod = "PayPal",
                Status = "Completed",
                PaidAt = new DateTime(2026, 6, 9, 11, 25, 0, DateTimeKind.Utc)
            },
            new Payment
            {
                Id = 28,
                OrderId = 28,
                TransactionId = "TXN-110444",
                Amount = 295.98m,
                PaymentMethod = "Credit Card",
                Status = "Completed",
                PaidAt = new DateTime(2026, 6, 12, 16, 0, 0, DateTimeKind.Utc)
            },
            new Payment
            {
                Id = 29,
                OrderId = 29,
                TransactionId = "TXN-110817",
                Amount = 171.5m,
                PaymentMethod = "Apple Pay",
                Status = "Completed",
                PaidAt = new DateTime(2026, 6, 15, 9, 30, 0, DateTimeKind.Utc)
            },
            new Payment
            {
                Id = 30,
                OrderId = 30,
                TransactionId = "TXN-111190",
                Amount = 272m,
                PaymentMethod = "Credit Card",
                Status = "Completed",
                PaidAt = new DateTime(2026, 6, 18, 13, 50, 0, DateTimeKind.Utc)
            },
            new Payment
            {
                Id = 31,
                OrderId = 31,
                TransactionId = "TXN-111563",
                Amount = 283m,
                PaymentMethod = "PayPal",
                Status = "Completed",
                PaidAt = new DateTime(2026, 6, 21, 17, 15, 0, DateTimeKind.Utc)
            },
            new Payment
            {
                Id = 32,
                OrderId = 32,
                TransactionId = "TXN-111936",
                Amount = 272m,
                PaymentMethod = "Credit Card",
                Status = "Completed",
                PaidAt = new DateTime(2026, 6, 24, 12, 20, 0, DateTimeKind.Utc)
            },
            new Payment
            {
                Id = 33,
                OrderId = 33,
                TransactionId = "TXN-112309",
                Amount = 372.5m,
                PaymentMethod = "Apple Pay",
                Status = "Completed",
                PaidAt = new DateTime(2026, 6, 26, 15, 45, 0, DateTimeKind.Utc)
            },
            new Payment
            {
                Id = 34,
                OrderId = 34,
                TransactionId = "TXN-112682",
                Amount = 285m,
                PaymentMethod = "Credit Card",
                Status = "Completed",
                PaidAt = new DateTime(2026, 6, 28, 10, 0, 0, DateTimeKind.Utc)
            },
            new Payment
            {
                Id = 35,
                OrderId = 35,
                TransactionId = "TXN-113055",
                Amount = 272m,
                PaymentMethod = "PayPal",
                Status = "Completed",
                PaidAt = new DateTime(2026, 6, 29, 18, 30, 0, DateTimeKind.Utc)
            },
            new Payment
            {
                Id = 36,
                OrderId = 36,
                TransactionId = "TXN-113428",
                Amount = 189.99m,
                PaymentMethod = "Credit Card",
                Status = "Completed",
                PaidAt = new DateTime(2026, 6, 30, 14, 15, 0, DateTimeKind.Utc)
            },
            new Payment
            {
                Id = 37,
                OrderId = 37,
                TransactionId = "TXN-113801",
                Amount = 676m,
                PaymentMethod = "Credit Card",
                Status = "Completed",
                PaidAt = new DateTime(2026, 7, 2, 11, 20, 0, DateTimeKind.Utc)
            },
            new Payment
            {
                Id = 38,
                OrderId = 38,
                TransactionId = "TXN-114174",
                Amount = 600m,
                PaymentMethod = "Apple Pay",
                Status = "Completed",
                PaidAt = new DateTime(2026, 7, 5, 15, 0, 0, DateTimeKind.Utc)
            },
            new Payment
            {
                Id = 39,
                OrderId = 39,
                TransactionId = "TXN-114547",
                Amount = 382m,
                PaymentMethod = "PayPal",
                Status = "Completed",
                PaidAt = new DateTime(2026, 7, 8, 10, 45, 0, DateTimeKind.Utc)
            },
            new Payment
            {
                Id = 40,
                OrderId = 40,
                TransactionId = "TXN-114920",
                Amount = 375.97m,
                PaymentMethod = "Credit Card",
                Status = "Completed",
                PaidAt = new DateTime(2026, 7, 11, 16, 30, 0, DateTimeKind.Utc)
            },
            new Payment
            {
                Id = 41,
                OrderId = 41,
                TransactionId = "TXN-115293",
                Amount = 290m,
                PaymentMethod = "Apple Pay",
                Status = "Completed",
                PaidAt = new DateTime(2026, 7, 14, 9, 15, 0, DateTimeKind.Utc)
            },
            new Payment
            {
                Id = 42,
                OrderId = 42,
                TransactionId = "TXN-115666",
                Amount = 270m,
                PaymentMethod = "Credit Card",
                Status = "Completed",
                PaidAt = new DateTime(2026, 7, 17, 13, 40, 0, DateTimeKind.Utc)
            },
            new Payment
            {
                Id = 43,
                OrderId = 43,
                TransactionId = "TXN-116039",
                Amount = 437m,
                PaymentMethod = "PayPal",
                Status = "Completed",
                PaidAt = new DateTime(2026, 7, 19, 17, 50, 0, DateTimeKind.Utc)
            },
            new Payment
            {
                Id = 44,
                OrderId = 44,
                TransactionId = "TXN-116412",
                Amount = 371m,
                PaymentMethod = "Credit Card",
                Status = "Completed",
                PaidAt = new DateTime(2026, 7, 22, 12, 0, 0, DateTimeKind.Utc)
            },
            new Payment
            {
                Id = 45,
                OrderId = 45,
                TransactionId = "TXN-116785",
                Amount = 358m,
                PaymentMethod = "Apple Pay",
                Status = "Completed",
                PaidAt = new DateTime(2026, 7, 24, 15, 25, 0, DateTimeKind.Utc)
            },
            new Payment
            {
                Id = 46,
                OrderId = 46,
                TransactionId = "TXN-117158",
                Amount = 197m,
                PaymentMethod = "Credit Card",
                Status = "Completed",
                PaidAt = new DateTime(2026, 7, 26, 10, 10, 0, DateTimeKind.Utc)
            },
            new Payment
            {
                Id = 47,
                OrderId = 47,
                TransactionId = "TXN-117531",
                Amount = 252.99m,
                PaymentMethod = "PayPal",
                Status = "Completed",
                PaidAt = new DateTime(2026, 7, 28, 18, 0, 0, DateTimeKind.Utc)
            },
            new Payment
            {
                Id = 48,
                OrderId = 48,
                TransactionId = "TXN-117904",
                Amount = 244m,
                PaymentMethod = "Credit Card",
                Status = "Completed",
                PaidAt = new DateTime(2026, 7, 29, 14, 30, 0, DateTimeKind.Utc)
            },
            new Payment
            {
                Id = 49,
                OrderId = 49,
                TransactionId = "TXN-118277",
                Amount = 229m,
                PaymentMethod = "Apple Pay",
                Status = "Completed",
                PaidAt = new DateTime(2026, 7, 30, 9, 40, 0, DateTimeKind.Utc)
            },
            new Payment
            {
                Id = 50,
                OrderId = 50,
                TransactionId = "TXN-118650",
                Amount = 345m,
                PaymentMethod = "Credit Card",
                Status = "Completed",
                PaidAt = new DateTime(2026, 7, 31, 16, 15, 0, DateTimeKind.Utc)
            },
            new Payment
            {
                Id = 51,
                OrderId = 51,
                TransactionId = "TXN-119023",
                Amount = 708m,
                PaymentMethod = "Credit Card",
                Status = "Completed",
                PaidAt = new DateTime(2026, 8, 2, 10, 0, 0, DateTimeKind.Utc)
            },
            new Payment
            {
                Id = 52,
                OrderId = 52,
                TransactionId = "TXN-119396",
                Amount = 657m,
                PaymentMethod = "Apple Pay",
                Status = "Completed",
                PaidAt = new DateTime(2026, 8, 4, 14, 15, 0, DateTimeKind.Utc)
            },
            new Payment
            {
                Id = 53,
                OrderId = 53,
                TransactionId = "TXN-119769",
                Amount = 409.98m,
                PaymentMethod = "PayPal",
                Status = "Completed",
                PaidAt = new DateTime(2026, 8, 7, 11, 30, 0, DateTimeKind.Utc)
            },
            new Payment
            {
                Id = 54,
                OrderId = 54,
                TransactionId = "TXN-120142",
                Amount = 348m,
                PaymentMethod = "Credit Card",
                Status = "Completed",
                PaidAt = new DateTime(2026, 8, 9, 16, 50, 0, DateTimeKind.Utc)
            },
            new Payment
            {
                Id = 55,
                OrderId = 55,
                TransactionId = "TXN-120515",
                Amount = 256m,
                PaymentMethod = "Apple Pay",
                Status = "Completed",
                PaidAt = new DateTime(2026, 8, 11, 9, 20, 0, DateTimeKind.Utc)
            },
            new Payment
            {
                Id = 56,
                OrderId = 56,
                TransactionId = "TXN-120888",
                Amount = 473m,
                PaymentMethod = "Credit Card",
                Status = "Completed",
                PaidAt = new DateTime(2026, 8, 13, 13, 45, 0, DateTimeKind.Utc)
            },
            new Payment
            {
                Id = 57,
                OrderId = 57,
                TransactionId = "TXN-121261",
                Amount = 512m,
                PaymentMethod = "PayPal",
                Status = "Completed",
                PaidAt = new DateTime(2026, 8, 15, 17, 10, 0, DateTimeKind.Utc)
            },
            new Payment
            {
                Id = 58,
                OrderId = 58,
                TransactionId = "TXN-121634",
                Amount = 303m,
                PaymentMethod = "Credit Card",
                Status = "Completed",
                PaidAt = new DateTime(2026, 8, 17, 12, 0, 0, DateTimeKind.Utc)
            },
            new Payment
            {
                Id = 59,
                OrderId = 59,
                TransactionId = "TXN-122007",
                Amount = 244m,
                PaymentMethod = "Apple Pay",
                Status = "Completed",
                PaidAt = new DateTime(2026, 8, 18, 15, 30, 0, DateTimeKind.Utc)
            },
            new Payment
            {
                Id = 60,
                OrderId = 60,
                TransactionId = "TXN-122380",
                Amount = 350m,
                PaymentMethod = "Credit Card",
                Status = "Completed",
                PaidAt = new DateTime(2026, 8, 20, 10, 15, 0, DateTimeKind.Utc)
            },
            new Payment
            {
                Id = 61,
                OrderId = 61,
                TransactionId = "TXN-122753",
                Amount = 249.48m,
                PaymentMethod = "PayPal",
                Status = "Completed",
                PaidAt = new DateTime(2026, 8, 21, 18, 0, 0, DateTimeKind.Utc)
            },
            new Payment
            {
                Id = 62,
                OrderId = 62,
                TransactionId = "TXN-123126",
                Amount = 348m,
                PaymentMethod = "Credit Card",
                Status = "Completed",
                PaidAt = new DateTime(2026, 8, 22, 14, 20, 0, DateTimeKind.Utc)
            },
            new Payment
            {
                Id = 63,
                OrderId = 63,
                TransactionId = "TXN-123499",
                Amount = 228m,
                PaymentMethod = "Cash on Delivery",
                Status = "Pending",
                PaidAt = null
            },
            new Payment
            {
                Id = 64,
                OrderId = 64,
                TransactionId = "TXN-123872",
                Amount = 146m,
                PaymentMethod = "Credit Card",
                Status = "Completed",
                PaidAt = new DateTime(2026, 8, 24, 16, 40, 0, DateTimeKind.Utc)
            },
            new Payment
            {
                Id = 65,
                OrderId = 65,
                TransactionId = "TXN-124245",
                Amount = 254m,
                PaymentMethod = "Apple Pay",
                Status = "Completed",
                PaidAt = new DateTime(2026, 8, 25, 9, 30, 0, DateTimeKind.Utc)
            },
            new Payment
            {
                Id = 66,
                OrderId = 66,
                TransactionId = "TXN-124618",
                Amount = 162m,
                PaymentMethod = "PayPal",
                Status = "Completed",
                PaidAt = new DateTime(2026, 8, 25, 15, 0, 0, DateTimeKind.Utc)
            }
        );


        // --- Seed Reviews (72 Reviews across all 23 Products with Alternating Ratings) ---
        builder.Entity<Review>().HasData(
            new Review
            {
                Id = 1,
                UserId = customerUser1Id,
                ProductId = 1,
                Rating = 5,
                Comment = "Outstanding 4-person tent! Pitched it during heavy rain in the mountains and stayed bone dry. Setup took under 5 minutes.",
                Status = AdminResponse.Accepted.ToString(),
                CreatedAt = new DateTime(2026, 8, 10, 10, 0, 0, DateTimeKind.Utc),
                ApprovedAt = new DateTime(2026, 8, 10, 11, 0, 0, DateTimeKind.Utc)
            },
            new Review
            {
                Id = 2,
                UserId = customerUser2Id,
                ProductId = 1,
                Rating = 5,
                Comment = "Spacious vestibules easily accommodated 4 large expedition packs. Superb ventilation even in warm summer evenings.",
                Status = AdminResponse.Accepted.ToString(),
                CreatedAt = new DateTime(2026, 8, 12, 14, 30, 0, DateTimeKind.Utc),
                ApprovedAt = new DateTime(2026, 8, 12, 15, 30, 0, DateTimeKind.Utc)
            },
            new Review
            {
                Id = 3,
                UserId = customerUser3Id,
                ProductId = 1,
                Rating = 4,
                Comment = "High quality geodesic frame withstood gusty desert winds in Wadi Rum without flexing. Great tent!",
                Status = AdminResponse.Accepted.ToString(),
                CreatedAt = new DateTime(2026, 8, 15, 9, 15, 0, DateTimeKind.Utc),
                ApprovedAt = new DateTime(2026, 8, 15, 10, 15, 0, DateTimeKind.Utc)
            },
            new Review
            {
                Id = 4,
                UserId = customerUser2Id,
                ProductId = 2,
                Rating = 4,
                Comment = "The weight distribution on this 65L pack is incredible. The lumbar support saved my back on a 4-day ridge traverse.",
                Status = AdminResponse.Accepted.ToString(),
                CreatedAt = new DateTime(2026, 8, 11, 11, 0, 0, DateTimeKind.Utc),
                ApprovedAt = new DateTime(2026, 8, 11, 12, 0, 0, DateTimeKind.Utc)
            },
            new Review
            {
                Id = 5,
                UserId = customerUser4Id,
                ProductId = 2,
                Rating = 5,
                Comment = "Top quality Cordura fabric and waterproof rain cover included. Carried 22kg effortlessly.",
                Status = AdminResponse.Accepted.ToString(),
                CreatedAt = new DateTime(2026, 8, 13, 16, 45, 0, DateTimeKind.Utc),
                ApprovedAt = new DateTime(2026, 8, 13, 17, 45, 0, DateTimeKind.Utc)
            },
            new Review
            {
                Id = 6,
                UserId = customerUser1Id,
                ProductId = 2,
                Rating = 4,
                Comment = "Multiple attachment loops for trekking poles and ice axes. Sturdy hip belt padding.",
                Status = AdminResponse.Accepted.ToString(),
                CreatedAt = new DateTime(2026, 8, 16, 13, 20, 0, DateTimeKind.Utc),
                ApprovedAt = new DateTime(2026, 8, 16, 14, 20, 0, DateTimeKind.Utc)
            },
            new Review
            {
                Id = 7,
                UserId = customerUser3Id,
                ProductId = 3,
                Rating = 5,
                Comment = "Surprisingly sturdy for how small it folds down. Fits right into the bottle pocket of my backpack.",
                Status = AdminResponse.Accepted.ToString(),
                CreatedAt = new DateTime(2026, 8, 9, 15, 10, 0, DateTimeKind.Utc),
                ApprovedAt = new DateTime(2026, 8, 9, 16, 10, 0, DateTimeKind.Utc)
            },
            new Review
            {
                Id = 8,
                UserId = customerUser2Id,
                ProductId = 3,
                Rating = 4,
                Comment = "Supports my weight without creaking. Used it during lunch breaks on every mountain hike.",
                Status = AdminResponse.Accepted.ToString(),
                CreatedAt = new DateTime(2026, 8, 14, 18, 0, 0, DateTimeKind.Utc),
                ApprovedAt = new DateTime(2026, 8, 14, 19, 0, 0, DateTimeKind.Utc)
            },
            new Review
            {
                Id = 9,
                UserId = customerUser4Id,
                ProductId = 3,
                Rating = 4,
                Comment = "Ultra-lightweight aviation aluminum frame. Takes 3 seconds to unfold.",
                Status = AdminResponse.Accepted.ToString(),
                CreatedAt = new DateTime(2026, 8, 17, 12, 40, 0, DateTimeKind.Utc),
                ApprovedAt = new DateTime(2026, 8, 17, 13, 40, 0, DateTimeKind.Utc)
            },
            new Review
            {
                Id = 10,
                UserId = customerUser1Id,
                ProductId = 4,
                Rating = 5,
                Comment = "Super warm goose down sleeping bag for sub-zero mountain nights. Extremely compact when compressed.",
                Status = AdminResponse.Accepted.ToString(),
                CreatedAt = new DateTime(2026, 8, 8, 8, 30, 0, DateTimeKind.Utc),
                ApprovedAt = new DateTime(2026, 8, 8, 9, 30, 0, DateTimeKind.Utc)
            },
            new Review
            {
                Id = 11,
                UserId = customerUser3Id,
                ProductId = 4,
                Rating = 4,
                Comment = "The 750-fill power loft is unmatched. Draft collar kept body heat sealed in during a freezing night.",
                Status = AdminResponse.Accepted.ToString(),
                CreatedAt = new DateTime(2026, 8, 13, 20, 15, 0, DateTimeKind.Utc),
                ApprovedAt = new DateTime(2026, 8, 13, 21, 15, 0, DateTimeKind.Utc)
            },
            new Review
            {
                Id = 12,
                UserId = customerUser2Id,
                ProductId = 4,
                Rating = 5,
                Comment = "Silky ripstop lining and very comfortable 3D footbox. Packs into a tiny compression dry bag.",
                Status = AdminResponse.Accepted.ToString(),
                CreatedAt = new DateTime(2026, 8, 18, 10, 50, 0, DateTimeKind.Utc),
                ApprovedAt = new DateTime(2026, 8, 18, 11, 50, 0, DateTimeKind.Utc)
            },
            new Review
            {
                Id = 13,
                UserId = customerUser4Id,
                ProductId = 5,
                Rating = 5,
                Comment = "Only 48 grams! Boiled 500ml of water in under 2 minutes at 2,500m elevation. Micro flame control is buttery smooth.",
                Status = AdminResponse.Accepted.ToString(),
                CreatedAt = new DateTime(2026, 8, 10, 17, 25, 0, DateTimeKind.Utc),
                ApprovedAt = new DateTime(2026, 8, 10, 18, 25, 0, DateTimeKind.Utc)
            },
            new Review
            {
                Id = 14,
                UserId = customerUser1Id,
                ProductId = 5,
                Rating = 5,
                Comment = "The titanium pot support arms are rock solid and fold down into a miniature hard case. Essential backpacking gear.",
                Status = AdminResponse.Accepted.ToString(),
                CreatedAt = new DateTime(2026, 8, 15, 11, 35, 0, DateTimeKind.Utc),
                ApprovedAt = new DateTime(2026, 8, 15, 12, 35, 0, DateTimeKind.Utc)
            },
            new Review
            {
                Id = 15,
                UserId = customerUser3Id,
                ProductId = 5,
                Rating = 4,
                Comment = "Remarkable fuel efficiency with standard isobutane canisters. Excellent wind resistance.",
                Status = AdminResponse.Accepted.ToString(),
                CreatedAt = new DateTime(2026, 8, 19, 14, 0, 0, DateTimeKind.Utc),
                ApprovedAt = new DateTime(2026, 8, 19, 15, 0, 0, DateTimeKind.Utc)
            },
            new Review
            {
                Id = 16,
                UserId = customerUser4Id,
                ProductId = 6,
                Rating = 5,
                Comment = "Exceptional grip on rocky trails in Petra and Wadi Rum. Completely waterproof and comfortable all day.",
                Status = AdminResponse.Accepted.ToString(),
                CreatedAt = new DateTime(2026, 8, 7, 13, 0, 0, DateTimeKind.Utc),
                ApprovedAt = new DateTime(2026, 8, 7, 14, 0, 0, DateTimeKind.Utc)
            },
            new Review
            {
                Id = 17,
                UserId = customerUser2Id,
                ProductId = 6,
                Rating = 4,
                Comment = "Minimal break-in period required! Hiked 18km on day one through wet scree and mud with zero blisters.",
                Status = AdminResponse.Accepted.ToString(),
                CreatedAt = new DateTime(2026, 8, 12, 9, 45, 0, DateTimeKind.Utc),
                ApprovedAt = new DateTime(2026, 8, 12, 10, 45, 0, DateTimeKind.Utc)
            },
            new Review
            {
                Id = 18,
                UserId = customerUser1Id,
                ProductId = 6,
                Rating = 5,
                Comment = "Heavy-duty oiled nubuck leather and solid ankle protection. Best hiking boots I've owned.",
                Status = AdminResponse.Accepted.ToString(),
                CreatedAt = new DateTime(2026, 8, 17, 15, 30, 0, DateTimeKind.Utc),
                ApprovedAt = new DateTime(2026, 8, 17, 16, 30, 0, DateTimeKind.Utc)
            },
            new Review
            {
                Id = 19,
                UserId = customerUser1Id,
                ProductId = 7,
                Rating = 4,
                Comment = "Terrific camp lantern. The warm campfire mode is great inside the tent and it charged my phone on the trail.",
                Status = AdminResponse.Accepted.ToString(),
                CreatedAt = new DateTime(2026, 8, 11, 21, 0, 0, DateTimeKind.Utc),
                ApprovedAt = new DateTime(2026, 8, 11, 22, 0, 0, DateTimeKind.Utc)
            },
            new Review
            {
                Id = 20,
                UserId = customerUser3Id,
                ProductId = 7,
                Rating = 5,
                Comment = "Blindingly bright on 1200 lumen turbo mode, yet battery lasted all weekend on medium mode.",
                Status = AdminResponse.Accepted.ToString(),
                CreatedAt = new DateTime(2026, 8, 14, 20, 30, 0, DateTimeKind.Utc),
                ApprovedAt = new DateTime(2026, 8, 14, 21, 30, 0, DateTimeKind.Utc)
            },
            new Review
            {
                Id = 21,
                UserId = customerUser4Id,
                ProductId = 7,
                Rating = 4,
                Comment = "The magnetic base and hanging hook make this super versatile around the campsite.",
                Status = AdminResponse.Accepted.ToString(),
                CreatedAt = new DateTime(2026, 8, 18, 19, 15, 0, DateTimeKind.Utc),
                ApprovedAt = new DateTime(2026, 8, 18, 20, 15, 0, DateTimeKind.Utc)
            },
            new Review
            {
                Id = 22,
                UserId = customerUser2Id,
                ProductId = 8,
                Rating = 5,
                Comment = "Solid stainless steel build. The spring-loaded needle-nose pliers and wood saw performed flawlessly.",
                Status = AdminResponse.Accepted.ToString(),
                CreatedAt = new DateTime(2026, 8, 9, 14, 20, 0, DateTimeKind.Utc),
                ApprovedAt = new DateTime(2026, 8, 9, 15, 20, 0, DateTimeKind.Utc)
            },
            new Review
            {
                Id = 23,
                UserId = customerUser1Id,
                ProductId = 8,
                Rating = 4,
                Comment = "One-handed blade deployment and locking mechanisms feel premium and secure. Great EDC tool.",
                Status = AdminResponse.Accepted.ToString(),
                CreatedAt = new DateTime(2026, 8, 13, 10, 10, 0, DateTimeKind.Utc),
                ApprovedAt = new DateTime(2026, 8, 13, 11, 10, 0, DateTimeKind.Utc)
            },
            new Review
            {
                Id = 24,
                UserId = customerUser3Id,
                ProductId = 8,
                Rating = 4,
                Comment = "Heavy duty and versatile. Used the wire cutters and screwdrivers to fix my camp stove in the field.",
                Status = AdminResponse.Accepted.ToString(),
                CreatedAt = new DateTime(2026, 8, 18, 16, 40, 0, DateTimeKind.Utc),
                ApprovedAt = new DateTime(2026, 8, 18, 17, 40, 0, DateTimeKind.Utc)
            },
            new Review
            {
                Id = 25,
                UserId = customerUser3Id,
                ProductId = 9,
                Rating = 5,
                Comment = "Left this in a hot car all day in the desert and the ice was still intact 18 hours later! Incredible insulation.",
                Status = AdminResponse.Accepted.ToString(),
                CreatedAt = new DateTime(2026, 8, 10, 16, 0, 0, DateTimeKind.Utc),
                ApprovedAt = new DateTime(2026, 8, 10, 17, 0, 0, DateTimeKind.Utc)
            },
            new Review
            {
                Id = 26,
                UserId = customerUser4Id,
                ProductId = 9,
                Rating = 4,
                Comment = "Wide mouth makes it easy to add large ice cubes and clean. Tough powder coat doesn't scratch.",
                Status = AdminResponse.Accepted.ToString(),
                CreatedAt = new DateTime(2026, 8, 15, 12, 20, 0, DateTimeKind.Utc),
                ApprovedAt = new DateTime(2026, 8, 15, 13, 20, 0, DateTimeKind.Utc)
            },
            new Review
            {
                Id = 27,
                UserId = customerUser2Id,
                ProductId = 9,
                Rating = 5,
                Comment = "Sturdy stainless steel handle makes carrying 1.5L very easy on long hikes.",
                Status = AdminResponse.Accepted.ToString(),
                CreatedAt = new DateTime(2026, 8, 20, 17, 50, 0, DateTimeKind.Utc),
                ApprovedAt = new DateTime(2026, 8, 20, 18, 50, 0, DateTimeKind.Utc)
            },
            new Review
            {
                Id = 28,
                UserId = customerUser4Id,
                ProductId = 10,
                Rating = 5,
                Comment = "Crisp and clear BAK-4 optics for desert stargazing and mountain wildlife observation. Emerald lenses give brilliant contrast.",
                Status = AdminResponse.Accepted.ToString(),
                CreatedAt = new DateTime(2026, 8, 8, 19, 30, 0, DateTimeKind.Utc),
                ApprovedAt = new DateTime(2026, 8, 8, 20, 30, 0, DateTimeKind.Utc)
            },
            new Review
            {
                Id = 29,
                UserId = customerUser1Id,
                ProductId = 10,
                Rating = 5,
                Comment = "Rubber armor provides great non-slip grip in wet weather. Smooth central focus wheel.",
                Status = AdminResponse.Accepted.ToString(),
                CreatedAt = new DateTime(2026, 8, 14, 11, 15, 0, DateTimeKind.Utc),
                ApprovedAt = new DateTime(2026, 8, 14, 12, 15, 0, DateTimeKind.Utc)
            },
            new Review
            {
                Id = 30,
                UserId = customerUser2Id,
                ProductId = 10,
                Rating = 4,
                Comment = "Comes with a nice padded strap and protective case. High quality glass for the price.",
                Status = AdminResponse.Accepted.ToString(),
                CreatedAt = new DateTime(2026, 8, 19, 15, 45, 0, DateTimeKind.Utc),
                ApprovedAt = new DateTime(2026, 8, 19, 16, 45, 0, DateTimeKind.Utc)
            },
            new Review
            {
                Id = 31,
                UserId = customerUser1Id,
                ProductId = 11,
                Rating = 4,
                Comment = "3 inches of thickness means zero feeling of rocks or cold ground underneath. Slept like a baby!",
                Status = AdminResponse.Accepted.ToString(),
                CreatedAt = new DateTime(2026, 8, 11, 8, 0, 0, DateTimeKind.Utc),
                ApprovedAt = new DateTime(2026, 8, 11, 9, 0, 0, DateTimeKind.Utc)
            },
            new Review
            {
                Id = 32,
                UserId = customerUser3Id,
                ProductId = 11,
                Rating = 5,
                Comment = "Brass valve allows rapid self-inflation. Rolls up surprisingly compact into the included stuff sack.",
                Status = AdminResponse.Accepted.ToString(),
                CreatedAt = new DateTime(2026, 8, 16, 14, 10, 0, DateTimeKind.Utc),
                ApprovedAt = new DateTime(2026, 8, 16, 15, 10, 0, DateTimeKind.Utc)
            },
            new Review
            {
                Id = 33,
                UserId = customerUser4Id,
                ProductId = 11,
                Rating = 4,
                Comment = "Contoured horizontal baffles keep you centered all night. Rugged ripstop fabric.",
                Status = AdminResponse.Accepted.ToString(),
                CreatedAt = new DateTime(2026, 8, 21, 10, 30, 0, DateTimeKind.Utc),
                ApprovedAt = new DateTime(2026, 8, 21, 11, 30, 0, DateTimeKind.Utc)
            },
            new Review
            {
                Id = 34,
                UserId = customerUser2Id,
                ProductId = 12,
                Rating = 5,
                Comment = "Natural cork handles absorb sweat and feel ergonomic all day. The quick-lock clamps never slip.",
                Status = AdminResponse.Accepted.ToString(),
                CreatedAt = new DateTime(2026, 8, 9, 13, 40, 0, DateTimeKind.Utc),
                ApprovedAt = new DateTime(2026, 8, 9, 14, 40, 0, DateTimeKind.Utc)
            },
            new Review
            {
                Id = 35,
                UserId = customerUser4Id,
                ProductId = 12,
                Rating = 4,
                Comment = "Extremely lightweight carbon fiber. Saved my knees on a 1,200m rocky descent.",
                Status = AdminResponse.Accepted.ToString(),
                CreatedAt = new DateTime(2026, 8, 15, 9, 25, 0, DateTimeKind.Utc),
                ApprovedAt = new DateTime(2026, 8, 15, 10, 25, 0, DateTimeKind.Utc)
            },
            new Review
            {
                Id = 36,
                UserId = customerUser1Id,
                ProductId = 12,
                Rating = 5,
                Comment = "Tungsten carbide tips bite securely into hard rock and dirt. High grade trekking poles.",
                Status = AdminResponse.Accepted.ToString(),
                CreatedAt = new DateTime(2026, 8, 20, 16, 15, 0, DateTimeKind.Utc),
                ApprovedAt = new DateTime(2026, 8, 20, 17, 15, 0, DateTimeKind.Utc)
            },
            new Review
            {
                Id = 37,
                UserId = customerUser3Id,
                ProductId = 13,
                Rating = 4,
                Comment = "Unpacked and set up literally in 10 seconds! Blackout coating keeps the interior completely dark and cool for sleeping past sunrise.",
                Status = AdminResponse.Accepted.ToString(),
                CreatedAt = new DateTime(2026, 8, 8, 11, 20, 0, DateTimeKind.Utc),
                ApprovedAt = new DateTime(2026, 8, 8, 12, 20, 0, DateTimeKind.Utc)
            },
            new Review
            {
                Id = 38,
                UserId = customerUser1Id,
                ProductId = 13,
                Rating = 5,
                Comment = "Perfect for weekend camping trips. Waterproof floor kept moisture out during morning dew.",
                Status = AdminResponse.Accepted.ToString(),
                CreatedAt = new DateTime(2026, 8, 13, 18, 30, 0, DateTimeKind.Utc),
                ApprovedAt = new DateTime(2026, 8, 13, 19, 30, 0, DateTimeKind.Utc)
            },
            new Review
            {
                Id = 39,
                UserId = customerUser2Id,
                ProductId = 13,
                Rating = 4,
                Comment = "Plenty of headroom and very easy to fold back down with the circular guide.",
                Status = AdminResponse.Accepted.ToString(),
                CreatedAt = new DateTime(2026, 8, 18, 12, 0, 0, DateTimeKind.Utc),
                ApprovedAt = new DateTime(2026, 8, 18, 13, 0, 0, DateTimeKind.Utc)
            },
            new Review
            {
                Id = 40,
                UserId = customerUser4Id,
                ProductId = 14,
                Rating = 5,
                Comment = "The red turn spigot has perfect water flow control. Hung it from a branch for an instant camp washing station.",
                Status = AdminResponse.Accepted.ToString(),
                CreatedAt = new DateTime(2026, 8, 10, 15, 50, 0, DateTimeKind.Utc),
                ApprovedAt = new DateTime(2026, 8, 10, 16, 50, 0, DateTimeKind.Utc)
            },
            new Review
            {
                Id = 41,
                UserId = customerUser2Id,
                ProductId = 14,
                Rating = 4,
                Comment = "Folds completely flat and weighs almost nothing when empty. No plastic taste at all.",
                Status = AdminResponse.Accepted.ToString(),
                CreatedAt = new DateTime(2026, 8, 16, 11, 10, 0, DateTimeKind.Utc),
                ApprovedAt = new DateTime(2026, 8, 16, 12, 10, 0, DateTimeKind.Utc)
            },
            new Review
            {
                Id = 42,
                UserId = customerUser1Id,
                ProductId = 14,
                Rating = 5,
                Comment = "Sturdy top handle supports the full 8L weight easily. Great for group cooking at camp.",
                Status = AdminResponse.Accepted.ToString(),
                CreatedAt = new DateTime(2026, 8, 21, 17, 40, 0, DateTimeKind.Utc),
                ApprovedAt = new DateTime(2026, 8, 21, 18, 40, 0, DateTimeKind.Utc)
            },
            new Review
            {
                Id = 43,
                UserId = customerUser1Id,
                ProductId = 15,
                Rating = 5,
                Comment = "The beechwood armrests and thick padded cushion make this the most comfortable camp chair ever made.",
                Status = AdminResponse.Accepted.ToString(),
                CreatedAt = new DateTime(2026, 8, 7, 16, 30, 0, DateTimeKind.Utc),
                ApprovedAt = new DateTime(2026, 8, 7, 17, 30, 0, DateTimeKind.Utc)
            },
            new Review
            {
                Id = 44,
                UserId = customerUser3Id,
                ProductId = 15,
                Rating = 5,
                Comment = "Solid folding frame. The removable cushion allows cooler mesh airflow during hot days.",
                Status = AdminResponse.Accepted.ToString(),
                CreatedAt = new DateTime(2026, 8, 12, 19, 20, 0, DateTimeKind.Utc),
                ApprovedAt = new DateTime(2026, 8, 12, 20, 20, 0, DateTimeKind.Utc)
            },
            new Review
            {
                Id = 45,
                UserId = customerUser4Id,
                ProductId = 15,
                Rating = 4,
                Comment = "Feels like high-end patio furniture that folds down into a travel bag. Highly recommended.",
                Status = AdminResponse.Accepted.ToString(),
                CreatedAt = new DateTime(2026, 8, 19, 13, 15, 0, DateTimeKind.Utc),
                ApprovedAt = new DateTime(2026, 8, 19, 14, 15, 0, DateTimeKind.Utc)
            },
            new Review
            {
                Id = 46,
                UserId = customerUser2Id,
                ProductId = 16,
                Rating = 4,
                Comment = "The separate shoe compartment and waterproof wet pouch are genius! Perfect for dirty hiking trail shoes.",
                Status = AdminResponse.Accepted.ToString(),
                CreatedAt = new DateTime(2026, 8, 9, 10, 45, 0, DateTimeKind.Utc),
                ApprovedAt = new DateTime(2026, 8, 9, 11, 45, 0, DateTimeKind.Utc)
            },
            new Review
            {
                Id = 47,
                UserId = customerUser1Id,
                ProductId = 16,
                Rating = 5,
                Comment = "Weighs under a pound yet carries 40L effortlessly. Breathable shoulder straps stay cool.",
                Status = AdminResponse.Accepted.ToString(),
                CreatedAt = new DateTime(2026, 8, 14, 15, 30, 0, DateTimeKind.Utc),
                ApprovedAt = new DateTime(2026, 8, 14, 16, 30, 0, DateTimeKind.Utc)
            },
            new Review
            {
                Id = 48,
                UserId = customerUser3Id,
                ProductId = 16,
                Rating = 4,
                Comment = "Folds into its own tiny inner pocket when traveling. Great secondary daypack.",
                Status = AdminResponse.Accepted.ToString(),
                CreatedAt = new DateTime(2026, 8, 20, 11, 0, 0, DateTimeKind.Utc),
                ApprovedAt = new DateTime(2026, 8, 20, 12, 0, 0, DateTimeKind.Utc)
            },
            new Review
            {
                Id = 49,
                UserId = customerUser4Id,
                ProductId = 17,
                Rating = 5,
                Comment = "Complete 24-piece dining set with plates, cutlery, tongs, and straws. The zippered organizer bag keeps everything neat and quiet.",
                Status = AdminResponse.Accepted.ToString(),
                CreatedAt = new DateTime(2026, 8, 11, 12, 15, 0, DateTimeKind.Utc),
                ApprovedAt = new DateTime(2026, 8, 11, 13, 15, 0, DateTimeKind.Utc)
            },
            new Review
            {
                Id = 50,
                UserId = customerUser2Id,
                ProductId = 17,
                Rating = 4,
                Comment = "Food-grade stainless steel washes clean instantly at camp. Plates are heavy duty and don't bend.",
                Status = AdminResponse.Accepted.ToString(),
                CreatedAt = new DateTime(2026, 8, 17, 18, 25, 0, DateTimeKind.Utc),
                ApprovedAt = new DateTime(2026, 8, 17, 19, 25, 0, DateTimeKind.Utc)
            },
            new Review
            {
                Id = 51,
                UserId = customerUser1Id,
                ProductId = 17,
                Rating = 5,
                Comment = "Must-have tableware kit for family camping weekends.",
                Status = AdminResponse.Accepted.ToString(),
                CreatedAt = new DateTime(2026, 8, 22, 14, 0, 0, DateTimeKind.Utc),
                ApprovedAt = new DateTime(2026, 8, 22, 15, 0, 0, DateTimeKind.Utc)
            },
            new Review
            {
                Id = 52,
                UserId = customerUser1Id,
                ProductId = 18,
                Rating = 5,
                Comment = "Total 360-degree sun and wind protection in the desert. The detachable neck drape and face cover saved my skin.",
                Status = AdminResponse.Accepted.ToString(),
                CreatedAt = new DateTime(2026, 8, 8, 14, 0, 0, DateTimeKind.Utc),
                ApprovedAt = new DateTime(2026, 8, 8, 15, 0, 0, DateTimeKind.Utc)
            },
            new Review
            {
                Id = 53,
                UserId = customerUser4Id,
                ProductId = 18,
                Rating = 5,
                Comment = "Breathable crown mesh keeps your head cool. Adjustable chin toggle keeps it secure in gusty winds.",
                Status = AdminResponse.Accepted.ToString(),
                CreatedAt = new DateTime(2026, 8, 13, 17, 10, 0, DateTimeKind.Utc),
                ApprovedAt = new DateTime(2026, 8, 13, 18, 10, 0, DateTimeKind.Utc)
            },
            new Review
            {
                Id = 54,
                UserId = customerUser3Id,
                ProductId = 18,
                Rating = 4,
                Comment = "Lightweight, quick-drying fabric. Foldable into any backpack pocket.",
                Status = AdminResponse.Accepted.ToString(),
                CreatedAt = new DateTime(2026, 8, 18, 9, 30, 0, DateTimeKind.Utc),
                ApprovedAt = new DateTime(2026, 8, 18, 10, 30, 0, DateTimeKind.Utc)
            },
            new Review
            {
                Id = 55,
                UserId = customerUser2Id,
                ProductId = 19,
                Rating = 4,
                Comment = "Beautiful solid polished brass construction. The luminous dial glows brightly in complete darkness for night navigation.",
                Status = AdminResponse.Accepted.ToString(),
                CreatedAt = new DateTime(2026, 8, 10, 20, 0, 0, DateTimeKind.Utc),
                ApprovedAt = new DateTime(2026, 8, 10, 21, 0, 0, DateTimeKind.Utc)
            },
            new Review
            {
                Id = 56,
                UserId = customerUser3Id,
                ProductId = 19,
                Rating = 5,
                Comment = "High damping fluid ensures needle settles quickly and accurately. Heavy, premium heirloom feel.",
                Status = AdminResponse.Accepted.ToString(),
                CreatedAt = new DateTime(2026, 8, 15, 16, 45, 0, DateTimeKind.Utc),
                ApprovedAt = new DateTime(2026, 8, 15, 17, 45, 0, DateTimeKind.Utc)
            },
            new Review
            {
                Id = 57,
                UserId = customerUser1Id,
                ProductId = 19,
                Rating = 4,
                Comment = "The protective flip lid and lanyard loop make this a great survival navigation tool.",
                Status = AdminResponse.Accepted.ToString(),
                CreatedAt = new DateTime(2026, 8, 21, 11, 20, 0, DateTimeKind.Utc),
                ApprovedAt = new DateTime(2026, 8, 21, 12, 20, 0, DateTimeKind.Utc)
            },
            new Review
            {
                Id = 58,
                UserId = customerUser4Id,
                ProductId = 20,
                Rating = 5,
                Comment = "Finally my camp kitchen is organized! Holds my stove, 4 gas canisters, pans, utensils, and seasonings with customizable velcro dividers.",
                Status = AdminResponse.Accepted.ToString(),
                CreatedAt = new DateTime(2026, 8, 12, 13, 50, 0, DateTimeKind.Utc),
                ApprovedAt = new DateTime(2026, 8, 12, 14, 50, 0, DateTimeKind.Utc)
            },
            new Review
            {
                Id = 59,
                UserId = customerUser1Id,
                ProductId = 20,
                Rating = 4,
                Comment = "Rigid sidewalls protect delicate gear and the MOLLE exterior webbing lets you clip extra pouches.",
                Status = AdminResponse.Accepted.ToString(),
                CreatedAt = new DateTime(2026, 8, 16, 19, 10, 0, DateTimeKind.Utc),
                ApprovedAt = new DateTime(2026, 8, 16, 20, 10, 0, DateTimeKind.Utc)
            },
            new Review
            {
                Id = 60,
                UserId = customerUser2Id,
                ProductId = 20,
                Rating = 5,
                Comment = "Heavy-duty 900D Oxford fabric with reinforced carry handles. Excellent gear tote.",
                Status = AdminResponse.Accepted.ToString(),
                CreatedAt = new DateTime(2026, 8, 22, 10, 40, 0, DateTimeKind.Utc),
                ApprovedAt = new DateTime(2026, 8, 22, 11, 40, 0, DateTimeKind.Utc)
            },
            new Review
            {
                Id = 61,
                UserId = customerUser3Id,
                ProductId = 21,
                Rating = 4,
                Comment = "Holds my stainless water bottle snugly without bouncing while trail running or hiking. Lots of organizer pockets.",
                Status = AdminResponse.Accepted.ToString(),
                CreatedAt = new DateTime(2026, 8, 7, 11, 0, 0, DateTimeKind.Utc),
                ApprovedAt = new DateTime(2026, 8, 7, 12, 0, 0, DateTimeKind.Utc)
            },
            new Review
            {
                Id = 62,
                UserId = customerUser2Id,
                ProductId = 21,
                Rating = 5,
                Comment = "Breathable lumbar mesh and wide waist belt distribute the weight comfortably.",
                Status = AdminResponse.Accepted.ToString(),
                CreatedAt = new DateTime(2026, 8, 14, 17, 35, 0, DateTimeKind.Utc),
                ApprovedAt = new DateTime(2026, 8, 14, 18, 35, 0, DateTimeKind.Utc)
            },
            new Review
            {
                Id = 63,
                UserId = customerUser4Id,
                ProductId = 21,
                Rating = 4,
                Comment = "Rugged tactical look with durable zippers and side MOLLE attachments.",
                Status = AdminResponse.Accepted.ToString(),
                CreatedAt = new DateTime(2026, 8, 19, 14, 50, 0, DateTimeKind.Utc),
                ApprovedAt = new DateTime(2026, 8, 19, 15, 50, 0, DateTimeKind.Utc)
            },
            new Review
            {
                Id = 64,
                UserId = customerUser1Id,
                ProductId = 22,
                Rating = 5,
                Comment = "Instant cooling sensation when you put them on! Thumb holes keep them in place and UV protection is 100% effective.",
                Status = AdminResponse.Accepted.ToString(),
                CreatedAt = new DateTime(2026, 8, 9, 9, 15, 0, DateTimeKind.Utc),
                ApprovedAt = new DateTime(2026, 8, 9, 10, 15, 0, DateTimeKind.Utc)
            },
            new Review
            {
                Id = 65,
                UserId = customerUser4Id,
                ProductId = 22,
                Rating = 4,
                Comment = "Compression fit prevents muscle fatigue on long mountain treks. Dries sweat instantly.",
                Status = AdminResponse.Accepted.ToString(),
                CreatedAt = new DateTime(2026, 8, 15, 15, 0, 0, DateTimeKind.Utc),
                ApprovedAt = new DateTime(2026, 8, 15, 16, 0, 0, DateTimeKind.Utc)
            },
            new Review
            {
                Id = 66,
                UserId = customerUser2Id,
                ProductId = 22,
                Rating = 5,
                Comment = "Great elasticity, doesn't slip down the bicep. Reflective logo is a nice safety touch for evening runs.",
                Status = AdminResponse.Accepted.ToString(),
                CreatedAt = new DateTime(2026, 8, 21, 18, 10, 0, DateTimeKind.Utc),
                ApprovedAt = new DateTime(2026, 8, 21, 19, 10, 0, DateTimeKind.Utc)
            },
            new Review
            {
                Id = 67,
                UserId = customerUser4Id,
                ProductId = 23,
                Rating = 5,
                Comment = "Folds down to just 36cm and fits inside my carry-on backpack! Internal steel cable lock is rock solid.",
                Status = AdminResponse.Accepted.ToString(),
                CreatedAt = new DateTime(2026, 8, 11, 10, 30, 0, DateTimeKind.Utc),
                ApprovedAt = new DateTime(2026, 8, 11, 11, 30, 0, DateTimeKind.Utc)
            },
            new Review
            {
                Id = 68,
                UserId = customerUser1Id,
                ProductId = 23,
                Rating = 5,
                Comment = "Natural cork handle feels amazing in the hand with zero blisters. Quick cam lever makes height adjustment effortless.",
                Status = AdminResponse.Accepted.ToString(),
                CreatedAt = new DateTime(2026, 8, 17, 16, 20, 0, DateTimeKind.Utc),
                ApprovedAt = new DateTime(2026, 8, 17, 17, 20, 0, DateTimeKind.Utc)
            },
            new Review
            {
                Id = 69,
                UserId = customerUser3Id,
                ProductId = 23,
                Rating = 4,
                Comment = "Tested on rocky ascents with full pack weight. Carbon-alloy construction is ultralight yet rugged.",
                Status = AdminResponse.Accepted.ToString(),
                CreatedAt = new DateTime(2026, 8, 22, 13, 45, 0, DateTimeKind.Utc),
                ApprovedAt = new DateTime(2026, 8, 22, 14, 45, 0, DateTimeKind.Utc)
            },
            new Review
            {
                Id = 70,
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
                Id = 71,
                UserId = customerUser4Id,
                ProductId = 10,
                Rating = 5,
                Comment = "Crisp and clear BAK-4 optics for desert stargazing and mountain wildlife observation.",
                Status = AdminResponse.Pending.ToString(),
                CreatedAt = new DateTime(2026, 8, 24, 10, 0, 0, DateTimeKind.Utc),
                ApprovedAt = null
            },
            new Review
            {
                Id = 72,
                UserId = customerUser1Id,
                ProductId = 15,
                Rating = 4,
                Comment = "Very comfortable chair, testing it again this weekend at the lake.",
                Status = AdminResponse.Pending.ToString(),
                CreatedAt = new DateTime(2026, 8, 24, 14, 0, 0, DateTimeKind.Utc),
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
