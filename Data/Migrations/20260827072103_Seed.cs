using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CampTravelGear.Migrations
{
    /// <inheritdoc />
    public partial class Seed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FullAddress = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Addresses_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Carts_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Testimonials",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ApprovedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Testimonials", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Testimonials_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Wishlists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wishlists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Wishlists_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    AddressId = table.Column<int>(type: "int", nullable: true),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Orders_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CartItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CartId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartItems_Carts_CartId",
                        column: x => x.CartId,
                        principalTable: "Carts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    IsMain = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductImages_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ApprovedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reviews_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WishlistItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WishlistId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    AddedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WishlistItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WishlistItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WishlistItems_Wishlists_WishlistId",
                        column: x => x.WishlistId,
                        principalTable: "Wishlists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    TransactionId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaymentMethod = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PaidAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payments_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "role-admin-001", "role-admin-stamp", "Admin", "ADMIN" },
                    { "role-customer-002", "role-customer-stamp", "Customer", "CUSTOMER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "user-admin-001", 0, "con-stamp-admin-001", "admin@barrameru.com", true, "Rahhala Admin", false, null, "ADMIN@BARRAMERU.COM", "ADMIN@BARRAMERU.COM", "AQAAAAIAAYagAAAAEEdaqL9mVLSYIYDQnf3VbN26q7GMI+7eZ1KJyWghZois4i24uitPbuHClLflhAdeGA==", "+962 6 500 2026", false, "sec-stamp-admin-001", false, "admin@barrameru.com" },
                    { "user-cust-001", 0, "con-stamp-john-001", "john.doe@example.com", true, "John Doe", false, null, "JOHN.DOE@EXAMPLE.COM", "JOHN.DOE@EXAMPLE.COM", "AQAAAAIAAYagAAAAEPvH9DFxtV1m7CiKbFG7DczGEaLFQzYgi5Ckg0OVEzpmEsDORBDjImwoH6IuqeZohA==", "+1 (555) 342-8901", false, "sec-stamp-john-001", false, "john.doe@example.com" },
                    { "user-cust-002", 0, "con-stamp-sara-002", "sara.helvey@example.com", true, "Sara G. Helvey", false, null, "SARA.HELVEY@EXAMPLE.COM", "SARA.HELVEY@EXAMPLE.COM", "AQAAAAIAAYagAAAAEPvH9DFxtV1m7CiKbFG7DczGEaLFQzYgi5Ckg0OVEzpmEsDORBDjImwoH6IuqeZohA==", "+1 (555) 781-4329", false, "sec-stamp-sara-002", false, "sara.helvey@example.com" },
                    { "user-cust-003", 0, "con-stamp-alex-003", "alex.h@example.com", true, "Alex Hunter", false, null, "ALEX.H@EXAMPLE.COM", "ALEX.H@EXAMPLE.COM", "AQAAAAIAAYagAAAAEPvH9DFxtV1m7CiKbFG7DczGEaLFQzYgi5Ckg0OVEzpmEsDORBDjImwoH6IuqeZohA==", "+1 (555) 629-1145", false, "sec-stamp-alex-003", false, "alex.h@example.com" },
                    { "user-cust-004", 0, "con-stamp-omar-004", "omar.khalil@example.com", true, "Omar Al-Khalil", false, null, "OMAR.KHALIL@EXAMPLE.COM", "OMAR.KHALIL@EXAMPLE.COM", "AQAAAAIAAYagAAAAEPvH9DFxtV1m7CiKbFG7DczGEaLFQzYgi5Ckg0OVEzpmEsDORBDjImwoH6IuqeZohA==", "+962 7 9876 5432", false, "sec-stamp-omar-004", false, "omar.khalil@example.com" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedAt", "IsDeleted", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 1, 15, 8, 0, 0, 0, DateTimeKind.Utc), false, "Bags & Pack", null },
                    { 2, new DateTime(2026, 1, 15, 8, 0, 0, 0, DateTimeKind.Utc), false, "Tent & Accessories", null },
                    { 3, new DateTime(2026, 1, 15, 8, 0, 0, 0, DateTimeKind.Utc), false, "Camp Kitchen", null },
                    { 4, new DateTime(2026, 1, 15, 8, 0, 0, 0, DateTimeKind.Utc), false, "Knives & Tools", null },
                    { 5, new DateTime(2026, 1, 15, 8, 0, 0, 0, DateTimeKind.Utc), false, "Climbing & Hiking", null }
                });

            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "Id", "City", "FullAddress", "UserId" },
                values: new object[,]
                {
                    { 1, "Amman", "742 Evergreen Terrace", "user-cust-001" },
                    { 2, "Boulder", "1824 Mountain View Ridge", "user-cust-002" },
                    { 3, "Aqaba", "45 King Hussein Street", "user-cust-003" },
                    { 4, "Salt Lake City", "89 Alpine Summit Blvd", "user-cust-004" },
                    { 5, "Irbid", "12 University Road", "user-cust-001" },
                    { 6, "Denver", "33 Panorama Way", "user-cust-002" },
                    { 7, "Amman", "500 Sunset Strip", "user-cust-003" },
                    { 8, "Aqaba", "104 Red Sea Marina Way", "user-cust-004" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "role-admin-001", "user-admin-001" },
                    { "role-customer-002", "user-cust-001" },
                    { "role-customer-002", "user-cust-002" },
                    { "role-customer-002", "user-cust-003" },
                    { "role-customer-002", "user-cust-004" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "CreatedAt", "Description", "IsActive", "IsDeleted", "Name", "Price", "Stock", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, 2, new DateTime(2026, 1, 15, 8, 0, 0, 0, DateTimeKind.Utc), "Engineered for rugged wilderness expeditions and weekend family getaways alike, the Rahhala Horizon 4-Person Tent delivers uncompromising storm protection and exceptional ventilation. Constructed from heavy-duty 210T ripstop polyester with a 3,500mm polyurethane waterproof coating, this tent keeps your camp completely dry even during continuous torrential downpours.\n\nThe freestanding geodesic frame uses 9.5mm aircraft-grade anodized aluminum poles that resist high mountain winds up to 60 km/h. Inside, dual oversized mesh doors and dual vestibules provide generous gear storage and cross-ventilation to prevent condensation. Features factory seam-taped bathtub floors, reflective guy lines, and internal organizer pockets for headlamps and maps.", true, false, "Rahhala Horizon 4-Person Geodesic Camping Tent", 189.00m, 28, null },
                    { 2, 1, new DateTime(2026, 1, 15, 8, 0, 0, 0, DateTimeKind.Utc), "Built for multi-day alpine traverses and rugged backcountry journeys, the Alpine Apex 65L backpack combines maximum load stability with breathable trail ergonomics. The dynamic torso adjustment system fits heights from 5'4\" to 6'4\", transferring 80% of pack weight smoothly to the multi-density contoured hip belt.\n\nFabricated from 420D high-tenacity Cordura nylon with abrasion-resistant Hypalon reinforcement panels, this pack easily withstands jagged rock scrambles. Includes a dedicated sleeping bag bottom compartment with internal divider, integrated 3L hydration bladder sleeve, dual trekking pole attachments, and a stowable high-visibility waterproof rain cover.", true, false, "Alpine Apex 65L Internal Frame Mountain Expedition Backpack", 149.00m, 35, null },
                    { 3, 2, new DateTime(2026, 1, 15, 8, 0, 0, 0, DateTimeKind.Utc), "Weighing merely 580 grams, the Rahhala Trekker Camp Stool is the ultimate lightweight seating solution for backpackers, trail runners, and campfire gatherings. Crafted with 7075 aviation-grade aluminum tubing with shock-corded assembly, it sets up in under 5 seconds and supports weights up to 130 kg (285 lbs).\n\nThe seat is woven from 600D water-repellent oxford canvas with reinforced bar-tack stitching at all stress points. Packs down into an ultra-compact cylinder that slips effortlessly into side water bottle pockets, making it an indispensable companion on every journey.", true, false, "Rahhala Trekker Ultra-Compact Aluminum Folding Camp Stool", 38.00m, 45, null },
                    { 4, 2, new DateTime(2026, 1, 15, 8, 0, 0, 0, DateTimeKind.Utc), "Engineered for alpine bivouacs and cold mountain nights, the GlacierPeak sleeping bag delivers premium 750-fill-power hydrophobic goose down insulation rated for temperatures down to -10°C (14°F). The anatomically curved mummy profile minimizes dead air space for instant thermal retention.\n\nFeatures a 20D DWR-treated micro-ripstop shell that sheds condensation, a full-length insulated draft tube along the anti-snag YKK two-way zipper, and an ergonomic 3D footbox. Compresses down to the size of a water bottle in the included waterproof compression dry sack.", true, false, "GlacierPeak -10°C Ultralight Goose Down Mummy Sleeping Bag", 165.00m, 20, null },
                    { 5, 3, new DateTime(2026, 1, 15, 8, 0, 0, 0, DateTimeKind.Utc), "Crafted from precision-machined aerospace titanium, this ultralight 48g backpacking stove boils 1 liter of water in just 3.2 minutes while consuming minimal isobutane fuel. The concave burner head creates a localized micro-tornado flame that resists gusts up to 30 km/h without requiring a bulky windscreen.\n\nFoldable serrated pot supports accommodate cookware from small 500ml titanium mugs to 2-liter camp pots. Fits standard threaded Lindal valve fuel canisters and folds neatly into an included protective hard-shell case for effortless packing.", true, false, "TitanFlame Backcountry Micro Windproof Titanium Camp Stove", 42.00m, 60, null },
                    { 6, 5, new DateTime(2026, 1, 15, 8, 0, 0, 0, DateTimeKind.Utc), "Conquer demanding rocky terrains, mud trails, and mountain summits with confidence. The TerraGrip boots combine waterproof full-grain nubuck leather with breathable HydroGuard waterproof membrane booties that keep feet completely dry while releasing internal perspiration.\n\nEquipped with custom deep-lug Vibram Megagrip rubber outsoles for exceptional braking power on steep descents. An injected EVA midsole and thermoplastic shank absorb trail shock and protect feet from sharp jagged rocks throughout long trekking days.", true, false, "TerraGrip Waterproof Ankle-Support Hiking Trail Boots", 135.00m, 24, null },
                    { 7, 4, new DateTime(2026, 1, 15, 8, 0, 0, 0, DateTimeKind.Utc), "Light up your entire campsite with 360-degree diffused illumination. The Solaris Lantern outputs up to 1,200 lumens with 4 versatile lighting modes: Warm Campfire Glow, Cool Daylight, Red Night-Vision, and Emergency SOS Flashing.\n\nPowered by an internal 5,200mAh rechargeable lithium battery that provides up to 70 hours of continuous runtime on low mode and doubles as an emergency USB power bank to charge smartphones on the trail. Features an IPX6 waterproof rating, magnetic base, and top carabiner hook.", true, false, "Solaris 1200-Lumen Rechargeable LED Camp Lantern & Power Bank", 39.00m, 55, null },
                    { 8, 4, new DateTime(2026, 1, 15, 8, 0, 0, 0, DateTimeKind.Utc), "Precision crafted from heat-treated 440C stainless steel, this heavy-duty multi-tool integrates 14 essential backcountry tools: spring-loaded needle-nose pliers, wire cutters, wood saw, serrated blade, can opener, bottle opener, Phillips and flat screwdrivers, and a ruler.\n\nEvery individual implement securely locks in place with a safety lever to prevent accidental closure during intense cutting or sawing tasks. Includes a heavy-duty ballistic nylon belt sheath.", true, false, "SwissForge 14-in-1 Tactical Stainless Outdoor Multi-Tool", 49.00m, 40, null },
                    { 9, 3, new DateTime(2026, 1, 15, 8, 0, 0, 0, DateTimeKind.Utc), "Keep hydration icy cold for 36 hours or soup steaming hot for 18 hours. The HydroShield Canteen is manufactured from food-grade 18/8 kitchen stainless steel with TempLock double-wall vacuum insulation that never transfers flavor or creates outer condensation.\n\nFeatures a wide mouth for adding ice cubes, a leakproof twist-lock cap with an integrated stainless carrying handle, and a durable powder-coat exterior finish that resists scuffs and trail drops.", true, false, "HydroShield Double-Wall Vacuum Insulated Camping Canteen 1.5L", 36.00m, 50, null },
                    { 10, 4, new DateTime(2026, 1, 15, 8, 0, 0, 0, DateTimeKind.Utc), "Experience breathtaking clarity on wildlife spotting and trail reconnaissance. The ApexView features fully multi-coated FMC green broadband optics and premium BAK-4 roof prisms that deliver 99.5% light transmission even in low-light twilight conditions.\n\nO-ring sealed and nitrogen purged for 100% fogproof and waterproof performance in all climates. Rugged rubber armor absorbs shocks and provides a non-slip grip in wet weather. Includes neck strap, carrying case, and protective lens caps.", true, false, "ApexView 12x50 HD Waterproof Nitrogen-Purged Binoculars", 89.00m, 22, null },
                    { 11, 2, new DateTime(2026, 1, 15, 8, 0, 0, 0, DateTimeKind.Utc), "Say goodbye to waking up on hard uneven ground. The CloudRest features 3 inches of high-density open-cell memory foam with an insulated R-value of 4.8, providing true 4-season thermal barrier from frozen ground.\n\nDual wide-mouth brass valves inflate the pad in under 60 seconds with zero pumping required. The top surface is soft-touch peached polyester that prevents sleeping bag rustle, while the bottom uses tough 75D puncture-resistant polyester with anti-slip silicone dots.", true, false, "CloudRest Self-Inflating 3-Inch Ergonomic Camp Sleeping Pad", 52.00m, 32, null },
                    { 12, 5, new DateTime(2026, 1, 15, 8, 0, 0, 0, DateTimeKind.Utc), "Engineered with 100% 3K carbon fiber shafts, these trekking poles absorb trail vibrations and reduce knee strain by up to 25% on steep mountain descents. Weighs just 210 grams per pole.\n\nAluminum SpeedLock levers allow quick length adjustments from 65cm to 135cm even while wearing thick winter gloves. Features ergonomic natural cork grips that wick sweat, breathable padded wrist straps, carbide tungsten tips, and interchangeable mud and snow baskets.", true, false, "TrailFlow Quick-Lock Carbon Fiber Trekking Poles (Pair)", 64.00m, 30, null },
                    { 13, 2, new DateTime(2026, 1, 15, 8, 0, 0, 0, DateTimeKind.Utc), "Instant hydraulic pop-up camping tent designed for effortless 10-second setup. Engineered with 190T silver-coated blackout Oxford fabric providing UPF 50+ UV shielding and 2000mm water resistance. Dimensions: 190 x 120 x 100 cm (74.8 x 47.2 x 39.4 in), comfortably accommodating 1-2 persons with dual-layer mesh ventilation doors.", true, false, "Rahhala Quick-Pitch 2-Person Pop-Up Blackout Camping Tent", 79.99m, 35, null },
                    { 14, 3, new DateTime(2026, 1, 15, 8, 0, 0, 0, DateTimeKind.Utc), "Heavy-duty 8-Liter (281 oz) collapsible camping water reservoir made from 100% food-grade, BPA-free PE/PA material. Features a 360-degree leakproof rotatable on/off spigot for easy handwashing, cooking, and hydration dispensing. Folds completely flat when empty and includes a reinforced carry and tree-hanging handle.", true, false, "CityFox 8L Collapsible Outdoor Water Bag with Spigot Dispenser", 18.50m, 60, null },
                    { 15, 2, new DateTime(2026, 1, 15, 8, 0, 0, 0, DateTimeKind.Utc), "Premium ergonomic folding camp lounge chair featuring a removable high-density quilted cushion for all-season comfort. Built with natural solid beechwood armrests and a high-strength triangular carbon steel frame supporting up to 150 kg (330 lbs). Folds in 3 seconds into a slim, easy-carry profile.", true, false, "UrbanWave Deluxe Padded Folding Camp Chair with Solid Beechwood Armrests", 68.00m, 25, null },
                    { 16, 1, new DateTime(2026, 1, 15, 8, 0, 0, 0, DateTimeKind.Utc), "Ultralight 40-liter multi-compartment hiking backpack weighing only 1 lb (450g). Features an isolated bottom shoe compartment, waterproof wet gear pocket, breathable honeycomb mesh shoulder straps, dual water bottle mesh pockets, front bungee organizer, and trekking pole attachment loops.", true, false, "AeroTrail 40L Ultralight Packable Daypack with Separate Shoe & Wet Pocket", 45.00m, 40, null },
                    { 17, 3, new DateTime(2026, 1, 15, 8, 0, 0, 0, DateTimeKind.Utc), "Complete 24-piece outdoor dining kit crafted from mirror-polished food-grade 18/8 stainless steel. Includes 4 dinner knives, 4 dinner forks, 4 tablespoons, 4 dessert spoons, 4 pairs of chopsticks, 2 stainless straws with brush, 2 BBQ food tongs, 4 stainless plates, and 1 multi-opener, all organized in a heavy-duty zippered Oxford case.", true, false, "CampMaster 4-Person Stainless Steel Cutlery & Tableware Mess Kit (24-Piece)", 34.00m, 50, null },
                    { 18, 5, new DateTime(2026, 1, 15, 8, 0, 0, 0, DateTimeKind.Utc), "Comprehensive 360-degree UPF 50+ sun protection outdoor hat featuring a wide 3.8-inch brim, removable breathable neck drape, and mesh face shield. Built with water-repellent quick-dry nylon, crown air circulation vents, and an adjustable windproof chin toggle drawstring.", true, false, "SaharaShield UPF 50+ Sun Protection Safari Boonie Hat with Detachable Neck & Face Flaps", 22.00m, 70, null },
                    { 19, 4, new DateTime(2026, 1, 15, 8, 0, 0, 0, DateTimeKind.Utc), "Classic survival navigation compass crafted from heavy-gauge polished solid brass with an engraved protective flip lid. Equipped with a 360-degree rotating azimuth bezel, rapid-settling magnetic needle, fluorescent glow-in-the-dark dial markings for night orienteering, and a solid lanyard ring.", true, false, "PathFinder Heritage Vintage Solid Brass Pocket Compass with 360 Luminous Dial", 26.00m, 55, null },
                    { 20, 1, new DateTime(2026, 1, 15, 8, 0, 0, 0, DateTimeKind.Utc), "Rugged 35-liter modular camp storage tote engineered from 900D water-resistant ballistic Oxford cloth with PE reinforced bottom panels. Includes movable padded Velcro dividers to securely store gas canisters, camping stoves, cookware, and lanterns without rattling. Features Molle loops and a padded shoulder strap.", true, false, "TacticalCook Heavy-Duty Modular Camping Gear Organizer Trunk 35L", 52.00m, 30, null },
                    { 21, 1, new DateTime(2026, 1, 15, 8, 0, 0, 0, DateTimeKind.Utc), "Tactical multi-pocket outdoor waist pack featuring a dedicated drawstring water bottle holster fitting up to 1L bottles. Built with 1000D abrasion-resistant nylon, dual front buckle compartments, laser-cut Molle attachment points, and an adjustable heavy-duty quick-release waist belt (28-50 inches).", true, false, "Vanguard Tactical MOLLE Hydration Waist Pack & Trail Fanny Bag", 29.50m, 45, null },
                    { 22, 5, new DateTime(2026, 1, 15, 8, 0, 0, 0, DateTimeKind.Utc), "High-performance ice-silk cooling compression sleeves delivering UPF 50+ UV protection and immediate skin temperature reduction by 3-5 deg C. Features ergonomic thumb hole cuffs to prevent slippage, reflective safety accents for twilight visibility, and seamless 4-way stretch fabric for hiking and climbing.", true, false, "AeroCool UPF 50+ UV Compression Cooling Arm Sleeves with Thumb Holes", 14.00m, 80, null },
                    { 23, 5, new DateTime(2026, 1, 15, 8, 0, 0, 0, DateTimeKind.Utc), "Ultra-compact tri-fold collapsible hiking poles folding down to just 36 cm (14 in). Features sweat-absorbing ergonomic natural cork handles, extended EVA choke grips, durable aluminum SpeedLock levers (110-130 cm adjustment), tungsten carbide tips, rubber walking feet, mud/snow baskets, and a ripstop carry bag.", true, false, "Namkabaw Quick-Deploy Tri-Fold Carbon-Alloy Trekking Poles with Natural Cork Grip", 58.00m, 35, null }
                });

            migrationBuilder.InsertData(
                table: "Testimonials",
                columns: new[] { "Id", "ApprovedAt", "Content", "CreatedAt", "Name", "Status", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 7, 21, 10, 0, 0, 0, DateTimeKind.Utc), "Rahhala gear survived our 10-day alpine trek in freezing storms. Best outdoor brand quality and fast delivery!", new DateTime(2026, 7, 20, 9, 0, 0, 0, DateTimeKind.Utc), "Sara G. Helvey", "Accepted", "user-cust-002" },
                    { 2, new DateTime(2026, 7, 26, 10, 0, 0, 0, DateTimeKind.Utc), "The most reliable camping gear store I have ever used. Authentic rugged equipment and top customer support.", new DateTime(2026, 7, 25, 11, 0, 0, 0, DateTimeKind.Utc), "John Doe", "Accepted", "user-cust-001" },
                    { 3, new DateTime(2026, 7, 27, 16, 0, 0, 0, DateTimeKind.Utc), "The top outdoor camping outfitter in Jordan. Genuine technical gear and great customer care at their Amman center!", new DateTime(2026, 7, 27, 14, 0, 0, 0, DateTimeKind.Utc), "Omar Al-Khalil", "Accepted", "user-cust-004" },
                    { 4, null, "Their geodesic tents and titanium stoves are elite level. Highly recommended to any wilderness adventurer.", new DateTime(2026, 7, 28, 16, 0, 0, 0, DateTimeKind.Utc), "Alex Hunter", "Pending", "user-cust-003" },
                    { 5, null, "Visit this promotional third-party spam link for discounts.", new DateTime(2026, 7, 29, 8, 0, 0, 0, DateTimeKind.Utc), "Spam User", "Rejected", "user-cust-001" }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "AddressId", "CreatedAt", "OrderDate", "Status", "TotalAmount", "UserId" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2026, 3, 5, 14, 20, 0, 0, DateTimeKind.Utc), new DateTime(2026, 3, 5, 14, 20, 0, 0, DateTimeKind.Utc), "Completed", 227m, "user-cust-001" },
                    { 2, 2, new DateTime(2026, 3, 10, 11, 15, 0, 0, DateTimeKind.Utc), new DateTime(2026, 3, 10, 11, 15, 0, 0, DateTimeKind.Utc), "Completed", 227m, "user-cust-002" },
                    { 3, 3, new DateTime(2026, 3, 16, 16, 40, 0, 0, DateTimeKind.Utc), new DateTime(2026, 3, 16, 16, 40, 0, 0, DateTimeKind.Utc), "Completed", 217m, "user-cust-003" },
                    { 4, 4, new DateTime(2026, 3, 21, 9, 30, 0, 0, DateTimeKind.Utc), new DateTime(2026, 3, 21, 9, 30, 0, 0, DateTimeKind.Utc), "Completed", 221m, "user-cust-004" },
                    { 5, 5, new DateTime(2026, 3, 26, 13, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 3, 26, 13, 0, 0, 0, DateTimeKind.Utc), "Completed", 177m, "user-cust-001" },
                    { 6, 6, new DateTime(2026, 3, 29, 18, 10, 0, 0, DateTimeKind.Utc), new DateTime(2026, 3, 29, 18, 10, 0, 0, DateTimeKind.Utc), "Cancelled", 188m, "user-cust-002" },
                    { 7, 7, new DateTime(2026, 4, 3, 10, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 4, 3, 10, 0, 0, 0, DateTimeKind.Utc), "Completed", 262m, "user-cust-003" },
                    { 8, 8, new DateTime(2026, 4, 7, 15, 25, 0, 0, DateTimeKind.Utc), new DateTime(2026, 4, 7, 15, 25, 0, 0, DateTimeKind.Utc), "Completed", 284m, "user-cust-004" },
                    { 9, 1, new DateTime(2026, 4, 12, 12, 10, 0, 0, DateTimeKind.Utc), new DateTime(2026, 4, 12, 12, 10, 0, 0, DateTimeKind.Utc), "Completed", 241m, "user-cust-001" },
                    { 10, 2, new DateTime(2026, 4, 16, 17, 45, 0, 0, DateTimeKind.Utc), new DateTime(2026, 4, 16, 17, 45, 0, 0, DateTimeKind.Utc), "Completed", 181.99m, "user-cust-002" },
                    { 11, 3, new DateTime(2026, 4, 20, 11, 30, 0, 0, DateTimeKind.Utc), new DateTime(2026, 4, 20, 11, 30, 0, 0, DateTimeKind.Utc), "Completed", 164m, "user-cust-003" },
                    { 12, 4, new DateTime(2026, 4, 23, 14, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 4, 23, 14, 0, 0, 0, DateTimeKind.Utc), "Completed", 181.5m, "user-cust-004" },
                    { 13, 5, new DateTime(2026, 4, 27, 16, 20, 0, 0, DateTimeKind.Utc), new DateTime(2026, 4, 27, 16, 20, 0, 0, DateTimeKind.Utc), "Completed", 158m, "user-cust-001" },
                    { 14, 6, new DateTime(2026, 4, 29, 9, 50, 0, 0, DateTimeKind.Utc), new DateTime(2026, 4, 29, 9, 50, 0, 0, DateTimeKind.Utc), "Completed", 187m, "user-cust-002" },
                    { 15, 7, new DateTime(2026, 5, 2, 11, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 5, 2, 11, 0, 0, 0, DateTimeKind.Utc), "Completed", 430m, "user-cust-003" },
                    { 16, 8, new DateTime(2026, 5, 5, 14, 30, 0, 0, DateTimeKind.Utc), new DateTime(2026, 5, 5, 14, 30, 0, 0, DateTimeKind.Utc), "Completed", 302m, "user-cust-004" },
                    { 17, 1, new DateTime(2026, 5, 9, 10, 15, 0, 0, DateTimeKind.Utc), new DateTime(2026, 5, 9, 10, 15, 0, 0, DateTimeKind.Utc), "Completed", 315m, "user-cust-001" },
                    { 18, 2, new DateTime(2026, 5, 13, 16, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 5, 13, 16, 0, 0, 0, DateTimeKind.Utc), "Completed", 209m, "user-cust-002" },
                    { 19, 3, new DateTime(2026, 5, 16, 13, 40, 0, 0, DateTimeKind.Utc), new DateTime(2026, 5, 16, 13, 40, 0, 0, DateTimeKind.Utc), "Completed", 178.48m, "user-cust-003" },
                    { 20, 4, new DateTime(2026, 5, 20, 9, 20, 0, 0, DateTimeKind.Utc), new DateTime(2026, 5, 20, 9, 20, 0, 0, DateTimeKind.Utc), "Completed", 196m, "user-cust-004" },
                    { 21, 5, new DateTime(2026, 5, 23, 15, 10, 0, 0, DateTimeKind.Utc), new DateTime(2026, 5, 23, 15, 10, 0, 0, DateTimeKind.Utc), "Completed", 174m, "user-cust-001" },
                    { 22, 6, new DateTime(2026, 5, 26, 17, 35, 0, 0, DateTimeKind.Utc), new DateTime(2026, 5, 26, 17, 35, 0, 0, DateTimeKind.Utc), "Completed", 131.5m, "user-cust-002" },
                    { 23, 7, new DateTime(2026, 5, 28, 12, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 5, 28, 12, 0, 0, 0, DateTimeKind.Utc), "Completed", 269m, "user-cust-003" },
                    { 24, 8, new DateTime(2026, 5, 31, 16, 50, 0, 0, DateTimeKind.Utc), new DateTime(2026, 5, 31, 16, 50, 0, 0, DateTimeKind.Utc), "Cancelled", 338m, "user-cust-004" },
                    { 25, 1, new DateTime(2026, 6, 3, 10, 10, 0, 0, DateTimeKind.Utc), new DateTime(2026, 6, 3, 10, 10, 0, 0, DateTimeKind.Utc), "Completed", 493m, "user-cust-001" },
                    { 26, 2, new DateTime(2026, 6, 6, 14, 40, 0, 0, DateTimeKind.Utc), new DateTime(2026, 6, 6, 14, 40, 0, 0, DateTimeKind.Utc), "Completed", 497m, "user-cust-002" },
                    { 27, 3, new DateTime(2026, 6, 9, 11, 25, 0, 0, DateTimeKind.Utc), new DateTime(2026, 6, 9, 11, 25, 0, 0, DateTimeKind.Utc), "Completed", 303m, "user-cust-003" },
                    { 28, 4, new DateTime(2026, 6, 12, 16, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 6, 12, 16, 0, 0, 0, DateTimeKind.Utc), "Completed", 295.98m, "user-cust-004" },
                    { 29, 5, new DateTime(2026, 6, 15, 9, 30, 0, 0, DateTimeKind.Utc), new DateTime(2026, 6, 15, 9, 30, 0, 0, DateTimeKind.Utc), "Completed", 171.5m, "user-cust-001" },
                    { 30, 6, new DateTime(2026, 6, 18, 13, 50, 0, 0, DateTimeKind.Utc), new DateTime(2026, 6, 18, 13, 50, 0, 0, DateTimeKind.Utc), "Completed", 272m, "user-cust-002" },
                    { 31, 7, new DateTime(2026, 6, 21, 17, 15, 0, 0, DateTimeKind.Utc), new DateTime(2026, 6, 21, 17, 15, 0, 0, DateTimeKind.Utc), "Completed", 283m, "user-cust-003" },
                    { 32, 8, new DateTime(2026, 6, 24, 12, 20, 0, 0, DateTimeKind.Utc), new DateTime(2026, 6, 24, 12, 20, 0, 0, DateTimeKind.Utc), "Completed", 272m, "user-cust-004" },
                    { 33, 1, new DateTime(2026, 6, 26, 15, 45, 0, 0, DateTimeKind.Utc), new DateTime(2026, 6, 26, 15, 45, 0, 0, DateTimeKind.Utc), "Completed", 372.5m, "user-cust-001" },
                    { 34, 2, new DateTime(2026, 6, 28, 10, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 6, 28, 10, 0, 0, 0, DateTimeKind.Utc), "Completed", 285m, "user-cust-002" },
                    { 35, 3, new DateTime(2026, 6, 29, 18, 30, 0, 0, DateTimeKind.Utc), new DateTime(2026, 6, 29, 18, 30, 0, 0, DateTimeKind.Utc), "Completed", 272m, "user-cust-003" },
                    { 36, 4, new DateTime(2026, 6, 30, 14, 15, 0, 0, DateTimeKind.Utc), new DateTime(2026, 6, 30, 14, 15, 0, 0, DateTimeKind.Utc), "Completed", 189.99m, "user-cust-004" },
                    { 37, 1, new DateTime(2026, 7, 2, 11, 20, 0, 0, DateTimeKind.Utc), new DateTime(2026, 7, 2, 11, 20, 0, 0, DateTimeKind.Utc), "Completed", 676m, "user-cust-001" },
                    { 38, 2, new DateTime(2026, 7, 5, 15, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 7, 5, 15, 0, 0, 0, DateTimeKind.Utc), "Completed", 600m, "user-cust-002" },
                    { 39, 3, new DateTime(2026, 7, 8, 10, 45, 0, 0, DateTimeKind.Utc), new DateTime(2026, 7, 8, 10, 45, 0, 0, DateTimeKind.Utc), "Completed", 382m, "user-cust-003" },
                    { 40, 4, new DateTime(2026, 7, 11, 16, 30, 0, 0, DateTimeKind.Utc), new DateTime(2026, 7, 11, 16, 30, 0, 0, DateTimeKind.Utc), "Completed", 375.97m, "user-cust-004" },
                    { 41, 5, new DateTime(2026, 7, 14, 9, 15, 0, 0, DateTimeKind.Utc), new DateTime(2026, 7, 14, 9, 15, 0, 0, DateTimeKind.Utc), "Completed", 290m, "user-cust-001" },
                    { 42, 6, new DateTime(2026, 7, 17, 13, 40, 0, 0, DateTimeKind.Utc), new DateTime(2026, 7, 17, 13, 40, 0, 0, DateTimeKind.Utc), "Completed", 270m, "user-cust-002" },
                    { 43, 7, new DateTime(2026, 7, 19, 17, 50, 0, 0, DateTimeKind.Utc), new DateTime(2026, 7, 19, 17, 50, 0, 0, DateTimeKind.Utc), "Completed", 437m, "user-cust-003" },
                    { 44, 8, new DateTime(2026, 7, 22, 12, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 7, 22, 12, 0, 0, 0, DateTimeKind.Utc), "Completed", 371m, "user-cust-004" },
                    { 45, 1, new DateTime(2026, 7, 24, 15, 25, 0, 0, DateTimeKind.Utc), new DateTime(2026, 7, 24, 15, 25, 0, 0, DateTimeKind.Utc), "Completed", 358m, "user-cust-001" },
                    { 46, 2, new DateTime(2026, 7, 26, 10, 10, 0, 0, DateTimeKind.Utc), new DateTime(2026, 7, 26, 10, 10, 0, 0, DateTimeKind.Utc), "Completed", 197m, "user-cust-002" },
                    { 47, 3, new DateTime(2026, 7, 28, 18, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 7, 28, 18, 0, 0, 0, DateTimeKind.Utc), "Completed", 252.99m, "user-cust-003" },
                    { 48, 4, new DateTime(2026, 7, 29, 14, 30, 0, 0, DateTimeKind.Utc), new DateTime(2026, 7, 29, 14, 30, 0, 0, DateTimeKind.Utc), "Completed", 244m, "user-cust-004" },
                    { 49, 5, new DateTime(2026, 7, 30, 9, 40, 0, 0, DateTimeKind.Utc), new DateTime(2026, 7, 30, 9, 40, 0, 0, DateTimeKind.Utc), "Completed", 229m, "user-cust-001" },
                    { 50, 6, new DateTime(2026, 7, 31, 16, 15, 0, 0, DateTimeKind.Utc), new DateTime(2026, 7, 31, 16, 15, 0, 0, DateTimeKind.Utc), "Completed", 345m, "user-cust-002" },
                    { 51, 7, new DateTime(2026, 8, 2, 10, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 8, 2, 10, 0, 0, 0, DateTimeKind.Utc), "Completed", 708m, "user-cust-003" },
                    { 52, 8, new DateTime(2026, 8, 4, 14, 15, 0, 0, DateTimeKind.Utc), new DateTime(2026, 8, 4, 14, 15, 0, 0, DateTimeKind.Utc), "Completed", 657m, "user-cust-004" },
                    { 53, 1, new DateTime(2026, 8, 7, 11, 30, 0, 0, DateTimeKind.Utc), new DateTime(2026, 8, 7, 11, 30, 0, 0, DateTimeKind.Utc), "Completed", 409.98m, "user-cust-001" },
                    { 54, 2, new DateTime(2026, 8, 9, 16, 50, 0, 0, DateTimeKind.Utc), new DateTime(2026, 8, 9, 16, 50, 0, 0, DateTimeKind.Utc), "Completed", 348m, "user-cust-002" },
                    { 55, 3, new DateTime(2026, 8, 11, 9, 20, 0, 0, DateTimeKind.Utc), new DateTime(2026, 8, 11, 9, 20, 0, 0, DateTimeKind.Utc), "Completed", 256m, "user-cust-003" },
                    { 56, 4, new DateTime(2026, 8, 13, 13, 45, 0, 0, DateTimeKind.Utc), new DateTime(2026, 8, 13, 13, 45, 0, 0, DateTimeKind.Utc), "Completed", 473m, "user-cust-004" },
                    { 57, 5, new DateTime(2026, 8, 15, 17, 10, 0, 0, DateTimeKind.Utc), new DateTime(2026, 8, 15, 17, 10, 0, 0, DateTimeKind.Utc), "Completed", 512m, "user-cust-001" },
                    { 58, 6, new DateTime(2026, 8, 17, 12, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 8, 17, 12, 0, 0, 0, DateTimeKind.Utc), "Completed", 303m, "user-cust-002" },
                    { 59, 7, new DateTime(2026, 8, 18, 15, 30, 0, 0, DateTimeKind.Utc), new DateTime(2026, 8, 18, 15, 30, 0, 0, DateTimeKind.Utc), "Completed", 244m, "user-cust-003" },
                    { 60, 8, new DateTime(2026, 8, 20, 10, 15, 0, 0, DateTimeKind.Utc), new DateTime(2026, 8, 20, 10, 15, 0, 0, DateTimeKind.Utc), "Completed", 350m, "user-cust-004" },
                    { 61, 1, new DateTime(2026, 8, 21, 18, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 8, 21, 18, 0, 0, 0, DateTimeKind.Utc), "Completed", 249.48m, "user-cust-001" },
                    { 62, 2, new DateTime(2026, 8, 22, 14, 20, 0, 0, DateTimeKind.Utc), new DateTime(2026, 8, 22, 14, 20, 0, 0, DateTimeKind.Utc), "Completed", 348m, "user-cust-002" },
                    { 63, 3, new DateTime(2026, 8, 23, 11, 10, 0, 0, DateTimeKind.Utc), new DateTime(2026, 8, 23, 11, 10, 0, 0, DateTimeKind.Utc), "Processing", 228m, "user-cust-003" },
                    { 64, 4, new DateTime(2026, 8, 24, 16, 40, 0, 0, DateTimeKind.Utc), new DateTime(2026, 8, 24, 16, 40, 0, 0, DateTimeKind.Utc), "Processing", 146m, "user-cust-004" },
                    { 65, 5, new DateTime(2026, 8, 25, 9, 30, 0, 0, DateTimeKind.Utc), new DateTime(2026, 8, 25, 9, 30, 0, 0, DateTimeKind.Utc), "Processing", 254m, "user-cust-001" },
                    { 66, 6, new DateTime(2026, 8, 25, 15, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 8, 25, 15, 0, 0, 0, DateTimeKind.Utc), "Processing", 162m, "user-cust-002" }
                });

            migrationBuilder.InsertData(
                table: "ProductImages",
                columns: new[] { "Id", "ImageUrl", "IsMain", "ProductId" },
                values: new object[,]
                {
                    { 1, "/images/Products/1/img_1.jpg", true, 1 },
                    { 2, "/images/Products/1/img_2.jpg", false, 1 },
                    { 3, "/images/Products/1/img_3.jpg", false, 1 },
                    { 4, "/images/Products/1/img_4.jpg", false, 1 },
                    { 5, "/images/Products/2/img_1.jpg", true, 2 },
                    { 6, "/images/Products/2/img_2.jpg", false, 2 },
                    { 7, "/images/Products/2/img_3.jpg", false, 2 },
                    { 8, "/images/Products/2/img_4.jpg", false, 2 },
                    { 9, "/images/Products/3/img_1.jpg", true, 3 },
                    { 10, "/images/Products/3/img_2.jpg", false, 3 },
                    { 11, "/images/Products/3/img_3.jpg", false, 3 },
                    { 12, "/images/Products/3/img_4.jpg", false, 3 },
                    { 13, "/images/Products/4/img_1.jpg", true, 4 },
                    { 14, "/images/Products/4/img_2.jpg", false, 4 },
                    { 15, "/images/Products/4/img_3.jpg", false, 4 },
                    { 16, "/images/Products/4/img_4.jpg", false, 4 },
                    { 17, "/images/Products/5/img_1.jpg", true, 5 },
                    { 18, "/images/Products/5/img_2.jpg", false, 5 },
                    { 19, "/images/Products/5/img_3.jpg", false, 5 },
                    { 20, "/images/Products/5/img_4.jpg", false, 5 },
                    { 21, "/images/Products/6/img_1.jpg", true, 6 },
                    { 22, "/images/Products/6/img_2.jpg", false, 6 },
                    { 23, "/images/Products/6/img_3.jpg", false, 6 },
                    { 24, "/images/Products/6/img_4.jpg", false, 6 },
                    { 25, "/images/Products/7/img_1.jpg", true, 7 },
                    { 26, "/images/Products/7/img_2.jpg", false, 7 },
                    { 27, "/images/Products/7/img_3.jpg", false, 7 },
                    { 28, "/images/Products/7/img_4.jpg", false, 7 },
                    { 29, "/images/Products/8/img_1.jpg", true, 8 },
                    { 30, "/images/Products/8/img_2.jpg", false, 8 },
                    { 31, "/images/Products/8/img_3.jpg", false, 8 },
                    { 32, "/images/Products/8/img_4.jpg", false, 8 },
                    { 33, "/images/Products/9/img_1.jpg", true, 9 },
                    { 34, "/images/Products/9/img_2.jpg", false, 9 },
                    { 35, "/images/Products/9/img_3.jpg", false, 9 },
                    { 36, "/images/Products/9/img_4.jpg", false, 9 },
                    { 37, "/images/Products/10/img_1.jpg", true, 10 },
                    { 38, "/images/Products/10/img_2.jpg", false, 10 },
                    { 39, "/images/Products/10/img_3.jpg", false, 10 },
                    { 40, "/images/Products/10/img_4.jpg", false, 10 },
                    { 41, "/images/Products/11/img_1.jpg", true, 11 },
                    { 42, "/images/Products/11/img_2.jpg", false, 11 },
                    { 43, "/images/Products/11/img_3.jpg", false, 11 },
                    { 44, "/images/Products/11/img_4.jpg", false, 11 },
                    { 45, "/images/Products/12/img_1.jpg", true, 12 },
                    { 46, "/images/Products/12/img_2.jpg", false, 12 },
                    { 47, "/images/Products/12/img_3.jpg", false, 12 },
                    { 48, "/images/Products/12/img_4.jpg", false, 12 },
                    { 49, "/images/Products/13/img_1.png", true, 13 },
                    { 50, "/images/Products/13/img_2.png", false, 13 },
                    { 51, "/images/Products/13/img_3.png", false, 13 },
                    { 52, "/images/Products/13/img_4.png", false, 13 },
                    { 53, "/images/Products/14/img_1.png", true, 14 },
                    { 54, "/images/Products/14/img_2.png", false, 14 },
                    { 55, "/images/Products/14/img_3.png", false, 14 },
                    { 56, "/images/Products/14/img_4.png", false, 14 },
                    { 57, "/images/Products/15/img_1.png", true, 15 },
                    { 58, "/images/Products/15/img_2.png", false, 15 },
                    { 59, "/images/Products/15/img_3.png", false, 15 },
                    { 60, "/images/Products/15/img_4.png", false, 15 },
                    { 61, "/images/Products/16/img_1.png", true, 16 },
                    { 62, "/images/Products/16/img_2.png", false, 16 },
                    { 63, "/images/Products/16/img_3.png", false, 16 },
                    { 64, "/images/Products/16/img_4.png", false, 16 },
                    { 65, "/images/Products/17/img_1.png", true, 17 },
                    { 66, "/images/Products/17/img_2.png", false, 17 },
                    { 67, "/images/Products/17/img_3.png", false, 17 },
                    { 68, "/images/Products/18/img_1.png", true, 18 },
                    { 69, "/images/Products/18/img_2.png", false, 18 },
                    { 70, "/images/Products/18/img_3.png", false, 18 },
                    { 71, "/images/Products/18/img_4.png", false, 18 },
                    { 72, "/images/Products/19/img_1.png", true, 19 },
                    { 73, "/images/Products/19/img_2.png", false, 19 },
                    { 74, "/images/Products/19/img_3.png", false, 19 },
                    { 75, "/images/Products/20/img_1.png", true, 20 },
                    { 76, "/images/Products/20/img_2.png", false, 20 },
                    { 77, "/images/Products/20/img_3.png", false, 20 },
                    { 78, "/images/Products/21/img_1.png", true, 21 },
                    { 79, "/images/Products/21/img_2.png", false, 21 },
                    { 80, "/images/Products/21/img_3.png", false, 21 },
                    { 81, "/images/Products/21/img_4.png", false, 21 },
                    { 82, "/images/Products/22/img_1.png", true, 22 },
                    { 83, "/images/Products/22/img_2.png", false, 22 },
                    { 84, "/images/Products/22/img_3.png", false, 22 },
                    { 85, "/images/Products/23/img_1.png", true, 23 },
                    { 86, "/images/Products/23/img_2.png", false, 23 },
                    { 87, "/images/Products/23/img_3.png", false, 23 },
                    { 88, "/images/Products/23/img_4.png", false, 23 }
                });

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "Id", "ApprovedAt", "Comment", "CreatedAt", "ProductId", "Rating", "Status", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 8, 10, 11, 0, 0, 0, DateTimeKind.Utc), "Outstanding 4-person tent! Pitched it during heavy rain in the mountains and stayed bone dry. Setup took under 5 minutes.", new DateTime(2026, 8, 10, 10, 0, 0, 0, DateTimeKind.Utc), 1, 5, "Accepted", "user-cust-001" },
                    { 2, new DateTime(2026, 8, 12, 15, 30, 0, 0, DateTimeKind.Utc), "Spacious vestibules easily accommodated 4 large expedition packs. Superb ventilation even in warm summer evenings.", new DateTime(2026, 8, 12, 14, 30, 0, 0, DateTimeKind.Utc), 1, 5, "Accepted", "user-cust-002" },
                    { 3, new DateTime(2026, 8, 15, 10, 15, 0, 0, DateTimeKind.Utc), "High quality geodesic frame withstood gusty desert winds in Wadi Rum without flexing. Great tent!", new DateTime(2026, 8, 15, 9, 15, 0, 0, DateTimeKind.Utc), 1, 4, "Accepted", "user-cust-003" },
                    { 4, new DateTime(2026, 8, 11, 12, 0, 0, 0, DateTimeKind.Utc), "The weight distribution on this 65L pack is incredible. The lumbar support saved my back on a 4-day ridge traverse.", new DateTime(2026, 8, 11, 11, 0, 0, 0, DateTimeKind.Utc), 2, 4, "Accepted", "user-cust-002" },
                    { 5, new DateTime(2026, 8, 13, 17, 45, 0, 0, DateTimeKind.Utc), "Top quality Cordura fabric and waterproof rain cover included. Carried 22kg effortlessly.", new DateTime(2026, 8, 13, 16, 45, 0, 0, DateTimeKind.Utc), 2, 5, "Accepted", "user-cust-004" },
                    { 6, new DateTime(2026, 8, 16, 14, 20, 0, 0, DateTimeKind.Utc), "Multiple attachment loops for trekking poles and ice axes. Sturdy hip belt padding.", new DateTime(2026, 8, 16, 13, 20, 0, 0, DateTimeKind.Utc), 2, 4, "Accepted", "user-cust-001" },
                    { 7, new DateTime(2026, 8, 9, 16, 10, 0, 0, DateTimeKind.Utc), "Surprisingly sturdy for how small it folds down. Fits right into the bottle pocket of my backpack.", new DateTime(2026, 8, 9, 15, 10, 0, 0, DateTimeKind.Utc), 3, 5, "Accepted", "user-cust-003" },
                    { 8, new DateTime(2026, 8, 14, 19, 0, 0, 0, DateTimeKind.Utc), "Supports my weight without creaking. Used it during lunch breaks on every mountain hike.", new DateTime(2026, 8, 14, 18, 0, 0, 0, DateTimeKind.Utc), 3, 4, "Accepted", "user-cust-002" },
                    { 9, new DateTime(2026, 8, 17, 13, 40, 0, 0, DateTimeKind.Utc), "Ultra-lightweight aviation aluminum frame. Takes 3 seconds to unfold.", new DateTime(2026, 8, 17, 12, 40, 0, 0, DateTimeKind.Utc), 3, 4, "Accepted", "user-cust-004" },
                    { 10, new DateTime(2026, 8, 8, 9, 30, 0, 0, DateTimeKind.Utc), "Super warm goose down sleeping bag for sub-zero mountain nights. Extremely compact when compressed.", new DateTime(2026, 8, 8, 8, 30, 0, 0, DateTimeKind.Utc), 4, 5, "Accepted", "user-cust-001" },
                    { 11, new DateTime(2026, 8, 13, 21, 15, 0, 0, DateTimeKind.Utc), "The 750-fill power loft is unmatched. Draft collar kept body heat sealed in during a freezing night.", new DateTime(2026, 8, 13, 20, 15, 0, 0, DateTimeKind.Utc), 4, 4, "Accepted", "user-cust-003" },
                    { 12, new DateTime(2026, 8, 18, 11, 50, 0, 0, DateTimeKind.Utc), "Silky ripstop lining and very comfortable 3D footbox. Packs into a tiny compression dry bag.", new DateTime(2026, 8, 18, 10, 50, 0, 0, DateTimeKind.Utc), 4, 5, "Accepted", "user-cust-002" },
                    { 13, new DateTime(2026, 8, 10, 18, 25, 0, 0, DateTimeKind.Utc), "Only 48 grams! Boiled 500ml of water in under 2 minutes at 2,500m elevation. Micro flame control is buttery smooth.", new DateTime(2026, 8, 10, 17, 25, 0, 0, DateTimeKind.Utc), 5, 5, "Accepted", "user-cust-004" },
                    { 14, new DateTime(2026, 8, 15, 12, 35, 0, 0, DateTimeKind.Utc), "The titanium pot support arms are rock solid and fold down into a miniature hard case. Essential backpacking gear.", new DateTime(2026, 8, 15, 11, 35, 0, 0, DateTimeKind.Utc), 5, 5, "Accepted", "user-cust-001" },
                    { 15, new DateTime(2026, 8, 19, 15, 0, 0, 0, DateTimeKind.Utc), "Remarkable fuel efficiency with standard isobutane canisters. Excellent wind resistance.", new DateTime(2026, 8, 19, 14, 0, 0, 0, DateTimeKind.Utc), 5, 4, "Accepted", "user-cust-003" },
                    { 16, new DateTime(2026, 8, 7, 14, 0, 0, 0, DateTimeKind.Utc), "Exceptional grip on rocky trails in Petra and Wadi Rum. Completely waterproof and comfortable all day.", new DateTime(2026, 8, 7, 13, 0, 0, 0, DateTimeKind.Utc), 6, 5, "Accepted", "user-cust-004" },
                    { 17, new DateTime(2026, 8, 12, 10, 45, 0, 0, DateTimeKind.Utc), "Minimal break-in period required! Hiked 18km on day one through wet scree and mud with zero blisters.", new DateTime(2026, 8, 12, 9, 45, 0, 0, DateTimeKind.Utc), 6, 4, "Accepted", "user-cust-002" },
                    { 18, new DateTime(2026, 8, 17, 16, 30, 0, 0, DateTimeKind.Utc), "Heavy-duty oiled nubuck leather and solid ankle protection. Best hiking boots I've owned.", new DateTime(2026, 8, 17, 15, 30, 0, 0, DateTimeKind.Utc), 6, 5, "Accepted", "user-cust-001" },
                    { 19, new DateTime(2026, 8, 11, 22, 0, 0, 0, DateTimeKind.Utc), "Terrific camp lantern. The warm campfire mode is great inside the tent and it charged my phone on the trail.", new DateTime(2026, 8, 11, 21, 0, 0, 0, DateTimeKind.Utc), 7, 4, "Accepted", "user-cust-001" },
                    { 20, new DateTime(2026, 8, 14, 21, 30, 0, 0, DateTimeKind.Utc), "Blindingly bright on 1200 lumen turbo mode, yet battery lasted all weekend on medium mode.", new DateTime(2026, 8, 14, 20, 30, 0, 0, DateTimeKind.Utc), 7, 5, "Accepted", "user-cust-003" },
                    { 21, new DateTime(2026, 8, 18, 20, 15, 0, 0, DateTimeKind.Utc), "The magnetic base and hanging hook make this super versatile around the campsite.", new DateTime(2026, 8, 18, 19, 15, 0, 0, DateTimeKind.Utc), 7, 4, "Accepted", "user-cust-004" },
                    { 22, new DateTime(2026, 8, 9, 15, 20, 0, 0, DateTimeKind.Utc), "Solid stainless steel build. The spring-loaded needle-nose pliers and wood saw performed flawlessly.", new DateTime(2026, 8, 9, 14, 20, 0, 0, DateTimeKind.Utc), 8, 5, "Accepted", "user-cust-002" },
                    { 23, new DateTime(2026, 8, 13, 11, 10, 0, 0, DateTimeKind.Utc), "One-handed blade deployment and locking mechanisms feel premium and secure. Great EDC tool.", new DateTime(2026, 8, 13, 10, 10, 0, 0, DateTimeKind.Utc), 8, 4, "Accepted", "user-cust-001" },
                    { 24, new DateTime(2026, 8, 18, 17, 40, 0, 0, DateTimeKind.Utc), "Heavy duty and versatile. Used the wire cutters and screwdrivers to fix my camp stove in the field.", new DateTime(2026, 8, 18, 16, 40, 0, 0, DateTimeKind.Utc), 8, 4, "Accepted", "user-cust-003" },
                    { 25, new DateTime(2026, 8, 10, 17, 0, 0, 0, DateTimeKind.Utc), "Left this in a hot car all day in the desert and the ice was still intact 18 hours later! Incredible insulation.", new DateTime(2026, 8, 10, 16, 0, 0, 0, DateTimeKind.Utc), 9, 5, "Accepted", "user-cust-003" },
                    { 26, new DateTime(2026, 8, 15, 13, 20, 0, 0, DateTimeKind.Utc), "Wide mouth makes it easy to add large ice cubes and clean. Tough powder coat doesn't scratch.", new DateTime(2026, 8, 15, 12, 20, 0, 0, DateTimeKind.Utc), 9, 4, "Accepted", "user-cust-004" },
                    { 27, new DateTime(2026, 8, 20, 18, 50, 0, 0, DateTimeKind.Utc), "Sturdy stainless steel handle makes carrying 1.5L very easy on long hikes.", new DateTime(2026, 8, 20, 17, 50, 0, 0, DateTimeKind.Utc), 9, 5, "Accepted", "user-cust-002" },
                    { 28, new DateTime(2026, 8, 8, 20, 30, 0, 0, DateTimeKind.Utc), "Crisp and clear BAK-4 optics for desert stargazing and mountain wildlife observation. Emerald lenses give brilliant contrast.", new DateTime(2026, 8, 8, 19, 30, 0, 0, DateTimeKind.Utc), 10, 5, "Accepted", "user-cust-004" },
                    { 29, new DateTime(2026, 8, 14, 12, 15, 0, 0, DateTimeKind.Utc), "Rubber armor provides great non-slip grip in wet weather. Smooth central focus wheel.", new DateTime(2026, 8, 14, 11, 15, 0, 0, DateTimeKind.Utc), 10, 5, "Accepted", "user-cust-001" },
                    { 30, new DateTime(2026, 8, 19, 16, 45, 0, 0, DateTimeKind.Utc), "Comes with a nice padded strap and protective case. High quality glass for the price.", new DateTime(2026, 8, 19, 15, 45, 0, 0, DateTimeKind.Utc), 10, 4, "Accepted", "user-cust-002" },
                    { 31, new DateTime(2026, 8, 11, 9, 0, 0, 0, DateTimeKind.Utc), "3 inches of thickness means zero feeling of rocks or cold ground underneath. Slept like a baby!", new DateTime(2026, 8, 11, 8, 0, 0, 0, DateTimeKind.Utc), 11, 4, "Accepted", "user-cust-001" },
                    { 32, new DateTime(2026, 8, 16, 15, 10, 0, 0, DateTimeKind.Utc), "Brass valve allows rapid self-inflation. Rolls up surprisingly compact into the included stuff sack.", new DateTime(2026, 8, 16, 14, 10, 0, 0, DateTimeKind.Utc), 11, 5, "Accepted", "user-cust-003" },
                    { 33, new DateTime(2026, 8, 21, 11, 30, 0, 0, DateTimeKind.Utc), "Contoured horizontal baffles keep you centered all night. Rugged ripstop fabric.", new DateTime(2026, 8, 21, 10, 30, 0, 0, DateTimeKind.Utc), 11, 4, "Accepted", "user-cust-004" },
                    { 34, new DateTime(2026, 8, 9, 14, 40, 0, 0, DateTimeKind.Utc), "Natural cork handles absorb sweat and feel ergonomic all day. The quick-lock clamps never slip.", new DateTime(2026, 8, 9, 13, 40, 0, 0, DateTimeKind.Utc), 12, 5, "Accepted", "user-cust-002" },
                    { 35, new DateTime(2026, 8, 15, 10, 25, 0, 0, DateTimeKind.Utc), "Extremely lightweight carbon fiber. Saved my knees on a 1,200m rocky descent.", new DateTime(2026, 8, 15, 9, 25, 0, 0, DateTimeKind.Utc), 12, 4, "Accepted", "user-cust-004" },
                    { 36, new DateTime(2026, 8, 20, 17, 15, 0, 0, DateTimeKind.Utc), "Tungsten carbide tips bite securely into hard rock and dirt. High grade trekking poles.", new DateTime(2026, 8, 20, 16, 15, 0, 0, DateTimeKind.Utc), 12, 5, "Accepted", "user-cust-001" },
                    { 37, new DateTime(2026, 8, 8, 12, 20, 0, 0, DateTimeKind.Utc), "Unpacked and set up literally in 10 seconds! Blackout coating keeps the interior completely dark and cool for sleeping past sunrise.", new DateTime(2026, 8, 8, 11, 20, 0, 0, DateTimeKind.Utc), 13, 4, "Accepted", "user-cust-003" },
                    { 38, new DateTime(2026, 8, 13, 19, 30, 0, 0, DateTimeKind.Utc), "Perfect for weekend camping trips. Waterproof floor kept moisture out during morning dew.", new DateTime(2026, 8, 13, 18, 30, 0, 0, DateTimeKind.Utc), 13, 5, "Accepted", "user-cust-001" },
                    { 39, new DateTime(2026, 8, 18, 13, 0, 0, 0, DateTimeKind.Utc), "Plenty of headroom and very easy to fold back down with the circular guide.", new DateTime(2026, 8, 18, 12, 0, 0, 0, DateTimeKind.Utc), 13, 4, "Accepted", "user-cust-002" },
                    { 40, new DateTime(2026, 8, 10, 16, 50, 0, 0, DateTimeKind.Utc), "The red turn spigot has perfect water flow control. Hung it from a branch for an instant camp washing station.", new DateTime(2026, 8, 10, 15, 50, 0, 0, DateTimeKind.Utc), 14, 5, "Accepted", "user-cust-004" },
                    { 41, new DateTime(2026, 8, 16, 12, 10, 0, 0, DateTimeKind.Utc), "Folds completely flat and weighs almost nothing when empty. No plastic taste at all.", new DateTime(2026, 8, 16, 11, 10, 0, 0, DateTimeKind.Utc), 14, 4, "Accepted", "user-cust-002" },
                    { 42, new DateTime(2026, 8, 21, 18, 40, 0, 0, DateTimeKind.Utc), "Sturdy top handle supports the full 8L weight easily. Great for group cooking at camp.", new DateTime(2026, 8, 21, 17, 40, 0, 0, DateTimeKind.Utc), 14, 5, "Accepted", "user-cust-001" },
                    { 43, new DateTime(2026, 8, 7, 17, 30, 0, 0, DateTimeKind.Utc), "The beechwood armrests and thick padded cushion make this the most comfortable camp chair ever made.", new DateTime(2026, 8, 7, 16, 30, 0, 0, DateTimeKind.Utc), 15, 5, "Accepted", "user-cust-001" },
                    { 44, new DateTime(2026, 8, 12, 20, 20, 0, 0, DateTimeKind.Utc), "Solid folding frame. The removable cushion allows cooler mesh airflow during hot days.", new DateTime(2026, 8, 12, 19, 20, 0, 0, DateTimeKind.Utc), 15, 5, "Accepted", "user-cust-003" },
                    { 45, new DateTime(2026, 8, 19, 14, 15, 0, 0, DateTimeKind.Utc), "Feels like high-end patio furniture that folds down into a travel bag. Highly recommended.", new DateTime(2026, 8, 19, 13, 15, 0, 0, DateTimeKind.Utc), 15, 4, "Accepted", "user-cust-004" },
                    { 46, new DateTime(2026, 8, 9, 11, 45, 0, 0, DateTimeKind.Utc), "The separate shoe compartment and waterproof wet pouch are genius! Perfect for dirty hiking trail shoes.", new DateTime(2026, 8, 9, 10, 45, 0, 0, DateTimeKind.Utc), 16, 4, "Accepted", "user-cust-002" },
                    { 47, new DateTime(2026, 8, 14, 16, 30, 0, 0, DateTimeKind.Utc), "Weighs under a pound yet carries 40L effortlessly. Breathable shoulder straps stay cool.", new DateTime(2026, 8, 14, 15, 30, 0, 0, DateTimeKind.Utc), 16, 5, "Accepted", "user-cust-001" },
                    { 48, new DateTime(2026, 8, 20, 12, 0, 0, 0, DateTimeKind.Utc), "Folds into its own tiny inner pocket when traveling. Great secondary daypack.", new DateTime(2026, 8, 20, 11, 0, 0, 0, DateTimeKind.Utc), 16, 4, "Accepted", "user-cust-003" },
                    { 49, new DateTime(2026, 8, 11, 13, 15, 0, 0, DateTimeKind.Utc), "Complete 24-piece dining set with plates, cutlery, tongs, and straws. The zippered organizer bag keeps everything neat and quiet.", new DateTime(2026, 8, 11, 12, 15, 0, 0, DateTimeKind.Utc), 17, 5, "Accepted", "user-cust-004" },
                    { 50, new DateTime(2026, 8, 17, 19, 25, 0, 0, DateTimeKind.Utc), "Food-grade stainless steel washes clean instantly at camp. Plates are heavy duty and don't bend.", new DateTime(2026, 8, 17, 18, 25, 0, 0, DateTimeKind.Utc), 17, 4, "Accepted", "user-cust-002" },
                    { 51, new DateTime(2026, 8, 22, 15, 0, 0, 0, DateTimeKind.Utc), "Must-have tableware kit for family camping weekends.", new DateTime(2026, 8, 22, 14, 0, 0, 0, DateTimeKind.Utc), 17, 5, "Accepted", "user-cust-001" },
                    { 52, new DateTime(2026, 8, 8, 15, 0, 0, 0, DateTimeKind.Utc), "Total 360-degree sun and wind protection in the desert. The detachable neck drape and face cover saved my skin.", new DateTime(2026, 8, 8, 14, 0, 0, 0, DateTimeKind.Utc), 18, 5, "Accepted", "user-cust-001" },
                    { 53, new DateTime(2026, 8, 13, 18, 10, 0, 0, DateTimeKind.Utc), "Breathable crown mesh keeps your head cool. Adjustable chin toggle keeps it secure in gusty winds.", new DateTime(2026, 8, 13, 17, 10, 0, 0, DateTimeKind.Utc), 18, 5, "Accepted", "user-cust-004" },
                    { 54, new DateTime(2026, 8, 18, 10, 30, 0, 0, DateTimeKind.Utc), "Lightweight, quick-drying fabric. Foldable into any backpack pocket.", new DateTime(2026, 8, 18, 9, 30, 0, 0, DateTimeKind.Utc), 18, 4, "Accepted", "user-cust-003" },
                    { 55, new DateTime(2026, 8, 10, 21, 0, 0, 0, DateTimeKind.Utc), "Beautiful solid polished brass construction. The luminous dial glows brightly in complete darkness for night navigation.", new DateTime(2026, 8, 10, 20, 0, 0, 0, DateTimeKind.Utc), 19, 4, "Accepted", "user-cust-002" },
                    { 56, new DateTime(2026, 8, 15, 17, 45, 0, 0, DateTimeKind.Utc), "High damping fluid ensures needle settles quickly and accurately. Heavy, premium heirloom feel.", new DateTime(2026, 8, 15, 16, 45, 0, 0, DateTimeKind.Utc), 19, 5, "Accepted", "user-cust-003" },
                    { 57, new DateTime(2026, 8, 21, 12, 20, 0, 0, DateTimeKind.Utc), "The protective flip lid and lanyard loop make this a great survival navigation tool.", new DateTime(2026, 8, 21, 11, 20, 0, 0, DateTimeKind.Utc), 19, 4, "Accepted", "user-cust-001" },
                    { 58, new DateTime(2026, 8, 12, 14, 50, 0, 0, DateTimeKind.Utc), "Finally my camp kitchen is organized! Holds my stove, 4 gas canisters, pans, utensils, and seasonings with customizable velcro dividers.", new DateTime(2026, 8, 12, 13, 50, 0, 0, DateTimeKind.Utc), 20, 5, "Accepted", "user-cust-004" },
                    { 59, new DateTime(2026, 8, 16, 20, 10, 0, 0, DateTimeKind.Utc), "Rigid sidewalls protect delicate gear and the MOLLE exterior webbing lets you clip extra pouches.", new DateTime(2026, 8, 16, 19, 10, 0, 0, DateTimeKind.Utc), 20, 4, "Accepted", "user-cust-001" },
                    { 60, new DateTime(2026, 8, 22, 11, 40, 0, 0, DateTimeKind.Utc), "Heavy-duty 900D Oxford fabric with reinforced carry handles. Excellent gear tote.", new DateTime(2026, 8, 22, 10, 40, 0, 0, DateTimeKind.Utc), 20, 5, "Accepted", "user-cust-002" },
                    { 61, new DateTime(2026, 8, 7, 12, 0, 0, 0, DateTimeKind.Utc), "Holds my stainless water bottle snugly without bouncing while trail running or hiking. Lots of organizer pockets.", new DateTime(2026, 8, 7, 11, 0, 0, 0, DateTimeKind.Utc), 21, 4, "Accepted", "user-cust-003" },
                    { 62, new DateTime(2026, 8, 14, 18, 35, 0, 0, DateTimeKind.Utc), "Breathable lumbar mesh and wide waist belt distribute the weight comfortably.", new DateTime(2026, 8, 14, 17, 35, 0, 0, DateTimeKind.Utc), 21, 5, "Accepted", "user-cust-002" },
                    { 63, new DateTime(2026, 8, 19, 15, 50, 0, 0, DateTimeKind.Utc), "Rugged tactical look with durable zippers and side MOLLE attachments.", new DateTime(2026, 8, 19, 14, 50, 0, 0, DateTimeKind.Utc), 21, 4, "Accepted", "user-cust-004" },
                    { 64, new DateTime(2026, 8, 9, 10, 15, 0, 0, DateTimeKind.Utc), "Instant cooling sensation when you put them on! Thumb holes keep them in place and UV protection is 100% effective.", new DateTime(2026, 8, 9, 9, 15, 0, 0, DateTimeKind.Utc), 22, 5, "Accepted", "user-cust-001" },
                    { 65, new DateTime(2026, 8, 15, 16, 0, 0, 0, DateTimeKind.Utc), "Compression fit prevents muscle fatigue on long mountain treks. Dries sweat instantly.", new DateTime(2026, 8, 15, 15, 0, 0, 0, DateTimeKind.Utc), 22, 4, "Accepted", "user-cust-004" },
                    { 66, new DateTime(2026, 8, 21, 19, 10, 0, 0, DateTimeKind.Utc), "Great elasticity, doesn't slip down the bicep. Reflective logo is a nice safety touch for evening runs.", new DateTime(2026, 8, 21, 18, 10, 0, 0, DateTimeKind.Utc), 22, 5, "Accepted", "user-cust-002" },
                    { 67, new DateTime(2026, 8, 11, 11, 30, 0, 0, DateTimeKind.Utc), "Folds down to just 36cm and fits inside my carry-on backpack! Internal steel cable lock is rock solid.", new DateTime(2026, 8, 11, 10, 30, 0, 0, DateTimeKind.Utc), 23, 5, "Accepted", "user-cust-004" },
                    { 68, new DateTime(2026, 8, 17, 17, 20, 0, 0, DateTimeKind.Utc), "Natural cork handle feels amazing in the hand with zero blisters. Quick cam lever makes height adjustment effortless.", new DateTime(2026, 8, 17, 16, 20, 0, 0, DateTimeKind.Utc), 23, 5, "Accepted", "user-cust-001" },
                    { 69, new DateTime(2026, 8, 22, 14, 45, 0, 0, DateTimeKind.Utc), "Tested on rocky ascents with full pack weight. Carbon-alloy construction is ultralight yet rugged.", new DateTime(2026, 8, 22, 13, 45, 0, 0, DateTimeKind.Utc), 23, 4, "Accepted", "user-cust-003" },
                    { 70, null, "The multi-tool is great quality, but the outer packaging was slightly squished during delivery.", new DateTime(2026, 8, 23, 19, 0, 0, 0, DateTimeKind.Utc), 8, 2, "Rejected", "user-cust-002" },
                    { 71, null, "Crisp and clear BAK-4 optics for desert stargazing and mountain wildlife observation.", new DateTime(2026, 8, 24, 10, 0, 0, 0, DateTimeKind.Utc), 10, 5, "Pending", "user-cust-004" },
                    { 72, null, "Very comfortable chair, testing it again this weekend at the lake.", new DateTime(2026, 8, 24, 14, 0, 0, 0, DateTimeKind.Utc), 15, 4, "Pending", "user-cust-001" }
                });

            migrationBuilder.InsertData(
                table: "OrderItems",
                columns: new[] { "Id", "OrderId", "ProductId", "Quantity", "UnitPrice" },
                values: new object[,]
                {
                    { 1, 1, 1, 1, 189m },
                    { 2, 1, 3, 1, 38m },
                    { 3, 2, 2, 1, 149m },
                    { 4, 2, 5, 1, 42m },
                    { 5, 2, 9, 1, 36m },
                    { 6, 3, 4, 1, 165m },
                    { 7, 3, 11, 1, 52m },
                    { 8, 4, 6, 1, 135m },
                    { 9, 4, 12, 1, 64m },
                    { 10, 4, 18, 1, 22m },
                    { 11, 5, 10, 1, 89m },
                    { 12, 5, 7, 1, 39m },
                    { 13, 5, 8, 1, 49m },
                    { 14, 6, 15, 2, 68m },
                    { 15, 6, 20, 1, 52m },
                    { 16, 7, 1, 1, 189m },
                    { 17, 7, 16, 1, 45m },
                    { 18, 7, 22, 2, 14m },
                    { 19, 8, 2, 1, 149m },
                    { 20, 8, 6, 1, 135m },
                    { 21, 9, 4, 1, 165m },
                    { 22, 9, 7, 1, 39m },
                    { 23, 9, 14, 2, 18.5m },
                    { 24, 10, 13, 1, 79.99m },
                    { 25, 10, 15, 1, 68m },
                    { 26, 10, 17, 1, 34m },
                    { 27, 11, 8, 1, 49m },
                    { 28, 11, 10, 1, 89m },
                    { 29, 11, 19, 1, 26m },
                    { 30, 12, 23, 2, 58m },
                    { 31, 12, 21, 1, 29.5m },
                    { 32, 12, 9, 1, 36m },
                    { 33, 13, 5, 2, 42m },
                    { 34, 13, 20, 1, 52m },
                    { 35, 13, 18, 1, 22m },
                    { 36, 14, 6, 1, 135m },
                    { 37, 14, 11, 1, 52m },
                    { 38, 15, 1, 1, 189m },
                    { 39, 15, 4, 1, 165m },
                    { 40, 15, 3, 2, 38m },
                    { 41, 16, 2, 1, 149m },
                    { 42, 16, 10, 1, 89m },
                    { 43, 16, 12, 1, 64m },
                    { 44, 17, 6, 2, 135m },
                    { 45, 17, 16, 1, 45m },
                    { 46, 18, 15, 2, 68m },
                    { 47, 18, 17, 1, 34m },
                    { 48, 18, 7, 1, 39m },
                    { 49, 19, 13, 2, 79.99m },
                    { 50, 19, 14, 1, 18.5m },
                    { 51, 20, 8, 2, 49m },
                    { 52, 20, 9, 2, 36m },
                    { 53, 20, 19, 1, 26m },
                    { 54, 21, 20, 2, 52m },
                    { 55, 21, 5, 1, 42m },
                    { 56, 21, 22, 2, 14m },
                    { 57, 22, 23, 1, 58m },
                    { 58, 22, 18, 2, 22m },
                    { 59, 22, 21, 1, 29.5m },
                    { 60, 23, 4, 1, 165m },
                    { 61, 23, 11, 2, 52m },
                    { 62, 24, 1, 1, 189m },
                    { 63, 24, 2, 1, 149m },
                    { 64, 25, 1, 2, 189m },
                    { 65, 25, 3, 2, 38m },
                    { 66, 25, 7, 1, 39m },
                    { 67, 26, 2, 2, 149m },
                    { 68, 26, 6, 1, 135m },
                    { 69, 26, 12, 1, 64m },
                    { 70, 27, 4, 1, 165m },
                    { 71, 27, 10, 1, 89m },
                    { 72, 27, 8, 1, 49m },
                    { 73, 28, 13, 2, 79.99m },
                    { 74, 28, 15, 1, 68m },
                    { 75, 28, 17, 2, 34m },
                    { 76, 29, 16, 2, 45m },
                    { 77, 29, 20, 1, 52m },
                    { 78, 29, 21, 1, 29.5m },
                    { 79, 30, 23, 2, 58m },
                    { 80, 30, 5, 2, 42m },
                    { 81, 30, 9, 2, 36m },
                    { 82, 31, 6, 1, 135m },
                    { 83, 31, 11, 2, 52m },
                    { 84, 31, 18, 2, 22m },
                    { 85, 32, 10, 2, 89m },
                    { 86, 32, 19, 2, 26m },
                    { 87, 32, 22, 3, 14m },
                    { 88, 33, 1, 1, 189m },
                    { 89, 33, 4, 1, 165m },
                    { 90, 33, 14, 1, 18.5m },
                    { 91, 34, 2, 1, 149m },
                    { 92, 34, 15, 2, 68m },
                    { 93, 35, 6, 1, 135m },
                    { 94, 35, 8, 2, 49m },
                    { 95, 35, 7, 1, 39m },
                    { 96, 36, 13, 1, 79.99m },
                    { 97, 36, 20, 1, 52m },
                    { 98, 36, 23, 1, 58m },
                    { 99, 37, 1, 2, 189m },
                    { 100, 37, 2, 2, 149m },
                    { 101, 38, 4, 2, 165m },
                    { 102, 38, 6, 2, 135m },
                    { 103, 39, 10, 2, 89m },
                    { 104, 39, 12, 2, 64m },
                    { 105, 39, 3, 2, 38m },
                    { 106, 40, 13, 3, 79.99m },
                    { 107, 40, 15, 2, 68m },
                    { 108, 41, 16, 2, 45m },
                    { 109, 41, 23, 2, 58m },
                    { 110, 41, 5, 2, 42m },
                    { 111, 42, 20, 2, 52m },
                    { 112, 42, 17, 2, 34m },
                    { 113, 42, 8, 2, 49m },
                    { 114, 43, 6, 2, 135m },
                    { 115, 43, 9, 3, 36m },
                    { 116, 43, 21, 2, 29.5m },
                    { 117, 44, 1, 1, 189m },
                    { 118, 44, 11, 2, 52m },
                    { 119, 44, 7, 2, 39m },
                    { 120, 45, 4, 1, 165m },
                    { 121, 45, 2, 1, 149m },
                    { 122, 45, 18, 2, 22m },
                    { 123, 46, 10, 1, 89m },
                    { 124, 46, 19, 2, 26m },
                    { 125, 46, 22, 4, 14m },
                    { 126, 47, 15, 2, 68m },
                    { 127, 47, 13, 1, 79.99m },
                    { 128, 47, 14, 2, 18.5m },
                    { 129, 48, 23, 2, 58m },
                    { 130, 48, 12, 2, 64m },
                    { 131, 49, 6, 1, 135m },
                    { 132, 49, 16, 1, 45m },
                    { 133, 49, 8, 1, 49m },
                    { 134, 50, 1, 1, 189m },
                    { 135, 50, 5, 2, 42m },
                    { 136, 50, 9, 2, 36m },
                    { 137, 51, 1, 2, 189m },
                    { 138, 51, 4, 2, 165m },
                    { 139, 52, 2, 2, 149m },
                    { 140, 52, 6, 2, 135m },
                    { 141, 52, 10, 1, 89m },
                    { 142, 53, 13, 2, 79.99m },
                    { 143, 53, 15, 2, 68m },
                    { 144, 53, 3, 3, 38m },
                    { 145, 54, 23, 2, 58m },
                    { 146, 54, 12, 2, 64m },
                    { 147, 54, 20, 2, 52m },
                    { 148, 55, 16, 2, 45m },
                    { 149, 55, 17, 2, 34m },
                    { 150, 55, 8, 2, 49m },
                    { 151, 56, 1, 1, 189m },
                    { 152, 56, 2, 1, 149m },
                    { 153, 56, 6, 1, 135m },
                    { 154, 57, 4, 2, 165m },
                    { 155, 57, 11, 2, 52m },
                    { 156, 57, 7, 2, 39m },
                    { 157, 58, 10, 2, 89m },
                    { 158, 58, 21, 2, 29.5m },
                    { 159, 58, 18, 3, 22m },
                    { 160, 59, 15, 2, 68m },
                    { 161, 59, 19, 2, 26m },
                    { 162, 59, 22, 4, 14m },
                    { 163, 60, 23, 2, 58m },
                    { 164, 60, 5, 3, 42m },
                    { 165, 60, 9, 3, 36m },
                    { 166, 61, 13, 2, 79.99m },
                    { 167, 61, 14, 3, 18.5m },
                    { 168, 61, 17, 1, 34m },
                    { 169, 62, 2, 1, 149m },
                    { 170, 62, 6, 1, 135m },
                    { 171, 62, 12, 1, 64m },
                    { 172, 63, 1, 1, 189m },
                    { 173, 63, 7, 1, 39m },
                    { 174, 64, 16, 1, 45m },
                    { 175, 64, 20, 1, 52m },
                    { 176, 64, 8, 1, 49m },
                    { 177, 65, 4, 1, 165m },
                    { 178, 65, 10, 1, 89m },
                    { 179, 66, 23, 1, 58m },
                    { 180, 66, 15, 1, 68m },
                    { 181, 66, 9, 1, 36m }
                });

            migrationBuilder.InsertData(
                table: "Payments",
                columns: new[] { "Id", "Amount", "OrderId", "PaidAt", "PaymentMethod", "Status", "TransactionId" },
                values: new object[,]
                {
                    { 1, 227m, 1, new DateTime(2026, 3, 5, 14, 20, 0, 0, DateTimeKind.Utc), "Credit Card", "Completed", "TXN-100373" },
                    { 2, 227m, 2, new DateTime(2026, 3, 10, 11, 15, 0, 0, DateTimeKind.Utc), "PayPal", "Completed", "TXN-100746" },
                    { 3, 217m, 3, new DateTime(2026, 3, 16, 16, 40, 0, 0, DateTimeKind.Utc), "Credit Card", "Completed", "TXN-101119" },
                    { 4, 221m, 4, new DateTime(2026, 3, 21, 9, 30, 0, 0, DateTimeKind.Utc), "Apple Pay", "Completed", "TXN-101492" },
                    { 5, 177m, 5, new DateTime(2026, 3, 26, 13, 0, 0, 0, DateTimeKind.Utc), "Credit Card", "Completed", "TXN-101865" },
                    { 6, 188m, 6, null, "Credit Card", "Failed", "TXN-102238" },
                    { 7, 262m, 7, new DateTime(2026, 4, 3, 10, 0, 0, 0, DateTimeKind.Utc), "Credit Card", "Completed", "TXN-102611" },
                    { 8, 284m, 8, new DateTime(2026, 4, 7, 15, 25, 0, 0, DateTimeKind.Utc), "PayPal", "Completed", "TXN-102984" },
                    { 9, 241m, 9, new DateTime(2026, 4, 12, 12, 10, 0, 0, DateTimeKind.Utc), "Apple Pay", "Completed", "TXN-103357" },
                    { 10, 181.99m, 10, new DateTime(2026, 4, 16, 17, 45, 0, 0, DateTimeKind.Utc), "Credit Card", "Completed", "TXN-103730" },
                    { 11, 164m, 11, new DateTime(2026, 4, 20, 11, 30, 0, 0, DateTimeKind.Utc), "Cash on Delivery", "Completed", "TXN-104103" },
                    { 12, 181.5m, 12, new DateTime(2026, 4, 23, 14, 0, 0, 0, DateTimeKind.Utc), "Credit Card", "Completed", "TXN-104476" },
                    { 13, 158m, 13, new DateTime(2026, 4, 27, 16, 20, 0, 0, DateTimeKind.Utc), "PayPal", "Completed", "TXN-104849" },
                    { 14, 187m, 14, new DateTime(2026, 4, 29, 9, 50, 0, 0, DateTimeKind.Utc), "Credit Card", "Completed", "TXN-105222" },
                    { 15, 430m, 15, new DateTime(2026, 5, 2, 11, 0, 0, 0, DateTimeKind.Utc), "Credit Card", "Completed", "TXN-105595" },
                    { 16, 302m, 16, new DateTime(2026, 5, 5, 14, 30, 0, 0, DateTimeKind.Utc), "Apple Pay", "Completed", "TXN-105968" },
                    { 17, 315m, 17, new DateTime(2026, 5, 9, 10, 15, 0, 0, DateTimeKind.Utc), "Credit Card", "Completed", "TXN-106341" },
                    { 18, 209m, 18, new DateTime(2026, 5, 13, 16, 0, 0, 0, DateTimeKind.Utc), "PayPal", "Completed", "TXN-106714" },
                    { 19, 178.48m, 19, new DateTime(2026, 5, 16, 13, 40, 0, 0, DateTimeKind.Utc), "Credit Card", "Completed", "TXN-107087" },
                    { 20, 196m, 20, new DateTime(2026, 5, 20, 9, 20, 0, 0, DateTimeKind.Utc), "Apple Pay", "Completed", "TXN-107460" },
                    { 21, 174m, 21, new DateTime(2026, 5, 23, 15, 10, 0, 0, DateTimeKind.Utc), "Credit Card", "Completed", "TXN-107833" },
                    { 22, 131.5m, 22, new DateTime(2026, 5, 26, 17, 35, 0, 0, DateTimeKind.Utc), "PayPal", "Completed", "TXN-108206" },
                    { 23, 269m, 23, new DateTime(2026, 5, 28, 12, 0, 0, 0, DateTimeKind.Utc), "Credit Card", "Completed", "TXN-108579" },
                    { 24, 338m, 24, null, "Credit Card", "Failed", "TXN-108952" },
                    { 25, 493m, 25, new DateTime(2026, 6, 3, 10, 10, 0, 0, DateTimeKind.Utc), "Credit Card", "Completed", "TXN-109325" },
                    { 26, 497m, 26, new DateTime(2026, 6, 6, 14, 40, 0, 0, DateTimeKind.Utc), "Apple Pay", "Completed", "TXN-109698" },
                    { 27, 303m, 27, new DateTime(2026, 6, 9, 11, 25, 0, 0, DateTimeKind.Utc), "PayPal", "Completed", "TXN-110071" },
                    { 28, 295.98m, 28, new DateTime(2026, 6, 12, 16, 0, 0, 0, DateTimeKind.Utc), "Credit Card", "Completed", "TXN-110444" },
                    { 29, 171.5m, 29, new DateTime(2026, 6, 15, 9, 30, 0, 0, DateTimeKind.Utc), "Apple Pay", "Completed", "TXN-110817" },
                    { 30, 272m, 30, new DateTime(2026, 6, 18, 13, 50, 0, 0, DateTimeKind.Utc), "Credit Card", "Completed", "TXN-111190" },
                    { 31, 283m, 31, new DateTime(2026, 6, 21, 17, 15, 0, 0, DateTimeKind.Utc), "PayPal", "Completed", "TXN-111563" },
                    { 32, 272m, 32, new DateTime(2026, 6, 24, 12, 20, 0, 0, DateTimeKind.Utc), "Credit Card", "Completed", "TXN-111936" },
                    { 33, 372.5m, 33, new DateTime(2026, 6, 26, 15, 45, 0, 0, DateTimeKind.Utc), "Apple Pay", "Completed", "TXN-112309" },
                    { 34, 285m, 34, new DateTime(2026, 6, 28, 10, 0, 0, 0, DateTimeKind.Utc), "Credit Card", "Completed", "TXN-112682" },
                    { 35, 272m, 35, new DateTime(2026, 6, 29, 18, 30, 0, 0, DateTimeKind.Utc), "PayPal", "Completed", "TXN-113055" },
                    { 36, 189.99m, 36, new DateTime(2026, 6, 30, 14, 15, 0, 0, DateTimeKind.Utc), "Credit Card", "Completed", "TXN-113428" },
                    { 37, 676m, 37, new DateTime(2026, 7, 2, 11, 20, 0, 0, DateTimeKind.Utc), "Credit Card", "Completed", "TXN-113801" },
                    { 38, 600m, 38, new DateTime(2026, 7, 5, 15, 0, 0, 0, DateTimeKind.Utc), "Apple Pay", "Completed", "TXN-114174" },
                    { 39, 382m, 39, new DateTime(2026, 7, 8, 10, 45, 0, 0, DateTimeKind.Utc), "PayPal", "Completed", "TXN-114547" },
                    { 40, 375.97m, 40, new DateTime(2026, 7, 11, 16, 30, 0, 0, DateTimeKind.Utc), "Credit Card", "Completed", "TXN-114920" },
                    { 41, 290m, 41, new DateTime(2026, 7, 14, 9, 15, 0, 0, DateTimeKind.Utc), "Apple Pay", "Completed", "TXN-115293" },
                    { 42, 270m, 42, new DateTime(2026, 7, 17, 13, 40, 0, 0, DateTimeKind.Utc), "Credit Card", "Completed", "TXN-115666" },
                    { 43, 437m, 43, new DateTime(2026, 7, 19, 17, 50, 0, 0, DateTimeKind.Utc), "PayPal", "Completed", "TXN-116039" },
                    { 44, 371m, 44, new DateTime(2026, 7, 22, 12, 0, 0, 0, DateTimeKind.Utc), "Credit Card", "Completed", "TXN-116412" },
                    { 45, 358m, 45, new DateTime(2026, 7, 24, 15, 25, 0, 0, DateTimeKind.Utc), "Apple Pay", "Completed", "TXN-116785" },
                    { 46, 197m, 46, new DateTime(2026, 7, 26, 10, 10, 0, 0, DateTimeKind.Utc), "Credit Card", "Completed", "TXN-117158" },
                    { 47, 252.99m, 47, new DateTime(2026, 7, 28, 18, 0, 0, 0, DateTimeKind.Utc), "PayPal", "Completed", "TXN-117531" },
                    { 48, 244m, 48, new DateTime(2026, 7, 29, 14, 30, 0, 0, DateTimeKind.Utc), "Credit Card", "Completed", "TXN-117904" },
                    { 49, 229m, 49, new DateTime(2026, 7, 30, 9, 40, 0, 0, DateTimeKind.Utc), "Apple Pay", "Completed", "TXN-118277" },
                    { 50, 345m, 50, new DateTime(2026, 7, 31, 16, 15, 0, 0, DateTimeKind.Utc), "Credit Card", "Completed", "TXN-118650" },
                    { 51, 708m, 51, new DateTime(2026, 8, 2, 10, 0, 0, 0, DateTimeKind.Utc), "Credit Card", "Completed", "TXN-119023" },
                    { 52, 657m, 52, new DateTime(2026, 8, 4, 14, 15, 0, 0, DateTimeKind.Utc), "Apple Pay", "Completed", "TXN-119396" },
                    { 53, 409.98m, 53, new DateTime(2026, 8, 7, 11, 30, 0, 0, DateTimeKind.Utc), "PayPal", "Completed", "TXN-119769" },
                    { 54, 348m, 54, new DateTime(2026, 8, 9, 16, 50, 0, 0, DateTimeKind.Utc), "Credit Card", "Completed", "TXN-120142" },
                    { 55, 256m, 55, new DateTime(2026, 8, 11, 9, 20, 0, 0, DateTimeKind.Utc), "Apple Pay", "Completed", "TXN-120515" },
                    { 56, 473m, 56, new DateTime(2026, 8, 13, 13, 45, 0, 0, DateTimeKind.Utc), "Credit Card", "Completed", "TXN-120888" },
                    { 57, 512m, 57, new DateTime(2026, 8, 15, 17, 10, 0, 0, DateTimeKind.Utc), "PayPal", "Completed", "TXN-121261" },
                    { 58, 303m, 58, new DateTime(2026, 8, 17, 12, 0, 0, 0, DateTimeKind.Utc), "Credit Card", "Completed", "TXN-121634" },
                    { 59, 244m, 59, new DateTime(2026, 8, 18, 15, 30, 0, 0, DateTimeKind.Utc), "Apple Pay", "Completed", "TXN-122007" },
                    { 60, 350m, 60, new DateTime(2026, 8, 20, 10, 15, 0, 0, DateTimeKind.Utc), "Credit Card", "Completed", "TXN-122380" },
                    { 61, 249.48m, 61, new DateTime(2026, 8, 21, 18, 0, 0, 0, DateTimeKind.Utc), "PayPal", "Completed", "TXN-122753" },
                    { 62, 348m, 62, new DateTime(2026, 8, 22, 14, 20, 0, 0, DateTimeKind.Utc), "Credit Card", "Completed", "TXN-123126" },
                    { 63, 228m, 63, null, "Cash on Delivery", "Pending", "TXN-123499" },
                    { 64, 146m, 64, new DateTime(2026, 8, 24, 16, 40, 0, 0, DateTimeKind.Utc), "Credit Card", "Completed", "TXN-123872" },
                    { 65, 254m, 65, new DateTime(2026, 8, 25, 9, 30, 0, 0, DateTimeKind.Utc), "Apple Pay", "Completed", "TXN-124245" },
                    { 66, 162m, 66, new DateTime(2026, 8, 25, 15, 0, 0, 0, DateTimeKind.Utc), "PayPal", "Completed", "TXN-124618" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_UserId",
                table: "Addresses",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_CartId_ProductId",
                table: "CartItems",
                columns: new[] { "CartId", "ProductId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_ProductId",
                table: "CartItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_UserId",
                table: "Carts",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Name",
                table: "Categories",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ProductId",
                table: "OrderItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_AddressId",
                table: "Orders",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_OrderId",
                table: "Payments",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_ProductId",
                table: "ProductImages",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_Name",
                table: "Products",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ProductId",
                table: "Reviews",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_UserId",
                table: "Reviews",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Testimonials_UserId",
                table: "Testimonials",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_WishlistItems_ProductId",
                table: "WishlistItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_WishlistItems_WishlistId_ProductId",
                table: "WishlistItems",
                columns: new[] { "WishlistId", "ProductId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Wishlists_UserId",
                table: "Wishlists",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "CartItems");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "ProductImages");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "Testimonials");

            migrationBuilder.DropTable(
                name: "WishlistItems");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Carts");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Wishlists");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
