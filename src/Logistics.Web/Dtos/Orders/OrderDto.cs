using Logistics.Domain.Enums;
using Logistics.Domain.ValueObjects;
using Logistics.Web.Dtos.Addresses;
using Logistics.Web.Dtos.Payments;
using Logistics.Web.Dtos.Users;
using Logistics.Web.Dtos.Vehicles;

namespace Logistics.Web.Dtos.Orders;

/// <summary>
/// Транспортный класс для сущности заказ
/// </summary>
public class OrderDto : BaseDto
{
    /// <summary>
    /// Id пользователя создавшего заказ
    /// </summary>
    public int UserId { get; set; }
    
    /// <summary>
    /// Навигационное свойство
    /// </summary>
    public UserDto? User { get; set; }
    
    /// <summary>
    /// Id транспорта, доставляющего заказ
    /// </summary>
    public int? VehicleId { get; set; }
    
    /// <summary>
    /// Навигационное свойство транспорта
    /// </summary>
    public VehicleDto? Vehicle { get; set; }
    
    /// <summary>
    /// Id адреса для доставки заказа
    /// </summary>
    public int AddressId { get; set; }
    
    /// <summary>
    /// Навигационное свойство адреса
    /// </summary>
    public required AddressDto Address { get; set; }
    
    /// <summary>
    /// Навигационное свойство платежа
    /// </summary>
    public PaymentDto? Payment { get; set; }
    
        
    /// <summary>
    /// Стоимость доставки
    /// </summary>
    public Money? DeliveryCost { get; set; }
    
    /// <summary>
    /// Связь с таблицей товаров
    /// </summary>
    public ICollection<OrderProductDto> OrderProducts { get; set; } = new List<OrderProductDto>();
    
    /// <summary>
    /// Связь с таблицей промоакции
    /// </summary>
    public ICollection<OrderPromotionDto> OrderPromotions { get; set; } = new List<OrderPromotionDto>();
    
    /// <summary>
    /// Статус заказа
    /// </summary>
    public OrderStatus Status { get; set; }
}