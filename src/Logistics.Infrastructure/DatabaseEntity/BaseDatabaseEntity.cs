namespace Logistics.Infrastructure.DatabaseEntity;

/// <summary>
/// Базовый класс
/// </summary>
public abstract class BaseDatabaseEntity
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