using Logistics.Domain.Entities.Notifications;
using Logistics.Domain.Entities.Users;
using Logistics.Domain.Enums;

namespace Logistics.Domain.Builders;

/// <summary>
/// Вспомогательный класс для создания уведомления
/// </summary>
public class NotificationBuilder
{
    private readonly Notification _notification;

    public NotificationBuilder(User recipient)
    {
        _notification = new Notification
        {
            Recipient = recipient,
            RecipientId = recipient.Id,
            SendDate = DateTime.UtcNow,
            Status = NotificationStatus.New
        };
    }

    public NotificationBuilder WithType(NotificationType type = NotificationType.All)
    {
        _notification.Type = type;
        return this;
    }

    public NotificationBuilder WithTitle(string title = "")
    {
        _notification.Title = title;
        return this;
    }

    public NotificationBuilder WithText(string text = "")
    {
        _notification.Text = text;
        return this;
    }

    public NotificationBuilder WithEmail(bool isEmail = false)
    {
        _notification.IsEmail = isEmail;
        return this;
    }

    public NotificationBuilder WithLetter(Letter? letter = null)
    {
        _notification.Letter = letter;
        return this;
    }

    public NotificationBuilder WithHashCode(int hashCode = 0)
    {
        _notification.HashCode = hashCode;
        return this;
    }

    public Notification Build()
    {
        return _notification;
    }
}