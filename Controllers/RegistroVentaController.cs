using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vendor.Repository;
using Vendor.Models;

namespace Vendor.Controllers
{
    [Route("vendor/[controller]")]
    [ApiController]
    public class RegistroVentaController : Controller
    {
        private readonly RegistroVentaRepository _repository;

        public RegistroVentaController(RegistroVentaRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RegistroDeVenta>> ObtenerRegistroVenta(int id)
        {
            var registroVenta = await _repository.ObtenerRegistroVenta(id);
            if (registroVenta == null) return NotFound();
            return Ok(registroVenta);
        }

        [HttpGet("por-venta/{ventaId}")]
        public async Task<ActionResult<List<RegistroDeVenta>>> ObtenerPorVenta(int ventaId)
        {
            var detalles = await _repository.ObtenerPorVenta(ventaId);
            return Ok(detalles);
        }
    }
    
}
