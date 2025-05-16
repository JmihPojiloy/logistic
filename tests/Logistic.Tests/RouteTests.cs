using Logistics.Domain.Entities.Addresses;
using Logistics.Domain.Entities.Delivery;

namespace Logistic.Tests;

[TestFixture]
public class RouteTests
{
    [Test]
    public void SetDistance_ValidCoordinates_ComputesDistance()
    {
        var shippingAddress = new Address
        {
            Latitude = 55.7558,
            Longitude = 37.6173
        };

        var deliveryAddress = new Address
        {
            Latitude = 59.9343,
            Longitude = 30.3351
        };

        var route = new Route();
        route.SetDistance(shippingAddress, deliveryAddress);

        Assert.That(route.Distance > 0, Is.True);
    }

    [Test]
    public void SetDistance_InvalidCoordinates_ThrowsArgumentException()
    {
        var shippingAddress = new Address();
        var deliveryAddress = new Address();
        var route = new Route();

        Assert.Throws<ArgumentException>(() => route.SetDistance(shippingAddress, deliveryAddress));
    }
}