using Logistics.Domain.Entities.Addresses;
using Logistics.Domain.Entities.Payments;
using Logistics.Domain.Entities.Users;
using Logistics.Domain.Entities.Vehicles;
using Logistics.Domain.Enums;
using Logistics.Domain.ValueObjects;

namespace Logistics.Domain.Entities.Orders;

/// <summary>
/// Класс заказа
/// </summary>
public class Order : BaseEntity
{
    /// <summary>
    /// Id пользователя создавшего заказ
    /// </summary>
    public int UserId { get; set; }
    
    /// <summary>
    /// Навигационное свойство
    /// </summary>
    public required User User { get; set; }
    
    /// <summary>
    /// Id транспорта, доставляющего заказ
    /// </summary>
    public int VehicleId { get; set; }
    
    /// <summary>
    /// Навигационное свойство транспорта
    /// </summary>
    public Vehicle? Vehicle { get; set; }
    
    /// <summary>
    /// Id адреса для доставки заказа
    /// </summary>
    public int AddressId { get; set; }
    
    /// <summary>
    /// Навигационное свойство адреса
    /// </summary>
    public required Address Address { get; set; }
    
    /// <summary>
    /// Стоимость доставки
    /// </summary>
    public Money? DeliveryCost { get; set; }
    
    /// <summary>
    /// Навигационное свойство платежа
    /// </summary>
    public Payment? Payment { get; set; }
    
    /// <summary>
    /// Связь с таблицей товаров
    /// </summary>
    public ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();
    
    /// <summary>
    /// Связь с таблицей промоакции
    /// </summary>
    public ICollection<OrderPromotion> OrderPromotions { get; set; } = new List<OrderPromotion>();
    
    /// <summary>
    /// Статус заказа
    /// </summary>
    public OrderStatus Status { get; set; }
}