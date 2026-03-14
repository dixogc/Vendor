using Vendor.Models;
using Vendor.Repository;

namespace Vendor.Service
{
    public class ProductoService
    {
        private readonly VendorDbContext _context;

        public ProductoService (VendorDbContext context)
        {
            _context = context;
        }

        public async Task<List<Producto>> ProductosBajoStock()
        {
            var productos = _context.Producto.Where(p => p.Stock <= p.StockMinimo).ToList();
            return productos;
        }
    }
}
