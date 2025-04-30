using Logistics.Domain.Entities.Products;

namespace Logistics.Domain.Entities.Warehouses;

public class Inventory : BaseEntity
{
    public int ProductId { get; set; }
    public required Product Product { get; set; }
    public int WarehouseId { get; set; }
    public required Warehouse Warehouse { get; set; }
    public int Quantity { get; set; }
}