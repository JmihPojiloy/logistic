namespace Logistics.Web.Dtos;

/// <summary>
/// Базовый класс
/// </summary>
public class BaseDto
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