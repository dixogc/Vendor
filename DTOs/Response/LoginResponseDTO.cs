namespace Vendor.DTOs.Response
{
    public class LoginResponseDTO
    {
        public string Token { get; set; } = string.Empty;
        public DateTime Expiracion { get; set; }
    }
}
