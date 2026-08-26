using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinanceFlow.Migrations
{
    /// <inheritdoc />
    public partial class AddIdentityAndFkContrainsMuchTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "sueldo_id",
                table: "SueldosPresupuestos",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "PresupuestoId",
                table: "sueldo_historico",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SueldosPresupuestos_sueldo_id",
                table: "SueldosPresupuestos",
                column: "sueldo_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_sueldo_historico_PresupuestoId",
                table: "sueldo_historico",
                column: "PresupuestoId");

            migrationBuilder.AddForeignKey(
                name: "FK_sueldo_historico_SueldosPresupuestos_PresupuestoId",
                table: "sueldo_historico",
                column: "PresupuestoId",
                principalTable: "SueldosPresupuestos",
                principalColumn: "presupuesto_id");

            migrationBuilder.AddForeignKey(
                name: "FK_SueldosPresupuestos_sueldo_historico_sueldo_id",
                table: "SueldosPresupuestos",
                column: "sueldo_id",
                principalTable: "sueldo_historico",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_sueldo_historico_SueldosPresupuestos_PresupuestoId",
                table: "sueldo_historico");

            migrationBuilder.DropForeignKey(
                name: "FK_SueldosPresupuestos_sueldo_historico_sueldo_id",
                table: "SueldosPresupuestos");

            migrationBuilder.DropIndex(
                name: "IX_SueldosPresupuestos_sueldo_id",
                table: "SueldosPresupuestos");

            migrationBuilder.DropIndex(
                name: "IX_sueldo_historico_PresupuestoId",
                table: "sueldo_historico");

            migrationBuilder.DropColumn(
                name: "sueldo_id",
                table: "SueldosPresupuestos");

            migrationBuilder.DropColumn(
                name: "PresupuestoId",
                table: "sueldo_historico");
        }
    }
}
