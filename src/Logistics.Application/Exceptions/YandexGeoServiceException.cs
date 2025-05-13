namespace Logistics.Application.Exceptions;

public class YandexGeoServiceException : Exception
{
    public YandexGeoServiceException(string message) : base(message){}
}