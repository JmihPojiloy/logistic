namespace Logistics.Domain.Entities.Addresses;

/// <summary>
/// Класс адреса
/// </summary>
public class Address : BaseEntity
{
    /// <summary>
    /// Индекс
    /// </summary>
    public string? Zip { get; set; }
    
    /// <summary>
    /// Страна
    /// </summary>
    public string? Country { get; set; }
    
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
