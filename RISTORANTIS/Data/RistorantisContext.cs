using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using RISTORANTIS.Models;

#nullable disable

namespace RISTORANTIS.Data
{
    public partial class RistorantisContext : DbContext
    {
        public RistorantisContext()
        {
        }

        public RistorantisContext(DbContextOptions<RistorantisContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Administrador> Administradors { get; set; }
        public virtual DbSet<Bloquear> Bloquears { get; set; }
        public virtual DbSet<Cliente> Clientes { get; set; }
        public virtual DbSet<PedirRegisto> PedirRegistos { get; set; }
        public virtual DbSet<PratoDoDium> PratoDoDia { get; set; }
        public virtual DbSet<Registar> Registars { get; set; }
        public virtual DbSet<Restaurante> Restaurantes { get; set; }
        public virtual DbSet<SelecionarPFavorito> SelecionarPFavoritos { get; set; }
        public virtual DbSet<SelecionarRFavorito> SelecionarRFavoritos { get; set; }
        public virtual DbSet<SelecionarServico> SelecionarServicos { get; set; }
        public virtual DbSet<TipoPratoDoDium> TipoPratoDoDia { get; set; }
        public virtual DbSet<TipoServico> TipoServicos { get; set; }
        public virtual DbSet<Utilizador> Utilizadors { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("name=RistorantisContext");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Administrador>(entity =>
            {
                entity.HasKey(e => e.IdAdministrador)
                    .HasName("PK__Administ__2D89616F71E22291");

                entity.HasOne(d => d.IdCriadorNavigation)
                    .WithMany(p => p.InverseIdCriadorNavigation)
                    .HasForeignKey(d => d.IdCriador)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Administr__ID_CR__239E4DCF");
            });

            modelBuilder.Entity<Bloquear>(entity =>
            {
                entity.HasKey(e => new { e.DataBloquear, e.IdAdministrador, e.IdUtilizador })
                    .HasName("PK__Bloquear__2980262E37D7DB24");

                entity.Property(e => e.DataBloquear).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.IdAdministradorNavigation)
                    .WithMany(p => p.Bloquears)
                    .HasForeignKey(d => d.IdAdministrador)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Bloquear__ID_Adm__4CA06362");

                entity.HasOne(d => d.IdUtilizadorNavigation)
                    .WithMany(p => p.Bloquears)
                    .HasForeignKey(d => d.IdUtilizador)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Bloquear__ID_Uti__4D94879B");
            });

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.IdCliente)
                    .HasName("PK__Cliente__E005FBFF7C1CDF55");

                entity.Property(e => e.IdCliente).ValueGeneratedNever();

                entity.HasOne(d => d.IdClienteNavigation)
                    .WithOne(p => p.Cliente)
                    .HasForeignKey<Cliente>(d => d.IdCliente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Cliente__ID_Clie__2A4B4B5E");
            });

            modelBuilder.Entity<PedirRegisto>(entity =>
            {
                entity.HasKey(e => e.IdPedirRegisto)
                    .HasName("PK__Pedir_Re__51F07872961CD5AC");

                entity.Property(e => e.IdPedirRegisto).ValueGeneratedNever();

                entity.Property(e => e.DataPedido).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.IdAdministradorNavigation)
                    .WithMany(p => p.PedirRegistos)
                    .HasForeignKey(d => d.IdAdministrador)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Pedir_Reg__ID_Ad__412EB0B6");

                entity.HasOne(d => d.IdRestauranteNavigation)
                    .WithMany(p => p.PedirRegistos)
                    .HasForeignKey(d => d.IdRestaurante)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Pedir_Reg__ID_Re__403A8C7D");
            });

            modelBuilder.Entity<PratoDoDium>(entity =>
            {
                entity.HasKey(e => e.IdPrato)
                    .HasName("PK__PratoDoD__7B3DC2D2E42F2E03");

                entity.HasOne(d => d.TipoNavigation)
                    .WithMany(p => p.PratoDoDia)
                    .HasForeignKey(d => d.Tipo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PratoDoDia__Tipo__33D4B598");
            });

            modelBuilder.Entity<Registar>(entity =>
            {
                entity.HasKey(e => new { e.DataDisponibilidade, e.IdRestaurante, e.IdPratoDoDia })
                    .HasName("PK__Registar__F9938641C4E53C64");

                entity.HasOne(d => d.IdPratoDoDiaNavigation)
                    .WithMany(p => p.Registars)
                    .HasForeignKey(d => d.IdPratoDoDia)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Registar__ID_Pra__3C69FB99");

                entity.HasOne(d => d.IdRestauranteNavigation)
                    .WithMany(p => p.Registars)
                    .HasForeignKey(d => d.IdRestaurante)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Registar__ID_Res__3B75D760");
            });

            modelBuilder.Entity<Restaurante>(entity =>
            {
                entity.HasKey(e => e.IdRestaurante)
                    .HasName("PK__Restaura__E5F26F712CE18D98");

                entity.Property(e => e.IdRestaurante).ValueGeneratedNever();

                entity.HasOne(d => d.IdRestauranteNavigation)
                    .WithOne(p => p.Restaurante)
                    .HasForeignKey<Restaurante>(d => d.IdRestaurante)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Restauran__ID_Re__2F10007B");
            });

            modelBuilder.Entity<SelecionarPFavorito>(entity =>
            {
                entity.HasKey(e => new { e.IdCliente, e.IdPrato })
                    .HasName("PK__Selecion__D7B627D2ECE677B5");

                entity.HasOne(d => d.IdClienteNavigation)
                    .WithMany(p => p.SelecionarPFavoritos)
                    .HasForeignKey(d => d.IdCliente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Seleciona__ID_Cl__47DBAE45");

                entity.HasOne(d => d.IdPratoNavigation)
                    .WithMany(p => p.SelecionarPFavoritos)
                    .HasForeignKey(d => d.IdPrato)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Seleciona__ID_Pr__48CFD27E");
            });

            modelBuilder.Entity<SelecionarRFavorito>(entity =>
            {
                entity.HasKey(e => new { e.IdCliente, e.IdRestaurante })
                    .HasName("PK__Selecion__EE5ADD08C4B4B4DC");

                entity.HasOne(d => d.IdClienteNavigation)
                    .WithMany(p => p.SelecionarRFavoritos)
                    .HasForeignKey(d => d.IdCliente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Seleciona__ID_Cl__440B1D61");

                entity.HasOne(d => d.IdRestauranteNavigation)
                    .WithMany(p => p.SelecionarRFavoritos)
                    .HasForeignKey(d => d.IdRestaurante)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Seleciona__ID_Re__44FF419A");
            });

            modelBuilder.Entity<SelecionarServico>(entity =>
            {
                //entity.HasNoKey();
                entity.HasKey(e=> new { e.ID_SelectServico})
                    .HasName("PK__tmp_ms_x__A50489DDF1E46E6F");
                entity.HasOne(d => d.IdRestauranteNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdRestaurante)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Seleciona__ID_Re__38996AB5");

                entity.HasOne(d => d.IdServicoNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdServico)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Seleciona__ID_Se__37A5467C");
            });

            modelBuilder.Entity<TipoPratoDoDium>(entity =>
            {
                entity.HasKey(e => e.IdTipoP)
                    .HasName("PK__Tipo_Pra__42F05C532000F47C");
            });

            modelBuilder.Entity<TipoServico>(entity =>
            {
                entity.HasKey(e => e.IdServico)
                    .HasName("PK__Tipo_Ser__8C3D4AF95DE75645");
            });

            modelBuilder.Entity<Utilizador>(entity =>
            {
                entity.HasKey(e => e.IdUtilizador)
                    .HasName("PK__Utilizad__020698178D8BACDB");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
