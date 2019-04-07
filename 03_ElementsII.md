<!--

author:   Sebastian Zug & André Dietrich
email:    zug@ovgu.de   & andre.dietrich@ovgu.de
version:  0.0.1
language: de
narrator: Deutsch Female

import: https://raw.githubusercontent.com/liaScript/rextester_template/master/README.md

-->

# Vorlesung Softwareentwicklung - 3 - Elemente der Sprache

**Fragen an die heutige Veranstaltung ...**

*

---------------------------------------------------------------------
Link auf die aktuelle Vorlesung im Versionsmanagementsystem GitHub

https://github.com/liaScript/CsharpCourse/blob/master/03_ElementsII.md

Die interaktive Form ist unter diese Link zu finden ->
[LiaScript Vorlesung 1](https://liascript.github.io/course/?https://raw.githubusercontent.com/liaScript/CsharpCourse/master/03_ElementsII.md#1)

---------------------------------------------------------------------

**Wie weit sind wir schon gekommen?**

c# Schlüsselwörter:

| abstract    | as       | base     | bool       | break      |`byte`     |  
| case        | catch    | char     |`checked`   |`class`     | const     |
| continue    | decimal  | default  | delegate   | do         |`double`   |
| else        | enum     | event    | explicit   | extern     | false     |
| finally     | fixed    | float    | for        | foreach    | goto      |
| if          | implicit | in       |`int`       | interface  | internal  |
| is          | lock     | long     |`namespace` | new        | null      |
| object      | operator | out      | override   | params     | private   |
| protected   | public   | readonly | ref        | return     |`sbyte`    |
| sealed      | short    | sizeof   | stackalloc |`static`    | string    |
| struct      | switch   | this     | throw      | true       | try       |
| typeof      | uint     | ulong    |`unchecked` | unsafe     |`ushort`   |
| `using`     | virtual  |`void`    | volatile   | while      |           |


Auf die Auführung der kontextabhängigen Schlüsselwörter wie `where` oder
`ascending` wurde hier verzichtet.

---

## Kontrollfragen

*1. Was bedeutet die Aussage, das C# ein einheitliches Typsystem habe?*

[( )] Es gibt lediglich einen Datentyp.
[(X)] Alle Typen erben von der Klasse `Object`
[( )] Alle Methoden können auf alle Typen angewandt werden.
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
für jeden Typ und geben in jedem Fall einen bool Wert zurück. Dabei muss
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

Konditionale Operatoren ermöglichen die Verknüpfung von Boolschen Aussagen

```csharp    
bool a=true, b=true, c=false;
Console.WriteLine(a && (b || c)); // short-circuit evaluation

// alternativ

Console.WriteLine(a & (b | c));   // keine short-circuit evaluation
```

Beachten Sie den Unterschied zu C oder C++ wo die Operatoren `&` und `|` bitweise Operationen definieren.

### Char / String Datentypen und Operatoren


                                   {{0-1}}
********************************************************************************

**Type char**

Der `char` Datentyp repräsentiert Unicode Zeichen (vgl. [Link](https://de.wikipedia.org/wiki/Unicode)) mit einer Breite von 2 Byte.

| char | Bedeutung                   | Wert   |
| ---- | --------------------------- | ------ |
| \\'   | Einzelnes Anführungszeichen | 0x0027 |
| \\\   | Backslash                   | 0x0022 |
| \\0   | Null                        | 0x0000 |
| \\n   | Neue Zeilenenden            | 0x000A |
| \\t   | Tabulator                   | 0x0009 |


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

********************************************************************************

                                   {{3-4}}
********************************************************************************
Mit C# 6.0 wurden *interpolated strings* eingeführt. Damit ist es möglich bei
der Stringgenerierung Code einzubetten. Diese bieten ein beeindruckendes Spektrum
an variablen String-Definitionen.

```
{<interpolatedExpression>[,<alignment>][:<formatString>]}
```

```csharp        InterpolatedStrings.cs
using System;

namespace MyNamespace
{
  public class Program
  {
    public static void Main(string[] args)
    {
      double a = 5.0, b = 3;
      // Anstatt auf Indizes zu bauen, wie in anderen Sprachen
      Console.WriteLine("Das Ergebnis von {0} + {1} = {2}", a, b, a+b);
      // Unmittelbare Einbettung
      Console.WriteLine($"Das Ergebnis von {a} + {b} = {a + b}");
      Console.WriteLine($"Offenbar ist {a > b ? a : b} der größere Wert!");
      Console.WriteLine($@"\t Produktes {a * b, 7:F3} \n\tDifferenz {a + b, 7:F3}");
      Console.WriteLine("\nAus Maus!");
    }
  }
}
```

```
mcs InterpolatedStrings.cs
mono InterpolatedStrings.exe

Das Ergebnis von 5 + 3 = 8
Das Ergebnis von 5 + 3 = 8
Offenbar ist 5 der größere Wert!
\t Produktes  15.000 \n\tDifferenz   8.000
```

********************************************************************************

### Enumerations

Enumerationstypen erlauben die Auswahl aus einer Aufstellung von Konstanten.

<!-- --{{1}}-- Idee des Codefragments:
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
      Day startingDay = Day.Sun;
      Console.WriteLine(startingDay);
    }
  }
}
```
@Rextester.eval(@CSharp)

### Arrays

Arrays sind potentiell multidimensionale Container beliebiger Daten, also auch
von Arrays und haben folgende Eigenschaften:

* Ein Array kann eindimensional, mehrdimensional oder verzweigt sein.
* Die Größe innerhalb der Dimensionen eines Arrays wird festgelegt, wenn die Arrayinstanz erstellt wird. Eine Anpassung zur Lebensdauer ist nicht vorgesehen.
* Numerische Arrayelemente sind standardmäßig auf 0 (null) festgelegt, Verweiselemente auf NULL.
* Arrays sind nullbasiert: Der Index eines Arrays mit n Elementen beginnt bei 0 und endet bei n-1.
* Arraytypen sind Referenztypen, die IEnumerable und IEnumerable<T> implementieren, können also mit `foreach` iteriert werden.

### Eindimensionale Arrays

```csharp
using System;

namespace Rextester
{
  public class Program
  {
    public static void Main(string[] args)
    {
      int [] intArray = new int [5];
      int [] Array = new int[] { 1, 3, 5, 7, 9 };

      string sentence = "Das ist eine Sammlung von Worten";
      string [] stringArray = sentence.Split();

      foreach(string i in stringArray){
        Console.WriteLine(i);
      }
    }
  }
}
```
@Rextester.eval(@CSharp)

## 2. Konstrukte der Programmsteuerung

if
switch
range
...



## 3. Beispiel der Woche ...

![SoftwareLebenszyklus](/img/02_Elemente/Pi_statistisch.png)<!-- width="60%" --> [WikiMonteCarlo](#7)

Betrachtet wird ein Einheitsquadrat mit Einheitsviertelkreis, indem $n$
zufällige Punkte erzeugt werden. Dabei verhält sich die Anzahl Punkte im
Viertelkreis zur Gesamtanzahl Punkte genauso wie der Flächeninhalt des
Viertelkreis zur Quadratfläche:

$$\frac{Punkte_{in}}{Punkte_{all}} = \frac{A_{Kreisausschnitt}}{Quadratfläche} = \frac{\frac{1}{4}\pi r^2}{r^2} = \frac{1}{4} \pi$$


```csharp    HelloWorld_rex.cs
using System;

namespace Rextester
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var random = new Random();
      double x, y;
      for (int i=0; i<100; i++){
        x = random.NextDouble();
        y = random.NextDouble();
        dist = Math.Sqrt(x*x + y * y);
        if dist > 0
      }
      Console.WriteLine("Hello, world!");
    }
  }
}
```
@Rextester.eval(@CSharp)


## Anhang

**Referenzen**

[Springob]  ZUM-Wiki, ""Springob, "Introduction To Dotnet", [Link](https://www.slideshare.net/samirbhogayta/introduction-to-dotnet)

**Autoren**

Sebastian Zug, André Dietrich
