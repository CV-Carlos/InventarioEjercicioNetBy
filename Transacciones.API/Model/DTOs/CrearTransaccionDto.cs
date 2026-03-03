using System.ComponentModel.DataAnnotations;

namespace Transacciones.API.Models.DTOs
{
    public class CrearTransaccionDto
    {
        [Required(ErrorMessage = "El tipo es requerido")]
        [RegularExpression("^(Compra|Venta)$", ErrorMessage = "El tipo debe ser 'Compra' o 'Venta'")]
        public string Tipo { get; set; } = string.Empty;

        [Required(ErrorMessage = "El producto es requerido")]
        public int ProductoId { get; set; }

        [Required(ErrorMessage = "La cantidad es requerida")]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor a 0")]
        public int Cantidad { get; set; }

        [Required(ErrorMessage = "El precio unitario es requerido")]
        [Range(0, double.MaxValue, ErrorMessage = "El precio unitario debe ser mayor o igual a 0")]
        public decimal PrecioUnitario { get; set; }

        public string? Detalle { get; set; }
    }
}