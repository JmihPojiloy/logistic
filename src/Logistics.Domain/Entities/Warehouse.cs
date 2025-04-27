using Logistics.Domain.Enums;

namespace Logistics.Domain.Entities;

public class Warehouse : BaseEntity
{
    public int AddressId { get; set; }
    public required Address Address { get; set; }
    public required string Name { get; set; }
    public int Square { get; set; }
    public WarehouseStatus Status { get; set; }
}