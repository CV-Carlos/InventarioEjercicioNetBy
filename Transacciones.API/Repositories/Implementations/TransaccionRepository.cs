using Microsoft.EntityFrameworkCore;
using Transacciones.API.Data;
using Transacciones.API.Models.Entities;
using Transacciones.API.Repositories.Interfaces;

namespace Transacciones.API.Repositories.Implementations
{
    public class TransaccionRepository : ITransaccionRepository
    {
        private readonly TransaccionesDbContext _context;

        public TransaccionRepository(TransaccionesDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Transaccion>> ObtenerTodosAsync(int? productoId, DateTime? fechaInicio, DateTime? fechaFin, string? tipo)
        {
            var query = _context.Transacciones.AsQueryable();

            if (productoId.HasValue)
                query = query.Where(t => t.ProductoId == productoId.Value);

            if (fechaInicio.HasValue)
                query = query.Where(t => t.Fecha >= fechaInicio.Value);

            if (fechaFin.HasValue)
                query = query.Where(t => t.Fecha <= fechaFin.Value);

            if (!string.IsNullOrWhiteSpace(tipo))
            {
                if (Enum.TryParse<TipoTransaccion>(tipo, true, out var tipoEnum))
                    query = query.Where(t => t.Tipo == tipoEnum);
            }

            return await query.OrderByDescending(t => t.Fecha).ToListAsync();
        }

        public async Task<Transaccion?> ObtenerPorIdAsync(int id)
        {
            return await _context.Transacciones.FindAsync(id);
        }

        public async Task<Transaccion> CrearAsync(Transaccion transaccion)
        {
            transaccion.CreadoEn = DateTime.UtcNow;
            _context.Transacciones.Add(transaccion);
            await _context.SaveChangesAsync();
            return transaccion;
        }

        public async Task<Transaccion?> ActualizarAsync(Transaccion transaccion)
        {
            var existente = await _context.Transacciones.FindAsync(transaccion.Id);
            if (existente == null) return null;

            existente.Fecha = transaccion.Fecha;
            existente.Tipo = transaccion.Tipo;
            existente.ProductoId = transaccion.ProductoId;
            existente.Cantidad = transaccion.Cantidad;
            existente.PrecioUnitario = transaccion.PrecioUnitario;
            existente.PrecioTotal = transaccion.PrecioTotal;
            existente.Detalle = transaccion.Detalle;

            await _context.SaveChangesAsync();
            return existente;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            var transaccion = await _context.Transacciones.FindAsync(id);
            if (transaccion == null) return false;

            _context.Transacciones.Remove(transaccion);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}