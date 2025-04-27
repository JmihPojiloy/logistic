using Logistics.Domain.Enums;

namespace Logistics.Domain.Entities;

public class Vehicle : BaseEntity
{
    public int DriverId { get; set; }
    public Driver? Driver { get; set; }
    public int RouteId { get; set; }
    public Route? Route { get; set; }
    public int OrderId { get; set; }
    public Order? Order { get; set; }
    public int VehiceleMaintananceId { get; set; }
    public VehicleMaintenance? VehiceleMaintanance { get; set; }
    public required string Name {get; set;}
    public int LoadCapacity { get; set; }
    public VehicleStatus Status { get; set; }
    public int MileAge  { get; set; }
}