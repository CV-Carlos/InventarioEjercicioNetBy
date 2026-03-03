using Microsoft.EntityFrameworkCore;
using Productos.API.Data;
using Productos.API.Models.Entities;
using Productos.API.Repositories.Interfaces;

namespace Productos.API.Repositories.Implementations
{
    public class ProductoRepository : IProductoRepository
    {
        private readonly ProductosDbContext _context;

        public ProductoRepository(ProductosDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Producto>> ObtenerTodosAsync(string? nombre,
                                                                   int? categoriaId,
                                                                   decimal? precioMin,
                                                                   decimal? precioMax,
                                                                   bool? activo)
        {
            var query = _context.Productos.Include(p => p.Categoria).AsQueryable();

            if (!string.IsNullOrWhiteSpace(nombre))
                query = query.Where(p => p.Nombre.ToLower().Contains(nombre.ToLower()));

            if (categoriaId.HasValue)
                query = query.Where(p => p.CategoriaId == categoriaId.Value);

            if (precioMin.HasValue)
                query = query.Where(p => p.Precio >= precioMin.Value);

            if (precioMax.HasValue)
                query = query.Where(p => p.Precio <= precioMax.Value);

            if (activo.HasValue)
                query = query.Where(p => p.Activo == activo.Value);

            return await query.OrderBy(p => p.Nombre).ToListAsync();
        }

        public async Task<Producto?> ObtenerPorIdAsync(int id)
        {
            return await _context.Productos
                .Include(p => p.Categoria)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Producto> CrearAsync(Producto producto)
        {
            producto.CreadoEn = DateTime.UtcNow;
            producto.ActualizadoEn = DateTime.UtcNow;
            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();
            return producto;
        }

        public async Task<Producto> ActualizarAsync(Producto producto)
        {
            producto.ActualizadoEn = DateTime.UtcNow;
            _context.Productos.Update(producto);
            await _context.SaveChangesAsync();
            return producto;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null) return false;

            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ActualizarStockAsync(int id, int cantidad)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null) return false;

            producto.Stock += cantidad;
            producto.ActualizadoEn = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}