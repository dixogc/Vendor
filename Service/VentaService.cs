using Vendor.DTOs.Request;
using Vendor.Models;
using Vendor.Repository;

namespace Vendor.Service
{
    public class VentaService
    {
        private readonly VendorDbContext _context;

        public VentaService(VendorDbContext context)
        {
            _context = context;
        }

        public async Task<Venta> RegistrarVentaCompleta(VentaRequest request)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var nuevaVenta = new Venta
                {
                    Fecha = DateOnly.FromDateTime(DateTime.Now),
                    Lugar = request.Lugar,
                    MetodoPago = request.MetodoPago,
                    TotalVenta = 0 
                };

                _context.Venta.Add(nuevaVenta);
                await _context.SaveChangesAsync(); 

                decimal totalAcumulado = 0;

                foreach (var item in request.Detalles)
                {
                    var producto = await _context.Producto.FindAsync(item.ProductoId);

                    if (producto == null)
                        throw new Exception($"El producto con ID {item.ProductoId} no existe.");

                    if (producto.Stock < item.Cantidad)
                        throw new Exception($"Stock insuficiente para {producto.NombreProducto}. Disponible: {producto.Stock}");

                    var detalle = new RegistroDeVenta
                    {
                        VentaID = nuevaVenta.Id,
                        ProductoID = producto.Id,
                        PrecioVenta = producto.PrecioVenta, 
                        Cantidad = item.Cantidad,
                        Subtotal = item.Cantidad * producto.PrecioVenta
                    };

                    producto.Stock -= item.Cantidad;

                    totalAcumulado += (decimal)detalle.Subtotal;
                    _context.RegistroDeVenta.Add(detalle);
                }

                nuevaVenta.TotalVenta = totalAcumulado;
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                return nuevaVenta;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception("Error al procesar la venta: " + ex.Message);
            }
        }
    }
}
