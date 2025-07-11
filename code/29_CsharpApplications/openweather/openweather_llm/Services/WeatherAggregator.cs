using WeatherApp.Domain;
using Microsoft.Extensions.Logging;

namespace WeatherApp.Services;

public class WeatherAggregator
{
    private readonly IGeoLocationService geo;
    private readonly IWeatherProvider provider;
    private readonly IEnumerable<IWeatherProcessor> processors;
    private readonly IEnumerable<IDataSink> sinks;
    private readonly ILogger<WeatherAggregator> logger;

    public WeatherAggregator(
        IGeoLocationService geo,
        IWeatherProvider provider,
        IEnumerable<IWeatherProcessor> processors,
        IEnumerable<IDataSink> sinks,
        ILogger<WeatherAggregator> logger)
    {
        this.geo = geo;
        this.provider = provider;
        this.processors = processors;
        this.sinks = sinks;
        this.logger = logger;
    }

    public async Task FetchAndDistributeAsync(string city)
    {
        logger.LogInformation("Abruf für Stadt: {city}", city);
        var (lat, lon) = await geo.GetCoordinatesAsync(city);
        var data = await provider.GetWeatherAsync(lat, lon);
        data.City = city;

        foreach (var processor in processors)
            processor.Process(data);

        foreach (var sink in sinks)
            sink.OnWeatherDataReceived(data);
    }
}