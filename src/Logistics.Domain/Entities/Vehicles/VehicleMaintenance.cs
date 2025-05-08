using Logistics.Domain.ValueObjects;

namespace Logistics.Domain.Entities.Vehicles;

/// <summary>
/// Класс технического обслуживания транспорта
/// </summary>
public class VehicleMaintenance : BaseEntity
{
    /// <summary>
    /// Id автомобиля
    /// </summary>
    public int VehicleId { get; set; }
    
    /// <summary>
    /// Навигационное свойство автомобиля
    /// </summary>
    public required Vehicle Vehicle { get; set; }
    
    /// <summary>
    /// Дата прохождения технического обслуживания
    /// </summary>
    public DateTime MaintenanceDate { get; set; }
    
    /// <summary>
    /// Описание проводимых работ
    /// </summary>
    public required string Description { get; set; }
    
    /// <summary>
    /// Стоимость ТО 
    /// </summary>
    public Money? MaintenancePrice { get; set; }
}