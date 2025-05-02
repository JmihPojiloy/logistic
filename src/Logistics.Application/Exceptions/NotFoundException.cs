namespace Logistics.Application.Exceptions;

/// <summary>
/// Класс ошибки не найденной записи в БД
/// </summary>
public class NotFoundException : Exception
{
    /// <summary>
    /// Конструктор для создания класса ошибки
    /// </summary>
    /// <param name="name">Название не найденной сущности</param>
    /// <param name="key">Id сущности</param>
    public NotFoundException(string name, object key)
        : base($"Entity \"{name}\"({key}) not found.") { }
}