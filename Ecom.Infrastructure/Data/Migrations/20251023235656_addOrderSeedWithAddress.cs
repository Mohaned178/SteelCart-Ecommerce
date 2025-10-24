using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Ecom.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addOrderSeedWithAddress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ProductName",
                table: "OrderItems",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "MainImage",
                table: "OrderItems",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 2, "Gaming and professional laptops", "Laptops" },
                    { 3, "Audio devices and gaming headsets", "Headphones" },
                    { 4, "High-resolution and gaming monitors", "Monitors" },
                    { 5, "Tools and gear for fitness and sports", "Sports Equipment" }
                });

            migrationBuilder.UpdateData(
                table: "DeliveryMethods",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DeliveryTime", "Description", "Name" },
                values: new object[] { "3-5 business days", "Fast international shipping with tracking and insurance.", "DHL Express" });

            migrationBuilder.UpdateData(
                table: "DeliveryMethods",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DeliveryTime", "Description", "Name", "Price" },
                values: new object[] { "2-4 business days", "Reliable worldwide delivery with door-to-door service.", "FedEx Priority", 18.50m });

            migrationBuilder.InsertData(
                table: "DeliveryMethods",
                columns: new[] { "Id", "DeliveryTime", "Description", "Name", "Price" },
                values: new object[,]
                {
                    { 3, "5-7 business days", "Affordable local delivery for domestic orders.", "UPS Ground", 10.00m },
                    { 4, "4-6 business days", "Trusted shipping in the Middle East and Europe.", "Aramex Standard", 12.75m }
                });

            migrationBuilder.InsertData(
                table: "OrderItems",
                columns: new[] { "Id", "MainImage", "OrdersId", "Price", "ProductItemId", "ProductName", "Quantity" },
                values: new object[,]
                {
                    { 1, "https://example.com/images/rtx4090.jpg", null, 2000m, 1, "NVIDIA RTX 4090", 1 },
                    { 2, "https://example.com/images/rx7900xt.jpg", null, 1000m, 2, "AMD Radeon RX 7900 XT", 2 },
                    { 3, "https://example.com/images/football.jpg", null, 90m, 5, "Adidas Official Match Football", 1 },
                    { 4, "https://example.com/images/shoes.jpg", null, 150m, 6, "Nike Zoom Running Shoes", 1 }
                });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "Description",
                value: "High-end gaming GPU with 24GB VRAM");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "Description",
                value: "Top-tier AMD GPU for 4K gaming");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Description", "Name", "NewPrice", "OldPrice" },
                values: new object[] { "Mid-range GPU offering great performance for 1440p gaming", "NVIDIA RTX 4070 Ti", 799m, 899m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CategoryId", "Description", "Name", "NewPrice", "OldPrice" },
                values: new object[] { 2, "Compact gaming laptop with Ryzen 9 and RTX 4070", "ASUS ROG Zephyrus G14", 1800m, 2000m });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "Name", "NewPrice", "OldPrice", "rating" },
                values: new object[,]
                {
                    { 5, 2, "Apple’s professional laptop with M3 chip", "MacBook Pro M3", 2500m, 2700m, 0.0 },
                    { 6, 2, "Premium ultrabook for creators and professionals", "Dell XPS 15", 1900m, 2100m, 0.0 },
                    { 7, 3, "Noise-cancelling wireless headphones", "Sony WH-1000XM5", 350m, 400m, 0.0 },
                    { 8, 3, "Gaming headset with dual chamber drivers", "HyperX Cloud Alpha", 120m, 150m, 0.0 },
                    { 9, 3, "Wireless earbuds with active noise cancellation", "Apple AirPods Pro 2", 250m, 280m, 0.0 },
                    { 10, 4, "Ultra-wide curved gaming monitor 49-inch", "Samsung Odyssey G9", 1400m, 1600m, 0.0 },
                    { 11, 4, "4K 144Hz gaming monitor with HDR support", "LG Ultragear 27GP950", 800m, 950m, 0.0 },
                    { 12, 4, "27-inch 4K productivity monitor with USB-C", "Dell UltraSharp U2723QE", 650m, 700m, 0.0 },
                    { 13, 5, "Lightweight running shoes for all distances", "Nike Air Zoom Pegasus 40", 130m, 160m, 0.0 },
                    { 14, 5, "Professional football boots for firm ground", "Adidas Predator Accuracy", 150m, 180m, 0.0 },
                    { 15, 5, "Official indoor basketball for training and matches", "Wilson Evolution Basketball", 60m, 75m, 0.0 },
                    { 16, 5, "High-end badminton racket used by pros", "Yonex Astrox 100ZZ", 220m, 260m, 0.0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DeliveryMethods",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "DeliveryMethods",
                keyColumn: "Id",
                keyValue: 4);

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
                table: "Products",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 16);

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

            migrationBuilder.AlterColumn<string>(
                name: "ProductName",
                table: "OrderItems",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "MainImage",
                table: "OrderItems",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.UpdateData(
                table: "DeliveryMethods",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DeliveryTime", "Description", "Name" },
                values: new object[] { "Only a week", "The fast Delivery in the world", "DHL" });

            migrationBuilder.UpdateData(
                table: "DeliveryMethods",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DeliveryTime", "Description", "Name", "Price" },
                values: new object[] { "Only take two week", "Make your product save", "XXX", 12m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "Description",
                value: "High-end gaming GPU");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "Description",
                value: "Powerful AMD GPU");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Description", "Name", "NewPrice", "OldPrice" },
                values: new object[] { "Mid-range gaming GPU", "NVIDIA RTX 4070", 600m, 700m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CategoryId", "Description", "Name", "NewPrice", "OldPrice" },
                values: new object[] { 1, "Budget-friendly GPU", "AMD Radeon RX 7600", 300m, 350m });
        }
    }
}
