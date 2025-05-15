using AutoMapper;
using Logistics.Application.Exceptions;
using Logistics.Application.Interfaces.Filters;
using Logistics.Application.Interfaces.Repositories;
using Logistics.Domain.Entities.Notifications;
using Logistics.Infrastructure.Database;
using Logistics.Infrastructure.DatabaseEntity.Notifications;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Logistics.Infrastructure.Repositories.Notifications;

/// <summary>
/// Класс репозитория доступа к БД для писем
/// </summary>
public class LetterRepository : IRepository<Letter>
{
    private readonly LogisticDbContext _context;
    private readonly IMapper _mapper;

    public LetterRepository(LogisticDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    /// <summary>
    /// Метод получения записи по ID
    /// </summary>
    /// <param name="id">ID записи</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Письмо</returns>
    /// <exception cref="NotFoundException">Ошибка не найденной записи</exception>
    public async Task<Letter> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await _context.Letters
            .AsNoTracking()
            .Include(l => l.Notification)
            .FirstOrDefaultAsync(x => x.Id == id ,cancellationToken);
        if (entity == null) throw new NotFoundException("Letter", id);
        
        return _mapper.Map<Letter>(entity);
    }

    /// <summary>
    /// Метод получения всех записей из БД
    /// </summary>
    /// <param name="filter">Фильтр параметров</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Все письма</returns>
    public async Task<IReadOnlyList<Letter>> GetAllByFilterAsync(IFilter? filter = null,CancellationToken cancellationToken = default)
    {
        var entities = await _context.Letters
            .AsNoTracking()
            .Include(l => l.Notification)
            .ToListAsync(cancellationToken);
        
        return _mapper.Map<IReadOnlyList<Letter>>(entities);
    }

    /// <summary>
    /// Метод добавления или обновления записи в БД
    /// </summary>
    /// <param name="entity">Письмо</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Обновленная или добавленная запись</returns>
    public async Task<Letter> AddOrUpdateAsync(Letter entity, CancellationToken cancellationToken = default)
    {
        EntityEntry<LetterEntity> result;
        var letter = _mapper.Map<LetterEntity>(entity);

        if (letter.Id == 0)
        {
            result = await _context.Letters.AddAsync(letter, cancellationToken);
        }
        else
        {
            result = _context.Letters.Update(letter);
        }
        
        return _mapper.Map<Letter>(result.Entity);
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
        var entity = await _context.Letters
            .FirstOrDefaultAsync(x => x.Id == id ,cancellationToken);
        if (entity == null) throw new NotFoundException("Letter", id);
        
        _context.Letters.Remove(entity);

        return entity.Id;
    }
}