using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vendor.Models
{
    public class Producto
    {
        [Key]
        public int Id { get; set; }
        public string NombreProducto { get; set; }
        public int Stock { get; set; }
        public int StockMinimo { get; set; }
        public decimal PrecioVenta { get; set; }
        public decimal PrecioCompra { get; set; }

    }
}



