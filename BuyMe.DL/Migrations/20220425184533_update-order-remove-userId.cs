using Microsoft.EntityFrameworkCore.Migrations;

namespace BuyMe.DL.Migrations
{
    public partial class updateorderremoveuserId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Orders");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                column: "UserId",
                value: "ceaaac8a-8158-43a8-877b-3c92cb203f44");

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2,
                column: "UserId",
                value: "65135530-46dd-4ee1-a88d-80dc397191ed");
        }
    }
}
