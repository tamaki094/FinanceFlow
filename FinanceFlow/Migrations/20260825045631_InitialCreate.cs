using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinanceFlow.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "categorias",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    descripcion = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    es_gasto = table.Column<bool>(type: "bit", nullable: false),
                    nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categorias", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "usuarios",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    fecha_registro = table.Column<DateTime>(type: "datetime2", nullable: true),
                    foto_url = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    email_verificado = table.Column<bool>(type: "bit", nullable: true),
                    estatus_activo = table.Column<bool>(type: "bit", nullable: true),
                    proveedor = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    telefono = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    uid = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuarios", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "gastos",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    concepto = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    fecha = table.Column<DateTime>(type: "date", nullable: false),
                    monto = table.Column<decimal>(type: "numeric(12,2)", nullable: false),
                    categoria_id = table.Column<long>(type: "bigint", nullable: false),
                    usuario_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gastos", x => x.id);
                    table.ForeignKey(
                        name: "FK_gastos_categorias_categoria_id",
                        column: x => x.categoria_id,
                        principalTable: "categorias",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_gastos_usuarios_usuario_id",
                        column: x => x.usuario_id,
                        principalTable: "usuarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "sueldo_historico",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    activo = table.Column<bool>(type: "bit", nullable: false),
                    fecha_asignacion = table.Column<DateTime>(type: "date", nullable: false),
                    monto = table.Column<decimal>(type: "numeric(12,2)", nullable: false),
                    usuario_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sueldo_historico", x => x.id);
                    table.ForeignKey(
                        name: "FK_sueldo_historico_usuarios_usuario_id",
                        column: x => x.usuario_id,
                        principalTable: "usuarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SueldosPresupuestos",
                columns: table => new
                {
                    presupuesto_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    usuario_id = table.Column<long>(type: "bigint", nullable: true),
                    monto_necesarios = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    monto_play = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    monto_ahorro = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    monto_provisiones = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    mes_anio = table.Column<string>(type: "nvarchar(7)", maxLength: 7, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SueldosPresupuestos", x => x.presupuesto_id);
                    table.ForeignKey(
                        name: "FK_SueldosPresupuestos_usuarios_usuario_id",
                        column: x => x.usuario_id,
                        principalTable: "usuarios",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_categorias_nombre",
                table: "categorias",
                column: "nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_gastos_categoria_id",
                table: "gastos",
                column: "categoria_id");

            migrationBuilder.CreateIndex(
                name: "IX_gastos_usuario_id",
                table: "gastos",
                column: "usuario_id");

            migrationBuilder.CreateIndex(
                name: "IX_sueldo_historico_usuario_id",
                table: "sueldo_historico",
                column: "usuario_id");

            migrationBuilder.CreateIndex(
                name: "IX_SueldosPresupuestos_usuario_id",
                table: "SueldosPresupuestos",
                column: "usuario_id");

            migrationBuilder.CreateIndex(
                name: "IX_usuarios_email",
                table: "usuarios",
                column: "email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "gastos");

            migrationBuilder.DropTable(
                name: "sueldo_historico");

            migrationBuilder.DropTable(
                name: "SueldosPresupuestos");

            migrationBuilder.DropTable(
                name: "categorias");

            migrationBuilder.DropTable(
                name: "usuarios");
        }
    }
}
