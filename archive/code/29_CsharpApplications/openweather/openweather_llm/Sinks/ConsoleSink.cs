using WeatherApp.Domain;
using Microsoft.Extensions.Logging;

namespace WeatherApp.Sinks;

public class ConsoleSink : IDataSink
{
    public void OnWeatherDataReceived(WeatherData data)
    {
        Console.WriteLine($"[{data.Timestamp:u}] Wetter in {data.City}: {data.Description}, {data.Temperature}°C, {data.WindSpeed} m/s");
    }
}