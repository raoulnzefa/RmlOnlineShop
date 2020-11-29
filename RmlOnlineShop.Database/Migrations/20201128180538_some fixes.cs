using Microsoft.EntityFrameworkCore.Migrations;

namespace RmlOnlineShop.Database.Migrations
{
    public partial class somefixes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SessionId",
                table: "stocksReservedOnOrder",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SessionId",
                table: "stocksReservedOnOrder");
        }
    }
}
