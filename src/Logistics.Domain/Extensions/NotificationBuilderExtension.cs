
using Logistics.Domain.Builders;
using Logistics.Domain.Entities.Users;

namespace Logistics.Domain.Extensions;

/// <summary>
/// Класс расширения пользователя для создания уведомления 
/// </summary>
public static class NotificationBuilderExtension
{
    public static NotificationBuilder Notify(this User user)
    {
        return new NotificationBuilder(user);
    }
}