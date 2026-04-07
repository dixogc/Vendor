using Microsoft.EntityFrameworkCore;
using Vendor.Models;

namespace Vendor.Repository
{
    public class InversionRepository
    {
        private readonly VendorDbContext _context;

        public InversionRepository(VendorDbContext context)
        {
            _context = context;
        }

        public async Task RegistrarInversion(Inversion inversion)
        {
            _context.Inversion.Add(inversion);
            await _context.SaveChangesAsync();
        }

        public async Task<Inversion> ObtenerInversionPorId(int id)
        {
            var inversion = await _context.Inversion.FindAsync(id);
            return  inversion;
        }

        public async Task<List<Inversion>> ObtenerTodasLasInversiones()
        {
            var inversiones = await _context.Inversion.ToListAsync();
            return inversiones;
        }

        public async Task EditarInversion(Inversion inversion)
        {
            _context.Entry(inversion).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
