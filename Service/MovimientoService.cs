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

        public async Task<Movimiento> RegistroDeMovimientoPorSaldoInicial(MovimientoInicialRequestDTO request)
        {
                var primerMovimiento = new Movimiento
                {
                    Tipo = Tipo.SALDO_INICIAL,
                    Monto = request.MontoInicial,
                    Fecha = DateOnly.FromDateTime(DateTime.Now),
                    ReferenciaID = null //no hay una entidad que almacene el saldo inicial, por lo que para este tipo la referencia es null
                };
                _context.Movimiento.Add(primerMovimiento);
                await _context.SaveChangesAsync();
            return primerMovimiento;
        }
    }
}
