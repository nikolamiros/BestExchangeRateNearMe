using GeoAPI.Geometries;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAPI.Migrations
{
    public partial class NewMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<IPoint>(
                name: "Location",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ExchangeRates",
                columns: table => new
                {
                    ExchangeOfficerId = table.Column<string>(nullable: false),
                    Currency = table.Column<int>(nullable: false),
                    Rate = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExchangeRates", x => new { x.ExchangeOfficerId, x.Currency });
                    table.ForeignKey(
                        name: "FK_ExchangeRates_AspNetUsers_ExchangeOfficerId",
                        column: x => x.ExchangeOfficerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExchangeRates");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "AspNetUsers");
        }
    }
}
