using AutoMapper;
using Logistics.Application.Exceptions;
using Logistics.Application.Interfaces.Filters;
using Logistics.Application.Interfaces.Repositories;
using Logistics.Domain.Entities.Users;
using Logistics.Infrastructure.Database;
using Logistics.Infrastructure.DatabaseEntity.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Logistics.Infrastructure.Repositories.Users;

/// <summary>
/// Класс репозитория доступа к БД для пользователя
/// </summary>
public class UserRepository : IRepository<User>
{
    private readonly LogisticDbContext _context;
    private readonly IMapper _mapper;

    public UserRepository(LogisticDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    /// <summary>
    /// Метод получения записи по ID
    /// </summary>
    /// <param name="id">ID записи</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Потзователь</returns>
    /// <exception cref="NotFoundException">Ошибка не найденной записи</exception>
    public async Task<User> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await _context.Users
            .AsNoTracking()
            .Include(u => u.Addresses)
            .Include(u => u.Orders)
            .Include(u => u.Notifications)
            .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
        if (entity == null) throw new NotFoundException("User", id);
        
        return _mapper.Map<User>(entity);
    }

    /// <summary>
    /// Метод получения всех записей из БД
    /// </summary>
    /// <param name="filter">Фильтр параметров</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Все пользователи</returns>
    public async Task<IReadOnlyList<User>> GetAllByFilterAsync(IFilter? filter = null, CancellationToken cancellationToken = default)
    {
        var entities = await _context.Users
            .AsNoTracking()
            .Include(u => u.Addresses)
            .Include(u => u.Orders)
            .Include(u => u.Notifications)
            .ToListAsync(cancellationToken);
        
        return _mapper.Map<IReadOnlyList<User>>(entities);
    }

    /// <summary>
    /// Метод добавления или обновления записи в БД
    /// </summary>
    /// <param name="entity">Пользователь</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Обновленная или добавленная запись</returns>
    public async Task<User> AddOrUpdateAsync(User entity, CancellationToken cancellationToken = default)
    {
        EntityEntry<UserEntity> result;
        var user = _mapper.Map<UserEntity>(entity);

        if (user.Id == 0)
        {
            result = await _context.Users.AddAsync(user, cancellationToken);
        }
        else
        {
            result = _context.Users.Update(user);
        }
        
        return _mapper.Map<User>(result.Entity);
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
        var entity = await _context.Users
            .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
        if (entity == null) throw new NotFoundException("User", id);
        _context.Users.Remove(entity);
        
        return entity.Id;
    }
}