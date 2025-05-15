using AutoMapper;
using Logistics.Application.Exceptions;
using Logistics.Application.Filters;
using Logistics.Application.Interfaces.Filters;
using Logistics.Application.Interfaces.Repositories;
using Logistics.Domain.Entities.Delivery;
using Logistics.Infrastructure.Database;
using Logistics.Infrastructure.DatabaseEntity.Delivery;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Logistics.Infrastructure.Repositories.Delivery;

/// <summary>
/// Класс репозитория доступа к БД для маршрутов 
/// </summary>
public class RouteRepository : IRouteRepository
{
    private readonly LogisticDbContext _context;
    private readonly IMapper _mapper;

    public RouteRepository(LogisticDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    /// <summary>
    /// Метод получения записи по ID
    /// </summary>
    /// <param name="id">ID записи</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Маршрут</returns>
    /// <exception cref="NotFoundException">Ошибка не найденной записи</exception>
    public async Task<Route> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var entity = await _context.Routes.
                AsNoTracking()
                .Include(r => r.Vehicle)
                .Include(r => r.Address)
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (entity == null) throw new NotFoundException("Route", id);
        
        return _mapper.Map<Route>(entity);
    }

    /// <summary>
    /// Получить все записи из БД
    /// </summary>
    /// <param name="filter">Фильтр параметров</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Коллекция маршрутов</returns>
    public async Task<IReadOnlyList<Route>> GetAllByFilterAsync(IFilter? filter = null,CancellationToken cancellationToken = default)
    {
        var query = _context.Routes.AsQueryable();

        if (filter is RouteFilter routeFilter)
        {
            if (routeFilter.AddressId.HasValue)
            {
                query = query.Where(r => r.AddressId == routeFilter.AddressId);
            }

            if (routeFilter.VehicleId.HasValue)
            {
                query = query.Where(r => r.VehicleId == routeFilter.VehicleId);
            }
        }
        
        var entities = await query
            .AsNoTracking()
            .Include(r => r.Vehicle)
            .Include(r => r.Address)
            .ToListAsync(cancellationToken);
        
        return _mapper.Map<IReadOnlyList<Route>>(entities);
    }

    /// <summary>
    /// Метод обновления или добавления записи в БД
    /// </summary>
    /// <param name="entity">Маршрут</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Обновленная или добавленная запись</returns>
    public async Task<Route> AddOrUpdateAsync(Route entity, CancellationToken cancellationToken)
    {
        EntityEntry<RouteEntity> result;
        var route = _mapper.Map<RouteEntity>(entity);

        if (route.Id == 0)
        {
            result = await _context.Routes.AddAsync(route, cancellationToken);
        }
        else
        {
            result = _context.Routes.Update(route);
        }
        
        return _mapper.Map<Route>(result.Entity);
    }

    /// <summary>
    /// Метод удаления записи в бд
    /// </summary>
    /// <param name="id">ID записи</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>ID удаленной записи</returns>
    /// <exception cref="NotFoundException">Ошибка не найденной записи</exception>
    public async Task<int> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var entity = await _context.Routes
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (entity == null) throw new NotFoundException("Route", id);
        
        _context.Routes.Remove(entity);

        return entity.Id;
    }

    /// <summary>
    /// Метод получения маршрута по адресу
    /// </summary>
    /// <param name="id">ID адреса</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Маршрут</returns>
    /// <exception cref="NotFoundException">Ошибка не найденной записи</exception>
    public async Task<Route> GetRouteByAddressId(int id, CancellationToken cancellationToken = default)
    {
        var entity = await _context.Routes
            .AsNoTracking()
            .Include(r => r.Vehicle)
            .FirstOrDefaultAsync(x => x.AddressId == id, cancellationToken);
        if (entity == null) throw new NotFoundException("Route", id);
        
        return _mapper.Map<Route>(entity);
    }
}