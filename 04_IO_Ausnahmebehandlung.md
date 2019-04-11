<!--

author:   Sebastian Zug & André Dietrich
email:    zug@ovgu.de   & andre.dietrich@ovgu.de
version:  0.0.1
language: de
narrator: Deutsch Female

import: https://raw.githubusercontent.com/liaScript/rextester_template/master/README.md

-->

# Vorlesung Softwareentwicklung - 4 - Funktionen und Strukturen

**Fragen an die heutige Veranstaltung ...**

*

---------------------------------------------------------------------
Link auf die aktuelle Vorlesung im Versionsmanagementsystem GitHub

https://github.com/liaScript/CsharpCourse/blob/master/04_FunktionenStrukturen.md

Die interaktive Form ist unter diese Link zu finden ->
[LiaScript Vorlesung 1](https://liascript.github.io/course/?https://raw.githubusercontent.com/liaScript/CsharpCourse/master/04_FunktionenStrukturen.md#1)

---------------------------------------------------------------------

**Wie weit sind wir schon gekommen?**

c# Schlüsselwörter:

| abstract    | as       | base     | bool       |`break`     |`byte`     |  
|`case`       | catch    | char     |`checked`   |`class`     | const     |
|`continue`   |`decimal` | default  | delegate   |`do`        |`double`   |
|`else`       |`enum`    | event    | explicit   | extern     |`false`    |
| finally     | fixed    | float    | for        | foreach    |`goto`     |
|`if`         | implicit | in       |`int`       | interface  | internal  |
| is          | lock     |`long`    |`namespace` | new        | null      |
| object      | operator | out      | override   | params     | private   |
| protected   | public   | readonly | ref        |`return`    |`sbyte`    |
| sealed      |`short`   | sizeof   | stackalloc |`static`    | string    |
| struct      |`switch`  | this     | throw      |`true`      | try       |
| typeof      |`uint`    |`ulong`   |`unchecked` | unsafe     |`ushort`   |
|`using`      | virtual  |`void`    | volatile   |`while`     |           |


Auf die Auführung der kontextabhängigen Schlüsselwörter wie `where` oder
`ascending` wurde hier verzichtet.

---

## Kontrollfragen

*1.

--------------------------------------------------------------------

*2. Hier stehen jetzt Ihre Fragen ...*

---------------------------------------------------------------------

## 1. Schreiboperation

C# selbst besitzt keine Anweisungen für die Ein- und Ausgabe von Daten, dazu
existieren aber mehrere Bibliotheken, die im folgenden für die Bildschirmausgabe
und das Schreiben in Dateien vorgestellt werden sollen.

Für das Schreiben stehen zwei Methoden `System.Console.Write` und `System.Console.ẀriteLine`.

<!-- --{{1}}-- Idee des Codeblockes
  * Erinnerung an Steuerzeichen /n /t
 -->

```csharp   Write/WriteLine
using System;

namespace Rextester
{
  public class Program
  {
    public static void Main(string[] args)
    {
      string s = "Start " + "Zero";
      Console.Write("One");     
      Console.Write("Two");     
      Console.WriteLine("Three");
      Console.WriteLine();  
      Console.WriteLine("Four");
    }
  }
}
```
@Rextester.eval(@CSharp)

Diese decken erstens eine **große Bandbreite von Übergabeparametern** und bedienen
zweitens verschiedene **Ausgabeschnittstellen**.

| WriteLine() Methoden                                    | Anwendung                                 |
| ------------------------------------------------------- | ----------------------------------------- |
| `WriteLine()`                                             | Zeilenumbruch                             |
| `WriteLine(UInt64), WriteLine(Double), WriteLine(Object)` | Ausgabe von Variablen der Basisdatentypen |
| `WriteLine(Object)`                                       |                                           |
| `WriteLine(String)`                                       | Ausgabe einer `string` Variable           |
| `WriteLine(String, Object)`                               | String und Formatinformationen            |
| `WriteLine(String, Object, Object)`                       |                                           |
| `WriteLine(String, Object, Object, Object)`               |                                           |
| `WriteLine(String, Object[])`                             |                                           |

### Nicht-String Parameter

Die Darstellung der Basisdatentypen erfolgt unter der Maßgabe, dass diese vom
der Klasse Objekt erben und damit die Methode `toString()` implementieren.
Diese wird implizit aufgerufen.

<!-- --{{1}}-- Idee des Codeblockes
  * Darstellung ToString()
 -->

```csharp   Write/WriteLine
using System;

namespace Rextester
{
  public class Student{
    string Name = "Mickey Maus";
    int Alter = 35;
  }

  public class Program
  {
    public static void Main(string[] args)
    {
       Student s = new() Student();
       Console.WriteLine(s);
    }
  }
}
```
@Rextester.eval(@CSharp)

Für nutzerspezifische Referenztypen gibt `WriteLine` den Namen der Instanz
zurück. Eigene Implementierungen der `ToString()` Methode erlauben individuelle
Ausgaben.

### Kombinierte Formatierung von Strings   

Die "Kombinierte Formatierung" unter C# ermöglicht eine breite Festlegung bezüglich des Formats der Ausgaben. Die folgenden Aussagen beziehen sich dabei aber nicht
nur auf die Anwendung im Zusammenhang mit `WriteLine()` und `Write()` sondern
können auch auf:

* [String.Format](https://docs.microsoft.com/de-de/dotnet/api/system.string.format) (und damit ToString()!)
* [String.Builder](https://docs.microsoft.com/de-de/dotnet/api/system.text.stringbuilder)
* [Debug.WriteLine](https://docs.microsoft.com/de-de/dotnet/api/system.diagnostics.debug.writeline#System_Diagnostics_Debug_WriteLine_System_String_System_Object___)
...

angewandt werden.

Dabei sind folgende Ausdrücke möglich:

```
"{n}"
"{n, width}"
"{n, width:format}"
"{n, width:format precision}"    //ohne Lehrzeichen zwischen format und precision
```

<!-- --{{1}}-- Idee des Codeblockes
  * Diskussion der Indizes, Durchtauschen der Indizes, Erzeugung Exception
  * Darstellung der Breite
 -->

```csharp   IndizesBreite
using System;

namespace Rextester
{
  public class Program
  {
    public static void Main(string[] args)
    {
       int ivalue = 56;
       double dvalue = 43.2234;
       bool bvalue = true;
       Console.WriteLine("Das ist ein Test");
    }
  }
}
```
@Rextester.eval(@CSharp)

| Format Symbol | Bedeutung                | Beispiel      |
| ------------- | ------------------------ | ------------- |
| G             | Default                  |               |
| E, e          | Expoentiell              | 1.052033E+003 |
| X, x          | Hexadezimal              | 1FF           |
| P, p          | Prozent                  | -38.8         |
| D, d          | Dezimal                  | 1231          |
| N, n          | Dezimal mit Trennzeichen | 1.23432,12    |

Eine komplette Auflistung findet sich unter https://docs.microsoft.com/de-de/dotnet/standard/base-types/standard-numeric-format-strings

> Achtung: Die Formatzeichen sind typspezifisch, es exisiteren analoge Zeichen
> mit unterschiedlicher Bedeutung für Zeitwerte

<!-- --{{1}}-- Idee des Codeblockes
  * Illustration, dass die Formatierungsmethoden auf verschiedenen Ebenen
  funktionen
  * Hinweis auf fehlender Möglichkeit einer Breitenangabe
  * Einführung einer cultural Instanz

 -->
```csharp   IndizesBreite
using System;
using System.Globalization;
using System.Threading;

namespace Rextester
{
  public class Program
  {
    public static void Main(string[] args)
    {
      //Globale Definition des Kulturkreises
      //Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
      DateTime thisDate = new DateTime(2008, 3, 15);
      Console.WriteLine(thisDate.ToString("d"));   // d = kurzes Datum
                                                   // D = langes Datum
                                                   // f = vollständig
      Console.WriteLine("{0:D}", thisDate);
    }
  }
}
```
@Rextester.eval(@CSharp)

In Kombination mit den Breiten und Präzisionsinformationen lasse sich damit sehr
mächtige Dastellungen realisieren.

```csharp   IndizesBreite
using System;

namespace Rextester
{
  public class Program
  {
    public static void Main(string[] args)
    {
       double value = 1233.1232;
       Console.WriteLine(value);
       Console.WriteLine(value.ToString("F:20,5"));

    }
  }
}
```
@Rextester.eval(@CSharp)


### Zeichenfolgeninterpolation

https://docs.microsoft.com/de-de/dotnet/csharp/language-reference/tokens/interpolated


### Stream Konzept




### Beispiel

In Markdown sind Tabellen nach folgendem Muster aufgebaut:

```
| Column I | Column II | Column III      |
|:---------|:----------|:----------------|
| Peter    | 42        | C-Programmierer |
| Astrid   | 23        | Level Designer  |
```

Geben Sie die Daten bestimmte Fußballvereine in einer Markdown-Tabelle aus.

```csharp   IndizesBreite
using System;

namespace Rextester
{
  public class Program
  {
    public static void Main(string[] args)
    {
       string [] clubs = {"Rot Weiß", "Blau Gelb", "Eintracht", "Kickers"};
       int maxlength = 0;
       foreach(string club in clubs)  maxlength =  club.Length < maxlength ? maxlength : club.Length ;

       //Console.WriteLine("".PadLeft(maxlength + 10, '-'));
       Console.Write("| ");
       Console.Write("Name".ToString("G:10"));
       //
       //Console.WriteLine(maxlength);

    }
  }
}
```
@Rextester.eval(@CSharp)


## 2. Leseoperationen



## 4. Ausnahmebehandlungen

Die C#-Funktionen zur Ausnahmebehandlung unterstützen bei der Handhabung von
unerwarteten oder außergewöhnlichen Situationen, die beim Ausführen von Programmen auftreten.

```csharp   IndizesBreite
using System;
using System.Globalization;

namespace Rextester
{
  public class Program
  {
    public static void Main(string[] args)
    {
        // Beispiel 1: Zugriff auf das Filesystem eines Rechners aus dem Netz
        System.IO.FileStream file = null;
        //System.IO.FileInfo fileInfo = new System.IO.FileInfo(@"NoPermission.txt");

        // Beispiel 2: Division durch Null
        int a = 0, b = 5;
        //a = b / a;

        //Beispiel 3:
        string s = "5";
        Console.WriteLine(int.Parse(s, NumberStyles.Float);
    }
  }
}
```
@Rextester.eval(@CSharp)

Dabei gelten folgende Regeln für den Umgange mit Exceptions:

+ Wenn für eine spezifische Ausnahme kein Ausnahmehandler existiert, beendet sich das Programm mit einer Fehlermeldung.
+ Alle Ausnahmen sind von `System.Exception` abgeleitet und enthalten detaillierte Informationen über den Fehler, z.B. den Zustand der Aufrufliste und eine Textbeschreibung des Fehlers.
+ Ausnahmen, die innerhalb eines `try`-Blocks auftreten, werden auf einen Ausnahme
handler, der mit dem Schlüsselwort `catch` gekennzeichnet ist, umgeleitet.
+ Ausnahmen werden durch die CLR ausgelöst oder in Software mit dem `throw`
Befehl.
+ ein `finally`-Block wird im Anschluss an die Aktivierung eines `catch`
Blockes ausgeführt, wenn eine Ausnahme ausgelöst wurde. Hier werden Ressourcen freizugeben, beispielsweise ein Stream geschlossen.

try catch finally


### Beispiel Exception-Handling

Schreiben Sie die Einträge eines Arrays in eine Datei!

Lösung unter  ....

| Schritt 1: Welche Fehler können auftreten? Welche Fehler werden durch die Implementierung abgefangen? |
| Schritt 2: Wo sollen die Fehler abgefangen werden?                                                    |
| Schritt 3: Gibt es Prioritäten bei der Abarbeitung?                                                   |
| Schritt 4: Sind abschließende "Arbeiten" notwendig?                                                   |

[Link auf die Dokumentation der StreamWriter Klasse](
https://docs.microsoft.com/de-de/dotnet/api/system.io.streamwriter.-ctor?view=netframework-4.7.2#System_IO_StreamWriter__ctor_System_String_)


## 5. Beispiel der Woche ...

Entwickeln Sie ein Programm, dass als Kommandozeilen-Parameter eine Funktionsnamen
und eine Ganzzahl übernimmt und die entsprechende Ausführung realisiert. Als
Funktionen sollen dabei `Square` und `Reciprocal` dienen. Der Aufruf erfolgt
also mit

```bash
mono Calculator Square 7    
mono Calculator Reciprocal 9
```
Welche Varianten der Eingaben müssen Sie prüfen?

Eine mögliche Lösung finden Sie unter ... [Link]()

```csharp    Calculator.cs
using System;
namespace Calcualator
{
  class MainClass
  {
    static double Square(int num) => num * num;
    static double Reciprocal (int num) => 1f / num;

    static void Main(string[] args)
    {
      bool Error = false;
      double result = 0;
      int num = 1;
      if (args.Length == 2)
      {
        // Hier geht es weiter, welche Fälle müssen Sie bedenken?
        // int.TryParse(args[1], out num) erlaubt ein fehlertolerantes Parsen
        // eines strings
      }
      else Error = true;

      if (Error)
      {
        Console.WriteLine("Please enter a function and a numeric argument.");
        Console.WriteLine("Usage: Square    <int> or\n       Reciprocal <int>");
      }
      else
      {
        Console.WriteLine("{0} Operation on {1} generates {2}", args[0], num, result );
      }
    }
  }
}
```csharp   




## Anhang

**Referenzen**

[MSDoku] C# Dokumentation, "Pattern Matching",  [Link](https://docs.microsoft.com/en-us/dotnet/csharp/pattern-matching)

[WikiMonteCarlo]  ZUM-Wiki, "Monte Carlo Simulation" Autor "Springob", [Link](https://de.wikipedia.org/wiki/Monte-Carlo-Simulation#/media/File:Pi_statistisch.png)

**Autoren**

Sebastian Zug, André Dietrich
