using Logistics.Domain.Entities;
using Logistics.Domain.Entities.Addresses;
using Logistics.Domain.Entities.Orders;
using Logistics.Domain.Enums;
using Logistics.Infrastructure.DatabaseEntity.Addresses;
using Logistics.Infrastructure.DatabaseEntity.Notifications;
using Logistics.Infrastructure.DatabaseEntity.Orders;

namespace Logistics.Infrastructure.DatabaseEntity.Users;

/// <summary>
/// Класс для хранения сущности пользователя в БД
/// </summary>
public class UserEntity : BaseDatabaseEntity
{
    /// <summary>
    /// Фамилия
    /// </summary>
    public string? LastName { get; set; }
    
    /// <summary>
    /// Имя
    /// </summary>
    public required string FirstName { get; set; }
    
    /// <summary>
    /// Отчество
    /// </summary>
    public string? MiddleName { get; set; }
    
    /// <summary>
    /// Электронная почта
    /// </summary>
    public string? Email { get; set; }
    
    /// <summary>
    /// Номер телефона
    /// </summary>
    public int PhoneNumber { get; set; }
    
    /// <summary>
    /// Пол
    /// </summary>
    public Gender? Gender { get; set; }
    
    /// <summary>
    /// Адреса пользователя
    /// </summary>
    public ICollection<AddressEntity> Addresses { get; set; } = new List<AddressEntity>();
    
    /// <summary>
    /// Заказы
    /// </summary>
    public ICollection<OrderEntity> Orders { get; set; } = new List<OrderEntity>();
    
    /// <summary>
    /// Уведомления пользователю
    /// </summary>
    public ICollection<NotificationEntity> Notifications { get; set; } = new List<NotificationEntity>();
}