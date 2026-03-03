namespace Transacciones.API.Models.DTOs
{
    public class CrearTransaccionDto
    {
        public string Tipo { get; set; } = string.Empty;
        public int ProductoId { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public string? Detalle { get; set; }
    }
}