using Productos.API.Models.Entities;

namespace Productos.API.Repositories.Interfaces
{
    public interface IProductoRepository
    {
        Task<IEnumerable<Producto>> ObtenerTodosAsync(string? nombre, int? categoriaId, decimal? precioMin, decimal? precioMax, bool? activo);
        Task<Producto?> ObtenerPorIdAsync(int id);
        Task<Producto> CrearAsync(Producto producto);
        Task<Producto> ActualizarAsync(Producto producto);
        Task<bool> EliminarAsync(int id);
        Task<bool> ActualizarStockAsync(int id, int cantidad);
    }
}