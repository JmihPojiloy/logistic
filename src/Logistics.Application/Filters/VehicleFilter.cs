using Logistics.Application.Interfaces.Filters;
using Logistics.Domain.Enums;

namespace Logistics.Application.Filters;

/// <summary>
/// Фильтр для репозитория транспорта
/// </summary>
public class VehicleFilter : IFilter
{
    /// <summary>
    /// Статус транспорта
    /// </summary>
    public VehicleStatus? Status {get; set;}
}