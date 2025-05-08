using Logistics.Web.Dtos.Products;

namespace Logistics.Web.Dtos.Warehouses;

/// <summary>
/// Транспортный класс для сущности остатки
/// </summary>
public class InventoryDto : BaseDto
{
    /// <summary>
    /// Id товара
    /// </summary>
    public int ProductId { get; set; }
    
    /// <summary>
    /// Навигационное свойство товара
    /// </summary>
    public required ProductDto Product { get; set; }
    
    /// <summary>
    /// Id склада
    /// </summary>
    public int WarehouseId { get; set; }
    
    /// <summary>
    /// Навигационное свойство склада
    /// </summary>
    public required WarehouseDto Warehouse { get; set; }
    
    /// <summary>
    /// Количество товара на складе
    /// </summary>
    public int Quantity { get; set; }
}