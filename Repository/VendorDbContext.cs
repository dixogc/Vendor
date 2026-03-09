using Microsoft.EntityFrameworkCore;

namespace Vendor.Repository
{
    public class VendorDbContext : DbContext
    {
        public DbSet<Producto> producto { get; set; }
        public DbSet<Venta> Venta { get; set; }
        public DbSet<RegistroDeVenta> RegistroDeVenta { get; set; }
    }
}
