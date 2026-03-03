using System.ComponentModel.DataAnnotations;

namespace Productos.API.Models.DTOs
{
    public class ActualizarProductoDto
    {
        [Required(ErrorMessage = "El nombre es requerido")]
        [MaxLength(200, ErrorMessage = "El nombre no puede superar 200 caracteres")]
        public string Nombre { get; set; } = string.Empty;

        public string? Descripcion { get; set; }

        [Required(ErrorMessage = "La categoría es requerida")]
        public int CategoriaId { get; set; }

        [MaxLength(500, ErrorMessage = "La URL no puede superar 500 caracteres")]
        public string? ImagenUrl { get; set; }

        [Required(ErrorMessage = "El precio es requerido")]
        [Range(0, double.MaxValue, ErrorMessage = "El precio debe ser mayor o igual a 0")]
        public decimal Precio { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "El stock debe ser mayor o igual a 0")]
        public int Stock { get; set; }

        public bool Activo { get; set; }
    }
}