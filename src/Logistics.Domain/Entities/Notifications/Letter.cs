using Logistics.Domain.Enums;

namespace Logistics.Domain.Entities.Notifications;

/// <summary>
/// Класс письма
/// </summary>
public class Letter : BaseEntity
{
    /// <summary>
    /// Id уведомления
    /// </summary>
    public int NotificationId { get; set; }
    
    /// <summary>
    /// Навигационное свойство уведомления
    /// </summary>
    public required Notification Notification { get; set; }
    
    /// <summary>
    /// Адрес получателя
    /// </summary>
    public required string RecipientEmail {get; set;}
    
    /// <summary>
    /// Тема письма
    /// </summary>
    public string? Subject {get; set;}
    
    /// <summary>
    /// Заголовок письма
    /// </summary>
    public string? Title {get; set;}
    
    /// <summary>
    /// Текст письма
    /// </summary>
    public string? Text {get; set;}
    
    /// <summary>
    /// Статус отправки письма
    /// </summary>
    public LetterStatus Status {get; set;}
    
    /// <summary>
    /// Url для вставки в письмо
    /// </summary>
    public string? Url {get; set;}
    
    /// <summary>
    /// Дата отправки
    /// </summary>
    public DateTime? SendDate {get; set;}
    
    /// <summary>
    /// Хэш код письма для предотвращения дубликатов
    /// </summary>
    public int? HashCode { get; set; }
}