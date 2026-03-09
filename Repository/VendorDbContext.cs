using Microsoft.EntityFrameworkCore;

namespace Vendor.Repository
{
    public class VendorDbContext : DbContext
    {
        public VendorDbContext(DbContextOptions<VendorDbContext> options)
        : base(options){}
        public DbSet<Producto> Producto { get; set; }
        public DbSet<Venta> Venta { get; set; }
        public DbSet<RegistroDeVenta> RegistroDeVenta { get; set; }
    }
}
