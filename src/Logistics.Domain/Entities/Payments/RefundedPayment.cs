using Logistics.Domain.ValueObjects;

namespace Logistics.Domain.Entities;

public class RefundedPayment : BaseEntity
{
    public int PaymentId { get; set; }
    public virtual required Payment Payment { get; set; }
    public int? ExternalReceiptId { get; set; }
    public required Money Amount { get; set; }
    public DateTime? CancellationDate { get; set; }
}