using AutoMapper;
using Logistics.Application.Exceptions;
using Logistics.Application.Interfaces.Filters;
using Logistics.Application.Interfaces.Repositories;
using Logistics.Domain.Entities.Warehouses;
using Logistics.Infrastructure.Database;
using Logistics.Infrastructure.DatabaseEntity.Warehouses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Logistics.Infrastructure.Repositories.Warehouses;

/// <summary>
/// Класс репозитория доступа к БД для остатков на складе
/// </summary>
public class InventoryRepository : IRepository<Inventory>
{
    private readonly LogisticDbContext _context;
    private readonly IMapper _mapper;

    public InventoryRepository(LogisticDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    /// <summary>
    /// Метод получения записи по ID
    /// </summary>
    /// <param name="id">ID записи</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Остатки на складе</returns>
    /// <exception cref="NotFoundException">Ошибка не найденной записи</exception>
    public async Task<Inventory> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await _context.Inventories
            .AsNoTracking()
            .Include(i => i.Product)
            .Include(i => i.Warehouse)
            .FirstOrDefaultAsync(i => i.Id == id, cancellationToken);
        if (entity == null) throw new NotFoundException("Inventory", id);
        
        return _mapper.Map<Inventory>(entity);
    }

    /// <summary>
    /// Метод получения всех записей из БД
    /// </summary>
    /// <param name="filter">Фильтр параметров</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Все остатки на складе</returns>
    public async Task<IReadOnlyList<Inventory>> GetAllByFilterAsync(IFilter? filter = null,CancellationToken cancellationToken = default)
    {
        var entities = await _context.Inventories
            .AsNoTracking()
            .Include(i => i.Product)
            .Include(i => i.Warehouse)
            .ToListAsync(cancellationToken);
        
        return _mapper.Map<IReadOnlyList<Inventory>>(entities);
    }

    /// <summary>
    /// Метод добавления или обновления записи в БД
    /// </summary>
    /// <param name="entity">Остатки на складе</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Обновленная или добавленная запись</returns>
    public async Task<Inventory> AddOrUpdateAsync(Inventory entity, CancellationToken cancellationToken = default)
    {
        EntityEntry<InventoryEntity> result;
        var inventory = _mapper.Map<InventoryEntity>(entity);

        if (inventory.Id == 0)
        {
            result = await _context.Inventories.AddAsync(inventory, cancellationToken);
        }
        else
        {
            result = _context.Inventories.Update(inventory);
        }
        
        return _mapper.Map<Inventory>(result);
    }

    /// <summary>
    /// Метод удаления записи в БД
    /// </summary>
    /// <param name="id">ID записи</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>ID удаленной записи</returns>
    /// <exception cref="NotFoundException">Ошибка не найденной записи</exception>
    public async Task<int> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await _context.Inventories
            .FirstOrDefaultAsync(i => i.Id == id, cancellationToken);
        if (entity == null) throw new NotFoundException("Inventory", id);
        _context.Inventories.Remove(entity);
        
        return await _context.SaveChangesAsync(cancellationToken);
    }
}