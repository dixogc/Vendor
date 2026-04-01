using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.Json;
using Vendor.Models;

namespace Vendor.Repository
{
    public class VentaRepository
    {
        private readonly VendorDbContext _context;

        public VentaRepository(VendorDbContext context)
        {
            _context = context;
        }

        public async Task RegistrarVenta(Venta venta)
        {
            _context.Venta.Add(venta);
            await _context.SaveChangesAsync();
        }
        public async Task<Venta> ObtenerVentaPorId(int id)
        {
            //También trae los productos vendidos en esa venta
            return await _context.Venta
                .Include(v => v.Detalles)
                .FirstOrDefaultAsync(v => v.Id == id);
        }

        public async Task<IEnumerable<Venta>> ObtenerTodasLasVentas()
        {
            return await _context.Venta.ToListAsync();
        }

        public async Task<int> ObtenerVentasDelDia()
        {

            return await _context.Venta.CountAsync(f => f.Fecha == DateOnly.FromDateTime(DateTime.Now));
            
        }
        public async Task EditarVenta(Venta venta)
        {
            _context.Entry(venta).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        public async Task EliminarVenta(Venta venta)
        {
            _context.Venta.Remove(venta);
            await _context.SaveChangesAsync();
        }
    }
}
