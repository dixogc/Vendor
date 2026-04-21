using Microsoft.AspNetCore.Mvc;
using Vendor.Models;
using Vendor.Repository;
using Vendor.DTOs.Request;
using Vendor.Service;

namespace Vendor.Controllers
{
    [Route("vendor/[controller]")]
    [ApiController]
    public class MovimientosController : Controller
    {
        private readonly VendorDbContext _context;
        private readonly MovimientoRepository _repository;
        private readonly MovimientoService _service;

        public MovimientosController (VendorDbContext context, MovimientoRepository repository, MovimientoService service)
        {
            _context = context;
            _repository = repository;
            _service = service;
        }

        [HttpPost]
        public async Task<ActionResult<Movimientos>> RegistrarMovimiento(Movimientos movimiento)
        {
            await _repository.RegistrarMovimiento(movimiento);
            return CreatedAtAction(nameof(ObtenerMovimientoPorId), new { id = movimiento.Id }, movimiento);
        }

        [HttpPost]
        public async Task<ActionResult<Movimientos>> RegistrarMovimientoInicial(MovimientoInicialRequest request)
        {
            try
            {
                var movimientoInicial = _service.RegistroDeMovimientoPorSaldoInicial(request);
                return CreatedAtAction(nameof(ObtenerMovimientoPorId), new { id = movimientoInicial.Id }, movimientoInicial);
            }catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
            
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Producto>> ObtenerMovimientoPorId(int id)
        {
            var movimiento = await _repository.ObtenerMovimientoPorId(id);
            if (movimiento == null) return NotFound();

            return Ok(movimiento);
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movimientos>>> ObtenerTodosLosMovimientos()
        {
            var movimientos = await _repository.ObtenerTodosLosMovimientos();
            if (movimientos.Count == 0) return NotFound();
            return Ok(movimientos);
        }
    }
}
