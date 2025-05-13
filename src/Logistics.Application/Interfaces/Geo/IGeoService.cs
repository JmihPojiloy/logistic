
namespace Logistics.Application.Interfaces.Geo;
/// <summary>
/// Интерфейс для получения координат по адресу
/// </summary>
public interface IGeoService
{
    public Task<(double lat, double lon)> GetCoordinates(string address);
}