using Microsoft.AspNetCore.Mvc;
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

        [HttpGet("{id}")]
        public async Task<ActionResult<Producto>> ObtenerProducto(int id)
        {
            var producto = await _repository.ObtenerPorId(id);
            if (producto == null) return NotFound();

            return producto;
        }

        [HttpPost]
        public async Task<ActionResult<Producto>> CrearProducto(Producto producto)
        {
            await _repository.CrearProducto(producto);
            return CreatedAtAction(nameof(ObtenerProducto), new { id = producto.Id }, producto);
        }
    }
}
