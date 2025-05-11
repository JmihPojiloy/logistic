using Logistics.Domain.Entities.Orders;

namespace Logistics.Application.Interfaces.Repositories;

/// <summary>
/// Интерфейс репозитория заказов
/// </summary>
public interface IOrderRepository : IRepository<Order>
{
    Task<IReadOnlyCollection<Order>> GetOrdersByUserIdAsync(int userId, CancellationToken cancellationToken = default);
}