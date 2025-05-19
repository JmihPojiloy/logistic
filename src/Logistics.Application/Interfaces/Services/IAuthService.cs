using Logistics.Domain.Entities.Users;

namespace Logistics.Application.Interfaces.Services;

/// <summary>
/// Интерфейс сервиса авторизации и аутентификации
/// </summary>
public interface IAuthService
{
    Task<User> AddNewUserAsync(UserCredential userCredential, CancellationToken cancellationToken);
    Task<bool> CheckCredentialAsync(int phone, string password, CancellationToken cancellationToken);
    Task<string> LoginAsync(int phone, string password, CancellationToken cancellationToken);
}