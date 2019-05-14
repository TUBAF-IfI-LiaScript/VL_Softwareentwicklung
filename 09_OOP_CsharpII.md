<!--

author:   Sebastian Zug & André Dietrich
email:    zug@ovgu.de   & andre.dietrich@ovgu.de
version:  0.0.1
language: de
narrator: Deutsch Female

import: https://raw.githubusercontent.com/liaScript/rextester_template/master/README.md

-->

# Vorlesung Softwareentwicklung - 9 Interfaces

--------------------------------------------------------------------
Link auf die aktuelle Vorlesung im Versionsmanagementsystem GitHub

https://github.com/liaScript/CsharpCourse/blob/master/09_OOP_CsharpII.md

Die interaktive Form ist unter diese Link zu finden ->
[LiaScript Vorlesung 9](https://liascript.github.io/course/?https://raw.githubusercontent.com/liaScript/CsharpCourse/master/09_OOP_CsharpII.md#1)

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

## 1. Operatorenüberladung in C\#

                                         {{0-1}}
******************************************************************************

Operatoren sind ein Set von Tokens, die grundlegende Operationen für
Grunddatentypen beschreiben.

```csharp
int a = 4;
int b = 7;
int c = a + b;                   // + Addition

string s1 = "Hello";
string s2 = "World";
string s3 = s1 + " " + s2;       // + für String Konkatenation
```

Analog zu Methoden werden können Operatoren überladen werden. Entsprechend wird
den Operatoren eine spezifische Bedeutung für die Klassen gegeben.

+ Operatoren werden in der Klasse überladen
+ Operatoren-Überladung ist immer static
+ Nutzung des Schlüsselwortes `operator`

**Überladbare Operatoren**

| Opererator                           | Bedeutug                                                                |
| ------------------------------------ | ----------------------------------------------------------------------- |
| +, -, !, ~, ++, --, true, false      | unäre Operatoren, überladbar                                            |
| +, -, \*, /, %, &, ^, <<, >>          | binäre Operatoren, überladbar                                           |
| ==, !=, <, >, <=, >=                 | Vergleichsoperatoren, überladbar                                        |
| []                                   | nicht überladbar, aber selbe Funktion mit Indexern                      |
| ()                                   | nicht überladbar, aber mittels custom conversion gleiche Funktionalität |
| +=, -=, \*=, /=, %=, &=, ^=, <<=, >>= | Werden durch die zugehörigen Operatoren automatisch überladen           |


******************************************************************************

                                       {{1-2}}
******************************************************************************

**Beispiel**

```csharp    Operatoren
using System;
using System.Reflection;
using System.ComponentModel.Design;

namespace Rextester
{
  public class Vector {
    public double X;
    public double Y;

    public Vector (double x, double y){
      this.X = x;
      this.Y = y;
    }

    //public static Vector operator +(Vector p1, Vector p2){
    //  return new Vector(p1.X + p2.X, p1.Y + p2.Y);
    //}

    public static Vector operator -(Vector p1, Vector p2){
      return new Vector(p1.X - p2.X, p1.Y - p2.Y);
    }

    public override string ToString(){
      return "x = " + X.ToString() + ", y = " + Y.ToString();
    }
  }

  public class Program
  {
    public static void Main(string[] args)
    {
      Vector a = new Vector (3,4);
      Vector b = new Vector (9,6);
      Console.WriteLine (a+b);
    }
  }
}
```
@Rextester.eval(@CSharp)

Die Operatoren += und -= werden dabei automatisch mit überladen.


******************************************************************************

                                       {{2-3}}
******************************************************************************

> **Merke:** Die Typen beim Überladen von Operatoren müssen nicht übereinstimmen!

Nehmen wir an, dass wir eine Skalierung $r$ unseres Vektors einfügen wollen und dafür
dessen Länge manipulieren.

```csharp
// Es müssen beide Varianten implementiert werden!
public static Point operator *(Point p1, double ratio)
{
  new Point(p1.X * ratio, p1.Y * ratio);
}

public static Point operator *(int ratio, Point p1)
{
  new Point(p1.X * ratio, p1.Y * ratio);
}

static void Main(string[] args)
{
  Point ptOne = new Point(100, 100);
  Point ptTwo = new Point(40, 40);
}
Console.WriteLine((ptOne * 2.5));
Console.WriteLine((1 * ptOne));
```

Unäre Operatoren (++, --) können in gleicher Art und Weise überschrieben werden.


******************************************************************************

                                       {{3-4}}
******************************************************************************

Wann sind zwei Klasseninstanzen gleich? Müssen alle Inhalte übereinstimmen?
Gibt es besondere Felder, deren Übereinstimmung relevanter sind?

| class | Felder                                               |
| ----- | ---------------------------------------------------- |
| Haus  | Farbe der Fenster, Markise (ja/nein), Zahl der Räume |
| Tier  | Art, Rasse, Geschlecht                               |
| Datei | Typ, Inhalt, Namen                                   |

```csharp    Upcast
using System;
using System.Reflection;
using System.ComponentModel.Design;

namespace Rextester
{
  public class Vector {
    public double X;
    public double Y;

    public Vector (double x, double y){
      this.X = x;
      this.Y = y;
    }

    public static bool operator ==(Vector p1, Vector p2){
      return (p1.X == p2.X) && (p1.Y == p2.Y);
    }

    public static bool operator !=(Vector p1, Vector p2){
      return (p1.X != p2.X) || (p1.Y != p2.Y);
    }

    //public override bool Equals(object p){
    //  return ???
    //}
  }

  public class Program
  {
    public static void Main(string[] args)
    {
      Vector a = new Vector (3,4);
      Vector b = new Vector (9,6);
      Console.WriteLine (a == b);
    }
  }
}
```
@Rextester.eval(@CSharp)

Die unären Operatoren `True` und `False` nehmen eine kleine Sonderrolle
ein:

```csharp  
public static bool operator true(Point p1) => (p1.X>0) && (p1.Y>0);
public static bool operator false(Point p1) => (p1.X < 0) && (p1.Y < 0);

Point pt1 = new Point(10, 10);
if (pt1) Console.WriteLine("true"); // true

// Point is neither true nor false:
Point pt2 = new Point(10, -10);
```

******************************************************************************

## 2. Kompatiblität zwischen Typen

                                   {{0-2}}
******************************************************************************

Strukturieren Sie die Klassen "Zug", "GüterZug", "PersonenZug" und "ICE" in einer
sinnvolle Vererbungshierarchie. Wie setzen Sie diese in C# Code um?

******************************************************************************

                                  {{1-2}}
******************************************************************************

```csharp    Upcast
using System;
using System.Reflection;
using System.ComponentModel.Design;

namespace Rextester
{
  class Zug
  {
    public Zug()
    {
      Console.WriteLine("Zug-ctor");
    }
  }

  class PersonenZug : Zug
  {
    public PersonenZug() : base()
    {
      Console.WriteLine("PersonenZug-ctor");
    }
  }

  class Ice : PersonenZug
  {
    public Ice()
    {
      Console.WriteLine("ICE-ctor");
    }
  }

  class GueterZug : Zug
  {
    public GueterZug()
    {
      Console.WriteLine("GueterZug-ctor");
    }
  }


  public class Program
  {
    public static void Main(string[] args)
    {
      Console.WriteLine("Generiere neuen ICE ");
      Ice ice = new Ice();
      Console.WriteLine("Generieren neuen Güterzug");
      GueterZug gueter = new GueterZug();
    }
  }
}
```
@Rextester.eval(@CSharp)

******************************************************************************

                                   {{2-3}}
******************************************************************************

In diesem Fall ist Zug die Basisklasse und PersonenZug, GueterZug und ICE sind
abgeleitete Klassen. Wir haben bereits gesehen, dass ein Mapping möglich ist,
aber was sind die Konsequenzen?

Eine Variablen vom Basisdatentyp kann immer eine Instanz einer abgeleiteten
Klasse zugewiesen werden. Entsprechend unterscheidet man dann zwischen dem
statischen und dem dynamischen Typ der Variablen. Der statische Typ ist immer der
immer der, der auch deklariert wurde. Der dynamische Typ wird durch die aktuelle
Referenz einer Instanz einer abgeleiteten Klasse von Zug bestimmt und ist
veränderlich.

| Zuweisung                  | statischer Typ von Zug | dynamischer Typ von Zug |
| -------------------------- | ---------------------- | ----------------------- |
| `Zug RB51 = new Zug()`     | Zug                    | Zug                     |
| `RB51 = new PersonenZug()` | Zug                    | PersonenZug             |
| `RB51 = new Ice`           | Zug                    | ICE                     |

******************************************************************************


### Laufzeitprüfung

Entsprechend brauchen wir eine Typprüfung, die untersucht, ob die Variable von
einem bestimmten dynamischen Typ  oder einem daraus abgeleiteten Typ ist.


+ der dynamische Typ einer Klasse kann zur Laufzeit geprüft werden
+ Typtest liefert bei null-Werten immer false

```csharp    Typprüfung
using System;
using System.Reflection;
using System.ComponentModel.Design;

namespace Rextester
{
  class Zug
  {
    public Zug()
    {
      Console.WriteLine("Zug-ctor");
    }
  }

  class PersonenZug : Zug
  {
    public PersonenZug() : base()
    {
      Console.WriteLine("PersonenZug-ctor");
    }
  }

  class Ice : PersonenZug
  {
    public Ice()
    {
      Console.WriteLine("ICE-ctor");
    }
  }

  class GueterZug : Zug
  {
    public GueterZug()
    {
      Console.WriteLine("GueterZug-ctor");
    }
  }

  public class Program
  {
    public static void Main(string[] args)
    {
      Zug IC239 = new Ice();
      Console.WriteLine("IC239 ist ein Zug? " + (IC239 is Zug)); // true
      Console.WriteLine("IC239 ist ein PersonenZug? " + (IC239 is PersonenZug)); // true
      Console.WriteLine("IC239 ist ein Ice? " + (IC239 is Ice)); // true
      IC239 = null;
      Console.WriteLine("IC239 ist ein Ice? " + (IC239 is Ice)); // false
    }
  }
}
```
@Rextester.eval(@CSharp)

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
      Console.WriteLine("human ist ein Fußballspieler? " + (human is Fußballspieler));
      //Console.WriteLine(human.rückennummer);
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

### Beispiel

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

## 3. Interfaces

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
