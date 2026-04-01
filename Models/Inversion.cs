using System.ComponentModel.DataAnnotations;


namespace Vendor.Models
{
    public class Inversion
    {
        [Key]
        public int Id { get; set; }
        public decimal Total { get; set; }
        public DateOnly Fecha { get; set; }
    }
}
