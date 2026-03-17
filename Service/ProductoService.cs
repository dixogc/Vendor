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

        
    }
}
