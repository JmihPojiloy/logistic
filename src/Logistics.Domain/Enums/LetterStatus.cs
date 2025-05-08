namespace Logistics.Domain.Enums;

/// <summary>
/// Статусы письма
/// </summary>
public enum LetterStatus
{
    InQueue = 0, // в очереди на отправку
    Error = 1, // ошибка отправки
    Success = 2, // успешная отправка
    Duplicate = 3, // дубликат
    InvalidEmail = 4, // неверный адрес
}