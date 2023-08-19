using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace GestionLogistica.Models
{
    public partial class GestionLogisticaContext : DbContext
    {
        public GestionLogisticaContext()
        {
        }

        public GestionLogisticaContext(DbContextOptions<GestionLogisticaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cliente> Clientes { get; set; } = null!;
        public virtual DbSet<Empresa> Empresas { get; set; } = null!;
        public virtual DbSet<Equipo> Equipos { get; set; } = null!;
        public virtual DbSet<Gestionenvio> Gestionenvios { get; set; } = null!;
        public virtual DbSet<Usuario> Usuarios { get; set; } = null!;
        /*
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=SF-SVASQUEZ\\SQLEXPRESS;Database=GestionLogistica;Trusted_Connection=True;TrustServerCertificate=True");
            }
        }*/

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.ToTable("cliente");

                entity.Property(e => e.Celular)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Correo)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Direccion)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Empresa>(entity =>
            {
                entity.ToTable("empresa");

                entity.Property(e => e.ContactoEmpresa)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NombreEmpresa)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Equipo>(entity =>
            {
                entity.ToTable("equipo");

                entity.Property(e => e.Cpu)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Detalles)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Estado)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Marca)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Modelo)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Propietario)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Ram)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("RAM");

                entity.Property(e => e.Serial)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.HasOne(d => d.Empresa)
                    .WithMany(p => p.Equipos)
                    .HasForeignKey(d => d.EmpresaId)
                    .HasConstraintName("FK_equipo_empresa");
            });

            modelBuilder.Entity<Gestionenvio>(entity =>
            {
                entity.HasKey(e => e.GestionId);

                entity.ToTable("gestionenvio");

                entity.Property(e => e.Direccion)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.FechaGestion).HasColumnType("date");

                entity.Property(e => e.FechaLlegada).HasColumnType("date");

                entity.Property(e => e.NombreCliente)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Observaciones)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Telefono)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.Equipo)
                    .WithMany(p => p.Gestionenvios)
                    .HasForeignKey(d => d.EquipoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_gestionenvio_equipo");

                entity.HasOne(d => d.Usuario)
                    .WithMany(p => p.Gestionenvios)
                    .HasForeignKey(d => d.UsuarioId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_gestionenvio_usuario");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("usuario");

                entity.Property(e => e.Contraseña)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Estado)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Rol)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Telefono)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
