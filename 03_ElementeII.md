<!--

author:   Sebastian Zug & André Dietrich
email:    zug@ovgu.de   & andre.dietrich@ovgu.de
version:  0.0.1
language: de
narrator: Deutsch Female

import: https://raw.githubusercontent.com/liaScript/rextester_template/master/README.md

-->

# Vorlesung Softwareentwicklung - 3 - Elemente der Sprache

---------------------------------------------------------------------

Link auf die aktuelle Vorlesung im Versionsmanagementsystem GitHub

https://github.com/liaScript/CsharpCourse/blob/master/03_ElementeII.md

Die interaktive Form ist unter diese Link zu finden ->
[LiaScript Vorlesung 3](https://liascript.github.io/course/?https://raw.githubusercontent.com/liaScript/CsharpCourse/master/03_ElementeII.md#1)

---------------------------------------------------------------------

**Wie weit sind wir schon gekommen?**

c# Schlüsselwörter:

| abstract    | as       | base     |`bool`      | break      |`byte`     |  
| case        | catch    | char     |`checked`   |`class`     | const     |
| continue    | decimal  | default  | delegate   | do         |`double`   |
| else        | enum     | event    | explicit   | extern     |`false`    |
| finally     | fixed    |`float`   | for        | foreach    | goto      |
| if          | implicit | in       |`int`       | interface  | internal  |
| is          | lock     |`long`    |`namespace` | new        | null      |
| object      | operator | out      | override   | params     | private   |
| protected   | public   | readonly | ref        | return     |`sbyte`    |
| sealed      |`short`   | sizeof   | stackalloc |`static`    |`string`   |
| struct      | switch   | this     | throw      |`true`      | try       |
| typeof      |`uint`    |`ulong`   |`unchecked` | unsafe     |`ushort`   |
| `using`     | virtual  |`void`    | volatile   | while      |           |

Auf die Auführung der kontextabhängigen Schlüsselwörter wie `where` oder
`ascending` wurde hier verzichtet.

---

## Kontrollfragen

*1. Welche Funktionalität lässt sich mit dem Schlüsselwort `checked` abdecken?*

[( )] Überwachung von allen arithmetischen Operationen
[(X)] Überlaufüberprüfung bei arithmetischen Operationen für ganzzahlige Typen und Konvertierungen
[( )] Genauigkeitsüberwachung bei Gleitkommazahlen
---------------------------------------------------------------------

*2. Hier stehen jetzt Ihre Fragen ...*

---------------------------------------------------------------------

### Boolscher Datentyp und Operatoren

Der `bool`-Typ umfasst die logischen Werte `true` and `false`. Diese sind durch keine cast-Operatoren in numerische Werte und umgekehrt wandelbar!

```csharp  
using System;

namespace Rextester
{
    public class Program
    {
        public static void Main(string[] args)
        {
            bool x = true;
            int y = 1;
            Console.WriteLine(x == y);
        }
    }
}
```
@Rextester.eval(@CSharp)

Die Vergleichsoperatoren `==` und `!=` testen auf Gleichheit oder Ungleichheit
für jeden Typ und geben in jedem Fall einen `bool` Wert zurück. Dabei muss
unterschieden werden zwischen Referenztypen (Zeigen beide Variablen auf die
gleiche Objektinstanz?) und Wertetypen (Stimmen die spezifischen Werte überein?).

<!-- --{{1}}-- Idee des Codefragments:
    * Einführung eines weiteren Objektes, dass auf student2 zeigt,
      anschließend Ausführung der Vergleichsoperation
-->
```csharp    
using System;

namespace Rextester
{
    public class Person{
      public string Name;
      public Person (string n) {Name = n;}
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            Person student1 = new Person("Sebastian");
            Person student2 = new Person("Sebastian");
            Console.WriteLine(student1 == student2);
        }
    }
}
```
@Rextester.eval(@CSharp)

Die Gleichheits- und Vergleichsoperationen  `==`, `!=`, `>=`, `>` usw. sind auf
alle  numerischen Typen anwendbar. Für Nutzerspezifische Datentypen können die
entsprechenden Operatoren überladen werden.

Logische Operatoren `&`, `&&`, `|`, `||` und `^` ermöglichen die Verknüpfung von Boolschen Aussagen. Dabei wird zwischen "konditionalen" und "nicht-konditionalen" Operatoren
unterschieden. Die erste Gruppe kann auf alle Basisdatentypen angewandt werden
die letztgenannte nur auf `bool`.

<!-- --{{1}}-- Idee des Codefragments:
    * Wechsel zu && -> Fehlermeldung
-->
```csharp    
using System;

namespace Rextester
{
    public class Program
    {
        public static void Main(string[] args)
        {
            byte a =  6; // 0110
            byte b = 10; // 1010

            Console.WriteLine(Convert.ToString(a & b, toBase:2));
        }
    }
}
```
@Rextester.eval(@CSharp)


```csharp    
bool a=true, b=true, c=false;
Console.WriteLine(a || (b && c)); // short-circuit evaluation

// alternativ
Console.WriteLine(a | (b & c));   // keine short-circuit evaluation
```


### Char / String Datentypen und Operatoren

                                   {{0-1}}
********************************************************************************

**Type char**

Der `char` Datentyp repräsentiert Unicode Zeichen (vgl. [Link](https://de.wikipedia.org/wiki/Unicode)) mit einer Breite von 2 Byte.

| char | Bedeutung                   | Wert   |
| ---- | --------------------------- | ------ |
| \\'  | Einzelnes Anführungszeichen | 0x0027 |
| \\\  | Backslash                   | 0x0022 |
| \\0  | Null                        | 0x0000 |
| \\n  | Neue Zeilenenden            | 0x000A |
| \\t  | Tabulator                   | 0x0009 |


Mit `\u` oder `\x` lassen sich zudem alle Unicode Zeichen mit einem
4-elementigen Hexadezimalen Code abrufen.

```csharp  
using System;  
namespace Rextester
{
    public class Program
    {
        public static void Main(string[] args)
        {
          Console.WriteLine('\u2328' + " Unicodeblock Miscellaneous Technical");
          Console.WriteLine('\u2F0C' + " Unicodeblock Kangxi Radicals");
        }
    }
}
```
@Rextester.eval(@CSharp)

Entsprechend der Datenbreite können `char` Variablen implizit in `short`
überführt werden. Für andere numerische Typen ist eine explizite Konvertierung
notwendig.

********************************************************************************

{{1-4}}
**Type string**

                                   {{1-2}}
********************************************************************************
Als Referenztyp verweisen `string` Instanzen auf Folgen von Unicodezeichen abgeschlossen durch ein Null `\0`. Bei der Interpretation der Steuerzeichen
muss hinterfragt werden, ob eine Ausgabe des Zeichens oder eine Realisierung
der Steuerzeichenbedeutung gewünscht ist. Dazu wird der *verbatim* Suffix
genutzt.

```csharp  
using System;  
namespace Rextester
{
    public class Program
    {
        public static void Main(string[] args)
        {
          Console.WriteLine("Das ist ein \n Test der \t über mehrere Zeilen geht!");
          Console.WriteLine(@"Das ist ein \n Test der \t über mehrere Zeilen geht!");
        }
    }
}
```
@Rextester.eval(@CSharp)

********************************************************************************

                                   {{2-3}}
********************************************************************************
Der Additionsoperator steht für 2 `string` Variablen bzw. 1 `string` und eine
andere Variable als Verknüpfungsoperator (sofern für den zweiten Operanden
die Methode `toString()` implementiert ist) bereit.

<!-- --{{1}}-- Idee des Codefragments:
  * Integration einer ToString Methode
    public override  string ToString() {return "Der Name ist " + Name;}
-->
```csharp  
using System;  
namespace Rextester
{
    public class Person{
      public string Name;
      public Person (string n) {Name = n;}
    }

    public class Program
    {
        public static void Main(string[] args)
        {
          Console.WriteLine("String + String = " + "StringString" );
          Console.WriteLine("String + Zahl 5 = " + 5); // Implizites .ToString()

          Person ich = new Person ("Sebastian");
          Console.WriteLine("Wer ist das? " + ich);
        }
    }
}
```
@Rextester.eval(@CSharp)

Der Gebrauch des `+` Operators im Zusammenhang mit `string` Daten ist nicht effektiv.
eine bessere Performanz bietet `System.Text.StringBuilder`.

In der nächsten Vorlesung werden wir uns explizit mit den Konzepten der
Ausgabe und entsprechend den Methoden der String Generierung beschäftigen.

********************************************************************************

### Enumerations


                                   {{0-1}}
********************************************************************************

Enumerationstypen erlauben die Auswahl aus einer Aufstellung von Konstanten, die
als Enumeratorlisten bezeichnet wird. Was passiert intern? Die Konstanten werden
auf einen ganzzahligen Typ außer char gemappt. Der Standardtyp von Enumerationselementen ist `int`. Um eine Enumeration eines anderen ganzzahligen Typs, z. B. `byte` zu deklarieren, setzen Sie einen Doppelpunkt hinter dem Bezeichner, auf den der Typ folgt.

<!-- --{{1}}-- Idee des Codefragments:
  * Darstellung des Enum spezifischen Cast Operators
        Day startingDay = (Day) 5;
  * Darstellung der Möglichkeit Constanten zuzuordnen Sat = 5
-->
```csharp  
using System;  

namespace Rextester
{
  public class Program
  {
    enum Day {Sat, Sun, Mon, Tue, Wed, Thu, Fri};

    public static void Main(string[] args)
    {
      Day startingDay = Day.Wed;
      Console.WriteLine(startingDay);
    }
  }
}
```
@Rextester.eval(@CSharp)

********************************************************************************

                                   {{1-2}}
********************************************************************************

Dabei schließen sich die Instanzen nicht gegenseitig aus, mit einem entsprechenden
Attribut können wir auch Mehrfachbelegungen realisieren.

<!-- --{{1}}-- Idee des Codefragments:
  * Hinweis auf Zahlenzuordnung mit Zweierpotenzen
-->
```csharp  
// https://docs.microsoft.com/de-de/dotnet/api/system.flagsattribute?view=netframework-4.7.2

using System;  

namespace Rextester
{
  public class Program
  {
    [FlagsAttribute] // <- Specifisches Enum Attribut
    enum MultiHue : short
    {
       None = 0, Black = 1, Red = 2, Green = 4, Blue = 8
    };

    public static void Main(string[] args)
    {
       Console.WriteLine(
            "\nAll possible combinations of values with FlagsAttribute:");
       for( int val = 0; val <= 16; val++ )
          Console.WriteLine( "{0,3} - {1:G}", val, (MultiHue)val);
    }
  }
}
```
@Rextester.eval(@CSharp)

********************************************************************************

### Arrays

                                   {{0-1}}
********************************************************************************

Arrays sind potentiell multidimensionale Container beliebiger Daten, also auch
von Arrays und haben folgende Eigenschaften:

* Ein Array kann eindimensional, mehrdimensional oder verzweigt sein.
* Die Größe innerhalb der Dimensionen eines Arrays wird festgelegt, wenn die Arrayinstanz erstellt wird. Eine Anpassung zur Lebensdauer ist nicht vorgesehen.
* Numerische Arrayelemente sind standardmäßig auf 0 (null) festgelegt, Verweiselemente auf NULL.
* Arrays sind nullbasiert: Der Index eines Arrays mit n Elementen beginnt bei 0 und endet bei n-1.
* Arraytypen sind Referenztypen, die IEnumerable und IEnumerable<T> implementieren, können also mit `foreach` iteriert werden.

********************************************************************************

                                   {{1-2}}
********************************************************************************
**Eindimensionale Arrays**

Eindimensionale Arrays werden über das Format

```
<typ>[] name = new <typ>[<anzahl>];
```

deklariert. Die spezifische Größenangabe kann entfallen, wenn mit der
Deklaration auch die Initialierung erfolgt.

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

namespace Rextester
{
  public class Program
  {
    public static void Main(string[] args)
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
}
```
@Rextester.eval(@CSharp)

********************************************************************************

                                   {{2-3}}
********************************************************************************

> Achtung Die unterschiedliche Initialisierung von Wert- und Referenztypen
> generiert ggf. Fehler!

Erzeugung eines Arrays von structs - Wertetypen

```csharp
public struct Point {public int X, Y;}
....
Point [] pointcloud = new Point[100];
int x = pointcloud[99].X                    // x = 0
```
Erzeugung eines Arrays von Klasseninstanzen - Referenztypen

```csharp
public class Point {public int X, Y;}
....
Point [] pointcloud = new Point[100];
int x = pointcloud[99].X                    // Runtime Error, Null-Referenz!
```

********************************************************************************

                                   {{3-4}}
********************************************************************************

**Mehrdimensionale Arrays**

C# unterscheidet zwei Typen mehrdimensionaler Arrays, die sich bei der
Initalisierung und Indizierung unterschiedlich verhalten.

<!--
style="width: 90%; max-width: 560px; display: block; margin-left: auto; margin-right: auto;"
-->
````````````
  Rechteckige Arrays
                      ┏━━━━━┯━━━━━┯━━━━━┯━━━━━┓
  a[zeile, Spalte] ──>┃[0,0]│[0,1]│[0,2]│[0,3]┃
                      ┠─────┼─────┼─────┼─────┨    
                      ┃[1,0]│[1,1]│[1,2]│[1,3]┃
                      ┗━━━━━┷━━━━━┷━━━━━┷━━━━━┛


  Ausgefranste Arrays

                    ┏━━━┓       ┏━━━━━━━┯━━━━━━━┯━━━━━━━┯━━━━━━━┓
   a[index] ──>     ┃[0]┃ ──>   ┃[0],[0]│[0],[1]│[0],[2]│[0],[3]┃
                    ┠───┨       ┣━━━━━━━┿━━━━━━━┿━━━━━━━┷━━━━━━━┛
                    ┃[1]┃       ┃[0],[0]│[0],[0]┃
                    ┠───┨       ┣━━━━━━━┿━━━━━━━┿━━━━━━━┓
                    ┃[2]┃       ┃[0],[0]│[0],[1]│[0],[1]┃
                    ┗━━━┛       ┗━━━━━━━┷━━━━━━━┷━━━━━━━┛
````````````

```csharp
int[,] =  rectangularMatrix =
{
  {1,2,3},
  {0,1,2},
  {0,0,1}
};

int [][] = jaggedMatrix ={
    new int[] {1,2,3},
    new int[] {0,1,2},
    new int[] {0,0,1}
};
```


********************************************************************************

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

Vielfach werden `var`-Variablen im Initialisierungteil von `for`- und `foreach`-
Anweisungen bzw. in der `using`-Anweisung verwendet. Eine wesentliche Rolle
spielen sie bei der Verwendung von anonymen Typen. Aber Vorsicht: `var`
kann nur mit lokalen Variablen verwendet werden.

<!-- --{{1}}-- Idee des Codefragments:
  * Statische Beschränkung der Loop! Fehler generieren
  * Ersetzen durch intArray.Length
  * Wie kann man nach mehreren Zeichen splitten?
-->
```csharp    Usagevar
using System;
using System.Collections.Generic;

namespace Rextester
{
  public class Program
  {
    public static void Main(string[] args)
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
}
```
@Rextester.eval(@CSharp)

Weitere Infos https://docs.microsoft.com/de-de/dotnet/csharp/programming-guide/classes-and-structs/implicitly-typed-local-variables


## 2. Anweisungen

Anweisungen  setzen sich zusammen aus Zuweisungen, Methodenaufrufen, Verzweigungen
Sprunganweisungen und Anweisungen zur Fehlerbehandlung.

![ISO 9126](./img/03_ElementeII/Flowchart_de.png)<!-- width="30%" --> [WikiFlow](#13)

### Verzweigungen

                                   {{0-1}}
********************************************************************************

**if**

Verzweigungen in C# sind allein aufgrund von boolschen Ausdrücken realisiert. Eine
implizite Typwandlung wie in C `if (value)` ist nicht vorgesehen.

```csharp
if (BooleanExpression) Statement else Statement
```

Warum brauche sollte ich in jedem Fall Klammern um Anweisungen setzen, gerade
wenn diese nur ein Zeile umfasst?


<!-- --{{1}}-- Idee des Codefragments:
  * Was passiert wenn A == false? Es gibt keine Ausgabe mehr. Wenn man aber
  eine Klammer um das innere if setzt, folgt daraus eine gänzlich andere
  Bedeutung.
-->
```csharp
using System;

namespace Rextester
{
  public class Program
  {
    public static void Main(string[] args)
    {
        bool A = true, B = false;
        if (A)
          if (B)
            Console.WriteLine("Fall 1");  // A & B
          else
            Console.WriteLine("Fall 2");  // A & not B
    }
  }
}
```
@Rextester.eval(@CSharp)

> **Merke**: Das setzen der Klammern steigert die Lesbarkeit ...

... nur bei langen `else if` Reihen kann drauf verzichtet werden.


********************************************************************************

                                   {{1-2}}
********************************************************************************
**switch**

Die `switch`-Anweisung ist eine Mehrfachverzweigung. Sie umfasst einen Ausdruck
und mehrere Anweisungsfolgen, die durch `case` eingeleitet werden.

Anders als bei vielen anderen Sprachen erlaubt C# `switch`-Verzweigungen anhand
auch von `string`s (zusätzlich zu allen Ganzzahl-Typen, bool, char, enums). Es
fehlt hier aber die Möglichkeit sogenannte *Fall Through*  durch das Weglassen
von `break`-Anweisungen zu realisieren. Jeder switch muss mit einem `break`,
`return`, `throw`, `continue` oder `goto` beendet werden. Interessant ist die
Möglichkeit auf `case: null` zu testen!

<!-- --{{1}}-- Idee des Codefragments:
  * A
-->
```csharp
using System;

namespace Rextester
{
  public class Program
  {
    public static void Main(string[] args)
    {
      string day = "Sonntag";
      string output;
      switch (day){
        case "Montag": case "Dienstag":
        case "Mittwoch": case "Donnerstag": case "Freitag":
          output = "Wochentag";
          break;
        case "Samstag": case "Sonntag":
          output = "Wochenende";
          break;
        default:
          output = "Kein Wochentag!";
          break;
      }
      Console.WriteLine(output);
    }
  }
}
```
@Rextester.eval(@CSharp)

C# 7.0 führt darüber hinaus das pattern matching mit switch ein. Damit werden
komplexe Typ und Werte-Prüfungen innerhalb der case Statements möglich.

```csharp
public static double ComputeArea_Version(object shape)
{
    switch (shape)
    {
        case Square s when s.Side == 0:
        case Circle c when c.Radius == 0:
        case Triangle t when t.Base == 0 || t.Height == 0:
        case Rectangle r when r.Length == 0 || r.Height == 0:
            return 0;

        case Square s:
            return s.Side * s.Side;
        case Circle c:
            return c.Radius * c.Radius * Math.PI;
        case Triangle t:
            return t.Base * t.Height / 2;
        case Rectangle r:
            return r.Length * r.Height;
    }
}
```  

Codebeispiel aus [MSDoku](#11)

********************************************************************************

###Schleifen

Eine Schleife (auch „Wiederholung“ oder englisch loop) ist eine Kontrollstruktur
in Programmiersprachen. Sie wiederholt einen Anweisungs-Block – den sogenannten
Schleifenrumpf oder Schleifenkörper –, solange die Schleifenbedingung als
Laufbedingung gültig bleibt bzw. als Abbruchbedingung nicht eintritt. Schleifen,
deren Schleifenbedingung immer zur Fortsetzung führt oder die keine
Schleifenbedingung haben, sind Endlosschleifen.

**Zählschleife - for**

```
for (initializer; condition; iterator)
    body
```

Üblich sind für alle drei Komponenten einzelne Anweisungen. Das erhöht die
Lesbarkeit, gleichzeitig können aber auch komplexere Anweisungen integriert
werden.

<!-- --{{1}}-- Idee des Codefragments:
  * Hinweis, dass Dekrementieren und Intkrementieren durch beliebige andere
  Funktioen ersetzt werden können.
-->
```csharp
using System;

namespace Rextester
{
  public class Program
  {
    public static void Main(string[] args)
    {
      for (int i = 0, j = 10;
           i<10 && j>5;
           Console.WriteLine("Start: i={0}, j={1}", i, j), i++, j--)
        {
            //empty
        }
    }
  }
}
```
@Rextester.eval(@CSharp)

**Kopf- Fußgesteuerte schleife - while/do while**

Eine `while`-Schleife führt eine Anweisung oder einen Anweisungsblock so lange
aus, wie ein angegebener boolescher Ausdruck gültig ist. Da der Ausdruck vor jeder
Ausführung der Schleife ausgewertet wird, wird eine while-Schleife entweder nie
oder mehrmals ausgeführt. Dies unterscheidet sich von der do-Schleife, die ein
oder mehrmals ausgeführt wird.

**Iteration - foreach**

Als alternative Möglichkeit zum Durchlaufen von Containern, die `IEnumerable`
implementieren bietet sich die Iteration mit `foreach` an. Dabei werden alle
Elemente nacheinander aufgerufen, ohne dass eine Laufvariable nötig ist.

<!-- --{{1}}-- Idee des Codefragments:
  * foreach (char in "TU Bergakademie")
-->
```csharp
using System;

namespace Rextester
{
  public class Program
  {
    public static void Main(string[] args)
    {
      int [] array = new int [] {1,2,3,4,5,6};
      foreach (int entry in array){
          Console.Write("{0} ", entry);
      }
    }
  }
}
```
@Rextester.eval(@CSharp)

### Sprünge

Während  bestimmte Positionen im Code adressiert, lassen sich mit
`break` Schleifen beenden, dient `continue` der Unterbrechung des aktuellen
Blockes.  

| Sprunganweisung | Wirkung                                                                              |
| --------------- | ------------------------------------------------------------------------------------ |
| `break`         | beendet die Ausführung der nächsten einschließenden Schleife oder `switch`-Anweisung |
| `continue`      | realisiert einen Sprung in die nächste Iteration der einschließenden Schleife        |
| `goto <label>`  | Sprung an eine Stelle im Code, er durch das Label markiert ist                       |
| `return`                |     beendet die Ausführung der Methode, in der sie angezeigt wird und gibt den optional nachfolgenden Wert zurücksetzen                                                                                  |

<!-- --{{1}}-- Idee des Codefragments:
-->
```csharp   GoTo
using System;

namespace Rextester
{
  public class Program
  {
    public static void Main(string[] args)
    {
      int dummy = 0;
      for (int y = 0; y < 10; y++)
      {
        for (int x = 0; x < 10; x++)
        {
          if (x == 5 && y == 5)
          {
            goto Outer;
          }
        }
        dummy++;
      }
      Outer:
          Console.WriteLine(dummy);
    }
  }
}
```
@Rextester.eval(@CSharp)

Vgl. Links zur Diskussion um goto auf https://de.wikipedia.org/wiki/Sprunganweisung

## 3. Beispiel der Woche ...

![MonteCaroloPI](/img/03_ElementeII/Pi_statistisch.png)<!-- width="30%" --> [WikiMonteCarlo](#7)

Betrachtet wird ein Einheitsquadrat mit Einheitsviertelkreis, indem $n$
zufällige Punkte erzeugt werden. Dabei verhält sich die Anzahl Punkte im
Viertelkreis zur Gesamtanzahl Punkte genauso wie der Flächeninhalt des
Viertelkreis zur Quadratfläche:

$$\frac{Punkte_{in}}{Punkte_{all}} = \frac{A_{Kreisausschnitt}}{Quadratfläche} = \frac{\frac{1}{4}\pi r^2}{r^2} = \frac{1}{4} \pi$$


```csharp      CalcPi
using System;

namespace Rextester
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var random = new Random();
      double x, y, dist;
      int inside = 0, outside = 0;
      for (int i=0; i<100000; i++){
        x = random.NextDouble();
        y = random.NextDouble();
        dist = Math.Sqrt(x*x + y * y);
        if (dist > 1) outside ++;
        else inside++;
      }
      Console.WriteLine("{0:F}", 4 * (float)inside/(inside + outside));
    }
  }
}
```
@Rextester.eval(@CSharp)


## Anhang

**Referenzen**

[MSDoku] C# Dokumentation, "Pattern Matching",  [Link](https://docs.microsoft.com/en-us/dotnet/csharp/pattern-matching)

[WikiMonteCarlo]  ZUM-Wiki, "Monte Carlo Simulation" Autor "Springob", [Link](https://de.wikipedia.org/wiki/Monte-Carlo-Simulation#/media/File:Pi_statistisch.png)

[WikiFlow]  Wikipedia, "Flussdiagramm" Autor "Erik Streb", [Link](https://upload.wikimedia.org/wikipedia/commons/6/6b/Flowchart_de.svg)

**Autoren**

Sebastian Zug, André Dietrich
