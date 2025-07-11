using System.Net.Http;
using System.Text.Json;
using WeatherApp.Domain;
using Microsoft.Extensions.Logging;

namespace WeatherApp.Services;

public class OpenWeatherMapWeatherProvider : IWeatherProvider
{
    private readonly string apiKey;
    private readonly ILogger<OpenWeatherMapWeatherProvider> logger;
    private readonly HttpClient http = new();

    public OpenWeatherMapWeatherProvider(string apiKey, ILogger<OpenWeatherMapWeatherProvider> logger)
    {
        this.apiKey = apiKey;
        this.logger = logger;
    }

    public async Task<WeatherData> GetWeatherAsync(double lat, double lon)
    {
        var url = $"http://api.openweathermap.org/data/2.5/weather?lat={lat}&lon={lon}&appid={apiKey}&units=metric";
        var json = await http.GetStringAsync(url);
        var doc = JsonDocument.Parse(json);
        var root = doc.RootElement;

        var data = new WeatherData
        {
            Description = root.GetProperty("weather")[0].GetProperty("description").GetString() ?? "",
            Temperature = root.GetProperty("main").GetProperty("temp").GetDouble(),
            WindSpeed = root.GetProperty("wind").GetProperty("speed").GetDouble()
        };

        logger.LogInformation("Wetterdaten abgerufen: {@data}", data);
        return data;
    }
}