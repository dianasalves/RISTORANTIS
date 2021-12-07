﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RISTORANTIS.Data;

namespace RISTORANTIS.Migrations
{
    [DbContext(typeof(RistorantisContext))]
    [Migration("20201228183841_FirstStep")]
    partial class FirstStep
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("RISTORANTIS.Models.Administrador", b =>
                {
                    b.Property<int>("IdAdministrador")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID_Administrador")
                        .UseIdentityColumn();

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(1)
                        .HasColumnType("nvarchar(1)");

                    b.Property<int>("IdCriador")
                        .HasColumnType("int")
                        .HasColumnName("ID_CRIADOR");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("IdAdministrador")
                        .HasName("PK__Administ__2D89616F71E22291");

                    b.HasIndex("IdCriador");

                    b.ToTable("Administrador");
                });

            modelBuilder.Entity("RISTORANTIS.Models.Bloquear", b =>
                {
                    b.Property<DateTime>("DataBloquear")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("date")
                        .HasColumnName("Data_Bloquear")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<int>("IdAdministrador")
                        .HasColumnType("int")
                        .HasColumnName("ID_Administrador");

                    b.Property<int>("IdUtilizador")
                        .HasColumnType("int")
                        .HasColumnName("ID_Utilizador");

                    b.Property<DateTime?>("DataDesbloqueio")
                        .HasColumnType("date")
                        .HasColumnName("Data_Desbloqueio");

                    b.Property<string>("MotivoBloqueio")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("Motivo_Bloqueio");

                    b.HasKey("DataBloquear", "IdAdministrador", "IdUtilizador")
                        .HasName("PK__Bloquear__2980262E37D7DB24");

                    b.HasIndex("IdAdministrador");

                    b.HasIndex("IdUtilizador");

                    b.ToTable("Bloquear");
                });

            modelBuilder.Entity("RISTORANTIS.Models.Cliente", b =>
                {
                    b.Property<int>("IdCliente")
                        .HasColumnType("int")
                        .HasColumnName("ID_Cliente");

                    b.HasKey("IdCliente")
                        .HasName("PK__Cliente__E005FBFF7C1CDF55");

                    b.ToTable("Cliente");
                });

            modelBuilder.Entity("RISTORANTIS.Models.PedirRegisto", b =>
                {
                    b.Property<int>("IdPedirRegisto")
                        .HasColumnType("int")
                        .HasColumnName("ID_Pedir_Registo");

                    b.Property<DateTime?>("DataAceitacao")
                        .HasColumnType("date")
                        .HasColumnName("Data_Aceitacao");

                    b.Property<DateTime>("DataPedido")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("date")
                        .HasColumnName("Data_Pedido")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<int>("IdAdministrador")
                        .HasColumnType("int")
                        .HasColumnName("ID_Administrador");

                    b.Property<int>("IdRestaurante")
                        .HasColumnType("int")
                        .HasColumnName("ID_Restaurante");

                    b.Property<string>("MotivoRejeicao")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("Motivo_Rejeicao");

                    b.Property<bool>("Resultado")
                        .HasColumnType("bit");

                    b.HasKey("IdPedirRegisto")
                        .HasName("PK__Pedir_Re__51F07872961CD5AC");

                    b.HasIndex("IdAdministrador");

                    b.HasIndex("IdRestaurante");

                    b.ToTable("Pedir_Registo");
                });

            modelBuilder.Entity("RISTORANTIS.Models.PratoDoDium", b =>
                {
                    b.Property<int>("IdPrato")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID_Prato")
                        .UseIdentityColumn();

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Tipo")
                        .HasColumnType("int");

                    b.HasKey("IdPrato")
                        .HasName("PK__PratoDoD__7B3DC2D2E42F2E03");

                    b.HasIndex("Tipo");

                    b.ToTable("PratoDoDia");
                });

            modelBuilder.Entity("RISTORANTIS.Models.Registar", b =>
                {
                    b.Property<DateTime>("DataDisponibilidade")
                        .HasColumnType("date")
                        .HasColumnName("Data_Disponibilidade");

                    b.Property<int>("IdRestaurante")
                        .HasColumnType("int")
                        .HasColumnName("ID_Restaurante");

                    b.Property<int>("IdPratoDoDia")
                        .HasColumnType("int")
                        .HasColumnName("ID_PratoDoDia");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Fotografia")
                        .HasMaxLength(1)
                        .HasColumnType("nvarchar(1)");

                    b.Property<decimal>("Preco")
                        .HasColumnType("money");

                    b.HasKey("DataDisponibilidade", "IdRestaurante", "IdPratoDoDia")
                        .HasName("PK__Registar__F9938641C4E53C64");

                    b.HasIndex("IdPratoDoDia");

                    b.HasIndex("IdRestaurante");

                    b.ToTable("Registar");
                });

            modelBuilder.Entity("RISTORANTIS.Models.Restaurante", b =>
                {
                    b.Property<int>("IdRestaurante")
                        .HasColumnType("int")
                        .HasColumnName("ID_Restaurante");

                    b.Property<string>("DiaDescanso")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("Dia_Descanso");

                    b.Property<string>("EnderecoCodigoPostal")
                        .IsRequired()
                        .HasMaxLength(8)
                        .HasColumnType("nvarchar(8)")
                        .HasColumnName("Endereco_Codigo_Postal");

                    b.Property<string>("EnderecoLocalidade")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("Endereco_Localidade");

                    b.Property<string>("EnderecoMorada")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("Endereco_Morada");

                    b.Property<string>("Fotografia")
                        .IsRequired()
                        .HasMaxLength(1)
                        .HasColumnType("nvarchar(1)");

                    b.Property<string>("Horario")
                        .IsRequired()
                        .HasMaxLength(1)
                        .HasColumnType("nvarchar(1)");

                    b.Property<string>("LocalizacaoGps")
                        .IsRequired()
                        .HasMaxLength(1)
                        .HasColumnType("nvarchar(1)")
                        .HasColumnName("Localizacao_GPS");

                    b.Property<string>("Telefone")
                        .IsRequired()
                        .HasMaxLength(9)
                        .HasColumnType("nvarchar(9)");

                    b.HasKey("IdRestaurante")
                        .HasName("PK__Restaura__E5F26F712CE18D98");

                    b.ToTable("Restaurante");
                });

            modelBuilder.Entity("RISTORANTIS.Models.SelecionarPFavorito", b =>
                {
                    b.Property<int>("IdCliente")
                        .HasColumnType("int")
                        .HasColumnName("ID_Cliente");

                    b.Property<int>("IdPrato")
                        .HasColumnType("int")
                        .HasColumnName("ID_Prato");

                    b.Property<bool>("NotificacaoP")
                        .HasColumnType("bit")
                        .HasColumnName("Notificacao_P");

                    b.HasKey("IdCliente", "IdPrato")
                        .HasName("PK__Selecion__D7B627D2ECE677B5");

                    b.HasIndex("IdPrato");

                    b.ToTable("Selecionar_P_Favoritos");
                });

            modelBuilder.Entity("RISTORANTIS.Models.SelecionarRFavorito", b =>
                {
                    b.Property<int>("IdCliente")
                        .HasColumnType("int")
                        .HasColumnName("ID_Cliente");

                    b.Property<int>("IdRestaurante")
                        .HasColumnType("int")
                        .HasColumnName("ID_Restaurante");

                    b.Property<bool>("NotificacaoR")
                        .HasColumnType("bit")
                        .HasColumnName("Notificacao_R");

                    b.HasKey("IdCliente", "IdRestaurante")
                        .HasName("PK__Selecion__EE5ADD08C4B4B4DC");

                    b.HasIndex("IdRestaurante");

                    b.ToTable("Selecionar_R_Favoritos");
                });

            modelBuilder.Entity("RISTORANTIS.Models.SelecionarServico", b =>
                {
                    b.Property<int>("IdRestaurante")
                        .HasColumnType("int")
                        .HasColumnName("ID_Restaurante");

                    b.Property<int>("IdServico")
                        .HasColumnType("int")
                        .HasColumnName("ID_Servico");

                    b.HasIndex("IdRestaurante");

                    b.HasIndex("IdServico");

                    b.ToTable("Selecionar_Servico");
                });

            modelBuilder.Entity("RISTORANTIS.Models.TipoPratoDoDium", b =>
                {
                    b.Property<int>("IdTipoP")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID_Tipo_P")
                        .UseIdentityColumn();

                    b.Property<string>("NomeTipoP")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("Nome_tipo_P");

                    b.HasKey("IdTipoP")
                        .HasName("PK__Tipo_Pra__42F05C532000F47C");

                    b.ToTable("Tipo_PratoDoDia");
                });

            modelBuilder.Entity("RISTORANTIS.Models.TipoServico", b =>
                {
                    b.Property<int>("IdServico")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID_Servico")
                        .UseIdentityColumn();

                    b.Property<string>("NomeTipoS")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("Nome_Tipo_S");

                    b.HasKey("IdServico")
                        .HasName("PK__Tipo_Ser__8C3D4AF95DE75645");

                    b.ToTable("Tipo_Servico");
                });

            modelBuilder.Entity("RISTORANTIS.Models.Utilizador", b =>
                {
                    b.Property<int>("IdUtilizador")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID_Utilizador")
                        .UseIdentityColumn();

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Estado")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("IdUtilizador")
                        .HasName("PK__Utilizad__020698178D8BACDB");

                    b.ToTable("Utilizador");
                });

            modelBuilder.Entity("RISTORANTIS.Models.Administrador", b =>
                {
                    b.HasOne("RISTORANTIS.Models.Administrador", "IdCriadorNavigation")
                        .WithMany("InverseIdCriadorNavigation")
                        .HasForeignKey("IdCriador")
                        .HasConstraintName("FK__Administr__ID_CR__239E4DCF")
                        .IsRequired();

                    b.Navigation("IdCriadorNavigation");
                });

            modelBuilder.Entity("RISTORANTIS.Models.Bloquear", b =>
                {
                    b.HasOne("RISTORANTIS.Models.Administrador", "IdAdministradorNavigation")
                        .WithMany("Bloquears")
                        .HasForeignKey("IdAdministrador")
                        .HasConstraintName("FK__Bloquear__ID_Adm__4CA06362")
                        .IsRequired();

                    b.HasOne("RISTORANTIS.Models.Utilizador", "IdUtilizadorNavigation")
                        .WithMany("Bloquears")
                        .HasForeignKey("IdUtilizador")
                        .HasConstraintName("FK__Bloquear__ID_Uti__4D94879B")
                        .IsRequired();

                    b.Navigation("IdAdministradorNavigation");

                    b.Navigation("IdUtilizadorNavigation");
                });

            modelBuilder.Entity("RISTORANTIS.Models.Cliente", b =>
                {
                    b.HasOne("RISTORANTIS.Models.Utilizador", "IdClienteNavigation")
                        .WithOne("Cliente")
                        .HasForeignKey("RISTORANTIS.Models.Cliente", "IdCliente")
                        .HasConstraintName("FK__Cliente__ID_Clie__2A4B4B5E")
                        .IsRequired();

                    b.Navigation("IdClienteNavigation");
                });

            modelBuilder.Entity("RISTORANTIS.Models.PedirRegisto", b =>
                {
                    b.HasOne("RISTORANTIS.Models.Administrador", "IdAdministradorNavigation")
                        .WithMany("PedirRegistos")
                        .HasForeignKey("IdAdministrador")
                        .HasConstraintName("FK__Pedir_Reg__ID_Ad__412EB0B6")
                        .IsRequired();

                    b.HasOne("RISTORANTIS.Models.Restaurante", "IdRestauranteNavigation")
                        .WithMany("PedirRegistos")
                        .HasForeignKey("IdRestaurante")
                        .HasConstraintName("FK__Pedir_Reg__ID_Re__403A8C7D")
                        .IsRequired();

                    b.Navigation("IdAdministradorNavigation");

                    b.Navigation("IdRestauranteNavigation");
                });

            modelBuilder.Entity("RISTORANTIS.Models.PratoDoDium", b =>
                {
                    b.HasOne("RISTORANTIS.Models.TipoPratoDoDium", "TipoNavigation")
                        .WithMany("PratoDoDia")
                        .HasForeignKey("Tipo")
                        .HasConstraintName("FK__PratoDoDia__Tipo__33D4B598")
                        .IsRequired();

                    b.Navigation("TipoNavigation");
                });

            modelBuilder.Entity("RISTORANTIS.Models.Registar", b =>
                {
                    b.HasOne("RISTORANTIS.Models.PratoDoDium", "IdPratoDoDiaNavigation")
                        .WithMany("Registars")
                        .HasForeignKey("IdPratoDoDia")
                        .HasConstraintName("FK__Registar__ID_Pra__3C69FB99")
                        .IsRequired();

                    b.HasOne("RISTORANTIS.Models.Restaurante", "IdRestauranteNavigation")
                        .WithMany("Registars")
                        .HasForeignKey("IdRestaurante")
                        .HasConstraintName("FK__Registar__ID_Res__3B75D760")
                        .IsRequired();

                    b.Navigation("IdPratoDoDiaNavigation");

                    b.Navigation("IdRestauranteNavigation");
                });

            modelBuilder.Entity("RISTORANTIS.Models.Restaurante", b =>
                {
                    b.HasOne("RISTORANTIS.Models.Utilizador", "IdRestauranteNavigation")
                        .WithOne("Restaurante")
                        .HasForeignKey("RISTORANTIS.Models.Restaurante", "IdRestaurante")
                        .HasConstraintName("FK__Restauran__ID_Re__2F10007B")
                        .IsRequired();

                    b.Navigation("IdRestauranteNavigation");
                });

            modelBuilder.Entity("RISTORANTIS.Models.SelecionarPFavorito", b =>
                {
                    b.HasOne("RISTORANTIS.Models.Cliente", "IdClienteNavigation")
                        .WithMany("SelecionarPFavoritos")
                        .HasForeignKey("IdCliente")
                        .HasConstraintName("FK__Seleciona__ID_Cl__47DBAE45")
                        .IsRequired();

                    b.HasOne("RISTORANTIS.Models.PratoDoDium", "IdPratoNavigation")
                        .WithMany("SelecionarPFavoritos")
                        .HasForeignKey("IdPrato")
                        .HasConstraintName("FK__Seleciona__ID_Pr__48CFD27E")
                        .IsRequired();

                    b.Navigation("IdClienteNavigation");

                    b.Navigation("IdPratoNavigation");
                });

            modelBuilder.Entity("RISTORANTIS.Models.SelecionarRFavorito", b =>
                {
                    b.HasOne("RISTORANTIS.Models.Cliente", "IdClienteNavigation")
                        .WithMany("SelecionarRFavoritos")
                        .HasForeignKey("IdCliente")
                        .HasConstraintName("FK__Seleciona__ID_Cl__440B1D61")
                        .IsRequired();

                    b.HasOne("RISTORANTIS.Models.Restaurante", "IdRestauranteNavigation")
                        .WithMany("SelecionarRFavoritos")
                        .HasForeignKey("IdRestaurante")
                        .HasConstraintName("FK__Seleciona__ID_Re__44FF419A")
                        .IsRequired();

                    b.Navigation("IdClienteNavigation");

                    b.Navigation("IdRestauranteNavigation");
                });

            modelBuilder.Entity("RISTORANTIS.Models.SelecionarServico", b =>
                {
                    b.HasOne("RISTORANTIS.Models.Restaurante", "IdRestauranteNavigation")
                        .WithMany()
                        .HasForeignKey("IdRestaurante")
                        .HasConstraintName("FK__Seleciona__ID_Re__38996AB5")
                        .IsRequired();

                    b.HasOne("RISTORANTIS.Models.TipoServico", "IdServicoNavigation")
                        .WithMany()
                        .HasForeignKey("IdServico")
                        .HasConstraintName("FK__Seleciona__ID_Se__37A5467C")
                        .IsRequired();

                    b.Navigation("IdRestauranteNavigation");

                    b.Navigation("IdServicoNavigation");
                });

            modelBuilder.Entity("RISTORANTIS.Models.Administrador", b =>
                {
                    b.Navigation("Bloquears");

                    b.Navigation("InverseIdCriadorNavigation");

                    b.Navigation("PedirRegistos");
                });

            modelBuilder.Entity("RISTORANTIS.Models.Cliente", b =>
                {
                    b.Navigation("SelecionarPFavoritos");

                    b.Navigation("SelecionarRFavoritos");
                });

            modelBuilder.Entity("RISTORANTIS.Models.PratoDoDium", b =>
                {
                    b.Navigation("Registars");

                    b.Navigation("SelecionarPFavoritos");
                });

            modelBuilder.Entity("RISTORANTIS.Models.Restaurante", b =>
                {
                    b.Navigation("PedirRegistos");

                    b.Navigation("Registars");

                    b.Navigation("SelecionarRFavoritos");
                });

            modelBuilder.Entity("RISTORANTIS.Models.TipoPratoDoDium", b =>
                {
                    b.Navigation("PratoDoDia");
                });

            modelBuilder.Entity("RISTORANTIS.Models.Utilizador", b =>
                {
                    b.Navigation("Bloquears");

                    b.Navigation("Cliente");

                    b.Navigation("Restaurante");
                });
#pragma warning restore 612, 618
        }
    }
}
