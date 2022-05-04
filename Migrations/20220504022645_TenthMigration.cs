using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FM_API.Migrations
{
    public partial class TenthMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Create_at",
                schema: "public",
                table: "presupuesto",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Deleted_at",
                schema: "public",
                table: "presupuesto",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Update_at",
                schema: "public",
                table: "presupuesto",
                type: "timestamp with time zone",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Create_at",
                schema: "public",
                table: "presupuesto");

            migrationBuilder.DropColumn(
                name: "Deleted_at",
                schema: "public",
                table: "presupuesto");

            migrationBuilder.DropColumn(
                name: "Update_at",
                schema: "public",
                table: "presupuesto");
        }
    }
}
