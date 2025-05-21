using Logistics.Domain.Enums;
using Logistics.Domain.ValueObjects;
using Logistics.Infrastructure.DatabaseEntity.Addresses;
using Logistics.Infrastructure.DatabaseEntity.Payments;
using Logistics.Infrastructure.DatabaseEntity.Users;
using Logistics.Infrastructure.DatabaseEntity.Vehicles;

namespace Logistics.Infrastructure.DatabaseEntity.Orders;

/// <summary>
/// Класс для хранения сущности заказ в БД
/// </summary>
public class OrderEntity : BaseDatabaseEntity
{
    /// <summary>
    /// Id пользователя создавшего заказ
    /// </summary>
    public int UserId { get; set; }
    
    /// <summary>
    /// Навигационное свойство
    /// </summary>
    public required UserEntity User { get; set; }
    
    /// <summary>
    /// Id транспорта, доставляющего заказ
    /// </summary>
    public int? VehicleId { get; set; }
    
    /// <summary>
    /// Навигационное свойство транспорта
    /// </summary>
    public VehicleEntity? Vehicle { get; set; }
    
    /// <summary>
    /// Id адреса для доставки заказа
    /// </summary>
    public int AddressId { get; set; }
    
    /// <summary>
    /// Навигационное свойство адреса
    /// </summary>
    public required AddressEntity Address { get; set; }
    
    /// <summary>
    /// Стоимость доставки
    /// </summary>
    public Money? DeliveryCost { get; set; }
    
    /// <summary>
    /// Навигационное свойство платежа
    /// </summary>
    public PaymentEntity? Payment { get; set; }
    
    /// <summary>
    /// Связь с таблицей товаров
    /// </summary>
    public ICollection<OrderProductEntity> OrderProducts { get; set; } = new List<OrderProductEntity>();
    
    /// <summary>
    /// Связь с таблицей промоакции
    /// </summary>
    public ICollection<OrderPromotionEntity> OrderPromotions { get; set; } = new List<OrderPromotionEntity>();
    
    /// <summary>
    /// Статус заказа
    /// </summary>
    public OrderStatus Status { get; set; }
}