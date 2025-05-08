using Logistics.Domain.Enums;
using Logistics.Infrastructure.DatabaseEntity.Addresses;

namespace Logistics.Infrastructure.DatabaseEntity.Warehouses;

/// <summary>
/// Класс для хранения сущности склада в БД
/// </summary>
public class WarehouseEntity : BaseDatabaseEntity
{
    /// <summary>
    /// Id адреса склада
    /// </summary>
    public int AddressId { get; set; }
    
    /// <summary>
    /// Адрес склада
    /// </summary>
    public required AddressEntity Address { get; set; }
    
    /// <summary>
    /// Остатки на складе
    /// </summary>
    public ICollection<InventoryEntity> Inventories { get; set; } = new List<InventoryEntity>();
    
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