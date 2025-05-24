using Logistics.Domain.Enums;
using Logistics.Domain.ValueObjects;

namespace Logistics.Domain.Entities.Orders;

/// <summary>
/// Класс оплаченного заказа
/// </summary>
public class PaidOrder : BaseEntity
{
    /// <summary>
    /// Id заказа
    /// </summary>
    public int? OrderId { get; set; }
    
    /// <summary>
    /// Имя водителя
    /// </summary>
    public string? DriverName { get; set; }
    
    /// <summary>
    /// Расстояние в км
    /// </summary>
    public int? Distance { get; set; }
    
    /// <summary>
    /// Стоимость заказа с доставкой
    /// </summary>
    public Money? TotalCost { get; set; }
    
    /// <summary>
    /// Время доставки
    /// </summary>
    public TimeSpan? LeadTime { get; set; }
    
    /// <summary>
    /// Статус заказа
    /// </summary>
    public OrderStatus? Status { get; set; }
}