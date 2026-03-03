using Microsoft.AspNetCore.Mvc;
using Transacciones.API.Models.DTOs;
using Transacciones.API.Services.Interfaces;

namespace Transacciones.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransaccionesController : ControllerBase
    {
        private readonly ITransaccionService _service;

        public TransaccionesController(ITransaccionService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos(
            [FromQuery] int? productoId,
            [FromQuery] DateTime? fechaInicio,
            [FromQuery] DateTime? fechaFin,
            [FromQuery] string? tipo)
        {
            var transacciones = await _service.ObtenerTodosAsync(productoId, fechaInicio, fechaFin, tipo);
            return Ok(transacciones);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var transaccion = await _service.ObtenerPorIdAsync(id);
            if (transaccion == null) return NotFound(new { mensaje = "Transacción no encontrada" });
            return Ok(transaccion);
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] CrearTransaccionDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var (transaccion, error) = await _service.CrearAsync(dto);
            if (error != null) return BadRequest(new { mensaje = error });

            return CreatedAtAction(nameof(ObtenerPorId), new { id = transaccion!.Id }, transaccion);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] CrearTransaccionDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var (transaccion, error) = await _service.ActualizarAsync(id, dto);
            if (error != null) return BadRequest(new { mensaje = error });
            if (transaccion == null) return NotFound(new { mensaje = "Transacción no encontrada" });

            return Ok(transaccion);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var resultado = await _service.EliminarAsync(id);
            if (!resultado) return NotFound(new { mensaje = "Transacción no encontrada" });
            return Ok(new { mensaje = "Transacción eliminada correctamente" });
        }
    }
}