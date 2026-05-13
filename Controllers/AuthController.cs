using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Vendor.DTOs.Request;
using Vendor.Service;

namespace Vendor.Controllers
{
    [Route("vendor/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly UsuarioService _service;
        private readonly TokenService _tokenService;

        public AuthController(UsuarioService service, TokenService tokenService)
        {
            _service = service;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult> Login([FromBody] LoginRequestDTO request)
        {
            var usuarioValido = await _service.ValidarCredenciales(request.Correo, request.Password);

            if (!usuarioValido)
                return Unauthorized(new { mensaje = "Credenciales incorrectas" });

            var response = _tokenService.GenerarToken(request.Correo);
            return Ok(response);
        }
        [HttpPost("sign-up")]
        [AllowAnonymous]
        public async Task<ActionResult> Singup([FromBody] SignupRequestDTO request)
        {
            var nuevoUsuario = await _service.ValidarCredencialesSignUp(request.Nombre, request.Correo, request.Password);
            if (nuevoUsuario == null) return Unauthorized(new { mensaje = "El correo que ingresó ya está registrado" });
            var response = _tokenService.GenerarToken(request.Correo);
            return Ok(response);
        }

    }
}
