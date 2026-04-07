using Vendor.DTOs.Request;
using Vendor.Models;
using Vendor.Repository;

namespace Vendor.Service
{
    public class InversionService
    {
        private readonly VendorDbContext _context;

        public InversionService(VendorDbContext context)
        {
            _context = context;
        }

        public async Task<Inversion> RegistrarInversionCompleta(InversionRequest request)
        {
            //Comenzamos una transacción
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                //Construimos una nueva inversión
                var nuevaInversion = new Inversion
                {
                    Total = 0,
                    Fecha = DateOnly.FromDateTime(DateTime.Now)
                };
                _context.Inversion.Add(nuevaInversion);
                await _context.SaveChangesAsync();

                decimal totalAcumulado = 0;//Inicializamos la variable para guardar el total final de invertido

                //Por cada producto en la lista de inversión
                foreach(var item in request.Detalles)
                {
                    var producto = await _context.Producto.FindAsync(item.ProductoId);//Obtenemos el Id del producto

                    if (producto == null) //verificamos que el producto exista
                        throw new Exception($"El producto con ID {item.ProductoId} no existe.");

                    //Generamos un registro del producto para esta inversión
                    var detalle = new DetalleInversion
                    {
                        InversionID = nuevaInversion.Id,
                        ProductoID = producto.Id,
                        Precio_Compra = producto.PrecioCompra,
                        Subtotal = item.Cantidad * producto.PrecioCompra
                    };

                    producto.Stock += item.Cantidad; //actualizamos el stock
                    totalAcumulado += (decimal)detalle.Subtotal; //actualizamos el total de inversión
                    _context.DetalleInversion.Add(detalle);
                }
                nuevaInversion.Total = totalAcumulado; //obtenemos el total final de inversión
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();//Si no hubieron errores al guardar, se ejecuta el commit de transacción
                return nuevaInversion;
                

            } catch (Exception ex)
            {
                //Si hubo un error en el proceso, la transacción no se completa y se muestra el mensaje de error
                await transaction.RollbackAsync();
                throw new Exception("Error al procesar la inversión: " + ex.Message);
            }
        }
    }
}
