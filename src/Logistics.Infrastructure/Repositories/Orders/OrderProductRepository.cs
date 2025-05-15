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
/// Класс репозитория доступа к БД для заказ-товар
/// </summary>
public class OrderProductRepository : IRepository<OrderProduct>
{
    private readonly LogisticDbContext _context;
    private readonly IMapper _mapper;

    public OrderProductRepository(LogisticDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    /// <summary>
    /// Метод получения записи по ID
    /// </summary>
    /// <param name="id">ID записи</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Заказ-Товар</returns>
    /// <exception cref="NotFoundException">Ошибка не найденной записи</exception>
    public async Task<OrderProduct> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await _context.OrderProducts
            .AsNoTracking()
            .Include(o => o.Order)
            .Include(o => o.Product)
            .FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
        if(entity == null) throw new NotFoundException("OrderProduct", id);
        
        return _mapper.Map<OrderProduct>(entity);
    }

    /// <summary>
    /// Метод получения всех записей из БД
    /// </summary>
    /// <param name="filter">Фильтр параметров</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Все заказ-товар</returns>
    public async Task<IReadOnlyList<OrderProduct>> GetAllByFilterAsync(IFilter? filter = null,CancellationToken cancellationToken = default)
    {
        var entities = await _context.OrderProducts
            .AsNoTracking()
            .Include(o => o.Order)
            .Include(o => o.Product)
            .ToListAsync(cancellationToken);
        
        return _mapper.Map<IReadOnlyList<OrderProduct>>(entities);
    }

    /// <summary>
    /// Метод добавления или обновления записи в БД
    /// </summary>
    /// <param name="entity">Заказ-товар</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Обновленная или добавленная запись</returns>
    public async Task<OrderProduct> AddOrUpdateAsync(OrderProduct entity, CancellationToken cancellationToken = default)
    {
        EntityEntry<OrderProductEntity> result;
        var orderProduct = _mapper.Map<OrderProductEntity>(entity);

        if (orderProduct.Id == 0)
        {
            result = await _context.OrderProducts.AddAsync(orderProduct, cancellationToken);
        }
        else
        {
            result = _context.OrderProducts.Update(orderProduct);
        }
        
        return _mapper.Map<OrderProduct>(result.Entity);
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
        var entity = await _context.OrderProducts
            .FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
        if(entity == null) throw new NotFoundException("OrderProduct", id); 
        
        _context.OrderProducts.Remove(entity);
        
        return entity.Id;
    }
}