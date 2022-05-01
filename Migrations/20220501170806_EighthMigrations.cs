using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FM_API.Migrations
{
    public partial class EighthMigrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Deleted",
                schema: "public",
                table: "categoria");

            migrationBuilder.AddColumn<DateTime>(
                name: "Create_at",
                schema: "public",
                table: "categoria",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Deleted_at",
                schema: "public",
                table: "categoria",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Update_at",
                schema: "public",
                table: "categoria",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Create_at",
                schema: "public",
                table: "categoria");

            migrationBuilder.DropColumn(
                name: "Deleted_at",
                schema: "public",
                table: "categoria");

            migrationBuilder.DropColumn(
                name: "Update_at",
                schema: "public",
                table: "categoria");

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                schema: "public",
                table: "categoria",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
