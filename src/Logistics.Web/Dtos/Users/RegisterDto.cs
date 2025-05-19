using Logistics.Domain.Enums;

namespace Logistics.Web.Dtos.Users;

/// <summary>
/// Транспортный класс для регистрации пользователя 
/// </summary>
public class RegisterDto : BaseDto
{
    /// <summary>
    /// Телефон - логин
    /// </summary>
    public int Phone {get; set;}
    
    /// <summary>
    /// Пароль
    /// </summary>
    public string Password {get; set;}
    
    /// <summary>
    /// Роль пользователя в системе
    /// </summary>
    public UserRole Role {get; set;}
}