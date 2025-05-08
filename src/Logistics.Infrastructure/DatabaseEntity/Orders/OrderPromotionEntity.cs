using Logistics.Infrastructure.DatabaseEntity.Promotions;

namespace Logistics.Infrastructure.DatabaseEntity.Orders;

/// <summary>
/// Класс для хранения сущности ЗаказПромоакция в БД
/// </summary>
public class OrderPromotionEntity : BaseDatabaseEntity
{
    /// <summary>
    /// Id заказа
    /// </summary>
    public int OrderId { get; set; }
    
    /// <summary>
    /// Навигационное свойство заказа
    /// </summary>
    public OrderEntity? Order { get; set; }
    
    /// <summary>
    /// Id промоакции
    /// </summary>
    public int PromotionId { get; set; }
    
    /// <summary>
    /// Навигационное свойство промоакции
    /// </summary>
    public PromotionEntity? Promotion { get; set; }    
}