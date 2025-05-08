namespace Logistics.Infrastructure.DatabaseEntity.Addresses;

/// <summary>
/// Класс для хранения сущности адреса в БД
/// </summary>
public class AddressEntity : BaseDatabaseEntity
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