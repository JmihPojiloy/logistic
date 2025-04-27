using Logistics.Domain.ValueObjects;

namespace Logistics.Domain.Entities;

public class Product : BaseEntity
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public required Money Price { get; set; }
    public double Weight { get; set; }
    public double Height { get; set; }
    public double Width { get; set; }
    public int Code {get; set;}
}