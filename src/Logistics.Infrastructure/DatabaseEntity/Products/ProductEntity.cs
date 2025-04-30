using Logistics.Domain.ValueObjects;

namespace Logistics.Infrastructure.DatabaseEntity.Products;

public class ProductEntity : BaseEntity
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public Money? Price { get; set; }
    public double? Weight { get; set; }
    public double? Height { get; set; }
    public double? Width { get; set; }
    public int Code {get; set;}
}