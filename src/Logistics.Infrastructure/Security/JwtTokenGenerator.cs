using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Logistics.Application.Interfaces.Security;
using Logistics.Domain.Entities.Users;
using Logistics.Domain.Enums;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Logistics.Infrastructure.Security;

/// <summary>
/// Класс реализации генератора jwt токена
/// </summary>
public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly IConfiguration _configuration;

    public JwtTokenGenerator(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    /// <summary>
    /// Метод генерации jwn токена на основе учетных данных пользователя
    /// </summary>
    /// <param name="userCredential">Учетные данные пользователя</param>
    /// <returns>JWT токен</returns>
    public string GenerateJwtToken(UserCredential userCredential)
    {
        var jwtSettings = _configuration.GetSection("JwtSettings");
        var secretKey = Encoding.UTF8.GetBytes(jwtSettings["Secret"] ?? string.Empty);

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, userCredential.UserId.ToString()),
            new(ClaimTypes.NameIdentifier, userCredential.Id.ToString()),
            new(ClaimTypes.Role, userCredential.Role.ToString()),
            new(ClaimTypes.MobilePhone, userCredential.Phone.ToString())
        };
        
        if (userCredential.Role == UserRole.Admin)
        {
            claims.Add(new Claim(ClaimTypes.Role, UserRole.Manager.ToString()));
            claims.Add(new Claim(ClaimTypes.Role, UserRole.User.ToString()));
        }
        
        var credentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(double.Parse(jwtSettings["ExpiresInMinutes"] ?? "30")),
            Issuer = jwtSettings["Issuer"],
            Audience = jwtSettings["Audience"],
            SigningCredentials = credentials
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        
        return tokenHandler.WriteToken(token);
    }
}