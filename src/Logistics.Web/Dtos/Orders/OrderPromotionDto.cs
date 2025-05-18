using Logistics.Web.Dtos.Promotions;

namespace Logistics.Web.Dtos.Orders;

/// <summary>
/// Транспортный класс для сущности ЗаказПромоакция
/// </summary>
public class OrderPromotionDto : BaseDto
{
    /// <summary>
    /// Id заказа
    /// </summary>
    public int OrderId { get; set; }
    
    /// <summary>
    /// Id промоакции
    /// </summary>
    public int PromotionId { get; set; }
}