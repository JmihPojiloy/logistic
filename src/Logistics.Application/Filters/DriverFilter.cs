using Logistics.Application.Interfaces.Filters;
using Logistics.Domain.Enums;

namespace Logistics.Application.Filters;

/// <summary>
/// Фильтр для репозитория водителей
/// </summary>
public class DriverFilter : IFilter
{
    /// <summary>
    /// Статус водителя
    /// </summary>
    public DriverStatus? Status { get; set; }
}