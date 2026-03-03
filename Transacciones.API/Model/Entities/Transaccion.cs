namespace Transacciones.API.Models.Entities
{
    public enum TipoTransaccion
    {
        Compra,
        Venta
    }

    public class Transaccion
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public TipoTransaccion Tipo { get; set; }
        public int ProductoId { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal PrecioTotal { get; set; }
        public string? Detalle { get; set; }
        public DateTime CreadoEn { get; set; }
        public bool Eliminado { get; set; }
    }
}