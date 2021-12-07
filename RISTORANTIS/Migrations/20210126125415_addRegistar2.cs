using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RISTORANTIS.Migrations
{
    public partial class addRegistar2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "RegistarDataDisponibilidade",
                table: "Selecionar_P_Favoritos",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RegistarIdPratoDoDia",
                table: "Selecionar_P_Favoritos",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RegistarIdRestaurante",
                table: "Selecionar_P_Favoritos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Selecionar_P_Favoritos_RegistarDataDisponibilidade_RegistarIdRestaurante_RegistarIdPratoDoDia",
                table: "Selecionar_P_Favoritos",
                columns: new[] { "RegistarDataDisponibilidade", "RegistarIdRestaurante", "RegistarIdPratoDoDia" });

            migrationBuilder.AddForeignKey(
                name: "FK_Selecionar_P_Favoritos_Registar_RegistarDataDisponibilidade_RegistarIdRestaurante_RegistarIdPratoDoDia",
                table: "Selecionar_P_Favoritos",
                columns: new[] { "RegistarDataDisponibilidade", "RegistarIdRestaurante", "RegistarIdPratoDoDia" },
                principalTable: "Registar",
                principalColumns: new[] { "Data_Disponibilidade", "ID_Restaurante", "ID_PratoDoDia" },
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Selecionar_P_Favoritos_Registar_RegistarDataDisponibilidade_RegistarIdRestaurante_RegistarIdPratoDoDia",
                table: "Selecionar_P_Favoritos");

            migrationBuilder.DropIndex(
                name: "IX_Selecionar_P_Favoritos_RegistarDataDisponibilidade_RegistarIdRestaurante_RegistarIdPratoDoDia",
                table: "Selecionar_P_Favoritos");

            migrationBuilder.DropColumn(
                name: "RegistarDataDisponibilidade",
                table: "Selecionar_P_Favoritos");

            migrationBuilder.DropColumn(
                name: "RegistarIdPratoDoDia",
                table: "Selecionar_P_Favoritos");

            migrationBuilder.DropColumn(
                name: "RegistarIdRestaurante",
                table: "Selecionar_P_Favoritos");
        }
    }
}
