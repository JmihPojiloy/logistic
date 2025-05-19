using Logistics.Domain.Enums;

namespace Logistics.Infrastructure.DatabaseEntity.Users;

public class UserCredentialEntity : BaseDatabaseEntity
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
    public UserEntity User { get; set; }
}