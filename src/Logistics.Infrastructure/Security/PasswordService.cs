using Logistics.Application.Interfaces.Services;
using Microsoft.AspNetCore.Identity;

namespace Logistics.Infrastructure.Security;

/// <summary>
/// Класс реализация работы с паролями
/// </summary>
public class PasswordService : IPasswordService
{
    private readonly PasswordHasher<object?> _hasher = new();
    
    /// <summary>
    /// Хэширование пароля
    /// </summary>
    /// <param name="password">Пароль</param>
    /// <returns>Захэшированный пароль</returns>
    public string HashPassword(string password)
    {
        return _hasher.HashPassword(null, password);
    }

    /// <summary>
    /// Проверка пароля
    /// </summary>
    /// <param name="hashedPassword">Хэшированный пароль из БД</param>
    /// <param name="password">Пароль для проверки</param>
    /// <returns>true/false</returns>
    public bool VerifyPassword(string hashedPassword, string password)
    {
        var result = _hasher.VerifyHashedPassword(null, hashedPassword, password);
        return result == PasswordVerificationResult.Success;
    }
}