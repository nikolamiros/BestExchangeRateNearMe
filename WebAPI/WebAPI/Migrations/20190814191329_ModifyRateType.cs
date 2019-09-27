using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAPI.Migrations
{
    public partial class ModifyRateType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ExchangeRates",
                table: "ExchangeRates");

            migrationBuilder.DropColumn(
                name: "RateType",
                table: "ExchangeRates");

            migrationBuilder.RenameColumn(
                name: "Rate",
                table: "ExchangeRates",
                newName: "SellRate");

            migrationBuilder.AddColumn<decimal>(
                name: "BuyRate",
                table: "ExchangeRates",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExchangeRates",
                table: "ExchangeRates",
                columns: new[] { "ExchangeOfficerId", "Currency" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ExchangeRates",
                table: "ExchangeRates");

            migrationBuilder.DropColumn(
                name: "BuyRate",
                table: "ExchangeRates");

            migrationBuilder.RenameColumn(
                name: "SellRate",
                table: "ExchangeRates",
                newName: "Rate");

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
    }
}
