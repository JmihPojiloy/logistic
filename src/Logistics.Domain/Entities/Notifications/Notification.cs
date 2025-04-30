using Logistics.Domain.Enums;

namespace Logistics.Domain.Entities;

public class Notification : BaseEntity
{
    public NotificationType Type { get; set; }
    public int HashCode { get; set; }
    public int SenderId { get; set; }
    public required User Sender { get; set; }
    public int RecipientId { get; set; }
    public required User Recipient { get; set; }
    public string? Title { get; set; }
    public string? Text { get; set; }
    public NotificationStatus Status { get; set; }
    public bool IsEmail { get; set; }
    public DateTime SendDate { get; set; }
}