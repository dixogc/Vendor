using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Vendor.DTOs.Request;
using Vendor.DTOs.Response;

namespace Vendor.Service
{
    public class TokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public LoginResponseDTO GenerarToken(string username)
        {
            var jwtSettings = _configuration.GetSection("Jwt");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]!));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Claims que viajan dentro del token
            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Name, username)
        };

            var expiration = DateTime.UtcNow.AddMinutes(
                double.Parse(jwtSettings["ExpirationMinutes"]!)
            );

            // Construcción del token
            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: expiration,
                signingCredentials: credentials
            );

            return new LoginResponseDTO
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiracion = expiration
            };
        }
    }
}
