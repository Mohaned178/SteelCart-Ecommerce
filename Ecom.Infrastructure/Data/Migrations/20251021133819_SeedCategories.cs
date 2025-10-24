using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Ecom.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "Name" },
                values: new object[] { "All kinds of GPU products", "Graphics Cards" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "Name", "NewPrice", "OldPrice" },
                values: new object[] { "High-end gaming GPU", "NVIDIA RTX 4090", 2000m, 2200m });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "Name", "NewPrice", "OldPrice", "rating" },
                values: new object[,]
                {
                    { 2, 1, "Powerful AMD GPU", "AMD Radeon RX 7900 XT", 1000m, 1200m, 0.0 },
                    { 3, 1, "Mid-range gaming GPU", "NVIDIA RTX 4070", 600m, 700m, 0.0 },
                    { 4, 1, "Budget-friendly GPU", "AMD Radeon RX 7600", 300m, 350m, 0.0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "Name" },
                values: new object[] { "test", "test" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "Name", "NewPrice", "OldPrice" },
                values: new object[] { "test", "test", 12m, 0m });
        }
    }
}
