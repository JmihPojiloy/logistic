using Logistics.Domain.Entities.Users;

namespace Logistics.Application.Interfaces.Security;

/// <summary>
/// Интерфейс генерации jwt токена 
/// </summary>
public interface IJwtTokenGenerator
{
    string GenerateJwtToken(UserCredential userCredential);
}