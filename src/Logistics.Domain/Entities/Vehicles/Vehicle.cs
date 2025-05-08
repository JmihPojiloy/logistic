using Logistics.Domain.Entities.Delivery;
using Logistics.Domain.Entities.Orders;
using Logistics.Domain.Enums;

namespace Logistics.Domain.Entities.Vehicles;

/// <summary>
/// Класс транспорта
/// </summary>
public class Vehicle : BaseEntity
{
    /// <summary>
    /// Навигационное свойство водителя
    /// </summary>
    public Driver? Driver { get; set; }
    
    /// <summary>
    /// Навигационное свойство маршрута
    /// </summary>
    public Route? Route { get; set; }
    
    /// <summary>
    /// Коллекция заказов
    /// </summary>
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    
    /// <summary>
    /// Сведения о ТО транспорта
    /// </summary>
    public ICollection<VehicleMaintenance> VehicleMaintenance { get; set; } = new List<VehicleMaintenance>();
    
    /// <summary>
    /// Навигационное свойство отслеживания местоположения транспорта
    /// </summary>
    public DeliveryTracking? DeliveryTracking { get; set; }
    
    /// <summary>
    /// Название транспорта
    /// </summary>
    public required string Name {get; set;}
    
    /// <summary>
    /// Грузоподъемность
    /// </summary>
    public int? LoadCapacity { get; set; }
    
    /// <summary>
    /// Статус автомобиля
    /// </summary>
    public VehicleStatus Status { get; set; }
    
    /// <summary>
    /// Пробег
    /// </summary>
    public int? MileAge  { get; set; }
}