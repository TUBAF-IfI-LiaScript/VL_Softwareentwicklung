using System.Net.Http;
using System.Text.Json;
using WeatherApp.Domain;
using Microsoft.Extensions.Logging;

namespace WeatherApp.Services;

public class OpenWeatherMapGeoService : IGeoLocationService
{
    private readonly string apiKey;
    private readonly ILogger<OpenWeatherMapGeoService> logger;
    private readonly HttpClient http = new();

    public OpenWeatherMapGeoService(string apiKey, ILogger<OpenWeatherMapGeoService> logger)
    {
        this.apiKey = apiKey;
        this.logger = logger;
    }

    public async Task<(double Lat, double Lon)> GetCoordinatesAsync(string city)
    {
        var url = $"http://api.openweathermap.org/geo/1.0/direct?q={city}&limit=1&appid={apiKey}";
        var json = await http.GetStringAsync(url);
        var doc = JsonDocument.Parse(json);
        var root = doc.RootElement[0];
        double lat = root.GetProperty("lat").GetDouble();
        double lon = root.GetProperty("lon").GetDouble();
        logger.LogInformation("Geo Koordinaten für {city}: {lat}, {lon}", city, lat, lon);
        return (lat, lon);
    }
}