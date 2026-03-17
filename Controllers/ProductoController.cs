using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vendor.Repository;
using Vendor.Models;

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
            return CreatedAtAction(nameof(ObtenerProductoPorId), new { id = producto.Id }, producto);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Producto>>> ObtenerTodosLosProductos()
        {
            var productos = await _repository.ObtenerTodosLosProductos();
            if(productos.Count() == 0) return NoContent();
            return Ok(productos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Producto>> ObtenerProductoPorId(int id)
        {
            var producto = await _repository.ObtenerPorId(id);
            if (producto == null) return NotFound();

            return Ok(producto);
        }

        [HttpGet("Stock")]
        public async Task<ActionResult<IEnumerable<Producto>>> ObtenerProductosBajoStock()
        {
            var productos = await _repository.ProductosBajoStock();
            if (productos.Count() == 0) return NoContent();
            return Ok(productos);
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
