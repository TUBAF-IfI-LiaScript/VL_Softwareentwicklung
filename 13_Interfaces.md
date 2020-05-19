<!--

author:   Sebastian Zug & André Dietrich
email:    Sebastian.Zug@informatik.tu-freiberg.de & andre.dietrich@informatik.tu-freiberg.de
version:  0.0.1
language: de
narrator: Deutsch Female

import: https://raw.githubusercontent.com/liaScript/rextester_template/master/README.md

-->

# Softwareentwicklung - 12 - OOP Implementierung in Csharp

**TU Bergakademie Freiberg - Sommersemester 2020**

Link auf die aktuelle Vorlesung im Versionsmanagementsystem GitHub

[https://github.com/SebastianZug/CsharpCourse/blob/SoSe2020/13_Interfaces.md](https://github.com/SebastianZug/CsharpCourse/blob/SoSe2020/13_Interfaces.md)

Die interaktive Form ist unter diese Link zu finden ->
[LiaScript Vorlesung 13](https://liascript.github.io/course/?https://raw.githubusercontent.com/SebastianZug/CsharpCourse/SoSe2020/13_Interfaces.md#1)

---------------------------------------------------------------------

## 7 Fragen in 7 Minuten

**1. Jetzt sind Sie dran ...**

**2. Jetzt sind Sie dran ...**

**3. Jetzt sind Sie dran ...**

**4. Jetzt sind Sie dran ...**

**5. Jetzt sind Sie dran ...**

**6. Jetzt sind Sie dran ...**

**7. Jetzt sind Sie dran ...**


## Interfaces

                                         {{0-1}}
*******************************************************************************

**Wiederholung -Abstrakte Basisklasse**

+ abstrakte Basisklassen können die grundsätzliche Struktur für abgeleitete Klassen vorgeben
+ es können keine Objekte von abstrakten Basisklassen erstellt werden
+ abstrakte Basisklassen zählen als „nicht vollständig“
+ abstrakte Methoden müssen in abgeleiteten Klassen implementiert werden

```csharp    AbstrakteKlasse
abstract class Shape
{
  protected double area;
  protected double scope;
  abstract public double Area();
  abstract public double Scope();
  protected virtual void plot(){
     Console.WriteLine("Hier wird auf die GUI zugegriffen und ein Blob gemalt");
  }
}

class Rectangular:Shape
{
  public override double Area() => area;
  public override double Scope() => scope;
  public Rectangular(double sideA, double sideB )
  {
      area = sideA * sideB;
      scope = 2 * sideA + 2 * sideB;
  }
  public override plot(){
     Console.WriteLine("Hier wird auf die GUI zugegriffen und ein Rechteck gemalt");
  }
}

class App
{
  static void Main(string[] args)
  {
    Rectangular rect = new Rectangular(2, 3);
    Console.WriteLine($"Area: {rect.Area()}, " + $"Scope: {rect.Scope()}");
  }
}
```
Neben Feldern und Methoden können auch Properties und Indexer als abstract deklariert werden.

*******************************************************************************

                                    {{1-2}}
*******************************************************************************

**Interfaces**

Interfaces setzen die Idee der abstrakten Klassen konsequent fort und umfassen nur
abstrakte Member. Sie bilden die Signatur einer Klasse, in der Methoden, Properties,
Indexer und Events erfasst werden.

> Merke: Interfaces umfassen keine Felder!

Charakteristik von Interfaces:

+ alle Bestandteile aus einem Interface müssen implementiert werden
+ Klassen „implementieren“ Interfaces und „erben“ von Basisklassen
+ Interfaces haben das Schlüsselwort `interface` und fangen im allgemeinen mit dem Buchstaben I an
+ alle Elemente sind implizit `abstract` und `public`

```csharp    UpCastExample
using System;
using System.Reflection;
using System.ComponentModel.Design;

namespace Rextester
{
  interface IShape
  {
    double Area();
    double Scope();
  }

  class Rectangular : IShape   // Rectangular implementiert das Interface IShape
  {
    double area;
    double scope;
    //public double Area() => area;
    public double Area() {return area;}
    //public double Scope() => scope;
    public double Scope() {return scope;}
    public Rectangular(double sideA, double sideB)
    {
      area = sideA * sideB;
      scope = 2 * sideA + 2 * sideB;
    }
  }

  public class Program
  {
    public static void Main(string[] args)
    {
      Rectangular rect = new Rectangular(2, 3);
      Console.WriteLine("Area: {0}, " + "Scope: {1}", rect.Area(), rect.Scope());
    }
  }
}
```
@Rextester.eval(@CSharp)

Eine Klasse kann nur von einer anderen Klasse erben, aber beliebig viele Interfaces implementieren.
*******************************************************************************

                                    {{2-3}}
*******************************************************************************
**Anwendung**

```csharp    UpCastExample
using System;
using System.Reflection;
using System.ComponentModel.Design;

namespace Rextester
{
  interface IBaseInterface { void M(); }
  interface IDerivedInterface : IBaseInterface { void N(); }

  class A : IBaseInterface
  {
    public void M()
    {
      Console.WriteLine("Methode M in {0}", this.GetType().Name);
    }
  }

  class B : IDerivedInterface
  {
    public void M()
    {
      Console.WriteLine("Methode M in {0}", this.GetType().Name);
    }
    public void N()
    {
      Console.WriteLine("Methode N in {0}", this.GetType().Name);
    }
  }


  public class Program
  {
    public static void Main(string[] args)
    {
      IBaseInterface t1 = new A();    // Statischer Typ IBaseInterface, dynamischer class A
      IBaseInterface t2 = new B();    // Statischer Typ IBaseInterface, dynamischer class A
      t1.M();
      t2.M();
      Console.WriteLine(t2 is IDerivedInterface);
      (t2 as IDerivedInterface).N();
      (t2 as B).N();

    }
  }
}
```
@Rextester.eval(@CSharp)

*******************************************************************************

                                    {{3-4}}
*******************************************************************************

**Gegenüberstellung**

| interface                         | abstract class                                    |
| --------------------------------- | ------------------------------------------------- |
| viele Interfaces möglich          | immer nur eine Basisklasse                        |
| speichert keine Daten             | kann Felder umfassen                              |
| keine Konstruktorensignaturen     | kann Konstruktoren umfassen                       |
| beinhaltet nur Methodensignaturen | kann Signaturen und Implementierungen integrieren |
| keine Zugriffsmodifizierer        | beliebige Zugriffsmodifizierer                    |
| keine statischen Member           | statische Member möglich                          |

> Merke: Interfaces geben keine Struktur vor, sondern nur ein Verhalten!

**Und wozu brauche ich das?**

Klassen können nunmehr erben und gleichzeitig Interfaces implementieren:

```csharp
interface INewInterface { }
interface IAnotherInterface { }

class Base { }
class Derived : Base, INewInterface { }
class Another : Base, INewInterface, IAnotherInterface { }
```

Insbesondere ist es möglich verschiedenen Klassen ein gleiches Verhalten zu geben,
sie fungieren dabei als Platzhalter. Wenn eine Anwendung nicht gegen Einkaufsliste.Add()
oder Studenten.Remove() entwickelt wird, sondern gegen IList.Remove() und IList.Add()
wird die Verbindung entkoppelt.

```csharp
interface IList {              // interface für Geräusche
  public Add();
  public Remove();
}

class Einkaufsliste : IList { }
class Studenten : IList { }
class Material : IList { }
```

Ein Interface beschreibt Teile des Verhaltens einer Klasse und fungiert dabei
wie ein Platzhalter.

*******************************************************************************
