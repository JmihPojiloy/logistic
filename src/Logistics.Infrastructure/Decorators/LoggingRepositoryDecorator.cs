using Logistics.Application.Interfaces.Repositories;
using Logistics.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Logistics.Infrastructure.Decorators;

/// <summary>
/// Класс декоратор для логгирования репозиториев
/// </summary>
/// <typeparam name="T">Декорируемый тип</typeparam>
public class LoggingRepositoryDecorator<T> : IRepository<T> where T : BaseEntity
{
    private readonly IRepository<T> _inner;
    private readonly ILogger _logger;

    public LoggingRepositoryDecorator(IRepository<T> inner, ILogger logger)
    {
        _inner = inner;
        _logger = logger;
    }
    
    public async Task<T> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting {Entity} by ID: {Id}", typeof(T).Name, id);
        return await _inner.GetByIdAsync(id, cancellationToken);
    }

    public async Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting all {Entity}", typeof(T).Name);
        return await _inner.GetAllAsync(cancellationToken);
    }

    public async Task<T> AddOrUpdateAsync(T entity, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Adding or update new {Entity}", typeof(T).Name);
        return await _inner.AddOrUpdateAsync(entity, cancellationToken);
    }

    public Task<int> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting entity with ID: {Id}", id);
        return _inner.DeleteAsync(id, cancellationToken);
    }
}