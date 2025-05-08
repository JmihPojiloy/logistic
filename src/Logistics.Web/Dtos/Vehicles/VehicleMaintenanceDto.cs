using Logistics.Domain.ValueObjects;

namespace Logistics.Web.Dtos.Vehicles;

/// <summary>
/// Транспортный класс для сущности техническое обслуживание
/// </summary>
public class VehicleMaintenanceDto : BaseDto
{
    /// <summary>
    /// Id автомобиля
    /// </summary>
    public int VehicleId { get; set; }
    
    /// <summary>
    /// Навигационное свойство автомобиля
    /// </summary>
    public required VehicleDto Vehicle { get; set; }
    
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