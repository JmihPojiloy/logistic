using Logistics.Domain.Entities.Users;

namespace Logistics.Application.Interfaces.Services;

public interface IUserService
{
    Task<User> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<User>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<User> AddOrUpdateAsync(User entity, CancellationToken cancellationToken = default);
    Task<int> DeleteAsync(int id, CancellationToken cancellationToken = default);
}