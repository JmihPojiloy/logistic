using Logistics.Application.Interfaces.Services;
using Logistics.Application.Interfaces.UnitOfWork;
using Logistics.Domain.Entities.Users;

namespace Logistics.Application.Services;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;

    public UserService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<User> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var repo = _unitOfWork.GetRepository<User>();
        
        return await repo.GetByIdAsync(id, cancellationToken);
    }

    public async Task<IReadOnlyList<User>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var repo = _unitOfWork.GetRepository<User>();
        
        return await repo.GetAllByFilterAsync(null, cancellationToken);
    }

    public async Task<User> AddOrUpdateAsync(User entity, CancellationToken cancellationToken = default)
    {
        var repo = _unitOfWork.GetRepository<User>();
        var result = await repo.AddOrUpdateAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return result;
    }

    public async Task<int> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var repo = _unitOfWork.GetRepository<User>();
        var result = await repo.DeleteAsync(id, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return result;
    }
}