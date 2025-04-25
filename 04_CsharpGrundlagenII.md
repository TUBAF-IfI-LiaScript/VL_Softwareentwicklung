<!--

author:   Sebastian Zug, Galina Rudolf, André Dietrich, `Lina` & `Florian2501`
email:    sebastian.zug@informatik.tu-freiberg.de
version:  1.0.7
language: de
narrator: Deutsch Female
comment:  Werte- und Referenzdatentypen, Array, String, implizite Variablendefinition und Nullables
tags:      
logo:     

import: https://github.com/liascript/CodeRunner

import: https://raw.githubusercontent.com/TUBAF-IfI-LiaScript/VL_Softwareentwicklung/master/config.md

-->

[![LiaScript](https://raw.githubusercontent.com/LiaScript/LiaScript/master/badges/course.svg)](https://liascript.github.io/course/?https://github.com/TUBAF-IfI-LiaScript/VL_Softwareentwicklung/blob/master/04_CsharpGrundlagenII.md)

# C# Grundlagen II

| Parameter                | Kursinformationen                                                                                  |
| ------------------------ | -------------------------------------------------------------------------------------------------- |
| **Veranstaltung:**       | `Vorlesung Softwareentwicklung`                                                                    |
| **Teil:**                | `4/27`                                                                                             |
| **Semester**             | @config.semester                                                                                   |
| **Hochschule:**          | @config.university                                                                                 |
| **Inhalte:**             | @comment                                                                                           |
| **Link auf den GitHub:** | https://github.com/TUBAF-IfI-LiaScript/VL_Softwareentwicklung/blob/master/04_CsharpGrundlagenII.md |
| **Autoren**              | @author                                                                                            |

![](https://media.giphy.com/media/26tn33aiTi1jkl6H6/source.gif)

---------------------------------------------------------------------

## Einschub Explizite Projektdefinitionen

In der letzten Veranstaltung haben wir uns mit der Erstellung von C#-Programmen
beschäftigt und die Notwendigkeit 

+ der expliziten Beschreibung von Abhängigkeiten 
+ der Angabe von Versionsinformationen
+ der Autorenschaft etc.

diskutiert.

> Natürlich ist das kein ausgemachtes Problem von C# ... Codebeispiel in Python

## Referenzdatentypen

```ascii
                                     C# Typen
                                         |
                       .------------------------------------.
                       |                                    |
                   Werttypen                           Referenztypen
                       |                                    |
         .-------+-----+---+--------.        .-------+---------+-------.
         |       |         |        |        |       |         |       |
     Vordefi-  Enumer-  Structs   Tupel   Klassen  Inter    Arrays  Delegates
 nierte Typen  ation                     (String) -faces
         |
         |      ...............................................................
         |           Klassenbibliotheksbasierte / Benutzerdefinierte Typen
         |
         .----+------+-----------+-------------.
         |           |           |             |
     Character    Ganzzahl   Gleitkommazahl   Bool
                     |
             .------+---------.
             |                |
     mit Vorzeichen     vorzeichenlos                                                                      .
```


In der vergangenen Veranstaltung haben wir bereits über die Trennlinie zwischen Werttypen und Referenztypen gesprochen. Was bedeutet die Idee aber grundsätzlich?


| Aspekt                              | Stack                                                                                                                                                 | Heap                                                                                                                                                                                         |
| ----------------------------------- | ----------------------------------------------------------------------------------------------------------------------------------------------------- | -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| Format                              | Es ist ein Array des Speichers. Es ist eine LIFO (Last In First Out) Datenstruktur. In ihr können Daten nur von oben hinzugefügt und gelöscht werden. | Es ist ein Speicherbereich, in dem Chunks zum Speichern bestimmter Arten von Datenobjekten zugewiesen werden. In ihm können Daten in beliebiger Reihenfolge gespeichert und entfernt werden. |
| Was wird abgelegt?                  | Wertedatentypen                                                                                                                                       | Referenzdatentypen                                                                                                                                                                           |
| Was wird auf dem Stack gespeichert? | Wert                                                                                                                                                  | Referenz                                                                                                                                                                                     |
| Kann die Größe variiert werden?     | nein                                                                                                                                                  | ja                                                                                                                                                                                           |
| Zugriffsgeschwindigkeit             | hoch                                                                                                                                                  | gering                                                                                                                                                                                       |
|     Freigabe                                |  vom Compiler organisiert                                                                                                                                                                             |     vom Garbage Collector realisiert                                                                                                             |

**Wie werden Objekte auf dem Stack/Heap angelegt?**

```csharp
int x = 5;
int y = 6;
int[] array = new int[] { 4, 5, 7};
```


```ascii

          STACK                        HEAP
   +-----------------+         +-----------------+
   | 5               |         | ...             |
   +-----------------+         +-----------------+
   | 6               |   +-->  | 4               |  0x1234
   +-----------------+   |     +-----------------+
   | ...             |   |     | 5               |
   +-----------------+   |     +-----------------+
   | 0x1234          | --+     | 7               |
   +-----------------+         +-----------------+
                               | ...             |
                               +-----------------+                                                         .
```

**Und was bedeutet dieser Unterschied?**

Ein zentrales Element ist die unterschiedliche Wirkung des Zuweisungsoperators `=`. Analoges gilt für den Vergleichsoperator `==` den wir bereits betrachtet haben.

```csharp    ExampleArrays
using System;

public class Program
{
  static void Main(string[] args)
  {
    // Zuweisung für Wertetypen
    int x = 5;
    int y = 6;
    y = x;
    Console.WriteLine("{0}, {1}", x, y);
    // Zuweisung für Referenztypen
    int [] intArrayA = new int[]{1,2,3};
    int [] intArrayB = intArrayA;
    Console.WriteLine("Alter Status {0}",intArrayB[0]);
    intArrayA[0] = 55;
    Console.WriteLine("Neuer Status {0}",intArrayA[0]);
    Console.WriteLine("Neuer Status {0}",intArrayB[0]);
    // Und wenn wir beides vermischen?
    intArrayA[1] = x;
    Console.WriteLine("Neuer Status {0}",intArrayA[1]);
  }
}
```
@LIA.eval(`["main.cs"]`, `mcs main.cs`, `mono main.exe`)


```ascii

                    STACK                        HEAP
             +-----------------+         +-----------------+
x            | 5               |         | ...             |
             +-----------------+         +-----------------+
y            | 6               |   +-->  | 55              |  0x1234
             +-----------------+   |     +-----------------+
             | ...             |   |     | 2               |
             +-----------------+   |     +-----------------+
intArrayA    | 0x1234          | --+     | 3               |
             +-----------------+   |     +-----------------+
intArrayB    | 0x1234          | --+     | ...             |
             +-----------------+         +-----------------+                                                 .
```

Muss die Referenz immer auf ein Objekt auf dem Heap zeigen?

```csharp                NullReference.cs
using System;

public class Program
{
  static void Main(string[] args)
  {
    int [] intArrayA = new int[]{1,2,3};
    int [] intArrayB; //= null;
    if (intArrayB is not null) {      
    	Console.WriteLine("Alles ok, mit intArrayB");
    } else {
      Console.WriteLine("intArrayB ist null");
    }    
  }
}
```
```xml   -myproject.csproj
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net8.0</TargetFramework>
  </PropertyGroup>
</Project>
```
@LIA.eval(`["Program.cs", "project.csproj"]`, `dotnet build -nologo`, `dotnet run -nologo`)


Mit `null` kann angezeigt werden, dass diese Referenz noch nicht zugeordnet
wurde.

### Array Datentyp

                                   {{0-1}}
********************************************************************************

Arrays sind potentiell multidimensionale Container beliebiger Daten, also auch
von Arrays und haben folgende Eigenschaften:

* Ein Array kann eindimensional, mehrdimensional oder verzweigt sein.
* Die Größe innerhalb der Dimensionen eines Arrays wird festgelegt, wenn die Arrayinstanz erstellt wird. Eine Anpassung zur Lebensdauer ist nicht vorgesehen.
* Arrays sind nullbasiert: Der Index eines Arrays mit n Elementen beginnt bei $0$ und endet bei $n-1$.
* Arraytypen sind Referenztypen.
* Arrays können mit `foreach` iteriert werden.

> **Merke:** In C# sind Arrays tatsächlich Objekte und nicht nur adressierbare Regionen zusammenhängender Speicher wie in C und C++.

********************************************************************************

                                   {{1-2}}
********************************************************************************
**Eindimensionale Arrays**

Eindimensionale Arrays werden über das Format

```
<typ>[] name = new <typ>[<anzahl>];
```

deklariert.

Die spezifische Größenangabe kann entfallen, wenn mit der
Deklaration auch die Initialisierung erfolgt.

```
<typ>[] name = new <typ>[] {<eintrag_0>, <eintrag_1>, <eintrag_2>};
```

<!-- --{{1}}-- Idee des Codefragments:
  * Statische Beschränkung der Loop! Fehler generieren
  * Ersetzen durch intArray.Length
  * Wie kann man nach mehreren Zeichen splitten?
-->
```csharp    ExampleArrays
using System;

public class Program
{
  static void Main(string[] args)
  {
    int [] intArray = new int [5];
    short [] shortArray = new short[] { 1, 3, 5, 7, 9 };
    for (int i = 0; i < 3; i++){
      Console.Write("{0, 3}", intArray[i]);
    }
    Console.WriteLine("");
    string sentence = "Das ist eine Sammlung von Worten";
    string [] stringArray = sentence.Split();
    foreach(string i in stringArray){
      Console.Write("{0, -9}", i);
    }
  }
}
```
@LIA.eval(`["main.cs"]`, `mcs main.cs`, `mono main.exe`)

> **Recherche**: Wie kann ich nach mehreren Zeichen splitten?

********************************************************************************

                              {{2-3}}
********************************************************************************

```csharp    ExampleArrays
using System;

public class Program
{
  static void Main(string[] args)
  {
    int [] intArray = new int [5];
    short [] shortArray = new short[] { 1, 3, 5, 7, 9 };
    for (int i = 0; i < 3; i++){
      Console.Write("{0, 3}", intArray[i]);
    }
    Console.WriteLine("");
    string sentence = "Das ist eine Sammlung von Worten";
    string [] stringArray = sentence.Split();
    foreach(string i in stringArray){
      Console.Write("{0, -9}", i);
    }
  }
}
```
@LIA.eval(`["main.cs"]`, `mcs main.cs`, `mono main.exe`)

********************************************************************************

                                   {{2-3}}
********************************************************************************

**Mehrdimensionale Arrays**

C# unterscheidet zwei Typen mehrdimensionaler Arrays, die sich bei der
Initalisierung und Indizierung unterschiedlich verhalten.


```ascii
  Rechteckige Arrays
                      +-----+-----+-----+-----+
  a[zeile, Spalte] ──>|[0,0]│[0,1]│[0,2]│[0,3]|
                      +-----+-----+-----+-----+
                      |[1,0]│[1,1]│[1,2]│[1,3]|
                      +-----+-----+-----+-----+

  Ausgefranste Arrays

                    +---+       +-------+-------+-------+-------+
   a[index] ──>     |[0]| ──>   |[0],[0]│[0],[1]│[0],[2]│[0],[3]|
                    +---+       +-------+-------+-------+-------+
                    |[1]|       |[1],[0]│[1],[1]|
                    +---+       +-------+-------+-------+
                    |[2]|       |[2],[0]│[2],[1]│[2],[2]|
                    +---+       +-------+-------+-------+                                           .
```

```csharp
int[,]  rectangularMatrix = //entspricht int[3,3]
{
  {1,2,3},
  {0,1,2},
  {0,0,1}
};

int [][] jaggedMatrix ={ //entspricht int[3][]
    new int[] {1,2,3},
    new int[] {0,1,2},
    new int[] {0,0,1}
};
```

********************************************************************************

### String Datentyp

                              {{0-1}}
********************************************************************************
Als Referenztyp verweisen `string` Instanzen auf Folgen von Unicodezeichen, die durch ein Null `\0` abgeschlossen sind. Bei der Interpretation der Steuerzeichen
muss hinterfragt werden, ob eine Ausgabe des Zeichens oder eine Realisierung
der Steuerzeichenbedeutung gewünscht ist.

```csharp      StringVerbatim.cs
using System;

public class Program
{
  static void Main(string[] args)
  {
    string text1 = "Das ist ein \n Test der \t über mehrere Zeilen geht!";
    string text2 = @"Das ist ein
    Test der
    über mehrere Zeilen geht!";
    Console.WriteLine(text1);
    Console.WriteLine(text2);
  }
}
```
@LIA.eval(`["main.cs"]`, `mcs main.cs`, `mono main.exe`)

********************************************************************************

                              {{1-2}}
********************************************************************************
Der Additionsoperator steht für 2 `string` Variablen bzw. 1 `string` und eine
andere Variable als Verknüpfungsoperator (sofern für den zweiten Operanden
die Methode `toString()` implementiert ist) bereit.

<!-- --{{1}}-- Idee des Codefragments:
* Integration einer ToString Methode
-->
```csharp        ToString.cs
using System;

public class Program
{
  static void Main(string[] args)
  {
    Console.WriteLine("String + String = " + "StringString" );
    Console.WriteLine("String + Zahl 5 = " + 5); // Implizites .ToString()
  }
}

```
@LIA.eval(`["main.cs"]`, `mcs main.cs`, `mono main.exe`)

Der Gebrauch des `+` Operators im Zusammenhang mit `string` Daten ist nicht effektiv.
eine bessere Performanz bietet `System.Text.StringBuilder`.

In der nächsten Vorlesung werden wir uns explizit mit den Konzepten der
Ausgabe und entsprechend den Methoden der String Generierung beschäftigen.

********************************************************************************

### Konstante Werte

Konstanten sind unveränderliche Werte, die zur Compilezeit bekannt sind und sich während der Lebensdauer des Programms nicht ändern. Der Versuch einer Änderung wird durch den Compiler überwacht.

```csharp        ToString.cs
using System;

public class Program
{
  static void Main(string[] args)
  {
     const double pi = 3.14;
     pi = 5; //erzeugt Fehlermeldung, da pi konstant ist
     Console.WriteLine(pi);
  }
}
```
@LIA.eval(`["main.cs"]`, `mcs main.cs`, `mono main.exe`)

> In C# gibt es auch das Schlüsselwort `readonly`, das eine Variable als konstant kennzeichnet, aber erst zur Laufzeit initialisiert wird.

| `const`                                 | `readonly`                                                |
| --------------------------------------- | --------------------------------------------------------- |
| Muss zur Compilezeit definiert werden   | Kann zur Kompilierzeit oder zur Laufzeit definiert werden |
| Implizit statisch                       | Instanz-Ebene oder statisch                               |
| Assembler-übergreifend kopiert          | Assembler-übergreifend gemeinsam genutzt                  |
| Speicher nicht zuweisen                 | Speicher zuweisen                                         |

### Implizit typisierte Variablen

C# erlaubt bei den lokalen Variablen eine Definition ohne der expliziten Angabe
des Datentyps. Die Variablen werden in diesem Fall mit dem Schlüsselwort `var`
definiert, der Typ ergibt sich infolge der Auswertung des Ausdrucks auf der
rechten Seite der Initialisierungsanweisung zur Compilierzeit.

```csharp
var i = 10; // i compiled as an int
var s = "untypisch"; // s is compiled as a string
var a = new[] {0, 1, 2}; // a is compiled as int[]
```
`var`-Variablen sind trotzdem typisierte Variablen, nur der Typ wird vom
Compiler zugewissen.

Vielfach werden `var`-Variablen im Initialisierungsteil von `for`- und `foreach`-
Anweisungen bzw. in der `using`-Anweisung verwendet. Eine wesentliche Rolle
spielen sie bei der Verwendung von anonymen Typen.

```csharp    UsageVar.cs
using System;
using System.Collections.Generic;

public class Program
{
  static void Main(string[] args)
  {
    //int num = 123;
    //string str = "asdf";
    //Dictionary<int, string> dict = new Dictionary<int, string>();
    var num = 123;
    var str = "asdf";
    var dict = new Dictionary<int, string>();
    Console.WriteLine("{0}, {1}, {2}", num.GetType(), str.GetType(), dict.GetType());
  }
}
```
@LIA.eval(`["main.cs"]`, `mcs main.cs`, `mono main.exe`)

Weitere Infos https://docs.microsoft.com/de-de/dotnet/csharp/programming-guide/classes-and-structs/implicitly-typed-local-variables


### Nullable - Leere Variablen

Ein "leer-lassen" ist nur für Referenzdatentypen möglich, Wertedatentypen können nicht uninitialisiert bleiben (Compilerfehler)

<!-- --{{0}}-- Idee des Codefragments:
    * Der Ausgangszustand generiert einen Fehler
    * Initalisierung mit string text = null
    * Evaluation von int i = null;
-->
```csharp                                      Initialisation.cs
using System;

public class Program
{
  static void Main(string[] args){
    string text = null;   // Die Referenz zeigt auf kein Objekt im Heap
    //int i = null;
    if (text == null) Console.WriteLine("Die Variable hat keinen Wert!");
    else Console.WriteLine("Der Wert der Variablen ist {0}", text);
  }
}
```
@LIA.eval(`["main.cs"]`, `mcs main.cs`, `mono main.exe`)

Aus der Definition heraus kann zum Beispiel eine `int` Variable nur einen Wert zwischen int.MinValue und int.MaxValue annehmen. Eine `null` ist nicht vorgesehen und eine `0` gehört zum "normalen" Wertebereich.

Um gleichermaßen "nicht-besetzte" Werte-Variablen zu ermöglichen integriert C#
das Konzept der sogenannte null-fähigen Typen (*nullable types*) ein. Dazu wird
dem Typnamen ein  Fragezeichen angehängt. Damit ist es möglich diesen auch den
Wert `null` zuzuweisen bzw. der Compiler realisiert dies.

<!-- --{{0}}-- Idee des Codefragments:
    * einfache Variable ist mit null initialisierbar
-->
```csharp                                      Iniitalisation
using System;

public class Program
{
  static void Main(string[] args){
    int? i = null;
    if (i == null) Console.WriteLine("Die Variable hat keinen Wert!");
    else Console.WriteLine("Der Wert der Variablen ist {0}", i);
  }
}
```
@LIA.eval(`["main.cs"]`, `mcs main.cs`, `mono main.exe`)

Wie wird das Ganze umgesetzt? Jeder `Typ?` wird vom Compiler dazu in einen generischen Typ `Nullable<Typ>`
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


## Aufgaben

- [ ] Experimentieren Sie mit Arrays und Enumerates. Schreiben Sie Programme, die Arrays nach bestimmten Einträgen durchsuchen. Erstellen Sie Arrays aus Enum Einträgen und zählen Sie die Häufigkeit des Vorkommens.

- [ ] Studieren Sie C# Codebeispiele. Einen guten Startpunkt bieten zum Beispiel die ''1000 C# Examples'' unter https://www.sanfoundry.com/csharp-programming-examples/

## Quizze

Als was kann ein String (z.B. "Hello World") auch gesehen werden?

[[ Struktur
|  (Klasse)
|  Liste
|  Nichts weiter, string ist string
]]

Welche Funktion realisiert das folgende Codebeispiel?

```csharp   PrimeNumbers.cs
using System;

public class Program
{
  static void Main(string[] args)
  {
    for (int number = 0; number < 20; number ++)
    {
      bool prime = true;
      for (int i = 2; i <= number / 2; i++)
      {
        if(number % i == 0)
        {
          prime = false;
          break;
        }
      }
      if (prime == true) Console.Write("{0}, ", number);
    }
  }
}
```
@LIA.eval(`["main.cs"]`, `mcs main.cs`, `mono main.exe`)