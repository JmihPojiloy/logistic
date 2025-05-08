using Logistics.Domain.Entities.Payments;
using Logistics.Domain.ValueObjects;
using Logistics.Infrastructure.DatabaseEntity.Orders;

namespace Logistics.Infrastructure.DatabaseEntity.Payments;

/// <summary>
/// Класс для хранения сущности платеж в БД
/// </summary>
public class PaymentEntity : BaseDatabaseEntity
{
    /// <summary>
    /// Id заказа
    /// </summary>
    public int OrderId { get; set; }
    
    /// <summary>
    /// Навигационное свойство заказа
    /// </summary>
    public required OrderEntity Order { get; set; }
    
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
    public ICollection<RefundedPaymentEntity>? CancelledPayments { get; set; }
}