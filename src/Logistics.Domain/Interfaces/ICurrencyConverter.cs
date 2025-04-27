using Logistics.Domain.Enums;

namespace Logistics.Domain.Interfaces;

/// <summary>
/// Контракт для конвертера валют (реализуется в Infrastructure)
/// </summary>
public interface ICurrencyConverter
{
    decimal Convert(Currency from, Currency to, decimal amount);
}