using Logistics.Domain.ValueObjects;

namespace Logistics.Infrastructure.DatabaseEntity.Products;

/// <summary>
/// Класс для хранения сущности товара в БД
/// </summary>
public class ProductEntity : BaseEntity
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
    /// Вес товара без упаковки
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
    /// Артикул товара
    /// </summary>
    public int Code {get; set;}
}