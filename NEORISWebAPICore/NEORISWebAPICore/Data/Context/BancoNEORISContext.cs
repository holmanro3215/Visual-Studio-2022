using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using NEORISWebAPICore.Data.Models;

namespace NEORISWebAPICore.Data.Context
{
    public partial class BancoNEORISContext : DbContext
    {
        public BancoNEORISContext()
        {
        }

        public BancoNEORISContext(DbContextOptions<BancoNEORISContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cliente> Clientes { get; set; } = null!;
        public virtual DbSet<Cuenta> Cuentas { get; set; } = null!;
        public virtual DbSet<Genero> Generos { get; set; } = null!;
        public virtual DbSet<Movimiento> Movimientos { get; set; } = null!;
        public virtual DbSet<Persona> Personas { get; set; } = null!;
        public virtual DbSet<TipoCuenta> TipoCuentas { get; set; } = null!;
        public virtual DbSet<TipoMovimiento> TipoMovimientos { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-ULJD9IP;Database=BancoNEORIS;user=sa;password=1q2w3e;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.IdCliente);

                entity.Property(e => e.Contrasena).HasMaxLength(50);

                entity.HasOne(d => d.IdPersonaNavigation)
                    .WithMany(p => p.Clientes)
                    .HasForeignKey(d => d.IdPersona)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Clientes_Personas");
            });

            modelBuilder.Entity<Cuenta>(entity =>
            {
                entity.HasKey(e => e.IdCuenta);

                entity.Property(e => e.NumeroCuenta).HasMaxLength(50);

                entity.HasOne(d => d.IdClienteNavigation)
                    .WithMany(p => p.Cuenta)
                    .HasForeignKey(d => d.IdCliente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Cuentas_Clientes");

                entity.HasOne(d => d.IdTipoCuentaNavigation)
                    .WithMany(p => p.Cuenta)
                    .HasForeignKey(d => d.IdTipoCuenta)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Cuentas_TipoCuentas");
            });

            modelBuilder.Entity<Genero>(entity =>
            {
                entity.HasKey(e => e.IdGenero);

                entity.Property(e => e.Genero1)
                    .HasMaxLength(20)
                    .HasColumnName("Genero");
            });

            modelBuilder.Entity<Movimiento>(entity =>
            {
                entity.HasKey(e => e.IdMovimiento);

                entity.Property(e => e.Fecha).HasColumnType("date");

                entity.HasOne(d => d.IdCuentaNavigation)
                    .WithMany(p => p.Movimientos)
                    .HasForeignKey(d => d.IdCuenta)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Movimientos_Cuentas");

                entity.HasOne(d => d.IdTipoMovimientoNavigation)
                    .WithMany(p => p.Movimientos)
                    .HasForeignKey(d => d.IdTipoMovimiento)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Movimientos_TipoMovimientos");
            });

            modelBuilder.Entity<Persona>(entity =>
            {
                entity.HasKey(e => e.IdPersona);

                entity.Property(e => e.Direccion).HasMaxLength(100);

                entity.Property(e => e.Identificacion).HasMaxLength(50);

                entity.Property(e => e.Nombre).HasMaxLength(100);

                entity.Property(e => e.Telefono).HasMaxLength(20);

                entity.HasOne(d => d.IdGeneroNavigation)
                    .WithMany(p => p.Personas)
                    .HasForeignKey(d => d.IdGenero)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Personas_Generos");
            });

            modelBuilder.Entity<TipoCuenta>(entity =>
            {
                entity.HasKey(e => e.IdTipoCuenta);

                entity.Property(e => e.TipoCuenta1)
                    .HasMaxLength(20)
                    .HasColumnName("TipoCuenta");
            });

            modelBuilder.Entity<TipoMovimiento>(entity =>
            {
                entity.HasKey(e => e.IdTipoMovimiento);

                entity.Property(e => e.TipoMovimiento1)
                    .HasMaxLength(20)
                    .HasColumnName("TipoMovimiento");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
