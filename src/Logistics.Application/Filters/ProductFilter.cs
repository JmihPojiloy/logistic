using Logistics.Application.Interfaces.Filters;

namespace Logistics.Application.Filters;

/// <summary>
/// Фильтр для репозитория товаров
/// </summary>
public class ProductFilter : IFilter
{
    /// <summary>
    /// Id товаров
    /// </summary>
    public List<int> ProductsIds { get; set; } = new();
}