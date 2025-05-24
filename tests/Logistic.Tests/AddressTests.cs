using Logistics.Domain.Entities.Addresses;

namespace Logistic.Tests;

[TestFixture]
public class AddressTests
{
    [Test]
    public void GetAddressForGeocoding_ValidFields_ReturnsFormattedAddress()
    {
        var address = new Address
        {
            Country = " Russia ",
            City = " Moscow ",
            Street = " Tverskaya ",
            HouseNumber = "1"
        };

        var result = address.GetAddressForGeocoding();

        Assert.That(result, Is.EqualTo("Russia, Moscow, Tverskaya, 1"));
    }

    [TestCase(null, "City", "Street", "1")]
    [TestCase("Country", null, "Street", "1")]
    [TestCase("Country", "City", null, "1")]
    [TestCase("Country", "City", "Street", null)]
    public void GetAddressForGeocoding_MissingFields_ReturnsEmpty(
        string? country, string? city, string? street, string? houseNumber)
    {
        var address = new Address
        {
            Country = country,
            City = city,
            Street = street,
            HouseNumber = houseNumber
        };

        var result = address.GetAddressForGeocoding();

        Assert.That(result, Is.Empty);
    }
}