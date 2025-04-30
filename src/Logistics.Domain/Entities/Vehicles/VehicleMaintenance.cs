namespace Logistics.Domain.Entities.Vehicles;

public class VehicleMaintenance : BaseEntity
{
    public int VehicleId { get; set; }
    public required Vehicle Vehicle { get; set; }
    public DateTime MaintenanceDate { get; set; }
    public required string Description { get; set; }
    public decimal MaintenancePrice { get; set; }
}