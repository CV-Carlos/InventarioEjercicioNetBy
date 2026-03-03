using Microsoft.EntityFrameworkCore;
using Transacciones.API.Models.Entities;

namespace Transacciones.API.Data
{
    public class TransaccionesDbContext : DbContext
    {
        public TransaccionesDbContext(DbContextOptions<TransaccionesDbContext> options) : base(options) { }

        public DbSet<Transaccion> Transacciones { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("transacciones");

            modelBuilder.Entity<Transaccion>(entity =>
            {
                entity.ToTable("transacciones");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                      .HasColumnName("id");
                entity.Property(e => e.Fecha)
                      .HasColumnName("fecha");
                entity.Property(e => e.Tipo)
                      .HasColumnName("tipo")
                      .HasColumnType("tipo_transaccion");
                entity.Property(e => e.ProductoId)
                      .HasColumnName("producto_id");
                entity.Property(e => e.Cantidad)
                      .HasColumnName("cantidad");
                entity.Property(e => e.PrecioUnitario)
                      .HasColumnName("precio_unitario")
                      .HasColumnType("numeric(10,2)");
                entity.Property(e => e.PrecioTotal)
                      .HasColumnName("precio_total")
                      .HasColumnType("numeric(10,2)")
                      .ValueGeneratedOnAddOrUpdate();
                entity.Property(e => e.Detalle)
                      .HasColumnName("detalle");
                entity.Property(e => e.CreadoEn)
                      .HasColumnName("creado_en");
                entity.Property(e => e.Eliminado)
                      .HasColumnName("eliminado");
            });
        }
    }
}