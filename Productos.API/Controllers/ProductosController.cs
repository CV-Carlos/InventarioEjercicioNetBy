using Microsoft.AspNetCore.Mvc;
using Productos.API.Models.DTOs;
using Productos.API.Services.Interfaces;

namespace Productos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductosController : ControllerBase
    {
        private readonly IProductoService _service;

        public ProductosController(IProductoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos(
            [FromQuery] string? nombre,
            [FromQuery] int? categoriaId,
            [FromQuery] decimal? precioMin,
            [FromQuery] decimal? precioMax,
            [FromQuery] bool? activo)
        {
            var productos = await _service.ObtenerTodosAsync(nombre,
                                                             categoriaId,
                                                             precioMin,
                                                             precioMax,
                                                             activo);
            return Ok(productos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var producto = await _service.ObtenerPorIdAsync(id);
            if (producto == null) return NotFound(new { mensaje = "Producto no encontrado" });
            return Ok(producto);
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] CrearProductoDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var producto = await _service.CrearAsync(dto);
            return CreatedAtAction(nameof(ObtenerPorId), new { id = producto.Id }, producto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] ActualizarProductoDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var producto = await _service.ActualizarAsync(id, dto);
            if (producto == null) return NotFound(new { mensaje = "Producto no encontrado" });
            return Ok(producto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var resultado = await _service.EliminarAsync(id);
            if (!resultado) return NotFound(new { mensaje = "Producto no encontrado" });
            return Ok(new { mensaje = "Producto eliminado correctamente" });
        }

        [HttpPatch("{id}/stock")]
        public async Task<IActionResult> ActualizarStock(int id, [FromQuery] int cantidad)
        {
            var resultado = await _service.ActualizarStockAsync(id, cantidad);
            if (!resultado) return NotFound(new { mensaje = "Producto no encontrado" });
            return Ok(new { mensaje = "Stock actualizado correctamente" });
        }
    }
}