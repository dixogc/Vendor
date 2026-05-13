using Vendor.Models;

namespace Vendor.DTOs.Response
{
    public class DashboardResponseDTO
    {
        public int NumeroDeVentas { get; set; }
        public IEnumerable<Producto> ProductosDeBajoStock { get; set; }
    }
}
