using Logistics.Domain.Enums;
using Logistics.Infrastructure.DatabaseEntity.Delivery;
using Logistics.Infrastructure.DatabaseEntity.Orders;

namespace Logistics.Infrastructure.DatabaseEntity.Vehicles;

/// <summary>
/// Класс для хранения сущности транспорт
/// </summary>
public class VehicleEntity : BaseDatabaseEntity
{
    /// <summary>
    /// Навигационное свойство водителя
    /// </summary>
    public DriverEntity? Driver { get; set; }
    
    /// <summary>
    /// Навигационное свойство маршрута
    /// </summary>
    public RouteEntity? Route { get; set; }
    
    /// <summary>
    /// Коллекция заказов
    /// </summary>
    public ICollection<OrderEntity>? Orders { get; set; }
    
    /// <summary>
    /// Сведения о ТО транспорта
    /// </summary>
    public ICollection<VehicleMaintenanceEntity>? VehicleMaintenance { get; set; }
    
    /// <summary>
    /// Навигационное свойство отслеживания местоположения транспорта
    /// </summary>
    public DeliveryTrackingEntity? DeliveryTracking { get; set; }
    
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