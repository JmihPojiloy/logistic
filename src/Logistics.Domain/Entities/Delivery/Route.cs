using Logistics.Domain.Entities.Addresses;
using Logistics.Domain.Entities.Orders;
using Logistics.Domain.Entities.Vehicles;

namespace Logistics.Domain.Entities.Delivery;

/// <summary>
/// Класс маршрута
/// </summary>
public class Route : BaseEntity
{
    /// <summary>
    /// Id транспорта
    /// </summary>
    public int VehicleId { get; set; }
    
    /// <summary>
    /// Навигационное свойство транспорта
    /// </summary>
    public Vehicle? Vehicle { get; set; }
    
    /// <summary>
    /// Id адреса
    /// </summary>
    public int AddressId { get; set; }
    
    /// <summary>
    /// Навигационное свойство адреса
    /// </summary>
    public Address? Address { get; set; }
    
    /// <summary>
    /// Расстояние
    /// </summary>
    public int? Distance { get; set; }
    
    /// <summary>
    /// Время выполнения
    /// </summary>
    public TimeSpan? LeadTime { get; set; }
}