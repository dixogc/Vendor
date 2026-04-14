using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Vendor.Models;
using Vendor.Repository;

namespace Vendor.Controllers
{
    [Route("vendor/[controller]")]
    [ApiController]
    public class DetalleInversionController : Controller
    {
        private readonly VendorDbContext _context;
        private readonly DetalleInversionRepository _repository;

        public DetalleInversionController(VendorDbContext context, DetalleInversionRepository repository)
        {
            _context = context;
            _repository = repository;
        }

        [HttpGet("/por-inversion/{id}")]
        public async Task<ActionResult<DetalleInversion>> ObtenerDetalleInversionPorInversionId(int id)
        {
            var detalleInversion = await _repository.ObtenerDetalleInversionPorId(id);
            if (detalleInversion == null) return NotFound();
            return Ok(detalleInversion);
        }

    }
}
