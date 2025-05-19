using Logistics.Application.Exceptions;
using Logistics.Application.Interfaces.Repositories;
using Logistics.Application.Interfaces.Security;
using Logistics.Application.Interfaces.Services;
using Logistics.Application.Interfaces.UnitOfWork;
using Logistics.Domain.Entities.Users;

namespace Logistics.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUserCredentialRepository _userCredentialRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordService _passwordService;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public AuthService(
        IUserCredentialRepository userCredentialRepository, 
        IUnitOfWork unitOfWork, 
        IPasswordService passwordService,
        IJwtTokenGenerator jwtTokenGenerator)
    {
        _userCredentialRepository = userCredentialRepository;
        _unitOfWork = unitOfWork;
        _passwordService = passwordService;
        _jwtTokenGenerator = jwtTokenGenerator;
    }
    
    /// <summary>
    /// Регистрация нового пользователя
    /// </summary>
    /// <param name="userCredential">Учетные данные пользователя</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Зарегистрированный пользователь</returns>
    public async Task<User> AddNewUserAsync(UserCredential userCredential, CancellationToken cancellationToken)
    {
        var userRepo = _unitOfWork.GetRepository<User>();
        var user = new User
        {
            FirstName = "New User",
            CreatedDate = DateTime.UtcNow,
            PhoneNumber = userCredential.Phone,
        };

        await _unitOfWork.BeginTransactionAsync(cancellationToken);
        
        var processedUser = await userRepo.AddOrUpdateAsync(user, cancellationToken);
        
        userCredential.UserId = processedUser.Id;
        userCredential.User = processedUser;
        userCredential.PasswordHash = _passwordService.HashPassword(userCredential.PasswordHash);
        
        await _userCredentialRepository.AddAsync(userCredential, cancellationToken);
        
        await _unitOfWork.CommitAsync(cancellationToken);
        
        return processedUser;
    }

    /// <summary>
    /// Проверка пароля
    /// </summary>
    /// <param name="phone">Номер телефона (логин)</param>
    /// <param name="password">Пароль для проверки</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>true/false</returns>
    /// <exception cref="NotFoundException">Ошибка не найденного пользователя</exception>
    public async Task<bool> CheckCredentialAsync(int phone, string password, CancellationToken cancellationToken)
    {
        var userCredential = await _userCredentialRepository.GetByPhoneAsync(phone, cancellationToken);
        if(userCredential == null) throw new NotFoundException("UserCredential", phone);
        
        return _passwordService.VerifyPassword(userCredential.PasswordHash, password);
    }

    public async Task<string> LoginAsync(int phone, string password, CancellationToken cancellationToken)
    {
        var credential = await _userCredentialRepository.GetByPhoneAsync(phone, cancellationToken);

        if (credential == null || !_passwordService.VerifyPassword(credential.PasswordHash, password))
            throw new UnauthorizedAccessException("Invalid credentials.");

        return _jwtTokenGenerator.GenerateJwtToken(credential);
    }
}