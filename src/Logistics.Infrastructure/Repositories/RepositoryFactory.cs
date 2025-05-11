using System.Collections.Concurrent;
using Logistics.Application.Interfaces.Repositories;
using Logistics.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace Logistics.Infrastructure.Repositories;

/// <summary>
/// Класс реализует фабрику репозиториев
/// </summary>
public class RepositoryFactory : IRepositoryFactory
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ConcurrentDictionary<Type, Type> _customRepoTypes = new();

    public RepositoryFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        
        // определяем специализированные репозитории не использующие напрямую IRepository<> 
        // например IOrderRepository : IRepository<Order>
        var repoInterfaces = typeof(RepositoryFactory).Assembly
            .GetTypes()
            .Where(t => t is { IsInterface: true } &&
                        t.IsGenericType == false && 
                        t.Name.StartsWith("I") &&
                        t.Name.EndsWith("Repository"));

        foreach (var iface in repoInterfaces)
        {
            var impl = typeof(RepositoryFactory).Assembly
                .GetTypes()
                .FirstOrDefault(t =>
                    t.IsClass &&
                    !t.IsAbstract &&
                    iface.IsAssignableFrom(t));

            if (impl != null)
                _customRepoTypes.TryAdd(iface, impl);
        }
    }
    
    /// <summary>
    /// Метод создает репозиторий по типу, определяя интерфейс generic или нет
    /// </summary>
    /// <typeparam name="TDom">Тип репозитория</typeparam>
    /// <returns>Репозиторий</returns>
    /// <exception cref="InvalidOperationException">Ошибка при отсутствии репозитория</exception>
    public IRepository<TDom> CreateRepository<TDom>() where TDom : BaseEntity
    {
        // Поиск конкретного кастомного репозитория типа IProductRepository : IRepository<Product>
        var customRepoInterface = _customRepoTypes
            .Keys
            .FirstOrDefault(k =>
                typeof(IRepository<TDom>).IsAssignableFrom(k) &&
                typeof(TDom).Name == k.Name.Replace("Repository", string.Empty));

        if (customRepoInterface != null)
        {
            var implType = _customRepoTypes[customRepoInterface];
            return (IRepository<TDom>)ActivatorUtilities.CreateInstance(_serviceProvider, implType);
        }

        // Поиск generic реализации IRepository<TDom>
        var genericRepoType = typeof(IRepository<>).MakeGenericType(typeof(TDom));

        var impl = typeof(RepositoryFactory).Assembly
            .GetTypes()
            .FirstOrDefault(t =>
                genericRepoType.IsAssignableFrom(t) &&
                t is { IsClass: true, IsAbstract: false });

        if (impl == null)
            throw new InvalidOperationException($"No repository found for {typeof(TDom).Name}");

        return (IRepository<TDom>)ActivatorUtilities.CreateInstance(_serviceProvider, impl);
    }
}