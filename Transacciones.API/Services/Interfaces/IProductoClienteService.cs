using Transacciones.API.Models.DTOs;

namespace Transacciones.API.Services.Interfaces
{
    public interface IProductoClienteService
    {
        Task<ProductoDetalleDto?> ObtenerProductoAsync(int id);
        Task<bool> ActualizarStockAsync(int id, int cantidad);
    }
}