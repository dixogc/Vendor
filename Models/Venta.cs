using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Venta
{
    [Key]
    public int Id { get; set; }
    public DateTime Fecha { get; set; }
    public string Lugar { get; set; }
    public decimal TotalVenta { get; set; }
    public enum MetodoPago
    {
        TRANSFERENCIA,
        EFECTIVO
    }
}



