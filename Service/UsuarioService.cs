using Microsoft.AspNetCore.Identity.Data;
using Microsoft.EntityFrameworkCore;
using Vendor.Models;
using Vendor.Repository;
using Vendor.DTOs;
using Vendor.DTOs.Request;

namespace Vendor.Service
{
    public class UsuarioService
    {
        private readonly VendorDbContext _context;

        public UsuarioService(VendorDbContext context)
        {
            _context = context;
        }

        public async Task<Usuario> ValidarUsuario(InicioDeSesionRequest request)
        {
            return await _context.Usuario.SingleOrDefaultAsync(x => x.Correo == request.Correo && x.Password == request.Password);
        }
    }
}
