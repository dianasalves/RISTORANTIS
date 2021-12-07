using Microsoft.EntityFrameworkCore.Migrations;

namespace RISTORANTIS.Migrations
{
    public partial class aaaa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ID_SelectServico",
                table: "Selecionar_Servico",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "SelecionarRFavoritoIdCliente",
                table: "Selecionar_R_Favoritos",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SelecionarRFavoritoIdRestaurante",
                table: "Selecionar_R_Favoritos",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK__tmp_ms_x__A50489DDF1E46E6F",
                table: "Selecionar_Servico",
                column: "ID_SelectServico");

            migrationBuilder.CreateIndex(
                name: "IX_Selecionar_R_Favoritos_SelecionarRFavoritoIdCliente_SelecionarRFavoritoIdRestaurante",
                table: "Selecionar_R_Favoritos",
                columns: new[] { "SelecionarRFavoritoIdCliente", "SelecionarRFavoritoIdRestaurante" });

            migrationBuilder.AddForeignKey(
                name: "FK_Selecionar_R_Favoritos_Selecionar_R_Favoritos_SelecionarRFavoritoIdCliente_SelecionarRFavoritoIdRestaurante",
                table: "Selecionar_R_Favoritos",
                columns: new[] { "SelecionarRFavoritoIdCliente", "SelecionarRFavoritoIdRestaurante" },
                principalTable: "Selecionar_R_Favoritos",
                principalColumns: new[] { "ID_Cliente", "ID_Restaurante" },
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Selecionar_R_Favoritos_Selecionar_R_Favoritos_SelecionarRFavoritoIdCliente_SelecionarRFavoritoIdRestaurante",
                table: "Selecionar_R_Favoritos");

            migrationBuilder.DropPrimaryKey(
                name: "PK__tmp_ms_x__A50489DDF1E46E6F",
                table: "Selecionar_Servico");

            migrationBuilder.DropIndex(
                name: "IX_Selecionar_R_Favoritos_SelecionarRFavoritoIdCliente_SelecionarRFavoritoIdRestaurante",
                table: "Selecionar_R_Favoritos");

            migrationBuilder.DropColumn(
                name: "SelecionarRFavoritoIdCliente",
                table: "Selecionar_R_Favoritos");

            migrationBuilder.DropColumn(
                name: "SelecionarRFavoritoIdRestaurante",
                table: "Selecionar_R_Favoritos");

            migrationBuilder.AlterColumn<int>(
                name: "ID_SelectServico",
                table: "Selecionar_Servico",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");
        }
    }
}
