using Vendor.DTOs.Request;
using Vendor.Models;
using Vendor.Repository;

namespace Vendor.Service
{
    public class VentaService
    {
        private readonly VendorDbContext _context;
        private readonly MovimientoRepository _movimiento;

        public VentaService(VendorDbContext context, MovimientoRepository movimiento)
        {
            _context = context;
            _movimiento = movimiento;
        }

        public async Task<Venta> RegistrarVentaCompleta(VentaRequest request)
        {
            //Comenzamos una transacción
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                //Construimos una nueva venta
                var nuevaVenta = new Venta
                {
                    Fecha = DateOnly.FromDateTime(DateTime.Now),
                    Lugar = request.Lugar,
                    MetodoPago = request.MetodoPago,
                    TotalVenta = 0 
                };

                _context.Venta.Add(nuevaVenta);
                await _context.SaveChangesAsync(); //Guardamos la venta

                decimal totalAcumulado = 0; //Inicializamos variable para guardar el total de la venta

                //por cada producto vendido...
                foreach (var item in request.Detalles)
                {
                    var producto = await _context.Producto.FindAsync(item.ProductoId); //obtenemos el Id

                    if (producto == null) //verificamos que el producto exista
                        throw new Exception($"El producto con ID {item.ProductoId} no existe.");//NOTA: agregar opción para registrar productos nuevos al hacer el registro

                    if (producto.Stock < item.Cantidad) //verificamos que si tenemos suficiente stock del producto 
                        throw new Exception($"Stock insuficiente para {producto.NombreProducto}. Disponible: {producto.Stock}");

                    //Genereamos el registro de venta para el producto
                    var detalle = new RegistroDeVenta
                    {
                        VentaID = nuevaVenta.Id,
                        ProductoID = producto.Id,
                        PrecioVenta = producto.PrecioVenta, 
                        Cantidad = item.Cantidad,
                        Subtotal = item.Cantidad * producto.PrecioVenta
                    };

                    //Actualizamos el stock del producto
                    producto.Stock -= item.Cantidad;

                    //Sumamos el subtotal al total de venta
                    totalAcumulado += (decimal)detalle.Subtotal;
                    _context.RegistroDeVenta.Add(detalle);
                }

                //Obtenemos el total final de la venta
                nuevaVenta.TotalVenta = totalAcumulado;
                await _context.SaveChangesAsync();

                //Generamos un nuevo movimiento para el manejo de finanzas
                var nuevoMovimiento = new Movimientos
                {
                    Tipo = Tipo.EntradaPorVenta,
                    Monto = nuevaVenta.TotalVenta,
                    Fecha = DateOnly.FromDateTime(DateTime.Now),
                    ReferenciaID = nuevaVenta.Id
                };
                await _movimiento.RegistrarMovimiento(nuevoMovimiento);
                await _context.SaveChangesAsync();

                //Si no hubo ningún error, se hace commit de la transacción, por lo que los datos se guardarán en la BD
                await transaction.CommitAsync();

                return nuevaVenta;
            }
            catch (Exception ex)
            {
                //Si al guardar, alguno de los datos falló, la transacción no se realiza y mostramos el error
                await transaction.RollbackAsync();
                throw new Exception("Error al procesar la venta: " + ex.Message);
            }
        }
    }
}
