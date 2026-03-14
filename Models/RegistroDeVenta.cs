using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vendor.Models
{
    public class RegistroDeVenta
    {
        [Key]
        public int Id { get; set; }

        public int ProductoID { get; set; }
        [ForeignKey("ProductoID")]
        public virtual Producto Producto { get; set; }
        public int VentaID { get; set; }
        [ForeignKey("VentaID")]
        public virtual Venta Venta { get; set; } 
        public decimal PrecioVenta { get; set; }
        public int Cantidad { get; set; }
        public decimal Subtotal { get; set; }
    }
}


