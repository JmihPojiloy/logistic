using Logistics.Domain.Entities.Addresses;
using Logistics.Domain.Entities.Orders;
using Logistics.Domain.Entities.Vehicles;
using Logistics.Domain.ValueObjects;

namespace Logistics.Domain.Entities.Delivery;

/// <summary>
/// Класс маршрута
/// </summary>
public class Route : BaseEntity
{
    /// <summary>
    /// Id транспорта
    /// </summary>
    public int VehicleId { get; set; }
    
    /// <summary>
    /// Навигационное свойство транспорта
    /// </summary>
    public Vehicle? Vehicle { get; set; }
    
    /// <summary>
    /// Id адреса
    /// </summary>
    public int AddressId { get; set; }
    
    /// <summary>
    /// Навигационное свойство адреса
    /// </summary>
    public Address? Address { get; set; }
    
    /// <summary>
    /// Расстояние
    /// </summary>
    public int? Distance { get; set; }
    
    /// <summary>
    /// Стоимость за километр
    /// </summary>
    public Money? Cost { get; set; }
    
    /// <summary>
    /// Время выполнения
    /// </summary>
    public TimeSpan? LeadTime { get; set; }

    /// <summary>
    /// Метод расчета расстояния
    /// </summary>
    /// <param name="shippingAddress">Адрес отгрузки</param>
    /// <param name="deliveryAddress">Адрес доставки</param>
    /// <exception cref="ArgumentException">Ошибка пустого аргумента</exception>
    public void SetDistance(Address shippingAddress, Address deliveryAddress)
    {
        if (shippingAddress.Latitude == null || 
            shippingAddress.Longitude == null || 
            deliveryAddress.Latitude == null ||
            deliveryAddress.Longitude == null)
        {
            throw new ArgumentException("Invalid arguments to SetDistance");
        }
        const double earthRadiusKm = 6371.0;
        
        var startLatitude = shippingAddress.Latitude * Math.PI / 180;
        var startLongitude = shippingAddress.Longitude * Math.PI / 180;
        var endLatitude = deliveryAddress.Latitude * Math.PI / 180;
        var endLongitude = deliveryAddress.Longitude * Math.PI / 180;
        
        var u = Math.Sin((double)((endLatitude - startLatitude) / 2)!);
        var v = Math.Sin((double)((endLongitude - startLongitude) / 2)!);
        var result = 
            2.0 * earthRadiusKm * Math.Asin(Math.Sqrt(u * u + Math.Cos((double)startLatitude!) * Math.Cos((double)endLatitude!) * v * v));
        
        Distance = (int)result;
    }
}