using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CompaniesEntityFramework.Migrations
{
    public partial class initialcreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CompanyCountry",
                columns: table => new
                {
                    CountryId = table.Column<Guid>(nullable: false),
                    CountryName = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("CompanyCountry_pk", x => x.CountryId);
                });

            migrationBuilder.CreateTable(
                name: "CompanyCurrency",
                columns: table => new
                {
                    CurrencyId = table.Column<Guid>(nullable: false),
                    CurrencyName = table.Column<string>(maxLength: 100, nullable: false),
                    CurrencySymbol = table.Column<string>(maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("CompanyCurrency_pk", x => x.CurrencyId);
                });

            migrationBuilder.CreateTable(
                name: "CompanyIndustry",
                columns: table => new
                {
                    IndustryId = table.Column<Guid>(nullable: false),
                    IndustryName = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("CompanyIndustry_pk", x => x.IndustryId);
                });

            migrationBuilder.CreateTable(
                name: "CompanyInformation",
                columns: table => new
                {
                    CompanyId = table.Column<Guid>(nullable: false),
                    CountryId = table.Column<Guid>(nullable: false),
                    CurrencyId = table.Column<Guid>(nullable: false),
                    IndustryId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Exchange = table.Column<string>(maxLength: 150, nullable: false),
                    Ipo = table.Column<DateTime>(type: "date", nullable: false),
                    MarketCapitalization = table.Column<decimal>(type: "money", nullable: false),
                    OutstandingShares = table.Column<float>(nullable: false),
                    Url = table.Column<string>(maxLength: 200, nullable: false),
                    Logo = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("CompanyInformation_pk", x => x.CompanyId);
                    table.ForeignKey(
                        name: "CompanyCountry_CompanyInformation",
                        column: x => x.CountryId,
                        principalTable: "CompanyCountry",
                        principalColumn: "CountryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "CompanyInformation_CompanyCurrency",
                        column: x => x.IndustryId,
                        principalTable: "CompanyCurrency",
                        principalColumn: "CurrencyId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "CompanyIndustry_CompanyInformation",
                        column: x => x.IndustryId,
                        principalTable: "CompanyIndustry",
                        principalColumn: "IndustryId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompanyInformation_CountryId",
                table: "CompanyInformation",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyInformation_IndustryId",
                table: "CompanyInformation",
                column: "IndustryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompanyInformation");

            migrationBuilder.DropTable(
                name: "CompanyCountry");

            migrationBuilder.DropTable(
                name: "CompanyCurrency");

            migrationBuilder.DropTable(
                name: "CompanyIndustry");
        }
    }
}
