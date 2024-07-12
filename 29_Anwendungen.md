<!--

author:   Sebastian Zug, Galina Rudolf & André Dietrich
email:    sebastian.zug@informatik.tu-freiberg.de
version:  1.0.3
language: de
narrator: Deutsch Female
comment:  Zusammenfassung und Ausblick
tags:      
logo:     

import: https://github.com/liascript/CodeRunner

import: https://raw.githubusercontent.com/TUBAF-IfI-LiaScript/VL_Softwareentwicklung/master/config.md

-->

[![LiaScript](https://raw.githubusercontent.com/LiaScript/LiaScript/master/badges/course.svg)](https://liascript.github.io/course/?https://github.com/TUBAF-IfI-LiaScript/VL_Softwareentwicklung/blob/master/27_Anwendungen.md)

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

## Bits&Bytes

https://liascript.github.io/course/?https://raw.githubusercontent.com/SebastianZug/RoboLabVortraege/main/19_Fussball/README.md#1

![](https://raw.githubusercontent.com/SebastianZug/RoboLabVortraege/main/19_Fussball/image/football_results.png)

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

https://cobwebsonmymind.wordpress.com/2011/04/13/thingspeak-net-class/

Wir fokussieren uns auf zwei Methoden für das grundsätzliche Schreiben eines Wertes auf den Server.

> __Aufgabe:__ Bewerten Sie den Code im Hinblick auf:
>
> - Verwendbarkeit des Beispiels
> - Entwurfsqualität
> - Implementierung

```csharp
using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Text;
using System.Net;
using System.IO;

namespace ThingSpeak
{
    public class ThingSpeak
    {
        private const string _url = "http://api.thingspeak.com/";
        private const string _APIKey = "YOUR_KEY_HERE";

        public static Boolean SendDataToThingSpeak(string field1, string field2, string field3, string field4, string field5, string field6, string field7, string field8, out Int16 TSResponse)
        {
            StringBuilder sbQS = new StringBuilder();

            // Build the querystring
            sbQS.Append(_url + "update?key=" + _APIKey);
            if (field1 != null) sbQS.Append("&field1=" + HttpUtility.UrlEncode(field1));
            if (field2 != null) sbQS.Append("&field2=" + HttpUtility.UrlEncode(field2));
            if (field3 != null) sbQS.Append("&field3=" + HttpUtility.UrlEncode(field3));
            if (field4 != null) sbQS.Append("&field4=" + HttpUtility.UrlEncode(field4));
            if (field5 != null) sbQS.Append("&field5=" + HttpUtility.UrlEncode(field5));
            if (field6 != null) sbQS.Append("&field6=" + HttpUtility.UrlEncode(field6));
            if (field7 != null) sbQS.Append("&field7=" + HttpUtility.UrlEncode(field7));
            if (field8 != null) sbQS.Append("&field8=" + HttpUtility.UrlEncode(field8));
            // The response will be a "0" if there is an error or the entry_id if > 0
            TSResponse = Convert.ToInt16(PostToThingSpeak(sbQS.ToString()));
            if (TSResponse > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        private static string PostToThingSpeak(string QueryString)
        {
            StringBuilder sbResponse = new StringBuilder();
            byte[] buf = new byte[8192];

            // Hit the URL with the querystring and put the response in webResponse
            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(QueryString);
            HttpWebResponse webResponse = (HttpWebResponse)myRequest.GetResponse();
            try
            {
                Stream myResponse = webResponse.GetResponseStream();
                int count = 0;
                // Read the response buffer and return
                do
                {
                    count = myResponse.Read(buf, 0, buf.Length);
                    if (count != 0)
                    {
                        sbResponse.Append(Encoding.ASCII.GetString(buf, 0, count));
                    }
                }
                while (count > 0);
                return sbResponse.ToString();
            }
            catch (WebException ex)
            {
                return "0";
            }

        }
    }
}
```

Ablauf eines Schreibprozesses:

1. Initiierung:

   + der Nutzer spezifiziert die Kanalkonfiguration und den Kanalnamen

2. Laufzeit:

   + Schreiben der Werte
   + Versenden
   + Evaluation des Erfolgs und "Markierung" der bereits versandten Daten

## Resumee

<!--data-type="none"-->
| Woche | Tag       | Inhalt der Vorlesung                              |
| :---- | --------- | :------------------------------------------------ |
| 1     | 5. April  | Organisation, Einführung von GitHub und LiaScript |
| 2     | 8. April  | Softwareentwicklung als Prozess                   |
|       | 12. April | Konzepte von Dotnet und C#                        |
| 3     | 15. April | Elemente der Sprache C# I                         |
|       | 19. April | Elemente der Sprache C# II                        |
| 4     | 22. April | Strukturen / Konzepte der OOP                     |
|       | 26. April | Säulen Objektorientierter Programmierung          |
| 5     | 29. April | Klassenelemente in C#  / Vererbung                |
|       | 3. Mai    | Klassenelemente in C#  / Interfaces               |
| 6     | 6. Mai    | Generics                                          |
|       | 10. Mai   | Container                                         |
| 7     | 13. Mai   | Versionsmanagement im SWE-Prozess I               |
|       | 17. Mai   | Versionsmanagement im SWE_Pprozess II             |
| 8     | 20. Mai   | _Pfingstmontag_                                   |
|       | 24. Mai   | UML Konzepte                                      |
| 9     | 27. Mai   | UML Diagrammtypen                                 |
|       | 31. Mai   | UML Anwendungsbeispiel                            |
| 10    | 3. Juni   | Testen                                            |
|       | 7. Juni   | Dokumentation und Build Toolchains                |
| 11    | 10. Juni  | Continuous Integration in GitHub                  |
|       | 14. Juni  | Delegaten                                         |
| 12    | 17. Juni  | Events                                            |
|       | 21. Juni  | Threadkonzepte in C#                              |
| 13    | 24. Juni  | Taskmodell                                        |
|       | 28. Juni  | Design Pattern                                    |
| 14    | 1. Juli   | Language Integrated Query                         |
|       | 5. Juli   | GUI - MAUI                                        |
| 15    | 8. Juli   | GUI - MAUI                                        |
|       | 12. Juli  | Anwendungungsfälle                                |


## Aus die Maus

> __Danke für Ihr Interesse! Viel Erfolg bei den Prüfungen__
