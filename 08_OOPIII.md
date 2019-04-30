<!--

author:   Sebastian Zug & André Dietrich
email:    zug@ovgu.de   & andre.dietrich@ovgu.de
version:  0.0.1
language: de
narrator: Deutsch Female

import: https://raw.githubusercontent.com/liaScript/rextester_template/master/README.md

-->

# Vorlesung Softwareentwicklung - 8 - Vererbung

--------------------------------------------------------------------
Link auf die aktuelle Vorlesung im Versionsmanagementsystem GitHub

https://github.com/liaScript/CsharpCourse/blob/master/08_OOPIII.md

Die interaktive Form ist unter diese Link zu finden ->
[LiaScript Vorlesung 8](https://liascript.github.io/course/?https://raw.githubusercontent.com/liaScript/CsharpCourse/master/08_OOPIII.md#1)

---------------------------------------------------------------------

**Wie weit sind wir schon gekommen?**

c# Schlüsselwörter:

| abstract    | as       | base     |`bool`      |`break`     |`byte`     |
|`case`       |`catch`   |`char`    |`checked`   |`class`     |`const`    |
|`continue`   |`decimal` | default  | delegate   |`do`        |`double`   |
|`else`       |`enum`    | event    | explicit   | extern     |`false`    |
|`finally`    | fixed    |`float`   |`for`       |`foreach`   |`goto`     |
|`if`         | implicit | in       |`int`       | interface  |`internal` |
| is          | lock     |`long`    |`namespace` |`new`       |`null`     |
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

*1.  Hier stehen jetzt Ihre Fragen ...*

---------------------------------------------------------------------

## 1. Vererbung in C#

                                      {{0-1}}
*****************************************************************************

> Vererbung bildet neben Kapselung und Polymorphie die zentrale Säule des
> objektorientierten Programmierens. Die Vererbung ermöglicht die Erstellung
> neuer Klassen, die ein in exisitierenden Klassen definiertes Verhalten
> wieder verwenden, erweitern und ändern. [MS.NET Programmierhandbuch]

Die Klasse, deren Member vererbt werden, wird Basisklasse genannt, die erbende
Klasse als abgeleitete Klasse bezeichnet.

```csharp    Vererbung
using System;
using System.Reflection;
using System.ComponentModel.Design;

namespace Rextester
{
  public class Person {
    public int geburtsjahr;
    public string name;
  }

  public class Fußballspieler : Person {
    public byte rückennumemr;
  }

  public class Schiedsrichter : Person {
    public bool assistent = true;
  }

  public class Program
  {
    public static void Main(string[] args){
      Person Mensch = new Person {geburtsjahr = 1956, name = "Löw"};
      Console.WriteLine("{0,4} - {1}", Mensch.geburtsjahr, Mensch.name );

      Console.WriteLine("Felder in der Instanz '{0}' von '{1}'", Mensch.name, Mensch);
      var fields = Mensch.GetType().GetFields();
      foreach (FieldInfo field in fields){
         Console.WriteLine(" x " + field.Name);
      }
    }
  }
}
```
@Rextester.eval(@CSharp)

> *Merke*: Im Unterschied zu Klassen ist für Structs unter C# keine Vererbung möglich!

In C# kann jede Klassendefinition nur eine Basisklasse referenzieren. Im Sinne
einer realitätsnahen Modellierung wären Mehrfachvererbungen aber
durchaus zielführend. Ein Amphibienfahrzeug leitet sich aus den Basisklassen
Wasserfahrzeug und Landfahrzeug ab, ein Touchpad integriert die Member von
Eingabegerät und Ausgabegerät. C# verzichtet  drauf um Mehrdeutigkeiten und Fehler
ausschließen zu können, die aus gleichnamige Membern hervorgehen.

*****************************************************************************
                                  {{1-2}}
*****************************************************************************

Konstruktoren werden nicht vererbt, jedoch wird

+ kann mit dem Schlüsselwort `base` auf die Konstruktoren der Basisklasse zurückgegriffen werden.
+ wird sofern aus der abgeleiteten Klasse kein expliziter Aufruf erfolgt, der
Standardkonstruktor der Basisklasse aufgerufen.

Ein Beispiel für den impliziten Aufruf des Standardkonstruktors:

<!-- --{{0}}-- Idee des Codefragments:
  * Fügen Sie einen leeren Standardkonstruktor mit einer Ausgabe in Fußballspieler ein
    public Fußballspieler(){
       Console.WriteLine("ctor of Fußballspieler");
     }
  * nutzen Sie nun base um den zweiten in Person exisitierenden Konstruktor zu
    adressieren.
       public Fußballspieler() : base(1)
-->
```csharp    ImplicitConstructorCall
using System;
using System.Reflection;
using System.ComponentModel.Design;

namespace Rextester
{
  public class Person {
    public int geburtsjahr;
    public string name;

    public Person(){
      geburtsjahr = 1984;
      name = "Orwell";
      Console.WriteLine("ctor of Person");
    }

    public Person(int auswahl){
      if (auswahl == 1) {name = "Micky Maus";}
      else {name = "Donald Duck";}
    }
  }

  public class Fußballspieler : Person {
    public byte rückennummer;
  }

  public class Program
  {
    public static void Main(string[] args){
      Fußballspieler champ = new Fußballspieler();
      Console.WriteLine("{0,4} - {1}", champ.geburtsjahr, champ.name );

    }
  }
}
```
@Rextester.eval(@CSharp)

*****************************************************************************

## 2. Kompatiblität zwischen Typen

Boxing and Unboxing System.Object


Martin Foliensatz 8


| Zuweisung                 | statischer Typ von Zug | dynamischer Typ von Zug |
| ------------------------- | ---------------------- | ----------------------- |
| `Zug zug = new Zug()`     | Zug                    | Zug                     |
| `zug = new PersonenZug()` | Zug                    | PersonenZug             |
| `zug = new Ice`           | Zug                    | Ice                     |



### Casts über Klassen

In einer der vorangegangenen Vorlesungen wurde bereits auf Konvertierungen zwischen
unterschiedlichen Datentypen eingegangen. Analoge Muster lassen sich auch
auf Klassen anwenden, allerdings sind hier einige Besonderheiten zu beachten.

+ implizit auf die Basisklasse  (upcast)
+ explizit auf die abgeleitete Klasse (downcast)

gekastet werden. Zunächst ein Beispiel für einen *upcast* anhand unseres
Fußballbeispiels. Zugriffe auf Member, die  in der Basisklasse nicht enthalten
sind führen logischerweise zum Fehler.

```csharp    Upcast
using System;
using System.Reflection;
using System.ComponentModel.Design;

namespace Rextester
{
  public class Person {
    public int geburtsjahr;
    public string name;
  }

  public class Fußballspieler : Person {
    public byte rückennummer;
  }

  public class Program
  {
    public static void Main(string[] args)
    {
      Fußballspieler champ = new Fußballspieler {geburtsjahr = 1956,
                                                 name = "Maier",
                                                 rückennummer = 13};

      Console.WriteLine("Felder in der Instanz '{0}' von '{1}'", champ.name, champ);
      var fields = champ.GetType().GetFields();
      foreach (FieldInfo field in fields){
        Console.WriteLine(" x " + field.Name);
      }      

      Person human = champ;     // Castoperation Fußballspieler -> Person
      Console.WriteLine(human.rückennummer);
    }
  }
}
```
@Rextester.eval(@CSharp)

In umgekehrter Richtung vollzieht sich der *Downcast*, eine Instanz der
Basisklasse wird auf einen abgeleiteten Typ gemappt.

```csharp    Downcast
using System;
using System.Reflection;
using System.ComponentModel.Design;

namespace Rextester
{
  public class Person {
    public int geburtsjahr;
    public string name;
  }

  public class Fußballspieler : Person {
    public byte rückennummer;
  }

  public class Program
  {
    public static void Main(string[] args)
    {
      Person Mensch = new Person {geburtsjahr = 1956,
                                  name = "Maier"};

      //Fußballspieler champ = Mensch; // Fehler wird zur Compilezeit erkannt
      Fußballspieler champ = (Fußballspieler) Mensch; // Fehler zur Laufzeit erkannt

    }
  }
}
```
@Rextester.eval(@CSharp)

*Upcast* und *Downcast* ...  wozu brauche ich das den? Nehmen wir an, dass wir
eine Ausgabemethode für beide Typen - Person und Fußballspieler - benötigen.
Ja, es wäre möglich diese als Memberfunktion zu implementieren, problematisch
wäre aber dann, dass wir an unterschiedlichen Stellen im Code spezifische
Befehle für die Ausgabe in der Konsole zu stehen haben. Sollen die Log-Daten
nun plötzlich in eine Datei ausgegeben werden, müsste diese Anpassung überall
vollzogen werden. Entsprechend ist eine externe (statische) Logger-Klasse
wesentlich geeigneter diese Funktionalität zu kapseln. Allerdings wäre dann ein
überladen der entsprechenden Ausgabefunktion mit allen vorkommenden Typen notwendig.
Dies kann durch entsprechende Casts umgangen werden.

```csharp    UpCastExample
using System;
using System.Reflection;
using System.ComponentModel.Design;

namespace Rextester
{
  public class Person
  {
    public int geburtsjahr;
    public string name;
  }

  public class Fußballspieler : Person
  {
    public byte rückennummer;
  }

  public static class Logger
  {
    public static void printPerson(Person person){
        Console.WriteLine("{0} - {1}", person.name, person.geburtsjahr);
        if (person is Fußballspieler)
          Console.WriteLine("{0} - {1}", person.name, (person as Fußballspieler).rückennummer);
    }
  }

  public class Program
  {
    public static void Main(string[] args)
    {
       Person Mensch = new Person {geburtsjahr = 1956,
                                  name = "Maier"};
       Logger.printPerson(Mensch);

       Fußballspieler Champ = new Fußballspieler{geburtsjahr = 1967,
                                                 name = "Müller",
                                                 rückennummer = 13};
       Logger.printPerson(Champ);
    }
  }
}
```
@Rextester.eval(@CSharp)


## 3. Polymorphie in C#



> **Merke** Polymorphie bezeichnet die Tatsache, dass Klassenmember ausgehend
> von Ihrer Nutzung ein unterschiedliches Verhalten erzeugen.


> **Merke** Dynamische Bindung bezeichnet die Tatsache, dass bei Aufruf einer
> überschriebenen Methode über eine Basisklassenreferenz oder ein
> Interface trotzdem die Implementierung der abgeleiteten Klasse zum
> Einsatz kommt.

Abgeleitete Klassen können aus der Basisklasse geerbte Methoden neu deklarieren.
Dabei kann gewählt werden, ob die Methode verdeckt oder überschrieben werden soll.
In beiden Fällen wird die ursprüngliche Methode durch eine neue ersetzt.

Dynamische Bindung erlaubt den Aufruf von überschriebenen Methoden aus der
Basisklasse heraus, wobei das Überschreiben muss in der Basisklasse explizit
erlaubt werden


### Überschreiben von Methoden

### Verdecken von Methoden

### Versiegeln von Klassen oder Membern

### Abstrakte Klassen



## 4. Beispiel der Woche ...

```csharp
using System;
using System.Collections;
using System.Collections.Generic;

namespace Rextester
{
  public class Program
  {
    public static void Main(string[] args){

    }
  }
}
```
@Rextester.eval(@CSharp)




## Anhang

**Referenzen**



**Autoren**

Sebastian Zug, André Dietrich
