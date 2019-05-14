<!--

author:   Sebastian Zug & André Dietrich
email:    zug@ovgu.de   & andre.dietrich@ovgu.de
version:  0.0.1
language: de
narrator: Deutsch Female

import: https://raw.githubusercontent.com/liaScript/rextester_template/master/README.md

-->

# Vorlesung Softwareentwicklung - 7 - OOP in C#

--------------------------------------------------------------------
Link auf die aktuelle Vorlesung im Versionsmanagementsystem GitHub

https://github.com/liaScript/CsharpCourse/blob/master/07_OOP_Csharp.md

Die interaktive Form ist unter diese Link zu finden ->
[LiaScript Vorlesung 7](https://liascript.github.io/course/?https://raw.githubusercontent.com/liaScript/CsharpCourse/master/07_OOP_Csharp.md#1)

---------------------------------------------------------------------

**Wie weit sind wir schon gekommen?**

c# Schlüsselwörter:

| abstract    | as       | base     |`bool`      |`break`     |`byte`     |
|`case`       |`catch`   |`char`    |`checked`   |`class`     | const     |
|`continue`   |`decimal` | default  | delegate   |`do`        |`double`   |
|`else`       |`enum`    | event    | explicit   | extern     |`false`    |
|`finally`    | fixed    |`float`   |`for`       |`foreach`   |`goto`     |
|`if`         | implicit | in       |`int`       | interface  |`internal` |
| is          | lock     |`long`    |`namespace` |`new`       |`null`     |
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

*1. Was sind Felder in der C# Welt?*

Variablen beliebigen Typs, die einer Klasse unmittelbar zugeordnet sind. In
Feldern werden Daten abgelegt, die klassenweit Verwendung finden. Als Felder
werden nur methodenrelevante Variablen integriert.

--------------------------------------------------------------------

*2. Erklären Sie die Unterschied zwischen Instanzfeldern und statischen Feldern.*

Instanzfelder beziehen sich auf eine individuelle Instanz der Klasse, während
statische Felder über allen Instanzen wirken.

--------------------------------------------------------------------

*3. Was passiert, wenn für eine Klasse kein Konstruktor vorgegeben wird?*

Der Kompiler erstellt automatisch einen Standardkonstruktor, sobald ein einziger
Konstruktor definiert wird, entfällt dieser Schritt.

---------------------------------------------------------------------

*4. Welche Wirkung haben fehlende Set und Get Implmentierungen bei Properties?*

ohne get ... lesegeschützt, ohne set ... schreibgeschützt. Properties können aber auch
durch Zugriffsattribute feingranulare Regeln definieren.  

---------------------------------------------------------------------

*5. Wozu verwendet man Indexer?*

---------------------------------------------------------------------

*6. Welche Varianten stehen unter C# zur Verfügung, um konstante Werte zu definieren?*

---------------------------------------------------------------------

## 1. Klassen in C#

> Klassen [und Strukturen] sind zwei der grundlegenden Konstrukte des allgemeinen Typsystems in .NET Framework. Bei beiden handelt es sich um eine Datenstruktur, die einen als logische Einheit zusammengehörenden Satz von Daten und Verhalten kapselt.

                                  {{0-1}}
******************************************************************************

Entsprechend können Klassenspezifikationen folgende Elemente umfassen:

| Member / Elemente   | englische Bezeichnung | Funktion                                                                                    |
|:------------------- | --------------------- |:------------------------------------------------------------------------------------------- |
| Felder              | *fields*              | Daten                                                                                       |
| Konstanten          | *constant*            | konstante Daten                                                                             |
| Eigenschaften       | *property*            | Daten und Zugriffsmethoden                                                                  |
| Methoden            | *method*              | Funktionen / Prozeduren                                                                     |
| Konstruktoren       | *constructor*         | Instanziierung einer Klasse                                                                 |
| Ereignisse          | *event*               | Informationsaustausch zwischen Klassen                                                      |
| Finalizer           | *finalizer*           | "Destruktoren"                                                                              |
| Indexer             | *indexer*             | Ähnlich Eigenschaften, Adressierung über Indizes                                            |
| Operatoren          | *operators*           | Set von '==', '+' etc. mit eigener Bedeutung                                                |
| Geschachtelte Typen | *embedded types*      | Integrierte Klassen oder Structs, die nur innerhalb einer Klasse/ Structs angewendet werden |

******************************************************************************
                                     {{1-2}}
******************************************************************************

```csharp         Klassenelemente
class Person{
  // *************** Felder ************************************************
  string name;                     // beachte Groß/ Kleinschreibung
  public int Geburtsjahr;          // der Variablennamen!

  // ************** Konstruktoren ******************************************
  public Person(string name, int geburtsjahr){
    this.name = name;
    Geburtsjahr = geburtsjahr;
  }

  // ************** Methoden ***********************************************
  int AktuellesAlter () => DateTime.Today.Year - Geburtsjahr;

  public override string ToString(){
     return name + " ist " + AktuellesAlter().ToString() + "Jahre alt."
  }

  // ************* Operatoren **********************************************
  public static bool operator< (Person person1, Person Person2){
    // TODO Hausaufgabe  
  }
}

```

Und wie legen wir eine Instanz an? Dazu sind mehrere Schritte notwendig:

```csharp  
Person p;  // Generierung einer Referenzvariablen p auf dem Stack
p = new Person();  // Generierung einer Instanz im Heap
// alles zusammen
// Person p = new Person();
```

Als Operanden erwartet der new-Operator einen Klassennamen und eine Parameterliste,
die an den entsprechenden Konstruktor übergeben wird.

Vgl. obriges Beispiel mit Fehlern unter [Link](https://github.com/liaScript/CsharpCourse/tree/master/code/07_OOPII/PersonManagement_with_errors). Identifizieren Sie die Fehler und
korrigieren Sie diese.

******************************************************************************
                                     {{2-3}}
******************************************************************************

|                              | Fields                                       | Methods                                            |
| ---------------------------- | -------------------------------------------- | -------------------------------------------------- |
| Statisches Attribut          | `static`                                     | `static`                                           |
| Zugriffsattribute            | `public`, `internal`, `private`, `protected` | `public`, `internal`, `private`, `protected`       |
| Vererbungsattribut           | `new`                                        | `new`, `virtual`, `abstract`, `override`, `sealed` |
| Unsafe Attribute             | `unsafe`                                     |                                                    |
| Attribut Teilimplementierung |                                              | `partitial`                                        |
| Unmanaged Code Attribute     |                                              | `unsafe extern`                                    |
| Read-only Attribute          | `readonly`                                   |                                                    |
| Threading Attribute          | `volatile`                                   |                                                    |

******************************************************************************

### Felder

                                     {{0-1}}
******************************************************************************

Felder sind Variablen eines beliebigen Typs, die einer Klasse unmittelbar
zugeordnet sind. In Feldern werden die Daten abgelegt, die übergreifend
Verwendung finden.

Der Idee der Kapselung folgend, sollten nur methodenlokal relevante Variablen
auch dort deklariert werden.

Eine Klasse oder Struktur kann Instanzenfelder, statische Felder oder beides
gemischt verfügen.

<!--
style="width: 90%; max-width: 560px; display: block; margin-left: auto; margin-right: auto;"
-->
```ascii   

  +-----------------+   +-----------------+  +-----------------+        
  | Instanz 0       |   | Instanz 1       |  | Instanz 2       |       
  +-----------------+   +-----------------+  +-----------------+       
  | - Intanzfeld0   |   | - Intanzfeld0   |  | - Intanzfeld0   |
  | - Intanzfeld1   |   | - Intanzfeld1   |  | - Intanzfeld1   |
  |                 |   |                 |  |                 |
  | ..................... ✛ StatischesFeld0 .................. |
  | ..................... ✛ StatischesFeld1 .................. |  
  |                 |   |                 |  |                 |      
  +-----------------+   +-----------------+  +-----------------+
  | ✛ Method1()     |   | ✛ Method1()     |  | ✛ Method1()     |
  | ✛ Method2()     |   | ✛ Method2()     |  | ✛ Method2()     |     
  +-----------------+   +-----------------+  +-----------------+
```

Instanzenfelder beziehen sich als Datensat individuell auf die "eigene" Instanz,
statisches Felder gehören zur Klasse selbst und werden von allen Instanzen einer
Klasse gemeinsam verwendet. "Lokale" Änderungen, werden somit übergreifend
sichtbar.

```csharp    AnwendungStatischeVariablen
using System;
using System.Collections;
using System.Collections.Generic;

namespace Rextester
{
  public class Person{
    string name;    
    int index;             
    public int Geburtsjahr;          
    public static int Count;          // <- Statische Variable Count

    public Person(string name, int geburtsjahr){
      this.name = name;
      Geburtsjahr = geburtsjahr;
      index = Count;
      Count = Count + 1;
    }

    public override string ToString(){
       return name + " ist die " + index.ToString() + " von " + Count.ToString() + " Personen";
    }
  }

  public class Program
  {
    public static void Main(string[] args){
      Person Student1 = new Person("Mickey", 1935);
      Console.WriteLine(Student1);
      Person Student2 = new Person("Donald", 1927);
      Console.WriteLine(Student1);
      Console.WriteLine(Student2);
    }
  }
}
```
@Rextester.eval(@CSharp)

******************************************************************************

                                     {{1-2}}
******************************************************************************

Felder können mit der Deklaration oder im Konstruktor  initialisiert werden.
Desweiteren kann mit `readonly` der Wert nach dem Ende des
Konstruktorabarbeitung geschützt werden. Eine solche Variable kann als static
deklariert werden, um zu vermeiden, dass eine entsprechende Zahl von Kopien
erstellt wird.

```csharp          ReadOnlyExample
public class Person{
  string name;    
  int index = 0;
  readonly string Kategorie = "Student";
  readonly string Hochschule;

  public Person(){
    //...
    Hochschule = "TU Freiberg";
    //...
  }
```

******************************************************************************

### Konstanten

Konstanten sind unveränderliche Datensätze, die zur Kompilierzeit(!) bekannt
sind und sich danach nicht mehr verändern lassen. Nur die in C# integrierten
Typen - einfache Datentypen und System.Object - können als `const` deklariert
werden.

Varianten "konstanter" Variablen in C#

|                     | Konstante    | Readonly              | Readonly statisch     |
| ------------------- | ------------ | --------------------- | --------------------- |
| Attribute           | `const`      | `readonly`            | `readonly static`     |
| Veränderbar bis ... | Kompilierung | Ende des Konstruktors | Ende des Konstruktors |
| Statisch            | Standard, ja | Nein                  | Ja                    |
| Individuelle Kopien | Ja           | Ja                    | Nein                  |
| Zugriff             | Klasse       | Instanz               | Instanz               |

<!-- --{{0}}-- Idee des Codefragments:
  * Versuchen Sie die Variable innnerhalb von Main zu manipulieren
  * Wechseln Sie readonly gegen const? Welche Anpassungen müssen Sie vornehmen?
-->
```csharp    ReadOnlyVsConst
using System;

namespace Rextester
{
  public class Person{
    public readonly string name;    

    public Person(string name){
      this.name = name;
    }
  }

  public class Program
  {
    public static void Main(string[] args){
      Person Student1 = new Person("Mickey");
      Console.WriteLine(Student1.name);
    }
  }
}
```
@Rextester.eval(@CSharp)

### Konstuktoren

                                     {{0-1}}
******************************************************************************

Beim Erzeugen einer Instanz einer `class` oder eines `structs` wird deren
Konstruktor aufgerufen. Dieser ist für die Initialisierung der Instanz auf der
Zustandsebene verantwortlich. Konstruktoren können überladen werden und
verschiedene Signaturen abbilden.

Wenn für eine Klasse kein Konstuktor vorgegeben wird, erstellt der Kompiler
standardmäßig einen, der das Objekt instanziiert und Membervariablen auf die
Standardwerte festlegt.

```csharp    Constructors
public class Wine
{
   public decimal Price;
   public int Year;

   // public Wine() // <- Implzit vorhanden, kann aber überschrieben werden
                    // Standardkonstruktor
   public Wine (decimal price){Price = price;}
   public Wine (decimal price, int year) : this (price) {Year = year;}
}
```

Der Standardkonstruktor wird implizit generiert, wenn kein anderer Konstruktor
durch den Entwickler spezifiziert wurde. Sofern das geschieht, steht dieser auch
nicht mehr bereit.


******************************************************************************

                                     {{1-2}}
******************************************************************************

Ein Konstruktor kann einen anderen Konstruktor der gleichen Klasse über das
Schlüsselwort `this` aufrufen. Dabei kann der Aufruf mit oder ohne Parameter
erfolgen.

```csharp    ReadOnlyVsConst
using System;

namespace Rextester
{
  class Car
  {
    public readonly int NumberOfSeats;
    public readonly int MaxSpeed;
    private int CurrentSpeed;

    public Car(int maxSpeed, int numberOfSeats)
    {
       Console.WriteLine("2 arg ctor");
       this.MaxSpeed = maxSpeed;
       this.NumberOfSeats = numberOfSeats;
    }

    public Car(int maxSpeed) : this(maxSpeed, 5)
    {
       Console.WriteLine("1 arg ctor");
    }

    public Car() : this(100)
    {
       Console.WriteLine("0 arg ctor");
    }
  }

  public class Program
  {
    public static void Main(string[] args){
       Car myVehicle = new Car(5);
    }
  }
}
```
@Rextester.eval(@CSharp)



******************************************************************************

                                     {{2-3}}
******************************************************************************


**Statische Konstruktoren**

+ ... können nicht über Zugriffsmodifizierer oder Parameter verfügen.

+ ... werden automatisch vor dem Erzeugen der ersten Instanz ausgeführt und können nicht direkt aufgerufen werden. Damit hat der Nutzer keine Kontrolle, wann der Konstruktor ausgeführt wird.

+ ... werden kein zweites mal aufgerufen, wenn eine Ausnahme ausgelöst wird.

```csharp    StatitcConstructor
using System;

namespace Rextester
{
  public class BAFStudent
  {
     public static string Universität;
     public string NameStudent;

     static BAFStudent(){
       Console.WriteLine("Universität wird initialisiert");
       Universität = "TU BAF Freiberg";
     }

     public BAFStudent(string name){
       Console.WriteLine("Name wird initialisiert");
       NameStudent = name;
     }
  }

   public class Program
     {
       public static void Main(string[] args){
         BAFStudent student0 = new BAFStudent("Humboldt");
         Console.WriteLine("{0,20} - {1,-10}", BAFStudent.Universität, student0.NameStudent);
         BAFStudent student1 = new BAFStudent("Winkler");
         Console.WriteLine("{0,20} - {1,-10}", BAFStudent.Universität, student1.NameStudent);
       }
     }
   }

```
@Rextester.eval(@CSharp)

******************************************************************************

                                    {{3-4}}
******************************************************************************

Für die Objektinitialisierung besteht neben den Konstruktoren und dem
unmittelbaren Zugriff auf die Membervariablen (vermeiden!) die Möglichkeit
direkt nach dem Konstruktoraufruf die Belegung abzubilden.

```csharp    ObjectInitializer
using System;

namespace Rextester
{
  public class Wine
  {
     public decimal Price;
     public int Year;
     public string Vinyard;

     public Wine () {}
     public Wine (decimal price){Price = price;}
     public Wine (decimal price, int year, string vinyard = "Chateau Lafite" ){
       Price = price;
       Year = year;
       Vinyard = vinyard;
     }
     public override string ToString()
     {
       return String.Format("| {0,5} Euro | {1,5} | {2,-18}|", Price, Year, Vinyard );
     }
  }

   public class Program
     {
       public static void Main(string[] args){
         // Initalisierung über Standardkonstruktor und direkten Feldzugriff
         Wine bottle0 = new Wine();
         bottle0.Vinyard = "Chateau Latour";
         Console.WriteLine(bottle0);

         // Initialisierung über die Konstruktoren
         Wine bottle1 = new Wine(23);
         Console.WriteLine(bottle1);
         Wine bottle2 = new Wine(3432, 1956);
         Console.WriteLine(bottle2);

         // Initialisierung über Initalizer
         Wine bottle3 = new Wine() {Price = 19, Year = 1910};
         Console.WriteLine(bottle3);

       }
     }
   }

```
@Rextester.eval(@CSharp)

3 Varianten, und was ist nun besser? Der Aufruf über den Konstruktor ermöglicht
die Initialisierung von `readonly` Variablen.

Initalizer werden als atomare Funktion realisiert, sind damit Thread-sicher,
sind damit aber auch schwieriger zu debuggen. Zudem können nur `public`
Member damit adressiert werden. An dieser Stelle wird deutlich, dass
Initializier ggf. beim schnellen Testen Tipparbeit sparen, in realen Anwendungen
aber nicht zum Einsatz kommen sollten.

******************************************************************************

### Destruktoren / Finalizer

Mit Finalizern (die auch als Destruktoren bezeichnet werden) werden alle
erforderlichen endgültigen Bereinigungen durchgeführt, wenn eine Klasseninstanz
vom Garbage Collector gesammelt wird.

```csharp    ReadOnlyVsConst
using System;

namespace Rextester
{
  public class Person{
    public string name;    

    public Person(string name){this.name = name;}
    ~Person() {Console.WriteLine("The {0} destructor is executing.", ToString());}
  }

  public class Program
  {
    public static void Main(string[] args){
      Person Student1 = new Person("Mickey");
      Console.WriteLine(Student1.name);
      Console.WriteLine("Aus die Maus!");
    }
  }
}
```
@Rextester.eval(@CSharp)

Der Finalizer ruft implizit die Methode `Finalize` aus der Basisklasse des Typs
`Objekt` auf.

### Eigenschaften

Eigenschaft (Properties) organisieren den Zugriff auf private Felder über einen
flexiblen Mechanismus zum Lesen, Schreiben oder Berechnen des Wertes. Entsprechend
können Eigenschaften können wie öffentliche Datenmember verwendet werden. Damit
wird das Konzept der Kapselung auf effiziente Zugriffsmethoden abgebildet.

Ausgangspunkt:

<!-- --{{0}}-- Idee des Codefragments:
  * Fügen Sie eine Lese / Schreibmethode für die Variable Wochentag ein, die
    Prüft, ob die Eingabe zwischen Mo = 0 und Freitag = 5 liegt.
-->
```csharp    ReadOnlyVsConst
using System;

namespace Rextester
{
  public class Vorlesung{
    private byte wochentag;
  }

  public class Program
  {
    public static void Main(string[] args){
      Vorlesung SoWi = new Vorlesung();
      SoWi.wochentag = 4;
    }
  }
}
```
@Rextester.eval(@CSharp)

C# hält, wie andere OOP Sprachen auch dafür eine eigene kompakte Syntax bereit,
die Aspekte der Felder und der Methoden kombiniert. Der aufrufende Nutzer
sieht eine Feld, der Zugriff kann aber über eine Methode konfiguriert werden.
Dabei können durchaus mehrere Eigenschaften auf eine private Variable verweisen.

Für den Benutzer eines Objekts erscheint eine Eigenschaft wie ein Feld; der
Zugriff auf die Eigenschaft erfordert dieselbe Syntax.

```csharp    ReadOnlyVsConst
using System;

namespace Rextester
{
  public class Vorlesung
  {
    private byte wochentag;            // Private Variable

    public byte Wochentag              // Öffentliche Variable
    {
      get { return wochentag; }        // Property accessors
      set {
        if ((value < 7) & (value >= 0))
            wochentag = value;
        else
          Console.WriteLine("Fehlerhafte Eingabe!");
      }
    }
  }

  public class Program
  {
    public static void Main(string[] args)
    {
      Vorlesung SoWi = new Vorlesung();
      SoWi.Wochentag = 4;
      Console.WriteLine(SoWi.Wochentag);
    }
  }
}
```
@Rextester.eval(@CSharp)

Die Assessoren können beliebig kombiniert werden. Eine Eigenschaft ohne einen
`set`-Accessor ist schreibgeschützt. Eine Eigenschaft ohne einen `get`-Accessor
ist lesegeschützt.

Zudem lassen sich mit der *Fat Arrow* Notation die Darstellungen wiederum
verkürzen. Beispielhaft ist an folgendem Beispiel auch, dass sich die Properties
eine vollkommen andere Informationsstruktur bedienen als die eigentlichen
privaten Variablen abbilden (= Kapselung).

```csharp  
decimal currentPrice, sharesOwned;

public decimal Worth
{
    get => currentPrice * SharesOwned;
    set => sharesOwned = value / currentPrice
}

// Kompakt für

public decimal Worth
{
    get { return currentPrice * SharesOwned; }
    set { sharesOwned = value / currentPrice; }
}
```

Und wie sieht es mit dem Zugriffsschutz der Eigenschaften aus? Insbesondere
`set` sollte soweit wie möglich eingeschränkt werden. Dafür können `internal`,
`private` und `protected` genutzt werden.

### Indexer

Indexer bilden die Zugriffsmethodik für Arrays `MyArray[3]` auf Klassen ab, um
den Zugriff auf Arrays, Listen oder andere Container zu kapseln.  Dabei wird
folgende Notation benutzt:

```csharp  
string [] words = "Das ist ein beispielhafter Text".Split();
//    Typ der Rückgabevariablen
//       |    this Referenz auf das eigene Objekt
//       |     |    Typ der Indexvariable
//       |     |     |      Bezeichner der Variable
//       v     v     v       v
public string this [int index]{
     get  {return words[index]; }
     set  {words[index] = value; }
}
```


```csharp    IndexerExample
using System;

namespace Rextester
{
  public class Months
  {
    string[] months = {"Jan", "Feb", "März", "April", "Mai", "Juni", "Juli",
                       "Aug", "Sep", "Okt", "Nov", "Dez"};

    public string this[byte index]{
      get {return months[index];}
    }
  }

  public class Program
  {
    public static void Main(string[] args)
    {
      Months MonthList = new Months();
      Console.WriteLine(MonthList[5]);
    }
  }
}
```
@Rextester.eval(@CSharp)

Was ist der Vorteil der Klasse + Indexer Lösung? Wie würden Sie die Indizierung
noch absichern?

## 2. Besonderheiten

Eine statische Klasse kann im Unterschied zu einer nicht statischen Klasse nicht
instanziiert werden. Der Zugriff erfolgt immer über den Klassennamen.

Eine statische Klasse:

+ enthält nur statische Member
+ kann nicht instanziiert werden
+ ist versiegelt
+ darf keine Instanzkonstruktoren enthalten


```csharp    StaticClass
using System;

namespace Rextester
{
  public class Program
  {
    public static void Main(string[] args)
    {
      double number = -3.14;  
      Console.WriteLine(Math.Abs(number));  
      Console.WriteLine(Math.Floor(number));  
      Console.WriteLine(Math.Round(Math.Abs(number)));  
      //var a = new Math();
    }
  }
}
```
@Rextester.eval(@CSharp)

## 3. Beispiel der Woche ...

Entwickeln Sie eine Klassenstruktur für die Speicherung der Daten eines
Studenten.


```csharp    StatitcConstructor
using System;
using System.Collections.Generic;

namespace Rextester
{
  public class Student
  {
     private static int globalerZähler;
     private readonly int uid;
     public string Name { get; set; }
     private bool eingeschrieben;
     private List<string> fächer;

     static Student(){
        globalerZähler = 0;
     }

     public Student(string name)
     {
       Name = name;
       Eingeschrieben = true;
       uid = globalerZähler;
       fächer = new List<string>();
       Console.WriteLine("Der Student {0} (Nr. {1}) ist angelegt!", Name, uid);
     }

     public bool Eingeschrieben
     {
       get {return eingeschrieben;}
       set {
          if (eingeschrieben != value)
            eingeschrieben = value;
          else
          {
            if (value) Console.WriteLine("!Student {0} ist schon eingeschrieben!", Name);
            else Console.WriteLine("!Student {0} ist schon exmatrikuliert!", Name);
          }
         }
       }

      public void addTopic(string Fächername){
          fächer.Add(Fächername);
      }

      public void printTopics(){
          Console.WriteLine("Student {0} hat folgende Fächer absolviert:", Name);
          foreach (string topic in fächer){
            Console.Write(topic + " ");
          }
          Console.WriteLine();
      }
     }

   public class Program
     {
       public static void Main(string[] args){
         Student student0 = new Student("Humboldt");
         student0.addTopic("Softwareentwicklung");
         student0.addTopic("Höhere Mathematik I");
         student0.addTopic("Prozedurale Programmierung");
         student0.printTopics();
         student0.Eingeschrieben = true;
       }
     }
   }

```
@Rextester.eval(@CSharp)

## Anhang

**Referenzen**



**Autoren**

Sebastian Zug, André Dietrich, Hernan Valdes, Christian Bräunlich
