<!--

author:   Sebastian Zug & André Dietrich
email:    zug@ovgu.de   & andre.dietrich@ovgu.de
version:  0.0.1
language: de
narrator: Deutsch Female

import: https://raw.githubusercontent.com/liaScript/rextester_template/master/README.md

-->

# Vorlesung Softwareentwicklung - 6 - Objektorientierung und Klassen

--------------------------------------------------------------------
Link auf die aktuelle Vorlesung im Versionsmanagementsystem GitHub

https://github.com/liaScript/CsharpCourse/blob/master/06_OOPI.md

Die interaktive Form ist unter diese Link zu finden ->
[LiaScript Vorlesung 6](https://liascript.github.io/course/?https://raw.githubusercontent.com/liaScript/CsharpCourse/master/06_OOPI.md#1)

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
Jetzt mal konkret, wozu brauche ich das?

* dass Sie Messwerte erfassen und diese in eine Datei schreiben, Ihr Sensor generiert Zeitweise fehlerhafte Werte. Sie können dies Situation mit einzelnen Flags in einem struct ausweisen oder aber den Wert direkt als ungültig kennzeichnen.
* in einer Umfrage haben Teilnehmer einzelne Fragen nicht beantwortet. Die zugehörigen Variablen werden als `null` gekennzeichnet.

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
      int a = 5;
      if (a + i == null)
        Console.WriteLine("Das Ergebnis der Rechnung ist null!");
      // int c = a + i;                        // Compilerfeler
      // int c = a + i.Value;                  // Ausnahme für i == null
      // int c = a + i.GetValueOrDefault();    // Ausblendung von i == null mit
                                               // Standardwert   
      // int? c= a + i;
      // int d = c ?? -1;                      // Spezifikation einer eigenen
                                               // Null-Repräsenation
    }
  }
}
```
@Rextester.eval(@CSharp)

*******************************************************************************

## 2. Visionen der Objektorientierung

                                     {{0-1}}
*******************************************************************************

> Ein Objekt ist ein Bestandteil eines Programms, der Zustände enthalten kann. Diese Zustände werden von dem Objekt vor einem Zugriff von außen versteckt und damit geschützt. Außerdem stellt ein Objekt anderen Objekten Operationen zur Verfügung. Von außen kann dabei auf das Objekt ausschließlich zugegriffen werden, indem eine Operation auf dem Objekt aufgerufen wird.
Ein Objekt legt dabei selbst fest, wie es auf den Aufruf einer Operation reagiert. Die Reaktion kann in Änderungen des eigenen Zustands oder dem Aufruf von Operationen auf weiteren Objekte bestehen.

> **Merke**  *Ein Objekt ist eine zur Ausführungszeit vorhandene und für ihre Member Speicher allozierende Instanz, die sich aus der der Spezifikation einer Klasse erschließt.*

Ideen der OOP:
* Objekte der *realen Welt* müssen sich in der Programmierung widerspiegeln
* Es geht nicht um das Manipulieren von Daten, sondern um Zustandsänderungen von Objekten
* Im Zentrum der objektorientierten Programmierung stehen Objekte, die mittels Nachrichten miteinander kommunizieren

> **Merke** Wir haben zwei Herausforderungen zu meistern - Modellierung und Realisierung.

![OOPGeschichte](/img/06_OOPI/OOPHistory.png)<!-- width="60%" --> [WikiOOP](#7)


*******************************************************************************

                                     {{1-4}}
*******************************************************************************

**Beispiel - Simulationsumgebung Fußballspiel:**

+ 1 Objekt vom Typ "Spielsituation"
+ 1 Objekt vom Typ "Ball"
+ 2 Objekte vom Typ "Trainer"
+ 3 Objekte vom Typ "Schiedsrichter"
+ 22 Objekte vom Typ "Fußballspieler"

**Welche Eigenschaften hat jedes Objekt des Typen "Spieler"?**

*******************************************************************************

                                     {{2-4}}
*******************************************************************************
+ Name, Alter, Geschlecht, Gewicht, Größe
+ Position (x, y, z),  
+ im Spiel, Geschwindigkeit
+ Mannschaft, Rolle (Stürmer, Tormann, Verteidiger), Nummer
+ physischer Zustand (topfit, ausgepowert, verletzt)

Einige der Eigenschaften:
- ändern sich im Spielkontext, andere bleiben konstant
- lassen sich durchaus allen Personen zuordnen, anderen nur spezifischen Kategorien von Beteiligten.

*******************************************************************************

                                     {{3-4}}
*******************************************************************************

**Welche Methoden sollten über dem Objekt "Spieler" erlaubt sein und wie verändert dies deren Zustand**

+ "FängtDenBall()" -> Wirkt sich auf den Zustand von Ball aus, die Position des Balles ist identisch mit der des Spielers ... und es gibt nur einen Ball!
+ "WirftDenBall()"
+ "Foul(Spieler gefoulterSpieler)" -> Wirkt sich auf die Fitness von gefoulterSpieler aus  



Welche Schwachstellen sehen Sie bei unserem Modellierungsansatz / der
Realisierung?

*******************************************************************************

### Kapselung

                                     {{0-1}}
*******************************************************************************

>  Die Verkapselung bezieht sich auf die "Einhüllung" von Daten und Methoden innerhalb einer Struktur einhüllt, die die Objektimplementierung verbirgt und den unmittelbaren Datenzugriff außerhalb vorbestimmter Dienste unterbindet.

Vom Innenleben einer Klasse soll der Verwender – gemeint sind sowohl die Algorithmen, die mit der Klasse arbeiten, als auch der Programmierer, der diese entwickelt – möglichst wenig wissen müssen (Geheimnisprinzip). Durch die Kapselung werden nur Angaben über das „Was“ (Funktionsweise) einer Klasse nach außen sichtbar, nicht aber das „Wie“ (die interne Darstellung).

Standardidentifier für Daten- und Methodenzugriffe sind dabei:

| Bezeichner | UML Kürzel | Bedeutung                                                                         |
| ---------- | ---------- | --------------------------------------------------------------------------------- |
| public     | +          | Zugreifbar für alle Objekte (auch die anderer Klassen)                            |
| private    | -          | Nur für Objekte der eigenen Klasse zugreifbar                                     |
| protected  | #          | Nur für Objekte der eigenen Klasse und von Spezialisierungen derselben zugreifbar |

sowie weitere programmiersprachenspezifische Realisierungen (`internal`, `protected`).



**Vorteile**

+ Da die Implementierung einer Klasse anderen Klassen nicht bekannt ist, kann die Implementierung geändert werden, ohne die Zusammenarbeit mit anderen Klassen zu beeinträchtigen.
+ Es ergibt sich eine erhöhte Übersichtlichkeit, da nur die öffentliche Schnittstelle einer Klasse betrachtet werden muss.
+ Beim Zugriff über eine Zugriffsfunktion spielt es von außen keine Rolle, ob diese Funktion 1:1 im Inneren der Klasse existiert, das Ergebnis einer Berechnung ist oder möglicherweise aus anderen Quellen (z. B. einer Datei oder Datenbank) stammt.
+ Deutlich verbesserte Testbarkeit, Stabilität und Änderbarkeit der Software bzw. deren Teile (Module).

**Nachteile**

+ In Abhängigkeit vom Anwendungsfall Geschwindigkeitseinbußen durch den Aufruf von Zugriffsfunktionen (direkter Zugriff auf die Datenelemente wäre schneller).
+ Zusätzlicher Programmieraufwand für die Erstellung von Zugriffsfunktionen.

*******************************************************************************

                                     {{1-2}}
*******************************************************************************

**Beispiel Fußballsimulation**

1. Die Position x,y eines jeden Spielers und des Balls sollte nur über entsprechende Zugriffsmethoden manipuliert werden.
2. Die konkrete Implementierung der "Foul" oder "SchießtDenBall"-Methode bleibt geheim :-)

<!--
style="width: 90%; max-width: 860px; display: block; margin-left: auto; margin-right: auto;"
-->
````ascii
                                       public struct Position{float x; float y};
  +---------------------------+        
  | Spieler                   |        public class Spieler{ // oder struct !!!   
  +---------------------------+           enum Rolle {Stürmer, Tormann, Verteidiger};
  | - name: string            |           private string Name; byte? Alter;   |
  | - alter: byte             |           private int x, y, dx, dy;           |  Geschützte
  | - player: Postition       |           private Rolle player;               |  Felder
  | ...                       | -->       private Position position;          |
  +---------------------------+                                                
  | ✛ FängtDenBall(): void    |           public get ... set ...              |  Zugriffsmethoden für
  | ✛ SchießtDenBall(): Kraft |                                                  Felder
  | ✛ Foul()                  |           public void FängtDenBall();
  | ...                       |           public Kraft = SchießtDenBall();
  +---------------------------+           public Foul(SpielerX);              |  Event an SpielerX im
                                       }                                         "Erfolgsfall"
````

*******************************************************************************

### Vererbung


                                     {{0-1}}
*******************************************************************************
> Die Vererbung dient dazu, aufbauend auf existierenden Klassen neue zu
> schaffen. Aus der Klassenspezifikation einer Klasse wird eine neue Klasse
> hergeleitet. Diese ist dann entweder eine Erweiterung oder eine Einschränkung
> der ursprünglichen Klasse.

Die vererbende Klasse wird meist Basisklasse (auch Super-, Ober- oder Elternklasse) genannt, die erbende abgeleitete Klasse (auch Sub-, Unter- oder Kindklasse). Den Vorgang des Erbens nennt man meist Ableitung oder Spezialisierung.

![Vererbungsbeispiel](/img/06_OOPI/Vererbungsbeispiel.png)<!-- width="60%" --> [WikiInheri](#7)


**Vorteile**

+ Abbildung der Eigenschaften und Daten in einem hierarchischen Konzept
+ Steigerung der Wartbarkeit und Erweiterbarkeit

**Nachteile**

+ Eine Klasse, die als Subklasse aus anderen Klassen entsteht, ist kein autonomer Baustein. Bei der Verwendung der Klasse wird es immer wieder zu Rückgriffen auf die Basisklasse(n) kommen.
+ Bisweilen schwierige Modellierung
+ Schaffung von Abhängigkeitsverhältnissen, die dem Modularisierungsgedanken nicht entsprechen.


*******************************************************************************

                                     {{1-2}}
*******************************************************************************


**Am Beispiel Fußballspiel**

<!--
style="width: 90%; max-width: 560px; display: block; margin-left: auto; margin-right: auto;"
-->
````ascii

   Erbende Klasse                  Höherabstrakte Klasse          

  +-----------------------+         
  | Spieler               |       
  +-----------------------+       
  | + Position            |           
  | ...                   | --.
  +-----------------------+   |
  | + FängtDenBall()      |   |   +------------------------+
  | + SchießtDenBall()    |   |   | Person                 |    
  | + Foul()              |   |   +------------------------+
  | ...                   |   '--▷| Name                   |
  +-----------------------+       | Alter                  |
                                  | ...                    |
  +-----------------------+       +------------------------+  
  | Schiedsrichter        |       | SetName()              |
  +-----------------------+   .--▷| SetAge()               |
  | + Rolle               |   |   | ...                    |    
  | + ...                 |   |   +------------------------+  
  +-----------------------+   |
  | + StartedSpiel()      |   |   
  | + BeendetDasSpiel()   | --'           
  | + ErkenntFoul()       |
  | ...                   |       
  +-----------------------+

````

*******************************************************************************

                                     {{2-3}}
*******************************************************************************


**Zentrale Objekt-Klasse**

https://docs.microsoft.com/de-de/dotnet/api/system.object?view=netframework-4.8

OOP Sprachen verfügen meist über eine zentrale Klasse, von der alle Klassen in
letztlich abgeleitet sind. Diese heißt bei diesen Sprachen Object. In Eiffel
wird sie mit ANY bezeichnet. Zu den wenigen Ausnahmen, in denen es keine solche
Klasse gibt, zählen C++ oder Python.

In den Sprachen mit zentraler Basisklasse erbt eine Klasse, für die keine
Basisklasse angegeben wird, implizit von dieser besonderen Klasse. Ein Vorteil
davon ist, dass allgemeine Funktionalität, beispielsweise für die Serialisierung,
Ausgaben, Hashwerte oder die Typinformation, dort untergebracht werden kann. Weiterhin ermöglicht es
die Deklaration von Variablen, denen ein Objekt jeder beliebigen Klasse
zugewiesen werden kann. Dies ist besonders hilfreich zur Implementierung von
Containerklassen, wenn eine Sprache keine generische Programmierung unterstützt.

```csharp                                      Iniitalisation
using System;

namespace Rextester
{
  public class Program
  {
    public static void Main(string[] args){
      Console.WriteLine(typeof(int));
      Console.WriteLine(typeof(int).BaseType);
      Console.WriteLine(typeof(int).BaseType.BaseType);
    }
  }
}
```
@Rextester.eval(@CSharp)


```csharp                           AccessMethods
using System;

namespace Rextester
{
  public class Program
  {
    public static void Main(string[] args){
      Type t = typeof(Program);
      Console.WriteLine("------ Methods --------");
      System.Reflection.MethodInfo[] methodInfo = t.GetMethods();

      foreach (System.Reflection.MethodInfo mInfo in methodInfo)
        Console.WriteLine(mInfo.ToString());

      Console.WriteLine("------  Members ------");
      System.Reflection.MemberInfo[] memberInfo = t.GetMembers();
      foreach (System.Reflection.MemberInfo mInfo in methodInfo)
        Console.WriteLine(mInfo.ToString());
    }
  }
}
```
@Rextester.eval(@CSharp)

*******************************************************************************

### Polymorphie

> Polymorphie oder Polymorphismus (griechisch für Vielgestaltigkeit) ermöglicht, dass ein Bezeichner sich in seiner Funktionalität in Abhängigkeit von den Datentypen verändert.  

Die Polymorphie der objektorientierten Programmierung ist eine Eigenschaft, die
immer im Zusammenhang mit Vererbung und Schnittstellen (Interfaces) auftritt.
Eine Methode ist polymorph, wenn sie in verschiedenen Klassen die gleiche
Signatur hat, jedoch erneut implementiert ist.

```csharp
public class Shape
{
    public int X { get; private set; }
    ....
    // Virtual method
    public virtual void Draw()
    {
        Console.WriteLine("Performing base class drawing tasks");
    }
}

class Circle : Shape
{
    public override void Draw()
    {
        // Code to draw a circle...
        Console.WriteLine("Drawing a circle");
        base.Draw();
    }
}

class Square : Shape {}
...

```
In C# ist jeder Typ polymorph, da alle Typen, einschließlich
benutzerdefinierten Typen, von Object erben.

Beim Vererben erhält die abgeleitete Klasse alle Methoden, Felder, Eigenschaften
und Ereignisse der Basisklasse. Dabei gilt es zu entscheiden, welche davon
unverändert übernommen und welche auf die spezifischen Anforderungen angepasst
werden sollen.

**Am Beispiel Fußballspiel**

```csharp   
using System;

namespace Rextester
{
  public class Program
  {
    public class Person{
      private int alter;
      public virtual void setAge(int alter) {
        this.alter = alter;
      }
    }

    public class Spieler {
      public override void setAge(int alter) {
        // hier wird noch getestet ob der Spieler älter als 16 ist
      }
    }
  }
}
```

### Weitere Beispiele

| Beispiel  | Kapselung                                                                                                         | Vererbung                                                      | Polymorphie                                                                                                              |
| --------- | ----------------------------------------------------------------------------------------------------------------- | -------------------------------------------------------------- | ------------------------------------------------------------------------------------------------------------------------ |
| Auto      | Interne Daten (CAN-Nachrichten zwischen Motor und Getriebe, Zündzeitpunkte, etc.) sind für mich nicht interessant | Kleinwagen und Kombis sind lediglich spezielle Arten von Autos | Ein Maserati Quattroporte und ein C Corsa können beide fahren. Trotzdem sind die Auswirkungen verschieden                |
| Säugetier | Die genauen Vorgänge der Verdauung interessieren nicht. Nur das benötigte Futter ist wichtig                      | Eine Springmaus und eine Hausratte sind beide aus der Ordnung der Nagetiere. Beide Tiere teilen viele gleiche Eigenschaften                                                           | Fortbewegen ist eine Eigenschaft jedes Säugetieres. Ein Wal schwimmt jedoch und ein Känguru hüpft. Ein Pferd galoppiert. |


### Begriffe

| Begriff     | Bedeutung                                                                                                                                                                                                                        |
| ----------- | -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| Klassen     | Klassen sind Vorlagen, aus denen Instanzen genannte Objekte zur Laufzeit erzeugt werden.                                                                                                                                         |
| Objekt      | Ein Element, welches Funktionen, Methoden, Prozeduren, einen inneren Zustand, oder mehrere dieser Dinge besitzt. Es leitet sich von  einer Spezifikation ab.                                                                     |
| Entität     | Ein Objekt, welches eine Identität besitzt, welche unveränderlich ist. Beispielsweise kann eine Person ihre Adresse, Telefonnummer oder Namen ändern, ohne zu einer anderen Person zu werden. Eine Person ist also eine Entität. |
| Eigenschaft | Ein Bestandteil des Zustands eines Objekts.                                                                                                                                                                                      |
| Prozedur    | Verändert den Zustand eines Objektes, ohne einen Rückgabewert zu liefern. Eine Prozedur kann andere Objekte als Parameter entgegennehmen.                                                                                        |
| Funktion    | Ordnet einer gegebenen Eingabe einen bestimmten Rückgabewert zu. Eine Funktion zeichnet sich insbesondere dadurch aus, dass sie nicht den Zustand eines Objekts verändert.                                                       |
| Methode     | Verändert den Zustand eines Objekts und liefert zudem einen Rückgabewert. Eine Methode kann andere Objekte als Parameter entgegennehmen.                                                                                         |
| Modul       | Eine zusammengefasste Gruppe von Objekten.                                                                                                                                                                                       |
[Ghosh]

## 3. Prinzipien des (objektorientierten) Softwareentwurfs

> **Merke:** Software lebt!

+ Prinzipien zum Entwurf von Systemen
+ Prinzipien zum Entwurf einzelner Klassen
+ Prinzipien zum Entwurf miteinander kooperierender Klassen

Robert C. Martin [Link](https://de.wikipedia.org/wiki/Robert_Cecil_Martin)
fasste eine wichtige Gruppe von Prinzipien zur Erzeugung wartbarer und
erweiterbarer Software unter dem Begriff "SOLID" zusammen [Martin](#10). Robert
C. Martin erklärte diese Prinzipien zu den wichtigsten Entwurfsprinzipien. Die
SOLID-Prinzipien bestehen aus:

* Single Responsibility Prinzip
* Open-Closed Prinzip
* Liskovsches Substitutionsprinzip
* Interface Segregation Prinzip
* Dependency Inversion Prinzip

Die folgende Darstellung basiert auf den Referenzen [Just](#10) [](#10). Eine
sehr gute, an einem Beispiel vorangetrieben Erläuterung ist unter [Krämer](#10)


### Prinzip einer einzigen Verantwortung (Single-Responsibility-Prinzip SRP)

In der objektorientierten Programmierung sagt das SRP aus, dass jede Klasse nur
eine fest definierte Aufgabe zu erfüllen hat. In einer Klasse sollten lediglich
Funktionen vorhanden sein, die direkt zur Erfüllung dieser Aufgabe beitragen.

> “There should never be more than one reason for a class to change. [Robert C. Martin]

+  Verantwortlichkeit = Grund für eine Änderung (multiple Veränderungen == multiple Verantwortlichkeiten)

```csharp
class Employee
{
  public Money calculatePay()
  public void save()
  public String reportHours()
}
```

* Mehrere Verantwortlichkeiten innerhalb eines Software-Moduls führen zu zerbrechlichem Design, da Wechselwirkungen bei den Verantwortlichkeit nicht ausgeschlossen werden können


```java                       SpaceStation
class SpaceStation{}
  public initialize()
  public void run_sensors()
  public void load_supplies(type, quantity)
  public void use_supplies(type, quantity)
  public void report_supplies ()
  public void load_fuel(quantity)
  public void report_fuel()
  public void activate_thrusters()
}
```

Eine mögliche Realisierung zum Beispiel findet sich unter https://medium.com/@severinperez/writing-flexible-code-with-the-single-responsibility-principle-b71c4f3f883f

> **Merke:** Vermeiden Sie "God"-Objekte


**Verallgemeinerung [WikiSRP]**

Eine Verallgemeinerung des SRP stellt Curly’s Law [CodingHorror] dar, welches das Konzept
"methods should do one thing" bis "single source of truth" zusammenfasst und
auf alle Aspekte eines Softwareentwurfs anwendet. Dazu gehören
nicht nur Klassen, sondern unter anderem auch Funktionen und Variablen.

```csharp
var numbers = new [] { 5,8,4,3,1 };
numbers = numbers.OrderBy(i => i);

var numbers = new [] { 5,8,4,3,1 };
var orderedNumbers = numbers.OrderBy(i => i);
```

Da die Variable `numbers` zuerst die unsortierten Zahlen repräsentiert und nachher
die sortierten Zahlen, wird Curly’s Law verletzt. Dies lässt sich auflösen,
indem eine zusätzliche Variable eingeführt wird.

### Open-Closed Prinzip

Bertrand Meyer beschreibt das Open-Closed-Prinzip durch:
*Module sollten sowohl offen (für Erweiterungen) als auch verschlossen (für Modifikationen) sein.* [[Meyer](#11)]

Eine Erweiterung im Sinne des Open-Closed-Prinzips ist beispielsweise die
Vererbung. Diese verändert das vorhandene Verhalten der Einheit nicht, erweitert
aber die Einheit um zusätzliche Funktionen oder Daten. Überschriebene Methoden
verändern auch nicht das Verhalten der Basisklasse, sondern nur das der
abgeleiteten Klasse.

Beispiel: Etablierung einer `ObservableList` auf der Basis der generischen .NET
`List` Klasse:

```csharp
class List<T>{
  public bool Contains (T item);
  public void Sort (Comparison<T> comparison);
}

class ObservableList<T> : List<T>
{
  public event Action CollectionChanged;
}
```


###  Liskovsche Substitutionsprinzip (LSP)

> Das Liskovsche Substitutionsprinzip (LSP) oder Ersetzbarkeitsprinzip besagt, dass ein Programm, das Objekte einer Basisklasse T verwendet, auch mit Objekten der davon abgeleiteten Klasse S korrekt funktionieren muss, ohne dabei das Programm zu verändern:

*"Sei $q(x)$ eine beweisbare Eigenschaft von Objekten $x$ des Typs $T$. Dann soll $q(y)$ für Objekte $y$ des Typs $S$ wahr sein, wobei $S$bein Untertyp von $T$ ist.“* [2]

Beispiel: Grafische Darstellung von verschiedenen Primitiven

![Liskov](/img/06_OOPI/LiskovUML.jpg)<!-- width="60%" --> [WikiLiskov](#7)

Beispiel: Vögel / Flugunfähgige Vögel


### Interface Segregation Prinzip

Zu große Schnittstellen in mehrere
Schnittstellen aufgeteilt werden, falls implementierende Klassen unnötige
Methoden haben müssen. Nach erfolgreicher Anwendung dieses Entwurfprinzips würde
ein Modul, das eine Schnittstelle benutzt, nur die Methoden implementieren
müssen, die es auch wirklich braucht.

                                     {{1-2}}
*******************************************************************************

Was sind eigentlich Interfaces?

Eine Schnittstelle enthält Definitionen für eine Gruppe von zugehörigen
Funktionalitäten, die von einer Klasse oder einer Struktur implementiert werden
können.

Durch die Verwendung von Schnittstellen können Sie beispielsweise das Verhalten aus mehreren Quellen in einer Klasse einbeziehen. Diese Funktion ist wichtig in C#, da die Sprache die mehrfache Vererbung von Klassen nicht unterstützt. Zudem müssen Sie eine Schnittstelle verwenden, wenn Sie die Vererbung für Strukturen simulieren möchten, da sie tatsächlich nicht von einer anderen Struktur oder Klasse erben können. [MSProgrammierhandbuchC#]

https://docs.microsoft.com/de-de/dotnet/csharp/programming-guide/interfaces/


*******************************************************************************

                                      {{2-3}}
*******************************************************************************

```csharp
public interface IVehicle
{
    void Drive();
    void Fly();
}


public class MultiFunctionalCar : IVehicle
{
    public void Drive()
    {
        //actions to start driving car
        Console.WriteLine("Drive a multifunctional car");
    }

    public void Fly()
    {
        //actions to start flying
        Console.WriteLine("Fly a multifunctional car");
    }
}

public class Car : IVehicle
{
    public void Drive()
    {
        //actions to drive a car
        Console.WriteLine("Driving a car");
    }

    public void Fly()
    {
        throw new NotImplementedException();
    }
}
```

Lösung unter Beachtung des Interface Segregation Prinzip

```csharp
public interface ICar
{
    void Drive();
}

public interface IAirplane
{
    void Fly();
}

public class Car : ICar
{
    public void Drive()
    {
        //actions to drive a car
        Console.WriteLine("Driving a car");
    }
}


public class MultiFunctionalCar : ICar, IAirplane
{
    public void Drive()
    {
        //actions to start driving car
        Console.WriteLine("Drive a multifunctional car");
    }

    public void Fly()
    {
        //actions to start flying
        Console.WriteLine("Fly a multifunctional car");
    }
}
```

Man könnte jetzt sogar ein Highlevel Interface realisieren, dass beide Aspekte
integriert.

```
public interface IMultiFunctionalVehicle : ICar, IAirplane
{
}

public class MultiFunctionalCar : IMultiFunctionalVehicle
{
}
```
**Vorteil**

+ übersichtlichere kleinere Schnittstellen, die flexibler kombiniert werden können
+ Klassen umfassen keine Methoden, die sie nicht benötigen

-> Das Prinzip der Schnittstellentrennung verbessert die Lesbarkeit und Wartbarkeit unseres Codes.

*******************************************************************************

### Dependency Inversion Prinzip

> "High-level modules should not depend on low-level modules. Both should
depend on abstractions. Abstractions should not depend upon details. Details
should depend upon abstractions" [Martin]

Eine Klasse einer höheren Ebene soll nicht von einer Klasse einer tieferen Ebene
abhängig sein!


```csharp
public class Email
{
    public string ToAddress { get; set; }
    public string Subject { get; set; }
    public string Content { get; set; }
    public void SendEmail()
    {
        //Send email
    }
}

public class SMS
{
    public string PhoneNumber { get; set; }
    public string Message { get; set; }
    public void SendSMS()
    {
        //Send sms
    }
}

public class Notification
{
    private Email _email;
    private SMS _sms;
    public Notification()
    {
        _email = new Email();
        _sms = new SMS();
    }

    public void Send()
    {
        _email.SendEmail();      // Abhängigkeit von Email
        _sms.SendSMS();          // Abhängigkeit von SMS
    }
}

```


```csharp
// Schritt 1: Interface Definition
public interface IMessage
{
    void SendMessage();
}

// Schritt 2: Die niederwertigeren Klassen implmentieren das Interface
public class Email : IMessage
{
    public string ToAddress { get; set; }
    public string Subject { get; set; }
    public string Content { get; set; }
    public void SendMessage()
    {
        //Send email
    }
}

public class SMS : IMessage
{
    public string PhoneNumber { get; set; }
    public string Message { get; set; }
    public void SendMessage()
    {
        //Send sms
    }
}

// Schritt 3: Die höherwertige Klasse wird gegen das Interface implementiert
public class Notification
{
    private ICollection<IMessage> _messages;
    public Notification(ICollection<IMessage> messages)
    {
        this._messages = messages;
    }
    public void Send()
    {
        foreach(var message in _messages)
        {
            message.SendMessage();
        }
    }
}
```

Beispiel aus https://exceptionnotfound.net/simply-solid-the-dependency-inversion-principle/


## 5. Beispiel der Woche ...

```csharp           Fußballspieler                                   
using System;
using System.Collections;
using System.Collections.Generic;

namespace Rextester
{
  class Person{
    public int alter;
  }

  class Spieler : Person{
    public float erfahrung;
    public int rückennummer;
  }

  class Torwart : Spieler{
    public void FängtDenBall(){
      if (erfahrung > 0.5 & alter < 60) {
        Console.WriteLine("Ball gefangen!");
      }
      else {
        Console.WriteLine("Das war wohl nichts!");
      }
    }
  }

  public class Program
  {
    public static void Main(string[] args){
      Torwart A = new Torwart();
      A.alter = 25;
      A.rückennummer = 13;
      A.erfahrung = 0.8f;
      A.FängtDenBall();
    }
  }
}
```
@Rextester.eval(@CSharp)


## Anhang

**Referenzen**

[WikiInheri]  Wikipedia, "Vererbung", Autor "cactus26", [Link](https://de.wikipedia.org/wiki/Vererbung_&28Programmierung&29\#Kovarianz_und_Kontravarianz)

[Ghosh] Debasish Ghosh "Functional and Reactive Domain Modeling". Manning, 2016,

[Martin] Robert Cecil Martin "Clean Code: Refactoring, Patterns, Testen und Techniken für sauberen Code" mitp-Verlag, 2009, ISBN 978-0-13-235088-4.

[Just] Markus Just, IT Designers Gruppe, "Entwurfsprinzipien", Foliensatz Fachhochschule Esslingen [Link](http://www.it-designers-gruppe.de/fileadmin/Inhalte/Studentenportal/Die_SOLID-Prinzipien__Folien___1_.pdf)

[UncleBob] Robert C. Martin, Webseite "The principles of OOD", http://www.butunclebob.com/ArticleS.UncleBob.PrinciplesOfOod

[WikiSRP] Wikipedia, "Single-Responsibility-Prinzip", [Link](https://de.wikipedia.org/wiki/Single-Responsibility-Prinzip)

[CodingHorror] https://blog.codinghorror.com/curlys-law-do-one-thing/

[Meyer] Bertrand Meyer, "Object Oriented Software Construction" Prentice Hall, 1988,

[Krämer] Andre Krämer, "SOLID - Die 5 Prinzipien für objektorientiertes Softwaredesign", [Link](https://www.informatik-aktuell.de/entwicklung/methoden/solid-die-5-prinzipien-fuer-objektorientiertes-softwaredesign.html)

[WikiLiskov] Wikipedia, "Liskovsches Substitutionsprinzip", Autor "T800", [Link](https://commons.wikimedia.org/w/index.php?curid=3644974)

**Autoren**

Sebastian Zug, André Dietrich
