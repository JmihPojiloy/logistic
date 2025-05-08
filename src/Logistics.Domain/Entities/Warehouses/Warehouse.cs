using Logistics.Domain.Entities.Addresses;
using Logistics.Domain.Enums;

namespace Logistics.Domain.Entities.Warehouses;

/// <summary>
/// Класс склада
/// </summary>
public class Warehouse : BaseEntity
{
    /// <summary>
    /// Id адреса склада
    /// </summary>
    public int AddressId { get; set; }
    
    /// <summary>
    /// Адрес склада
    /// </summary>
    public required Address Address { get; set; }
    
    /// <summary>
    /// Остатки на складе
    /// </summary>
    public ICollection<Inventory>? Inventories { get; set; }
    
    /// <summary>
    /// Название склада
    /// </summary>
    public required string Name { get; set; }
    
    /// <summary>
    /// Размер (площадь) склада
    /// </summary>
    public int? Square { get; set; }
    
    /// <summary>
    /// Статус склада
    /// </summary>
    public WarehouseStatus Status { get; set; }
}