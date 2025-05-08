using Logistics.Domain.ValueObjects;
using Logistics.Web.Dtos.Orders;

namespace Logistics.Web.Dtos.Payments;

/// <summary>
/// Транспортный класс для сущности платеж
/// </summary>
public class PaymentDto : BaseDto
{
    /// <summary>
    /// Id заказа
    /// </summary>
    public int OrderId { get; set; }
    
    /// <summary>
    /// Навигационное свойство заказа
    /// </summary>
    public required OrderDto Order { get; set; }
    
    /// <summary>
    /// Внешний идентификатор чека
    /// </summary>
    public int? ExternalReceiptId { get; set; }
    
    /// <summary>
    /// Сумма платежа
    /// </summary>
    public required Money Amount { get; set; }
    
    /// <summary>
    /// Дата платежа
    /// </summary>
    public DateTime? PaymentDate { get; set; }
    
    /// <summary>
    /// Отмены или возвраты по данному платежу
    /// </summary>
    public ICollection<RefundedPaymentDto> CancelledPayments { get; set; } = new List<RefundedPaymentDto>();
}