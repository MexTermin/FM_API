using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FM_API.Migrations
{
    public partial class TwelfthMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.RenameColumn(
                name: "Id_budgetYear",
                schema: "public",
                table: "presupuesto",
                newName: "Year");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Year",
                schema: "public",
                table: "presupuesto",
                newName: "Id_budgetYear");

            migrationBuilder.CreateTable(
                name: "años_presupuesto",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Create_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Update_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Year = table.Column<int>(type: "integer", nullable: false)
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
    }
}
