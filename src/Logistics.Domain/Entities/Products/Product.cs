using Logistics.Domain.Entities.Warehouses;
using Logistics.Domain.ValueObjects;

namespace Logistics.Domain.Entities.Products;

/// <summary>
/// Класс заказываемого товара
/// </summary>
public class Product : BaseEntity
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
    /// Стоимость товара
    /// </summary>
    public Money? Price { get; set; }
    
    /// <summary>
    /// Вес товара
    /// </summary>
    public double? Weight { get; set; }
    
    /// <summary>
    /// Высота товара без упаковки
    /// </summary>
    public double? Height { get; set; }
    
    /// <summary>
    /// Ширина товара без упаковки
    /// </summary>
    public double? Width { get; set; }
    
    /// <summary>
    /// Артикул
    /// </summary>
    public int Code {get; set;}
    
    /// <summary>
    /// Остатки на складе
    /// </summary>
    public ICollection<Inventory> Inventories { get; set; } = new List<Inventory>();
}