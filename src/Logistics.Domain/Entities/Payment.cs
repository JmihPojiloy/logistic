using Logistics.Domain.ValueObjects;

namespace Logistics.Domain.Entities;

public class Payment : BaseEntity
{
    public int UserId { get; set; }
    public required User User { get; set; }
    public int OrderId { get; set; }
    public required Order Order { get; set; }
    public int ExternalReceiptId { get; set; }
    public required Money Amount { get; set; }
    public DateTime PaymentDate { get; set; }
    public virtual ICollection<CancelledPayment>? CancelledPayments { get; set; }
}