using Logistics.Domain.ValueObjects;
using Logistics.Web.Dtos.Warehouses;

namespace Logistics.Web.Dtos.Products;

/// <summary>
/// Транспортный класс для обработки сущности товара 
/// </summary>
public class ProductDto: BaseDto
{
    /// <summary>
    /// Название товара 
    /// </summary>
    public required string Name { get; set; }
    
    /// <summary>
    /// Описание товара
    /// </summary>
    public string? Description { get; set; }
    
    /// <summary>
    /// Цена товара
    /// </summary>
    public Money? Price { get; set; }
    
    /// <summary>
    /// Вес товара без упаковки
    /// </summary>
    public double? Weight { get; set; }
    
    /// <summary>
    /// Вес товара без упаковки
    /// </summary>
    public double? Height { get; set; }
    
    /// <summary>
    /// Ширина товара без упаковки
    /// </summary>
    public double? Width { get; set; }
    
    /// <summary>
    /// Артикул товара
    /// </summary>
    public int Code {get; set;}
    
    /// <summary>
    /// Остатки на складе
    /// </summary>
    public ICollection<InventoryDto>? Inventories { get; set; }
}