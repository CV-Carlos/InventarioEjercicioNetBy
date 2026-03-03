using Transacciones.API.Models.DTOs;

namespace Transacciones.API.Services.Interfaces
{
    public interface ITransaccionService
    {
        Task<PaginadoDto<TransaccionDto>> ObtenerTodosAsync(int? productoId, DateTime? fechaInicio, DateTime? fechaFin, string? tipo, int pagina, int itemsPorPagina); Task<TransaccionDto?> ObtenerPorIdAsync(int id);
        Task<(TransaccionDto? transaccion, string? error)> CrearAsync(CrearTransaccionDto dto);
        Task<(TransaccionDto? transaccion, string? error)> ActualizarAsync(int id, CrearTransaccionDto dto);
        Task<bool> EliminarAsync(int id);
    }
}