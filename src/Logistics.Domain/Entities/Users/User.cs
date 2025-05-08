using Logistics.Domain.Entities.Addresses;
using Logistics.Domain.Entities.Notifications;
using Logistics.Domain.Entities.Orders;
using Logistics.Domain.Enums;

namespace Logistics.Domain.Entities.Users;

/// <summary>
/// Класс пользователя
/// </summary>
public class User : BaseEntity
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
    public ICollection<Address> Addresses { get; set; } = new List<Address>();
    
    /// <summary>
    /// Заказы
    /// </summary>
    public ICollection<Order> Orders { get; set; } = new List<Order>();
    
    /// <summary>
    /// Уведомления пользователя
    /// </summary>
    public ICollection<Notification> Notifications { get; set; } = new List<Notification>();
}