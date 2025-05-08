using Logistics.Web.Dtos.Orders;

namespace Logistics.Web.Dtos.Promotions;

/// <summary>
/// Транспортный класс для сущности промоакция
/// </summary>
public class PromotionDto : BaseDto
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
    public ICollection<OrderPromotionDto>? OrderPromotions { get; set; }
}