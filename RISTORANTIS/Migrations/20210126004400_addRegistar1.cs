using Microsoft.EntityFrameworkCore.Migrations;

namespace RISTORANTIS.Migrations
{
    public partial class addRegistar1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Selecionar_R_Favoritos_Selecionar_R_Favoritos_SelecionarRFavoritoIdCliente_SelecionarRFavoritoIdRestaurante",
                table: "Selecionar_R_Favoritos");

            migrationBuilder.DropIndex(
                name: "IX_Selecionar_R_Favoritos_SelecionarRFavoritoIdCliente_SelecionarRFavoritoIdRestaurante",
                table: "Selecionar_R_Favoritos");

            migrationBuilder.DropColumn(
                name: "SelecionarRFavoritoIdCliente",
                table: "Selecionar_R_Favoritos");

            migrationBuilder.DropColumn(
                name: "SelecionarRFavoritoIdRestaurante",
                table: "Selecionar_R_Favoritos");

            migrationBuilder.AddColumn<int>(
                name: "ID_Registo",
                table: "Registar",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ID_Registo",
                table: "Registar");

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
    }
}
