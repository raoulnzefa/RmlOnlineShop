using Microsoft.EntityFrameworkCore.Migrations;

namespace RmlOnlineShop.Database.Migrations
{
    public partial class AddednewPropertiesfortheorder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ClientId",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmailCustomer",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstNameCustomer",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastNameCustomer",
                table: "Orders",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "EmailCustomer",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "FirstNameCustomer",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "LastNameCustomer",
                table: "Orders");
        }
    }
}
