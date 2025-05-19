using AutoMapper;
using Logistics.Application.Exceptions;
using Logistics.Application.Interfaces.Repositories;
using Logistics.Domain.Entities.Users;
using Logistics.Infrastructure.Database;
using Logistics.Infrastructure.DatabaseEntity.Users;
using Microsoft.EntityFrameworkCore;

namespace Logistics.Infrastructure.Repositories.Users;

/// <summary>
/// Класс репозитория для доступа к учетным данным пользователей
/// </summary>
public class UserCredentialRepository : IUserCredentialRepository
{
    private readonly LogisticDbContext _context;
    private readonly IMapper _mapper;

    public UserCredentialRepository(LogisticDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    /// <summary>
    /// Метод нахождения учетных данных по логину
    /// </summary>
    /// <param name="phone">Телефон - логин</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Учетные данные</returns>
    /// <exception cref="NotFoundException">Ошибка не найденной записи</exception>
    public async Task<UserCredential?> GetByPhoneAsync(int phone, CancellationToken cancellationToken)
    {
        var entity = await _context.UserCredentials
            .Include(u => u.User)
            .FirstOrDefaultAsync(u => u.Phone == phone, cancellationToken: cancellationToken);

        if (entity == null) throw new NotFoundException("UserCredential", phone);
        
        return _mapper.Map<UserCredential>(entity);
    }

    /// <summary>
    /// Метод добавления учетных данных
    /// </summary>
    /// <param name="userCredential">Учетные данные</param>
    /// <param name="cancellationToken">Токен отмены</param>
    public async Task AddAsync(UserCredential userCredential, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<UserCredentialEntity>(userCredential);
        await _context.UserCredentials.AddAsync(entity, cancellationToken);
    }
}