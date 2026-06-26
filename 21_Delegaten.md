<!--

author:   Sebastian Zug, Galina Rudolf & André Dietrich
email:    sebastian.zug@informatik.tu-freiberg.de
version:  1.0.10
language: de
narrator: Deutsch Female
comment:  Grundidee, Multicast Delegaten, Anonyme/Lambda Funktionen, generische Delegaten, Action und Func
tags:      
logo:     

import: https://github.com/liascript/CodeRunner
        https://raw.githubusercontent.com/liascript-templates/plantUML/master/README.md 

import: https://raw.githubusercontent.com/TUBAF-IfI-LiaScript/VL_Softwareentwicklung/master/config.md

-->

[![LiaScript](https://raw.githubusercontent.com/LiaScript/LiaScript/master/badges/course.svg)](https://liascript.github.io/course/?https://github.com/TUBAF-IfI-LiaScript/VL_Softwareentwicklung/blob/master/21_Delegaten.md)

# Delegaten

| Parameter                | Kursinformationen                                                                         |
| ------------------------ | ----------------------------------------------------------------------------------------- |
| **Veranstaltung:**       | `Vorlesung Softwareentwicklung`                                                           |
| **Teil:**                | `21/27`                                                                                   |
| **Semester**             | @config.semester                                                                          |
| **Hochschule:**          | @config.university                                                                        |
| **Inhalte:**             | @comment                                                                                  |
| **Link auf den GitHub:** | https://github.com/TUBAF-IfI-LiaScript/VL_Softwareentwicklung/blob/master/21_Delegaten.md |
| **Autoren**              | @author                                                                                   |

![](https://media.giphy.com/media/26tn33aiTi1jkl6H6/source.gif)

---------------------------------------------------------------------

## Exkurse: Logging

Wie arbeiten wir bisher in Bezug auf Textausgaben?

```csharp    ImplicitConstructorCall
using System;
using System.Reflection;
using System.ComponentModel.Design;

public class Person {
  public int geburtsjahr;
  public string name;

  public Person(){
    geburtsjahr = 1984;
    name = "Orwell";
    Console.WriteLine("ctor of Person");
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
```
@LIA.eval(`["main.cs"]`, `mcs main.cs`, `mono main.exe`)

Dieses Vorgehen kann auf Dauer ziemlich nerven ...

> Lösung: Verwenden Sie ein Logging Framework, z.B. NLog - ein Logging-Framework für .NET-Anwendungen!
>
> NLog wird als NuGet-Paket eingebunden - das zugrundeliegende Paketmanagement haben
> wir in der Vorlesung [Dokumentation und Build-Tools](18_Dokumentation_BuildTools.md) besprochen.

| **Merkmal**            | **Beschreibung**                                                              | **`print()`** | **Logging-Framework** |
| ---------------------- | ----------------------------------------------------------------------------- | ------------- | --------------------- |
| **Zentrale Steuerung** | Konfiguration und Steuerung der Ausgabe zentral möglich                       | ❌            | ✅                    |
| **Log-Level**          | Nachrichten können je nach Wichtigkeit kategorisiert werden                   | ❌            | ✅                    |
| **Formatierung**       | Ausgaben können standardisiert formatiert werden (z. B. mit Zeitstempel)      | ❌            | ✅                    |
| **Dateihandling**      | Logs können automatisch in Dateien geschrieben und rotiert werden             | ❌            | ✅                    |
| **Mehrere Ausgaben**   | Gleichzeitige Ausgabe an Konsole, Datei, Netzwerk usw.                        | ❌            | ✅                    |
| **Thread-Sicherheit**  | Gleichzeitige Ausgaben mehrerer Threads führen nicht zu vermischten Zeilen    | ❌            | ✅                    |
| **Integration**        | Logs können mit externen Tools (z. B. Logserver, Dashboards) verwendet werden | ❌            | ✅                    |


                              {{1-2}}
***********************************************************************

NLog:

+ ermöglicht das Protokollieren von Informationen, Warnungen, Fehlern und anderen Ereignissen,
+ unterstützt Datei-Logging, Datenbank-Logging, E-Mail-Logging, Konsolen-Logging und mehr

nlog.config:

```
<?xml version="1.0" encoding="utf-8" ?>
<nlog xmlns="http://www.nlog-project.org/schemas/NLog.xsd"
      xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">

    <targets>
        <target name="logfile" xsi:type="File" fileName="file.txt" />
        <target name="logconsole" xsi:type="Console" />
    </targets>

    <rules>
        <logger name="*" minlevel="Info" writeTo="logconsole" />
        <logger name="*" minlevel="Debug" writeTo="logfile" />
    </rules>
</nlog>
```

```csharp
using NLog;

public class Program
{
    private static Logger logger = LogManager.GetCurrentClassLogger();

    public static void Main()
    {
        logger.Info("Anwendung gestartet");
        // ... Weitere Anwendungslogik ...
        logger.Error("Ein Fehler ist aufgetreten");
        // ... Weitere Anwendungslogik ...
        logger.Info("Anwendung beendet");
    }
}
```

+ https://github.com/NLog
+ https://riptutorial.com/nlog

***********************************************************************

## Motivation und Konzept der Delegaten

Ihre Aufgabe besteht darin folgendes Code-Fragment so umzuarbeiten, so dass
unterschiedliche Formen der Nutzer-Notifikation (neben Konsolenausgaben auch
Emails, Instant-Messager Nachrichten, Tonsignale) möglich sind. Welche Ideen
haben Sie dazu?

```csharp           Notification
using System;
using System.Reflection;
using System.Collections.Generic;

public class VideoEncodingService{

  private string userId;
  private string filename;

  public VideoEncodingService(string filename, string userId){
     this.userId = userId;
     this.filename = filename;
  }

  public void StartVideoEncoding(){
     Console.WriteLine("The encoding job takes a while!");
     NotifyUser();
  }

  public void NotifyUser(){
      Console.WriteLine("Dear user {0}, your encoding job {1} was finished",
                          userId, filename);
  }
}

public class Program{
  public static void Main(string[] args){
     VideoEncodingService myMovie = new VideoEncodingService("007.mpeg", "12321");
     myMovie.StartVideoEncoding();
  }
}
```
@LIA.eval(`["main.cs"]`, `mcs main.cs`, `mono main.exe`)

> [!IMPORTANT]
> Gegen das Hinzufügen weiterer Ausgabemethoden in die Klasse `VideoEncodingService` spricht
> die Tatsache, dass dies nicht deren zentrale Aufgabe ist. Eigentlich sollte
> sich die Klasse gar nicht darum kümmern müssen, welche Art der Notifikation
> genutzt werden soll, dies sollte dem Nutzer überlassen bleiben.

Folglich wäre es sinnvoll, wenn wir `StartVideoEncoding` eine Funktion als
Parameter übergeben könnten, die wir unabhängig von der eigentlichen Klasse
definiert haben.

```csharp
  public void TriggerMe(){
    // TODO
  }

  VideoEncodingService myMovie = new VideoEncodingService("007.mpeg",
                                                          "12321",
                                                          TriggerMe);
```

> In C würden wir an dieser Stelle von einem Funktionspointer sprechen.

```c FunktionPointer.c
// https://www.geeksforgeeks.org/function-pointer-in-c/

#include <stdio.h>
void add(int a, int b)
{
    printf("Addition is %d\n", a+b);
}

void subtract(int a, int b)
{
    printf("Subtraction is %d\n", a-b);
}

void multiply(int a, int b)
{
    printf("Multiplication is %d\n", a*b);
}

int main()
{
    // fun_ptr_arr is an array of function pointers
    void (*fun_ptr_arr[])(int, int) = {add, subtract, multiply};
    unsigned int ch, a = 15, b = 10;

    printf("Enter Choice: 0 for add, 1 for subtract and 2 "
            "for multiply\n");
    scanf("%d", &ch);

    if (ch > 2) return 0;

    (*fun_ptr_arr[ch])(a, b);

    return 0;
}
```
@LIA.evalWithDebug(`["main.c"]`, `gcc -Wall main.c -o a.out`, `./a.out`)

Die Deklaration `void (*fun_ptr_arr[])(int, int)` wirkt zunächst kryptisch. Sie
wird „von innen nach außen" gelesen, beginnend beim Bezeichner:

| Bestandteil                       | Bedeutung                                          |
| --------------------------------- | -------------------------------------------------- |
| `fun_ptr_arr`                     | der Name                                            |
| `fun_ptr_arr[]`                   | … ist ein _Array_                                   |
| `(*fun_ptr_arr[])`                | … von _Pointern_                                    |
| `(*fun_ptr_arr[])(int, int)`      | … auf _Funktionen_ mit zwei `int`-Parametern        |
| `void (*fun_ptr_arr[])(int, int)` | … die `void` _zurückgeben_                          |

> [!ATTENTION]
> Das `void` ist hier der **Rückgabetyp** der referenzierten Funktion — und
> **kein** `void`-Pointer (`void *`). Ein Funktionspointer ist gerade _nicht_
> typlos: Er trägt die vollständige Signatur (Parameter **und** Rückgabetyp) im
> Typ. Genau diese typsichere Signatur-Bindung formalisiert der C#-Delegat im
> nächsten Abschnitt sauberer.


### Grundidee

> [!IMPORTANT]
> **Ein Delegat ist ein Methodentyp und dient zur Deklaration von Variablen, die auf eine Methode verweisen.**

Für die Anwendung sind drei Vorgänge nötig:

1. Anlegen des Delegaten (Spezifikation einer Signatur)
2. Instantiierung (Zuweisung einer signaturkorrekten Methode)
3. Aufruf der Instanz

```csharp  Concept.cs
// Schritt 1 - Spezifikation
//[Zugriffsattribut] delegate Rückgabewert DelegatenName(Parameterliste);
public delegate int Rechenoperation(int x, int y);

static int Addition(int x, int y){
  return x + y;
}

static int Modulo(int dividend, int divisor){
  return dividend % divisor;
}

// Schritt 2 - Instanzieren
Rechenoperation myCalc = new Rechenoperation(Addition);
// oder
Rechenoperation myCalc = Addition;

// Schritt 3 - Ausführen
myCalc(7, 9);
```

Lassen Sie uns dieses Konzept auf unsere `VideoEncodingService`-Klasse anwenden.

```csharp           Notification
using System;
using System.Reflection;
using System.Collections.Generic;

// Schritt 1
public delegate void NotifyUser(string userId, string filename);

public class VideoEncodingService
{
  private string userId;
  private string filename;

  public VideoEncodingService(string filename, string userId){
     this.userId = userId;
     this.filename = filename;
  }

  public void StartVideoEncoding(NotifyUser notifier){
     Console.WriteLine("The encoding job takes a while!");
      // Schritt 3
      notifier(userId, filename);
  }
}

public class Program
{
  // Die Notifikationsmethode ist nun Bestandteil der "Nutzerklasse"
  public static void NotifyUserByText(string userId, string filename){
    Console.WriteLine("Dear user {0}, your encoding job {1} was finished",
                      userId, filename);
  }

  public static void Main(string[] args){
     VideoEncodingService myMovie = new VideoEncodingService("007.mpeg", "12321");
     // Schritt 2
     NotifyUser notifyMe = new NotifyUser(NotifyUserByText);
     myMovie.StartVideoEncoding(notifyMe);
  }
}
```
@LIA.eval(`["main.cs"]`, `mcs main.cs`, `mono main.exe`)

> [!NOTE]
> Hier übergeben wir den Delegaten noch direkt als Methodenparameter und rufen
> ihn unmittelbar in `StartVideoEncoding` auf. In typischem C#-Code würde man die
> Benachrichtigung stattdessen als **`event`** modellieren: Der
> `VideoEncodingService` bietet dann ein Ereignis an (z. B. `EncodingFinished`),
> bei dem sich beliebig viele Interessenten _registrieren_ (`+=`) und das er nach
> getaner Arbeit _auslöst_. Der Dienst muss so seine „Zuhörer" nicht mehr als
> Parameter kennen. Dieses Publish-Subscribe-Muster auf Basis von Delegaten
> behandeln wir in der folgenden Vorlesung [Events](22_Events.md).

### Multicast Delegaten

Sollten wir uns mit dem Aufruf einer Methode zufrieden geben?

```csharp           MultiCast
using System;
using System.Reflection;

public class Program
{
  delegate int Calc(int x, int y);

  static int Add(int x, int y){
      Console.WriteLine("x + y");
      return x + y;
  }

  static int Multiply(int x, int y){
      Console.WriteLine("x * y");
      return x * y;
  }

  static int Divide(int x, int y){
      Console.WriteLine("x / y");
      return x / y;
  }

  public static void Main(string[] args){
    // alte Variante
    // Calc computer1 = new Calc(Divide);
    // neue Variante:
    // Calc computer2 = Divide;
    Calc computer3 = Add;
    computer3 += Multiply;
    computer3 += Multiply;
    computer3 += Divide;
    computer3 -= Add;
    Console.WriteLine("Zahl von eingebundenen Delegates {0}",
                      computer3.GetInvocationList().GetLength(0));
    Console.WriteLine("Ergebnis des letzten Methodenaufrufes {0}",
                                                      computer3(15, 5));
  }
}
```
@LIA.eval(`["main.cs"]`, `mcs main.cs`, `mono main.exe`)

> **Merke:** Es werden alle referenzierten Methoden ausgeführt - der Rückgabewert des Aufrufes entspricht dem der letzten Methode.
> Wir haben es nicht mit einer Verkettung von Methoden zu tuen, sondern mit einer Liste separater Aufrufe!

```csharp           ChainedMultiCast
using System;
using System.Reflection;

public class Program
{
  public delegate int Transformierer(int eingabe);

  static int Verdoppeln(int x) => x * 2;
  static int PlusEins(int x) => x + 1;
  static int Quadrieren(int x) => x * x;

  static Transformierer Verkette(Transformierer a, Transformierer b)
  {
      return x => b(a(x)); // Ergebnis von a wird an b weitergereicht
  }

  public static void Main(string[] args){
        Transformierer pipeline = Verkette(Verdoppeln, PlusEins);
        pipeline = Verkette(pipeline, Quadrieren);

        int ergebnis = pipeline(3); // ((3 * 2) + 1)^2 = 49
        Console.WriteLine(ergebnis); // Ausgabe: 49
  }
}
```
@LIA.eval(`["main.cs"]`, `mcs main.cs`, `mono main.exe`)

### Was passiert hinter den Kulissen?

Was wird anhand des Aufrufes

```
NotifyUser notifyMe = new NotifyUser(NotifyUserByText);
```

deutlich? Handelt es sich bei `notifyMe` wirklich nur um eine Methode?

Delegattypen werden von der `Delegate`-Klasse im .NET Framework abgeleitet.

https://learn.microsoft.com/de-de/dotnet/api/system.delegate?view=net-9.0

Wenn der C#-Compiler Delegiertentypen verarbeitet, erzeugt er automatisch eine versiegelte Klassenableitung aus `System.MulticastDelegate`. Diese Klasse (in Verbindung mit ihrer Basisklasse, `System.Delegate`) stellt die notwendige Infrastruktur zur Verfügung, damit der Delegierte eine Liste von Methoden vorhalten kann. Der Compiler erzeugt insbesondere drei Methoden, um diese  aufzurufen:

+ die synchrone Invoke()-Methode, die aber nicht explizit von Ihrem C#-Code aufgerufen wird
+ die asynchrone `BeginInvoke()` und
+ `EndInvoke()` als Methoden, die die Möglichkeit bieten, die die eigentliche Methode in einem separaten Ausführungsthread zu handhaben.

Entsprechend der Codezeile `delegate int Transformer(int x);` generiert der
Compiler eine spezielle `sealed class Transformer`

```csharp
sealed class Transformer : System.MulticastDelegate
{
  ...
  public int Invoke(int x);
  public IAsyncResult BeginInvoke(int x, AsyncCallback cb, object state);
  public int EndInvoke(IAsyncResult result);
  ...
}
```

Die Vererbungsbeziehung lässt sich wie folgt zusammenfassen: Die vom Compiler
erzeugte Klasse (hier exemplarisch `Transformer`) erbt von `MulticastDelegate`
und damit von `Delegate`. Die Felder `Target` (das Empfängerobjekt) und `Method`
(welche Methode) werden aus `System.Delegate` _geerbt_; die signaturspezifische
`Invoke`-Methode steuert der Compiler je Delegat-Typ bei.

```text @plantUML
@startuml
skinparam classAttributeIconSize 0

abstract class "System.Delegate" as Delegate {
  + Target : object
  + Method : MethodInfo
}

abstract class "System.MulticastDelegate" as Multicast {
  - _invocationList : object[]
}

class "Transformer" as Transformer << sealed, generiert >> {
  + Invoke(x : int) : int
  + BeginInvoke(...) : IAsyncResult
  + EndInvoke(...) : int
}

Delegate <|-- Multicast
Multicast <|-- Transformer

' has-a: ein Multicast-Delegat enthaelt selbst eine Liste von Delegaten
Multicast "1" o-- "0..*" Delegate : _invocationList

note right of Delegate
  Target + Method = Verweis auf
  "welche Methode auf welchem Objekt"
end note

note right of Multicast
  Zwei Beziehungen zugleich:
  * is-a  : IST ein Delegate (Vererbung)
  * has-a : ENTHAELT eine Liste von
            Delegaten -> ermoeglicht +=
  (vgl. Composite-Pattern)
end note

note bottom of Transformer
  Pro 'delegate'-Deklaration erzeugt
  der Compiler eine eigene, 'sealed'
  Klasse mit passender Invoke-Signatur.
end note
@enduml
```

> [!NOTE]
> **Alle** in C# deklarierten Delegaten (auch `Action`, `Func<>`, `EventHandler`)
> erben von `MulticastDelegate` — nie direkt von `Delegate`. Deshalb ist jeder
> Delegat von Haus aus multicast-fähig (`+=`), selbst wenn nur eine einzige
> Methode angehängt ist.



### Schnittstellen vs. Delegaten

```csharp    DelegatesVsInterfaces
delegate void XYZ(int p);

interface IXyz {
    void doit(int p);
}

class One {
    // All four methods below can be used to implement the XYZ delegate
    void XYZ1(int p) {...}
    void XYZ2(int p) {...}
    void XYZ3(int p) {...}
    void XYZ4(int p) {...}
}

class Two : IXyz {
    public void doit(int p) {
        // Only this method could be used to call an implementation through an interface
    }
}
```

Sowohl Delegaten als auch Schnittstellen ermöglichen einem Klassendesigner,
Typdeklarationen und Implementierungen zu trennen. Eine bestimmte Schnittstelle
kann von jeder Klasse oder Struktur implementiert werden. Ein Delegat
kann für eine Methode in einer beliebigen Klasse erstellt werden, sofern die
Methode zur Methodensignatur des Delegaten passt.


| Delegaten                                                                                            | Schnittstelle                                                                                    |
| ---------------------------------------------------------------------------------------------------- | ------------------------------------------------------------------------------------------------ |
| repräsentiert eine Methodensignatur                                                                  | wenn eine Klasse eine Schnittstelle implementiert, dann implementiert sie deren gesamte Methoden |
| lässt sich nur auf Methoden anwenden                                                                 | deckt sowohl Methoden als auch Eigenschaften ab                                                  |
| wird für die Behandlung von Ereignissen verwendet                                                    |                             |
| kann auf anonyme Methoden zugreifen                                                                  | kann nicht auf anonyme Methoden zugreifen.                                                       |
| beim Zugriff auf eine Methode über Delegaten ist kein Zugriff auf das Objekt der Klasse erforderlich | beim Methodenzugriff benötigen Sie das Objekt der Klasse, die eine Schnittstelle implementiert   |
| unterstützt keine Vererbung                                                                          | unterstützt Vererbung                                                                            |
| wird zur Laufzeit erstellt                                                                           | wird zur Kompilierzeit erstellt                                                                  |
| kann statische Methoden und Methoden versiegelter Klassen einschließen.                              | schließt statische Methoden und Methoden versiegelter Klassen nicht ein                          |
| kann jede Methode implementieren, die die gleiche Signatur wie der angegebene Delegat aufweist       | in der implementierenden Klasse wird die Methode mit gleichen Namen und Signatur überschrieben   |


> Merke: In beiden Fällen kann die Schnittstellenreferenz oder ein Delegat
> von einem  Objekt verwendet werden, das keine Kenntnis von der Klasse hat, die
> die  Schnittstellen- oder Delegatmethode implementiert.

Wann ist welche der beiden Varianten vorzuziehen? Verwenden Sie einen Delegaten
unter folgenden Umständen:

+ Ein Ereignis-Entwurfsmuster wird verwendet.
+ Es ist wünschenswert, eine statische Methode einzukapseln.
+ Der Aufrufer muss nicht auf andere Eigenschaften, Methoden oder Schnittstellen des Objekts zugreifen, das die Methode implementiert.
+ Eine einfache Zusammensetzung ist erwünscht.
+ Eine Klasse benötigt möglicherweise mehr als eine Implementierung der Methode (siehe oben).

Verwenden Sie eine Schnittstelle wenn:

+ Es gibt eine Gruppe verwandter Methoden, die aufgerufen werden können.
+ Eine Klasse benötigt nur eine Implementierung der Methodesignatur.
+ Die Klasse, die die Schnittstelle verwendet, möchte diese Schnittstelle in andere Schnittstellen- oder Klassentypen umwandeln.
+ Die implementierte Methode ist mit dem Typ oder der Identität der Klasse verknüpft, z. B. mit Vergleichsmethoden.

Ein Beispiel für die kombinierte Anwendung eines Delegaten ist
`IComparable` oder die generische Version `IComparable<T>`. `IComparable`
deklariert die `CompareTo`-Methode, die eine Ganzzahl zurückgibt, die eine
Beziehung angibt, die kleiner, gleich oder größer als zwei Objekte desselben
Typs ist. Damit kann `IComparable` Grundlage für einen Sortieralgorithmus
verwendet werden. Alternativ kann aber auch eine Delegatenvergleichsmethode
übergeben werden.

Obwohl die Verwendung einer Delegatenvergleichsmethode als
Grundlage eines Sortieralgorithmus gültig ist, gestaltet sich die fehlende Zuordnung nicht ideal. Da die
Fähigkeit zum Vergleichen zur Klasse gehört und sich der Vergleichsalgorithmus
zur Laufzeit nicht ändert, ist eine Einzelmethodenschnittstelle ideal.

```csharp     ComparableExample.cs
using System;
using System.Collections.Generic;

public class Student :  IComparable<Student>
{
    public string Name { get; set; }
    public int Matrikel { get; set; }

    public override string ToString()
    {
        return "ID: " + Matrikel + "   Name: " + Name;
    }

    // Default Vergleicher 
    public int CompareTo(Student compareStudent)
    {
        if (compareStudent == null)
            return 1;
        else
            return this.Matrikel.CompareTo(compareStudent.Matrikel);
    }
}

public class Example
{
    // Alternativer Vergleicher, der als Delegate übergeben wird
    public static int CompareStudentsByName(Student x, Student y)
    {
        if (x.Name == null && y.Name == null) return 0;
        else if (x.Name == null) return -1;
        else if (y.Name == null) return 1;
        else return x.Name.CompareTo(y.Name);
    }

    public static void Main()
    {
        List<Student> students = new List<Student>();
        students.Add(new Student() { Name = "Cotta", Matrikel = 1434 });
        students.Add(new Student() { Name= "Humboldt", Matrikel = 1234 });
        students.Add(new Student() { Name = "Zeuner", Matrikel = 1634 }); ;
        // Hier wird der Name nicht gesetzt
        students.Add(new Student() {  Matrikel = 1334 });
        students.Add(new Student() { Name = "Hardenberg", Matrikel = 1444 });
        students.Add(new Student() { Name = "Winkler", Matrikel = 1534 });

        Console.WriteLine("\nBefore sort:");
        foreach (var aStudent in students)
        {
            Console.WriteLine(aStudent);
        }

        // Call Sort on the list. This will use the default compare method
        students.Sort();

        Console.WriteLine("\nAfter sort by matrikel number:");
        foreach (var aStudent in students)
        {
            Console.WriteLine(aStudent);
        }

        // This shows calling the Sort(Comparison(T) overload using
        // a generic predefined delegate
        Comparison<Student> handler = new Comparison<Student>(CompareStudentsByName);
        students.Sort(handler);

        Console.WriteLine("\nAfter sort by name:");
        foreach (var aStudent in students)
        {
            Console.WriteLine(aStudent);
        }
    }
}
```
@LIA.evalWithDebug(`["main.cs"]`, `mcs main.cs`, `mono main.exe`)

## Praktische Implementierung

> Bislang bezogen sich unsere Delegaten immer auf statische Methoden oder Methoden einer Klasseninstanz. Darüber hinaus können hier aber auch alternative Formate genutzt werden, um die eigentliche Implementierung zu realisieren.

Neben dem Basiskonzept der Delegaten können in C# spezifischere Realisierungen
umgesetzt werden, die die Anwendung flexibler bzw. effizienter machen.

### Anonyme / Lambda Funktionen

Entwicklungshistorie von C# in Bezug auf Delegaten

| Version  | Delegatendefinition |
| -------- | ------------------- |
| $<2.0$   | benannte Methoden   |
| $>= 2.0$ | anonyme Methoden    |
| ab $3.0$ | Lambdaausdrücke     |

Dabei lösen Lambdaausdrücke die anonymen Methoden als bevorzugten Weg zum Schreiben von Inlinecode ab. Allerdings bieten anonyme Methode eine Funktion, über die Lambdaausdrücke nicht verfügen. Anonyme Methoden ermöglichen das Auslassen der Parameterliste. Das bedeutet, dass eine anonyme Methode in Delegaten mit verschiedenen Signaturen konvertiert werden kann.

**Anonyme Methoden**

Das Erstellen anonymer Methoden verkürzt den Code, da nunmehr ein Codeblock als Delegatparameter übergeben wird.

```csharp
// Declare a delegate pointing at an anonymous function.
DelgatenType d = delegate(int k) { /* ... */ };

```
Das folgende Codebeispiel illustriert die Verwendung. Dabei wird auch deutlich, wie
eine Methodenreferenz durch eine andere ersetzt werden kann.

```csharp           AnonymouseDelegate
using System;
using System.Reflection;
using System.Collections.Generic;

// Declare a delegate.
delegate void Printer(string s);

public class Program{

  static void DoWork(string k)
  {
      System.Console.WriteLine(k);
  }

  public static void Main(string[] args){
    // Anonyme Deklaration
    Printer p = delegate(string j)
    {
        Console.WriteLine(j);
    };

    p("The delegate using the anonymous method is called.");
    // Der existierende Delegat wird nun mit einer konkreten Methode
    // verknüpft
    p = DoWork;
  }
}
```
@LIA.eval(`["main.cs"]`, `mcs main.cs`, `mono main.exe`)


**Lambda Funktionen**

Ein Lambdaausdruck ist ein Codeblock, der wie ein Objekt behandelt wird. Er kann als Argument an eine Methode übergeben werden und er kann auch von Methodenaufrufen zurückgegeben werden.

```csharp
(<Paramter>) => { expression or statement; }

(int a) => a * 2;             // einzelner Ausdruck - Ausdruckslambda
(int a) => { return a * 2; }; // Anweisungsblock    - Anweisungslambda

```

```csharp           LambdaDelegate
using System;
using System.Reflection;
using System.Collections.Generic;

public class Program
{
  public delegate int Del( int Value);
  public static void Main(string[] args){
      Del obj = (Value) => {
              int x=Value*2;
              return x;
      };
      Console.WriteLine(obj(5));
  }
}
```
@LIA.eval(`["main.cs"]`, `mcs main.cs`, `mono main.exe`)

Jeder Lambdaausdruck kann in einen Delegat-Typ konvertiert werden. Der Delegat-Typ, in den ein Lambdaausdruck konvertiert werden kann, wird durch die Typen seiner Parameter und Rückgabewerte definiert.

```csharp           LambdaDelegate
using System;  
using System.Collections.Generic;  

public static class demo  
{  
  public static void Main()  
  {  
      List<int> list = new List<int>() { 1, 2, 3, 4, 5, 6 };  
      List<int> evenNumbers = list.FindAll(x => (x % 2) == 0);  
      foreach (var num in evenNumbers)  
      {  
          Console.Write("{0} ", num);  
      }  
      Console.WriteLine();  
      Console.Read();  
  }  
}
```
@LIA.eval(`["main.cs"]`, `mcs main.cs`, `mono main.exe`)

### Generische Delegaten

Delegaten können auch als Generics realisiert werden. Das folgende Beispiel
wendet ein Delegate "Transformer" auf ein Array von Werten an. Dabei stellt
C# sicher, dass der Typ der übergebenen Parameter in der gesamten Verarbeitungskette übernommen wird.

```csharp           GenericDelegates.cs
using System;
using System.Reflection;
using System.Collections.Generic;

// Schritt I - Generisches Delegat
delegate T Transformer<T>(T x);

class Utility
{
  public static void Transform<T>(ref T[] values, Transformer<T> trans)
  {
      for (int i = 0; i < values.Length; ++i)
          values[i] = trans(values[i]);
  }
}

public class Program
{
  // Schritt II - Spezifische Methode, die der Delegatensignatur entspricht
  static int Square(int x){
      Console.WriteLine("This is method Square(int x)");
      return x*x;
  }

  static double Square(double x){
      Console.WriteLine("This is method Square(double x)");
      return x*x;
  }

  // Ausgabefunktion für die Visualisierung der Ergebnisse
  static void printArray<T>(T[] values){
      foreach(T i in values)
          Console.Write(i + " ");
      Console.WriteLine();
  }

  public static void Main(string[] args){
    int[] values = { 1, 2, 3 };
    printArray<int>(values);

    Transformer <int> t = new Transformer<int>(Square);
    Utility.Transform<int>(ref values, t);
    printArray(values);
  }
}
```
@LIA.eval(`["main.cs"]`, `mcs main.cs`, `mono main.exe`)


### Action / Func

Der generischen Idee entsprechend kann man auf die explizite Definition von
eigenen Delegates vollständig verzichten. C# implementiert dafür
zwei Typen vor:

```csharp
delegate TResult Func<out TResult>();
delegate TResult Func<in T1, out TResult>(T1 arg1);
delegate TResult Func<in T1, in T2, out TResult>(T1 arg1, T2 arg2);
delegate TResult Func<in T1, in T2, in T3, out TResult>(T1 arg1, T2 arg2, T3 arg3);

delegate void Action();
delegate void Action<in T1>(T1 arg1);
delegate void Action<in T1, in T2>(T1 arg1, T2);
delegate void Action<in T1, in T2, in T3>(T1 arg1, T2 arg2, T3 arg3);
```

Im folgenden Beispiel wir die Anwendung illustriert. Dabei werden 3 Delegates
genutzt um die Funktionen `PrintHello` und `Square()` zu referenzieren.

| Delegate-Variante | Bedeutung                                                                 |
| ----------------- | ------------------------------------------------------------------------- |
| myOutput          | C#1.0 Version mit konkreter Methode und individuellem Delegaten (Zeile 8) |
| myActionOutput    | Generischer Delegatentyp ohne Rückgabewert `Action`                       |
| myFuncOutput      | Generischer Delegatentyp mit Rückgabewert `Func`                          |


```csharp           ActionUndFunc
using System;
using System.Reflection;
using System.Collections.Generic;

public delegate void Output(string text);

public class Program
{
  static void PrintHello(string text)
  {
      Console.WriteLine(text);
  }

  static int Square(int x)
  {
      Console.WriteLine("This is method Square(int x)");
      return x*x;
  }

  static double Square(double x)
  {
      Console.WriteLine("This is method Square(double x)");
      return x*x;
  }

  public static void Main(string[] args){
     Output myOutput = PrintHello;
     myOutput("Das ist eine Textausgabe");
     Action<string> myActionOutput = PrintHello;
     myActionOutput("Das ist eine Action-Testausgabe!");
     Func<double, double> myFuncOutput = Square;
     Console.WriteLine(myFuncOutput(5));
  }
}
```
@LIA.eval(`["main.cs"]`, `mcs main.cs`, `mono main.exe`)

Natürlich lassen sich auf `Func` und `Action` auch anonyme Methoden und Lambda Expressions anwenden!

```
Func<string, string> MyLambdaAction = text => text + "modifed by Lambda";
Console.WriteLine(MyLambdaAction("Tests"));
```

Warum würde die Verwendung von Action an dieser Stelle einen Fehler generieren?

### Praxisbeispiel: Kollisions-Callbacks in einer Physik-Engine

> Wo werden Delegaten "im echten Leben" eingesetzt? Ein klassisches Anwendungsfeld sind Spiele- und Physik-Engines (z.B. Unity oder Godot, das ebenfalls auf C# setzt). Die Engine erkennt, *dass* zwei Objekte kollidieren - was dann passieren soll, weiß nur der Entwickler des konkreten Spiels.

Die Engine soll universell sein: Sie kennt weder die Spielregeln noch die Objekte
des konkreten Spiels. Deshalb übergibt der Entwickler jedem Objekt eine Reaktion
in Form eines `Action`-Delegaten. Beim Zusammenstoß ruft die Engine diese auf -
ganz ohne zu wissen, *was* dabei geschieht.

```csharp           CollisionCallback
using System;
using System.Collections.Generic;

// Ein Spielobjekt mit Position, Größe und einer Reaktion auf Kollisionen
public class GameObject
{
  public string Name;
  public double X;
  public double Width;

  // Die Reaktion ist NICHT fest einprogrammiert, sondern wird von außen
  // hineingereicht: Welches andere Objekt wurde getroffen?
  public Action<GameObject> OnCollision;
}

// Die Engine kennt nur Geometrie - nicht die Spielregeln
public class PhysicsEngine
{
  private List<GameObject> objects = new List<GameObject>();

  public void Add(GameObject obj) => objects.Add(obj);

  public void DetectCollisions()
  {
    for (int i = 0; i < objects.Count; i++)
      for (int j = i + 1; j < objects.Count; j++)
      {
        var a = objects[i];
        var b = objects[j];
        bool overlap = Math.Abs(a.X - b.X) < (a.Width + b.Width) / 2;

        if (overlap)
        {
          // Die Engine ruft nur den hinterlegten Delegaten auf
          a.OnCollision?.Invoke(b);
          b.OnCollision?.Invoke(a);
        }
      }
  }
}

public class Program
{
  public static void Main(string[] args)
  {
    var engine = new PhysicsEngine();

    // Der Spieler reagiert mit einer benannten Methode
    var player = new GameObject { Name = "Spieler", X = 10, Width = 4,
                                  OnCollision = PlayerHit };

    // Ein Münzobjekt reagiert mit einem Lambda - kurz und lokal definiert
    var coin = new GameObject { Name = "Münze", X = 12, Width = 2,
        OnCollision = other => Console.WriteLine($"  {other.Name} sammelt die Münze ein (+10 Punkte)") };

    // Eine Wand braucht gar keine Reaktion (Delegat bleibt null -> ?. greift)
    var wall = new GameObject { Name = "Wand", X = 100, Width = 4 };

    engine.Add(player);
    engine.Add(coin);
    engine.Add(wall);

    engine.DetectCollisions();
  }

  static void PlayerHit(GameObject other)
  {
    Console.WriteLine($"  Spieler kollidiert mit {other.Name}!");
  }
}
```
@LIA.eval(`["main.cs"]`, `mcs main.cs`, `mono main.exe`)

Was zeigt dieses Beispiel?

+ Die `PhysicsEngine` ist **vollständig vom Spiel entkoppelt** - sie ruft nur `OnCollision` auf, ohne die konkreten Reaktionen zu kennen.
+ Jedes Objekt bringt seine **eigene Reaktion** mit: als benannte Methode (`PlayerHit`), als Lambda (Münze) oder gar nicht (Wand).
+ Der `?.`-Operator (`OnCollision?.Invoke(...)`) schützt vor dem Aufruf eines nicht gesetzten Delegaten (`null`).
+ Echte Engines wie Godot (das ebenfalls auf C# setzt) lösen genau dieses Muster über **Events** bzw. **Signals** - technisch eine Erweiterung des Delegaten. Wir greifen Godot als konkretes Praxisbeispiel in der **nächsten Vorlesung (Events)** wieder auf.

## Abgrenzung zu c++

> Gibt es in C++ Delegaten?

In C++ gibt es kein direktes Äquivalent zu C#-Delegaten. Stattdessen werden in C++ Funktionszeiger oder Funktionsobjekte (auch bekannt als Funktoren) verwendet, um ähnliche Funktionalitäten zu erreichen.

```c++           CppDelegates.cpp
#include <iostream>
#include <functional>  // std::function, std::bind
using namespace std;

// Normale Funktion
void PrintHello(string text)
{
    cout << text << endl;
}

// Normale Funktion
void Square(int x)
{
    cout << "This is method Square(int x)" << endl;
    cout << x * x << endl;
}

// Funktion mit Rückgabewert
int SquareReturn(int x)
{
    return x * x;
}

int main()
{
    // --- Funktionszeiger ---
    void (*myOutput)(string) = PrintHello;
    myOutput("Das ist eine Textausgabe");

    // --- Funktionsobjekt (Lambda) ---
    auto myLambdaAction = [](string text) {
        cout << text << " modifiziert durch Lambda" << endl;
    };
    myLambdaAction("Tests");

    // --- Funktionsobjekt (Lambda mit Rückgabewert) ---
    auto myFuncOutput = [](int x) {
        return x * x;
    };
    cout << myFuncOutput(5) << endl;

    // --- std::function mit freier Funktion ---
    std::function<void(string)> f1 = PrintHello;
    f1("Ausgabe über std::function mit freier Funktion");

    // --- std::function mit Lambda ---
    std::function<void(string)> f2 = [](string t) {
        cout << "Lambda via std::function: " << t << endl;
    };
    f2("Test");

    // --- std::function mit std::bind (z. B. um Argumente zu binden) ---
    std::function<void()> f3 = std::bind(Square, 7);
    f3();  // Gibt 49 aus

    // --- std::function mit Rückgabewert ---
    std::function<int(int)> f4 = SquareReturn;
    cout << "Quadrat über std::function<int(int)>: " << f4(6) << endl;

    return 0;
}
```
@LIA.eval(`["main.cpp"]`, `g++ -Wall main.cpp -o main`, `./main`)


## Exkurs: Und in Python? — von First-Class-Funktionen zu Dekoratoren

Python kennt kein eigenes Sprachkonstrukt `delegate`. Es braucht keines, weil
Funktionen hier _First-Class-Objekte_ sind: Eine Funktion ist einfach ein Wert,
den man in einer Variablen halten, als Parameter übergeben und zurückgeben kann.
Was C# über den _Typ_ `delegate` (bzw. `Action`/`Func`) formalisiert, ist in der
dynamisch typisierten Sprache Python bereits implizit vorhanden — siehe das
Python-Beispiel im Motivationsteil, in dem `quadriere` an `wende_an` übergeben
wird.

Am deutlichsten wird das, wenn man eine Funktion einer Variablen _zuweist_ und
anschließend über diese Variable aufruft — ganz so, wie man es mit einer Zahl
oder einem String täte:

```python
def quadriere(x):
    return x * x

# Die Funktion wird wie ein Wert in einer Variablen abgelegt
operation = quadriere      # kein Aufruf! (keine Klammern)

print(operation)           # <function quadriere at 0x...>
print(operation(5))        # Aufruf über die Variable -> 25
```
@LIA.eval(`["main.py"]`, `none`, `python3 main.py`, `*`)

Genau aus dieser Eigenschaft entsteht ein nächster Schritt: der **Dekorator**.
Ein Dekorator ist eine Funktion, die eine Funktion _entgegennimmt_ (wie ein
Delegat) **und** eine _neue_ Funktion _zurückgibt_ — also eine Funktion, die
Funktionen transformiert (eine _Higher-Order-Function_).

```python
def log_call(func):               # nimmt eine Funktion (wie ein Delegat)
    def wrapper(*args, **kwargs):  # baut eine neue Funktion drumherum
        print("Aufruf von", func.__name__)
        return func(*args, **kwargs)
    return wrapper                 # gibt eine Funktion zurück -> das Neue!

@log_call
def quadriere(x):
    return x * x

print(quadriere(5))
```
@LIA.eval(`["main.py"]`, `none`, `python3 main.py`, `*`)

Die Schreibweise `@log_call` ist dabei nur _Syntactic Sugar_ für

```python
quadriere = log_call(quadriere)
```

> [!IMPORTANT] 
> Das Python-Sprachfeature `@decorator` ist nicht
> identisch mit dem _Decorator-Pattern_ aus der Vorlesung zu den Design Patterns.
> Beide verfolgen dieselbe _Absicht_ (Verhalten ergänzen, ohne die ursprüngliche
> Funktion/Klasse zu ändern), aber das GoF-Pattern wird in C# klassisch über
> _Interfaces + Wrapper-Klassen_ realisiert.


## Aufgaben der Woche

!?[alt-text](https://www.youtube.com/watch?v=R8Blt5c-Vi4)
