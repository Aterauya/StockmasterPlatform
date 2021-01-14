using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CompaniesEntityFramework.Migrations
{
    public partial class addcompanysymboltable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "CompanyInformation_CompanyCurrency",
                table: "CompanyInformation");

            migrationBuilder.AddColumn<Guid>(
                name: "SymbolId",
                table: "CompanyInformation",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "CompanySymbol",
                columns: table => new
                {
                    SymbolId = table.Column<Guid>(nullable: false),
                    Symbol = table.Column<string>(unicode: false, maxLength: 6, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("CompanySymbol_pk", x => x.SymbolId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompanyInformation_CurrencyId",
                table: "CompanyInformation",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyInformation_SymbolId",
                table: "CompanyInformation",
                column: "SymbolId");

            migrationBuilder.AddForeignKey(
                name: "CompanyInformation_CompanyCurrency",
                table: "CompanyInformation",
                column: "CurrencyId",
                principalTable: "CompanyCurrency",
                principalColumn: "CurrencyId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "CompanySymbol_CompanyInformation",
                table: "CompanyInformation",
                column: "SymbolId",
                principalTable: "CompanySymbol",
                principalColumn: "SymbolId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "CompanyInformation_CompanyCurrency",
                table: "CompanyInformation");

            migrationBuilder.DropForeignKey(
                name: "CompanySymbol_CompanyInformation",
                table: "CompanyInformation");

            migrationBuilder.DropTable(
                name: "CompanySymbol");

            migrationBuilder.DropIndex(
                name: "IX_CompanyInformation_CurrencyId",
                table: "CompanyInformation");

            migrationBuilder.DropIndex(
                name: "IX_CompanyInformation_SymbolId",
                table: "CompanyInformation");

            migrationBuilder.DropColumn(
                name: "SymbolId",
                table: "CompanyInformation");

            migrationBuilder.AddForeignKey(
                name: "CompanyInformation_CompanyCurrency",
                table: "CompanyInformation",
                column: "IndustryId",
                principalTable: "CompanyCurrency",
                principalColumn: "CurrencyId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
