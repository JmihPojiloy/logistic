using System.Net;
using Logistics.Application.Exceptions;
using Logistics.Infrastructure.ExternalServices.Geo;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;

namespace Logistic.Tests;

[TestFixture]
public class YandexGeoServiceTests
{
    private Mock<IConfiguration> _configurationMock = null!;
    private Mock<ILogger<YandexGeoService>> _loggerMock = null!;
    private Mock<IHttpClientFactory> _httpClientFactoryMock = null!;
    private Mock<HttpMessageHandler> _httpMessageHandlerMock = null!;
    private YandexGeoService _geoService = null!;

    [SetUp]
    public void SetUp()
    {
        _configurationMock = new Mock<IConfiguration>();
        _loggerMock = new Mock<ILogger<YandexGeoService>>();
        _httpMessageHandlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);

        var httpClient = new HttpClient(_httpMessageHandlerMock.Object);

        _httpClientFactoryMock = new Mock<IHttpClientFactory>();
        _httpClientFactoryMock.Setup(f => f.CreateClient(It.IsAny<string>())).Returns(httpClient);

        _configurationMock.Setup(c => c["YandexToken"]).Returns("test-token");

        _geoService = new YandexGeoService(
            _httpClientFactoryMock.Object,
            _configurationMock.Object,
            _loggerMock.Object);
    }

    [Test]
    public async Task GetCoordinates_ReturnsValidCoordinates_WhenApiResponseIsValid()
    {
        const string jsonResponse = """
        {
          "response": {
            "GeoObjectCollection": {
              "featureMember": [
                {
                  "GeoObject": {
                    "Point": {
                      "pos": "25.197300 55.274243"
                    }
                  }
                }
              ]
            }
          }
        }
        """;

        SetupHttpResponse(jsonResponse);

        var (lat, lon) = await _geoService.GetCoordinates("Dubai", CancellationToken.None);

        Assert.That(lat, Is.EqualTo(55.274243));
        Assert.That(lon, Is.EqualTo(25.1973));
    }

    [Test]
    public void GetCoordinates_ThrowsException_WhenPosIsMissing()
    {
        const string jsonResponse = """
        {
          "response": {
            "GeoObjectCollection": {
              "featureMember": [
                {
                  "GeoObject": {
                    "Point": {
                      "pos": ""
                    }
                  }
                }
              ]
            }
          }
        }
        """;

        SetupHttpResponse(jsonResponse);

        var ex = Assert.ThrowsAsync<YandexGeoServiceException>(() => _geoService.GetCoordinates("Dubai", CancellationToken.None));
        Assert.That(ex!.Message, Does.Contain("Yandex Geocode API failed after multiple retries at address - [Dubai]."));
    }

    [Test]
    public void GetCoordinates_ThrowsException_AfterRetries()
    {
        _httpMessageHandlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ThrowsAsync(new HttpRequestException("Simulated failure"));

        var ex = Assert.ThrowsAsync<YandexGeoServiceException>(() => _geoService.GetCoordinates("Dubai", CancellationToken.None));
        Assert.That(ex!.Message, Does.Contain("failed after multiple retries"));
    }

    private void SetupHttpResponse(string jsonResponse)
    {
        _httpMessageHandlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(jsonResponse)
            });
    }
}
