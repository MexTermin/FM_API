using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FM_API.Migrations
{
    public partial class FourteenthMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_gastos_transacciones_Id_spent",
                schema: "public",
                table: "gastos");

            migrationBuilder.DropForeignKey(
                name: "FK_ingresos_transacciones_Id_income",
                schema: "public",
                table: "ingresos");

            migrationBuilder.DropIndex(
                name: "IX_ingresos_Id_income",
                schema: "public",
                table: "ingresos");

            migrationBuilder.DropIndex(
                name: "IX_gastos_Id_spent",
                schema: "public",
                table: "gastos");

            migrationBuilder.DropColumn(
                name: "Id_income",
                schema: "public",
                table: "transacciones");

            migrationBuilder.DropColumn(
                name: "Id_spent",
                schema: "public",
                table: "transacciones");

            migrationBuilder.DropColumn(
                name: "Id_income",
                schema: "public",
                table: "ingresos");

            migrationBuilder.DropColumn(
                name: "Id_spent",
                schema: "public",
                table: "gastos");

            migrationBuilder.CreateTable(
                name: "TransactionIncome",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Id_Transaction = table.Column<long>(type: "bigint", nullable: false),
                    Id_Income = table.Column<long>(type: "bigint", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "TransactionSpent",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Id_Transaction = table.Column<long>(type: "bigint", nullable: false),
                    Id_Spent = table.Column<long>(type: "bigint", nullable: false)
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TransactionIncome",
                schema: "public");

            migrationBuilder.DropTable(
                name: "TransactionSpent",
                schema: "public");

            migrationBuilder.AddColumn<long>(
                name: "Id_income",
                schema: "public",
                table: "transacciones",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "Id_spent",
                schema: "public",
                table: "transacciones",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "Id_income",
                schema: "public",
                table: "ingresos",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Id_spent",
                schema: "public",
                table: "gastos",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ingresos_Id_income",
                schema: "public",
                table: "ingresos",
                column: "Id_income");

            migrationBuilder.CreateIndex(
                name: "IX_gastos_Id_spent",
                schema: "public",
                table: "gastos",
                column: "Id_spent");

            migrationBuilder.AddForeignKey(
                name: "FK_gastos_transacciones_Id_spent",
                schema: "public",
                table: "gastos",
                column: "Id_spent",
                principalSchema: "public",
                principalTable: "transacciones",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ingresos_transacciones_Id_income",
                schema: "public",
                table: "ingresos",
                column: "Id_income",
                principalSchema: "public",
                principalTable: "transacciones",
                principalColumn: "Id");
        }
    }
}
