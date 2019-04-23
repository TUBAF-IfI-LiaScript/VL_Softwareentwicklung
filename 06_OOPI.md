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

> Ein Objekt ist ein Bestandteil eines Programms, der Zustände enthalten kann. Diese Zustände werden von dem Objekt vor einem Zugriff von außen versteckt und damit geschützt. Außerdem stellt ein Objekt anderen Objekten Operationen zur Verfügung. Von außen kann dabei auf das Objekt ausschließlich zugegriffen werden, indem eine Operation auf dem Objekt aufgerufen wird.
Ein Objekt legt dabei selbst fest, wie es auf den Aufruf einer Operation reagiert. Die Reaktion kann in Änderungen des eigenen Zustands oder dem Aufruf von Operationen auf weiteren Objekte bestehen.

> **Merke**  *Objekt = Identität + Zustand + Verhalten*

Ideen der OOP:
* Objekte der realen Welt müssen sich in der Programmierung widerspiegeln
* Es geht nicht um das Manipulieren von Daten sondern um Zustandsänderungen von Objekten
* Im Zentrum der objektorientierten Programmierung stehen Objekte, die mittels Nachrichten miteinander kommunizieren

> **Merke** Wir haben zwei Herausforderungen zu meistern - Modellierung und Realisierung.

Beispiel 1 - Simulationsumgebung Fußballspiel:

+ 1 Objekt vom Typ "Spielsituation"
+ 1 Objekt vom Typ "Ball"
+ 2 Objekte vom Typ "Trainer"
+ 3 Objekte vom Typ "Schiedsrichter"
+ 22 Objekte vom Typ "Fußballspieler"
+ alle sind aber gleichzeitig Menschen, die Fußballspieler gliedern sich  
weiter

Welche Eigenschaften hat jedes Objekt des Typen "Spieler":

+ Name, Alter, Geschlecht, Gewicht, Größe
+ Position (x, y, z),  
+ im Spiel, Geschwindigkeit
+ Mannschaft, Rolle (Stürmer, Tormann, Verteidiger), Nummer
+ physischer Zustand (topfit, ausgepowert, verletzt)

Welche Methoden sollten über dem Objekt erlaubt sein:

+ "FängtDenBall()"
+ "WirftDenBall()"
+ "BallAnnahmeFuß()"
+

<!--
style="width: 90%; max-width: 560px; display: block; margin-left: auto; margin-right: auto;"
-->
````ascii

  Modell                          Realisierung          


  +-----------------------+         
  | Spieler               |       class Spieler{ // oder struct !!!
  +-----------------------+     
  | + Name                |           string Name; byte? Alter;
  | + Alter               |           int x, y, dx, dy;
  | + Position            |           enum Position {Stürmer, Tormann, Verteidiger}
  | ...                   | -->       Position myPosition;
  +-----------------------+
  | + FängtDenBall()      |           void FängtDenBall()
  | + SchießtDenBall()    |           Kraft = SchießtDenBall()
  | + Foul()              |           Spieler = Foul()
  | ...                   |        }
  +-----------------------+

  +-----------------------+         
  | Schiedsrichter        |       class Schiedsrichter{ // oder struct !!!
  +-----------------------+     
  | + Name                |           string Name; byte? Alter;
  | + Alter               |           int x, y, dx, dy;
  | + Position            |           Position myPosition;
  | ...                   | -->       
  +-----------------------+
  | + StartedSpiel()      |           void StartedSpiel()
  | + BeendetDasSpiel()   |           Kraft = SchießtDenBall()
  | + ErkenntFoul()       |           float = Foul()
  | ...                   |        }
  +-----------------------+


````

Welche Schwachstellen sehen Sie bei unserem Modellierungsansatz / der
Realisierung?

### Kapselung

>  Die Verkapselung bezieht sich auf die "Einhüllung" von Daten und Methoden innerhalb einer Struktur einhüllt, die die Objektimplementierung verbirgt und den unmittelbaren Datenzugriff außerhalb vorbestimmter Dienste unterbindet.

Vom Innenleben einer Klasse soll der Verwender – gemeint sind sowohl die Algorithmen, die mit der Klasse arbeiten, als auch der Programmierer, der diese entwickelt – möglichst wenig wissen müssen (Geheimnisprinzip). Durch die Kapselung werden nur Angaben über das „Was“ (Funktionsweise) einer Klasse nach außen sichtbar, nicht aber das „Wie“ (die interne Darstellung).

Standardidentifier für Daten- und Methodenzugriffe sind dabei:

| Bezeichner | UML Kürzel | Bedeutung |
| ---------- | ---------- | --------- |
| public     | +           | Zugreifbar für alle Objekte (auch die anderer Klassen)          |
| private    | -           | Nur für Objekte der eigenen Klasse zugreifbar          |
| protected  | #           | Nur für Objekte der eigenen Klasse und von Spezialisierungen derselben zugreifbar          |

sowie weitere programmiersprachenspezifische Realisierungen (internal, protected, usw.).

**Am Beispiel Fußballspiel**
1. Die Position x,y eines jeden Spielers und des Balls sollte nur über entsprechende
Zugriffsmethoden manipuliert werden.
2. Die konkrete Implementierung der "Foul"-Methode bleibt geheim :-)

```csharp                           AccessMethods
using System;

namespace Rextester
{
  public class Program
  {
    public static void Main(string[] args){
      Type t = typeof(Program);
      Console.WriteLine("Methods:");
      System.Reflection.MethodInfo[] methodInfo = t.GetMethods();

      foreach (System.Reflection.MethodInfo mInfo in methodInfo)
        Console.WriteLine(mInfo.ToString());

      Console.WriteLine("Members:");
      System.Reflection.MemberInfo[] memberInfo = t.GetMembers();
      foreach (System.Reflection.MemberInfo mInfo in methodInfo)
        Console.WriteLine(mInfo.ToString());
    }
  }
}
```
@Rextester.eval(@CSharp)


**Vorteile**

+ Da die Implementierung einer Klasse anderen Klassen nicht bekannt ist, kann die Implementierung geändert werden, ohne die Zusammenarbeit mit anderen Klassen zu beeinträchtigen.
+ Es ergibt sich eine erhöhte Übersichtlichkeit, da nur die öffentliche Schnittstelle einer Klasse betrachtet werden muss.
+ Beim Zugriff über eine Zugriffsfunktion spielt es von außen keine Rolle, ob diese Funktion 1:1 im Inneren der Klasse existiert, das Ergebnis einer Berechnung ist oder möglicherweise aus anderen Quellen (z. B. einer Datei oder Datenbank) stammt.
+ Deutlich verbesserte Testbarkeit, Stabilität und Änderbarkeit der Software bzw. deren Teile (Module).

**Nachteile**

+ In Abhängigkeit vom Anwendungsfall Geschwindigkeitseinbußen durch den Aufruf von Zugriffsfunktionen (direkter Zugriff auf die Datenelemente wäre schneller).
+ Zusätzlicher Programmieraufwand für die Erstellung von Zugriffsfunktionen.

### Vererbung

> Die Vererbung dient dazu, aufbauend auf existierenden Klassen neue zu schaffen,.
Aus der Klassenspezifikation einer Klasse wird eine neue Klasse hergeleitet. Diese
ist dann entweder eine Erweiterung oder eine Einschränkung der ursprünglichen Klasse.

Die vererbende Klasse wird meist Basisklasse (auch Super-, Ober- oder Elternklasse) genannt, die erbende abgeleitete Klasse (auch Sub-, Unter- oder Kindklasse). Den Vorgang des Erbens nennt man meist Ableitung oder Spezialisierung.

![Vererbungsbeispiel](/img/06_OOPI/Vererbungsbeispiel.png)<!-- width="60%" --> [WikiInheri](#7)

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
  | ...                   | -->
  +-----------------------+
  | + FängtDenBall()      |       +------------------------+
  | + SchießtDenBall()    |       | Person                 |    
  | + Foul()              |       +------------------------+
  | ...                   |   ▷  ▹| Name                   |
  +-----------------------+       | Alter                  |
                                  | ...                    |
  +-----------------------+       +------------------------+  
  | Schiedsrichter        |       | SetName()              |
  +-----------------------+       | SetAge()               |
  | + Rolle               |       | ...                    |    
  | + ...                 |       +------------------------+  
  +-----------------------+
  | + StartedSpiel()      |      
  | + BeendetDasSpiel()   |           
  | + ErkenntFoul()       |
  | ...                   |       
  +-----------------------+

````

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




Nachteile

+ Eine Klasse, die als Subklasse aus anderen Klassen entsteht, ist kein autonomer Baustein. Bei der Verwendung der Klasse kann es immer wieder zu Rückgriffen auf die Basisklasse(n) kommen.

+ Bisweilen schwierige Modellierung (das )


Mit Vererbung schafft man Abhängigkeitsbeziehungen, die dem Modularisierungsgedanken nicht entsprechen. Man muss daher immer genau abwägen, ob man die Vorteile von Vererbung nutzen möchte (leichte Realisierbarkeit durch Rückgriff auf Gegebenes) oder ob man die Nachteile von Vererbung (keine Bausteine, die man autonom verwenden kann) vermeiden möchte.


### Polymorphie

> Polymorphie oder Polymorphismus (griechisch für Vielgestaltigkeit) ermöglicht, dass ein Bezeichner sich in seiner Funktionalität in Abhängigkeit von den Datentypen verändert.  

Die Vielgetaltigkeit deckt dabei zwei Aspekte ab:

+ Die virtuelle Methoden, die eine Basisklasse definiert und implementiert, können von abgeleiteten Klassen überschrieben werden. Zur Laufzeit, wenn die Methode der
abgeleiteten Klasse aufgerufen wird, sucht die CLR den Laufzeittyp des Objekts und ruft die Überschreibung der virtuellen Methode auf.

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

+ In Methodenparametern, Auflistungen und Arrays können Objekte einer abgeleiteten Klasse zur Laufzeit als Objekte einer Basisklasse behandelt werden. In diesem Fall ist der deklarierte Typ des Objekts nicht mehr mit dem Laufzeittyp identisch.


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
  | + SetName()           |   |   +------------------------+
  | + SetAge()            |   |   | Person                 |    
  | + ...                 |   |   +------------------------+
  | + Foul()              |   .--▷| Name                   |
  +-----------------------+       | Alter                  |
                                  | ...                    |
  +-----------------------+       +------------------------+  
  | Schiedsrichter        |   .--▷| SetName()              |
  +-----------------------+   |   | SetAge()               |
  | + Rolle               |   |   | ...                    |    
  | + ...                 |   |   +------------------------+  
  +-----------------------+   |
  | + SetName()           | --.      
  | + SetAge()            |            
  | + ...                 |
  | + StarteSpiel()       |       
  +-----------------------+

````


### Weitere Beispiele

| Beispiel  | Kapselung                                                                                                         | Vererbung                                                      | Polymorphie                                                                                                              |
| --------- | ----------------------------------------------------------------------------------------------------------------- | -------------------------------------------------------------- | ------------------------------------------------------------------------------------------------------------------------ |
| Auto      | Interne Daten (CAN-Nachrichten zwischen Motor und Getriebe, Zündzeitpunkte, etc.) sind für mich nicht interessant | Kleinwagen und Kombis sind lediglich spezielle Arten von Autos | Ein Maserati Quattroporte und ein C Corsa können beide fahren. Trotzdem sind die Auswirkungen verschieden                |
| Säugetier | Die genauen Vorgänge der Verdauung interessieren nicht. Nur das benötigte Futter ist wichtig                      | Eine Springmaus und eine Hausratte sind beide aus der Ordnung der Nagetiere. Beide Tiere teilen viele gleiche Eigenschaften                                                           | Fortbewegen ist eine Eigenschaft jedes Säugetieres. Ein Wal schwimmt jedoch und ein Känguru hüpft. Ein Pferd galoppiert. |


### Begriffe

| Begriff     | Bedeutung                                                                                                                                                                                                                                                                                                                                                                    |
| ----------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| Objekt      | Ein Element, welches Funktionen, Methoden, Prozeduren, einen inneren Zustand, oder mehrere dieser Dinge besitzt.                                                                                                                                                                                                                                                             |
| Entität     | Ein Objekt, welches eine Identität besitzt, welche unveränderlich ist. Beispielsweise kann eine Person ihre Adresse, Telefonnummer oder Namen ändern, ohne zu einer anderen Person zu werden. Eine Person ist also eine Entität.                                                                                                                                             |
| Wertobjekt  | Ein Objekt, welches über seinen Wert definiert wird. Eine Telefonnummer, welche sich ändert, ist also eine andere Telefonnummer. Gleichartig ist eine Adresse, bei der sich lediglich die Hausnummer ändert, eine andere Adresse, selbst wenn alle anderen Daten gleich bleiben. Somit stellt eine Telefonnummer und eine Adresse keine Entität dar, sondern ein Wertobjekt. |
| Eigenschaft | Ein Bestandteil des Zustands eines Objekts. Hierbei kann es sich um eine Entität oder ein Wertobjekt handeln.                                                                                                                                                                                                                                                                |
| Prozedur    | Verändert den Zustand eines Objektes, ohne einen Rückgabewert zu liefern. Eine Prozedur kann andere Objekte als Parameter entgegennehmen.                                                                                                                                                                                                                                    |
| Funktion    | Ordnet einer gegebenen Eingabe einen bestimmten Rückgabewert zu. Eine Funktion zeichnet sich insbesondere dadurch aus, dass sie nicht den Zustand eines Objekts verändert.                                                                                                                                                                                                   |
| Methode     | Verändert den Zustand eines Objekts und liefert zudem einen Rückgabewert. Eine Methode kann andere Objekte als Parameter entgegennehmen.                                                                                                                                                                                                                                     |
| Modul       | Eine zusammengefasste Gruppe von Objekten.                                                                                                                                                                                                                                                                                                                                   | [Ghosh]


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

Das Liskovsche Substitutionsprinzip (LSP) oder Ersetzbarkeitsprinzip besagt, dass ein Programm, das Objekte einer Basisklasse T verwendet, auch mit Objekten der davon abgeleiteten Klasse S korrekt funktionieren muss, ohne dabei das Programm zu verändern:

*"Sei $q(x)$ eine beweisbare Eigenschaft von Objekten $x$ des Typs $T$. Dann soll $q(y)$ für Objekte $y$ des Typs $S$ wahr sein, wobei $S$bein Untertyp von $T$ ist.“* [2]

Beispiel: Grafische Darstellung von verschiedenen Primitiven

![Liskov](/img/06_OOPI/LiskovUML.jpg)<!-- width="60%" --> [WikiLiskov](#7)

Beispiel: Vögel / Flugunfähgige Vögel


### Interface Segregation Prinzip





## 5. Beispiel der Woche ...

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
