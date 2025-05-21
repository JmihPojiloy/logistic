using Logistics.Domain.ValueObjects;
using Logistics.Infrastructure.DatabaseEntity.Addresses;
using Logistics.Infrastructure.DatabaseEntity.Vehicles;

namespace Logistics.Infrastructure.DatabaseEntity.Delivery;

/// <summary>
/// Класс для хранения сущности маршрут в БД
/// </summary>
public class RouteEntity : BaseDatabaseEntity
{
    /// <summary>
    /// Id транспорта
    /// </summary>
    public int? VehicleId { get; set; }
    
    /// <summary>
    /// Навигационное свойство транспорта
    /// </summary>
    public VehicleEntity? Vehicle { get; set; }
    
    /// <summary>
    /// Id адреса
    /// </summary>
    public int AddressId { get; set; }
    
    /// <summary>
    /// Навигационное свойство адреса
    /// </summary>
    public AddressEntity? Address { get; set; }
    
    /// <summary>
    /// Расстояние
    /// </summary>
    public int? Distance { get; set; }
    
    /// <summary>
    /// Стоимость за километр
    /// </summary>
    public Money? Cost { get; set; }
    
    /// <summary>
    /// Время выполнения
    /// </summary>
    public TimeSpan? LeadTime { get; set; }
}