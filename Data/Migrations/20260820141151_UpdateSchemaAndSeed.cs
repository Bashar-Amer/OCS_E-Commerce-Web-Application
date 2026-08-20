using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CampTravelGear.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSchemaAndSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Wishlists_UserId",
                table: "Wishlists");

            migrationBuilder.DropIndex(
                name: "IX_WishlistItems_WishlistId",
                table: "WishlistItems");

            migrationBuilder.DropIndex(
                name: "IX_Carts_UserId",
                table: "Carts");

            migrationBuilder.DropIndex(
                name: "IX_CartItems_CartId",
                table: "CartItems");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Testimonials",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Payments",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<string>(
                name: "PaymentMethod",
                table: "Payments",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Orders",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

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
                    { "user-admin-001", 0, "con-stamp-admin-001", "admin@barrameru.com", true, "Admin Administrator", false, null, "ADMIN@BARRAMERU.COM", "ADMIN@BARRAMERU.COM", "AQAAAAIAAYagAAAAEEdaqL9mVLSYIYDQnf3VbN26q7GMI+7eZ1KJyWghZois4i24uitPbuHClLflhAdeGA==", null, false, "sec-stamp-admin-001", false, "admin@barrameru.com" },
                    { "user-cust-001", 0, "con-stamp-john-001", "john.doe@example.com", true, "John Doe", false, null, "JOHN.DOE@EXAMPLE.COM", "JOHN.DOE@EXAMPLE.COM", "AQAAAAIAAYagAAAAEPvH9DFxtV1m7CiKbFG7DczGEaLFQzYgi5Ckg0OVEzpmEsDORBDjImwoH6IuqeZohA==", null, false, "sec-stamp-john-001", false, "john.doe@example.com" },
                    { "user-cust-002", 0, "con-stamp-sara-002", "sara.helvey@example.com", true, "Sara G. Helvey", false, null, "SARA.HELVEY@EXAMPLE.COM", "SARA.HELVEY@EXAMPLE.COM", "AQAAAAIAAYagAAAAEPvH9DFxtV1m7CiKbFG7DczGEaLFQzYgi5Ckg0OVEzpmEsDORBDjImwoH6IuqeZohA==", null, false, "sec-stamp-sara-002", false, "sara.helvey@example.com" },
                    { "user-cust-003", 0, "con-stamp-alex-003", "alex.h@example.com", true, "Alex Hunter", false, null, "ALEX.H@EXAMPLE.COM", "ALEX.H@EXAMPLE.COM", "AQAAAAIAAYagAAAAEPvH9DFxtV1m7CiKbFG7DczGEaLFQzYgi5Ckg0OVEzpmEsDORBDjImwoH6IuqeZohA==", null, false, "sec-stamp-alex-003", false, "alex.h@example.com" }
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
                    { 1, "Denver", "124 Mountain Trail Rd, Apt 4B", "user-cust-001" },
                    { 2, "Seattle", "582 Forest Pine Way", "user-cust-002" },
                    { 3, "Portland", "910 Alpine Ridge Ave", "user-cust-003" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "role-admin-001", "user-admin-001" },
                    { "role-customer-002", "user-cust-001" },
                    { "role-customer-002", "user-cust-002" },
                    { "role-customer-002", "user-cust-003" }
                });

            migrationBuilder.InsertData(
                table: "Carts",
                columns: new[] { "Id", "UserId" },
                values: new object[] { 1, "user-cust-001" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "CreatedAt", "Description", "IsActive", "IsDeleted", "Name", "Price", "Stock", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, 2, new DateTime(2026, 1, 15, 8, 0, 0, 0, DateTimeKind.Utc), "Compact aluminum lightweight folding camp stool for hiking trails and campsite relaxation.", true, false, "Black Folding Stool", 45.00m, 25, null },
                    { 2, 2, new DateTime(2026, 1, 15, 8, 0, 0, 0, DateTimeKind.Utc), "Double-layer waterproof 3-person dome camping tent with high-strength fiberglass poles and rainfly.", true, false, "Yellow 3-Person Tent", 65.00m, 18, null },
                    { 3, 2, new DateTime(2026, 1, 15, 8, 0, 0, 0, DateTimeKind.Utc), "Comfortable self-inflating sleeping pad with integrated air pillow and moisture barrier.", true, false, "Inflatable Camping Bed", 35.00m, 30, null },
                    { 4, 2, new DateTime(2026, 1, 15, 8, 0, 0, 0, DateTimeKind.Utc), "Closed-cell foam moisture-proof ground sleeping pad for rugged outdoor adventures.", true, false, "Orange Ground Camping Mat", 20.00m, 40, null },
                    { 5, 4, new DateTime(2026, 1, 15, 8, 0, 0, 0, DateTimeKind.Utc), "High-grade stainless steel multi-tool tactical folding pocket knife with secure locking mechanism.", true, false, "Multi-Function Folding Knife", 25.00m, 50, null },
                    { 6, 4, new DateTime(2026, 1, 15, 8, 0, 0, 0, DateTimeKind.Utc), "1000-lumen ultra-bright rechargeable waterproof tactical torch with zoomable beam.", true, false, "Tactical LED Flashlight", 50.00m, 35, null },
                    { 7, 4, new DateTime(2026, 1, 15, 8, 0, 0, 0, DateTimeKind.Utc), "High-definition compact roof prism binoculars with multi-coated anti-reflective lenses.", true, false, "Black HD Binoculars 10x42", 65.00m, 20, null },
                    { 8, 4, new DateTime(2026, 1, 15, 8, 0, 0, 0, DateTimeKind.Utc), "Heavy-duty metal sighting navigation compass with fluorescent dial and clinometer.", true, false, "Military Navigation Compass", 50.00m, 45, null },
                    { 9, 1, new DateTime(2026, 1, 15, 8, 0, 0, 0, DateTimeKind.Utc), "Heavy-duty ergonomic internal frame mountain hiking backpack with rain cover and hydration port.", true, false, "Trekking Expedition Backpack 65L", 150.00m, 15, null },
                    { 10, 5, new DateTime(2026, 1, 15, 8, 0, 0, 0, DateTimeKind.Utc), "Waterproof full-grain leather hiking boots with high-traction Vibram rubber outsoles.", true, false, "Brown Ankle-High Hiking Boots", 125.00m, 22, null },
                    { 11, 3, new DateTime(2026, 1, 15, 8, 0, 0, 0, DateTimeKind.Utc), "Double-wall vacuum insulated stainless steel canteen keeping drinks hot for 12h or cold for 24h.", true, false, "Insulated Metal Water Bottle 1L", 60.00m, 60, null },
                    { 12, 3, new DateTime(2026, 1, 15, 8, 0, 0, 0, DateTimeKind.Utc), "Foldable stainless steel tabletop barbecue grill for backcountry cooking and tailgating.", true, false, "Portable Camp Charcoal Grill", 100.00m, 12, null },
                    { 13, 5, new DateTime(2026, 1, 15, 8, 0, 0, 0, DateTimeKind.Utc), "9.8mm dynamic UIAA certified climbing rope with dry treatment for alpine ascents.", true, false, "Dynamic Climbing Ropes 60m", 80.00m, 25, null },
                    { 14, 1, new DateTime(2026, 1, 15, 8, 0, 0, 0, DateTimeKind.Utc), "Rugged military camo multi-compartment tactical pack with MOLLE webbing and padded harness.", true, false, "Camouflage Tactical Backpack 50L", 140.00m, 18, null }
                });

            migrationBuilder.InsertData(
                table: "Testimonials",
                columns: new[] { "Id", "ApprovedAt", "Content", "CreatedAt", "Name", "Status", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 7, 21, 10, 0, 0, 0, DateTimeKind.Utc), "We've seen amazing results already. I wish I would have thought of it first. Barrameru should be nominated for service of the year. Great Job!", new DateTime(2026, 7, 20, 9, 0, 0, 0, DateTimeKind.Utc), "Sara C. Helvey", "Accepted", "user-cust-002" },
                    { 2, new DateTime(2026, 7, 26, 14, 0, 0, 0, DateTimeKind.Utc), "The best camping gear store I have ever used. Fast delivery and authentic rugged equipment.", new DateTime(2026, 7, 25, 11, 0, 0, 0, DateTimeKind.Utc), "John Doe", "Accepted", "user-cust-001" }
                });

            migrationBuilder.InsertData(
                table: "Wishlists",
                columns: new[] { "Id", "UserId" },
                values: new object[] { 1, "user-cust-001" });

            migrationBuilder.InsertData(
                table: "CartItems",
                columns: new[] { "Id", "CartId", "ProductId", "Quantity", "UnitPrice" },
                values: new object[,]
                {
                    { 1, 1, 7, 1, 65.00m },
                    { 2, 1, 2, 1, 65.00m },
                    { 3, 1, 14, 1, 140.00m }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "AddressId", "CreatedAt", "OrderDate", "Status", "TotalAmount", "UserId" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2026, 8, 10, 14, 30, 0, 0, DateTimeKind.Utc), new DateTime(2026, 8, 10, 14, 30, 0, 0, DateTimeKind.Utc), "Delivered", 270.00m, "user-cust-001" },
                    { 2, 2, new DateTime(2026, 8, 18, 9, 15, 0, 0, DateTimeKind.Utc), new DateTime(2026, 8, 18, 9, 15, 0, 0, DateTimeKind.Utc), "Processing", 185.00m, "user-cust-002" },
                    { 3, 3, new DateTime(2026, 8, 19, 16, 45, 0, 0, DateTimeKind.Utc), new DateTime(2026, 8, 19, 16, 45, 0, 0, DateTimeKind.Utc), "Pending", 85.00m, "user-cust-003" }
                });

            migrationBuilder.InsertData(
                table: "ProductImages",
                columns: new[] { "Id", "ImageUrl", "IsMain", "ProductId" },
                values: new object[,]
                {
                    { 1, "/images/top-view-of-travel-equipment-for-a-mountain-trip-e1664201262219.jpg", true, 1 },
                    { 2, "/images/orange-tourist-tent-illuminated-from-inside-stands-in-mountains-above-clouds.jpg", true, 2 },
                    { 3, "/images/camping-in-steppe.jpg", true, 3 },
                    { 4, "/images/sandy-field-in-desert-of-egypt.jpg", true, 4 },
                    { 5, "/images/43.jpg", true, 5 },
                    { 6, "/images/panoramic-shot-of-two-young-tourists-looking-at-geographic-map-on-grey-background-e1664119398628.jpg", true, 6 },
                    { 7, "/images/high-angle-view-of-confident-couple-climbing-mountain-e1664201089286.jpg", true, 7 },
                    { 8, "/images/tourist-sitting-phu-sub-lek-viewpoint-sunset-lopburi-thailand-e1664089245105.jpg", true, 8 },
                    { 9, "/images/blue-hiking-backpack-with-fitness-mat-isolated-on-2021-09-03-13-40-43-utc-1.jpg", true, 9 },
                    { 10, "/images/tourist-with-a-backpack-against-the-sky-panorama.jpg", true, 10 },
                    { 11, "/images/spending-time-by-the-campfire.jpg", true, 11 },
                    { 12, "/images/paddling-kayaker-on-the-scenic-lake.jpg", true, 12 },
                    { 13, "/images/high-angle-view-of-confident-couple-climbing-mountain-e1664201089286.jpg", true, 13 },
                    { 14, "/images/group-of-friends-with-backpacks-doing-trekking-excursion-on-mountain.jpg", true, 14 }
                });

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "Id", "ApprovedAt", "Comment", "CreatedAt", "ProductId", "Rating", "Status", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 8, 12, 12, 0, 0, 0, DateTimeKind.Utc), "Excellent camping tent, set up took 5 minutes and withstands wind easily.", new DateTime(2026, 8, 12, 10, 0, 0, 0, DateTimeKind.Utc), 2, 5, "Accepted", "user-cust-001" },
                    { 2, new DateTime(2026, 8, 15, 15, 0, 0, 0, DateTimeKind.Utc), "Incredible build quality and very comfortable back padding for multi-day treks.", new DateTime(2026, 8, 15, 14, 0, 0, 0, DateTimeKind.Utc), 9, 5, "Accepted", "user-cust-002" },
                    { 3, new DateTime(2026, 8, 16, 13, 0, 0, 0, DateTimeKind.Utc), "Great optics and clear focus for wildlife watching.", new DateTime(2026, 8, 16, 11, 0, 0, 0, DateTimeKind.Utc), 7, 4, "Accepted", "user-cust-003" }
                });

            migrationBuilder.InsertData(
                table: "WishlistItems",
                columns: new[] { "Id", "AddedAt", "ProductId", "WishlistId" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 8, 1, 10, 0, 0, 0, DateTimeKind.Utc), 2, 1 },
                    { 2, new DateTime(2026, 8, 2, 11, 0, 0, 0, DateTimeKind.Utc), 9, 1 }
                });

            migrationBuilder.InsertData(
                table: "OrderItems",
                columns: new[] { "Id", "OrderId", "ProductId", "Quantity", "UnitPrice" },
                values: new object[,]
                {
                    { 1, 1, 7, 1, 65.00m },
                    { 2, 1, 2, 1, 65.00m },
                    { 3, 1, 14, 1, 140.00m },
                    { 4, 2, 9, 1, 150.00m },
                    { 5, 2, 3, 1, 35.00m },
                    { 6, 3, 11, 1, 60.00m },
                    { 7, 3, 5, 1, 25.00m }
                });

            migrationBuilder.InsertData(
                table: "Payments",
                columns: new[] { "Id", "Amount", "OrderId", "PaidAt", "PaymentMethod", "Status", "TransactionId" },
                values: new object[,]
                {
                    { 1, 270.00m, 1, new DateTime(2026, 8, 10, 14, 32, 0, 0, DateTimeKind.Utc), "Credit Card", "Completed", "TXN-849201" },
                    { 2, 185.00m, 2, new DateTime(2026, 8, 18, 9, 16, 0, 0, DateTimeKind.Utc), "PayPal", "Completed", "TXN-739102" },
                    { 3, 85.00m, 3, null, "Credit Card", "Pending", "TXN-109483" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Wishlists_UserId",
                table: "Wishlists",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WishlistItems_WishlistId_ProductId",
                table: "WishlistItems",
                columns: new[] { "WishlistId", "ProductId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_Name",
                table: "Products",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Name",
                table: "Categories",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Carts_UserId",
                table: "Carts",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_CartId_ProductId",
                table: "CartItems",
                columns: new[] { "CartId", "ProductId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Wishlists_UserId",
                table: "Wishlists");

            migrationBuilder.DropIndex(
                name: "IX_WishlistItems_WishlistId_ProductId",
                table: "WishlistItems");

            migrationBuilder.DropIndex(
                name: "IX_Products_Name",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Categories_Name",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Carts_UserId",
                table: "Carts");

            migrationBuilder.DropIndex(
                name: "IX_CartItems_CartId_ProductId",
                table: "CartItems");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "role-admin-001", "user-admin-001" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "role-customer-002", "user-cust-001" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "role-customer-002", "user-cust-002" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "role-customer-002", "user-cust-003" });

            migrationBuilder.DeleteData(
                table: "CartItems",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "CartItems",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "CartItems",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Testimonials",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Testimonials",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "WishlistItems",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "WishlistItems",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "role-admin-001");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "role-customer-002");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "user-admin-001");

            migrationBuilder.DeleteData(
                table: "Carts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Wishlists",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "user-cust-001");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "user-cust-002");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "user-cust-003");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Testimonials",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Payments",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "PaymentMethod",
                table: "Payments",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.CreateIndex(
                name: "IX_Wishlists_UserId",
                table: "Wishlists",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_WishlistItems_WishlistId",
                table: "WishlistItems",
                column: "WishlistId");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_UserId",
                table: "Carts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_CartId",
                table: "CartItems",
                column: "CartId");
        }
    }
}
