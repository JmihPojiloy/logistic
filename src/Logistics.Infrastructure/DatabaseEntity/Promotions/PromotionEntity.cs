using Logistics.Infrastructure.DatabaseEntity.Orders;

namespace Logistics.Infrastructure.DatabaseEntity.Promotions;

/// <summary>
/// Класс для хранения сущности промоакция в БД
/// </summary>
public class PromotionEntity : BaseDatabaseEntity
{
    /// <summary>
    /// Код акции
    /// </summary>
    public int Code {get; set;}
    
    /// <summary>
    /// Описание акции
    /// </summary>
    public required string Description {get; set;}
    
    /// <summary>
    /// Процент скидки
    /// </summary>
    public int? Discount {get; set;}
    
    /// <summary>
    /// Начало акции
    /// </summary>
    public DateTime? StartDate {get; set;}
    
    /// <summary>
    /// Конец акции
    /// </summary>
    public DateTime? EndDate {get; set;}
    
    /// <summary>
    /// Коллекция заказов которые участвуют в акции
    /// </summary>
    public ICollection<OrderPromotionEntity> OrderPromotions { get; set; } = new List<OrderPromotionEntity>();
}