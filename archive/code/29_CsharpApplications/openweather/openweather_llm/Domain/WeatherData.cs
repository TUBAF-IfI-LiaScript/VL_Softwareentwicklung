namespace WeatherApp.Domain;

public class WeatherData
{
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    public string Description { get; set; } = string.Empty;
    public double Temperature { get; set; }
    public double WindSpeed { get; set; }
    public string City { get; set; } = string.Empty;
}