namespace Logistics.Web.Dtos.Addresses;

/// <summary>
/// Транспортный класс для обработки сущности адреса
/// </summary>
public class AddressDto : BaseDto
{
    /// <summary>
    /// Индекс
    /// </summary>
    public string? Zip { get; set; }
    
    /// <summary>
    /// Страна
    /// </summary>
    public string? County { get; set; }
    
    /// <summary>
    /// Город
    /// </summary>
    public string? City { get; set; }
    
    /// <summary>
    /// Улица
    /// </summary>
    public string? Street { get; set; }
    
    /// <summary>
    /// Номер дома
    /// </summary>
    public string? HouseNumber { get; set; }
    
    /// <summary>
    /// Номер офиса или квартиры
    /// </summary>
    public string? ApartmentNumber { get; set; }
    
    /// <summary>
    /// Широта
    /// </summary>
    public double? Latitude { get; set; }
    
    /// <summary>
    /// Долгота
    /// </summary>
    public double? Longitude { get; set; }
}