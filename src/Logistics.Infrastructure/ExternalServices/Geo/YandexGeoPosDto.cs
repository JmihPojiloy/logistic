using Newtonsoft.Json;

namespace Logistics.Infrastructure.ExternalServices.Geo;
/// <summary>
/// Классы для десериализации ответа от геосервиса
/// </summary>

public sealed record YandexGeoPosDto(
    [property: JsonProperty("response")] YandexGeoResponse Response
);

public sealed record YandexGeoResponse(
    [property: JsonProperty("GeoObjectCollection")] YandexGeoObjectCollection GeoObjectCollection
);

public sealed record YandexGeoObjectCollection(
    [property: JsonProperty("featureMember")] List<YandexGeoFeature> FeatureMember
);

public sealed record YandexGeoFeature(
    [property: JsonProperty("GeoObject")] YandexGeoPointWrapper GeoObject
);

public sealed record YandexGeoPointWrapper(
    [property: JsonProperty("Point")] YandexGeoPoint Point
);

public sealed record YandexGeoPoint(
    [property: JsonProperty("pos")] string Pos
);
