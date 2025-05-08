using Logistics.Infrastructure.DatabaseEntity.Products;

namespace Logistics.Infrastructure.DatabaseEntity.Orders;

/// <summary>
/// Класс для хранения сущности ЗаказТовар в БД
/// </summary>
public class OrderProductEntity : BaseDatabaseEntity
{
    /// <summary>
    /// Id заказа
    /// </summary>
    public int OrderId { get; set; }
    
    /// <summary>
    /// Навигационное свойство заказа
    /// </summary>
    public OrderEntity? Order { get; set; }
    
    /// <summary>
    /// Id товара
    /// </summary>
    public int ProductId { get; set; }
    
    /// <summary>
    /// Навигационное свойство товара
    /// </summary>
    public ProductEntity? Product { get; set; }
    
    /// <summary>
    /// Количество товара в заказе
    /// </summary>
    public int Quantity { get; set; }
}