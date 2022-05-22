using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FM_API.Migrations
{
    public partial class SixteenthMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "estimate_income",
                schema: "public");

            migrationBuilder.DropTable(
                name: "estimate_spent",
                schema: "public");

            migrationBuilder.DropTable(
                name: "TransactionIncome",
                schema: "public");

            migrationBuilder.DropTable(
                name: "TransactionSpent",
                schema: "public");

            migrationBuilder.DropTable(
                name: "ingresos",
                schema: "public");

            migrationBuilder.DropTable(
                name: "gastos",
                schema: "public");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                schema: "public",
                table: "transacciones",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "Id_Type",
                schema: "public",
                table: "transacciones",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "Id_Type",
                schema: "public",
                table: "estimaciones",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "type",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_type", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_transacciones_Id_Type",
                schema: "public",
                table: "transacciones",
                column: "Id_Type");

            migrationBuilder.CreateIndex(
                name: "IX_estimaciones_Id_Type",
                schema: "public",
                table: "estimaciones",
                column: "Id_Type");

            migrationBuilder.AddForeignKey(
                name: "FK_estimaciones_type_Id_Type",
                schema: "public",
                table: "estimaciones",
                column: "Id_Type",
                principalSchema: "public",
                principalTable: "type",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_transacciones_type_Id_Type",
                schema: "public",
                table: "transacciones",
                column: "Id_Type",
                principalSchema: "public",
                principalTable: "type",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_estimaciones_type_Id_Type",
                schema: "public",
                table: "estimaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_transacciones_type_Id_Type",
                schema: "public",
                table: "transacciones");

            migrationBuilder.DropTable(
                name: "type",
                schema: "public");

            migrationBuilder.DropIndex(
                name: "IX_transacciones_Id_Type",
                schema: "public",
                table: "transacciones");

            migrationBuilder.DropIndex(
                name: "IX_estimaciones_Id_Type",
                schema: "public",
                table: "estimaciones");

            migrationBuilder.DropColumn(
                name: "Date",
                schema: "public",
                table: "transacciones");

            migrationBuilder.DropColumn(
                name: "Id_Type",
                schema: "public",
                table: "transacciones");

            migrationBuilder.DropColumn(
                name: "Id_Type",
                schema: "public",
                table: "estimaciones");

            migrationBuilder.CreateTable(
                name: "gastos",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Amount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gastos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ingresos",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Amount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ingresos", x => x.Id);
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

            migrationBuilder.CreateTable(
                name: "TransactionSpent",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Id_Spent = table.Column<long>(type: "bigint", nullable: false),
                    Id_Transaction = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionSpent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransactionSpent_gastos_Id_Spent",
                        column: x => x.Id_Spent,
                        principalSchema: "public",
                        principalTable: "gastos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TransactionSpent_transacciones_Id_Transaction",
                        column: x => x.Id_Transaction,
                        principalSchema: "public",
                        principalTable: "transacciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "TransactionIncome",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Id_Income = table.Column<long>(type: "bigint", nullable: false),
                    Id_Transaction = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionIncome", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransactionIncome_ingresos_Id_Income",
                        column: x => x.Id_Income,
                        principalSchema: "public",
                        principalTable: "ingresos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TransactionIncome_transacciones_Id_Transaction",
                        column: x => x.Id_Transaction,
                        principalSchema: "public",
                        principalTable: "transacciones",
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

            migrationBuilder.CreateIndex(
                name: "IX_TransactionIncome_Id_Income",
                schema: "public",
                table: "TransactionIncome",
                column: "Id_Income");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionIncome_Id_Transaction",
                schema: "public",
                table: "TransactionIncome",
                column: "Id_Transaction");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionSpent_Id_Spent",
                schema: "public",
                table: "TransactionSpent",
                column: "Id_Spent");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionSpent_Id_Transaction",
                schema: "public",
                table: "TransactionSpent",
                column: "Id_Transaction");
        }
    }
}
