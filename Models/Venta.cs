using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Venta
{
    [Key]
    public int Id { get; set; }
    public DateOnly Fecha { get; set; }
    public string Lugar { get; set; }
    public decimal TotalVenta { get; set; }
    public MetodoDePago MetodoPago { get; set; }
}
public enum MetodoDePago
{
    TRANSFERENCIA,
    EFECTIVO
}



