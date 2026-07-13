namespace WeatherApp.Domain;

public interface IGeoLocationService
{
    Task<(double Lat, double Lon)> GetCoordinatesAsync(string city);
}

public interface IWeatherProvider
{
    Task<WeatherData> GetWeatherAsync(double lat, double lon);
}

public interface IDataSink
{
    void OnWeatherDataReceived(WeatherData data);
}

public interface IWeatherProcessor
{
    void Process(WeatherData data);
}