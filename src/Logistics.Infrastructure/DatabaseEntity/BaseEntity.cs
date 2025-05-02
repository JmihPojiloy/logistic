namespace Logistics.Infrastructure.DatabaseEntity;

/// <summary>
/// Базовый класс
/// </summary>
public class BaseEntity
{
    /// <summary>
    /// Id класса в БД
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// Дата создания
    /// </summary>
    public DateTime CreatedOn { get; set; }
}