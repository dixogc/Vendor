using Microsoft.EntityFrameworkCore;
using Vendor.Models;

namespace Vendor.Repository
{
    public class DetalleInversionRepository
    {
        private readonly VendorDbContext _context;

        public DetalleInversionRepository(VendorDbContext context)
        {
            _context = context;
        }

        public async Task RegistrarDetalleInversion(DetalleInversion inversion)
        {
            _context.DetalleInversion.Add(inversion);
            await _context.SaveChangesAsync();
        }

        public async Task<DetalleInversion> ObtenerDetalleInversionPorId(int id)
        {
            var inversion = await _context.DetalleInversion.FindAsync(id);
            return inversion;
        }

    }
}
