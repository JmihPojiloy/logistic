using Logistics.Application.Interfaces.Repositories;
using Logistics.Application.Interfaces.Services;
using Logistics.Application.Interfaces.UnitOfWork;
using Logistics.Domain.Entities.Warehouses;

namespace Logistics.Application.Services;

public class WarehouseService : IWarehouseService
{
    private readonly IUnitOfWork _unitOfWork;

    public WarehouseService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    /// <summary>
    /// Получить все склады
    /// </summary>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Неизменяемая коллекция складов</returns>
    public async Task<IReadOnlyList<Warehouse>> GetAllAsync(CancellationToken cancellationToken)
    {
        var repo = _unitOfWork.GetRepository<Warehouse>();
        return await repo.GetAllByFilterAsync(null, cancellationToken);
    }

    /// <summary>
    /// Получить склад по Id
    /// </summary>
    /// <param name="warehouseId">Id склада</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Склад</returns>
    public async Task<Warehouse> GetByIdAsync(int warehouseId, CancellationToken cancellationToken)
    {
        var repo = _unitOfWork.GetRepository<Warehouse>();
        return await repo.GetByIdAsync(warehouseId, cancellationToken);
    }

    /// <summary>
    /// Добавить или обновить склад
    /// </summary>
    /// <param name="warehouse">Склад</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Добавленный или измененный склад</returns>
    public async Task<Warehouse> AddOrUpdateAsync(Warehouse warehouse, CancellationToken cancellationToken)
    {
        var repo = _unitOfWork.GetRepository<Warehouse>();
        var result = await repo.AddOrUpdateAsync(warehouse, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return result;
    }

    /// <summary>
    /// Удалить склад по Id
    /// </summary>
    /// <param name="warehouseId">Id склада</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Id удаленного склада</returns>
    public async Task<int> DeleteAsync(int warehouseId, CancellationToken cancellationToken)
    {
        var repo = _unitOfWork.GetRepository<Warehouse>();
        var result =  await repo.DeleteAsync(warehouseId, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return result;
    }
}