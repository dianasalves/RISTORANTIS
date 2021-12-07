using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RISTORANTIS.Migrations
{
    public partial class FirstStep : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            //    name: "Administrador",
            //    columns: table => new
            //    {
            //        ID_Administrador = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Username = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
            //        Password = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
            //        Email = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
            //        Nome = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
            //        ID_CRIADOR = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK__Administ__2D89616F71E22291", x => x.ID_Administrador);
            //        table.ForeignKey(
            //            name: "FK__Administr__ID_CR__239E4DCF",
            //            column: x => x.ID_CRIADOR,
            //            principalTable: "Administrador",
            //            principalColumn: "ID_Administrador",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Tipo_PratoDoDia",
            //    columns: table => new
            //    {
            //        ID_Tipo_P = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Nome_tipo_P = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK__Tipo_Pra__42F05C532000F47C", x => x.ID_Tipo_P);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Tipo_Servico",
            //    columns: table => new
            //    {
            //        ID_Servico = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Nome_Tipo_S = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK__Tipo_Ser__8C3D4AF95DE75645", x => x.ID_Servico);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Utilizador",
            //    columns: table => new
            //    {
            //        ID_Utilizador = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Nome = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
            //        Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
            //        Username = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
            //        Password = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
            //        Estado = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK__Utilizad__020698178D8BACDB", x => x.ID_Utilizador);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "PratoDoDia",
            //    columns: table => new
            //    {
            //        ID_Prato = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Nome = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
            //        Tipo = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK__PratoDoD__7B3DC2D2E42F2E03", x => x.ID_Prato);
            //        table.ForeignKey(
            //            name: "FK__PratoDoDia__Tipo__33D4B598",
            //            column: x => x.Tipo,
            //            principalTable: "Tipo_PratoDoDia",
            //            principalColumn: "ID_Tipo_P",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Bloquear",
            //    columns: table => new
            //    {
            //        Data_Bloquear = table.Column<DateTime>(type: "date", nullable: false, defaultValueSql: "(getdate())"),
            //        ID_Administrador = table.Column<int>(type: "int", nullable: false),
            //        ID_Utilizador = table.Column<int>(type: "int", nullable: false),
            //        Motivo_Bloqueio = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
            //        Data_Desbloqueio = table.Column<DateTime>(type: "date", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK__Bloquear__2980262E37D7DB24", x => new { x.Data_Bloquear, x.ID_Administrador, x.ID_Utilizador });
            //        table.ForeignKey(
            //            name: "FK__Bloquear__ID_Adm__4CA06362",
            //            column: x => x.ID_Administrador,
            //            principalTable: "Administrador",
            //            principalColumn: "ID_Administrador",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "FK__Bloquear__ID_Uti__4D94879B",
            //            column: x => x.ID_Utilizador,
            //            principalTable: "Utilizador",
            //            principalColumn: "ID_Utilizador",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Cliente",
            //    columns: table => new
            //    {
            //        ID_Cliente = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK__Cliente__E005FBFF7C1CDF55", x => x.ID_Cliente);
            //        table.ForeignKey(
            //            name: "FK__Cliente__ID_Clie__2A4B4B5E",
            //            column: x => x.ID_Cliente,
            //            principalTable: "Utilizador",
            //            principalColumn: "ID_Utilizador",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Restaurante",
            //    columns: table => new
            //    {
            //        ID_Restaurante = table.Column<int>(type: "int", nullable: false),
            //        Telefone = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: false),
            //        Localizacao_GPS = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
            //        Endereco_Codigo_Postal = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
            //        Endereco_Morada = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
            //        Endereco_Localidade = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
            //        Horario = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
            //        Fotografia = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
            //        Dia_Descanso = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK__Restaura__E5F26F712CE18D98", x => x.ID_Restaurante);
            //        table.ForeignKey(
            //            name: "FK__Restauran__ID_Re__2F10007B",
            //            column: x => x.ID_Restaurante,
            //            principalTable: "Utilizador",
            //            principalColumn: "ID_Utilizador",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Selecionar_P_Favoritos",
            //    columns: table => new
            //    {
            //        ID_Cliente = table.Column<int>(type: "int", nullable: false),
            //        ID_Prato = table.Column<int>(type: "int", nullable: false),
            //        Notificacao_P = table.Column<bool>(type: "bit", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK__Selecion__D7B627D2ECE677B5", x => new { x.ID_Cliente, x.ID_Prato });
            //        table.ForeignKey(
            //            name: "FK__Seleciona__ID_Cl__47DBAE45",
            //            column: x => x.ID_Cliente,
            //            principalTable: "Cliente",
            //            principalColumn: "ID_Cliente",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "FK__Seleciona__ID_Pr__48CFD27E",
            //            column: x => x.ID_Prato,
            //            principalTable: "PratoDoDia",
            //            principalColumn: "ID_Prato",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Pedir_Registo",
            //    columns: table => new
            //    {
            //        ID_Pedir_Registo = table.Column<int>(type: "int", nullable: false),
            //        Data_Pedido = table.Column<DateTime>(type: "date", nullable: false, defaultValueSql: "(getdate())"),
            //        Data_Aceitacao = table.Column<DateTime>(type: "date", nullable: true),
            //        Resultado = table.Column<bool>(type: "bit", nullable: false),
            //        Motivo_Rejeicao = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
            //        ID_Restaurante = table.Column<int>(type: "int", nullable: false),
            //        ID_Administrador = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK__Pedir_Re__51F07872961CD5AC", x => x.ID_Pedir_Registo);
            //        table.ForeignKey(
            //            name: "FK__Pedir_Reg__ID_Ad__412EB0B6",
            //            column: x => x.ID_Administrador,
            //            principalTable: "Administrador",
            //            principalColumn: "ID_Administrador",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "FK__Pedir_Reg__ID_Re__403A8C7D",
            //            column: x => x.ID_Restaurante,
            //            principalTable: "Restaurante",
            //            principalColumn: "ID_Restaurante",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Registar",
            //    columns: table => new
            //    {
            //        Data_Disponibilidade = table.Column<DateTime>(type: "date", nullable: false),
            //        ID_Restaurante = table.Column<int>(type: "int", nullable: false),
            //        ID_PratoDoDia = table.Column<int>(type: "int", nullable: false),
            //        Fotografia = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true),
            //        Descricao = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
            //        Preco = table.Column<decimal>(type: "money", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK__Registar__F9938641C4E53C64", x => new { x.Data_Disponibilidade, x.ID_Restaurante, x.ID_PratoDoDia });
            //        table.ForeignKey(
            //            name: "FK__Registar__ID_Pra__3C69FB99",
            //            column: x => x.ID_PratoDoDia,
            //            principalTable: "PratoDoDia",
            //            principalColumn: "ID_Prato",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "FK__Registar__ID_Res__3B75D760",
            //            column: x => x.ID_Restaurante,
            //            principalTable: "Restaurante",
            //            principalColumn: "ID_Restaurante",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Selecionar_R_Favoritos",
            //    columns: table => new
            //    {
            //        ID_Cliente = table.Column<int>(type: "int", nullable: false),
            //        ID_Restaurante = table.Column<int>(type: "int", nullable: false),
            //        Notificacao_R = table.Column<bool>(type: "bit", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK__Selecion__EE5ADD08C4B4B4DC", x => new { x.ID_Cliente, x.ID_Restaurante });
            //        table.ForeignKey(
            //            name: "FK__Seleciona__ID_Cl__440B1D61",
            //            column: x => x.ID_Cliente,
            //            principalTable: "Cliente",
            //            principalColumn: "ID_Cliente",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "FK__Seleciona__ID_Re__44FF419A",
            //            column: x => x.ID_Restaurante,
            //            principalTable: "Restaurante",
            //            principalColumn: "ID_Restaurante",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Selecionar_Servico",
            //    columns: table => new
            //    {
            //        ID_Servico = table.Column<int>(type: "int", nullable: false),
            //        ID_Restaurante = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.ForeignKey(
            //            name: "FK__Seleciona__ID_Re__38996AB5",
            //            column: x => x.ID_Restaurante,
            //            principalTable: "Restaurante",
            //            principalColumn: "ID_Restaurante",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "FK__Seleciona__ID_Se__37A5467C",
            //            column: x => x.ID_Servico,
            //            principalTable: "Tipo_Servico",
            //            principalColumn: "ID_Servico",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateIndex(
            //    name: "IX_Administrador_ID_CRIADOR",
            //    table: "Administrador",
            //    column: "ID_CRIADOR");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Bloquear_ID_Administrador",
            //    table: "Bloquear",
            //    column: "ID_Administrador");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Bloquear_ID_Utilizador",
            //    table: "Bloquear",
            //    column: "ID_Utilizador");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Pedir_Registo_ID_Administrador",
            //    table: "Pedir_Registo",
            //    column: "ID_Administrador");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Pedir_Registo_ID_Restaurante",
            //    table: "Pedir_Registo",
            //    column: "ID_Restaurante");

            //migrationBuilder.CreateIndex(
            //    name: "IX_PratoDoDia_Tipo",
            //    table: "PratoDoDia",
            //    column: "Tipo");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Registar_ID_PratoDoDia",
            //    table: "Registar",
            //    column: "ID_PratoDoDia");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Registar_ID_Restaurante",
            //    table: "Registar",
            //    column: "ID_Restaurante");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Selecionar_P_Favoritos_ID_Prato",
            //    table: "Selecionar_P_Favoritos",
            //    column: "ID_Prato");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Selecionar_R_Favoritos_ID_Restaurante",
            //    table: "Selecionar_R_Favoritos",
            //    column: "ID_Restaurante");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Selecionar_Servico_ID_Restaurante",
            //    table: "Selecionar_Servico",
            //    column: "ID_Restaurante");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Selecionar_Servico_ID_Servico",
            //    table: "Selecionar_Servico",
            //    column: "ID_Servico");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "Bloquear");

            //migrationBuilder.DropTable(
            //    name: "Pedir_Registo");

            //migrationBuilder.DropTable(
            //    name: "Registar");

            //migrationBuilder.DropTable(
            //    name: "Selecionar_P_Favoritos");

            //migrationBuilder.DropTable(
            //    name: "Selecionar_R_Favoritos");

            //migrationBuilder.DropTable(
            //    name: "Selecionar_Servico");

            //migrationBuilder.DropTable(
            //    name: "Administrador");

            //migrationBuilder.DropTable(
            //    name: "PratoDoDia");

            //migrationBuilder.DropTable(
            //    name: "Cliente");

            //migrationBuilder.DropTable(
            //    name: "Restaurante");

            //migrationBuilder.DropTable(
            //    name: "Tipo_Servico");

            //migrationBuilder.DropTable(
            //    name: "Tipo_PratoDoDia");

            //migrationBuilder.DropTable(
            //    name: "Utilizador");
        }
    }
}
