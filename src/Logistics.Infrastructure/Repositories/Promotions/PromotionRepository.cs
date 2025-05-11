using AutoMapper;
using Logistics.Application.Exceptions;
using Logistics.Application.Interfaces.Repositories;
using Logistics.Domain.Entities.Promotions;
using Logistics.Infrastructure.Database;
using Logistics.Infrastructure.DatabaseEntity.Promotions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Logistics.Infrastructure.Repositories.Promotions;

/// <summary>
/// Класс репозитория доступа к БД для промоакции
/// </summary>
public class PromotionRepository : IRepository<Promotion>
{
    private readonly LogisticDbContext _context;
    private readonly IMapper _mapper;

    public PromotionRepository(LogisticDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    /// <summary>
    /// Метод получения записи по ID
    /// </summary>
    /// <param name="id">ID записи</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Промоакция</returns>
    /// <exception cref="NotFoundException">Ошибка не найденной записи</exception>
    public async Task<Promotion> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await _context.Promotions
            .AsNoTracking()
            .Include(p => p.OrderPromotions)
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
        if(entity == null) throw new NotFoundException("Promotion", id);
        
        return _mapper.Map<Promotion>(entity);
    }

    /// <summary>
    /// Метод получения всех записей из БД
    /// </summary>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Все акции</returns>
    public async Task<IReadOnlyList<Promotion>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var entities = await _context.Promotions
            .AsNoTracking()
            .Include(p => p.OrderPromotions)
            .ToListAsync(cancellationToken);
        
        return _mapper.Map<IReadOnlyList<Promotion>>(entities);
    }

    /// <summary>
    /// Метод добавления или обновления записи в БД
    /// </summary>
    /// <param name="entity">Акция</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Обновленная или добавленная запись</returns>
    public async Task<Promotion> AddOrUpdateAsync(Promotion entity, CancellationToken cancellationToken = default)
    {
        EntityEntry<PromotionEntity> result;
        var promotion = _mapper.Map<PromotionEntity>(entity);

        if (promotion.Id == 0)
        {
            result = await _context.Promotions.AddAsync(promotion, cancellationToken);
        }
        else
        {
            result = _context.Promotions.Update(promotion);
        }
        
        return _mapper.Map<Promotion>(result.Entity);
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
        var entity = await _context.Promotions
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
        if(entity == null) throw new NotFoundException("Promotion", id);
        _context.Promotions.Remove(entity);
        
        return entity.Id;
    }
}