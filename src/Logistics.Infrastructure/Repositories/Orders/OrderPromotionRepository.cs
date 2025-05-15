using AutoMapper;
using Logistics.Application.Exceptions;
using Logistics.Application.Interfaces.Filters;
using Logistics.Application.Interfaces.Repositories;
using Logistics.Domain.Entities.Orders;
using Logistics.Infrastructure.Database;
using Logistics.Infrastructure.DatabaseEntity.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Logistics.Infrastructure.Repositories.Orders;

/// <summary>
/// Класс репозитория доступа к БД для заказ-промоакция
/// </summary>
public class OrderPromotionRepository : IRepository<OrderPromotion>
{
    private readonly LogisticDbContext _context;
    private readonly IMapper _mapper;

    public OrderPromotionRepository(LogisticDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    /// <summary>
    /// Метод получения записи по ID
    /// </summary>
    /// <param name="id">ID записи</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Заказ-Промоакция</returns>
    /// <exception cref="NotFoundException">Ошибка не найденной записи</exception>
    public async Task<OrderPromotion> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await _context.OrderPromotions
            .AsNoTracking()
            .Include(o => o.Order)
            .Include(o => o.Promotion)
            .FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
        if(entity == null) throw new NotFoundException("OrderPromotion", id);
        
        return _mapper.Map<OrderPromotion>(entity);
    }

    /// <summary>
    /// Метод получения всех записей из БД
    /// </summary>
    /// <param name="filter">Фильтр параметров</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Все заказ-промоакция</returns>
    public async Task<IReadOnlyList<OrderPromotion>> GetAllByFilterAsync(IFilter? filter = null,CancellationToken cancellationToken = default)
    {
        var entities = await _context.OrderPromotions
            .AsNoTracking()
            .Include(o => o.Order)
            .Include(o => o.Promotion)
            .ToListAsync(cancellationToken);
        
        return _mapper.Map<IReadOnlyList<OrderPromotion>>(entities);
    }

    /// <summary>
    /// Метод добавления или обновления записи в БД
    /// </summary>
    /// <param name="entity">Заказ-промоакция</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Обновленная или добавленная запись</returns>
    public async Task<OrderPromotion> AddOrUpdateAsync(OrderPromotion entity, CancellationToken cancellationToken = default)
    {
        EntityEntry<OrderPromotionEntity> result;
        var orderPromotion = _mapper.Map<OrderPromotionEntity>(entity);

        if (orderPromotion.Id == 0)
        {
            result = await _context.OrderPromotions.AddAsync(orderPromotion, cancellationToken);
        }
        else
        {
            result = _context.OrderPromotions.Update(orderPromotion);
        }
        
        return _mapper.Map<OrderPromotion>(result.Entity);
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
        var entity = await _context.OrderPromotions
            .FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
        if(entity == null) throw new NotFoundException("OrderPromotion", id);
        
        _context.OrderPromotions.Remove(entity);
        
        return entity.Id;
    }
}