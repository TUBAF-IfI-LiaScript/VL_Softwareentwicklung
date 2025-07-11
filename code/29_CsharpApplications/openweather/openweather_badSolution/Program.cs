using System;
using System.Net;
using System.Text.Json;
using System.IO;

using Microsoft.Extensions.Configuration;

class Program
{
    static void Main()
    {
        var config = new ConfigurationBuilder()
            .AddUserSecrets<Program>()
            .Build();

        var apiKey = config["OpenWeatherMap:ApiKey"];
        var thingSpeakApiKey = config["ThingSpeak:ApiKey"]; // ThingSpeak Write API Key
        string city = "Freiberg";

        WebClient client = new WebClient();
        string city_url = $"http://api.openweathermap.org/geo/1.0/direct?q={city}&limit=1&appid={apiKey}";
        string city_json = client.DownloadString(city_url);
        Console.WriteLine(city_json);

        JsonDocument cityDoc = JsonDocument.Parse(city_json);
        JsonElement cityRoot = cityDoc.RootElement;
        double lat = cityRoot[0].GetProperty("lat").GetDouble();
        double lon = cityRoot[0].GetProperty("lon").GetDouble();

        string weather_url = $"http://api.openweathermap.org/data/2.5/weather?lat={lat}&lon={lon}&appid={apiKey}&units=metric";
        Console.WriteLine(weather_url); 

        string weather_json = client.DownloadString(weather_url);
        Console.WriteLine(weather_json);

        JsonDocument weatherDoc = JsonDocument.Parse(weather_json);
        JsonElement weatherRoot = weatherDoc.RootElement;   
        string wetter = weatherRoot.GetProperty("weather")[0].GetProperty("description").GetString();
        double temp = weatherRoot.GetProperty("main").GetProperty("temp").GetDouble();
        double wind = weatherRoot.GetProperty("wind").GetProperty("speed").GetDouble();

        Console.WriteLine($"\nWetter in {city}:");
        Console.WriteLine($"Beschreibung: {wetter}");
        Console.WriteLine($"Temperatur: {temp}°C");
        Console.WriteLine($"Windgeschwindigkeit: {wind} m/s");

        // CSV-Datei erstellen/erweitern
        string csvFilePath = "wetterdaten.csv";
        string csvLine = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss},{city},{wetter},{temp},{wind}";
        
        // Prüfen ob CSV-Datei bereits existiert
        if (!File.Exists(csvFilePath))
        {
            // Header schreiben falls Datei nicht existiert
            string header = "Datum,Stadt,Beschreibung,Temperatur_C,Windgeschwindigkeit_ms";
            File.WriteAllText(csvFilePath, header + Environment.NewLine);
        }
        
        // Wetterdaten anhängen
        File.AppendAllText(csvFilePath, csvLine + Environment.NewLine);
        
        Console.WriteLine($"\nWetterdaten wurden in '{csvFilePath}' gespeichert.");

        // ThingSpeak Daten senden
        if (!string.IsNullOrEmpty(thingSpeakApiKey))
        {
            string thingSpeakUrl = $"https://api.thingspeak.com/update?api_key={thingSpeakApiKey}&field1={temp}&field2={wind}";
            
            try
            {
                string thingSpeakResponse = client.DownloadString(thingSpeakUrl);
                Console.WriteLine($"ThingSpeak Response: {thingSpeakResponse}");
                
                if (int.TryParse(thingSpeakResponse, out int entryId) && entryId > 0)
                {
                    Console.WriteLine($"Daten erfolgreich an ThingSpeak gesendet! Entry ID: {entryId}");
                }
                else
                {
                    Console.WriteLine("Fehler beim Senden an ThingSpeak.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fehler beim Senden an ThingSpeak: {ex.Message}");
            }
        }
        else
        {
            Console.WriteLine("ThingSpeak API Key nicht gefunden. Daten werden nicht gesendet.");
        }
    }
}