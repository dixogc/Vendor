using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vendor.DTOs.Request;
using Vendor.Models;
using Vendor.Repository;
using Vendor.Service;

namespace Vendor.Controllers
{
    [Route("vendor/[controller]")]
    [ApiController]
    public class InversionController : Controller
    {
        private readonly VendorDbContext _context;
        private readonly InversionRepository _repository;
        private readonly InversionService _service;

        public InversionController(VendorDbContext context, InversionRepository repository, InversionService service)
        {
            _context = context;
            _repository = repository;
            _service = service;
        }

        [HttpPost]
        public async Task<ActionResult<Inversion>> RegistrarInversion(InversionRequest inversionRequest)
        {
            try
            {
                var inversion = await _service.RegistrarInversionCompleta(inversionRequest);
                return CreatedAtAction(nameof(ObtenerInversionPorId), new { id = inversion.Id }, inversion);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Inversion>> ObtenerInversionPorId (int id)
        {
            var inversion = await _repository.ObtenerInversionPorId(id);
            if(inversion == null) return NotFound();
            return Ok(inversion);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Venta>>> ObtenerTodasLasInversiones()
        {
            var inversiones = await _repository.ObtenerTodasLasInversiones();
            if (inversiones.Count() == 0) return NoContent();
            return Ok(inversiones);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<Venta>> EditarInversion(int id, Inversion inversion)
        {
            if (id != inversion.Id) return BadRequest();
            try
            {
                await _repository.EditarInversion(inversion);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Inversion.Any(e => e.Id == id)) return NotFound();
                throw;
            }
            return NoContent();
        }
    }
}
