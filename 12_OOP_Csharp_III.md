<!--

author:   Sebastian Zug & André Dietrich
email:    Sebastian.Zug@informatik.tu-freiberg.de & andre.dietrich@informatik.tu-freiberg.de
version:  0.0.1
language: de
narrator: Deutsch Female

import: https://raw.githubusercontent.com/liaScript/rextester_template/master/README.md
        https://raw.githubusercontent.com/liascript-templates/plantUML/master/README.md

-->

# Softwareentwicklung - 12 - OOP Implementierung in Csharp

**TU Bergakademie Freiberg - Sommersemester 2020**

Link auf die aktuelle Vorlesung im Versionsmanagementsystem GitHub

[https://github.com/SebastianZug/CsharpCourse/blob/SoSe2020/12_OOP_Csharp_III.md](https://github.com/SebastianZug/CsharpCourse/blob/SoSe2020/12_OOP_Csharp_III.md)

Die interaktive Form ist unter diese Link zu finden ->
[LiaScript Vorlesung 12](https://liascript.github.io/course/?https://raw.githubusercontent.com/SebastianZug/CsharpCourse/SoSe2020/12_OOP_Csharp_III.md#1)

---------------------------------------------------------------------

## Rückblick auf die GitHub-Woche

TODO - Diagramm der Aktivitäten einfügen

TODO - Vorstellung PlantUML

TODO - Wiederholung git Projektfluss

## 7 Fragen in 7 Minuten

**1. Jetzt sind Sie dran ...**

**2. Jetzt sind Sie dran ...**

**3. Jetzt sind Sie dran ...**

**4. Jetzt sind Sie dran ...**

**5. Jetzt sind Sie dran ...**

**6. Jetzt sind Sie dran ...**

**7. Jetzt sind Sie dran ...**


## Abstrakte Klassen / Abstrakte Methoden

Mit `virtual` werden einzelne Methoden spezifiziert, die durch die abgeleiteten
Klassen implmentiert werden. Die Basisklasse hält aber eine "default" Implementierung
bereit. Letztendich kann man diesen Gedanken konsequent weiter treiben und die
Methoden der Basisklasse auf ein reines Muster reduzieren, dass keine eigenen Implementierungen
umfasst.

Diese Aufgabe übernehmen abstrakte Klassen und abstrakte Methoden. Eine abstrakte
Klasse:

+ kann nicht instanziiert werden
+ kann abstrakte Methoden umfassen
+ ist oft als Startpunkt(e) einer Vererbungshierarchie gedacht sind.

Innerhalb der Klasse können abstrakte Methoden integriert werden, die

+ implizit als virtuelle Methode implementiert angelegt werden
+ entsprechend keinen Methodenkörper umfassen

Eine nicht abstrakte Klasse, die von einer abstrakten Klasse abgeleitet wurde,
muss Implementierungen aller geerbten abstrakten Methoden und Accessoren
enthalten.

```csharp    abstractClass
using System;

namespace Rextester
{
  public abstract class Animal
  {
    public string Name;
    public Animal(string name){
      Name = name;
    }
    public abstract void makeSound();
  }

  public class Corcodile : Animal{

    public Corcodile(string name) : base(name){
      Name = name;
    }

    public override void makeSound(){
      Console.WriteLine("I'm a Crocodile");
    }
  }

  public class Program
  {
    public static void Main(string[] args){
      Corcodile A = new Corcodile("Tuffy");
      A.makeSound();
    }
  }
}
```
@Rextester.eval(@CSharp)

Abstrakte Klassen dienen somit als Template für nachgeordnete Unterklassen. Neben Methoden können auch Properties und Indexer als abstrakt deklariert werden.

Warum macht es keinen Sinn eine abstrakte Klasse als `sealed` zu deklarieren?

## Interfaces

                            {{0-1}}
*******************************************************************************

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
    public double Area() => area;
    public double Scope() => scope;
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

Schnittstellen werden verwendet:

+ um eine lose Kopplung zu erreichen.
+ um eine vollständige Abstraktion zu erreichen.
+ um komponentenbasierte Programmierung zu erreichen
+ um Mehrfachvererbung und Abstraktion zu erreichen.

*******************************************************************************

                            {{1-2}}
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

Es besteht keine Vererbungshierachie zwischen den beiden Klassen `A` und `B`! Vielmehr ergibt sich ein neuer Zusammenhang, die gemeinsame Implementierung eines Musters an Membern.

*******************************************************************************

                              {{2-3}}
*******************************************************************************

### Interfaces vs. Abstrakte Klassen

| interface                         | abstract class                                    |
| --------------------------------- | ------------------------------------------------- |
| viele Interfaces möglich          | immer nur eine Basisklasse                        |
| speichert keine Daten             | kann Felder umfassen                              |
| keine Konstruktorensignaturen     | kann Konstruktoren umfassen                       |
| beinhaltet nur Methodensignaturen | kann Signaturen und Implementierungen integrieren |
| keine Zugriffsmodifizierer        | beliebige Zugriffsmodifizierer                    |
| keine statischen Member           | statische Member möglich                          |

> Merke: Interfaces geben keine Struktur vor, sondern nur ein Verhalten!

### Anwendung

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

### Erweiterung von Interfaces

TODO Beispiel

```
@startuml

abstract class AbstractList
abstract AbstractCollection
interface List
interface Collection

List <|-- AbstractList
Collection <|-- AbstractCollection

Collection <|- List
AbstractCollection <|- AbstractList
AbstractList <|-- ArrayList

class ArrayList {
  Object[] elementData
  size()
}

annotation SuppressWarnings

@enduml
```
@plantUML.eval


### Auflösung von Namenskonflikten

TODO Beispiel

*******************************************************************************
