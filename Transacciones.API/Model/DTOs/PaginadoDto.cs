namespace Transacciones.API.Models.DTOs
{
    public class PaginadoDto<T>
    {
        public IEnumerable<T> Items { get; set; } = new List<T>();
        public int TotalItems { get; set; }
        public int PaginaActual { get; set; }
        public int ItemsPorPagina { get; set; }
        public int TotalPaginas => (int)Math.Ceiling((double)TotalItems / ItemsPorPagina);
    }
}