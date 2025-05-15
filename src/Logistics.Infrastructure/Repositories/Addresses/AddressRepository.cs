using AutoMapper;
using Logistics.Application.Exceptions;
using Logistics.Application.Interfaces.Filters;
using Logistics.Application.Interfaces.Repositories;
using Logistics.Domain.Entities.Addresses;
using Logistics.Infrastructure.Database;
using Logistics.Infrastructure.DatabaseEntity.Addresses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Logistics.Infrastructure.Repositories.Addresses;

/// <summary>
/// Класс репозитория доступа к БД для адресов
/// </summary>
public class AddressRepository : IRepository<Address>
{
    private readonly LogisticDbContext _context;
    private readonly IMapper _mapper;

    public AddressRepository(LogisticDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    /// <summary>
    /// Метод получения определенного адреса по ID
    /// </summary>
    /// <param name="id">ID адреса</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Адрес</returns>
    /// <exception cref="NotFoundException">Ошибка не найденной записи</exception>
    public async Task<Address> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var address = await _context.Addresses
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (address == null) throw new NotFoundException("Address", id);
        
        return _mapper.Map<Address>(address);
    }

    /// <summary>
    /// Метод получения всех адресов из БД
    /// </summary>
    /// <param name="filter">Фильтр параметров</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Все записи адресов</returns>
    public async Task<IReadOnlyList<Address>> GetAllByFilterAsync(IFilter? filter = null,CancellationToken cancellationToken = default)
    {
        var addresses = await _context.Addresses
            .AsNoTracking()
            .ToListAsync(cancellationToken);
        
        return _mapper.Map<IReadOnlyList<Address>>(addresses);
    }

    /// <summary>
    /// Метод добавления или обновления адреса из БД
    /// </summary>
    /// <param name="entity">Адрес</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Обновленный или добавленный адрес</returns>
    public async Task<Address> AddOrUpdateAsync(Address entity, CancellationToken cancellationToken)
    {
        EntityEntry<AddressEntity> result;
        var address = _mapper.Map<AddressEntity>(entity);

        if (address.Id == 0)
        {
            result = await _context.Addresses.AddAsync(address, cancellationToken); 
        }
        else
        {
            result = _context.Update(address);
        }
        
        return _mapper.Map<Address>(result.Entity);
    }

    /// <summary>
    /// Метод удаления адреса
    /// </summary>
    /// <param name="id">ID адреса</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>ID удаленной записи</returns>
    /// <exception cref="NotFoundException">Ошибка не найденной записи</exception>
    public async Task<int> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var address = await _context.Addresses
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (address == null) throw new NotFoundException("Address", id);
        
        _context.Addresses.Remove(address);

        return address.Id;
    }
}