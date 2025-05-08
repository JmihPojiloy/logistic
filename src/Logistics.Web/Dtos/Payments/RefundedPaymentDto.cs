
using Logistics.Domain.ValueObjects;

namespace Logistics.Web.Dtos.Payments;

/// <summary>
/// Транспортный класс для сущности возврат платежа
/// </summary>
public class RefundedPaymentDto : BaseDto
{
    /// <summary>
    /// Id платежа
    /// </summary>
    public int PaymentId { get; set; }
    
    /// <summary>
    /// Навигационное свойство платежа
    /// </summary>
    public required PaymentDto Payment { get; set; }
    
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