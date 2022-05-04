using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FM_API.Migrations
{
    public partial class EleventhMigrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Year",
                schema: "public",
                table: "presupuesto");

            migrationBuilder.AddColumn<long>(
                name: "Id_budgetYear",
                schema: "public",
                table: "presupuesto",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "años_presupuesto",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Year = table.Column<int>(type: "integer", nullable: false),
                    Deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Create_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Update_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_años_presupuesto", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_presupuesto_Id_budgetYear",
                schema: "public",
                table: "presupuesto",
                column: "Id_budgetYear");

            migrationBuilder.AddForeignKey(
                name: "FK_presupuesto_años_presupuesto_Id_budgetYear",
                schema: "public",
                table: "presupuesto",
                column: "Id_budgetYear",
                principalSchema: "public",
                principalTable: "años_presupuesto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_presupuesto_años_presupuesto_Id_budgetYear",
                schema: "public",
                table: "presupuesto");

            migrationBuilder.DropTable(
                name: "años_presupuesto",
                schema: "public");

            migrationBuilder.DropIndex(
                name: "IX_presupuesto_Id_budgetYear",
                schema: "public",
                table: "presupuesto");

            migrationBuilder.DropColumn(
                name: "Id_budgetYear",
                schema: "public",
                table: "presupuesto");

            migrationBuilder.AddColumn<int>(
                name: "Year",
                schema: "public",
                table: "presupuesto",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
