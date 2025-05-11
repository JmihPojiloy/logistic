using Logistics.Application.Interfaces.Repositories;
using Logistics.Domain.Entities;

namespace Logistics.Application.Interfaces.UnitOfWork;

/// <summary>
/// Интерфейс сущности для управления репозиториями, транзакциями и сохранением в БД
/// </summary>
public interface IUnitOfWork
{
    IRepository<T> GetRepository<T>() where T : BaseEntity;
    public void ResetRepositories();
    public Task BeginTransactionAsync(CancellationToken cancellationToken = default);
    public Task CommitAsync(CancellationToken cancellationToken = default);
    public Task RollbackAsync(CancellationToken cancellationToken = default);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    public void Dispose();
}