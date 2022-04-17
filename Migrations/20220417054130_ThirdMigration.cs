using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FM_API.Migrations
{
    public partial class ThirdMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_estimaciones_presupuesto_Id_presupuesto",
                schema: "public",
                table: "estimaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_gastos_estimaciones_Id_gastos",
                schema: "public",
                table: "gastos");

            migrationBuilder.DropForeignKey(
                name: "FK_gastos_transacciones_Id_gastos",
                schema: "public",
                table: "gastos");

            migrationBuilder.DropForeignKey(
                name: "FK_ingresos_estimaciones_Id_ingresos",
                schema: "public",
                table: "ingresos");

            migrationBuilder.DropForeignKey(
                name: "FK_ingresos_transacciones_Id_ingresos",
                schema: "public",
                table: "ingresos");

            migrationBuilder.DropForeignKey(
                name: "FK_transacciones_presupuesto_Id_presupuesto",
                schema: "public",
                table: "transacciones");

            migrationBuilder.DropIndex(
                name: "IX_transacciones_Id_presupuesto",
                schema: "public",
                table: "transacciones");

            migrationBuilder.DropIndex(
                name: "IX_estimaciones_Id_presupuesto",
                schema: "public",
                table: "estimaciones");

            migrationBuilder.RenameColumn(
                name: "Nombres",
                schema: "public",
                table: "usuario",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Correo",
                schema: "public",
                table: "usuario",
                newName: "Pass");

            migrationBuilder.RenameColumn(
                name: "Contrasegna",
                schema: "public",
                table: "usuario",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "Apellidos",
                schema: "public",
                table: "usuario",
                newName: "Lastname");

            migrationBuilder.RenameColumn(
                name: "Importe",
                schema: "public",
                table: "transacciones",
                newName: "Amount");

            migrationBuilder.RenameColumn(
                name: "Id_presupuesto",
                schema: "public",
                table: "transacciones",
                newName: "Id_spent");

            migrationBuilder.RenameColumn(
                name: "Id_ingresos",
                schema: "public",
                table: "transacciones",
                newName: "Id_income");

            migrationBuilder.RenameColumn(
                name: "Id_gastos",
                schema: "public",
                table: "transacciones",
                newName: "Id_budget");

            migrationBuilder.RenameColumn(
                name: "Descripcion",
                schema: "public",
                table: "transacciones",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "Categoria",
                schema: "public",
                table: "transacciones",
                newName: "Category");

            migrationBuilder.RenameColumn(
                name: "Mes",
                schema: "public",
                table: "presupuesto",
                newName: "Year");

            migrationBuilder.RenameColumn(
                name: "Agno",
                schema: "public",
                table: "presupuesto",
                newName: "Month");

            migrationBuilder.RenameColumn(
                name: "Monto",
                schema: "public",
                table: "ingresos",
                newName: "Amount");

            migrationBuilder.RenameColumn(
                name: "Id_ingresos",
                schema: "public",
                table: "ingresos",
                newName: "Id_income");

            migrationBuilder.RenameIndex(
                name: "IX_ingresos_Id_ingresos",
                schema: "public",
                table: "ingresos",
                newName: "IX_ingresos_Id_income");

            migrationBuilder.RenameColumn(
                name: "Monto",
                schema: "public",
                table: "gastos",
                newName: "Amount");

            migrationBuilder.RenameColumn(
                name: "Id_gastos",
                schema: "public",
                table: "gastos",
                newName: "Id_spent");

            migrationBuilder.RenameIndex(
                name: "IX_gastos_Id_gastos",
                schema: "public",
                table: "gastos",
                newName: "IX_gastos_Id_spent");

            migrationBuilder.RenameColumn(
                name: "Id_presupuesto",
                schema: "public",
                table: "estimaciones",
                newName: "Id_spent");

            migrationBuilder.RenameColumn(
                name: "Id_ingresos",
                schema: "public",
                table: "estimaciones",
                newName: "Id_income");

            migrationBuilder.RenameColumn(
                name: "Id_gastos",
                schema: "public",
                table: "estimaciones",
                newName: "Id_budget");

            migrationBuilder.RenameColumn(
                name: "Categoria",
                schema: "public",
                table: "estimaciones",
                newName: "Category");

            migrationBuilder.CreateIndex(
                name: "IX_transacciones_Id_budget",
                schema: "public",
                table: "transacciones",
                column: "Id_budget");

            migrationBuilder.CreateIndex(
                name: "IX_estimaciones_Id_budget",
                schema: "public",
                table: "estimaciones",
                column: "Id_budget");

            migrationBuilder.AddForeignKey(
                name: "FK_estimaciones_presupuesto_Id_budget",
                schema: "public",
                table: "estimaciones",
                column: "Id_budget",
                principalSchema: "public",
                principalTable: "presupuesto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_gastos_estimaciones_Id_spent",
                schema: "public",
                table: "gastos",
                column: "Id_spent",
                principalSchema: "public",
                principalTable: "estimaciones",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_gastos_transacciones_Id_spent",
                schema: "public",
                table: "gastos",
                column: "Id_spent",
                principalSchema: "public",
                principalTable: "transacciones",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ingresos_estimaciones_Id_income",
                schema: "public",
                table: "ingresos",
                column: "Id_income",
                principalSchema: "public",
                principalTable: "estimaciones",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ingresos_transacciones_Id_income",
                schema: "public",
                table: "ingresos",
                column: "Id_income",
                principalSchema: "public",
                principalTable: "transacciones",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_transacciones_presupuesto_Id_budget",
                schema: "public",
                table: "transacciones",
                column: "Id_budget",
                principalSchema: "public",
                principalTable: "presupuesto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_estimaciones_presupuesto_Id_budget",
                schema: "public",
                table: "estimaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_gastos_estimaciones_Id_spent",
                schema: "public",
                table: "gastos");

            migrationBuilder.DropForeignKey(
                name: "FK_gastos_transacciones_Id_spent",
                schema: "public",
                table: "gastos");

            migrationBuilder.DropForeignKey(
                name: "FK_ingresos_estimaciones_Id_income",
                schema: "public",
                table: "ingresos");

            migrationBuilder.DropForeignKey(
                name: "FK_ingresos_transacciones_Id_income",
                schema: "public",
                table: "ingresos");

            migrationBuilder.DropForeignKey(
                name: "FK_transacciones_presupuesto_Id_budget",
                schema: "public",
                table: "transacciones");

            migrationBuilder.DropIndex(
                name: "IX_transacciones_Id_budget",
                schema: "public",
                table: "transacciones");

            migrationBuilder.DropIndex(
                name: "IX_estimaciones_Id_budget",
                schema: "public",
                table: "estimaciones");

            migrationBuilder.RenameColumn(
                name: "Pass",
                schema: "public",
                table: "usuario",
                newName: "Correo");

            migrationBuilder.RenameColumn(
                name: "Name",
                schema: "public",
                table: "usuario",
                newName: "Nombres");

            migrationBuilder.RenameColumn(
                name: "Lastname",
                schema: "public",
                table: "usuario",
                newName: "Apellidos");

            migrationBuilder.RenameColumn(
                name: "Email",
                schema: "public",
                table: "usuario",
                newName: "Contrasegna");

            migrationBuilder.RenameColumn(
                name: "Id_spent",
                schema: "public",
                table: "transacciones",
                newName: "Id_presupuesto");

            migrationBuilder.RenameColumn(
                name: "Id_income",
                schema: "public",
                table: "transacciones",
                newName: "Id_ingresos");

            migrationBuilder.RenameColumn(
                name: "Id_budget",
                schema: "public",
                table: "transacciones",
                newName: "Id_gastos");

            migrationBuilder.RenameColumn(
                name: "Description",
                schema: "public",
                table: "transacciones",
                newName: "Descripcion");

            migrationBuilder.RenameColumn(
                name: "Category",
                schema: "public",
                table: "transacciones",
                newName: "Categoria");

            migrationBuilder.RenameColumn(
                name: "Amount",
                schema: "public",
                table: "transacciones",
                newName: "Importe");

            migrationBuilder.RenameColumn(
                name: "Year",
                schema: "public",
                table: "presupuesto",
                newName: "Mes");

            migrationBuilder.RenameColumn(
                name: "Month",
                schema: "public",
                table: "presupuesto",
                newName: "Agno");

            migrationBuilder.RenameColumn(
                name: "Id_income",
                schema: "public",
                table: "ingresos",
                newName: "Id_ingresos");

            migrationBuilder.RenameColumn(
                name: "Amount",
                schema: "public",
                table: "ingresos",
                newName: "Monto");

            migrationBuilder.RenameIndex(
                name: "IX_ingresos_Id_income",
                schema: "public",
                table: "ingresos",
                newName: "IX_ingresos_Id_ingresos");

            migrationBuilder.RenameColumn(
                name: "Id_spent",
                schema: "public",
                table: "gastos",
                newName: "Id_gastos");

            migrationBuilder.RenameColumn(
                name: "Amount",
                schema: "public",
                table: "gastos",
                newName: "Monto");

            migrationBuilder.RenameIndex(
                name: "IX_gastos_Id_spent",
                schema: "public",
                table: "gastos",
                newName: "IX_gastos_Id_gastos");

            migrationBuilder.RenameColumn(
                name: "Id_spent",
                schema: "public",
                table: "estimaciones",
                newName: "Id_presupuesto");

            migrationBuilder.RenameColumn(
                name: "Id_income",
                schema: "public",
                table: "estimaciones",
                newName: "Id_ingresos");

            migrationBuilder.RenameColumn(
                name: "Id_budget",
                schema: "public",
                table: "estimaciones",
                newName: "Id_gastos");

            migrationBuilder.RenameColumn(
                name: "Category",
                schema: "public",
                table: "estimaciones",
                newName: "Categoria");

            migrationBuilder.CreateIndex(
                name: "IX_transacciones_Id_presupuesto",
                schema: "public",
                table: "transacciones",
                column: "Id_presupuesto");

            migrationBuilder.CreateIndex(
                name: "IX_estimaciones_Id_presupuesto",
                schema: "public",
                table: "estimaciones",
                column: "Id_presupuesto");

            migrationBuilder.AddForeignKey(
                name: "FK_estimaciones_presupuesto_Id_presupuesto",
                schema: "public",
                table: "estimaciones",
                column: "Id_presupuesto",
                principalSchema: "public",
                principalTable: "presupuesto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_gastos_estimaciones_Id_gastos",
                schema: "public",
                table: "gastos",
                column: "Id_gastos",
                principalSchema: "public",
                principalTable: "estimaciones",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_gastos_transacciones_Id_gastos",
                schema: "public",
                table: "gastos",
                column: "Id_gastos",
                principalSchema: "public",
                principalTable: "transacciones",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ingresos_estimaciones_Id_ingresos",
                schema: "public",
                table: "ingresos",
                column: "Id_ingresos",
                principalSchema: "public",
                principalTable: "estimaciones",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ingresos_transacciones_Id_ingresos",
                schema: "public",
                table: "ingresos",
                column: "Id_ingresos",
                principalSchema: "public",
                principalTable: "transacciones",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_transacciones_presupuesto_Id_presupuesto",
                schema: "public",
                table: "transacciones",
                column: "Id_presupuesto",
                principalSchema: "public",
                principalTable: "presupuesto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
