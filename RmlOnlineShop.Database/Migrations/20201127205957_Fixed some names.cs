using Microsoft.EntityFrameworkCore.Migrations;

namespace RmlOnlineShop.Database.Migrations
{
    public partial class Fixedsomenames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderUrl",
                table: "Orders");

            migrationBuilder.AddColumn<string>(
                name: "OrderUniqueId",
                table: "Orders",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderUniqueId",
                table: "Orders");

            migrationBuilder.AddColumn<string>(
                name: "OrderUrl",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
