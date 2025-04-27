using Logistics.Domain.Enums;

namespace Logistics.Domain.Entities;

public class Order : BaseEntity
{
    public int UserId { get; set; }
    public required User User { get; set; }
    public int VehicleId { get; set; }
    public Vehicle? Vehicle { get; set; }
    public int AddressId { get; set; }
    public required Address Address { get; set; }
    public int RouteId { get; set; }
    public Route? Route { get; set; }
    public int PaymentId { get; set; }
    public Payment? Payment { get; set; }
    public virtual required ICollection<Product> Products { get; set; }
    public OrderStatus Status { get; set; }
}