namespace Productos.API.Models.DTOs
{
    public class ProductoSelectDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public int Stock { get; set; }
        public decimal Precio { get; set; }
    }
}