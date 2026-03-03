using Productos.API.Models.DTOs;
using Productos.API.Models.Entities;
using Productos.API.Repositories.Interfaces;
using Productos.API.Services.Interfaces;

namespace Productos.API.Services.Implementations
{
    public class ProductoService : IProductoService
    {
        private readonly IProductoRepository _repository;

        public ProductoService(IProductoRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ProductoDto>> ObtenerTodosAsync(string? nombre,
                                                                      int? categoriaId,
                                                                      decimal? precioMin,
                                                                      decimal? precioMax,
                                                                      bool? activo)
        {
            var productos = await _repository.ObtenerTodosAsync(nombre, categoriaId, precioMin, precioMax, activo);
            return productos.Select(MapearADto);
        }

        public async Task<ProductoDto?> ObtenerPorIdAsync(int id)
        {
            var producto = await _repository.ObtenerPorIdAsync(id);
            return producto == null ? null : MapearADto(producto);
        }

        public async Task<ProductoDto> CrearAsync(CrearProductoDto dto)
        {
            var producto = new Producto
            {
                Nombre = dto.Nombre,
                Descripcion = dto.Descripcion,
                CategoriaId = dto.CategoriaId,
                ImagenUrl = dto.ImagenUrl,
                Precio = dto.Precio,
                Stock = dto.Stock,
                Activo = true
            };

            var creado = await _repository.CrearAsync(producto);
            return MapearADto(creado);
        }

        public async Task<ProductoDto?> ActualizarAsync(int id, ActualizarProductoDto dto)
        {
            var producto = await _repository.ObtenerPorIdAsync(id);
            if (producto == null) return null;

            producto.Nombre = dto.Nombre;
            producto.Descripcion = dto.Descripcion;
            producto.CategoriaId = dto.CategoriaId;
            producto.ImagenUrl = dto.ImagenUrl;
            producto.Precio = dto.Precio;
            producto.Stock = dto.Stock;
            producto.Activo = dto.Activo;

            var actualizado = await _repository.ActualizarAsync(producto);
            return MapearADto(actualizado);
        }

        public async Task<bool> EliminarAsync(int id)
        {
            return await _repository.EliminarAsync(id);
        }

        public async Task<bool> ActualizarStockAsync(int id, int cantidad)
        {
            return await _repository.ActualizarStockAsync(id, cantidad);
        }

        private static ProductoDto MapearADto(Producto producto) => new()
        {
            Id = producto.Id,
            Nombre = producto.Nombre,
            Descripcion = producto.Descripcion,
            CategoriaId = producto.CategoriaId,
            CategoriaNombre = producto.Categoria?.Nombre,
            ImagenUrl = producto.ImagenUrl,
            Precio = producto.Precio,
            Stock = producto.Stock,
            Activo = producto.Activo
        };
    }
}