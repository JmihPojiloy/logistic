using System.Collections.Concurrent;
using Logistics.Application.Interfaces.Repositories;
using Logistics.Application.Interfaces.UnitOfWork;
using Logistics.Domain.Entities;
using Logistics.Domain.Entities.Products;
using Logistics.Infrastructure.Database;
using Logistics.Infrastructure.Decorators;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;

namespace Logistics.Infrastructure.UnitOfWorks;

/// <summary>
/// Класс реализации управления репозиториями, транзакциями и сохранением в БД
/// </summary>
public class UnitOfWork : IUnitOfWork
{
    private readonly LogisticDbContext _context;
    private readonly IRepositoryFactory _repositoryFactory;
    private readonly ILogger<UnitOfWork> _logger;
    private readonly ConcurrentDictionary<Type, object> _repositories = new();
    private IDbContextTransaction? _transaction;
    
    /// <summary>
    /// Список типов репозиториев которые необходимо задекорировать
    /// </summary>
    private  readonly HashSet<Type> _decoratedTypes = new()
    {
        typeof(Product)
    };

    public UnitOfWork(
        LogisticDbContext context, 
        IRepositoryFactory repositoryFactory,
        ILogger<UnitOfWork> logger)
    {
        _context = context;
        _repositoryFactory = repositoryFactory;
        _logger = logger;
    }
    
    /// <summary>
    /// Метод инициализации репозиториев при первом обращении кэшируется в коллекции
    /// </summary>
    /// <typeparam name="T">Тип репозитория</typeparam>
    /// <returns>Репозиторий</returns>
    public IRepository<T> GetRepository<T>() where T : BaseEntity
    {
        return (IRepository<T>)_repositories.GetOrAdd(typeof(T), _ =>
        {
            var repo = _repositoryFactory.CreateRepository<T>();
            return _decoratedTypes.Contains(typeof(T))
                ? new LoggingRepositoryDecorator<T>(repo, _logger)
                : repo;
        });
    }

    /// <summary>
    /// Очистка кэш-коллекции
    /// </summary>
    public void ResetRepositories() => _repositories.Clear();
    
    /// <summary>
    /// Начинает транзакцию для работы с БД
    /// </summary>
    /// <param name="cancellationToken">Токен отмены</param>
    public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_transaction != null)
        {
            _logger.LogWarning("Transaction already started.");
            return;
        }

        _transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
        _logger.LogInformation("Transaction started.");
    }
    
    /// <summary>
    /// Метод сохраняет транзакцию
    /// </summary>
    /// <param name="cancellationToken">Токен отмены</param>
    public async Task CommitAsync(CancellationToken cancellationToken = default)
    {
        if (_transaction == null)
        {
            _logger.LogWarning("No transaction to commit.");
            return;
        }

        await _context.SaveChangesAsync(cancellationToken);
        await _transaction.CommitAsync(cancellationToken);
        _logger.LogInformation("Transaction committed.");
        await _transaction.DisposeAsync();
        _transaction = null;
    }
    
    /// <summary>
    /// Метод отката транзакции при ошибке
    /// </summary>
    /// <param name="cancellationToken">Токен отмены</param>
    public async Task RollbackAsync(CancellationToken cancellationToken = default)
    {
        if (_transaction == null)
        {
            _logger.LogWarning("No transaction to rollback.");
            return;
        }

        await _transaction.RollbackAsync(cancellationToken);
        _logger.LogInformation("Transaction rolled back.");
        await _transaction.DisposeAsync();
        _transaction = null;
    }
    
    /// <summary>
    /// Метод сохранения изменений в БД 
    /// </summary>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Результат успешного или не успешного созранения</returns>
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _context.SaveChangesAsync(cancellationToken);
    }
    
    /// <summary>
    /// Метод освобождения ресурсов после работы с БД
    /// </summary>
    public void Dispose()
    {
        _transaction?.Dispose();
        _context.Dispose();
    }
}