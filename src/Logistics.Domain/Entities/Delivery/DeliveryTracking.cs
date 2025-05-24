using Logistics.Domain.Entities.Vehicles;

namespace Logistics.Domain.Entities.Delivery;

/// <summary>
/// Класс отслеживания транспорта
/// </summary>
public class DeliveryTracking : BaseEntity
{
    /// <summary>
    /// Id транспорта
    /// </summary>
    public int? VehicleId { get; set; }
    
    /// <summary>
    /// Навигационное свойство транспорта
    /// </summary>
    public Vehicle? Vehicle { get; set; }
    
    /// <summary>
    /// Широта
    /// </summary>
    public double? Latitude { get; set; }
    
    /// <summary>
    /// Долгота
    /// </summary>
    public double? Longitude { get; set; }
}