namespace Logistics.Application.Interfaces.Services;

/// <summary>
/// Интерфейс для работы с паролями
/// </summary>
public interface IPasswordService
{
    string HashPassword(string password);
    bool VerifyPassword(string hashedPassword, string password);
}