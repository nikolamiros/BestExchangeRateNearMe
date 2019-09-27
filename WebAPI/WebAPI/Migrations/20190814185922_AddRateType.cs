using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAPI.Migrations
{
    public partial class AddRateType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ExchangeRates",
                table: "ExchangeRates");

            migrationBuilder.AddColumn<int>(
                name: "RateType",
                table: "ExchangeRates",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExchangeRates",
                table: "ExchangeRates",
                columns: new[] { "ExchangeOfficerId", "Currency", "RateType" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ExchangeRates",
                table: "ExchangeRates");

            migrationBuilder.DropColumn(
                name: "RateType",
                table: "ExchangeRates");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExchangeRates",
                table: "ExchangeRates",
                columns: new[] { "ExchangeOfficerId", "Currency" });
        }
    }
}
