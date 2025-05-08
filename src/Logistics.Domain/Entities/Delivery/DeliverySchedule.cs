using Logistics.Domain.Entities.Orders;

namespace Logistics.Domain.Entities.Delivery;

/// <summary>
/// Класс графика доставки заказа
/// </summary>
public class DeliverySchedule : BaseEntity
{
    /// <summary>
    /// Id заказа
    /// </summary>
    public int OrderId { get; set; }
    
    /// <summary>
    /// Навигационное свойство заказа
    /// </summary>
    public required Order Order { get; set; }
    
    /// <summary>
    /// Планируемая дата загрузки
    /// </summary>
    public DateTime? EstimatedLoadingDate { get; set; }
    
    /// <summary>
    /// Планируемая дата отправки
    /// </summary>
    public DateTime? EstimatedDeliveryDate { get; set; }
    
    /// <summary>
    /// Реальная дата загрузки
    /// </summary>
    public DateTime? ActualLoadingDate { get; set; }
    
    /// <summary>
    /// Реальная дата отправки
    /// </summary>
    public DateTime? ActualDeliveryDate { get; set; }
}