using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FM_API.Migrations
{
    public partial class ThirteenthMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_gastos_estimaciones_Id_spent",
                schema: "public",
                table: "gastos");

            migrationBuilder.DropForeignKey(
                name: "FK_ingresos_estimaciones_Id_income",
                schema: "public",
                table: "ingresos");

            migrationBuilder.DropColumn(
                name: "Id_income",
                schema: "public",
                table: "estimaciones");

            migrationBuilder.DropColumn(
                name: "Id_spent",
                schema: "public",
                table: "estimaciones");

            migrationBuilder.CreateTable(
                name: "estimate_income",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Id_Estimate = table.Column<long>(type: "bigint", nullable: false),
                    Id_Income = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_estimate_income", x => x.Id);
                    table.ForeignKey(
                        name: "FK_estimate_income_estimaciones_Id_Estimate",
                        column: x => x.Id_Estimate,
                        principalSchema: "public",
                        principalTable: "estimaciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_estimate_income_ingresos_Id_Income",
                        column: x => x.Id_Income,
                        principalSchema: "public",
                        principalTable: "ingresos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "estimate_spent",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Id_Estimate = table.Column<long>(type: "bigint", nullable: false),
                    Id_Spent = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_estimate_spent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_estimate_spent_estimaciones_Id_Estimate",
                        column: x => x.Id_Estimate,
                        principalSchema: "public",
                        principalTable: "estimaciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_estimate_spent_gastos_Id_Spent",
                        column: x => x.Id_Spent,
                        principalSchema: "public",
                        principalTable: "gastos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_estimate_income_Id_Estimate",
                schema: "public",
                table: "estimate_income",
                column: "Id_Estimate");

            migrationBuilder.CreateIndex(
                name: "IX_estimate_income_Id_Income",
                schema: "public",
                table: "estimate_income",
                column: "Id_Income");

            migrationBuilder.CreateIndex(
                name: "IX_estimate_spent_Id_Estimate",
                schema: "public",
                table: "estimate_spent",
                column: "Id_Estimate");

            migrationBuilder.CreateIndex(
                name: "IX_estimate_spent_Id_Spent",
                schema: "public",
                table: "estimate_spent",
                column: "Id_Spent");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "estimate_income",
                schema: "public");

            migrationBuilder.DropTable(
                name: "estimate_spent",
                schema: "public");

            migrationBuilder.AddColumn<long>(
                name: "Id_income",
                schema: "public",
                table: "estimaciones",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "Id_spent",
                schema: "public",
                table: "estimaciones",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddForeignKey(
                name: "FK_gastos_estimaciones_Id_spent",
                schema: "public",
                table: "gastos",
                column: "Id_spent",
                principalSchema: "public",
                principalTable: "estimaciones",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ingresos_estimaciones_Id_income",
                schema: "public",
                table: "ingresos",
                column: "Id_income",
                principalSchema: "public",
                principalTable: "estimaciones",
                principalColumn: "Id");
        }
    }
}
