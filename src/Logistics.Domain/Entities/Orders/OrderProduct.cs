using Logistics.Domain.Entities.Products;

namespace Logistics.Domain.Entities.Orders;

/// <summary>
/// Класс связывающий заказ и товар
/// </summary>
public class OrderProduct : BaseEntity
{
    /// <summary>
    /// Id заказа
    /// </summary>
    public int OrderId { get; set; }
    
    /// <summary>
    /// Навигационное свойство заказа
    /// </summary>
    public Order? Order { get; set; }
    
    /// <summary>
    /// Id товара
    /// </summary>
    public int ProductId { get; set; }
    
    /// <summary>
    /// Навигационное свойство товара
    /// </summary>
    public Product? Product { get; set; }
    
    /// <summary>
    /// Количество товара в заказе
    /// </summary>
    public int Quantity { get; set; }
}