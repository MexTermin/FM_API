using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FM_API.Migrations
{
    public partial class FirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.CreateTable(
                name: "presupuesto",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Mes = table.Column<int>(type: "integer", nullable: false),
                    Agno = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_presupuesto", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "rol",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Rol_type = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rol", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "estimaciones",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Categoria = table.Column<string>(type: "text", nullable: true),
                    Plan = table.Column<int>(type: "integer", nullable: false),
                    Id_presupuesto = table.Column<long>(type: "bigint", nullable: false),
                    Id_gastos = table.Column<long>(type: "bigint", nullable: false),
                    Id_ingresos = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_estimaciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_estimaciones_presupuesto_Id_presupuesto",
                        column: x => x.Id_presupuesto,
                        principalSchema: "public",
                        principalTable: "presupuesto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "transacciones",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Categoria = table.Column<string>(type: "text", nullable: true),
                    Descripcion = table.Column<string>(type: "text", nullable: true),
                    Importe = table.Column<int>(type: "integer", nullable: false),
                    Id_presupuesto = table.Column<long>(type: "bigint", nullable: false),
                    Id_gastos = table.Column<long>(type: "bigint", nullable: false),
                    Id_ingresos = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_transacciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_transacciones_presupuesto_Id_presupuesto",
                        column: x => x.Id_presupuesto,
                        principalSchema: "public",
                        principalTable: "presupuesto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "usuario",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Contrasegna = table.Column<string>(type: "text", nullable: false),
                    Correo = table.Column<string>(type: "text", nullable: false),
                    Nombres = table.Column<string>(type: "text", nullable: true),
                    Apellidos = table.Column<string>(type: "text", nullable: true),
                    Id_rol = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_usuario_rol_Id_rol",
                        column: x => x.Id_rol,
                        principalSchema: "public",
                        principalTable: "rol",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "gastos",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Monto = table.Column<int>(type: "integer", nullable: false),
                    Id_gastos = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gastos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_gastos_estimaciones_Id_gastos",
                        column: x => x.Id_gastos,
                        principalSchema: "public",
                        principalTable: "estimaciones",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_gastos_transacciones_Id_gastos",
                        column: x => x.Id_gastos,
                        principalSchema: "public",
                        principalTable: "transacciones",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ingresos",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Monto = table.Column<int>(type: "integer", nullable: false),
                    Id_ingresos = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ingresos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ingresos_estimaciones_Id_ingresos",
                        column: x => x.Id_ingresos,
                        principalSchema: "public",
                        principalTable: "estimaciones",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ingresos_transacciones_Id_ingresos",
                        column: x => x.Id_ingresos,
                        principalSchema: "public",
                        principalTable: "transacciones",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_estimaciones_Id_presupuesto",
                schema: "public",
                table: "estimaciones",
                column: "Id_presupuesto");

            migrationBuilder.CreateIndex(
                name: "IX_gastos_Id_gastos",
                schema: "public",
                table: "gastos",
                column: "Id_gastos");

            migrationBuilder.CreateIndex(
                name: "IX_ingresos_Id_ingresos",
                schema: "public",
                table: "ingresos",
                column: "Id_ingresos");

            migrationBuilder.CreateIndex(
                name: "IX_transacciones_Id_presupuesto",
                schema: "public",
                table: "transacciones",
                column: "Id_presupuesto");

            migrationBuilder.CreateIndex(
                name: "IX_usuario_Id_rol",
                schema: "public",
                table: "usuario",
                column: "Id_rol");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "gastos",
                schema: "public");

            migrationBuilder.DropTable(
                name: "ingresos",
                schema: "public");

            migrationBuilder.DropTable(
                name: "usuario",
                schema: "public");

            migrationBuilder.DropTable(
                name: "estimaciones",
                schema: "public");

            migrationBuilder.DropTable(
                name: "transacciones",
                schema: "public");

            migrationBuilder.DropTable(
                name: "rol",
                schema: "public");

            migrationBuilder.DropTable(
                name: "presupuesto",
                schema: "public");
        }
    }
}
