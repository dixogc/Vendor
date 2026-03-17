using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Vendor.Models;


namespace Vendor.Repository
{
    public class ProductoRepository
    {
        private readonly VendorDbContext _context;

        public ProductoRepository(VendorDbContext context)
        {
            _context = context;
        }

        public async Task CrearProducto(Producto producto)
        {
            _context.Producto.Add(producto);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Producto>> ObtenerTodosLosProductos()
        {
            var productos = new List<Producto>(); 
            productos = _context.Producto.ToList();
            return productos;
        }

        public async Task<Producto> ObtenerPorId(int id)
        {
            var producto = await _context.Producto.FindAsync(id);
            return producto;
        }

        public async Task<List<Producto>> ProductosBajoStock()
        {
            var productos = await _context.Producto.Where(p => p.Stock <= p.StockMinimo).ToListAsync();
            return productos;
        }
        public async Task EditarProducto(Producto producto)
        {
            _context.Entry(producto).State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }

        public async Task EliminarProducto(Producto producto)
        {
            _context.Producto.Remove(producto);
            await _context.SaveChangesAsync();
        }

    }
}
