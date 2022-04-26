using Microsoft.EntityFrameworkCore.Migrations;

namespace BuyMe.DL.Migrations
{
    public partial class updateorder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Orders",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                column: "Email",
                value: "qwe@example.com");

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2,
                column: "Email",
                value: "poi@example.com");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Orders");
        }
    }
}
