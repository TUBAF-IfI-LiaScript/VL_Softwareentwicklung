<!--

author:   Sebastian Zug & André Dietrich
email:    zug@ovgu.de   & andre.dietrich@ovgu.de
version:  0.0.1
language: de
narrator: Deutsch Female

import: https://raw.githubusercontent.com/liaScript/rextester_template/master/README.md

-->

# Vorlesung Softwareentwicklung - 5 - Funktionen und Strukturen

--------------------------------------------------------------------
Link auf die aktuelle Vorlesung im Versionsmanagementsystem GitHub

https://github.com/liaScript/CsharpCourse/blob/master/06_KlassenI.md

Die interaktive Form ist unter diese Link zu finden ->
[LiaScript Vorlesung 5](https://liascript.github.io/course/?https://raw.githubusercontent.com/liaScript/CsharpCourse/master/06_KlassenI.md#1)

---------------------------------------------------------------------

**Wie weit sind wir schon gekommen?**

c# Schlüsselwörter:

| abstract    | as       | base     |`bool`      |`break`     |`byte`     |  
|`case`       |`catch`   | char     |`checked`   |`class`     | const     |
|`continue`   |`decimal` | default  | delegate   |`do`        |`double`   |
|`else`       |`enum`    | event    | explicit   | extern     |`false`    |
|`finally`    | fixed    |`float`   |`for`       |`foreach`   |`goto`     |
|`if`         | implicit | in       |`int`       | interface  |`internal` |
| is          | lock     |`long`    |`namespace` |`new`       | null      |
| object      | operator |`out`     | override   |`params`    |`private`  |
| protected   |`public`  | readonly |`ref`       |`return`    |`sbyte`    |
| sealed      |`short`   | sizeof   | stackalloc |`static`    |`string`   |
|`struct`     |`switch`  |`this`    |`throw`     |`true`      |`try`      |
| typeof      |`uint`    |`ulong`   |`unchecked` | unsafe     |`ushort`   |
|`using`      | virtual  |`void`    | volatile   |`while`     |           |


Auf die Auführung der kontextabhängigen Schlüsselwörter wie `where` oder
`ascending` wurde hier verzichtet.

---

## Kontrollfragen

*1. Welche Sichtbarkeitsattribute können struct-Memberfunktionen in ihrer Sichtbarkeit spezifizieren und was bedeuten sie?*

--------------------------------------------------------------------

*2. Hier stehen jetzt Ihre Fragen ...*

---------------------------------------------------------------------

## Einschub - Nullable

                                 {{0-1}}
*******************************************************************************

... bevor es mit den Klassen weitergeht, noch eine Ergänzung zu den Variablen. Ein "leer-lassen" ist nur für Referenzdatentypen möglich, Wertedatentypen können nicht uninitialisiert bleiben (Compilerfehler)


<!-- --{{0}}-- Idee des Codefragments:
    * Der Ausgangszustand generiert einen Fehler
    * Initalisierung mit string text = null
    * Evaluation von int i = null;
-->
```csharp                                      Iniitalisation
using System;

namespace Rextester
{
  public class Program
  {
    public static void Main(string[] args){
      string text;
      // int i = null;

      try{
        Console.WriteLine("Der Inhalt von text ist ->{0}<-", text);
      }
      catch (Exception e)
      {
        throw new Exception(e.ToString());
      }
    }
  }
}
```
@Rextester.eval(@CSharp)

Aus der Definition heraus kann zum beispiel eine `int` Variable nur einen Wert zwischen int.MinValue und int.MaxValue annehmen. Eine `null` ist nicht vorgesehen und eine `0` gehört zum "normalen" Wertebereich.

*******************************************************************************

                                 {{1-2}}
*******************************************************************************
Um gleichermaßen "nicht-besetzte" Werte-Variablen zu ermöglichen integriert C#
das Konzept der sogenannte null-fähige Typen (*nullable types*) ein. Dazu wird
dem Typnamen ein  Fragezeichen angehängt. Damit ist es möglich diesen auch den
Wert `null` zuzuweisen bzw. der Compiler realisiert dies.

<!-- --{{0}}-- Idee des Codefragments:
    * einfache Variable ist mit null initialisierbar
    * Standardkonstruktor realisiert korrekte null Initialisierung
-->
```csharp                                      Iniitalisation
using System;

namespace Rextester
{
  //public struct Person{
  //  string name;
  //  int? alter;
  //}

  public class Program
  {
    public static void Main(string[] args){
      int? i = null;
      if (i == null) Console.WriteLine("Die Variable hat keinen Wert!")
      else Console.WriteLine("Der Wert der Variablen ist {0}", i)
    }
  }
}
```
@Rextester.eval(@CSharp)

Jeder Typ? wird vom Compiler dazu in einen generischen Typ `Nullable<Typ>`
transformiert, der folgende Methoden implementiert:

```csharp
public struct Nullable <T>{
  private bool defined;
  public bool HasValue {get;}
  ...
  private T value;
  public T Value {get;}
  ...
  public T GetValueOrDefault()   // value oder default Value entsprechend der
                                 // der Liste unter dem untenstehenden Link
  ...
}

```

https://docs.microsoft.com/de-de/dotnet/csharp/language-reference/keywords/default-values-table

*******************************************************************************

                                 {{2-3}}
*******************************************************************************
Jetzt mal konkret, wozu brauche ich das? Nehmen wir an,
* dass Sie Messwerte erfassen und diese in eine Datei schreiben, Ihr Sensor generiert Zeitweise fehlerhafte Werte. Sie können dies Situation mit einzelnen Flags in
einem struct ausweisen oder aber den Wert direkt als ungültig kennzeichnen.
*

Bei der weiteren Verwendung generieren `null`-Werte bei der Berechnung keine
Fehler sondern produzieren wiederum einen `null`-Wert. Entsprechend werden
Ergebnisse wiederum auf einen `Type?` abgebildet. Der ungesetzte Zustand pflanzt
sich also fort.

<!-- --{{0}}-- Idee des Codefragments:
    * einfache Variable ist mit null initialisierbar
    * Standardkonstruktor realisiert korrekte null Initialisierung
-->
```csharp                                      Iniitalisation
using System;

namespace Rextester
{
  public class Program
  {
    public static void Main(string[] args){
      int? i = null;
      if (i == null) Console.WriteLine("Die Variable hat keinen Wert!")
      else Console.WriteLine("Der Wert der Variablen ist {0}", i)
    }
  }
}
```
@Rextester.eval(@CSharp)

*******************************************************************************

## Vererbung
