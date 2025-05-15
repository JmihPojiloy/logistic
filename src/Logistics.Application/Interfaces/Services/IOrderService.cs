using Logistics.Domain.Entities.Orders;

namespace Logistics.Application.Interfaces.Services;

public interface IOrderService
{
    Task<IReadOnlyCollection<Order>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Order> AddOrUpdateAsync(Order order, CancellationToken cancellationToken = default);
    Task<int> DeleteAsync(int id, CancellationToken cancellationToken = default);
    Task<Order> PayOrderAsync(Order order, CancellationToken cancellationToken = default);
}