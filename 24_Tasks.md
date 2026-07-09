<!--

author:   Sebastian Zug, Galina Rudolf & André Dietrich
email:    sebastian.zug@informatik.tu-freiberg.de
version:  2.2.0
language: de
narrator: Deutsch Female
comment:  Weiterführende Abstraktionen für Multithreading, Task Modell in C#, asynchrone Methoden, koordinierte Tasks mit WhenAll, kooperativer Abbruch mit CancellationToken
tags:      
logo:     

import: https://github.com/liascript/CodeRunner

import: https://raw.githubusercontent.com/TUBAF-IfI-LiaScript/VL_Softwareentwicklung/master/config.md
        https://raw.githubusercontent.com/liascript-templates/plantUML/master/README.md

-->

[![LiaScript](https://raw.githubusercontent.com/LiaScript/LiaScript/master/badges/course.svg)](https://liascript.github.io/course/?https://github.com/TUBAF-IfI-LiaScript/VL_Softwareentwicklung/blob/master/24_Tasks.md)

# Tasks

| Parameter                | Kursinformationen                                                                     |
| ------------------------ | ------------------------------------------------------------------------------------- |
| **Veranstaltung:**       | `Vorlesung Softwareentwicklung`                                                       |
| **Teil:**                | `24/27`                                                                               |
| **Semester**             | @config.semester                                                                      |
| **Hochschule:**          | @config.university                                                                    |
| **Inhalte:**             | @comment                                                                              |
| **Link auf den GitHub:** | https://github.com/TUBAF-IfI-LiaScript/VL_Softwareentwicklung/blob/master/24_Tasks.md |
| **Autoren**              | @author                                                                               |

![](https://media.giphy.com/media/26tn33aiTi1jkl6H6/source.gif)

---------------------------------------------------------------------

## Rückblick: Von Threads zu einer offenen Frage

Die prozedurale/objektorientierte Programmierung basiert auf der Idee, dass ausgehend von einem
Hauptprogramm Methoden aufgerufen werden, deren Abarbeitung realisiert wird und
danach zum Hauptprogramm zurückgekehrt wird.

```csharp           SynchronOperation.cs
using System;
using System.Threading;

class Program
{
  public static void TransmitsMessage(string output){
    Thread.Sleep(1);
    Console.WriteLine(output);
  }

  public static void Main(string[] args){
    TransmitsMessage("Here we are");
    TransmitsMessage("Best wishes from Freiberg");
    TransmitsMessage("Nice to meet you");
  }
}
```
@LIA.eval(`["main.cs"]`, `mcs main.cs`, `mono main.exe`)

> An dieser Stelle spricht man von **synchronen** Methodenaufrufen. Das
Hauptprogramm (Rufer oder Caller) stoppt, wartet auf den Abschluss des
aufgerufenen Programms und setzt seine  Bearbeitung erst dann fort. **Synchron heißt blockierend**

Das
blockierende Verhalten des Rufers generiert aber einen entscheidenden Nachteil -
eine fehlende Reaktionsfähigkeit für die Zeit, in der die aufgerufene Methode
zum Beispiel eine Netzwerkverbindung aufbaut, Daten speichert oder Berechnungen
realisiert.

Der Rufer könnte in dieser Zeit auch andere Arbeiten umsetzen. Dafür muss er aber
nach dem Methodenaufruf die Kontrolle zurück bekommen und kann dann weiterarbeiten.

### Threads: die Kontrolle zurückbekommen

Mit Threads bekommt der Rufer die Kontrolle sofort zurück und kann weiterarbeiten,
während die Threads im Hintergrund laufen - abgewartet wird am Ende mit `Join`.

```csharp           Program.cs
using System;
using System.Threading;

class Program {
  static public int[] Result = { 0, 0, 0};
  static Random rnd = new Random();

  public static void TransmitsMessage(object index){
    Console.WriteLine("Thread {0} started!", Thread.CurrentThread.ManagedThreadId);
    // doing some fancy things here
    int delay = rnd.Next(200, 500);
    // int delay = Random.Shared.Next(200, 500);
    //static ThreadLocal<Random> rnd = new ThreadLocal<Random>(() => new Random());
    Thread.Sleep(delay);  // arbitrary duration
    Result[(int)index]= delay;
    Console.WriteLine("\nThread {0} says Hello", Thread.CurrentThread.ManagedThreadId);
  }

  public static void Main(string[] args){
    Thread ThreadA = new Thread (TransmitsMessage);
    ThreadA.Start(0);
    Thread ThreadB = new Thread (TransmitsMessage);
    ThreadB.Start(1);
    Thread ThreadC = new Thread (TransmitsMessage);
    ThreadC.Start(2);
    for (int i = 0; i<50; i++){
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
```
```-xml  AsynchonousBehaviour.csproj
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net8.0</TargetFramework>
  </PropertyGroup>
</Project>
```
@LIA.eval(`["Program.cs", "project.csproj"]`, `dotnet build -nologo`, `dotnet run -nologo`)

Im zeitlichen Ablauf sieht das so aus: `Main` startet A, B und C und läuft
*sofort* weiter (die `*`-Ausgabe), während die drei Threads unabhängig schlafen.
Erst die `Join`-Aufrufe lassen `Main` warten, bis alle fertig sind.

```text @plantUML
@startuml
participant Main
participant ThreadA
participant ThreadB
participant ThreadC

Main -> ThreadA : Start(0)
activate ThreadA
Main -> ThreadB : Start(1)
activate ThreadB
Main -> ThreadC : Start(2)
activate ThreadC

note over Main : Schleife: gibt "*" aus\n(läuft sofort weiter)
note over ThreadA, ThreadC : Sleep(delay)\n- alle drei warten -

ThreadA --> Main : Hello (Join)
deactivate ThreadA
ThreadB --> Main : Hello (Join)
deactivate ThreadB
ThreadC --> Main : Hello (Join)
deactivate ThreadC

note over Main : "Aus die Maus!"
@enduml
```

> Beachten Sie: `Main` und die drei Threads laufen **echt parallel** - deshalb
> mischen sich die `*` in der Ausgabe mit den "Hello"-Zeilen. Die drei Threads
> verbringen ihre Zeit aber fast vollständig in `Sleep`, also **wartend** - genau
> hier setzt gleich die Kritik an.

### Die Rechnung für Threads

Betrachten wir, was ein Thread in `TransmitsMessage` die meiste Zeit tut:
`Thread.Sleep(delay)` - er **schläft**. Ein vollwertiger Betriebssystem-Thread,
belegt und blockiert, nur um auf das Ablaufen einer Zeit zu warten. Bei drei
Aufgaben sind das drei blockierte Threads, die nichts tun. Das ist der Kern des
Problems, und es reiht sich in eine Liste von Nachteilen ein, die wir in der
Thread-Vorlesung gesehen haben:

+ **Teuer:** Jeder Thread belegt eigene Ressourcen (u. a. eigener Stack). Viele
  wartende Threads skalieren schlecht.
+ **Kein Rückgabewert:** Ergebnisse müssen über von außen sichtbare Variablen
  (hier das `Result`-Array) oder Callbacks zurückgereicht werden.
+ **Manuelle Verwaltung:** Erzeugen, Starten, `Join`-en liegt komplett in unserer
  Hand.
+ **Synchronisation nötig:** Sobald Threads geteilte Daten anfassen, drohen *Race
  Conditions*; der Schutz über `lock`/`Monitor` (das Fass-Beispiel aus VL 23) ist
  aufwändig und fehleranfällig.

> **Kern:** Threads geben uns Nebenläufigkeit, aber wir bezahlen mit Mechanik -
> und mit Threads, die beim Warten nur schlafen.

Was wäre, wenn wir nur noch sagen müssten, *was* nebenläufig laufen soll, nicht
*wie* - inklusive Rückgabewert, Abbruch, und mit einem Thread, der **während des
Wartens freigegeben** wird, statt zu schlafen?

> **Bild dafür:** ein Restaurant mit *einem* Kellner. Synchron bliebe er am Tisch
> stehen, bis die Küche fertig gekocht hat - ein Kellner pro Gast, absurd teuer.
> Stattdessen gibt er die Bestellung ab und bedient in der Wartezeit andere Tische.
> Genau das macht `await`: den Thread während des Wartens zurückgeben. Ein Kellner,
> viele Gäste.

Genau das ist das **Task-Modell**.

## Task Modell in C#

C# stellt für die asynchrone Programmierung die neuen Typen `Task`  und `Task<TResult>`und die Schlüsselwörter `await` und `async` zur
Verfügung. Diese sind zentrale Komponenten von den 
aufgabenbasierten asynchronen Muster (TAP - Task based Asynchronous Pattern), die in .NET Framework 4 eingeführt wurden.

| Aspekt                  | `Thread`                                                                                                   | `Task`                                                                                |
| ----------------------- | ---------------------------------------------------------------------------------------------------------- | ------------------------------------------------------------------------------------- |
| **Zweck**               | Repräsentiert einen physischen Ausführungspfad (Thread of Execution).                                      | Repräsentiert eine asynchrone, geplante Arbeitseinheit auf höherer Abstraktionsebene. |
| **Erstellung**          | Mit `new Thread()` explizit erstellt.                                                                      | Mit `Task.Run()` oder `Task.Factory.StartNew()` gestartet, meist über Thread-Pool.    |
| **Verwaltung**          | Muss manuell gestartet und verwaltet werden.                                                               | Wird vom .NET-Thread-Pool verwaltet.                                                  |
| **Rückgabewert**        | Kein direkter Rückgabewert. Ergebnisse müssen über gemeinsame Variablen oder Callbacks verarbeitet werden. | Kann ein Ergebnis mit `Task<TResult>` zurückgeben.                                    |
| **Stornierung**         | Keine native Unterstützung. Muss manuell implementiert werden.                                             | Unterstützt Abbruch über `CancellationToken`.                                         |
| **Async-Unterstützung** | Nicht für `async`/`await` geeignet.                                                                        | Vollständig integrierbar mit `async`/`await`.                                         |
| **Ressourcenverbrauch** | Teurer, da jeder Thread eigene Ressourcen verwendet.                                                       | Ressourcenschonender durch Wiederverwendung im Thread-Pool.                           |
| **Nebenläufigkeit**     | Führt exakt einen Codepfad aus.                                                                            | Kann beliebig viele Tasks gleichzeitig verwalten (abhängig von Systemressourcen).     |
| **Abstraktionsebene**   | Niedrig – direkte Steuerung der Threads.                                                                   | Höher – Fokus auf *was* getan werden soll, nicht *wie*.                               |


### Task-Klasse

Die `Task`-Klasse bildet einen Vorgang zur Lösung einer einzelnen Aufgabe ab, der keinen Wert zurück gibt und (in der Regel) asynchron ausgeführt wird. 
Ein `Task`-Objekt übernimmt eine Aufgabe, die asynchron auf einem Threadpool-Thread anstatt synchron auf dem Hauptanwendungsthread ausgeführt wird. 
Zum Überwachen des Bearbeitungsstatus stehen die Status-Eigenschaften des Threads 
und die Eigenschaften der Klasse `Task` zur Verfügung: `IsCanceled`, `IsCompleted`, und `IsFaulted`. 

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
```

Instanziierung und Ausführung von Tasks:

Die Instanziirung erfolgt über einen Konstruktor, die Ausführung wird durch den Aufruf der Methode Start veranlasst:

```csharp
Task task = new Task(() => {... Anweisungsblock ...});
task.Start();
```

> Bis hierher ist die API völlig identisch zu einem Thread (abgesehen von den Typen). 

Der verkürzte Aufruf mittels der statischen `Run`-Methode realisiert das gleiche Verhalten:

```csharp
Task task = Task.Run(() => {... Anweisungsblock ...});
```

An den Konstruktor und die Run-Methode können `Action`-Delegate übergeben werden, die den auszuführenden Code beinhalten.

> Delegaten können durch konkrete Methoden, anonyme Methoden oder Lambda-Ausdrücke realisiert werden.

Der Konstruktor wird nur in erweiterten Szenarien verwendet, wo es erforderlich ist, die Instanziirung und den Start zu trennen.

### Überwachung

Über Property `IsCompleted` kann der laufende Task aus dem Main-Thread überwacht werden. 
Um für die Durchführung einer einzelnen Aufgabe zu warten, rufen Sie die `Task.Wait` Methode auf. Ein Aufruf der Wait-Methode blockiert den aufrufenden Thread, bis die Instanz der Klasse die Ausführung abgeschlossen hat.

```csharp           TaskDefinition1
// Motiviert aus
// https://docs.microsoft.com/de-de/dotnet/api/system.threading.tasks.task?view=netframework-4.8
using System;
using System.Threading.Tasks;
using System.Threading;

public class Example
{
   public static void doSomething(){
     Console.WriteLine("Say hello!");
   }

   public static void Main()
   {
      Action<object> action = (object obj) =>
                        {
                           Console.WriteLine("Task={0}, obj={1}, Thread={2}",
                           Task.CurrentId, obj,
                           Thread.CurrentThread.ManagedThreadId);
                        };

      Task t1 = new Task(action, "alpha");
      t1.Start();
      Console.WriteLine("t1 has been launched. (Main Thread={0})",
                                Thread.CurrentThread.ManagedThreadId);

      // Nur der Vollständigkeit halber ...
      Task t2 = new Task(doSomething);
      t2.Start();
      Console.WriteLine("t2 has been launched. (Main Thread={0})",
                                Thread.CurrentThread.ManagedThreadId);                            


      Task t3 = Task.Run( () => {
                        // Just loop.
                        int ctr = 0;
                        for (ctr = 0; ctr <= 1000000; ctr++)
                        {}
                        Console.WriteLine("Finished {0} loop iterations",
                                          ctr);
                     } );
      t3.Wait();
   }
}
```
@LIA.eval(`["main.cs"]`, `mcs main.cs`, `mono main.exe`)


`Wait` ermöglicht auch die Beschränkung der Wartezeit auf ein bestimmtes Zeitintervall. Die `Wait(Int32)` und `Wait(TimeSpan)` Methoden blockiert den aufrufenden Thread, bis die Aufgabe abgeschlossen oder ein Timeoutintervall abgelaufen ist, je nach dem welcher Fall zuerst eintritt.

```csharp           WaitForNTimeSlots
using System;
using System.Threading;
using System.Threading.Tasks;

class Program {
  public static void Main(string[] args){
    // Wait on a single task with a timeout specified.
    Task taskA = Task.Run( () => Thread.Sleep(2000));
    //Task taskX = Task.Run(() => { throw new IndexOutOfRangeException(); } );
    //Task taskY = Task.Run(() => { throw new FormatException(); } );
    try {
      taskA.Wait(1000);       // Wait for 1 second.
      bool completed = taskA.IsCompleted;
      Console.WriteLine("Task A completed: {0}, Status: {1}",
                       completed, taskA.Status);
      if (! completed)
         Console.WriteLine("Timed out before task A completed.");
      //taskX.Wait();
      //taskY.Wait();
     }
     catch (AggregateException) {
        Console.WriteLine("Exception in taskA.");
     }
  }
}
```
@LIA.eval(`["main.cs"]`, `mcs main.cs`, `mono main.exe`)

Für komplexe Taskstrukturen kann man diese zum Beispiel in Arrays arrangieren.
Für diese Reihe von Aufgaben jeweils durch Aufrufen der `Wait` Methode zu warten
wäre aufwändig und wenig praktisch. `WaitAll` schließt diese Lücke und erlaubt
eine übergreifende Überwachung.

Im folgenden Beispiel werden zehn Aufgaben erstellt, die warten, bis alle zehn abgeschlossen werden, dann wird ihr Status anzeigt.

```csharp           WaitForAll
using System;
using System.Threading;
using System.Threading.Tasks;

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
```
@LIA.eval(`["main.cs"]`, `mcs main.cs`, `mono main.exe`)

### Generische Task-Klasse

Die generische Klasse `Task<T>` bildet ebenfalls einen Vorgang zur Lösung einer einzelnen Aufgabe ab, gibt aber im Unterschied zu der nicht generischen `Task`-Klasse einen Wert zurück. 
Die Konstruktoren und die Run-Methode der Klasse bekommen einen `Func`-Delegat bzw. einen als Lambda-Ausdruck formulierten Code übergeben, der einen Rückgabewert liefert.


```csharp           TaskClasses
public class Task<T>: Task{
  public Task (Func<T> f);
  ...
  public static Task<T> Run (Func <T> f);
  ...
  public T Result { get; }
}
```

Der Kanon der Möglichkeiten wird durch die Klasse `Task<TResult>` deutlich erweitert. 
Anstatt die Ergebnisse wie bei Threads in eine "außen stehende" Variable (z.B. Datenfeld einer Klasse) zu speichern, wird das Ergebnis im
`Task`-Objekt selbst  gespeichert und kann dann über die Eigenschaft `Result` abgerufen werden. 

```csharp      TaskWithReturn
Task<int> task = Task.Run(() => {int i;
                                 //... Anweisungsblock ...;
                                 return i;});
 Console.WriteLine("Finished with result {0}", task.Result);
```

Wie ist dieser Aufruf zu verstehen? Unser Task gibt anders als bei der synchronen
Abarbeitung nicht unmittelbar mit dem Ende der Bearbeitung einen Wert zurück, sondern
verspricht zu einem späteren Zeitpunkt einen Wert in einem bestimmten Format zu liefern. Dank der generischen Realisierung können dies beliebige Objekte sein.

Wie aber erfolgt die Rückgabe und wann?


## Asynchrone Methoden

Das Kernstück der asynchronen Programmierung mit TAP (task based asynchronous pattern) sind die Schlüsselwörter `async` und `await`.
"async" wird verwendet, um eine Methode zu markieren, die asynchronen Code enthält, 
"await" wird verwendet, um auf das Ergebnis einer asynchronen Operation zu warten, ohne den Aufrufer zu blockieren. 

```csharp
using System;
using System.Net.Http;
using System.Threading.Tasks;

public class Program
{
    public static async Task Main()
    {   Console.WriteLine("Beispiel mit Download");
        int n=await DownloadFileAsync();
        Console.WriteLine(n);
        Console.WriteLine("Download abgeschlossen!");
    }

    public static async Task<int> DownloadFileAsync()
    {
        using (var httpClient = new HttpClient())
        {
            Console.WriteLine("Starte den Download...");
            var url = "https://github.com/TUBAF-IfI-LiaScript/VL_Softwareentwicklung/blob/master/24_Tasks.md";
            var response = await httpClient.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            //Console.WriteLine("Datei heruntergeladen: " + content);
            return content.Length;
        }
    }
}
```

Die Initiierung und der Abschluss eines asynchronen Vorgangs wird in TAP in einer Methode realisiert, die 
das `async`-Präfix hat und dadurch eine `await`-Anweisung enthalten darf, wenn sie Awaitable-Typen zurückgibt, 
wie z. B. Task oder Task<TResult>.

### Was `async` genau bewirkt - und was nicht

Ein verbreitetes Missverständnis: `async` mache eine Methode "nebenläufig" oder
lasse sie auf einem eigenen Thread laufen. **Das tut es nicht.** `async` ist
zunächst nur ein *Schalter für den Compiler*, der zwei Dinge erlaubt:

1. Innerhalb der Methode darf `await` verwendet werden.
2. Der Compiler baut die Methode in eine **Zustandsmaschine** um: An jedem `await`
   kann sie *pausieren*, die Kontrolle an den Rufer zurückgeben und später - wenn
   der erwartete Task fertig ist - an genau dieser Stelle *weitermachen*.

> **Kurz:** `async` schaltet `await` frei und macht die Methode
> *unterbrechbar/fortsetzbar*. Ausgeführt wird sie zunächst ganz normal auf dem
> aufrufenden Thread - bis zum ersten `await`.

### `async` steckt an - die ganze Aufrufkette

Der entscheidende Punkt, der oft übersehen wird: **`await` darf nur in einer
Methode stehen, die selbst `async` ist.** Will also eine Methode das Ergebnis
einer asynchronen Operation abwarten, muss sie selbst `async` werden - und gibt
dann ihrerseits einen `Task` zurück. Deren Aufrufer steht vor demselben Problem
und muss ebenfalls `async` werden. So *wandert* die Markierung die Aufrufkette
hinauf, bis ganz oben (in `Main` oder einem Event-Handler).

Schauen wir uns das im Download-Beispiel von oben an:

<!-- style="display: block; margin-left: auto; margin-right: auto; max-width: 815px;" -->
```text @plantUML
@startuml
skinparam defaultTextAlignment center
rectangle "async Task Main()" as M
rectangle "async Task<int>\nDownloadFileAsync()" as D
rectangle "httpClient.GetAsync(url)\n(gibt Task<...> zurück)" as G

M -down-> D : await
D -down-> G : await
note right of G : hier passiert das\neigentliche Warten (I/O)
note left of M : muss async sein,\nweil es DownloadFileAsync\nawait-et
note left of D : muss async sein,\nweil es GetAsync\nawait-et
@enduml
```

- `httpClient.GetAsync(...)` liefert einen `Task<...>` (die eigentliche
  asynchrone I/O-Operation der Bibliothek).
- `DownloadFileAsync` will dieses Ergebnis mit `await` abwarten → **muss `async`
  sein** und gibt selbst `Task<int>` zurück.
- `Main` will *dessen* Ergebnis mit `await` abwarten → **muss ebenfalls `async`
  sein** (`async Task Main`).

> **Merksatz:** *"async all the way"* - asynchron ist keine lokale Eigenschaft
> einer einzelnen Methode, sondern zieht sich durch die gesamte Aufrufkette bis
> nach oben.

Warum unterbricht man die Kette nicht einfach, indem man das Ergebnis mit
`.Result` oder `.Wait()` "synchron abholt"? Weil man damit genau den Vorteil
wegwirft (der Thread blockiert dann doch wieder) und sich in UI-Anwendungen sogar
**Deadlocks** einhandeln kann. Die saubere Regel lautet daher: nicht die Kette
brechen, sondern `async`/`await` konsequent bis oben durchziehen.

Eine asynchrone Methode ruft einen Task auf, setzt die eigene Bearbeitung aber
fort und wartet auf dessen Beendigung.

```csharp
async void DoAsync(){
  Task<int> task = Task.Run(() => {int i;
                                   // Berechnungen
                                   return i;});
  // Instruktionen I
  // Methoden, die unabhängig von task ausgeführt werden
  int result = await task;
  // Instruktionen II
  // Hier wird nun mit dem Ergebnis result weitergearbeitet
}
```

Das Ergebnis der Operation hängt dabei davon ab, welche Zeitabläufe sich im
Programmablauf ergeben.

**Fall I** Das Ergebnis der Lambdafunktion liegt vor, bevor DoAsync die Zeile
mit await erreicht hat (Quasi-Synchroner Fall)

```text @plantUML
@startuml
participant "Rufer (main)" as Main
participant "DoAsync()" as Do
participant "Task.Run(()=>{..})" as Task

Main -> Do : Aufruf
activate Do
Do -> Task : starten
activate Task
Task --> Do : results (schon fertig)
deactivate Task
note over Do : Instruktionen I\nawait: Ergebnis liegt\nbereits vor -> kein Pausieren\nInstruktionen II
Do --> Main : return
deactivate Do
@enduml
```

**Fall II** Das Ergebnis der Lambdafunktion liegt erst später, nachdem DoAsync die Zeile mit await erreicht hat.
Die Methode pausiert an der Stelle des await-Ausdrucks und wartet darauf, dass der Task abgeschlossen wird. 
Während dieser Wartezeit wird der Thread, auf dem DoAsync() ausgeführt wird, nicht blockiert, sondern steht für andere Aufgaben zur Verfügung.

```text @plantUML
@startuml
participant "Rufer (main)" as Main
participant "DoAsync()" as Do
participant "Task.Run(()=>{..})" as Task

Main -> Do : Aufruf
activate Do
Do -> Task : starten
activate Task
note over Do : Instruktionen I
Do --> Main : return bei await\n(Kontrolle zurück,\nErgebnis fehlt noch)
deactivate Do
note over Main : läuft weiter
Task --> Do : results (später)
deactivate Task
activate Do
note over Do : Instruktionen II\n(Fortsetzung nach await)
deactivate Do
@enduml
```

Zwei sehr anschauliche Beispiele finden sich im Code Ordner des Projekts.

| Beispiel | Bemerkung |
| -------- | --------- |
| [AsyncExampleI.cs](https://github.com/TUBAF-IfI-LiaScript/VL_Softwareentwicklung/blob/master/code/24_Tasks/AsyncExampleI/Program.cs) | Generelle Einbettung des asynchronen Tasks |
| [AsyncExampleII.cs](https://github.com/TUBAF-IfI-LiaScript/VL_Softwareentwicklung/blob/master/code/24_Tasks/AsyncExampleII/Program.cs) | Was passiert eigentlich, wenn `main` zum Ende kommt mit den noch ausbleibenden Ergebnissen von Tasks? |

## Fehlerbehandlung in Tasks

Bei synchronem Code ist die Sache klar: Wirft eine Methode eine Exception, wird
sie sofort am Aufrufort geworfen und kann dort mit `try/catch` behandelt werden.
Bei asynchronen Tasks verschiebt sich dieser Zeitpunkt - und genau das ist eine
der häufigsten Fehlerquellen.

Wirft der Code innerhalb eines Tasks eine Exception, wird diese nicht sofort
weitergereicht. Der Task wechselt stattdessen in den Status `Faulted` und
"parkt" die Exception in seiner Eigenschaft `Exception`. Erst wenn der Rufer den
Task mit `await` (oder `Wait`/`Result`) abfragt, wird die Exception dort erneut
geworfen.

```csharp           Program.cs
using System;
using System.Threading.Tasks;

class Program
{
    static async Task<int> BerechneMitFehler()
    {
        await Task.Delay(200);
        throw new InvalidOperationException("Etwas ist schiefgelaufen!");
    }

    public static async Task Main()
    {
        Task<int> task = BerechneMitFehler();   // Exception fliegt hier NOCH nicht
        Console.WriteLine("Task gestartet, Status: {0}", task.Status);

        try
        {
            int ergebnis = await task;          // ... sondern erst HIER
            Console.WriteLine("Ergebnis: {0}", ergebnis);
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine("Abgefangen: {0}", ex.Message);
            Console.WriteLine("Task-Status jetzt: {0}", task.Status);
        }
    }
}
```
```xml   -project.csproj
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net8.0</TargetFramework>
  </PropertyGroup>
</Project>
```
@LIA.eval(`["Program.cs", "project.csproj"]`, `dotnet build -nologo`, `dotnet run -nologo`)

> Die Exception wird an der Stelle des `await` geworfen - nicht dort, wo der Task
> gestartet wurde. Das `try/catch` gehört also **um das `await`**, nicht um den
> Aufruf, der den Task erzeugt.

### Die Falle: `async void`

Eine asynchrone Methode sollte immer `Task` (bzw. `Task<T>`) zurückgeben, nicht
`void`. Bei `async void` gibt es keinen Task, den der Rufer abfragen könnte -
also gibt es auch keinen Ort, an dem eine Exception geworfen werden kann. Sie
geht damit **verloren** bzw. reißt im schlimmsten Fall den ganzen Prozess ab.

<!-- style="display: block; margin-left: auto; margin-right: auto; max-width: 815px;" -->
```ascii
   async Task  M()          async void  M()
        |                        |
   Exception                Exception
        |                        |
        v                        v
   im Task "geparkt"        kein Task vorhanden
        |                        |
   await wirft sie          niemand fängt sie
   -> catch möglich         -> Prozess kann abstürzen
```

> **Merksatz:** `async void` nur für Event-Handler verwenden, wo die Signatur es
> erzwingt. Überall sonst: `async Task`.

### Fehler bei mehreren Tasks - `AggregateException`

Warten wir gleichzeitig auf mehrere Tasks, können auch mehrere davon fehlschlagen.
`Task.WaitAll` bündelt diese Fehler in einer `AggregateException`, die über
`InnerExceptions` alle einzelnen Fehler zugänglich macht - das erklärt den
`catch (AggregateException ...)`-Block aus dem `WaitAll`-Beispiel weiter oben.

```csharp           Program.cs
using System;
using System.Threading.Tasks;

class Program
{
    static void Main()
    {
        Task taskX = Task.Run(() => { throw new IndexOutOfRangeException(); });
        Task taskY = Task.Run(() => { throw new FormatException(); });

        try
        {
            Task.WaitAll(taskX, taskY);
        }
        catch (AggregateException ae)
        {
            Console.WriteLine("{0} Fehler aufgetreten:", ae.InnerExceptions.Count);
            foreach (var ex in ae.Flatten().InnerExceptions)
                Console.WriteLine("   {0}: {1}", ex.GetType().Name, ex.Message);
        }
    }
}
```
```xml   -project.csproj
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net8.0</TargetFramework>
  </PropertyGroup>
</Project>
```
@LIA.eval(`["Program.cs", "project.csproj"]`, `dotnet build -nologo`, `dotnet run -nologo`)

> Achtung auf einen feinen Unterschied: `await Task.WhenAll(...)` wirft nur die
> **erste** der aufgetretenen Exceptions direkt. Die vollständige Liste steht auch
> hier über die `Exception`-Eigenschaft des von `WhenAll` zurückgegebenen Tasks
> zur Verfügung.

## Mehrere Tasks koordinieren - `WhenAll`

Kehren wir zum Frühstücksbeispiel vom Anfang der Vorlesung zurück. Der
entscheidende Gedanke war: Der Rufer soll die Kontrolle *behalten* und mehrere
Aufgaben **nebenläufig** anstoßen, statt blockierend auf jede einzelne zu warten.
Was wir bei den Threads mit `Join` und bei den synchronen Tasks mit `WaitAll`
gelöst haben, hat im Umfeld von `async`/`await` ein eigenes Pendant: `Task.WhenAll`.

Der Unterschied ist wichtig:

+ `Task.WaitAll(...)` **blockiert** den aufrufenden Thread, bis alle Tasks fertig sind.
+ `await Task.WhenAll(...)` **gibt die Kontrolle zurück** an den Rufer und setzt erst
  fort, wenn alle Tasks abgeschlossen sind - ohne den Thread zu blockieren.

Im Beispiel bereiten wir Eier, Speck und Toast zu. Zunächst nacheinander (jede
Aufgabe wird `await`-et, bevor die nächste beginnt), dann nebenläufig: Alle drei
Tasks werden *gestartet* und erst danach gemeinsam abgewartet.

```csharp           Program.cs
using System;
using System.Diagnostics;
using System.Threading.Tasks;

class Program
{
    static async Task BrateEier()   { await Task.Delay(300); Console.WriteLine("  Eier fertig"); }
    static async Task BrateSpeck()  { await Task.Delay(500); Console.WriteLine("  Speck fertig"); }
    static async Task ToasteBrot()  { await Task.Delay(200); Console.WriteLine("  Toast fertig"); }

    public static async Task Main()
    {
        var watch = Stopwatch.StartNew();

        // synchron: eins nach dem anderen -> Summe der Wartezeiten
        await BrateEier();
        await BrateSpeck();
        await ToasteBrot();
        Console.WriteLine("Nacheinander: {0} ms\n", watch.ElapsedMilliseconds);

        // nebenläufig: alle drei starten, DANN gemeinsam abwarten
        watch.Restart();
        Task eier  = BrateEier();
        Task speck = BrateSpeck();
        Task toast = ToasteBrot();
        await Task.WhenAll(eier, speck, toast);
        Console.WriteLine("Mit WhenAll: {0} ms", watch.ElapsedMilliseconds);
    }
}
```
```xml   -project.csproj
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net8.0</TargetFramework>
  </PropertyGroup>
</Project>
```
@LIA.eval(`["Program.cs", "project.csproj"]`, `dotnet build -nologo`, `dotnet run -nologo`)

> Die nacheinander ausgeführte Variante braucht rund `300 + 500 + 200 = 1000 ms`.
> Die nebenläufige Variante braucht nur so lange wie die **längste** Einzelaufgabe
> (`500 ms`) - genau das ist der Gewinn, den das Frühstücksbeispiel motiviert hat.

> **Hinweis:** `Task.WhenAll` gibt es auch mit Rückgabewert: `await Task.WhenAll(t1, t2)`
> auf einem `Task<T>[]` liefert ein `T[]` mit allen Ergebnissen. Das Gegenstück
> `Task.WhenAny` setzt fort, sobald der *erste* Task fertig ist - nützlich z. B. für
> Timeouts oder das schnellste von mehreren Ergebnissen.

## Kooperativer Abbruch - `CancellationToken`

In der Gegenüberstellung von `Thread` und `Task` hatten wir als Vorteil des
Task-Modells die *native Unterstützung für den Abbruch* über einen
`CancellationToken` genannt. Diese Zusage lösen wir nun ein.

Der Abbruch in .NET ist **kooperativ**: Es gibt keinen "harten" Abschuss eines
laufenden Tasks von außen (das wäre so gefährlich wie das veraltete
`Thread.Abort`). Stattdessen signalisiert der Rufer über eine
`CancellationTokenSource` einen *Abbruchwunsch*, und der Task prüft an geeigneten
Stellen selbst, ob er beenden soll.

```csharp           Cancellation.cs
using System;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    static void Main()
    {
        CancellationTokenSource cts = new CancellationTokenSource();
        CancellationToken token = cts.Token;

        Task worker = Task.Run(() => {
            int i = 0;
            while (!token.IsCancellationRequested)   // Task prüft den Wunsch selbst
            {
                Console.WriteLine("  arbeite... Schritt {0}", i++);
                Thread.Sleep(200);
            }
            Console.WriteLine("  Task hat den Abbruchwunsch bemerkt und stoppt.");
        }, token);

        Thread.Sleep(1000);      // Hauptthread lässt den Task eine Weile laufen
        Console.WriteLine("Main: fordere Abbruch an.");
        cts.Cancel();            // Abbruch nur SIGNALISIEREN, nicht erzwingen
        worker.Wait();
        Console.WriteLine("Main: fertig.");
    }
}
```
@LIA.eval(`["main.cs"]`, `mcs main.cs`, `mono main.exe`)

> Der Rufer ruft `cts.Cancel()` - erzwingt aber nichts. Der Task entscheidet an
> der Schleifenbedingung `!token.IsCancellationRequested` selbst, dass er aufhört.
> Genau dieses Zusammenspiel meint "kooperativer" Abbruch.

Zwei weitere Bausteine runden das Bild ab:

+ `token.ThrowIfCancellationRequested()` wirft eine `OperationCanceledException` -
  praktisch, um eine tief verschachtelte Berechnung sofort zu verlassen. Der Task
  wechselt dann in den Status `Canceled`.
+ `new CancellationTokenSource(TimeSpan)` löst den Abbruch **automatisch nach
  Ablauf einer Zeit** aus - der klassische Weg, einem Task ein Timeout zu geben.

## Fortgeschrittene Muster

Mit `WhenAll`, Fehlerbehandlung und `CancellationToken` ist das Handwerkszeug
beisammen. Zwei weitere Muster tauchen in der Praxis so häufig auf, dass sie hier
noch ihren Platz bekommen.

### Der Erste gewinnt - `WhenAny` als Timeout

`Task.WhenAll` wartet, bis **alle** Tasks fertig sind. Das Gegenstück
`Task.WhenAny` setzt fort, sobald der **erste** Task abgeschlossen ist, und liefert
genau diesen Task zurück. Ein typischer Einsatz: Man lässt die eigentliche Arbeit
gegen einen "Wecker-Task" (`Task.Delay`) antreten. Ist der Wecker zuerst fertig,
war die Arbeit zu langsam - ein Timeout.

```text @plantUML
@startuml
participant "Main" as Main
participant "arbeit\n(Task.Delay 900 ms)" as Arbeit
participant "timeout\n(Task.Delay 500 ms)" as Timeout

Main -> Arbeit : starten
activate Arbeit
Main -> Timeout : starten
activate Timeout
Main -> Main : await Task.WhenAny(arbeit, timeout)

Timeout --> Main : fertig nach 500 ms
deactivate Timeout
note over Main : WhenAny setzt fort\n-> Timeout hat "gewonnen"
note over Arbeit : läuft im Hintergrund\nweiter (nicht abgebrochen!)
Arbeit --> Arbeit : fertig nach 900 ms
deactivate Arbeit
@enduml
```

```csharp           Program.cs
using System;
using System.Threading.Tasks;

class Program
{
    static async Task<string> LangeArbeit()
    {
        await Task.Delay(900);
        return "Ergebnis der Arbeit";
    }

    public static async Task Main()
    {
        Task<string> arbeit  = LangeArbeit();
        Task         timeout = Task.Delay(500);

        Task ersterFertig = await Task.WhenAny(arbeit, timeout);

        if (ersterFertig == arbeit)
            Console.WriteLine("Rechtzeitig fertig: {0}", await arbeit);
        else
            Console.WriteLine("Timeout! Die Arbeit war zu langsam.");
    }
}
```
```xml   -project.csproj
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net8.0</TargetFramework>
  </PropertyGroup>
</Project>
```
@LIA.eval(`["Program.cs", "project.csproj"]`, `dotnet build -nologo`, `dotnet run -nologo`)

> `WhenAny` bricht den langsameren Task **nicht** ab - er läuft im Hintergrund
> weiter. Für ein sauberes Timeout kombiniert man das Muster daher meist mit einem
> `CancellationToken`, der den unterlegenen Task tatsächlich stoppt.

### Fortschritt melden - `IProgress<T>`

Beim `CancellationToken` fließt Information vom Rufer *zum* Task ("bitte aufhören").
Häufig braucht man auch die Gegenrichtung: Der laufende Task will dem Rufer seinen
**Fortschritt** melden (z. B. für einen Ladebalken). Dafür gibt es die Schnittstelle
`IProgress<T>`. Der Rufer stellt ein `Progress<T>`-Objekt bereit und gibt an, was
mit jeder Meldung geschehen soll; der Task ruft nur `Report(...)` auf.

```text @plantUML
@startuml
participant "Main (Rufer)" as Main
participant "Progress<int>\n(Callback)" as Prog
participant "RechneMitFortschritt" as Task

Main -> Prog : erstellen mit Callback\n(prozent => WriteLine)
Main -> Task : await ...(progress)
activate Task
loop 5 Arbeitsschritte
  Task -> Task : await Task.Delay(200)
  Task -> Prog : Report(i * 20)
  Prog -> Main : Callback: "Fortschritt X %"
end
Task --> Main : fertig
deactivate Task
@enduml
```

```csharp           Program.cs
using System;
using System.Threading.Tasks;

class Program
{
    static async Task RechneMitFortschritt(IProgress<int> progress)
    {
        for (int i = 1; i <= 5; i++)
        {
            await Task.Delay(200);          // ein Arbeitsschritt
            progress.Report(i * 20);        // Fortschritt in Prozent melden
        }
    }

    public static async Task Main()
    {
        var progress = new Progress<int>(prozent =>
            Console.WriteLine("  Fortschritt: {0} %", prozent));

        Console.WriteLine("Starte Berechnung...");
        await RechneMitFortschritt(progress);
        Console.WriteLine("Fertig!");
    }
}
```
```xml   -project.csproj
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net8.0</TargetFramework>
  </PropertyGroup>
</Project>
```
@LIA.eval(`["Program.cs", "project.csproj"]`, `dotnet build -nologo`, `dotnet run -nologo`)

> `Progress<T>` fängt den Aufrufkontext ein: In einer UI-Anwendung landet der
> Callback automatisch wieder im UI-Thread, sodass man von dort gefahrlos
> Oberflächenelemente aktualisieren kann - ein Detail, das uns in der
> MAUI-Vorlesung wieder begegnet.

### Ausblick

Das Task-Modell reicht noch deutlich weiter, als diese Vorlesung zeigen kann.
Zwei Stichworte für die eigene Vertiefung:

+ **`ValueTask<T>`** - eine ressourcenschonendere Alternative zu `Task<T>` für
  Methoden, die ihr Ergebnis oft schon synchron vorliegen haben und nur selten
  wirklich asynchron werden.
+ **`IAsyncEnumerable<T>`** mit `await foreach` - erlaubt es, einen *Strom* von
  Ergebnissen asynchron zu durchlaufen, statt auf ein einziges Gesamtergebnis zu
  warten (z. B. seitenweise aus einer Datenbank oder einem Netzwerk-Stream).
