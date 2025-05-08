using Logistics.Infrastructure.DatabaseEntity.Vehicles;

namespace Logistics.Infrastructure.DatabaseEntity.Delivery;

/// <summary>
/// Класс для хранения сущности отслеживания транспорта
/// </summary>
public class DeliveryTrackingEntity : BaseDatabaseEntity
{
    /// <summary>
    /// Id транспорта
    /// </summary>
    public int VehicleId { get; set; }
    
    /// <summary>
    /// Навигационное свойство транспорта
    /// </summary>
    public required VehicleEntity Vehicle { get; set; }
    
    /// <summary>
    /// Широта
    /// </summary>
    public double? Latitude { get; set; }
    
    /// <summary>
    /// Долгота
    /// </summary>
    public double? Longitude { get; set; }
}