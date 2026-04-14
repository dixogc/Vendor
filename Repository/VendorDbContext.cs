using Microsoft.EntityFrameworkCore;
using Vendor.Models;

namespace Vendor.Repository
{
    public class VendorDbContext : DbContext
    {
        public VendorDbContext(DbContextOptions<VendorDbContext> options)
        : base(options){}
        public DbSet<Producto> Producto { get; set; }
        public DbSet<Venta> Venta { get; set; }
        public DbSet<RegistroDeVenta> RegistroDeVenta { get; set; }
        public DbSet<Inversion> Inversion { get; set; } 
        public DbSet<DetalleInversion> DetalleInversion { get; set; }
        public DbSet<Movimientos> Movimiento { get; set; }

        //Metodo para poder leer el enum de Metodo de Pago y Movimiento
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Venta>()
                .Property(v => v.MetodoPago)
                .HasConversion<string>();
            modelBuilder.Entity<Movimientos>()
                .Property(m => m.Tipo)
                .HasConversion<string>();
        }
    }

}
