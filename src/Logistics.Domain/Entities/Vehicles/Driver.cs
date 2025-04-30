using Logistics.Domain.Entities.Delivery;
using Logistics.Domain.Entities.Vehicles;
using Logistics.Domain.Enums;

namespace Logistics.Domain.Entities;

public class Driver : BaseEntity
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public string? MiddleName { get; set; }
    public string? Email { get; set; }
    public int PhoneNumber { get; set; }
    public Gender? Gender { get; set; }
    public required string DriverLicense { get; set; }
    public int? VehicleId { get; set; }
    public Vehicle? Vehicle { get; set; }
    public int? RouteId { get; set; }
    public Route? Route { get; set; }
    public DriverStatus Status { get; set; }
}