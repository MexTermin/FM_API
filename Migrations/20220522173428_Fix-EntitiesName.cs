using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FM_API.Migrations
{
    public partial class FixEntitiesName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_estimaciones_type_Id_Type",
                schema: "public",
                table: "estimaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_transacciones_type_Id_Type",
                schema: "public",
                table: "transacciones");

            migrationBuilder.RenameColumn(
                name: "Id_Type",
                schema: "public",
                table: "transacciones",
                newName: "Id_type");

            migrationBuilder.RenameIndex(
                name: "IX_transacciones_Id_Type",
                schema: "public",
                table: "transacciones",
                newName: "IX_transacciones_Id_type");

            migrationBuilder.RenameColumn(
                name: "Id_Type",
                schema: "public",
                table: "estimaciones",
                newName: "Id_type");

            migrationBuilder.RenameIndex(
                name: "IX_estimaciones_Id_Type",
                schema: "public",
                table: "estimaciones",
                newName: "IX_estimaciones_Id_type");

            migrationBuilder.AddForeignKey(
                name: "FK_estimaciones_type_Id_type",
                schema: "public",
                table: "estimaciones",
                column: "Id_type",
                principalSchema: "public",
                principalTable: "type",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_transacciones_type_Id_type",
                schema: "public",
                table: "transacciones",
                column: "Id_type",
                principalSchema: "public",
                principalTable: "type",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_estimaciones_type_Id_type",
                schema: "public",
                table: "estimaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_transacciones_type_Id_type",
                schema: "public",
                table: "transacciones");

            migrationBuilder.RenameColumn(
                name: "Id_type",
                schema: "public",
                table: "transacciones",
                newName: "Id_Type");

            migrationBuilder.RenameIndex(
                name: "IX_transacciones_Id_type",
                schema: "public",
                table: "transacciones",
                newName: "IX_transacciones_Id_Type");

            migrationBuilder.RenameColumn(
                name: "Id_type",
                schema: "public",
                table: "estimaciones",
                newName: "Id_Type");

            migrationBuilder.RenameIndex(
                name: "IX_estimaciones_Id_type",
                schema: "public",
                table: "estimaciones",
                newName: "IX_estimaciones_Id_Type");

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
    }
}
