using Microsoft.EntityFrameworkCore.Migrations;

namespace RISTORANTIS.Migrations
{
    public partial class addSelecionarServico : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ID_SelectServico",
                table: "Selecionar_Servico",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ID_SelectServico",
                table: "Selecionar_Servico");
        }
    }
}
