namespace Logistics.Domain.Entities;

/// <summary>
/// Базовый класс
/// </summary>
public class BaseEntity
{
    /// <summary>
    /// Идентификатор БД
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// Дата создания записи в БД
    /// </summary>
    public DateTime CreatedDate { get; set; }
}