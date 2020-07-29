using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Proyecto_final_pro_3.Models
{
    public partial class DB_A64A4C_SuperMercadoContext : DbContext
    {
        public DB_A64A4C_SuperMercadoContext()
        {
        }

        public DB_A64A4C_SuperMercadoContext(DbContextOptions<DB_A64A4C_SuperMercadoContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Carrito> Carrito { get; set; }
        public virtual DbSet<Categoria> Categoria { get; set; }
        public virtual DbSet<Compra> Compra { get; set; }
        public virtual DbSet<DetalleOrden> DetalleOrden { get; set; }
        public virtual DbSet<Domicilio> Domicilio { get; set; }
        public virtual DbSet<Ofertas> Ofertas { get; set; }
        public virtual DbSet<Orden> Orden { get; set; }
        public virtual DbSet<Producto> Producto { get; set; }
        public virtual DbSet<Rol> Rol { get; set; }
        public virtual DbSet<Slider> Slider { get; set; }
        public virtual DbSet<SliderUser> SliderUser { get; set; }
        public virtual DbSet<StatusOrden> StatusOrden { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=SQL5045.site4now.net;Initial Catalog=DB_A64A4C_SuperMercado;User Id=DB_A64A4C_SuperMercado_admin;Password=Proyectofinal01");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Carrito>(entity =>
            {
                entity.HasKey(e => e.IdCarrito)
                    .HasName("PK_IdCarrito");

                entity.HasOne(d => d.IdProductoNavigation)
                    .WithMany(p => p.Carrito)
                    .HasForeignKey(d => d.IdProducto)
                    .HasConstraintName("FK_CARRITO_PRODUCTO");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Carrito)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK_CARRITO_USUARIO");
            });

            modelBuilder.Entity<Categoria>(entity =>
            {
                entity.HasKey(e => e.IdCategoria)
                    .HasName("PK_IdCategoria");
            });

            modelBuilder.Entity<Compra>(entity =>
            {
                entity.HasKey(e => e.IdCompra)
                    .HasName("PK_IdCompra");

                entity.Property(e => e.Fecha).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.IdProductoNavigation)
                    .WithMany(p => p.Compra)
                    .HasForeignKey(d => d.IdProducto)
                    .HasConstraintName("FK_COMPRA_PRODUCTO");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Compra)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK_COMPRA_USUARIO");
            });

            modelBuilder.Entity<DetalleOrden>(entity =>
            {
                entity.HasKey(e => e.IdDetalleOrden)
                    .HasName("PK_IdDetalleOrden");

                entity.HasOne(d => d.IdOrdenNavigation)
                    .WithMany(p => p.DetalleOrden)
                    .HasForeignKey(d => d.IdOrden)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_IdOrden");

                entity.HasOne(d => d.IdProductoNavigation)
                    .WithMany(p => p.DetalleOrden)
                    .HasForeignKey(d => d.IdProducto)
                    .HasConstraintName("FK_DETALLEORDEN_PRODUCTO");
            });

            modelBuilder.Entity<Domicilio>(entity =>
            {
                entity.HasKey(e => e.IdDomicilio)
                    .HasName("PK_IdDomicilio");

                entity.Property(e => e.Direccion).IsUnicode(false);

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Domicilio)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK_DOMICILIO_USUARIO");
            });

            modelBuilder.Entity<Ofertas>(entity =>
            {
                entity.HasKey(e => e.IdOfertas)
                    .HasName("PK_OFERTAS");

                entity.Property(e => e.Descripcion).IsUnicode(false);

                entity.HasOne(d => d.IdProductoNavigation)
                    .WithMany(p => p.Ofertas)
                    .HasForeignKey(d => d.IdProducto)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_OFERTAS_PRODUCTO");
            });

            modelBuilder.Entity<Orden>(entity =>
            {
                entity.HasKey(e => e.IdOrden)
                    .HasName("PK_IdOrden");

                entity.Property(e => e.Fecha).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Total).IsUnicode(false);

                entity.HasOne(d => d.IdDomicilioNavigation)
                    .WithMany(p => p.Orden)
                    .HasForeignKey(d => d.IdDomicilio)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_IdDomicilio");

                entity.HasOne(d => d.IdStatusOrdenNavigation)
                    .WithMany(p => p.Orden)
                    .HasForeignKey(d => d.IdStatusOrden)
                    .HasConstraintName("FK_ORDEN_STATUSORDEN");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Orden)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK_ORDEN_USUARIO");
            });

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.HasKey(e => e.IdProducto)
                    .HasName("PK_IdProducto");

                entity.HasOne(d => d.IdCategoriaNavigation)
                    .WithMany(p => p.Producto)
                    .HasForeignKey(d => d.IdCategoria)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PRODUCTO_CATEGORIA");
            });

            modelBuilder.Entity<Rol>(entity =>
            {
                entity.HasKey(e => e.IdRol)
                    .HasName("PK_IdRol");

                entity.Property(e => e.Nombre).IsUnicode(false);
            });

            modelBuilder.Entity<Slider>(entity =>
            {
                entity.HasKey(e => e.IdSlider)
                    .HasName("PK_SLIDER");
            });

            modelBuilder.Entity<SliderUser>(entity =>
            {
                entity.HasKey(e => e.IdSliderUser)
                    .HasName("PK_SLIDERUSER");

                entity.HasOne(d => d.IdSliderNavigation)
                    .WithMany(p => p.SliderUser)
                    .HasForeignKey(d => d.IdSlider)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_SLIDERUSER_SLIDER");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.SliderUser)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_SLIDERUSER_USER");
            });

            modelBuilder.Entity<StatusOrden>(entity =>
            {
                entity.HasKey(e => e.IdStatusOrden)
                    .HasName("PK_STATUSORDEN");

                entity.Property(e => e.Nombre).IsUnicode(false);
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario)
                    .HasName("PK_IdUsuario");

                entity.HasOne(d => d.IdRolNavigation)
                    .WithMany(p => p.Usuario)
                    .HasForeignKey(d => d.IdRol)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_IdRol");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
