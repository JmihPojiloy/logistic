namespace Logistics.Domain.Entities;

public class Address : BaseEntity
{
    
    public int Zip { get; private set; }
    public string Country { get; private set; } = null!;
    public string City { get; private set; } = null!;
    public string Street { get; private set; } = null!;
    public double Latitude { get; private set; }
    public double Longitude { get; private set; }

    public int UserId { get; private set; }
    public User? User { get; private set; }

    public int WarehouseId { get; private set; }
    public Warehouse? Warehouse { get; private set; }
    
    public Address()
    {
    }
    
    public Address(
        int zip,
        string country,
        string city,
        string street,
        double latitude,
        double longitude,
        int userId,
        int warehouseId)
    {
        if (string.IsNullOrWhiteSpace(country))
            throw new ArgumentException("Country is required.", nameof(country));

        if (string.IsNullOrWhiteSpace(city))
            throw new ArgumentException("City is required.", nameof(city));

        if (string.IsNullOrWhiteSpace(street))
            throw new ArgumentException("Street is required.", nameof(street));

        Zip = zip;
        Country = country;
        City = city;
        Street = street;
        Latitude = latitude;
        Longitude = longitude;
        UserId = userId;
        WarehouseId = warehouseId;
    }
}
