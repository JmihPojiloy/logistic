using Logistics.Domain.Enums;
using Logistics.Web.Dtos.Delivery;
using Logistics.Web.Dtos.Orders;

namespace Logistics.Web.Dtos.Vehicles;

/// <summary>
/// Транспортный класс для сущности транспорт
/// </summary>
public class VehicleDto : BaseDto
{
    /// <summary>
    /// Навигационное свойство водителя
    /// </summary>
    public DriverDto? Driver { get; set; }
    
    /// <summary>
    /// Навигационное свойство маршрута
    /// </summary>
    public RouteDto? Route { get; set; }
    
    /// <summary>
    /// Коллекция заказов
    /// </summary>
    public virtual ICollection<OrderDto> Orders { get; set; } = new List<OrderDto>();
    
    /// <summary>
    /// Сведения о ТО транспорта
    /// </summary>
    public ICollection<VehicleMaintenanceDto> VehicleMaintenance { get; set; } = new List<VehicleMaintenanceDto>();
    
    /// <summary>
    /// Навигационное свойство отслеживания местоположения транспорта
    /// </summary>
    public DeliveryTrackingDto? DeliveryTracking { get; set; }
    
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