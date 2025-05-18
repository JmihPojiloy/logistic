using Logistics.Web.Dtos.Products;

namespace Logistics.Web.Dtos.Orders;

/// <summary>
/// Транспортный класс сущности заказ
/// </summary>
public class OrderProductDto : BaseDto
{
    /// <summary>
    /// Id заказа
    /// </summary>
    public int OrderId { get; set; }
    
    /// <summary>
    /// Id товара
    /// </summary>
    public int ProductId { get; set; }
    
    /// <summary>
    /// Количество товара в заказе
    /// </summary>
    public int Quantity { get; set; }
}