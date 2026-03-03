using Transacciones.API.Models.Entities;

namespace Transacciones.API.Repositories.Interfaces
{
    public interface ITransaccionRepository
    {
        Task<IEnumerable<Transaccion>> ObtenerTodosAsync(int? productoId, DateTime? fechaInicio, DateTime? fechaFin, string? tipo);
        Task<Transaccion?> ObtenerPorIdAsync(int id);
        Task<Transaccion> CrearAsync(Transaccion transaccion);
        Task<Transaccion?> ActualizarAsync(Transaccion transaccion);
        Task<bool> EliminarAsync(int id);
    }
}