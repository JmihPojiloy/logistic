using Logistics.Domain.Enums;

namespace Logistics.Domain.Entities.Users;

/// <summary>
/// Класс учетных данных пользователя в системе
/// </summary>
public class UserCredential : BaseEntity
{
    /// <summary>
    /// Логин
    /// </summary>
    public int Phone { get; set; }
    
    /// <summary>
    /// Пароль
    /// </summary>
    public string PasswordHash { get; set; }
    
    /// <summary>
    /// Роль пользователя в системе
    /// </summary>
    public UserRole Role { get; set; }
    
    /// <summary>
    /// Id связанного пользователя
    /// </summary>
    public int UserId { get; set; }
    
    /// <summary>
    /// Навигационное свойство пользователя
    /// </summary>
    public User User { get; set; }
}