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
/// Класс репозитория доступа к БД для заказа
/// </summary>
public class OrderRepository : IOrderRepository
{
    private readonly LogisticDbContext _context;
    private readonly IMapper _mapper;

    public OrderRepository(LogisticDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    /// <summary>
    /// Метод получения записи по ID
    /// </summary>
    /// <param name="id">ID записи</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Заказ</returns>
    /// <exception cref="NotFoundException">Ошибка не найденной записи</exception>
    public async Task<Order> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await _context.Orders
            .AsNoTracking()
            .Include(o => o.User)
            .Include(o => o.Vehicle)
            .Include(o => o.Payment)
            .Include(o => o.Address)
            .Include(o => o.OrderProducts)
            .Include(o => o.OrderPromotions)
            .FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
        if (entity == null) throw new NotFoundException("Order", id);
        
        return _mapper.Map<Order>(entity);
    }

    /// <summary>
    /// Метод получения всех записей из БД
    /// </summary>
    /// <param name="filter">Фильтр параметров</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Все заказы</returns>
    public async Task<IReadOnlyList<Order>> GetAllByFilterAsync(IFilter? filter = null,CancellationToken cancellationToken = default)
    {
        var entities = await _context.Orders
            .AsNoTracking()
            .Include(o => o.User)
            .Include(o => o.Vehicle)
            .Include(o => o.Payment)
            .Include(o => o.Address)
            .Include(o => o.OrderProducts)
            .Include(o => o.OrderPromotions)
            .ToListAsync(cancellationToken);
        
        return _mapper.Map<IReadOnlyList<Order>>(entities);
    }

    /// <summary>
    /// Метод добавления или обновления записи в БД
    /// </summary>
    /// <param name="entity">Заказ</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Обновленная или добавленная запись</returns>
    public async Task<Order> AddOrUpdateAsync(Order entity, CancellationToken cancellationToken = default)
    {
        EntityEntry<OrderEntity> result;
        var order = _mapper.Map<OrderEntity>(entity);
        order.Vehicle = null;
        order.Address = null;

        if (order.Id == 0)
        {
            result = await _context.Orders.AddAsync(order, cancellationToken);
        }
        else
        {
            result = _context.Orders.Update(order);
        }
        
        await _context.SaveChangesAsync(cancellationToken);
        
        return _mapper.Map<Order>(result.Entity);
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
        var entity = await _context.Orders
            .FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
        if (entity == null) throw new NotFoundException("Order", id);
        
        _context.Orders.Remove(entity);
        
        return entity.Id;
    }

    /// <summary>
    /// Метод получения заказов для конкретного пользователя
    /// </summary>
    /// <param name="userId">ID пользователя</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Заказы</returns>
    public async Task<IReadOnlyCollection<Order>> GetOrdersByUserIdAsync(int userId, CancellationToken cancellationToken = default)
    {
        var entity = await _context.Orders
            .AsNoTracking()
            .Include(o => o.User)
            .Include(o => o.Vehicle)
            .Include(o => o.Payment)
            .Include(o => o.Address)
            .Include(o => o.OrderProducts)
            .Include(o => o.OrderPromotions)
            .Where(o => o.UserId == userId)
            .ToListAsync(cancellationToken);
        
        return _mapper.Map<IReadOnlyCollection<Order>>(entity);
    }
}