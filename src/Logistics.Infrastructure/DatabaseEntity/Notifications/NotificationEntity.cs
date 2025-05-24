using Logistics.Domain.Enums;
using Logistics.Infrastructure.DatabaseEntity.Users;

namespace Logistics.Infrastructure.DatabaseEntity.Notifications;

/// <summary>
/// Класс для хранения сущности уведомления в БД
/// </summary>
public class NotificationEntity : BaseDatabaseEntity
{
    /// <summary>
    /// Тип уведомления
    /// </summary>
    public NotificationType Type { get; set; }
    
    /// <summary>
    /// Id получателя
    /// </summary>
    public int? RecipientId { get; set; }
    
    /// <summary>
    /// Навигационное свойство получателя
    /// </summary>
    public UserEntity? Recipient { get; set; }
    
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
    public LetterEntity? Letter { get; set; }
    
    /// <summary>
    /// Хэш код уведомления для предотвращения дубликатов
    /// </summary>
    public int? HashCode { get; set; }
}