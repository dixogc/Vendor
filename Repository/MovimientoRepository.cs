using Microsoft.EntityFrameworkCore;
using Vendor.Models;

namespace Vendor.Repository
{
    public class MovimientoRepository
    {
        private readonly VendorDbContext _context;

        public MovimientoRepository (VendorDbContext context)
        {
            _context = context;
        }

        public async Task RegistrarMovimiento(Movimiento movimiento)
        {
            _context.Movimiento.Add(movimiento);
            await _context.SaveChangesAsync();
        }

        public async Task<Movimiento> ObtenerMovimientoPorId(int id)
        {
            var movimiento = await _context.Movimiento.FindAsync(id);
            return movimiento;
        }

        public async Task<List<Movimiento>> ObtenerTodosLosMovimientos()
        {
            var movimientos = await _context.Movimiento.ToListAsync();
            return movimientos;
        }

        public async Task EliminarMovimiento(Movimiento movimiento)
        {
             _context.Movimiento.Remove(movimiento);
            await _context.SaveChangesAsync();
        }
    }
}
