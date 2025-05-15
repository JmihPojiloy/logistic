using AutoMapper;
using Logistics.Application.Interfaces.Services;
using Logistics.Domain.Entities.Users;
using Logistics.Web.Dtos.Users;
using Microsoft.AspNetCore.Mvc;

namespace Logistics.Web.Controllers;

/// <summary>
/// Контроллер для обработки запросов работы с пользователем
/// </summary>
[Route("users")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public UserController(IUserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    /// <summary>
    /// Получить всех пользователей
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns>ОК со списком пользователей</returns>
    [HttpGet("getall")]
    public async Task<IActionResult> GetAllUsersAsync(CancellationToken cancellationToken)
    {
        var users = await _userService.GetAllAsync(cancellationToken);
        return Ok(_mapper.Map<IEnumerable<UserDto>>(users));
    }

    /// <summary>
    /// Получить пользователя по Id
    /// </summary>
    /// <param name="id">Id пользователя</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>ОК с пользователем</returns>
    [HttpGet("getbyid/{id:int}")]
    public async Task<IActionResult> GetUserByIdAsync(int id, CancellationToken cancellationToken)
    {
        var user = await _userService.GetByIdAsync(id, cancellationToken);
        return Ok(_mapper.Map<UserDto>(user));
    }

    /// <summary>
    /// Добавить нового пользователя
    /// </summary>
    /// <param name="user">Пользователь</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>ОК с добавленным пользователем</returns>
    [HttpPost("add")]
    public async Task<IActionResult> AddUserAsync(UserDto user, CancellationToken cancellationToken)
    {
        var userEntity = _mapper.Map<User>(user);
        var result = await _userService.AddOrUpdateAsync(userEntity, cancellationToken);
        
        return Ok(_mapper.Map<UserDto>(result));
    }

    /// <summary>
    /// Обновить данные о пользователе
    /// </summary>
    /// <param name="user">Пользователь</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>ОК с обновленным пользователем</returns>
    [HttpPut("update")]
    public async Task<IActionResult> UpdateUserAsync(UserDto user, CancellationToken cancellationToken)
    {
        var userEntity = _mapper.Map<User>(user);
        var result = await _userService.AddOrUpdateAsync(userEntity, cancellationToken);
        
        return Ok(_mapper.Map<UserDto>(result));
    }

    /// <summary>
    /// Удалить пользователя по id
    /// </summary>
    /// <param name="id">Id пользователя</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Id удаленного пользователя</returns>
    [HttpDelete("delete/{id:int}")]
    public async Task<IActionResult> DeleteUserAsync(int id, CancellationToken cancellationToken)
    {
        var result = await _userService.DeleteAsync(id, cancellationToken);
        
        return Ok(_mapper.Map<UserDto>(result));
    }
 }