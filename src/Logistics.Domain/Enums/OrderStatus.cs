namespace Logistics.Domain.Enums;

/// <summary>
/// Статус заказа
/// </summary>
public enum OrderStatus
{
    NotPaid, // не оплачен
    Assembly, // сборка заказа
    Loading, // погрузка
    InTransit, // в пути
    Arrived, // прибыл
    Issued // выдан
}