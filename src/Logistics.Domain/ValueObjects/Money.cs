using System.Globalization;
using Logistics.Domain.Enums;
using Logistics.Domain.Interfaces;

namespace Logistics.Domain.ValueObjects;

/// <summary>
/// Value Object для работы с денежными суммами и валютами.
/// Инкапсулирует логику округления, конвертации и форматирования.
/// </summary>
public sealed class Money : IEquatable<Money>
{
    private const int DecimalPlaces = 2;
    private decimal _sum;

    public decimal Sum
    {
        get => _sum;
        private init => _sum = Math.Round(value, DecimalPlaces);
    }

    public Currency Currency { get; private init; }

    // Основной конструктор (обязательные поля)
    public Money(decimal sum, Currency currency)
    {
        if (sum < 0)
            throw new ArgumentException("Сумма не может быть отрицательной");

        Sum = sum;
        Currency = currency;
    }

    // Для ORM (EF Core)
    private Money()
    {
    }

    /// <summary>
    /// Конвертация валют через внешний сервис (инфраструктурная зависимость)
    /// </summary>
    public Money ConvertTo(Currency targetCurrency, ICurrencyConverter converter)
    {
        if (converter == null)
            throw new ArgumentNullException(nameof(converter));

        decimal convertedSum = converter.Convert(Currency, targetCurrency, Sum);
        return new Money(convertedSum, targetCurrency);
    }

    // --- Форматирование ---
    public string ToFormattedString() => Sum.ToString("C", GetCulture(Currency));

    private static CultureInfo GetCulture(Currency currency) =>
        currency switch
        {
            Currency.USD => new CultureInfo("en-US"),
            Currency.EUR => new CultureInfo("de-DE"),
            Currency.KZT => new CultureInfo("kz-KZ"),
            _ => new CultureInfo("ru-RU") // По умолчанию
        };

    // --- Математические операторы ---
    public static Money operator +(Money a, Money b)
    {
        ValidateSameCurrency(a, b);
        return new Money(a.Sum + b.Sum, a.Currency);
    }

    public static Money operator -(Money a, Money b)
    {
        ValidateSameCurrency(a, b);
        return new Money(a.Sum - b.Sum, a.Currency);
    }

    public static Money operator *(Money money, decimal multiplier) =>
        new Money(money.Sum * multiplier, money.Currency);

    // --- Сравнение ---
    public static bool operator ==(Money? a, Money? b) =>
        a?.Equals(b) ?? b is null;

    public static bool operator !=(Money? a, Money? b) => !(a == b);

    public bool Equals(Money? other) =>
        other != null &&
        Sum == other.Sum &&
        Currency == other.Currency;

    public override bool Equals(object? obj) =>
        obj is Money other && Equals(other);

    public override int GetHashCode() =>
        HashCode.Combine(Sum, Currency);

    // --- Валидация ---
    private static void ValidateSameCurrency(Money a, Money b)
    {
        if (a.Currency != b.Currency)
            throw new InvalidOperationException("Нельзя выполнять операции с разными валютами");
    }
}