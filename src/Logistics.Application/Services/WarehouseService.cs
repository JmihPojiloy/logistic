using Logistics.Application.Interfaces.Repositories;
using Logistics.Application.Interfaces.Services;
using Logistics.Domain.Entities.Warehouses;

namespace Logistics.Application.Services;

public class WarehouseService : IService<Warehouse>
{
    public readonly IService<Warehouse> _warehouseRepository;

    public WarehouseService(IService<Warehouse> warehouseRepository)
    {
        _warehouseRepository = warehouseRepository;
    }
    
    /// <summary>
    /// Получить все склады
    /// </summary>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Неизменяемая коллекция складов</returns>
    public async Task<IReadOnlyList<Warehouse>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _warehouseRepository.GetAllAsync(cancellationToken);
    }

    /// <summary>
    /// Получить склад по Id
    /// </summary>
    /// <param name="warehouseId">Id склада</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Склад</returns>
    public async Task<Warehouse> GetByIdAsync(int warehouseId, CancellationToken cancellationToken)
    {
        return await _warehouseRepository.GetByIdAsync(warehouseId, cancellationToken);
    }

    /// <summary>
    /// Добавить или обновить склад
    /// </summary>
    /// <param name="warehouse">Склад</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Добавленный или измененный склад</returns>
    public async Task<Warehouse> AddOrUpdateAsync(Warehouse warehouse, CancellationToken cancellationToken)
    {
        return await _warehouseRepository.AddOrUpdateAsync(warehouse, cancellationToken);
    }

    /// <summary>
    /// Удалить склад по Id
    /// </summary>
    /// <param name="warehouseId">Id склада</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Id удаленного склада</returns>
    public async Task<int> DeleteAsync(int warehouseId, CancellationToken cancellationToken)
    {
        return await _warehouseRepository.DeleteAsync(warehouseId, cancellationToken);
    }
}