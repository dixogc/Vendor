using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using Vendor.Models;

namespace Vendor.Repository
{
    public class UsuarioRepository
    {
        private readonly VendorDbContext _context;

        public UsuarioRepository(VendorDbContext context)
        {
            _context = context;
        }

        public async Task RegistrarNuevoUsuario(Usuario usuario)
        {
            usuario.Password = BCrypt.Net.BCrypt.HashPassword(usuario.Password);
            _context.Usuario.Add(usuario);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> ValidarCredenciales(string correo, string password)
        {
            var usuario = await _context.Usuario.FirstOrDefaultAsync(u => u.Correo == correo);
            if (usuario == null)
            {
                return false;
            }
            bool esValida = BCrypt.Net.BCrypt.Verify(password, usuario.Password);

            return esValida;
        }
        
    }
}
