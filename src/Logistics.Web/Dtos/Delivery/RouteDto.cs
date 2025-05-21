using Logistics.Web.Dtos.Addresses;
using Logistics.Web.Dtos.Vehicles;

namespace Logistics.Web.Dtos.Delivery;

/// <summary>
/// Транспортный класс для сущности маршрута
/// </summary>
public class RouteDto : BaseDto
{
    /// <summary>
    /// Id транспорта
    /// </summary>
    public int? VehicleId { get; set; }
    
    /// <summary>
    /// Навигационное свойство транспорта
    /// </summary>
    public VehicleDto? Vehicle { get; set; }
    
    /// <summary>
    /// Id адреса
    /// </summary>
    public int AddressId { get; set; }
    
    /// <summary>
    /// Навигационное свойство адреса
    /// </summary>
    public AddressDto? Address { get; set; }
    
    /// <summary>
    /// Расстояние
    /// </summary>
    public int? Distance { get; set; }
    
    /// <summary>
    /// Время выполнения
    /// </summary>
    public TimeSpan? LeadTime { get; set; }
}