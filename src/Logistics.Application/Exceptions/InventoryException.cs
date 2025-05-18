namespace Logistics.Application.Exceptions;

/// <summary>
/// Класс обработки ошибок остатков товара
/// </summary>
public class InventoryException : Exception
{
    public InventoryException(string message) : base(message){}
}