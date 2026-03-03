using Productos.API.Models.DTOs;

namespace Productos.API.Services.Interfaces
{
    public interface IProductoService
    {
        Task<PaginadoDto<ProductoDto>> ObtenerTodosAsync(string? nombre, int? categoriaId, decimal? precioMin, decimal? precioMax, bool? activo, int pagina, int itemsPorPagina); Task<ProductoDto?> ObtenerPorIdAsync(int id);
        Task<ProductoDto> CrearAsync(CrearProductoDto dto);
        Task<ProductoDto?> ActualizarAsync(int id, ActualizarProductoDto dto);
        Task<bool> EliminarAsync(int id);
        Task<bool> ActualizarStockAsync(int id, int cantidad);
        Task<IEnumerable<ProductoSelectDto>> ObtenerParaSelectAsync();
    }
}