namespace Logistics.Domain.Entities.Addresses;

public class Address : BaseEntity
{
    public int? Zip { get; set; }
    public string? Country { get; set; }
    public string? City { get; set; }
    public string? Street { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
}
