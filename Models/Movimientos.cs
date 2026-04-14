using System.ComponentModel.DataAnnotations;


namespace Vendor.Models
{
    public class Movimientos
    {
        public int Id { get; set; }
        public Tipo Tipo { get; set; }
        public decimal Monto { get; set; }
        public DateOnly Fecha { get; set; }
        public int ReferenciaID { get; set; }
    }

    public enum Tipo
    {
        SaldoInicial,
        EntradaPorVenta,
        SalidaPorInversion
    }
}
