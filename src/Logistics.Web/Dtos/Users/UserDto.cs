using Logistics.Domain.Enums;
using Logistics.Web.Dtos.Addresses;
using Logistics.Web.Dtos.Notifications;
using Logistics.Web.Dtos.Orders;

namespace Logistics.Web.Dtos.Users;

/// <summary>
/// Транспортный класс для сущности пользователь
/// </summary>
public class UserDto : BaseDto
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
    public ICollection<AddressDto> Addresses { get; set; } = new List<AddressDto>();
    
    /// <summary>
    /// Заказы
    /// </summary>
    public ICollection<int> OrderIds { get; set; } = new List<int>();
    
    /// <summary>
    /// Уведомления пользователя
    /// </summary>
    public ICollection<NotificationDto> Notifications { get; set; } = new List<NotificationDto>();
}