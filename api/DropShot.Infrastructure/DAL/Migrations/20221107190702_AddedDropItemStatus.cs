using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DropShot.Infrastructure.DAL.Migrations
{
    public partial class AddedDropItemStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "DropItems",
                type: "text",
                nullable: false,
                defaultValue: "Available");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "DropItems");
        }
    }
}
