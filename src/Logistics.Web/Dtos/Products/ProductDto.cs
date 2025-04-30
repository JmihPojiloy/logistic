using Logistics.Domain.ValueObjects;

namespace Logistics.Web.Dtos.Products;

public class ProductDto: BaseDto
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public Money? Price { get; set; }
    public double? Weight { get; set; }
    public double? Height { get; set; }
    public double? Width { get; set; }
    public int Code {get; set;}
}