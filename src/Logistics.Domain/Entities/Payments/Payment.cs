using Logistics.Domain.Entities.Orders;
using Logistics.Domain.ValueObjects;

namespace Logistics.Domain.Entities.Payments;

/// <summary>
/// Класс платежа
/// </summary>
public class Payment : BaseEntity
{
    /// <summary>
    /// Id заказа
    /// </summary>
    public int OrderId { get; set; }
    
    /// <summary>
    /// Навигационное свойство заказа
    /// </summary>
    public required Order Order { get; set; }
    
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
    public ICollection<RefundedPayment> CancelledPayments { get; set; } = new List<RefundedPayment>();
}