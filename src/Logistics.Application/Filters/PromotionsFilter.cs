using Logistics.Application.Interfaces.Filters;

namespace Logistics.Application.Filters;

/// <summary>
/// Фильтр для репозитория промоакции
/// </summary>
public class PromotionsFilter : IFilter
{
    /// <summary>
    /// Id промоакций
    /// </summary>
    public List<int> PromotionsIds { get; set; } = new();
    
    /// <summary>
    /// Дата
    /// </summary>
    public DateTime? EndDate { get; set; }
}