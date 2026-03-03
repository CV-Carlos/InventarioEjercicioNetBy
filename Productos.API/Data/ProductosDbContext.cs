using Microsoft.EntityFrameworkCore;
using Productos.API.Models.Entities;

namespace Productos.API.Data
{
    public class ProductosDbContext : DbContext
    {
        public ProductosDbContext(DbContextOptions<ProductosDbContext> options) : base(options) { }

        public DbSet<Producto> Productos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Schema
            modelBuilder.HasDefaultSchema("productos");

            // Categoria
            modelBuilder.Entity<Categoria>(entity =>
            {
                entity.ToTable("categorias");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Nombre).HasColumnName("nombre").HasMaxLength(100).IsRequired();
                entity.Property(e => e.CreadoEn).HasColumnName("creado_en");
            });

            // Producto
            modelBuilder.Entity<Producto>(entity =>
            {
                entity.ToTable("productos");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Nombre).HasColumnName("nombre").HasMaxLength(200).IsRequired();
                entity.Property(e => e.Descripcion).HasColumnName("descripcion");
                entity.Property(e => e.CategoriaId).HasColumnName("categoria_id");
                entity.Property(e => e.ImagenUrl).HasColumnName("imagen_url").HasMaxLength(500);
                entity.Property(e => e.Precio).HasColumnName("precio").HasColumnType("numeric(10,2)");
                entity.Property(e => e.Stock).HasColumnName("stock");
                entity.Property(e => e.Activo).HasColumnName("activo");
                entity.Property(e => e.CreadoEn).HasColumnName("creado_en");
                entity.Property(e => e.ActualizadoEn).HasColumnName("actualizado_en");

                entity.HasOne(e => e.Categoria)
                      .WithMany(c => c.Productos)
                      .HasForeignKey(e => e.CategoriaId);
            });
        }
    }
}