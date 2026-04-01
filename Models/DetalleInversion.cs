using System.ComponentModel.DataAnnotations;


namespace Vendor.Models
{
    public class DetalleInversion
    {
        public int Id { get; set; }
        public int InversionID { get; set; }
        public int ProductoID { get; set; }
        public decimal Precio_Compra { get; set; }
        public decimal Subtotal { get; set; }
    }
}
