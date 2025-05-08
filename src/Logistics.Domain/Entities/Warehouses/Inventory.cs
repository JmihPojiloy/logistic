using Logistics.Domain.Entities.Products;

namespace Logistics.Domain.Entities.Warehouses;

/// <summary>
/// Класс остатков на складе
/// </summary>
public class Inventory : BaseEntity
{
    /// <summary>
    /// Id товара
    /// </summary>
    public int ProductId { get; set; }
    
    /// <summary>
    /// Навигационное свойство товара
    /// </summary>
    public required Product Product { get; set; }
    
    /// <summary>
    /// Id склада
    /// </summary>
    public int WarehouseId { get; set; }
    
    /// <summary>
    /// Навигационное свойство склада
    /// </summary>
    public required Warehouse Warehouse { get; set; }
    
    /// <summary>
    /// Количество товара на складе
    /// </summary>
    public int Quantity { get; set; }
}