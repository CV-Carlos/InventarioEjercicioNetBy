using Transacciones.API.Models.DTOs;
using Transacciones.API.Models.Entities;
using Transacciones.API.Repositories.Interfaces;
using Transacciones.API.Services.Interfaces;

namespace Transacciones.API.Services.Implementations
{
    public class TransaccionService : ITransaccionService
    {
        private readonly ITransaccionRepository _repository;
        private readonly IProductoClienteService _productoCliente;

        public TransaccionService(ITransaccionRepository repository, IProductoClienteService productoCliente)
        {
            _repository = repository;
            _productoCliente = productoCliente;
        }

        public async Task<PaginadoDto<TransaccionDto>> ObtenerTodosAsync(int? productoId,
                                                                         DateTime? fechaInicio,
                                                                         DateTime? fechaFin,
                                                                         string? tipo,
                                                                         int pagina,
                                                                         int itemsPorPagina)
        {
            var resultado = await _repository.ObtenerTodosAsync(productoId,
                                                                fechaInicio,
                                                                fechaFin,
                                                                tipo,
                                                                pagina,
                                                                itemsPorPagina);
            var transacciones = new List<TransaccionDto>();

            foreach (var t in resultado.Items)
            {
                var producto = await _productoCliente.ObtenerProductoAsync(t.ProductoId);
                transacciones.Add(MapearADto(t, producto));
            }

            return new PaginadoDto<TransaccionDto>
            {
                Items = transacciones,
                TotalItems = resultado.TotalItems,
                PaginaActual = resultado.PaginaActual,
                ItemsPorPagina = resultado.ItemsPorPagina
            };
        }

        public async Task<TransaccionDto?> ObtenerPorIdAsync(int id)
        {
            var transaccion = await _repository.ObtenerPorIdAsync(id);
            if (transaccion == null) return null;

            var producto = await _productoCliente.ObtenerProductoAsync(transaccion.ProductoId);
            return MapearADto(transaccion, producto);
        }

        public async Task<(TransaccionDto? transaccion, string? error)> CrearAsync(CrearTransaccionDto dto)
        {
            // Verificar que el producto existe
            var producto = await _productoCliente.ObtenerProductoAsync(dto.ProductoId);
            if (producto == null)
                return (null, "El producto no existe");

            if (!producto.Activo)
                return (null, "El producto no está activo");

            // Validar stock si es venta
            if (dto.Tipo.ToLower() == "venta" && producto.Stock < dto.Cantidad)
                return (null, $"Stock insuficiente. Stock disponible: {producto.Stock}");

            if (!Enum.TryParse<TipoTransaccion>(dto.Tipo, true, out var tipoEnum))
                return (null, "Tipo de transacción inválido. Use 'Compra' o 'Venta'");

            var transaccion = new Transaccion
            {
                Fecha = DateTime.UtcNow,
                Tipo = tipoEnum,
                ProductoId = dto.ProductoId,
                Cantidad = dto.Cantidad,
                PrecioUnitario = dto.PrecioUnitario,
                PrecioTotal = dto.Cantidad * dto.PrecioUnitario,
                Detalle = dto.Detalle
            };

            var creada = await _repository.CrearAsync(transaccion);

            // Actualizar stock: compra suma, venta resta
            var ajuste = tipoEnum == TipoTransaccion.Compra ? dto.Cantidad : -dto.Cantidad;
            await _productoCliente.ActualizarStockAsync(dto.ProductoId, ajuste);

            return (MapearADto(creada, producto), null);
        }

        public async Task<(TransaccionDto? transaccion, string? error)> ActualizarAsync(int id, CrearTransaccionDto dto)
        {
            var existente = await _repository.ObtenerPorIdAsync(id);
            if (existente == null)
                return (null, "Transacción no encontrada");

            var producto = await _productoCliente.ObtenerProductoAsync(dto.ProductoId);
            if (producto == null)
                return (null, "El producto no existe");

            if (!Enum.TryParse<TipoTransaccion>(dto.Tipo, true, out var tipoEnum))
                return (null, "Tipo de transacción inválido. Use 'Compra' o 'Venta'");

            existente.Tipo = tipoEnum;
            existente.ProductoId = dto.ProductoId;
            existente.Cantidad = dto.Cantidad;
            existente.PrecioUnitario = dto.PrecioUnitario;
            existente.PrecioTotal = dto.Cantidad * dto.PrecioUnitario;
            existente.Detalle = dto.Detalle;

            var actualizada = await _repository.ActualizarAsync(existente);
            return (MapearADto(actualizada!, producto), null);
        }

        public async Task<bool> EliminarAsync(int id)
        {
            return await _repository.EliminarAsync(id);
        }

        private static TransaccionDto MapearADto(Transaccion transaccion, ProductoDetalleDto? producto) => new()
        {
            Id = transaccion.Id,
            Fecha = transaccion.Fecha,
            Tipo = transaccion.Tipo.ToString(),
            ProductoId = transaccion.ProductoId,
            ProductoNombre = producto?.Nombre,
            StockActual = producto?.Stock ?? 0,
            Cantidad = transaccion.Cantidad,
            PrecioUnitario = transaccion.PrecioUnitario,
            PrecioTotal = transaccion.PrecioTotal,
            Detalle = transaccion.Detalle
        };
    }
}