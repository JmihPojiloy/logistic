using Logistics.Domain.Enums;
using Logistics.Web.Dtos.Addresses;

namespace Logistics.Web.Dtos.Warehouses;

/// <summary>
/// Транспортный класс для обработки сущности склад
/// </summary>
public class WarehouseDto : BaseDto
{
    /// <summary>
    /// Id адреса склада
    /// </summary>
    public int AddressId { get; set; }
    
    /// <summary>
    /// Адрес склада
    /// </summary>
    public required AddressDto Address { get; set; }
    
    /// <summary>
    /// Остатки на складе
    /// </summary>
    public ICollection<InventoryDto> Inventories { get; set; } = new List<InventoryDto>();
    
    /// <summary>
    /// Название склада
    /// </summary>
    public required string Name { get; set; }
    
    /// <summary>
    /// Размер (площадь) склада
    /// </summary>
    public int? Square { get; set; }
    
    /// <summary>
    /// Статус склада
    /// </summary>
    public WarehouseStatus Status { get; set; }
}