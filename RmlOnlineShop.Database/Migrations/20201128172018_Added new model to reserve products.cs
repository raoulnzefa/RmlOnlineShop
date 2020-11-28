using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RmlOnlineShop.Database.Migrations
{
    public partial class Addednewmodeltoreserveproducts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "stocksReservedOnOrder",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StockId = table.Column<int>(nullable: false),
                    QuantitySaved = table.Column<int>(nullable: false),
                    HoldUntillDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_stocksReservedOnOrder", x => x.Id);
                    table.ForeignKey(
                        name: "FK_stocksReservedOnOrder_Stocks_StockId",
                        column: x => x.StockId,
                        principalTable: "Stocks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_stocksReservedOnOrder_StockId",
                table: "stocksReservedOnOrder",
                column: "StockId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "stocksReservedOnOrder");
        }
    }
}
