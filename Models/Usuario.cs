using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Vendor.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        [MaxLength(15)]
        public string Nombre { get; set; }
        [EmailAddress]
        public string Correo { get; set; }
        [MinLength(8)]
        public string Password { get; set; }
    }
}
