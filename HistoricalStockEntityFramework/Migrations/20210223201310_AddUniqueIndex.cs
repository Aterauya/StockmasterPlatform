using Microsoft.EntityFrameworkCore.Migrations;

namespace HistoricalStockEntityFramework.Migrations
{
    public partial class AddUniqueIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_HistoricalStocks_FilterHash",
                table: "HistoricalStocks",
                column: "FilterHash",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_HistoricalStocks_FilterHash",
                table: "HistoricalStocks");
        }
    }
}
