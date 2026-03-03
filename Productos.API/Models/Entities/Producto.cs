namespace Productos.API.Models.Entities
{
    public class Producto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public int CategoriaId { get; set; }
        public string? ImagenUrl { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public bool Activo { get; set; }
        public DateTime CreadoEn { get; set; }
        public DateTime ActualizadoEn { get; set; }
        public bool Eliminado { get; set; }

        public Categoria Categoria { get; set; } = null!;
    }
}