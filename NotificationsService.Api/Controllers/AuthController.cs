using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NotificationsService.Api.Models;

namespace NotificationsService.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var configuredUser = _configuration["Auth:Username"] ?? "admin";
            var configuredPassword = _configuration["Auth:Password"] ?? "admin123";

            if (request.Username != configuredUser || request.Password != configuredPassword)
            {
                return Unauthorized("Usuario o contrasena incorrectos.");
            }

            var jwtKey = _configuration["Jwt:Key"] ?? "exam-products-api-secret-key-change-me";
            var issuer = _configuration["Jwt:Issuer"] ?? "ProductsApi";
            var audience = _configuration["Jwt:Audience"] ?? "ProductsApiUsers";
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, request.Username),
                new Claim(ClaimTypes.Role, "Admin")
            };

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiresIn = "2 horas"
            });
        }
    }
}
