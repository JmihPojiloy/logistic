namespace Logistics.Domain.Enums;

/// <summary>
/// Статусы уведомления
/// </summary>
public enum NotificationStatus
{

    New = 1, // новое
    InProgress = 2, // в обработке
    Finished = 3, // отправлено
    Draft = 4, // черновик
    Canceled = 5 // отменено
}