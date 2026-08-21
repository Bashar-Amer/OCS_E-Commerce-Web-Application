using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CampTravelGear.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserSeedData1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ApprovedAt", "Rating", "Status" },
                values: new object[] { null, 4, "Pending" });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ApprovedAt", "Comment", "Rating", "Status" },
                values: new object[] { null, "Arrived with a slight scratch on the outer lens cap.", 2, "Rejected" });

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "Id", "ApprovedAt", "Comment", "CreatedAt", "ProductId", "Rating", "Status", "UserId" },
                values: new object[] { 4, null, "Super warm down sleeping bag for sub-zero mountain nights. Highly recommended!", new DateTime(2026, 8, 20, 9, 30, 0, 0, DateTimeKind.Utc), 14, 5, "Pending", "user-cust-001" });

            migrationBuilder.UpdateData(
                table: "Testimonials",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Content", "Name" },
                values: new object[] { "We've seen amazing results already. Barrameru should be nominated for outdoor brand of the year. Great customer service!", "Sara G. Helvey" });

            migrationBuilder.UpdateData(
                table: "Testimonials",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ApprovedAt", "Status" },
                values: new object[] { null, "Pending" });

            migrationBuilder.InsertData(
                table: "Testimonials",
                columns: new[] { "Id", "ApprovedAt", "Content", "CreatedAt", "Name", "Status", "UserId" },
                values: new object[] { 3, null, "Spam test message or irrelevant feedback.", new DateTime(2026, 7, 28, 16, 0, 0, 0, DateTimeKind.Utc), "Alex Hunter", "Rejected", "user-cust-003" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Testimonials",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ApprovedAt", "Rating", "Status" },
                values: new object[] { new DateTime(2026, 8, 15, 15, 0, 0, 0, DateTimeKind.Utc), 5, "Accepted" });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ApprovedAt", "Comment", "Rating", "Status" },
                values: new object[] { new DateTime(2026, 8, 16, 13, 0, 0, 0, DateTimeKind.Utc), "Great optics and clear focus for wildlife watching.", 4, "Accepted" });

            migrationBuilder.UpdateData(
                table: "Testimonials",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Content", "Name" },
                values: new object[] { "We've seen amazing results already. I wish I would have thought of it first. Barrameru should be nominated for service of the year. Great Job!", "Sara C. Helvey" });

            migrationBuilder.UpdateData(
                table: "Testimonials",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ApprovedAt", "Status" },
                values: new object[] { new DateTime(2026, 7, 26, 14, 0, 0, 0, DateTimeKind.Utc), "Accepted" });
        }
    }
}
