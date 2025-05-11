using AutoMapper;
using Logistics.Application.Exceptions;
using Logistics.Application.Interfaces.Repositories;
using Logistics.Domain.Entities.Delivery;
using Logistics.Infrastructure.Database;
using Logistics.Infrastructure.DatabaseEntity.Delivery;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Logistics.Infrastructure.Repositories.Delivery;

/// <summary>
/// Класс репозитория доступа к записи график доставки в БД
/// </summary>
public class DeliveryScheduleRepository : IRepository<DeliverySchedule>
{
    private readonly LogisticDbContext _context;
    private readonly IMapper _mapper;

    public DeliveryScheduleRepository(LogisticDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    /// <summary>
    /// Получить запись по ID
    /// </summary>
    /// <param name="id">ID записи</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>График доставки</returns>
    /// <exception cref="NotFoundException">Ошибка не найденной записи</exception>
    public async Task<DeliverySchedule> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var entity = await _context.DeliverySchedules
            .AsNoTracking()
            .Include(d => d.Order)
            .FirstOrDefaultAsync(d => d.Id == id, cancellationToken);
        if(entity == null) throw new NotFoundException("DeliverySchedule", id);
        
        return _mapper.Map<DeliverySchedule>(entity);
    }

    /// <summary>
    /// Метод получения всех записей из БД
    /// </summary>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Все записи график доставки</returns>
    public async Task<IReadOnlyList<DeliverySchedule>> GetAllAsync(CancellationToken cancellationToken)
    {
        var entities = await _context.DeliverySchedules
            .AsNoTracking()
            .Include(d => d.Order)
            .ToListAsync(cancellationToken);
        
        return _mapper.Map<IReadOnlyList<DeliverySchedule>>(entities);
    }

    /// <summary>
    /// Метод добавления или обновления записи в БЖ
    /// </summary>
    /// <param name="entity">График доставки</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Добавленная или измененная запись</returns>
    public async Task<DeliverySchedule> AddOrUpdateAsync(DeliverySchedule entity, CancellationToken cancellationToken)
    {
        var deliverySchedule = _mapper.Map<DeliveryScheduleEntity>(entity);
        EntityEntry<DeliveryScheduleEntity> result;

        if (deliverySchedule.Id == 0)
        {
            result = await _context.DeliverySchedules.AddAsync(deliverySchedule, cancellationToken);
        }
        else
        {
            result = _context.DeliverySchedules.Update(deliverySchedule);
        }
        
        return _mapper.Map<DeliverySchedule>(result.Entity);
    }

    /// <summary>
    /// Метод удаления записи в БД
    /// </summary>
    /// <param name="id">ID записи</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>ID удаленной записи</returns>
    /// <exception cref="NotFoundException">Ошибка не найденной записи</exception>
    public async Task<int> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var entity = await _context.DeliverySchedules
            .FirstOrDefaultAsync(d => d.Id == id, cancellationToken);
        if(entity == null) throw new NotFoundException("DeliverySchedule", id);
        
        _context.DeliverySchedules.Remove(entity);
        
        return entity.Id;
    }
}