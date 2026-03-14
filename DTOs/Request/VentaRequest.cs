using Vendor.Models;

namespace Vendor.DTOs.Request
{
    public class VentaRequest
    {
        public string Lugar {  get; set; }
        public MetodoDePago MetodoPago { get; set; }
        public List<ProductoVentaDTO> Detalles { get; set; }
    }
}
