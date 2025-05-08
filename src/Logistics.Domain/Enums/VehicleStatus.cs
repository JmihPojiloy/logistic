namespace Logistics.Domain.Enums;

/// <summary>
/// Статус автомобиля
/// </summary>
public enum VehicleStatus
{
    Free, // свободен
    OnRoute, // на маршруте
    OnMaintenance, // на ТО
}