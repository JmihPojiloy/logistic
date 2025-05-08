using Logistics.Domain.Entities.Orders;

namespace Logistics.Domain.Entities.Promotions;

/// <summary>
/// Класс промоакции
/// </summary>
public class Promotion : BaseEntity
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
    public ICollection<OrderPromotion>? OrderPromotions { get; set; }
}