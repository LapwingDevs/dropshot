using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DropShot.Infrastructure.DAL.Migrations
{
    public partial class MovedPriceToProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Variants");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Products",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Products");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Variants",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
