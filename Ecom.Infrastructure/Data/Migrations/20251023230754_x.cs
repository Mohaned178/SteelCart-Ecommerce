using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecom.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class x : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "Id", "AppUserId", "City", "FirstName", "LastName", "State", "Street", "ZipCode" },
                values: new object[] { 1, "29d9dcca-7ccb-4295-acf2-6f0d1f6ae281", "Manchester", "Erling", "Haaland", "England", "Etihad Stadium Road", "M11 3FF" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
