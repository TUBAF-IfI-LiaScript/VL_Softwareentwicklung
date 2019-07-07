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

*1. afdasdf*


---------------------------------------------------------------------

## Wiederholung

Boxing unboxing



## Design Pattern


### Singleton Pattern

Das Singleton  ist ein in der Softwareentwicklung eingesetztes Entwurfsmuster und gehört zur Kategorie der Erzeugungsmuster. Es stellt sicher, dass von einer Klasse genau ein Objekt existiert. Dieses Singleton ist darüber hinaus üblicherweise global verfügbar. Es soll sicher gestellt werden, dass ein
Resourcenzugriff kanalisiert wird.

Ausgangspunkt der Überlegungen ist die Implementierung einer Klasse PrinterDriver. Über die entsprechenden Hashcodes kann gezeigt werden, dass es
sich um unterschiedliche Instanzen der Klasse handelt.

Welche Möglichkeiten sehen Sie diese Implementierung anzupassen, so dass das Singleton-Pattern realisiert wird?

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


Von *Lazy Creation* spricht man, wenn das einzige Objekt der Klasse erst erzeugt
wird, wenn es benötigt wird. Ziel ist, dass der Speicherbedarf und die
Rechenzeit für die Instanziierung des Objektes nur dann aufgewendet werden, wenn
das Objekt wirklich benötigt wird. Hierzu wird der Konstruktor ausschließlich
beim ersten Aufruf der Funktion getInstance() aufgerufen.

ACHTUNG: Auf den ersten Blick mag die folgende Lösung plausibel erscheinen, sie
hat aber einen zentralen Markel! Welche Einschränkung sehen Sie?

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

```csharp    SingletonPatternWithoutThreadSafety
using System;
using System.Threading;

namespace Rextester
{

  public class PrinterDriver{
    private PrinterDriver(){}
    private static PrinterDriver printerDriverInstance;

    public static int InstanceCount = 0;

    public static PrinterDriver getInstance(){
        Thread.Sleep(100);
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

Als Lösungsansatz können die Synchronisationsmethoden aus der Laufzeitumgebung
nutzen. `lock` garantiert, dass lediglich ein Thread einen bestimmten Codeabschnitt betreten hat und blockiert alle anderen. Eine mögliche Lösung könnte wie folgt aussehen:

```csharp    SingletonPatternWithoutThreadSafety
using System;
using System.Threading;

namespace Rextester
{

  public class PrinterDriver{
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

### Plot Duration


<div class="persistent" id="chart" style="position: relative; width:100%; height:400px;"></div>

<script>
var chartOptions = {
  xAxis: [{
    type: 'value'
  }],
  yAxis: [{
    type: 'value'
  }],
  series: [{
    type: "line",
    data: [[0, 4], [1, 3], [2, 2], [3, 4], [4, 1], [5, 2]],
  }],
};
let chart = echarts.init(document.getElementById('chart'));
chart.setOption(chartOptions);
</script>


Integration der LAZY struktur


## Anhang

**Referenzen**


**Autoren**

Sebastian Zug, André Dietrich
