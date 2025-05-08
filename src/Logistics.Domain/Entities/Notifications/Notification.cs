using Logistics.Domain.Entities.Users;
using Logistics.Domain.Enums;

namespace Logistics.Domain.Entities.Notifications;

/// <summary>
/// Класс уведомления
/// </summary>
public class Notification : BaseEntity
{
    /// <summary>
    /// Тип уведомления
    /// </summary>
    public NotificationType Type { get; set; }
    
    /// <summary>
    /// Id получателя
    /// </summary>
    public int RecipientId { get; set; }
    
    /// <summary>
    /// Навигационное свойство получателя
    /// </summary>
    public required User Recipient { get; set; }
    
    /// <summary>
    /// Заголовок уведомления
    /// </summary>
    public string? Title { get; set; }
    
    /// <summary>
    /// Текст уведомления
    /// </summary>
    public string? Text { get; set; }
    
    /// <summary>
    /// Статус отправки уведомления
    /// </summary>
    public NotificationStatus Status { get; set; }
    
    /// <summary>
    /// Флаг, определяющий дублирование по Email
    /// </summary>
    public bool IsEmail { get; set; }
    
    /// <summary>
    /// Дата отправки
    /// </summary>
    public DateTime SendDate { get; set; }
    
    /// <summary>
    /// Навигационное свойство письма, отправленного с уведомлением
    /// </summary>
    public Letter? Letter { get; set; }
    
    /// <summary>
    /// Хэш код уведомления для предотвращения дубликатов
    /// </summary>
    public int? HashCode { get; set; }
}