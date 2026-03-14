using Microsoft.EntityFrameworkCore;
using Vendor.Models;

namespace Vendor.Repository
{
    public class RegistroVentaRepository
    {
        private readonly VendorDbContext _context;

        public RegistroVentaRepository(VendorDbContext context)
        {
            _context = context;
        }

        public async Task CrearRegistroVenta(RegistroDeVenta registroDeVenta)
        {
            _context.RegistroDeVenta.Add(registroDeVenta);
            await _context.SaveChangesAsync();

        }

        public async Task<RegistroDeVenta> ObtenerRegistroVenta(int id)
        {
            return await _context.RegistroDeVenta
        .Include(r => r.Producto)
        .FirstOrDefaultAsync(r => r.Id == id);
        }
        public async Task<List<RegistroDeVenta>> ObtenerPorVenta(int ventaId)
        {
            return await _context.RegistroDeVenta
                .Where(r => r.VentaID == ventaId)
                .Include(r => r.Producto)
                .ToListAsync();
        }

        public async Task EditarRegistroVenta(RegistroDeVenta registroDeVenta)
        {
            _context.Entry(registroDeVenta).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task EliminarRegistroVenta (RegistroDeVenta registroDeVenta)
        {
            _context.RegistroDeVenta.Remove(registroDeVenta);
            await _context.SaveChangesAsync();
        }
    }
}
