using Logistics.Domain.Entities.Addresses;
using Logistics.Domain.Entities.Vehicles;

namespace Logistics.Domain.Entities.Delivery;

public class Route : BaseEntity
{
    public int VehicleId { get; set; }
    public Vehicle? Vehicle { get; set; }
    public int AddressId { get; set; }
    public Address? Address { get; set; }
    public int OrderId { get; set; }
    public Order? Order { get; set; }
    public int? Distance { get; set; }
    public DateTime? LeadTime { get; set; }
}