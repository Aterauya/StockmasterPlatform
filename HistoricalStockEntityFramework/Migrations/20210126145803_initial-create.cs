using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HistoricalStockEntityFramework.Migrations
{
    public partial class initialcreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HistoricalStocks",
                columns: table => new
                {
                    HistoricalStockId = table.Column<Guid>(nullable: false),
                    StockSymbol = table.Column<string>(unicode: false, maxLength: 10, nullable: false),
                    OpeningPrice = table.Column<decimal>(type: "money", nullable: false),
                    HighPrice = table.Column<decimal>(type: "money", nullable: false),
                    LowPrice = table.Column<decimal>(type: "money", nullable: false),
                    ClosePrice = table.Column<decimal>(type: "money", nullable: false),
                    Volume = table.Column<decimal>(type: "decimal(20, 10)", nullable: false),
                    ClosingDateTime = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("HistoricalStock_pk", x => x.HistoricalStockId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HistoricalStocks");
        }
    }
}
