using AutoMapper;
using Logistics.Application.Exceptions;
using Logistics.Application.Interfaces.Filters;
using Logistics.Application.Interfaces.Repositories;
using Logistics.Domain.Entities.Vehicles;
using Logistics.Infrastructure.Database;
using Logistics.Infrastructure.DatabaseEntity.Vehicles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Logistics.Infrastructure.Repositories.Vehicles;

/// <summary>
/// Класс репозитория доступа к БД для ТО
/// </summary>
public class VehicleMaintenanceRepository : IRepository<VehicleMaintenance>
{
    private readonly LogisticDbContext _context;
    private readonly IMapper _mapper;

    public VehicleMaintenanceRepository(LogisticDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    /// <summary>
    /// Метод получения записи по ID
    /// </summary>
    /// <param name="id">ID записи</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>ТО</returns>
    /// <exception cref="NotFoundException">Ошибка не найденной записи</exception>
    public async Task<VehicleMaintenance> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await _context.VehicleMaintenances
            .AsNoTracking()
            .Include(v => v.Vehicle)
            .FirstOrDefaultAsync(v => v.Id == id, cancellationToken);
        if(entity == null) throw new NotFoundException("VehicleMaintenance", id);
        
        return _mapper.Map<VehicleMaintenance>(entity);
    }

    /// <summary>
    /// Метод получения всех записей из БД
    /// </summary>
    /// <param name="filter">Филтр параметров</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Все ТО</returns>
    public async Task<IReadOnlyList<VehicleMaintenance>> GetAllByFilterAsync(IFilter? filter = null,CancellationToken cancellationToken = default)
    {
        var entities = await _context.VehicleMaintenances
            .AsNoTracking()
            .Include(v => v.Vehicle)
            .ToListAsync(cancellationToken);
        
        return _mapper.Map<IReadOnlyList<VehicleMaintenance>>(entities);
    }

    /// <summary>
    /// Метод добавления или обновления записи в БД
    /// </summary>
    /// <param name="entity">ТО</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Обновленная или добавленная запись</returns>
    public async Task<VehicleMaintenance> AddOrUpdateAsync(VehicleMaintenance entity, CancellationToken cancellationToken = default)
    {
        EntityEntry<VehicleMaintenanceEntity> result;
        var vehicleMaintenance = _mapper.Map<VehicleMaintenanceEntity>(entity);

        if (vehicleMaintenance.Id == 0)
        {
            result = await _context.VehicleMaintenances.AddAsync(vehicleMaintenance, cancellationToken);
        }
        else
        {
            result = _context.VehicleMaintenances.Update(vehicleMaintenance);
        }
        
        return _mapper.Map<VehicleMaintenance>(result.Entity);
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
        var entity = await _context.VehicleMaintenances
            .FirstOrDefaultAsync(v => v.Id == id, cancellationToken);
        if(entity == null) throw new NotFoundException("VehicleMaintenance", id);
        _context.VehicleMaintenances.Remove(entity);
        
        return entity.Id;
    }
}