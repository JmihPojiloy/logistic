using Logistics.Domain.Enums;
using Logistics.Domain.ValueObjects;

namespace Logistic.Tests;

[TestFixture]
public class MoneyTests
{
    [Test]
    public void Constructor_ShouldRoundValue_ToTwoDecimalPlaces()
    {
        var money = new Money(123.4567m, Currency.USD);
        Assert.That(money.Sum, Is.EqualTo(123.46m));
    }

    [Test]
    public void Constructor_ShouldThrow_WhenNegativeSum()
    {
        Assert.Throws<ArgumentException>(() =>
        {
            var money = new Money(-1m, Currency.EUR);
        });
    }

    [Test]
    public void Equals_ShouldReturnTrue_ForEqualMoney()
    {
        var a = new Money(100m, Currency.EUR);
        var b = new Money(100m, Currency.EUR);
        Assert.That(a.Equals(b), Is.True);
        Assert.That(a == b, Is.True);
    }

    [Test]
    public void Equals_ShouldReturnFalse_ForDifferentCurrency()
    {
        var a = new Money(100m, Currency.EUR);
        var b = new Money(100m, Currency.USD);
        Assert.That(a.Equals(b), Is.False);
    }

    [Test]
    public void OperatorAdd_ShouldReturnSum_OfSameCurrency()
    {
        var a = new Money(50m, Currency.KZT);
        var b = new Money(70m, Currency.KZT);
        var result = a + b;
        Assert.That(result.Sum, Is.EqualTo(120m));
    }

    [Test]
    public void OperatorAdd_ShouldThrow_WhenDifferentCurrency()
    {
        var a = new Money(10m, Currency.USD);
        var b = new Money(5m, Currency.EUR);
        Assert.Throws<InvalidOperationException>(() => { var _ = a + b; });
    }

    [Test]
    public void OperatorMultiply_ShouldScaleAmount()
    {
        var money = new Money(10.00m, Currency.EUR);
        var result = money * 2.5m;
        Assert.That(result.Sum, Is.EqualTo(25.00m));
    }

    [Test]
    public void ToFormattedString_ShouldRespectCurrencyCulture()
    {
        var money = new Money(99.99m, Currency.USD);
        var formatted = money.ToFormattedString();
        Assert.That(formatted, Does.Contain("$"));
    }
}