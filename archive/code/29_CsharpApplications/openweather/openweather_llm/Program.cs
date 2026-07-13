using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using WeatherApp.Domain;
using WeatherApp.Services;
using WeatherApp.Sinks;

public class Program
{
    public static async Task Main(string[] args)
    {
        var host = Host.CreateDefaultBuilder(args)
            .ConfigureServices((context, services) =>
            {
                IConfiguration config = context.Configuration;

                string openWeatherApiKey = config["OpenWeatherMap:ApiKey"] ?? throw new InvalidOperationException("OpenWeatherMap:ApiKey is not configured");
                string thingSpeakApiKey = config["ThingSpeak:ApiKey"] ?? throw new InvalidOperationException("ThingSpeak:ApiKey is not configured");

                services.AddLogging(cfg => cfg.AddConsole());

                services.AddSingleton<IGeoLocationService>(sp =>
                    new OpenWeatherMapGeoService(openWeatherApiKey, sp.GetRequiredService<ILogger<OpenWeatherMapGeoService>>()));

                services.AddSingleton<IWeatherProvider>(sp =>
                    new OpenWeatherMapWeatherProvider(openWeatherApiKey, sp.GetRequiredService<ILogger<OpenWeatherMapWeatherProvider>>()));

                services.AddSingleton<IWeatherProcessor, WindWarningProcessor>();

                services.AddSingleton<IDataSink>(sp =>
                    new CsvSink("wetterdaten.csv", sp.GetRequiredService<ILogger<CsvSink>>()));

                services.AddSingleton<IDataSink>(sp =>
                    new ThingSpeakSink(thingSpeakApiKey, sp.GetRequiredService<ILogger<ThingSpeakSink>>()));

                services.AddSingleton<IDataSink, ConsoleSink>();

                services.AddSingleton<WeatherAggregator>();
            })
            .Build();

        var aggregator = host.Services.GetRequiredService<WeatherAggregator>();
        await aggregator.FetchAndDistributeAsync("Freiberg");
    }
}