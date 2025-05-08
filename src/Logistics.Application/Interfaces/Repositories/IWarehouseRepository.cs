using Logistics.Domain.Entities.Warehouses;

namespace Logistics.Application.Interfaces.Repositories;

/// <summary>
/// Интерфейс репозитория склада для доступа к БД
/// </summary>
public interface IWarehouseRepository
{
    public Task<IReadOnlyList<Warehouse>> GetAllWarehousesAsync(CancellationToken cancellationToken = default);
    public Task<Warehouse> GetWarehouseByIdAsync(int warehouseId, CancellationToken cancellationToken = default);
    public Task<Warehouse> AddOrUpdateWarehouseAsync(Warehouse warehouse, CancellationToken cancellationToken = default);
    public Task<int> DeleteWarehouseAsync(int warehouseId, CancellationToken cancellationToken = default);
}