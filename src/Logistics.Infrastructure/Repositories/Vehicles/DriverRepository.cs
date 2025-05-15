using AutoMapper;
using Logistics.Application.Exceptions;
using Logistics.Application.Filters;
using Logistics.Application.Interfaces.Filters;
using Logistics.Application.Interfaces.Repositories;
using Logistics.Domain.Entities.Vehicles;
using Logistics.Infrastructure.Database;
using Logistics.Infrastructure.DatabaseEntity.Vehicles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Logistics.Infrastructure.Repositories.Vehicles;

/// <summary>
/// Класс репозитория доступа к БД для водителей
/// </summary>
public class DriverRepository : IRepository<Driver>
{
    private readonly LogisticDbContext _context;
    private readonly IMapper _mapper;

    public DriverRepository(LogisticDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    /// <summary>
    /// Метод получения записи по ID
    /// </summary>
    /// <param name="id">ID записи</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Водитель</returns>
    /// <exception cref="NotFoundException">Ошибка не найденной записи</exception>
    public async Task<Driver> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await _context.Drivers
            .AsNoTracking()
            .Include(d => d.Vehicle)
            .FirstOrDefaultAsync(d => d.Id == id, cancellationToken);
        if (entity == null) throw new NotFoundException("Driver", id);
        
        return _mapper.Map<Driver>(entity);
    }

    /// <summary>
    /// Метод получения всех записей из БД
    /// </summary>
    /// <param name="filter">Фильтр параметров</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Все водители</returns>
    public async Task<IReadOnlyList<Driver>> GetAllByFilterAsync(IFilter? filter,CancellationToken cancellationToken)
    {
        var query = _context.Drivers.AsQueryable();
        
        if (filter is DriverFilter driverFilter)
        {
            if (driverFilter.Status != null)
            {
                query = query.Where(d => d.Status == driverFilter.Status);
            }
        }
        var entities = await query
            .AsNoTracking()
            .Include(d => d.Vehicle)
            .ToListAsync(cancellationToken);
        
        return _mapper.Map<IReadOnlyList<Driver>>(entities);
    }

    /// <summary>
    /// Метод добавления или обновления записи в БД
    /// </summary>
    /// <param name="entity">Водитель</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Обновленная или добавленная запись</returns>
    public async Task<Driver> AddOrUpdateAsync(Driver entity, CancellationToken cancellationToken = default)
    {
        EntityEntry<DriverEntity> result;
        var driver = _mapper.Map<DriverEntity>(entity);

        if (driver.Id == 0)
        {
            result = await _context.Drivers.AddAsync(driver, cancellationToken);
        }
        else
        {
            result = _context.Drivers.Update(driver);
        }
        
        return _mapper.Map<Driver>(result.Entity);
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
        var entity = await _context.Drivers
            .FirstOrDefaultAsync(d => d.Id == id, cancellationToken);
        if (entity == null) throw new NotFoundException("Driver", id);
        _context.Drivers.Remove(entity);
        
        return entity.Id;
    }
}