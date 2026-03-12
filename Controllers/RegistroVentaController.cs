using Microsoft.AspNetCore.Mvc;
using Vendor.Repository;

namespace Vendor.Controllers
{
    public class RegistroVentaController : Controller
    {
        private readonly VendorDbContext _context;
        private readonly RegistroVentaRepository _repository;

        public RegistroVentaController(VendorDbContext context, RegistroVentaRepository repository)
        {
            _context = context;
            _repository = repository;
        }

        [HttpPost]
        public async Task<ActionResult> CrearRegistroVenta(RegistroDeVenta registroDeVenta)
        {
            await _repository.CrearRegistroVenta(registroDeVenta);
            return CreatedAtAction(nameof(registroDeVenta), new { id = registroDeVenta.Id }, registroDeVenta);

        }
    }
}
