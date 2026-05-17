using Microsoft.AspNetCore.Identity.Data;
using Microsoft.EntityFrameworkCore;
using Vendor.Models;
using Vendor.Repository;
using BCrypt;
using BCrypt.Net;
using Org.BouncyCastle.Crypto.Generators;


namespace Vendor.Service
{
    public interface IUsuarioService
    {
        public Task<bool> ValidarCredenciales(string correo, string password);
        public Task<Usuario?> ValidarCredencialesSignUp(string nombre, string correo, string password);
    }
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
            var usuario = await _repository.ValidarCredenciales(correo, password);
            if(!usuario) return false;
            return true;
        }

        public async Task<Usuario?> ValidarCredencialesSignUp(string nombre, string correo, string password)
        {
            var nuevoUsuario = new Usuario
            {
                Nombre = nombre,
                Correo = correo,
                Password = password
            };
            var usuarioExiste = await _repository.ValidarCredenciales(correo, password);
            if (!usuarioExiste) await _repository.RegistrarNuevoUsuario(nuevoUsuario);
            return nuevoUsuario;
        }
    }
}
