using Logistics.Application.Interfaces.Filters;

namespace Logistics.Application.Filters;

/// <summary>
/// Фильтр для репозитория маршрутов
/// </summary>
public class RouteFilter : IFilter
{
    /// <summary>
    /// Id адреса маршрута
    /// </summary>
    public int? AddressId { get; set; }
    
    /// <summary>
    /// Id транспорта назначенного на маршрут
    /// </summary>
    public int? VehicleId { get; set; }
}