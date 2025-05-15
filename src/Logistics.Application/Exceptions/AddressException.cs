namespace Logistics.Application.Exceptions;

/// <summary>
/// Класс ошибки обработки адреса
/// </summary>
public class AddressException : Exception
{
    public AddressException(int id) : 
        base($"Адрес c ID {id} не заполнен или заполнен не верно.") { }
}