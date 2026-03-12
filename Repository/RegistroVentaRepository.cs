using Microsoft.EntityFrameworkCore;

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
            var ventaRegistro = await _context.RegistroDeVenta.FindAsync(id);
            return ventaRegistro;
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
