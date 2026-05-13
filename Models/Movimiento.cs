using System.ComponentModel.DataAnnotations;


namespace Vendor.Models
{
    public class Movimiento
    {
        public int Id { get; set; }
        public Tipo Tipo { get; set; }
        public decimal Monto { get; set; }
        public DateOnly Fecha { get; set; }
        public int? ReferenciaID { get; set; }
    }

    public enum Tipo
    {
        SALDO_INICIAL,
        ENTRADA_POR_VENTA,
        SALIDA_POR_INVERSION
    }
}
