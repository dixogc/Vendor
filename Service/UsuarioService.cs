using Microsoft.AspNetCore.Identity.Data;
using Microsoft.EntityFrameworkCore;
using Vendor.Models;
using Vendor.Repository;
using BCrypt;
using BCrypt.Net;
using Org.BouncyCastle.Crypto.Generators;


namespace Vendor.Service
{
    public class UsuarioService
    {
        private readonly VendorDbContext _context;
        private readonly UsuarioRepository _repository;

        public UsuarioService(VendorDbContext context, UsuarioRepository repository)
        {
            _context = context;
            _repository = repository;
        }

        public async Task<bool> ValidarCredenciales(string correo, string password)
        {
            var usuario = await _repository.UsuarioExiste(correo);
            if(!usuario) return false;
            return await _repository.ValidarCredenciales(correo, BCrypt.Net.BCrypt.HashPassword(password));
        }

        public async Task<Usuario?> ValidarCredencialesSignUp(string nombre, string correo, string password)
        {
            var nuevoUsuario = new Usuario
            {
                Nombre = nombre,
                Correo = correo,
                Password = BCrypt.Net.BCrypt.HashPassword(password)
            };
            var usuarioExiste = await _repository.UsuarioExiste(correo);
            if (!usuarioExiste) await _repository.RegistrarNuevoUsuario(nuevoUsuario);
            return nuevoUsuario;
        }
    }
}
