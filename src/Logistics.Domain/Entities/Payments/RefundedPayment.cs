using Logistics.Domain.ValueObjects;

namespace Logistics.Domain.Entities.Payments;

/// <summary>
/// Класс отмененного платежа
/// </summary>
public class RefundedPayment : BaseEntity
{
    /// <summary>
    /// Id платежа
    /// </summary>
    public int PaymentId { get; set; }
    
    /// <summary>
    /// Навигационное свойство платежа
    /// </summary>
    public required Payment Payment { get; set; }
    
    /// <summary>
    /// Внешний идентификатор чека
    /// </summary>
    public int? ExternalReceiptId { get; set; }
    
    /// <summary>
    /// Сумма возврата
    /// </summary>
    public required Money Amount { get; set; }
    
    /// <summary>
    /// Дата возврата
    /// </summary>
    public DateTime? CancellationDate { get; set; }
}