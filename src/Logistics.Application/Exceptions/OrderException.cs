namespace Logistics.Application.Exceptions;

/// <summary>
/// Класс ошибки обработки заказа
/// </summary>
public class OrderException : Exception
{
    public OrderException(int id, string message = "") : base($"Exception occured: Order by Id - {id}. {message}") { }
}