<!--

author:   Sebastian Zug, Galina Rudolf & André Dietrich
email:    sebastian.zug@informatik.tu-freiberg.de
version:  1.0.4
language: de
narrator: Deutsch Female
comment:  Zusammenfassung und Ausblick
tags:      
logo:     

import: https://github.com/liascript/CodeRunner

import: https://raw.githubusercontent.com/TUBAF-IfI-LiaScript/VL_Softwareentwicklung/master/config.md
        https://raw.githubusercontent.com/liascript-templates/plantUML/master/README.md

-->

[![LiaScript](https://raw.githubusercontent.com/LiaScript/LiaScript/master/badges/course.svg)](https://liascript.github.io/course/?https://github.com/TUBAF-IfI-LiaScript/VL_Softwareentwicklung/blob/master/29_Anwendungen.md)

# Anwendungsbeispiele

| Parameter                | Kursinformationen                                                                           |
|--------------------------|---------------------------------------------------------------------------------------------|
| **Veranstaltung:**       | `Vorlesung Softwareentwicklung`                                                             |
| **Teil:**                | `27/27`                                                                                     |
| **Semester**             | @config.semester                                                                            |
| **Hochschule:**          | @config.university                                                                          |
| **Inhalte:**             | @comment                                                                                    |
| **Link auf den GitHub:** | https://github.com/TUBAF-IfI-LiaScript/VL_Softwareentwicklung/blob/master/27_Anwendungen.md |
| **Autoren**              | @author                                                                                     |

![](https://media.giphy.com/media/26tn33aiTi1jkl6H6/source.gif)

---------------------------------------------------------------------

## Nachfrage: Secrets

Wie gehen wir mit Schlüsseln, Passwörtern usw. in unseren Codes um?

Zielstellung:
+ Komfortable Handhabung im Projekt
+ Projektübergreifende Verwendung (?)
+ Speicherung ohne Weiterleitung an Repositories

Ein Lösungsansatz ist die Verwendung von [Microsoft.Extensions.Configuration.UserSecrets](https://www.nuget.org/packages/Microsoft.Extensions.Configuration.UserSecrets)

```
dotnet new console -o secret_example
dotnet add package Microsoft.Extensions.Configuration.UserSecrets
dotnet user-secrets init
dotnet user-secrets set "ServiceAPIKey" "1213234435"
```

Das war es schon. Nun finden Sie unter

+ `~/.microsoft/usersecrets/<user_secrets_id>/secrets.json` (Linux/macOS)
+ `%APPDATA%\Microsoft\UserSecrets\<user_secrets_id>\secrets.json` (Windows)

den Eintrag

```json
{
  "ServiceAPIKey": "1213234435"
}
```

Aus dem Programm heraus können Sie darauf unmittelbar zurückgreifen.

```csharp
using Microsoft.Extensions.Configuration;

var config = new ConfigurationBuilder().AddUserSecrets<Program>().Build();
string APIsecret = config["ServiceAPIKey"];

Console.WriteLine(APIsecret);
```

## Anwendungsbeispiel

Lassen Sie die Inhalte der Lehrveranstaltung anhand eines Codereviews Revue passieren lassen.

> Sie erhalten ein C# Programm und sollen es überarbeiten - welche Mängel finden Sie?

```csharp
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
```

[SOLID](https://liascript.github.io/course/?https://raw.githubusercontent.com/TUBAF-IfI-LiaScript/VL_Softwareentwicklung/master/15_UML_Modellierung.md#4)

## Erster Entwurf mit LLM

```text @plantUML.png
@startuml
skinparam classAttributeIconSize 0

class Program

class WeatherAggregator {
  -geoService: IGeoLocationService
  -weatherProvider: IWeatherProvider
  -sinks: List<IDataSink>
  -processors: List<IWeatherDataProcessor>
  +AddSink(sink: IDataSink)
  +AddProcessor(proc: IWeatherDataProcessor)
  +FetchAndDistribute(city: string)
  +event WeatherDataReceived
}

interface IGeoLocationService {
  +GetCoordinates(city: string): (double lat, double lon)
}

class OpenWeatherMapGeoService {
  -apiKey: string
  +GetCoordinates(city: string)
}

interface IWeatherProvider {
  +GetWeather(lat: double, lon: double): WeatherData
}

class OpenWeatherMapWeatherProvider {
  -apiKey: string
  +GetWeather(lat: double, lon: double)
}

interface IWeatherDataProcessor {
  +Process(data: WeatherData): WeatherData
}

class WindWarningProcessor {
  +Process(data: WeatherData): WeatherData
}

interface IDataSink {
  +OnWeatherDataReceived(data: WeatherData): void
}

class CsvSink {
  -filePath: string
  +OnWeatherDataReceived(data: WeatherData)
}

class ThingSpeakSink {
  -apiKey: string
  +OnWeatherDataReceived(data: WeatherData)
}

class ConsoleSink {
  +OnWeatherDataReceived(data: WeatherData)
}

class WeatherData {
  +City: string
  +Description: string
  +TemperatureC: double
  +WindSpeed: double
  +Warning?: string
}

Program --> WeatherAggregator

WeatherAggregator --> IGeoLocationService
WeatherAggregator --> IWeatherProvider
WeatherAggregator --> IDataSink
WeatherAggregator --> IWeatherDataProcessor

OpenWeatherMapGeoService ..|> IGeoLocationService
OpenWeatherMapWeatherProvider ..|> IWeatherProvider

WindWarningProcessor ..|> IWeatherDataProcessor

CsvSink ..|> IDataSink
ThingSpeakSink ..|> IDataSink
ConsoleSink ..|> IDataSink

WeatherAggregator --> WeatherData : emits event
@enduml
```



## Resumee


<!--data-type="none"-->
| Woche | Tag       | SWE                                      |
| :---- | --------- | :--------------------------------------- |
| 1     | 4. April  | Organisation, Einführung                 |
| 2     | 7. April  | Softwareentwicklung als Prozess          |
|       | 11. April | Konzepte von Dotnet und C#               |
| 3     | 14. April | Elemente der Sprache C# I                |
|       | 18. April | _Karfreitag_                             |
| 4     | 21. April | _Ostermontag_                            |
|       | 25. April | Elemente der Sprache C# II               |
| 5     | 28. April | Strukturen / Konzepte der OOP            |
|       | 2. Mai    | Säulen Objektorientierter Programmierung |
| 6     | 5. Mai    | Klassenelemente in C#  / Vererbung       |
|       | 9. Mai    | Klassenelemente in C#  / Interfaces      |
| 7     | 12. Mai   | Versionsmanagement im SWE-Prozess I      |
|       | 16. Mai   | Versionsmanagement im SWE_Pprozess II    |
| 8     | 19. Mai   | Generics                                 |
|       | 23. Mai   | Container                                |
| 9     | 26. Mai   | UML Konzepte                             |
|       | 30. Mai   | UML Diagrammtypen                        |
| 10    | 2. Juni   | UML Anwendungsbeispiel                   |
|       | 6. Juni   | Testen                                   |
| 11    | 9. Juni   | _Pfingstmontag_                          |
|       | 13. Juni  | Dokumentation und Build Toolchains       |
| 12    | 16. Juni  | Continuous Integration in GitHub         |
|       | 20. Juni  | Delegaten                                |
| 13    | 23. Juni  | Events                                   |
|       | 27. Juni  | Threadkonzepte in C#                     |
| 14    | 30. Juni  | Taskmodell                               |
|       | 4. Juli   | Design Pattern                           |
| 15    | 7. Juli   | Language Integrated Query                |
|       | 11. Juli  | GUI - MAUI (leider nicht geschafft)      |


## Aus die Maus

> __Danke für Ihr Interesse! Viel Erfolg bei den Prüfungen__
