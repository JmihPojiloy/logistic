using Logistics.Domain.ValueObjects;

namespace Logistics.Infrastructure.DatabaseEntity.Vehicles;

/// <summary>
/// Класс сущности техническое обслуживание в БД
/// </summary>
public class VehicleMaintenanceEntity : BaseDatabaseEntity
{
    /// <summary>
    /// Id автомобиля
    /// </summary>
    public int VehicleId { get; set; }
    
    /// <summary>
    /// Навигационное свойство автомобиля
    /// </summary>
    public required VehicleEntity Vehicle { get; set; }
    
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