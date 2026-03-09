using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;


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

        public async Task<Producto> ObtenerPorId(int id)
        {
            var producto = await _context.Producto.FindAsync(id);
            return producto;
        }

    }
}
