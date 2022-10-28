using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DropShot.Infrastructure.DAL.Migrations
{
    public partial class AddedStatusToVariant : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Variants",
                type: "text",
                nullable: false,
                defaultValue: "Warehouse");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Variants");
        }
    }
}
