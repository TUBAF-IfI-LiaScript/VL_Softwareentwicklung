<!--

author:   Sebastian Zug & André Dietrich
email:    zug@ovgu.de   & andre.dietrich@ovgu.de
version:  0.0.1
language: de
narrator: Deutsch Female

import: https://raw.githubusercontent.com/LiaTemplates/Rextester/master/README.md
import: https://raw.githubusercontent.com/LiaTemplates/WebDev/master/README.md
-->

# Vorlesung Softwareentwicklung - 23 - Tasks und asynchroner Code

--------------------------------------------------------------------
Link auf die aktuelle Vorlesung im Versionsmanagementsystem GitHub

https://github.com/liaScript/CsharpCourse/blob/master/23_Tasks.md

Die interaktive Form ist unter diese Link zu finden ->
[LiaScript Vorlesung 23](https://liascript.github.io/course/?https://raw.githubusercontent.com/liaScript/CsharpCourse/master/23_Tasks.md#1)

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

*1. Hier stehen jetzt Ihre Fragen ...*

## Wiederholung

Was war noch mal ein Delegat?

Und ein Action-Delegate? Action-Delegates bilden einen generische Delegaten, die
über n Parameter verfügt aber keinen Wert zurückgibt. Sie können diesen
Delegaten verwenden, um eine Methode als Parameter übergeben werden, ohne
explizit einen benutzerdefinierten Delegaten zu deklarieren.

Das folgende Beispiel nutzt einen konventionellen Delegaten `Output` (definiert
in Zeile 8), einen generischen Action-Delegaten und einen Func-Delegaten um
Funktionspointer für verschiedene Methoden zu erzeugen. Beachten Sie dabei, dass
die Funktion `Square()` überladen ist, um das automatische Mapping durch den
Compiler zu unterstreichen.

```csharp           ActionUndFunc
using System;
using System.Reflection;
using System.Collections.Generic;

namespace Rextester
{

    public delegate void Output(string text);

    public class Program{

      static void PrintHello(string text){
          Console.WriteLine(text);
      }

      static int Square(int x){
          Console.WriteLine("This is method Square(int x)");
          return x*x;
      }

      static float Square(float x){
          Console.WriteLine("This is method Square(float x)");
          return x*x;
      }

      public static void Main(string[] args){
         Output myOutput = PrintHello;
         myOutput("Das ist eine Textausgabe");

         Action<string> myActionOutput = PrintHello;
         myActionOutput("Das ist eine Action-Testausgabe!");

         Func<float, float> myFuncOutput = Square;
         Console.WriteLine(myFuncOutput(5));
      }
    }
}
```
@Rextester.eval(@CSharp)

Alternativ lassen sich auch anonyme Funktionen (Lambda-Funktionen) für die
Erzeugung eines Delegaten verwenden:

Ein Lambdaausdruck mit einem Ausdruck auf der rechten Seite des `=>`-Operators
wird als Ausdruckslambda bezeichnet. Ein Ausdruckslambda gibt das
Ergebnis des Ausdrucks zurück und hat folgende grundlegende Form:

```csharp
// generelle Struktur
// (input-parameters) => expression
Action line = () => Console.WriteLine();
Func<int, int, bool> testForEquality = (x, y) => x == y;
```

Damit lassen sich die Funktionalität und die Initialisierung des generischen
Delegaten in einem Ausdruck kombinieren. Für die Konfiguration eines Threads
bedeutet dies:

```csharp           DifferentKindsOfThreads
using System;
using System.Reflection;
using System.Threading;

// In System.Threading sind zwei Delegaten definiert:
// public delegate void ThreadStart();
// public delegate void ParameterizedThreadStart(object param);
// https://docs.microsoft.com/de-de/dotnet/api/system.threading.parameterizedthreadstart?view=netframework-4.8

namespace Rextester
{
    public class Program{

      static void PrintHello(){
          Console.WriteLine("PrintHello ohne Parameter");
      }

      static void PrintHello(object output){
          Console.WriteLine("PrintHello mit einem Object-Parameter - {0}", output);
      }

      static void PrintHello(string output, int number){
          Console.WriteLine("PrintHello mit einem string - {0} und eine int-Parameter", output, number);
      }

      public static void Main(string[] args){
         //Method with no parameter - ThreadStart Delegate
         ThreadStart dlgA = PrintHello;
         Thread ThreadA = new Thread(dlgA);
         ThreadA.Start();

         //Method with a parameter - ParameterizedThreadStart  Delegate
         ParameterizedThreadStart dlgB = PrintHello;
         Thread ThreadB = new Thread(dlgB);
         ThreadB.Start("12345");

         //lambda expression for smaller thread methods
         new Thread (() =>
         {
            Console.WriteLine("Lambda Funktion im Thread ausgeführt");
         }).Start();

         //lambda expression for more than one parameter
         new Thread(() => PrintHello("ABCD", 123)).Start();

         //anonymous methods C# 2.0
         new Thread (delegate()
         {
           Console.WriteLine ("Anonyme Methode im Thread ausgeführt");
         }).Start();
      }
    }
}
```
@Rextester.eval(@CSharp)

## Motivation

Die Prozdedurale Programmierung basiert auf der Idee, dass ausgehend von einem
Hauptprogramm Methoden aufgerufen werden, deren Abarbeitung realisiert wird und
danach zum Hauptprogramm zurückgekehrt wird.

```csharp           Synchronous
using System;
using System.Threading;

namespace Rextester
{
    class Program {

      public static void TransmitsMessage(string output){
        Random rnd = new Random();
        Thread.Sleep(1);
        Console.WriteLine(output);
      }

      public static void Main(string[] args){
        TransmitsMessage("Here we are");
        TransmitsMessage("Best wishes from Freiberg");
        TransmitsMessage("Nice to meet you");
      }  
    }
}
```
@Rextester.eval(@CSharp)

An dieser Stelle spricht man von **synchronen** Methodenaufrufen. Das
Hauptprogramm (Rufer oder Caller) stoppt, wartet auf den Abschluss des
aufgerufenen Programms und setzt seine  Bearbeitung erst dann fort. Das
blockierende Verhalten des Rufers generiert aber einen entscheidenden Nachteil -
eine fehlende Reaktionsfähigkeit für die Zeit, in der die aufgerufene Methode
zum Beispiel eine Netzwerkverbindung aufbaut, Daten speichert oder Berechnungen
realisiert.

Der Rufer könnte in dieser Zeit auch andere Arbeiten umsetzen. Dafür muss er aber
nach dem Methodenaufruf die Kontrolle zurück bekommen und kann dann weiterarbeiten.

Ein Beispiel aus der "Praxis" - Vorbereitung eines Frühstücks:

1. Schenken Sie sich eine Tasse Kaffee ein.
2. Erhitzen Sie eine Pfanne, und braten Sie darin zwei Eier.
3. Braten Sie drei Scheiben Frühstücksspeck.
4. Toasten Sie zwei Scheiben Brot.
5. Bestreichen Sie das getoastete Brot mit Butter und Marmelade.
6. Schenken Sie sich ein Glas Orangensaft ein.

Das anschauliche Beispiel entstammt der Microsoft Dokumentation und ist unter
https://docs.microsoft.com/de-de/dotnet/csharp/programming-guide/concepts/async/
zu finden.

Eine Lösung für diesen Ansatz könnten Threads bieten.

```csharp           AsynchonousBehaviour
using System;
using System.Threading;

namespace Rextester
{
    class Program {

      static public int[] Result = { 0, 0, 0};

      public static void TransmitsMessage(object index){
        Random rnd = new Random();
        Console.WriteLine("Thread {0} started!", Thread.CurrentThread.ManagedThreadId);
        // doing some fancy things here
        Thread.Sleep(rnd.Next(200, 500));  // arbitrary duration
        Result[(int)index]= 99;
        Console.WriteLine("Thread {0} says Hello", Thread.CurrentThread.ManagedThreadId);
      }

      public static void Main(string[] args){
        Thread ThreadA = new Thread (TransmitsMessage);
        ThreadA.Start(0);
        Thread ThreadB = new Thread (TransmitsMessage);
        ThreadB.Start(1);
        Thread ThreadC = new Thread (TransmitsMessage);
        ThreadC.Start(2);

        for (int i = 0; i<100; i++){
           Console.Write("*");
           Thread.Sleep(1);
        }
        Console.WriteLine();
        Console.WriteLine("Well done, so far!");

        ThreadA.Join();
        ThreadB.Join();
        ThreadC.Join();
        Console.WriteLine("Aus die Maus!");

        foreach(int i in Result){
          Console.Write("{0} ", i);
        }
      }
   }
}
```
@Rextester.eval(@CSharp)

Welche Nachteile sehen Sie in dieser Lösung?

## Task Modell in C#

                              {{0-1}}
********************************************************************************

C# stellt für die asynchrone Programmierung einen neuen Typen `Task` zur
Verfügung und für die `await` und `async` Keywörter ein.

Die Task-Klasse bildet einen einzelnen Vorgang ab, gibt keinen Wert zurück und
wird in der Regel asynchron ausgeführt. Task Objekte sind eine der zentralen
Komponenten von der aufgabenbasierte asynchrone Muster in .NET Framework 4
eingeführt wurden. Da die Arbeit, indem geleistet eine Task Objekt in der Regel
führt asynchron auf einem Threadpool-Thread anstatt synchron auf dem
Hauptanwendungsthread, Sie verwenden können der Status -Eigenschaft, als auch
die IsCanceled, IsCompleted, und IsFaulted Eigenschaften, um den Status eines
Vorgangs zu bestimmen. In den meisten Fällen wird ein Lambda-Ausdruck verwendet,
um die eigentliche Aufgabe zu spezifizieren.

```csharp           TaskClasses
public class Task{
  public Task (Action a);
  public TaskStatus Status {get;}
  public bool IsCompleted {get;}
  public static Task Run(Action a);
  public static Task Delay(int n);
  public void Wait();
  ...
}

public class Task<T>: Task{
  public Task (Func<T> f);
  ...
  public static Task<T> Run (Func <T> f);
  ...
}
```

Die Anwendung erfolgt dabei dem Muster:

```csharp           
Task task = new Task(() => {... Anweisungsblock ...});
Task.Start();
```

Hierbei wird deutlich, dass das `Task`-Objekt auf einem `Thread` aufbaut und  
lediglich eine höhere Abstraktionsstufe darstellt. Der verkürzte Aufruf mittels
der statischen `Run`-Methode realisiert das gleiche Verhalten:

```csharp           
Task task = Task.Run(() => {... Anweisungsblock ...});
```

********************************************************************************
                                       {{1-2}}
********************************************************************************

Es wäre nun möglich diesen laufenden Task aus dem Main-Thread anhand seiner
Variablen `IsCompleted` zu überwachen und die Fertigstellung zu erkennen. Dieses
Verhalten lässt sich zwar auch mit Threads umsetzen, mit dem höheren Abstraktionsgrad lässt sich die Komplexität des Aufrufes aber reduzieren.

Um für die Durchführung einer einzelnen Aufgabe zu warten, rufen Sie die Task. `Wait` Methode. Ein Aufruf der Wait Methode blockiert den aufrufenden Thread, bis die Instanz der Klasse die Ausführung abgeschlossen hat.

```csharp           TaskDefinition1
// https://docs.microsoft.com/de-de/dotnet/api/system.threading.tasks.task?view=netframework-4.8
using System;
using System.Threading.Tasks;

public class Example
{
   public static void Main()
   {
      Action<object> action = (object obj) =>
                                    {
                                       Console.WriteLine("Task={0}, obj={1}, Thread={2}",
                                       Task.CurrentId, obj,
                                       Thread.CurrentThread.ManagedThreadId);
                                    };

      // Create a task but do not start it.
      Task t1 = new Task(action, "alpha");
      t1.Start();
      Console.WriteLine("t1 has been launched. (Main Thread={0})",
                                Thread.CurrentThread.ManagedThreadId);


      Task t2 = Task.Run( () => {
                                  // Just loop.
                                  int ctr = 0;
                                  for (ctr = 0; ctr <= 1000000; ctr++)
                                  {}
                                  Console.WriteLine("Finished {0} loop iterations",
                                                    ctr);
                               } );
      t2.Wait();
   }
}
// The example displays the following output:
//        Finished 1000001 loop iterations
```

`Wait` ermöglicht auch die Beschränkung der Wartezeit auf ein bestimmtes Zeitintervall.Die `Wait(Int32)` und `Wait(TimeSpan)` Methoden blockiert den aufrufenden Thread, bis die Aufgabe abgeschlossen ist oder ein Timeoutintervall abläuft, welcher Fall zuerst eintritt.

```csharp           WaitForNTimeSlots
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Rextester
{
    class Program {
      public static void Main(string[] args){
        // Wait on a single task with a timeout specified.
        Task taskA = Task.Run( () => Thread.Sleep(2000));
        try {
          taskA.Wait(1000);       // Wait for 1 second.
          bool completed = taskA.IsCompleted;
          Console.WriteLine("Task A completed: {0}, Status: {1}",
                           completed, taskA.Status);
          if (! completed)
             Console.WriteLine("Timed out before task A completed.");                 
         }
         catch (AggregateException) {
            Console.WriteLine("Exception in taskA.");
         }  
      }
   }
}
```
@Rextester.eval(@CSharp)

Für komplexe Taskstrukturen kann man diese zum Beispiel in Arrays arrangieren.
Für diese Reihe von Aufgaben jeweils durch Aufrufen der `Wait` Methode zu warten
wäre aufwändig und wenig praktisch. `WaitAll` schließt diese Lücke und erlaubt
eine übergreifende Überwachung.

Das folgenden Beispiel werden zehn Aufgaben erstellt, die wartet, bis alle zehn abgeschlossen werden, und klicken Sie dann ihren Status angezeigt.

```csharp           WaitForAll
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Rextester
{
    class Program {
      public static void Main(string[] args){
        // Wait for all tasks to complete.
        Task[] tasks = new Task[10];
        for (int i = 0; i < 10; i++)
        {
            tasks[i] = Task.Run(() => Thread.Sleep(2000));
        }
        try {
           Task.WaitAll(tasks);
        }
        catch (AggregateException ae) {
           Console.WriteLine("One or more exceptions occurred: ");
           foreach (var ex in ae.Flatten().InnerExceptions)
              Console.WriteLine("   {0}", ex.Message);
        }   

        Console.WriteLine("Status of completed tasks:");
        foreach (var t in tasks)
           Console.WriteLine("   Task #{0}: {1}", t.Id, t.Status);
        }
   }
}
```
@Rextester.eval(@CSharp)

********************************************************************************

                                      {{2-3}}
********************************************************************************

Der Kanon der Möglichkeiten wird aber deutlich erweitert, wenn ein konkreter
Rückgabewert genutzt werden soll. Anstatt wie bei Threads mit einer
entsprechenden  "außenstehenden" Variablen zu arbeiten, wird das Ergebnis im
`Task`-Objekt selbst  gespeichert und kann dann abgerufen werden. Dieser Aspekt wird über die generische Konfiguration des Tasks abgebildet:

```csharp           
Task<int> task = Task.Run(() => {int i;
                                 //... Anweisungsblock ...;
                                 return i});
```

Wie ist dieser Aufruf zu verstehen? Unser Task gibt anders als bei der synchronen
Abarbeitung nicht unmittelbar mit dem Ende der Bearbeitung einen Wert zurück, sondern
verspricht zu einem späteren Zeitpunkt einen Wert in einem bestimmten Format zu liefern. Dank der generischen Realisierung können dies beliebige Objekte sein.

Wie aber erfolgt die Rückgabe wann?

********************************************************************************

## Asynchrone Methoden

Eine asynchrone Methode ruft einen Task auf, setzt die eigene Bearbeitung aber
fort und wartet auf dessen Beendigung.

```csharp           
aync void DoAsync(){
  Task<int> task = Task.Run(() => {int i;
                                   // Berechnungen
                                   return i;})
  // Methoden, die nach der Rückkehr nach DoAsync ausgeführt werden.
  int result = await task;
  // Hier wird nun mit dem Ergebnis result weitergearbeitet
}
```

Das Ergebnis der Operation hängt dabei davon ab, welche Zeitabläufe sich im
Programmablauf ergeben.

## Anhang

**Referenzen**


**Autoren**

Sebastian Zug, André Dietrich
