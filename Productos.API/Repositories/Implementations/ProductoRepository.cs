using Microsoft.EntityFrameworkCore;
using Productos.API.Data;
using Productos.API.Models.DTOs;
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

        public async Task<PaginadoDto<Producto>> ObtenerTodosAsync(string? nombre,
                                                                   int? categoriaId,
                                                                   decimal? precioMin,
                                                                   decimal? precioMax,
                                                                   bool? activo,
                                                                   int pagina,
                                                                   int itemsPorPagina)
        {
            var query = _context.Productos
                .Include(p => p.Categoria)
                .Where(p => !p.Eliminado)
                .AsQueryable();

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

            var totalItems = await query.CountAsync();

            var items = await query
                .OrderBy(p => p.Nombre)
                .Skip((pagina - 1) * itemsPorPagina)
                .Take(itemsPorPagina)
                .ToListAsync();

            return new PaginadoDto<Producto>
            {
                Items = items,
                TotalItems = totalItems,
                PaginaActual = pagina,
                ItemsPorPagina = itemsPorPagina
            };
        }

        public async Task<Producto?> ObtenerPorIdAsync(int id)
        {
            return await _context.Productos
                .Include(p => p.Categoria)
                .FirstOrDefaultAsync(p => p.Id == id && !p.Eliminado);
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
            var producto = await _context.Productos
                .FirstOrDefaultAsync(p => p.Id == id && !p.Eliminado);
            if (producto == null) return false;

            producto.Eliminado = true;
            producto.ActualizadoEn = DateTime.UtcNow;
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
        public async Task<IEnumerable<Producto>> ObtenerParaSelectAsync()
        {
            return await _context.Productos
                .Where(p => !p.Eliminado && p.Activo)
                .OrderBy(p => p.Nombre)
                .ToListAsync();
        }
    }
}