using AutoMapper;
using Logistics.Application.Exceptions;
using Logistics.Application.Interfaces.Repositories;
using Logistics.Domain.Entities.Payments;
using Logistics.Infrastructure.Database;
using Logistics.Infrastructure.DatabaseEntity.Payments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Logistics.Infrastructure.Repositories.Payments;

/// <summary>
/// Класс репозитория доступа к БД для платежей
/// </summary>
public class PaymentRepository : IRepository<Payment>
{
    private readonly LogisticDbContext _context;
    private readonly IMapper _mapper;

    public PaymentRepository(LogisticDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    /// <summary>
    /// Метод получения записи по ID
    /// </summary>
    /// <param name="id">ID записи</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Платеж</returns>
    /// <exception cref="NotFoundException">Ошибка не найденной записи</exception>
    public async Task<Payment> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await _context.Payments
            .AsNoTracking()
            .Include(p => p.Order)
            .Include(p => p.CancelledPayments)
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
        if (entity == null) throw new NotFoundException("Payment", id);
        
        return _mapper.Map<Payment>(entity);
    }

    /// <summary>
    /// Метод получения всех записей из БД
    /// </summary>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Все платежи</returns>
    public async Task<IReadOnlyList<Payment>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var entities = await _context.Payments
            .AsNoTracking()
            .Include(p => p.Order)
            .Include(p => p.CancelledPayments)
            .ToListAsync(cancellationToken);
        
        return _mapper.Map<IReadOnlyList<Payment>>(entities);
    }

    /// <summary>
    /// Метод добавления или обновления записи в БД
    /// </summary>
    /// <param name="entity">Платеж</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Обновленная или добавленная запись</returns>
    public async Task<Payment> AddOrUpdateAsync(Payment entity, CancellationToken cancellationToken = default)
    {
        EntityEntry<PaymentEntity> result;
        var payment = _mapper.Map<PaymentEntity>(entity);

        if (payment.Id == 0)
        {
            result = await _context.Payments.AddAsync(payment, cancellationToken);
        }
        else
        {
            result = _context.Payments.Update(payment);
        }
        
        return _mapper.Map<Payment>(result.Entity);
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
        var entity = await _context.Payments
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
        if (entity == null) throw new NotFoundException("Payment", id);
        
        _context.Payments.Remove(entity);
        
        return entity.Id;
    }
}