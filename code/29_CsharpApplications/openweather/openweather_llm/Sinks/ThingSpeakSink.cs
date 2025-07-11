using WeatherApp.Domain;
using Microsoft.Extensions.Logging;

namespace WeatherApp.Sinks;

public class ThingSpeakSink : IDataSink
{
    private readonly string apiKey;
    private readonly ILogger<ThingSpeakSink> logger;
    private readonly HttpClient http = new();

    public ThingSpeakSink(string apiKey, ILogger<ThingSpeakSink> logger)
    {
        this.apiKey = apiKey;
        this.logger = logger;
    }

    public void OnWeatherDataReceived(WeatherData data)
    {
        if (string.IsNullOrEmpty(apiKey)) return;

        var url = $"https://api.thingspeak.com/update?api_key={apiKey}&field1={data.Temperature}&field2={data.WindSpeed}";
        try
        {
            var response = http.GetStringAsync(url).Result;
            logger.LogInformation("ThingSpeak: {response}", response);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Fehler beim Senden an ThingSpeak");
        }
    }
}