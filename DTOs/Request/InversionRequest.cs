namespace Vendor.DTOs.Request
{
    public class InversionRequest
    {
        //Del usuario solo se necesita la lista de productos en los que se hizo la inversión
        public List<ProductoVentaDTO> Detalles { get; set; }
    }
}
