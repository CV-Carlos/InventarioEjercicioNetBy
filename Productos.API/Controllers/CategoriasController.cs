using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Productos.API.Data;

namespace Productos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriasController : ControllerBase
    {
        private readonly ProductosDbContext _context;

        public CategoriasController(ProductosDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var categorias = await _context.Categorias
                .OrderBy(c => c.Nombre)
                .Select(c => new { c.Id, c.Nombre })
                .ToListAsync();
            return Ok(categorias);
        }
    }
}