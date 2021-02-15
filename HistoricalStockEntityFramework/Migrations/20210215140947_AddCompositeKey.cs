using Microsoft.EntityFrameworkCore.Migrations;

namespace HistoricalStockEntityFramework.Migrations
{
    public partial class AddCompositeKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "HistoricalStock_pk",
                table: "HistoricalStocks");

            migrationBuilder.AddPrimaryKey(
                name: "HistoricalStock_pk",
                table: "HistoricalStocks",
                columns: new[] { "HistoricalStockId", "FilterHash" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "HistoricalStock_pk",
                table: "HistoricalStocks");

            migrationBuilder.AddPrimaryKey(
                name: "HistoricalStock_pk",
                table: "HistoricalStocks",
                column: "HistoricalStockId");
        }
    }
}
