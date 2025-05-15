using System.Globalization;
using Logistics.Application.Exceptions;
using Logistics.Application.Interfaces.Geo;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Logistics.Infrastructure.ExternalServices.Geo;

/// <summary>
/// Класс реализации сервиса для получения координат по адресу
/// </summary>
public class YandexGeoService : IGeoService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    private readonly ILogger<YandexGeoService> _logger;
    private const string BaseUrl = "https://geocode-maps.yandex.ru/v1/";

    public YandexGeoService(
        IHttpClientFactory httpClientFactory,
        IConfiguration configuration,
        ILogger<YandexGeoService> logger)
    {
        _httpClient = httpClientFactory.CreateClient();
        _configuration = configuration;
        _logger = logger;
    }

    /// <summary>
    /// Метод получения координат по адресу, использует три попытки подключения
    /// </summary>
    /// <param name="address">Адрес в формате [страна, город, улица, дом]</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Широта и долгота</returns>
    /// <exception cref="YandexGeoServiceException">Ошибка геосервиса</exception>
    public async Task<(double lat, double lon)> GetCoordinates(string address, CancellationToken cancellationToken)
    {
        var token = _configuration["YandexToken"];
        var url = $"{BaseUrl}?apikey={token}&geocode={address}&format=json";

        var entries = 3;

        for (int i = 0; i < entries; i++)
        {
            try
            {
                var response = await _httpClient.GetAsync(url, cancellationToken);
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync(cancellationToken);
                var data = JsonConvert.DeserializeObject<YandexGeoPosDto>(content);

                var pos = data?
                    .Response?
                    .GeoObjectCollection?
                    .FeatureMember?
                    .FirstOrDefault()?
                    .GeoObject?
                    .Point?
                    .Pos;

                if (string.IsNullOrWhiteSpace(pos))
                {
                    throw new YandexGeoServiceException($"Yandex returned empty position at address - [{address}].");
                }

                var parts = pos.Split(' ');
                return (
                    double.Parse(parts[1], CultureInfo.InvariantCulture),
                    double.Parse(parts[0], CultureInfo.InvariantCulture));
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "GeoService failed to fetch coordinates. Attempt {Attempt}/3", i + 1);
                await Task.Delay(500, cancellationToken);   
            }
        }
        
        throw new YandexGeoServiceException($"Yandex Geocode API failed after multiple retries at address - [{address}].");
    }
}