using AutoMapper;
using Logistics.Application.Interfaces.Services;
using Logistics.Domain.Entities.Users;
using Logistics.Web.Dtos.Users;
using Microsoft.AspNetCore.Mvc;

namespace Logistics.Web.Controllers;

/// <summary>
/// Контроллер для работы с учетными данными пользователей
/// </summary>
[ApiController]
[Route("account")]
public class AccountController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IMapper _mapper;

    public AccountController(IAuthService authService, IMapper mapper)
    {
        _authService = authService;
        _mapper = mapper;
    }

    /// <summary>
    /// Регистрация нового пользователя
    /// </summary>
    /// <param name="registerDto">Логин, пароль, роль в системе</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Зарегистрированный пользователь</returns>
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto, CancellationToken cancellationToken)
    {
        var userCredential = _mapper.Map<UserCredential>(registerDto);
        
        var result = await _authService.AddNewUserAsync(userCredential, cancellationToken);
        
        return Ok(_mapper.Map<UserDto>(result));
    }
    
    /// <summary>
    /// Вход в систему зарегистрированного пользователя
    /// </summary>
    /// <param name="dto">Логин и пароль</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>ОК c jwt токеном доступа</returns>
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto, CancellationToken cancellationToken)
    {
        var token = await _authService.LoginAsync(dto.Phone, dto.Password, cancellationToken);
        return Ok(new { token });
    }
}