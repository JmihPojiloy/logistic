using Logistics.Web.Dtos.Vehicles;

namespace Logistics.Web.Dtos.Delivery;

/// <summary>
/// Транспортный класс для сущности отслеживания транспорта
/// </summary>
public class DeliveryTrackingDto : BaseDto
{
    /// <summary>
    /// Id транспорта
    /// </summary>
    public int VehicleId { get; set; }
    
    /// <summary>
    /// Навигационное свойство транспорта
    /// </summary>
    public required VehicleDto Vehicle { get; set; }
    
    /// <summary>
    /// Широта
    /// </summary>
    public double? Latitude { get; set; }
    
    /// <summary>
    /// Долгота
    /// </summary>
    public double? Longitude { get; set; }
}