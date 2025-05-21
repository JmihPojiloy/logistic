using Logistics.Web.Dtos.Orders;

namespace Logistics.Web.Dtos.Delivery;

/// <summary>
/// Транспортный класс для обработки сущности расписания доставки
/// </summary>
public class DeliveryScheduleDto : BaseDto
{
    /// <summary>
    /// Id заказа
    /// </summary>
    public int OrderId { get; set; }
    
    /// <summary>
    /// Навигационное свойство заказа
    /// </summary>
    public OrderDto? Order { get; set; }
    
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