using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FM_API.Migrations
{
    public partial class SeventhMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                schema: "public",
                table: "categoria",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Deleted",
                schema: "public",
                table: "categoria");
        }
    }
}
