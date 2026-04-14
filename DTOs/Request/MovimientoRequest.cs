using Vendor.Models;

namespace Vendor.DTOs.Request
{
    public class MovimientoRequest
    {
        public Tipo TipoDeMovimiento { get; set; }
        public decimal Monto { get; set; }
        public int ReferenciaID { get; set; }
    }
}
