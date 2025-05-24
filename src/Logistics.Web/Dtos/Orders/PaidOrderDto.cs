using Logistics.Domain.Enums;
using Logistics.Domain.ValueObjects;

namespace Logistics.Web.Dtos.Orders;

/// <summary>
/// Транспортный класс для оплаченного заказа
/// </summary>
public class PaidOrderDto
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