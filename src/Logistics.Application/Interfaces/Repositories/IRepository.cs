using System.Linq.Expressions;
using Logistics.Domain.Entities;

namespace Logistics.Application.Interfaces.Repositories;

/// <summary>
/// Интерфейс обобщенного репозитория
/// </summary>
/// <typeparam name="TDom">Domain сущность</typeparam>
public interface IRepository<TDom> where TDom : BaseEntity
{
    Task<TDom> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<TDom>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<TDom> AddOrUpdateAsync(TDom entity, CancellationToken cancellationToken = default);
    Task<int> DeleteAsync(int id, CancellationToken cancellationToken = default);
}