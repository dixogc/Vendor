using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vendor.Repository;

namespace Vendor.Controllers
{
    [Route("vendor/[controller]")]
    [ApiController]
    public class ProductoController : Controller
    {
        private readonly VendorDbContext _context;
        private readonly ProductoRepository _repository;

        public ProductoController(VendorDbContext context, ProductoRepository repository)
        {
            _context = context;
            _repository = repository;
        }

        [HttpPost]
        public async Task<ActionResult<Producto>> CrearProducto(Producto producto)
        {
            await _repository.CrearProducto(producto);
            return CreatedAtAction(nameof(ObtenerProducto), new { id = producto.Id }, producto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Producto>> ObtenerProducto(int id)
        {
            var producto = await _repository.ObtenerPorId(id);
            if (producto == null) return NotFound();

            return producto;
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<Producto>> EditarProducto(int id, Producto producto)
        {
            if (id != producto.Id) return BadRequest();
            try
            {
                await _repository.EditarProducto(producto);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Producto.Any(e => e.Id == id)) return NotFound();
                throw;
            }
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> EliminarProducto(int id)
        {
            var producto = await _context.Producto.FindAsync(id);
            if (producto == null) return NotFound();
            await _repository.EliminarProducto(producto);
            return Ok();
        }
    }
}
