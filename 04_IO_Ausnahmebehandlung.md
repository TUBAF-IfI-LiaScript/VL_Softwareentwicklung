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

## 1. Input Operationen

C# selbst besitzt keine Anweisungen für die Ein- und Ausgabe von Daten, dazu
existieren aber mehrere Bibliotheken.


## 2. Output Operationen


## 3 Ausnahmebehandlungen


## 4. Beispiel der Woche ...

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
