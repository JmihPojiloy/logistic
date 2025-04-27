using Logistics.Domain.Enums;

namespace Logistics.Domain.Entities;

public class Letter : BaseEntity
{
    public int NotificationId { get; set; }
    public int HashCode {get; set;}
    public required string SenderEmail {get; set;}
    public required string RecipientEmail {get; set;}
    public string? Subject {get; set;}
    public string? Title {get; set;}
    public string? Text {get; set;}
    public LetterStatus Status {get; set;}
    public string? Url {get; set;}
    public DateTime SendDate {get; set;}
}