using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Vendor.DTOs.Request;
using Vendor.Service;

namespace Vendor.Controllers
{
    [Route("vendor/[controller]")]
    [ApiController]
    [Authorize]

    public class UsuarioController : Controller
    {
        private readonly UsuarioService _service;

        public UsuarioController(UsuarioService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<ActionResult> Login (InicioDeSesionRequest request)
        {
            var usuario = await _service.ValidarUsuario(request);
            if (usuario == null) return BadRequest();
            //TODO: Generar Token
            return Ok();
        }
        
    }
}
