namespace Transacciones.API.Models.DTOs
{
    public class ProductoDetalleDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public int Stock { get; set; }
        public bool Activo { get; set; }
    }
}