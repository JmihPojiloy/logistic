using Logistics.Domain.Enums;

namespace Logistics.Domain.Entities;

public class User : BaseEntity
{
    public string? LastName { get; set; }
    public required string FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string? Email { get; set; }
    public int PhoneNumber { get; set; }
    public Gender? Gender { get; set; }
    public UserRole UserRole { get; set; }
    public virtual ICollection<Address>? Addresses { get; set; }
    public virtual ICollection<Order>? Orders { get; set; }
}