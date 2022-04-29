using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FM_API.Migrations
{
    public partial class SixthMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                schema: "public",
                table: "transacciones");

            migrationBuilder.DropColumn(
                name: "Category",
                schema: "public",
                table: "estimaciones");

            migrationBuilder.AddColumn<long>(
                name: "Id_category",
                schema: "public",
                table: "transacciones",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "Id_category",
                schema: "public",
                table: "estimaciones",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                schema: "public",
                table: "categoria",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.CreateIndex(
                name: "IX_transacciones_Id_category",
                schema: "public",
                table: "transacciones",
                column: "Id_category");

            migrationBuilder.CreateIndex(
                name: "IX_estimaciones_Id_category",
                schema: "public",
                table: "estimaciones",
                column: "Id_category");

            migrationBuilder.AddForeignKey(
                name: "FK_estimaciones_categoria_Id_category",
                schema: "public",
                table: "estimaciones",
                column: "Id_category",
                principalSchema: "public",
                principalTable: "categoria",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_transacciones_categoria_Id_category",
                schema: "public",
                table: "transacciones",
                column: "Id_category",
                principalSchema: "public",
                principalTable: "categoria",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_estimaciones_categoria_Id_category",
                schema: "public",
                table: "estimaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_transacciones_categoria_Id_category",
                schema: "public",
                table: "transacciones");

            migrationBuilder.DropIndex(
                name: "IX_transacciones_Id_category",
                schema: "public",
                table: "transacciones");

            migrationBuilder.DropIndex(
                name: "IX_estimaciones_Id_category",
                schema: "public",
                table: "estimaciones");

            migrationBuilder.DropColumn(
                name: "Id_category",
                schema: "public",
                table: "transacciones");

            migrationBuilder.DropColumn(
                name: "Id_category",
                schema: "public",
                table: "estimaciones");

            migrationBuilder.AddColumn<string>(
                name: "Category",
                schema: "public",
                table: "transacciones",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Category",
                schema: "public",
                table: "estimaciones",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                schema: "public",
                table: "categoria",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}
