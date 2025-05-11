using Logistics.Domain.Entities;

namespace Logistics.Application.Interfaces.Services;

public interface IService<TDom> where TDom : BaseEntity
{
    Task<TDom> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<TDom>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<TDom> AddOrUpdateAsync(TDom entity, CancellationToken cancellationToken = default);
    Task<int> DeleteAsync(int id, CancellationToken cancellationToken = default);
}