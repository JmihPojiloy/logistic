namespace Logistics.Domain.Entities.Delivery;

public class DeliveryTracking : BaseEntity
{
    public int OrderId { get; set; }
    public required Order Order { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
}