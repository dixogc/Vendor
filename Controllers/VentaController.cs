using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vendor.Repository;

namespace Vendor.Controllers
{
    [Route("vendor/[controller]")]
    [ApiController]
    public class VentaController : Controller
    {
        private readonly VendorDbContext _context;
        private readonly VentaRepository _repository;

        public VentaController(VendorDbContext vendorDbContext, VentaRepository ventaRepository)
        {
            _context = vendorDbContext;
            _repository = ventaRepository;
        }

        [HttpPost]
        public async Task<ActionResult<Venta>> RegistrarVenta(Venta venta)
        {
            await _repository.RegistrarVenta(venta);
            return CreatedAtAction(nameof(RegistrarVenta), new { id = venta.Id }, venta);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Venta>> ObtenerVenta(int id)
        {
            var producto = await _repository.ObtenerVenta(id);
            if (producto == null) return NotFound();

            return producto;
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<Venta>> EditarVenta(int id, Venta venta)
        {
            if (id != venta.Id) return BadRequest();
            try
            {
                await _repository.EditarVenta(venta);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Venta.Any(e => e.Id == id)) return NotFound();
                throw;
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> EliminarVenta(int id)
        {
            var venta = await _context.Venta.FindAsync(id);
            if (venta == null) return NotFound();
            await _repository.EliminarVenta(venta);
            return Ok();
        }
    }


}
