namespace Logistics.Web.Dtos;

/// <summary>
/// Базовый транспортный класс
/// </summary>
public abstract class BaseDto
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