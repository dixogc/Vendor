using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class RegistroDeVenta
{
    [Key]
    public int Id { get; set; }
    [ForeignKey("ProductoID")]
    public int ProductoId { get; set; }
    [ForeignKey("VentaID")]
    public int VentaId { get; set; }
    public decimal PrecioVenta { get; set; }
    public decimal Subtotal { get; set; }
}

