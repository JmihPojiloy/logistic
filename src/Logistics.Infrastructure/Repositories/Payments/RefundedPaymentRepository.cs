using AutoMapper;
using Logistics.Application.Exceptions;
using Logistics.Application.Interfaces.Filters;
using Logistics.Application.Interfaces.Repositories;
using Logistics.Domain.Entities.Payments;
using Logistics.Infrastructure.Database;
using Logistics.Infrastructure.DatabaseEntity.Payments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Logistics.Infrastructure.Repositories.Payments;

/// <summary>
/// Класс репозитория доступа к БД для отмененного платежа
/// </summary>
public class RefundedPaymentRepository : IRepository<RefundedPayment>
{
    private readonly LogisticDbContext _context;
    private readonly IMapper _mapper;

    public RefundedPaymentRepository(LogisticDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    /// <summary>
    /// Метод получения записи по ID
    /// </summary>
    /// <param name="id">ID записи</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Отмененный платеж</returns>
    /// <exception cref="NotFoundException">Ошибка не найденной записи</exception>
    public async Task<RefundedPayment> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await _context.RefundedPayments
            .AsNoTracking()
            .Include(r => r.Payment)
            .FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
        if(entity == null) throw new NotFoundException("Refunded Payment", id);
        
        return _mapper.Map<RefundedPayment>(entity);
    }

    /// <summary>
    /// Метод получения всех записей из БД
    /// </summary>
    /// <param name="filter">Фильтр параметров</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Все отмененные платежи</returns>
    public async Task<IReadOnlyList<RefundedPayment>> GetAllByFilterAsync(IFilter? filter = null,CancellationToken cancellationToken = default)
    {
        var entities = await _context.RefundedPayments
            .AsNoTracking()
            .Include(r => r.Payment)
            .ToListAsync(cancellationToken);
        
        return _mapper.Map<IReadOnlyList<RefundedPayment>>(entities);
    }

    /// <summary>
    /// Метод добавления или обновления записи в БД
    /// </summary>
    /// <param name="entity">Отмененный платеж</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Обновленная или добавленная запись</returns>
    public async Task<RefundedPayment> AddOrUpdateAsync(RefundedPayment entity, CancellationToken cancellationToken = default)
    {
        EntityEntry<RefundedPaymentEntity> result;
        var refundedPayment = _mapper.Map<RefundedPaymentEntity>(entity);

        if (refundedPayment.Id == 0)
        {
            result = await _context.RefundedPayments.AddAsync(refundedPayment, cancellationToken);
        }
        else
        {
            result = _context.RefundedPayments.Update(refundedPayment);
        }
        
        return _mapper.Map<RefundedPayment>(result.Entity);
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
        var entity = await _context.RefundedPayments
            .FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
        if(entity == null) throw new NotFoundException("Refunded Payment", id);
        _context.RefundedPayments.Remove(entity);
        
        return entity.Id;
    }
}