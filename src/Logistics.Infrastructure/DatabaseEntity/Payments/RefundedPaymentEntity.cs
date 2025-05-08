using Logistics.Domain.ValueObjects;

namespace Logistics.Infrastructure.DatabaseEntity.Payments;

/// <summary>
/// Класс для хранения сущности отмененный платеж в БД
/// </summary>
public class RefundedPaymentEntity : BaseDatabaseEntity
{
    /// <summary>
    /// Id платежа
    /// </summary>
    public int PaymentId { get; set; }
    
    /// <summary>
    /// Навигационное свойство платежа
    /// </summary>
    public required PaymentEntity Payment { get; set; }
    
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