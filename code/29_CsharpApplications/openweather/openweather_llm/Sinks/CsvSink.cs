using WeatherApp.Domain;
using Microsoft.Extensions.Logging;

namespace WeatherApp.Sinks;

public class CsvSink : IDataSink
{
    private readonly string path;
    private readonly ILogger<CsvSink> logger;

    public CsvSink(string path, ILogger<CsvSink> logger)
    {
        this.path = path;
        this.logger = logger;
    }

    public void OnWeatherDataReceived(WeatherData data)
    {
        bool fileExists = File.Exists(path);
        using var writer = new StreamWriter(path, append: true);
        if (!fileExists)
            writer.WriteLine("Timestamp,City,Description,Temperature,WindSpeed");
        writer.WriteLine($"{data.Timestamp:u},{data.City},{data.Description},{data.Temperature},{data.WindSpeed}");
        logger.LogInformation("CSV gespeichert: {path}", path);
    }
}