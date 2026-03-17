using Vendor.Models;

namespace Vendor.DTOs.Response
{
    public class DashboardResponse
    {
        public int NumeroDeVentas { get; set; }
        public IEnumerable<Producto> ProductosDeBajoStock { get; set; }
    }
}
