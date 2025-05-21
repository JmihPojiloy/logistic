using AutoMapper;
using Logistics.Application.Exceptions;
using Logistics.Application.Interfaces.Filters;
using Logistics.Application.Interfaces.Repositories;
using Logistics.Domain.Entities.Notifications;
using Logistics.Infrastructure.Database;
using Logistics.Infrastructure.DatabaseEntity.Notifications;
using Logistics.Infrastructure.DatabaseEntity.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Logistics.Infrastructure.Repositories.Notifications;

/// <summary>
/// Класс репозитория доступа к БД для уведомлений
/// </summary>
public class NotificationRepository : IRepository<Notification>
{
    private readonly LogisticDbContext _context;
    private readonly IMapper _mapper;

    public NotificationRepository(LogisticDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    /// <summary>
    /// Метод получения записи по ID
    /// </summary>
    /// <param name="id">ID записи</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Уведомление</returns>
    /// <exception cref="NotFoundException">Ошибка не найденной записи</exception>
    public async Task<Notification> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await _context.Notifications
            .AsNoTracking()
            .Include(n => n.Recipient)
            .Include(n => n.Letter)
            .FirstOrDefaultAsync(n => n.Id == id, cancellationToken);
        if(entity == null) throw new NotFoundException("Notification", id);
        
        return _mapper.Map<Notification>(entity);
    }

    /// <summary>
    /// Метод получения всех записей из БД
    /// </summary>
    /// <param name="filter">Фильтр параметров</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Все уведомления</returns>
    public async Task<IReadOnlyList<Notification>> GetAllByFilterAsync(IFilter? filter = null,CancellationToken cancellationToken = default)
    {
        var entities = await _context.Notifications
            .AsNoTracking()
            .Include(n => n.Recipient)
            .Include(n => n.Letter)
            .ToListAsync(cancellationToken);
        
        return _mapper.Map<IReadOnlyList<Notification>>(entities);
    }
    
    /// <summary>
    /// Метод добавления или обновления записи в БД
    /// </summary>
    /// <param name="entity">Уведомление</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Обновленная или добавленная запись</returns>
    public async Task<Notification> AddOrUpdateAsync(Notification entity, CancellationToken cancellationToken = default)
    {
        EntityEntry<NotificationEntity> result;
        var notification = _mapper.Map<NotificationEntity>(entity);
        notification.Recipient.Orders = new List<OrderEntity>();
        if (entity.Id == 0)
        {
            result = await _context.Notifications.AddAsync(notification, cancellationToken);
        }
        else
        {
            result = _context.Notifications.Update(notification);
        }
        
        return _mapper.Map<Notification>(result.Entity);
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
        var entity = await _context.Notifications
            .FirstOrDefaultAsync(n => n.Id == id, cancellationToken);
        if(entity == null) throw new NotFoundException("Notification", id);
        
        _context.Notifications.Remove(entity);
        
        return entity.Id;
    }
}