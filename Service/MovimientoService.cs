using Vendor.Repository;
using Vendor.Models;
using Vendor.DTOs.Request;

namespace Vendor.Service
{
    public class MovimientoService
    {
        private readonly VendorDbContext _context;

        public MovimientoService(VendorDbContext context)
        {
            _context = context;
        }

        public async Task<Movimientos> RegistroDeMovimiento(MovimientoRequest request)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var nuevoMovimiento = new Movimientos
                {
                    Tipo = request.TipoDeMovimiento,
                    Monto = request.Monto,
                    Fecha = DateOnly.FromDateTime(DateTime.Now)
                };
            }catch (Exception ex)
            {

            }
    }
}
