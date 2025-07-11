using WeatherApp.Domain;
using Microsoft.Extensions.Logging;

namespace WeatherApp.Services;

public class WindWarningProcessor : IWeatherProcessor
{
    private readonly ILogger<WindWarningProcessor> logger;

    public WindWarningProcessor(ILogger<WindWarningProcessor> logger)
    {
        this.logger = logger;
    }

    public void Process(WeatherData data)
    {
        if (data.WindSpeed > 10)
            logger.LogWarning("Starker Wind in {city}: {speed} m/s", data.City, data.WindSpeed);
    }
}