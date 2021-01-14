using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CompaniesEntityFramework.Migrations
{
    public partial class RemovedUnneededModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "CompanyCountry_CompanyInformation",
                table: "CompanyInformation");

            migrationBuilder.DropForeignKey(
                name: "CompanyInformation_CompanyCurrency",
                table: "CompanyInformation");

            migrationBuilder.DropForeignKey(
                name: "CompanyIndustry_CompanyInformation",
                table: "CompanyInformation");

            migrationBuilder.DropTable(
                name: "CompanyCountry");

            migrationBuilder.DropTable(
                name: "CompanyCurrency");

            migrationBuilder.DropTable(
                name: "CompanyIndustry");

            migrationBuilder.DropIndex(
                name: "IX_CompanyInformation_CountryId",
                table: "CompanyInformation");

            migrationBuilder.DropIndex(
                name: "IX_CompanyInformation_CurrencyId",
                table: "CompanyInformation");

            migrationBuilder.DropIndex(
                name: "IX_CompanyInformation_IndustryId",
                table: "CompanyInformation");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "CompanyInformation");

            migrationBuilder.DropColumn(
                name: "CurrencyId",
                table: "CompanyInformation");

            migrationBuilder.DropColumn(
                name: "IndustryId",
                table: "CompanyInformation");

            migrationBuilder.AddColumn<string>(
                name: "CountryName",
                table: "CompanyInformation",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CurrencyName",
                table: "CompanyInformation",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CurrencySymbol",
                table: "CompanyInformation",
                maxLength: 5,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "IndustryName",
                table: "CompanyInformation",
                maxLength: 200,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CountryName",
                table: "CompanyInformation");

            migrationBuilder.DropColumn(
                name: "CurrencyName",
                table: "CompanyInformation");

            migrationBuilder.DropColumn(
                name: "CurrencySymbol",
                table: "CompanyInformation");

            migrationBuilder.DropColumn(
                name: "IndustryName",
                table: "CompanyInformation");

            migrationBuilder.AddColumn<Guid>(
                name: "CountryId",
                table: "CompanyInformation",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CurrencyId",
                table: "CompanyInformation",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "IndustryId",
                table: "CompanyInformation",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "CompanyCountry",
                columns: table => new
                {
                    CountryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CountryName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("CompanyCountry_pk", x => x.CountryId);
                });

            migrationBuilder.CreateTable(
                name: "CompanyCurrency",
                columns: table => new
                {
                    CurrencyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CurrencyName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CurrencySymbol = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("CompanyCurrency_pk", x => x.CurrencyId);
                });

            migrationBuilder.CreateTable(
                name: "CompanyIndustry",
                columns: table => new
                {
                    IndustryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IndustryName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("CompanyIndustry_pk", x => x.IndustryId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompanyInformation_CountryId",
                table: "CompanyInformation",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyInformation_CurrencyId",
                table: "CompanyInformation",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyInformation_IndustryId",
                table: "CompanyInformation",
                column: "IndustryId");

            migrationBuilder.AddForeignKey(
                name: "CompanyCountry_CompanyInformation",
                table: "CompanyInformation",
                column: "CountryId",
                principalTable: "CompanyCountry",
                principalColumn: "CountryId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "CompanyInformation_CompanyCurrency",
                table: "CompanyInformation",
                column: "CurrencyId",
                principalTable: "CompanyCurrency",
                principalColumn: "CurrencyId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "CompanyIndustry_CompanyInformation",
                table: "CompanyInformation",
                column: "IndustryId",
                principalTable: "CompanyIndustry",
                principalColumn: "IndustryId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
