using AutoMapper;
using Logistics.Application.Exceptions;
using Logistics.Application.Filters;
using Logistics.Application.Interfaces.Filters;
using Logistics.Application.Interfaces.Repositories;
using Logistics.Domain.Entities.Vehicles;
using Logistics.Domain.Enums;
using Logistics.Infrastructure.Database;
using Logistics.Infrastructure.DatabaseEntity.Vehicles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Logistics.Infrastructure.Repositories.Vehicles;

/// <summary>
/// Класс репозитория доступа к БД для писем
/// </summary>
public class VehicleRepository : IRepository<Vehicle>
{
    private readonly LogisticDbContext _context;
    private readonly IMapper _mapper;

    public VehicleRepository(LogisticDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    /// <summary>
    /// Метод получения записи по ID
    /// </summary>
    /// <param name="id">ID записи</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Транспорт</returns>
    /// <exception cref="NotFoundException">Ошибка не найденной записи</exception>
    public async Task<Vehicle> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var entity = await _context.Vehicles
            .AsNoTracking()
            .Include(x => x.Driver)
            .Include(x => x.DeliveryTracking)
            .Include(x => x.VehicleMaintenance)
            .Include(x => x.Orders)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (entity == null) throw new NotFoundException("Vehicle", id);
        
        return _mapper.Map<Vehicle>(entity);
    }

    /// <summary>
    /// Метод получения всех записей из БД
    /// </summary>
    /// <param name="filter">Фильтр параметров</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Все транспорты</returns>
    public async Task<IReadOnlyList<Vehicle>> GetAllByFilterAsync(IFilter? filter,CancellationToken cancellationToken)
    {
        var query = _context.Vehicles.AsQueryable();

        if (filter is VehicleFilter vehicleFilter)
        {
            if (vehicleFilter.Status != null)
            {
                query = query.Where(x => x.Status == vehicleFilter.Status);
            }    
        }
        
        var entities = await query
            .AsNoTracking()
            .Include(x => x.Driver)
            .Include(x => x.DeliveryTracking)
            .Include(x => x.VehicleMaintenance)
            .Include(x => x.Orders)
            .ToListAsync(cancellationToken);
        
        return _mapper.Map<List<Vehicle>>(entities);
    }

    /// <summary>
    /// Метод добавления или обновления записи в БД
    /// </summary>
    /// <param name="entity">Транспорт</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Обновленная или добавленная запись</returns>
    public async Task<Vehicle> AddOrUpdateAsync(Vehicle entity, CancellationToken cancellationToken = default)
    {
        EntityEntry<VehicleEntity> result;
        var vehicle = _mapper.Map<VehicleEntity>(entity);

        if (vehicle.Id == 0)
        {
            result = await _context.Vehicles.AddAsync(vehicle, cancellationToken);
        }
        else
        {
            result = _context.Vehicles.Update(vehicle);
        }
        
        return _mapper.Map<Vehicle>(result.Entity);
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
        var entity = await _context.Vehicles.FirstOrDefaultAsync(x => x.Id == id,cancellationToken);
        if (entity == null) throw new NotFoundException("Vehicle", id);
        
        _context.Vehicles.Remove(entity);
        
        return entity.Id;
    }
}