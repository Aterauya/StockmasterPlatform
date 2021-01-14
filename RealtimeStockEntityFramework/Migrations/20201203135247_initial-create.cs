using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RealtimeStockEntityFramework.Migrations
{
    public partial class initialcreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RealtimeStock",
                columns: table => new
                {
                    RealtimeStockId = table.Column<Guid>(nullable: false),
                    Price = table.Column<decimal>(type: "money", nullable: false),
                    StockSymbol = table.Column<string>(maxLength: 30, nullable: false),
                    DateTimeTraded = table.Column<DateTime>(type: "datetime", nullable: false),
                    Volume = table.Column<decimal>(type: "decimal(20, 10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RealtimeStock", x => x.RealtimeStockId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RealtimeStock");
        }
    }
}
