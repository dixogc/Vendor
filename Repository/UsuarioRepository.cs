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
            _context.Usuario.Add(usuario);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> UsuarioExiste(string correo)
        {
            var correoRegistrado = await _context.Usuario.FirstOrDefaultAsync(u => u.Correo == correo);
            if(correoRegistrado == null) return false;
            return true;
        }
        public async Task<bool> ValidarCredenciales(string correo, string password)
        {
            bool CredencialesSonValidas = _context.Usuario
    .Any(u => u.Correo == correo && u.Password == password);
            return CredencialesSonValidas;
        }
    }
}
