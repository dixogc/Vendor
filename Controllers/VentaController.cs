using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vendor.Repository;
using Vendor.Models;
using Vendor.Service;
using Vendor.DTOs;
using Vendor.DTOs.Request;

namespace Vendor.Controllers
{
    [Route("vendor/[controller]")]
    [ApiController]
    public class VentaController : Controller
    {
        private readonly VendorDbContext _context;
        private readonly VentaRepository _repository;
        private readonly VentaService _service;

        public VentaController(VendorDbContext vendorDbContext, VentaRepository ventaRepository, VentaService service)
        {
            _context = vendorDbContext;
            _repository = ventaRepository;
            _service = service;
        }

        [HttpPost]
        public async Task<ActionResult<Venta>> RegistrarVenta([FromBody] VentaRequest ventaRequest)
        {
            try
            {
                var ventaGenerada = await _service.RegistrarVentaCompleta(ventaRequest);
                return CreatedAtAction(nameof(ObtenerVenta), new { id = ventaGenerada.Id }, ventaGenerada);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Venta>> ObtenerVenta(int id)
        {
            var venta = await _repository.ObtenerVenta(id);
            if (venta == null) return NotFound();
            return Ok(venta);
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
