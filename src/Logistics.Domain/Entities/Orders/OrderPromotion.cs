using Logistics.Domain.Entities.Promotions;

namespace Logistics.Domain.Entities.Orders;

/// <summary>
/// Класс связывающий заказ и промоакцию
/// </summary>
public class OrderPromotion : BaseEntity
{
    /// <summary>
    /// Id заказа
    /// </summary>
    public int OrderId { get; set; }
    
    /// <summary>
    /// Навигационное свойство заказа
    /// </summary>
    public Order? Order { get; set; }
    
    /// <summary>
    /// Id промоакции
    /// </summary>
    public int PromotionId { get; set; }
    
    /// <summary>
    /// Навигационное свойство промоакции
    /// </summary>
    public Promotion? Promotion { get; set; }
}