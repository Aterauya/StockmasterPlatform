using Microsoft.EntityFrameworkCore.Migrations;

namespace HistoricalStockEntityFramework.Migrations
{
    public partial class AddFilterHash : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FilterHash",
                table: "HistoricalStocks",
                unicode: false,
                maxLength: 30,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FilterHash",
                table: "HistoricalStocks");
        }
    }
}
