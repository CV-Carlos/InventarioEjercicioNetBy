namespace Productos.API.Models.Entities
{
    public class Categoria
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public DateTime CreadoEn { get; set; }

        public ICollection<Producto> Productos { get; set; } = new List<Producto>();
    }
}