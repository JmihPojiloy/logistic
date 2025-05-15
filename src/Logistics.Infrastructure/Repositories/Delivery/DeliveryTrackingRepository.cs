using AutoMapper;
using Logistics.Application.Exceptions;
using Logistics.Application.Interfaces.Filters;
using Logistics.Application.Interfaces.Repositories;
using Logistics.Domain.Entities.Delivery;
using Logistics.Infrastructure.Database;
using Logistics.Infrastructure.DatabaseEntity.Delivery;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Logistics.Infrastructure.Repositories.Delivery;

/// <summary>
/// Класс репозитория доступа к записи отслеживание доставки в БД
/// </summary>
public class DeliveryTrackingRepository : IRepository<DeliveryTracking>
{
    private readonly LogisticDbContext _context;
    private readonly IMapper _mapper;

    public DeliveryTrackingRepository(LogisticDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    /// <summary>
    /// Метод получения записи по ID
    /// </summary>
    /// <param name="id">ID записи</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Отслеживание доставки</returns>
    /// <exception cref="NotFoundException">Ошибка не найденной записи</exception>
    public async Task<DeliveryTracking> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var entity = await _context.DeliveryTrackings
            .AsNoTracking()
            .Include(d => d.Vehicle)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (entity == null) throw new NotFoundException("Delivery tracking", id);
        
        return _mapper.Map<DeliveryTracking>(entity);
    }

    /// <summary>
    /// Метод получения всех записей из БД
    /// </summary>
    /// <param name="filter">Фильтр параметров</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns></returns>
    public async Task<IReadOnlyList<DeliveryTracking>> GetAllByFilterAsync(IFilter? filter = null,CancellationToken cancellationToken = default)
    {
        var entities = await _context.DeliveryTrackings
            .AsNoTracking()
            .Include(d => d.Vehicle)
            .ToListAsync(cancellationToken);
        
        return _mapper.Map<IReadOnlyList<DeliveryTracking>>(entities);
    }

    /// <summary>
    /// Метод добавления и обновления записи в БД
    /// </summary>
    /// <param name="entity">Отслеживание доставки</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Обновленная или добавленная запись</returns>
    public async Task<DeliveryTracking> AddOrUpdateAsync(DeliveryTracking entity, CancellationToken cancellationToken)
    {
        EntityEntry<DeliveryTrackingEntity> result;
        var deliveryTracking = _mapper.Map<DeliveryTrackingEntity>(entity);

        if (deliveryTracking.Id == 0)
        {
            result = await _context.DeliveryTrackings.AddAsync(deliveryTracking, cancellationToken);
        }
        else
        {
            result = _context.DeliveryTrackings.Update(deliveryTracking);
        }
        
        return _mapper.Map<DeliveryTracking>(result.Entity);
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
        var entity = await _context.DeliveryTrackings
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (entity == null) throw new NotFoundException("Delivery tracking", id);
        
        _context.DeliveryTrackings.Remove(entity);
        
        return entity.Id;
    }
}