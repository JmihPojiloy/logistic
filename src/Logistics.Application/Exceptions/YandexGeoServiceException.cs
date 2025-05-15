namespace Logistics.Application.Exceptions;

/// <summary>
/// Класс ошибки яндекс гео сервиса
/// </summary>
public class YandexGeoServiceException : Exception
{
    public YandexGeoServiceException(string message) : base(message){}
}