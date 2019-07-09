<!--
author:   Sebastian Zug & André Dietrich
email:    zug@ovgu.de   & andre.dietrich@ovgu.de
version:  0.0.1
language: de
narrator: Deutsch Female

import: https://raw.githubusercontent.com/LiaTemplates/Rextester/master/README.md
import: https://raw.githubusercontent.com/LiaTemplates/WebDev/master/README.md
import: https://raw.githubusercontent.com/liaTemplates/AlaSQL/master/README.md

script:  https://cdnjs.cloudflare.com/ajax/libs/echarts/4.1.0/echarts-en.min.js
-->


# Vorlesung Softwareentwicklung - 26 - Design Patterns

--------------------------------------------------------------------
Link auf die aktuelle Vorlesung im Versionsmanagementsystem GitHub

https://github.com/liaScript/CsharpCourse/blob/master/26_DesignPattern.md

Die interaktive Form ist unter diese Link zu finden ->
[LiaScript Vorlesung 26](https://liascript.github.io/course/?https://raw.githubusercontent.com/liaScript/CsharpCourse/master/26_DesignPattern.md#1)

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

*1. Welche Bedeutung hat der Zugriffsmodifizierer `internal`?*

*2. Welche Gemeinsamkeiten und welche Unterschiede Weisen Klassen und Strukturen in C# auf?*

*3. Was verbirgt sich hinter dem Konzept des "Indexers"*

---------------------------------------------------------------------

## Wiederholung - Polymorphie

Polymorphie ist ein Konzept in der objektorientierten Programmierung, das
ermöglicht, dass ein Bezeichner abhängig von seiner Verwendung Objekte
unterschiedlichen Datentyps annimmt. Wir mappen die Instanzen abgeleiteter
Klassen auf eine Basisklasse, um diese in einer Collection erfassen zu können.
Entsprechend verhalten sich dann namensgleiche  Methoden unterschiedlich.

Dieser Mechanismus wird beim sogenannten Boxing und Unboxing deutlich:

```csharp    Polymorphie
using System;

namespace Rextester
{
  public class Shape{
      protected int m_xpos;
      protected int m_ypos;
      public Shape(){}
      public Shape(int x, int y){
          m_xpos = x;
          m_ypos = y;
      }
      public virtual void Draw(){
          Console.WriteLine("Drawing a SHAPE at {0},{1}", m_xpos, m_ypos);
      }

      public override string ToString(){
          return String.Format("Shape at [x,y] {0},{1}", m_xpos, m_ypos);
      }
  }

  public class Program {
    public static void Main(string[] args){
      int i = 123;
      object o = i;     
      //object o = (object)i;  // identisches explizites boxing

      //+-- Statischer Typ von obj
      //|                +-- Dynamischer Typ von obj
      //|                |
      Object obj = new Shape();

      if (obj is Object) Console.WriteLine("Objekt Typ");
      if (obj is Shape) Console.WriteLine("Shape Typ");
    }
  }
}
```
@Rextester.eval(@CSharp)

> Merke: Boxing und Unboxing sind Cast-Operationen die eine Variable in eine Instanz von `object` konvertieren und umgekehrt.

*Wozu brauche ich das?*

Anwendungsbeispiel:

![Collections](http://www.plantuml.com/plantuml/png/fP0nJmCn38Lt_mfBBAsW4dC7L0GBy_q0vEQC6f7a3iU1bkF-EoGMugGRcVByylG-ouUi95fYW9Fl6PqN5nZogqyZ2KLqWNA-LnL_BCfFRaYT_sIy1MW_s9rev0dm2i_Fuv3tw9FMwRAOhYsrsVx97n_FDrYVIxCdEcOLSZhZez37Phl7zy7tCF-27jfcKysQj3hJwxvbISjHS3LRuWiB9yThGlTMI8nWqWYc_qcmDN6t-xgp2u3LBXquSEfB3Dy0)<!-- size="350px" -->

In folgendem Beispiel wurde allein die Klasse `Circle` implementiert.

```csharp    Polymorphie
using System;
using System.Collections.Generic;

namespace Rextester
{
  public class Shape{
      protected int m_xpos;
      protected int m_ypos;
      public Shape(){}
      public Shape(int x, int y){
          m_xpos = x;
          m_ypos = y;
      }
      public virtual void Draw(){
          Console.WriteLine("Drawing a SHAPE at {0},{1}", m_xpos, m_ypos);
      }

      public override string ToString(){
          return String.Format("Shape at [x,y] {0},{1}", m_xpos, m_ypos);
      }
  }

  public class Square : Shape{
      public Square(){}
      public Square(int x, int y) : base(x, y) {}
      public override void Draw()
      {
        Console.WriteLine("Drawing a SQUARE at {0},{1}", m_xpos, m_ypos);
      }

      public void CalcCircumference(){
        Console.WriteLine("Circumference of SQUARE {0}", 2 * m_xpos * m_ypos);
      }      
   }

  public class Program {
    public static void Main(string[] args){
      Shape sh = new Shape(100, 100);
      sh.Draw();

      Square sq = new Square(200, 200);
      sq.Draw();  
    }
  }
}
```
@Rextester.eval(@CSharp)

Haben wir bis hierher irgendeine Ausprägung von Polymorphie gesehen? Nein, bisher nutzen wir nur einzelne Vererbungstechniken.

Bevor wir uns aber die Polymorphie anschauen, noch eine Frage:
Erklären Sie die Bedeutung der Schlüsselworte `virtual` und `override`.

                                    {{0-1}}
*******************************************************************************

|                                          | **Virtuelle Member**           |
| Schlüsselwort                            | `virtual`                   |
| Implementierung in der Basisklasse       | muss implementiert werden   |
| Überschreiben in der abgeleiteten Klasse |  `override` |

In Bezug auf die Polymorphie bestimmen  die Schlüsselworte `new` und `override`
das Verhalten:

+ `override` realisiert eine "angepasste" Implementierung der Methode der Basisklasse
+ `new` implmentiert eine völlig neue Methode, die keinen Bezug mehr zur Basisklassenfunktion hat

*******************************************************************************
                                    {{1-3}}
*******************************************************************************

|                                          | **Virtuelle Member**          | **Abstrakte Member**          |
| Schlüsselwort                            | `virtual`                 | `abstract`                |
| Implementierung in der Basisklasse       | muss implementiert werden | keine Implementierung     |
| Überschreiben in der abgeleiteten Klasse | `override`                | muss überschrieben werden |

Beide Konzepte können auf Methoden (hier vorrangig betrachtet), Eigenschaften, Ereignisse und Indexer angewandt werden

*******************************************************************************

                                    {{2-3}}
*******************************************************************************

Und wie war das noch mal mit den abstrakten Klassen, wie hängt deren Konzept mit
abstrakten Elementen zusammen?

+ Sie können keine Instanzen einer abstrakten Klasse erstellen.
+ Eine abstrakte Klasse kann abstrakte oder normale, nicht abstrakte Mitglieder enthalten.
+ Eine abstrakte Klasse kann selbst von einer anderen abstrakten Klasse abgeleitet werden
+ Jede von einer abstrakten Klasse abgeleitete Klasse muss alle abstrakten Mitglieder der Klasse mithilfe des Schlüsselworts override implementieren, es sei denn, die abgeleitete Klasse ist selbst abstrakt.

*******************************************************************************

## Design Pattern

Design Pattern sind spezielle Muster für Interaktionen und Zusammenhänge  der
Bestandteile einer Softwarelösung. Sie präsentieren Implementierungsmodelle, die
für häufig wiederkehrende Abläufe (Generierung und Maskierung von Objekten) eine
flexible und gut wartbare Realisierung sicherstellen. Dafür werden die  Abläufe
abstrahiert und auf generisch Anwendbare Muster reduziert, die dann mit
domänenspezifische Bezeichnern versehen nicht nur für die vereinfachte Umsetzung
sondern auch für die Kommunikation dazu genutzt werden. Dies vereinfacht die
Interaktion zwischen Softwarearchitekten, Programmierer und andere
Projektmitglieder.

> Design Pattern sind Strukturen, Modelle, Schablonen und Muster, die sich zur Entwicklung stabiler Softwaremodelle nutzen lassen.

Entwurfsmuster für Software orientieren sich eng an den grundlegenden Prinzipien der objektorientierten Programmierung:

+ Vererbung
+ Kapselung
+ Polymorphie

Dabei sollte ein Muster:

+ ein oder mehrere Probleme lösen,
+ die Lesbarkeit und Wartbarkeit des Codes erhöhen
+ auf die Nutzung sprachspezifischer Feature verzichten, um eine Übertragbarkeit sicherzustellen
+ ein eindeutiges Set von Begriffen definieren
+ Denkanstöße für den eigenen Entwurf liefern

### Kategorien

In welchen Kategorien werden Design Pattern üblicherweise strukturiert:

1. Erzeugungsmuster (englisch creational patterns)

    Dienen der Erzeugung von Objekten. Sie entkoppeln die Konstruktion eines Objekts von seiner Repräsentation. Die Objekterzeugung wird gekapselt und ausgelagert, um den Kontext der Objekterzeugung unabhängig von der konkreten Implementierung zu halten, gemäß der Regel: „Programmiere auf die Schnittstelle, nicht auf die Implementierung!“

2. Strukturmuster (englisch structural patterns)

    Erleichtern den Entwurf von Software durch vorgefertigte Schablonen für Beziehungen zwischen Klassen.

3. Verhaltensmuster (englisch behavioral patterns)

    Modellieren komplexes Verhalten der Software und erhöhen damit die Flexibilität der Software hinsichtlich ihres Verhaltens.

> ACHTUNG: Entwurfsmuster sind keine Wunderwaffe und kein Garant für gutes Design! Möglichst viele Design Pattern zu nutzen verbaut mitunter den Blick auf elegantere Lösungen.


### Erzeugungsmuster - Singleton Pattern

                                         {{0-1}}
********************************************************************************

Das Singleton  ist ein in der Softwareentwicklung eingesetztes Entwurfsmuster
und gehört zur Kategorie der Erzeugungsmuster. Es stellt sicher, dass von einer
Klasse genau ein Objekt existiert. Dieses Singleton ist darüber hinaus
üblicherweise global verfügbar. Es soll sicher gestellt werden, dass ein
Resourcenzugriff kanalisiert wird.

![Singleton](http://www.plantuml.com/plantuml/png/JOwn3i9034FtV8N7LWY9cQcCdVi5HngLY6kABkb2rN_dX8JAQFlPSkUHIgnpfeUE0jR2MSYVQgzKqWpEoVqMKVI-XlIysA1lmONecs1GcxB4OXlXZAskXV8E_zdNWwZ08PgMSC8aqLlj64lJ_gCxKISsrbyV)<!-- width="220px" -->

**Beispiel**

Ausgangspunkt der Überlegungen ist die Implementierung einer Klasse
`PrinterDriver`. Über die entsprechenden Hashcodes kann gezeigt werden, dass es
sich um unterschiedliche Instanzen der Klasse handelt.

Welche Möglichkeiten sehen Sie diese Implementierung anzupassen, so dass das
Singleton-Pattern realisiert wird?

```csharp    SingletonPatternStart
using System;

namespace Rextester
{
  public class PrinterDriver{
    public void print(string text){
       Console.WriteLine("!PRINT {0}", text);
    }
  }

  public class Program {
    public static void Main(string[] args){
      PrinterDriver MyPrinter = new PrinterDriver();
      PrinterDriver FaultyPrinterInstance = new PrinterDriver();
      Console.WriteLine(MyPrinter.GetHashCode());
      Console.WriteLine(FaultyPrinterInstance.GetHashCode());
    }
  }
}
```
@Rextester.eval(@CSharp)

Offenbar kann der Druckertreiber mehrfach instantiiert werden. Welche Möglichkeiten sehen sie?

********************************************************************************

                                     {{1-3}}
********************************************************************************

** Variante 1**

> ACHTUNG: Auf den ersten Blick mag die folgende Lösung plausibel erscheinen, sie hat aber einen zentralen Markel! Welche Einschränkung sehen Sie?

```csharp    SingletonPatternStart
using System;

namespace Rextester
{
  public class PrinterDriver{
  private PrinterDriver(){}
  private static PrinterDriver printerDriverInstance;

  public static PrinterDriver getInstance(){
      if (printerDriverInstance == null){
          printerDriverInstance = new PrinterDriver();
      }
      return printerDriverInstance;
  }
  public void print(string text){
     Console.WriteLine("!PRINT {0}", text);
  }
}

public class Program {
  public static void Main(string[] args){
    PrinterDriver MyPrinter = PrinterDriver.getInstance();
    PrinterDriver FaultyPrinterInstance = PrinterDriver.getInstance();
    Console.WriteLine(MyPrinter.GetHashCode());
    Console.WriteLine(FaultyPrinterInstance.GetHashCode());
  }
}
}
```
@Rextester.eval(@CSharp)

Von *Lazy Creation* spricht man, wenn das einzige Objekt der Klasse erst erzeugt
wird, wenn es benötigt wird. Ziel ist, dass der Speicherbedarf und die
Rechenzeit für die Instanziierung des Objektes nur dann aufgewendet werden, wenn
das Objekt wirklich benötigt wird. Hierzu wird der Konstruktor ausschließlich
beim ersten Aufruf der Funktion getInstance() aufgerufen.

********************************************************************************

                                    {{2-3}}
********************************************************************************

Unter Rextester gelingt es leider nicht die Threads so zu konfigurieren, dass
mehrere Instanzen der Klasse entstehen. Wenn Sie aber den nachfolgenden Code
in Ihre Entwicklungsumgebung kopieren, können Sie den Effekt gut beobachten.

```csharp    SingletonPatternWithoutThreadSafety
using System;
using System.Threading;

namespace Rextester
{
  public sealed class PrinterDriver{
    private PrinterDriver(){}
    private static PrinterDriver printerDriverInstance;

    public static int InstanceCount = 0;

    public static PrinterDriver getInstance(){
        Thread.Sleep(10);
        if (printerDriverInstance == null){
            printerDriverInstance = new PrinterDriver();
            InstanceCount ++;
            System.Console.WriteLine("New Driver instantiated!");
        }
        return printerDriverInstance;
    }
    public void print(string text){
       Console.WriteLine("!PRINT {0}", text);
    }
  }

  public class Program {
    public static void CheckInitialization() {
        PrinterDriver localInstance = PrinterDriver.getInstance();
    }

    public static void Main(string[] args){
      for (int i = 0; i < 10; i++){
          new Thread(CheckInitialization).Start();
      }
      Thread.Sleep(1000);
      Console.WriteLine("{0} Instances of PrinterDriver established!", arg0: PrinterDriver.InstanceCount);
    }
  }
}
```
@Rextester.eval(@CSharp)

Welche Lösung sehen Sie?

********************************************************************************

                                    {{3-4}}
********************************************************************************

Als Lösungsansatz können die Synchronisationsmethoden aus der Laufzeitumgebung
nutzen. `lock` garantiert, dass lediglich ein Thread einen bestimmten
Codeabschnitt betreten hat und blockiert alle anderen. Eine mögliche Lösung
könnte wie folgt aussehen:

```csharp    SingletonPatternWithoutThreadSafety
using System;
using System.Threading;

namespace Rextester
{
  public sealed class PrinterDriver{
    private PrinterDriver(){}
    private static PrinterDriver printerDriverInstance;
    // Zusätzliches Feld "padlock"
    private static readonly object padlock = new object();

    public static int InstanceCount = 0;

    public static PrinterDriver getInstance(){
        Thread.Sleep(100);
        lock (padlock)
        {
           if (printerDriverInstance == null){
              printerDriverInstance = new PrinterDriver();
              InstanceCount ++;
              System.Console.WriteLine("New Driver instantiated!");
        }
        }
        return printerDriverInstance;
    }
    public void print(string text){
       Console.WriteLine("!PRINT {0}", text);
    }
  }

  public class Program {
    public static void CheckInitialization() {
        PrinterDriver localInstance = PrinterDriver.getInstance();
    }

    public static void Main(string[] args){
      for (int i = 0; i < 10; i++){
          new Thread(CheckInitialization).Start();
      }
      Thread.Sleep(1000);
      Console.WriteLine("{0} Instances of PrinterDriver established!", arg0: PrinterDriver.InstanceCount);
    }
  }
}
```
@Rextester.eval(@CSharp)

********************************************************************************

                                    {{4-5}}
********************************************************************************

Die einfachste Form der Realisierung kann aber mit `System.Lazy<T>` ab .NET 4 umgesetzt werden. Alles was man dabei braucht ist ein Delegate auf den Konstruktor
des Singletons.

```csharp    SingletonPatternWithoutThreadSafety
using System;

namespace Rextester
{
  public sealed class PrinterDriver{

      //private static readonly Lazy<PrinterDriver> lazy = new Lazy<PrinterDriver> (() => new PrinterDriver());
      private static readonly Lazy<PrinterDriver> lazy = new Lazy<PrinterDriver> (getInstance);

      static PrinterDriver getInstance(){
        return new PrinterDriver();
      }

      public static PrinterDriver Instance {
        get { return lazy.Value; }
      }

      private PrinterDriver(){}

      public void print(string text){
         Console.WriteLine("!PRINT {0}", text);
      }
  }

  public class Program {
    public static void Main(string[] args){
       PrinterDriver MyPrinter = PrinterDriver.Instance;
       PrinterDriver FaultyPrinterInstance = PrinterDriver.Instance;
       Console.WriteLine(MyPrinter.GetHashCode());
       Console.WriteLine(FaultyPrinterInstance.GetHashCode());
       MyPrinter.print("Singleton - Aus die Maus");
    }
  }
}
```
@Rextester.eval(@CSharp)



********************************************************************************

### Strukturmuster Adapter Pattern

Ausgangspunkt für das Beispiel ist die Notwendigkeit eine externes Buchungssystem mit einer Mitarbeiterdatenbank zu verknüpfen. Dabei sind Sie als Entwickler mit zwei Formen der Datenhaltung konfrontiert. Während Ihr Managementsystem für die Mitarbeiter `HRSystem` auf ein Array von strings setzt, erwartet das einzubindende Buchungssystem eine (generische) Liste von strings.  

```csharp
public class HRSystem{
   public string[][] GetEmployees(){
     string[][] employees = new string[4][];   
     employees[0] = new string[] { "100", "Deepak", "Team Leader" };
     employees[1] = new string[] { "101", "Rohit", "Developer" };
     employees[2] = new string[] { "102", "Gautam", "Developer" };
     employees[3] = new string[] { "103", "Dev", "Tester" };

     return employees;
   }
}

public class ThirdPartyBillingSystem
{
   ...
   public void ShowEmployeeList(){
     List<string> employee = employeeSource.GetEmployeeList();
     ...
   }
   ...
}
```

Der Adapter (englisch adapter pattern) – auch als Wrapper bezeichnet –
ist ein Entwurfsmuster das zur Übersetzung einer Schnittstelle in eine andere
dient. Dadurch wird die Kommunikation von Klassen mit zueinander inkompatiblen
Schnittstellen ermöglich.

![Adapter](./img/26_DesignPattern/ObjektAdapter.png)<!-- width="50%" -->  [WikipediaAdapter](#7)








```csharp    Adapter
// Das Beispiel ist motiviert durch den Code auf der Seite
// https://www.dotnettricks.com/learn/designpatterns/adapter-design-pattern-dotnet

using System;
using System.Collections.Generic;

namespace Rextester
{

  public interface ITarget{
    List<string> GetEmployeeList();
  }

  public class ThirdPartyBillingSystem
  {
     private ITarget employeeSource;

     public ThirdPartyBillingSystem(ITarget employeeSource){
       this.employeeSource = employeeSource;
     }

     public void ShowEmployeeList(){
       List<string> employee = employeeSource.GetEmployeeList();
       //To DO: Implement you business logic

       Console.WriteLine("######### Employee List ##########");
       foreach (var item in employee){
         Console.Write(item);
       }
     }
  }

  public class HRSystem{
     public string[][] GetEmployees(){
       string[][] employees = new string[4][];   
       employees[0] = new string[] { "100", "Deepak", "Team Leader" };
       employees[1] = new string[] { "101", "Rohit", "Developer" };
       employees[2] = new string[] { "102", "Gautam", "Developer" };
       employees[3] = new string[] { "103", "Dev", "Tester" };
       return employees;
     }
  }

  public class EmployeeAdapter : HRSystem, ITarget{
       public List<string> GetEmployeeList(){
         List<string> employeeList = new List<string>();
         string[][] employees = GetEmployees();
         foreach (string[] employee in employees)
         {
           employeeList.Add(employee[0]);
           employeeList.Add(",");
           employeeList.Add(employee[1]);
           employeeList.Add(",");
           employeeList.Add(employee[2]);
           employeeList.Add("\n");
         }

         return employeeList;
       }
    }

   public class Program {
      public static void Main(string[] args){
       ITarget Itarget = new EmployeeAdapter();
       ThirdPartyBillingSystem client = new ThirdPartyBillingSystem(Itarget);
       client.ShowEmployeeList();
     }
  }

}
```
@Rextester.eval(@CSharp)


### Erzeugungsmuster (Abstract) Factory Pattern





### Verhaltensmuster State Pattern


## Anhang

**Referenzen**

[WikipediaAdapter] Wikipedia "Entwurfsmuster Objektadapter", Autor jarling, https://commons.wikimedia.org/wiki/File:Objektadapter.svg

**Autoren**

Sebastian Zug, André Dietrich
