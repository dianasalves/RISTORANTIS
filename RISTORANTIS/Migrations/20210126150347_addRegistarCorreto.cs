using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RISTORANTIS.Migrations
{
    public partial class addRegistarCorreto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Selecionar_P_Favoritos_Registar_RegistarDataDisponibilidade_RegistarIdRestaurante_RegistarIdPratoDoDia",
                table: "Selecionar_P_Favoritos");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Registar_TempId_TempId1_TempId2",
                table: "Registar");

            migrationBuilder.DropPrimaryKey(
                name: "PK__Registar__F9938641C4E53C64",
                table: "Registar");

            migrationBuilder.DropColumn(
                name: "TempId",
                table: "Registar");

            migrationBuilder.DropColumn(
                name: "TempId1",
                table: "Registar");

            migrationBuilder.DropColumn(
                name: "TempId2",
                table: "Registar");

            migrationBuilder.AlterColumn<string>(
                name: "Fotografia",
                table: "Registar",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK__Registar__F9938641C4E53C64",
                table: "Registar",
                columns: new[] { "ID_Registo", "ID_Restaurante", "ID_PratoDoDia" });

            migrationBuilder.AddForeignKey(
                name: "FK_Selecionar_P_Favoritos_Registar_RegistarID_Registo_RegistarIdRestaurante_RegistarIdPratoDoDia",
                table: "Selecionar_P_Favoritos",
                columns: new[] { "RegistarID_Registo", "RegistarIdRestaurante", "RegistarIdPratoDoDia" },
                principalTable: "Registar",
                principalColumns: new[] { "ID_Registo", "ID_Restaurante", "ID_PratoDoDia" },
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Selecionar_P_Favoritos_Registar_RegistarDataDisponibilidade_RegistarIdRestaurante_RegistarIdPratoDoDia",
                table: "Selecionar_P_Favoritos");

            migrationBuilder.DropPrimaryKey(
                name: "PK__Registar__F9938641C4E53C64",
                table: "Registar");

            migrationBuilder.AlterColumn<string>(
                name: "Fotografia",
                table: "Registar",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000);

            migrationBuilder.AddColumn<DateTime>(
                name: "TempId",
                table: "Registar",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "TempId1",
                table: "Registar",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TempId2",
                table: "Registar",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Registar_TempId_TempId1_TempId2",
                table: "Registar",
                columns: new[] { "TempId", "TempId1", "TempId2" });

            migrationBuilder.AddPrimaryKey(
                name: "PK__Registar__F9938641C4E53C64",
                table: "Registar",
                column: "ID_Registo");

            migrationBuilder.AddForeignKey(
                name: "FK_Selecionar_P_Favoritos_Registar_RegistarDataDisponibilidade_RegistarIdRestaurante_RegistarIdPratoDoDia",
                table: "Selecionar_P_Favoritos",
                columns: new[] { "RegistarDataDisponibilidade", "RegistarIdRestaurante", "RegistarIdPratoDoDia" },
                principalTable: "Registar",
                principalColumns: new[] { "TempId", "TempId1", "TempId2" },
                onDelete: ReferentialAction.Restrict);
        }
    }
}
