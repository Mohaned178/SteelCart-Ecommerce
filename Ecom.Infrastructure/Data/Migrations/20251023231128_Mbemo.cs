using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Ecom.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Mbemo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "Id", "AppUserId", "City", "FirstName", "LastName", "State", "Street", "ZipCode" },
                values: new object[,]
                {
                    { 2, "00f772b6-25aa-4385-a3ee-99f60c0e1006", "Liverpool", "Mohamed", "Salah", "England", "Anfield Road", "L1 4BX" },
                    { 3, "f824594d-9cab-433f-b93a-6b80d14c6ca3", "Madrid", "Luka", "Modric", "Spain", "Santiago Bernabeu Ave", "28022" },
                    { 4, "a6f90d83-bdff-48cd-914d-234d9a4dcde8", "Barcelona", "Robert", "Lewandowski", "Spain", "Camp Nou Street", "08028" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
