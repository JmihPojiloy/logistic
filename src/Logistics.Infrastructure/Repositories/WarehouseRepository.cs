using AutoMapper;
using Logistics.Application.Exceptions;
using Logistics.Application.Interfaces.Repositories;
using Logistics.Domain.Entities.Products;
using Logistics.Domain.Entities.Warehouses;
using Logistics.Infrastructure.Database;
using Logistics.Infrastructure.DatabaseEntity.Warehouses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Logistics.Infrastructure.Repositories;

public class WarehouseRepository : IWarehouseRepository
{
    private readonly LogisticDbContext _context;
    private readonly IMapper _mapper;

    public WarehouseRepository(LogisticDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    /// <summary>
    /// Метод получения все записей складов из БД
    /// </summary>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Неизменяемый список складов</returns>
    public async Task<IReadOnlyList<Warehouse>> GetAllWarehousesAsync(CancellationToken cancellationToken)
    {
        var warehouseEntities = await _context.Warehouses
            .AsNoTracking()
            .Include(w => w.Inventories)
            .ThenInclude(i => i.Product)
            .Include(w => w.Address)
            .ToListAsync(cancellationToken: cancellationToken);
        
        return _mapper.Map<List<Warehouse>>(warehouseEntities);
    }

    /// <summary>
    /// Метод получения записи склада по Id
    /// </summary>
    /// <param name="warehouseId">Id склада</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Склад</returns>
    /// <exception cref="NotFoundException">Ошибка в случае не найденной записи</exception>
    public async Task<Warehouse> GetWarehouseByIdAsync(int warehouseId, CancellationToken cancellationToken)
    {
        var warehouseEntity = await _context.Warehouses
            .AsNoTracking()
            .Include(w => w.Inventories)
            .ThenInclude(i => i.Product)
            .Include(w => w.Address)
            .FirstOrDefaultAsync(w => w.Id == warehouseId, cancellationToken);
        if(warehouseEntity == null) throw new NotFoundException("Warehouse", warehouseId);
        
        return _mapper.Map<Warehouse>(warehouseEntity);
    }

    /// <summary>
    /// Метод добавления или удаления записи склада в БД
    /// </summary>
    /// <param name="warehouse">Склад</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Добавленный или обновленный склад</returns>
    public async Task<Warehouse> AddOrUpdateWarehouseAsync(Warehouse warehouse, CancellationToken cancellationToken)
    {
        var warehouseEntity = _mapper.Map<WarehouseEntity>(warehouse);
        EntityEntry<WarehouseEntity> result;
        
        if (warehouseEntity.Id == 0)
        {
            warehouseEntity.CreatedOn = DateTime.Now;
            result = await _context.Warehouses
                .AddAsync(warehouseEntity, cancellationToken);
        }
        else
        {
            result = _context.Warehouses.Update(warehouseEntity);
        }

        await _context.SaveChangesAsync(cancellationToken);
        
        return _mapper.Map<Warehouse>(result.Entity);
    }

    /// <summary>
    /// Метод удаления записи склад в БД
    /// </summary>
    /// <param name="warehouseId">Id склада</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Id удаленной записи</returns>
    /// <exception cref="NotFoundException">Ошибка в случае не найденной записи</exception>
    public async Task<int> DeleteWarehouseAsync(int warehouseId, CancellationToken cancellationToken)
    {
        var warehouseEntity = await _context.Warehouses
            .FirstOrDefaultAsync(w => w.Id == warehouseId, cancellationToken: cancellationToken);
        if (warehouseEntity == null) throw new NotFoundException("Warehouse", warehouseId);
        _context.Warehouses.Remove(warehouseEntity);
        await _context.SaveChangesAsync(cancellationToken);
        
        return warehouseEntity.Id;
    }
}