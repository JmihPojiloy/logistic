using Logistics.Domain.Entities.Warehouses;

namespace Logistics.Application.Interfaces.Services;

public interface IWarehouseService
{
    Task<Warehouse> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Warehouse>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Warehouse> AddOrUpdateAsync(Warehouse entity, CancellationToken cancellationToken = default);
    Task<int> DeleteAsync(int id, CancellationToken cancellationToken = default);
}