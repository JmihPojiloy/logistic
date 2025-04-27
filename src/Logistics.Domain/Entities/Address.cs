namespace Logistics.Domain.Entities;

public class Address : BaseEntity
{
    public int Zip { get; set; }
    public required string Country { get; set; }
    public required string City { get; set; }
    public required string Street { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public int UserId { get; set; }
    public int WarehouseId { get; set; }
}