using Logistics.Domain.Entities.Users;

namespace Logistics.Application.Interfaces.Repositories;

/// <summary>
/// Интерфейс репозитория учетных данных
/// </summary>
public interface IUserCredentialRepository
{
    Task<UserCredential?> GetByPhoneAsync(int phone, CancellationToken cancellationToken);
    Task AddAsync(UserCredential userCredential, CancellationToken cancellationToken);
}