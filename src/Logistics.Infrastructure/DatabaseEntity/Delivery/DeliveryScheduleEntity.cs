using Logistics.Infrastructure.DatabaseEntity.Orders;

namespace Logistics.Infrastructure.DatabaseEntity.Delivery;

/// <summary>
/// Класс для хранения сущности график доставки в БД
/// </summary>
public class DeliveryScheduleEntity : BaseDatabaseEntity
{
    /// <summary>
    /// Id заказа
    /// </summary>
    public int OrderId { get; set; }
    
    /// <summary>
    /// Навигационное свойство заказа
    /// </summary>
    public required OrderEntity Order { get; set; }
    
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