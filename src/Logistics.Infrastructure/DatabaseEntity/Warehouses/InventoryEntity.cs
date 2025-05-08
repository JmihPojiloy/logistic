using Logistics.Infrastructure.DatabaseEntity.Products;

namespace Logistics.Infrastructure.DatabaseEntity.Warehouses;

/// <summary>
/// Класс для хранения сущности остатков в БД
/// </summary>
public class InventoryEntity : BaseDatabaseEntity
{
    /// <summary>
    /// Id товара
    /// </summary>
    public int ProductId { get; set; }
    
    /// <summary>
    /// Навигационное свойство товара
    /// </summary>
    public required ProductEntity Product { get; set; }
    
    /// <summary>
    /// Id склада
    /// </summary>
    public int WarehouseId { get; set; }
    
    /// <summary>
    /// Навигационное свойство склада
    /// </summary>
    public required WarehouseEntity Warehouse { get; set; }
    
    /// <summary>
    /// Количество товара на складе
    /// </summary>
    public int Quantity { get; set; }
}