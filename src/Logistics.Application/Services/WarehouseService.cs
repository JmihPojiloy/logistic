using Logistics.Application.Interfaces.Repositories;
using Logistics.Application.Interfaces.Services;
using Logistics.Domain.Entities.Warehouses;

namespace Logistics.Application.Services;

public class WarehouseService : IWarehouseService
{
    public readonly IWarehouseRepository _warehouseRepository;

    public WarehouseService(IWarehouseRepository warehouseRepository)
    {
        _warehouseRepository = warehouseRepository;
    }
    
    /// <summary>
    /// Получить все склады
    /// </summary>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Неизменяемая коллекция складов</returns>
    public async Task<IReadOnlyList<Warehouse>> GetAllWarehousesAsync(CancellationToken cancellationToken)
    {
        return await _warehouseRepository.GetAllWarehousesAsync(cancellationToken);
    }

    /// <summary>
    /// Получить склад по Id
    /// </summary>
    /// <param name="warehouseId">Id склада</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Склад</returns>
    public async Task<Warehouse> GetWarehouseByIdAsync(int warehouseId, CancellationToken cancellationToken)
    {
        return await _warehouseRepository.GetWarehouseByIdAsync(warehouseId, cancellationToken);
    }

    /// <summary>
    /// Добавить или обновить склад
    /// </summary>
    /// <param name="warehouse">Склад</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Добавленный или измененный склад</returns>
    public async Task<Warehouse> AddOrUpdateWarehouseAsync(Warehouse warehouse, CancellationToken cancellationToken)
    {
        return await _warehouseRepository.AddOrUpdateWarehouseAsync(warehouse, cancellationToken);
    }

    /// <summary>
    /// Удалить склад по Id
    /// </summary>
    /// <param name="warehouseId">Id склада</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Id удаленного склада</returns>
    public async Task<int> DeleteWarehouseAsync(int warehouseId, CancellationToken cancellationToken)
    {
        return await _warehouseRepository.DeleteWarehouseAsync(warehouseId, cancellationToken);
    }
}