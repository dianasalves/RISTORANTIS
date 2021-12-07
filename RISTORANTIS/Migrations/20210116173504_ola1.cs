using Microsoft.EntityFrameworkCore.Migrations;

namespace RISTORANTIS.Migrations
{
    public partial class ola1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TipoServicoIdServico",
                table: "Tipo_Servico",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tipo_Servico_TipoServicoIdServico",
                table: "Tipo_Servico",
                column: "TipoServicoIdServico");

            migrationBuilder.AddForeignKey(
                name: "FK_Tipo_Servico_Tipo_Servico_TipoServicoIdServico",
                table: "Tipo_Servico",
                column: "TipoServicoIdServico",
                principalTable: "Tipo_Servico",
                principalColumn: "ID_Servico",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tipo_Servico_Tipo_Servico_TipoServicoIdServico",
                table: "Tipo_Servico");

            migrationBuilder.DropIndex(
                name: "IX_Tipo_Servico_TipoServicoIdServico",
                table: "Tipo_Servico");

            migrationBuilder.DropColumn(
                name: "TipoServicoIdServico",
                table: "Tipo_Servico");
        }
    }
}
