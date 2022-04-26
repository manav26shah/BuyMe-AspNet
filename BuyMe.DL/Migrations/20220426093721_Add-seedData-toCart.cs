using Microsoft.EntityFrameworkCore.Migrations;

namespace BuyMe.DL.Migrations
{
    public partial class AddseedDatatoCart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Carts",
                columns: new[] { "Id", "Email", "ProductId" },
                values: new object[] { 2, "poi@example.com", 2 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Carts",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
