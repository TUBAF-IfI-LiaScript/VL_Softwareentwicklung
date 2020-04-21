<!--

author:   Sebastian Zug & André Dietrich
email:    Sebastian.Zug@informatik.tu-freiberg.de & andre.dietrich@informatik.tu-freiberg.de
version:  0.0.1
language: de
narrator: Deutsch Female

import: https://raw.githubusercontent.com/liaScript/rextester_template/master/README.md

-->

# Softwareentwicklung - 5 - Elemente der Sprache C#

**TU Bergakademie Freiberg - Sommersemester 2020**

Link auf die aktuelle Vorlesung im Versionsmanagementsystem GitHub

[https://github.com/SebastianZug/CsharpCourse/blob/SoSe2020/04_DotnetGrundlagen.md](https://github.com/SebastianZug/CsharpCourse/blob/SoSe2020/05_CsharpElemente.md)

Die interaktive Form ist unter diese Link zu finden ->
[LiaScript Vorlesung 05](https://liascript.github.io/course/?https://raw.githubusercontent.com/SebastianZug/CsharpCourse/SoSe2020/05_CsharpElemente.md#1)

---------------------------------------------------------------------

## 7 Fragen in 7 Minuten

**1. Warum macht es Sinn Schlüsselwörter der Sprache C\# als Variablennamen zu nutzen? Wie kann das realisiert werden**

    [(X)] vorangestelltes "@"
    [( )] Unterstriche vor dem Namen
    [[?]] ... , um Variablennamen, die in C# Schlüsselworte sind, aber in Assemblies enthalten sind, die mit anderen Programmiersprachen erzeugt wurden, abzudecken

**2. Jetzt sind Sie dran ...**

**3. Jetzt sind Sie dran ...**

**4. Jetzt sind Sie dran ...**

**5. Jetzt sind Sie dran ...**

**6. Jetzt sind Sie dran ...**

**7. Jetzt sind Sie dran ...**


## Symbole

C# Programme umfassen Schlüsselwörter, Namen, Zahlen, Zeichen, Zeichenketten, Kommentare und Operatoren. Leerzeichen, Tabulatorsprünge oder Zeilenenden werden als Trennzeichen
zwischen diesen Elementen interpretiert.

                                 {{0-1}}
********************************************************************************

**Schlüsselwörter**

... C# umfasst 77 Schlüsselwörter (C# 7.0), die immer klein geschrieben werden.
Schlüsselwörter dürfen nicht als Namen verwendet werden. Ein vorangestelltes `@`
ermöglicht Ausnahmen.

```csharp
var
if
operator
@class // class als Name !
```

Welche Schlüsselwörter sind das?

| abstract    | as       | base     | bool       | break      | byte      |
| case        | catch    | char     | checked    |`class`     | const     |
| continue    | decimal  | default  | delegate   | do         | double    |
| else        | enum     | event    | explicit   | extern     | false     |
| finally     | fixed    | float    | for        | foreach    | goto      |
| if          | implicit | in       | int        | interface  | internal  |
| is          | lock     | long     |`namespace` | new        | null      |
| object      | operator | out      | override   | params     | private   |
| protected   | public   | readonly | ref        | return     | sbyte     |
| sealed      | short    | sizeof   | stackalloc |`static`    | string    |
| struct      | switch   | this     | throw      | true       | try       |
| typeof      | uint     | ulong    | unchecked  | unsafe     | ushort    |
| `using`     | virtual  |`void`    | volatile   | while      |           |

Auf die Auführung der kontextabhängigen Schlüsselwörter wie `where` oder
`ascending` wurde hier verzichtet.

Ist das viel oder wenig, welche Bedeutung hat die Zahl der Schlüsselwörter?

| Sprache    | Schlüsselwörter | Bemerkung                                             |
| ---------- | --------------- | ----------------------------------------------------- |
| F#         | 98              | 64 + 8 from ocaml + 26 future                         |
| C          | 42              | C89 - 32, C99 - 37,                                   |
| C++        | 92              | C++11                                                 |
| PHP        | 49              |                                                       |
| Java       | 51              | Java 5.0 (48 without unused keywords const and goto)  |
| JavaScript | 38              | reserved words + 8 words reserved in strict mode only |
| Python 3.7 | 35              |                                                       |
| Python 2.7 | 31              |                                                       |
| Smalltalk  | 6               |                                                       |

Weiterführende Links:

https://stackoverflow.com/questions/4980766/reserved-keywords-count-by-programming-language

oder

https://halyph.com/blog/2016/11/28/prog-lang-reserved-words.html


********************************************************************************

                                  {{1-2}}
********************************************************************************
**Namen**

... umfassen Buchstaben, Ziffern oder `_`. Das erste Zeichen eines Namens muss
ein Buchstabe (des Unicode-Zeichensatzes) oder ein `_` sein. Der C# Compiler ist
*case sensitive*

```csharp    GreekSymbols.cs
using System;

namespace Rextester
{
    public class Program
    {
        public static void Main(string[] args)
        {
          var Δ = 1;
          Δ++;
          System.Console.WriteLine(Δ);
        }
    }
}
```
@Rextester.eval(@CSharp)

Die Vergabe von Namen sollte sich an die Regeln der Klassenbibliothek halten
um bereits aus dem Namen der Typ ersichtlich wird:

+ C# bevorzugt *camel case* `MyNewClass` anstatt *underscoring* `My_new_class`.
   (Eine engagierte Diskussion zu diesem Thema findet sich unter [Link](https://whatheco.de/2011/02/10/camelcase-vs-underscores-scientific-showdown/))
+ außer bei lokalen Variablen und Parametern oder den Feldern einer Klasse,
      die nicht von außen sichbar sind beginnen Namen mit großen Anfangsbuchstaben
      (diese Konvention wird als *pascal case* bezeichnet)
+ Methoden ohne Rückgabewert sollten mit einem Verb beginnen `PrintResult()` alles
   andere mit einem Substantiv. Boolsche Ausdrücke auch mit einem Adjektiv
   `valid` oder `empty`.

********************************************************************************

                                     {{2-3}}
********************************************************************************

**Zahlen**

... Zahlenwerte können als vorzeichenbehaftet und vorzeichenlose Zahlenwerte,
als Ganzzahl oder gebrochene Zahl übergeben werden. C# deckt dafür verschiedene
Darstellungen ab.

Auf die unterschiedlichen Formate wird im Anschluss eingegangen.

********************************************************************************

                                      {{3-4}}
********************************************************************************
**Zeichenketten**

... analog zu C werden konstante Zeichen mit einfachen Hochkommas `'A'`, `'b'`
und Zeichenkettenkonstanten `"Bergakademie Freiberg"` mit doppelten Hochkommas
festgehalten. Es dürfen beliebige Zeichen bis auf die jeweiligen Hochkommas
oder das `\` als Escape-Zeichen (wenn diese nicht mit dem Escape Zeichen
kombiniert sind) eingeschlossen sein.

```csharp    Print
using System;

namespace Rextester
{
     public class Program
     {
         public static void Main(string[] args)
         {
             Console.WriteLine("Das ist ein ganzer Satz");
             Console.WriteLine('e');
         }
     }
}
```
@Rextester.eval(@CSharp)

********************************************************************************

                                 {{4-5}}
********************************************************************************
**Kommentare**

... C# unterscheidet zwischen *single-line* und *multi-line* Kommentaren. Diese
können mit XML-Tags versehen werden, um die automatische Generation einer
Dokumentation zu unterstützen. Darauf wird zu einem späteren Zeitpunkt
eingegangen.

Kommentare werden vor der Kompilierung aus dem Quellcode gelöscht.

```csharp    comments.cs
using System;

namespace Rextester
{
  // <summary> Diese Klasse gibt einen konstanten Wert aus </summary>
  public class Program
  {
    public static void Main(string[] args)
    {
        // Das ist ein Kommentar
        System.Console.WriteLine("Hier passiert irgendwas ...");
        /* Wenn man mal
           etwas mehr Platz
           braucht */
    }
  }
}
```
@Rextester.eval(@CSharp)

In einer der folgenden Veranstaltungen werden die Möglichkeiten der Dokumentation
explizit adressiert.

1. Code gut kommentieren (Zielgruppenorientierte Kommentierung)
2. Header-Kommentare als Einstiegspunkt
3. Gute Namensgebung für Variablen und Methoden
4. Community- und Sprach-Standards beachten
5. Dokumentationen schreiben
6. Dokumentation des Entwicklungsflusses

********************************************************************************

## Datentypen

Datentypen können sehr unterschiedlich strukturiert werden. Das nachfolgende
Schaubild realisiert dies auf 2 Ebenen (nach Mössenböck, Kompaktkurs C# 7 )

<!--
style="width: 100%; max-width: 560px; display: block; margin-left: auto; margin-right: auto;"
-->
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
 nierte Typen  ation                               -faces
         |
         |      ...............................................................
         |                           Benutzerdefinierte Typen
         |
    .----+------+-----------.
    |           |           |
Character    Ganzzahl   Gleitkommazahl
                |
        .------+---------.
        |                |
    Vorzeichen   vorzeichenlos                             vgl. Mössenböck
```

|                  | Werttypen        | Referenztypen                              |
|:---------------- |:---------------- |:------------------------------------------ |
| Variable enthält | einen Wert       | eine Referenz                              |
| Speicherort      | Stack            | Heap                                       |
| Zuweisung        | kopiert den Wert | kopiert die Referenz                       |
| Speicher         | Größe der Daten  | Größe der Daten, Objekt-Metadata, Referenz |

<!--
style="width: 80%; max-width: 360px; display: block; margin-left: auto; margin-right: auto;"
-->
```ascii
    struct Point
   +-----------------+
   | X               |
   +-----------------+     P1 (Instanz des structs)
   | Y               |
   +-----------------+


   Referenz P1          class Point
   +-----------+      +-----------------+
   | Reference |───>  | Object Metadata |
   +-----------+      +-----------------+
                      | X               |
                      +-----------------+
                      | Y               |
                      +-----------------+
````````````

Vorliegende Implementierung realisiert ein `Point struct`. Welche Änderungen
müssen vorgenommen werden, um einen Referenztypen zu benutzen? Welche
Auswirkung hat dies?

<!-- --{{1}}-- wechsel von struct auf class, erläuterung der Ausgaben, Erklärung der
idee des Copy-Konstruktors -->

```csharp    ValueTypes.cs
using System;

namespace Rextester
{
    public struct Point {public int X, Y;}

    public class Program
    {
        public static void Main(string[] args)
        {
            Point p1 = new Point();
            p1.X = 7;

            Point p2 = p1;
            Console.WriteLine(p1.X);
            Console.WriteLine(p2.X);

            p2.X = 8;
            Console.WriteLine(p1.X);
            Console.WriteLine(p2.X);
        }
    }
}
```
@Rextester.eval(@CSharp)


### Numerische Datentypen

| Type                        | Suffix | Name    | .NET Typ | Bit |
| --------------------------- | ------ | ------- | -------- | --- |
| Ganzzahl vorzeichenbehaftet |        | sbyte   | SByte    | 8   |
|                             |        | short   | Int16    | 16  |
|                             |        | int     | Int32    | 32  |
|                             | `L`    | long    | Int64    | 64  |
| Ganzzahl ohne Vorzeichen    |        | byte    | Byte     | 8   |
|                             |        | ushort  | UInt16   | 16  |
|                             | `U`    | uint    | UInt32   | 32  |
|                             | `UL`   | ulong   | UInt64   | 64  |
| Gleitkommazahl              | `F`    | float   | Single   | 32  |
|                             | `D`    | double  | Double   | 64  |
|                             | `M`    | decimal | Decimal  | 128 |

Eingabe von Zahlenwerten

```csharp    Numbers.cs
int x = 127;
long y = 0x74:
double d = 1.5;
double g = 1.3454E06;

// New in C# 7.0
var b = 0b1010_1011_0101_1001;
int million = 1_000_000;
```

Numerische Suffixe

| Suffix | C# Typ  | Beispiel         | Bemerkung                             |
| ------ | ------- | ---------------- | ------------------------------------- |
| F      | float   | float f = 1.0F   |                                       |
| D      | double  | double d = 1D    | Anwendung ?                           |
| U      | decimal | decimal d = 1.0M | Compilerfehler bei fehlen des Suffix  |
| U      | uint    | uint i = 1U      |                                       |


<!-- --{{0}}-- Illustration des Gebrauchs der Suffixe, Einführung von var -->
```csharp    HelloWorld_rex.cs
using System;

namespace Rextester // This namespace is necessary for Rextester API !
{
    public class Program
    {
        public static void Main(string[] args)
        {
            float f = 5.1F;
            Console.WriteLine(f.GetType());
        }
    }
}
```
@Rextester.eval(@CSharp)

Numerische Konvertierungen

C# kennt implizite und explizite Konvertierungen.

```csharp
int x = 1234;
long y = x;
short z = (short) x;
```
Da die Konvertierung von Ganzkommazahlen in Gleitkommazahlen in jedem Fall
umgesetzt werden kann, sieht C# hier eine implizite Konvertierung vor. Umgekehrt
muss diese explizit realisiert werden.

Explizite Konvertiering mit dem Typkonvertierungsoperator (runde Klammern) ist
ebenfalls nicht immer möglich. Zusätzliche Möglichkeiten der Typkonvertierung
bietet für elementare Datentypen die Klasse **Convert** durch zahlreiche Methoden
wie z.B.:

```csharp
int wert=Convert.ToInt32(Console.ReadLine());//string to int
```

### Exkurs: Gleitkommazahlen

Gleitkommazahlen? Wie funktioniert das eigentlich?

Welche Probleme treten ggf. auf?

**Rundungsfehler**

Ungenaue Darstellungen bei der Zahlenrepräsentation führen zu:

* algebraisch inkorrekten Ergebnissen
* fehlender Gleichheit bei Konvertierungen in der Verarbeitungskette
* Fehler beim Test auf Gleichheit

```csharp    FloatingPoint_Experiments.cs
using System;

namespace Rextester
{
    public class Program
    {
        public static void Main(string[] args)
        {

         double fnumber = 123456784649577.0;
         double additional = 0.0000001;
         Console.WriteLine("Experiment 1");
         Console.WriteLine("{0} + {1} = {2}", fnumber, additional,
                                              fnumber + additional);

          Double value = .1;
          Double result1 = value * 10;
          Double result2 = 0;
          for (int ctr = 1; ctr <= 10; ctr++){
               result2 += value;
          }
          Console.WriteLine("\n Experiment 2");
          Console.WriteLine(".1 * 10:           {0:R}", result1);
          Console.WriteLine(".1 Added 10 times: {0:R}", result2);
        }
    }
}
```
@Rextester.eval(@CSharp)

**Dezimal-Trennzeichen**

Im Beispielprogramm wird ein Dezimalpunkt als Trennzeichen verwendet. Diese
Darstellung ist jedoch kulturspezifisch. In Deutschland gelten das Komma als
Dezimaltrennzeichen und der Punkt als Tauschender-Trennzeichen. Speziell bei
Ein- und Ausgaben kann das zu Irritationen führen. Diese können durch die
Verwendung der Klasse **System.Globalization.CultureInfo** beseitigt werden.

Zum Beispiel wird mit der folgenden Anweisung die Eingabe eines
Dezimalpunkt statt Dezimalkomma erlaubt.

```csharp
double wert = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
```

**Division durch Null**

Die Datentypen `float` und `double` kennen die Werte *NegativeInfinity*
(*-1.#INF*) und *PositiveInfinity* (*1.#INF*), die bei Division durch Null
entstehen können. Ausserdem gibt es den Wert *NaN* (*not a number*, *1.#IND*), der
einen irregulären Zustand repräsentiert. Mit Hilfe der Methoden
*IsInfinity()* bzw. *IsNaN()* kann überprüft werden, ob diese Werte vorliegen.

```csharp
Console.WriteLine(Double.IsNaN(0.0/0.0));//gibt true aus
```

### Arithmetische Operatoren

                                 {{0-1}}
********************************************************************************

**Alle Numerischen Datentypen**

Die arithmetischen Operatoren `+`, `-`, `*`, `/`, `%` sind für alle numerischen
Typen mit Ausnahme der 8 und 16-Bit breiten Typen als Addition, Subtraktion,
Multiplikation, Division und Modulo.

Die Addition und Subtraktion kann mit Inkrement und Dekrement-Operatoren
abgebildet werden.

```csharp
using System;

namespace Rextester
{
    public class Program
    {
        public static void Main(string[] args)
        {
            int result = 101;
            for (int i = 0; i<100; i++ ){ // Anwendung des Inkrement Operators
              result--;  // Anwendung des Dekrement Operators
            }
            Console.WriteLine(result);
        }
    }
}
```
@Rextester.eval(@CSharp)

********************************************************************************

                                 {{1-2}}
********************************************************************************
**Integraltypen**

Divisionsoperationen generieren einen abgerundeten Wert bei der Anwendung auf
Ganzkommazahlen. Fangen sie mögliche Divisionen durch 0 mit entsprechenden
Exceptions ab!

<!-- --{{1}}-- Idee des Codefragments:
  * Wechsel zu Floatingpoint Zahlen (über Komma und Suffix),
  * Motivation der Format Specifiers von WriteLine
  * Division durch 0
-->
```csharp
using System;

namespace Rextester
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Division von 2/3  = {0:D}", 2/3);
        }
    }
}
```
@Rextester.eval(@CSharp)

Überlaufsituationen (Vergleiche Ariane 5 Beispiel der ersten Vorlesung) lassen
sich in C# sehr komfortabel handhaben:

<!-- --{{1}}-- Idee des Codefragments:
  * Einführung des checked Operators
-->
```csharp
using System;

namespace Rextester
{
    public class Program
    {
        public static void Main(string[] args)
        {
            int a = int.MinValue;
            Console.WriteLine("Wert von a                = {0}", a);
            a--;
            Console.WriteLine("Wert von a nach Dekrement = {0}", a);
        }
    }
}
```
@Rextester.eval(@CSharp)

Die Überprüfung kann auf Blöcke `checked{}` ausgedehnt werden oder per Compiler-Flag
den gesamten Code einbeziehen.
Der `checked` Operator kann nicht zur Analyse von Operationen mit Gleitkommazahlen herangezogen werden!

********************************************************************************


                                 {{2-3}}
********************************************************************************
**8 und 16-Bit Integraltypen**

Diese Typen haben keine "eigenen" Operatoren. Vielmehr konvertiert der Compiler
diese implizit, was bei der Abbildung auf den kleineren Datentyp zu
entsprechenden Fehlermeldungen führt.

<!-- --{{1}}-- Idee des Codefragments:
    * Generierung Kompilerfehler
    * Ergänzung cast Operator
-->
```csharp
using System;

namespace Rextester
{
    public class Program
    {
        public static void Main(string[] args)
        {
            short x = 1, y = 1;
            short z = x + y;
            Console.WriteLine("Division von 2/3  = {0:D}", z);
        }
    }
}
```
@Rextester.eval(@CSharp)

********************************************************************************
### Bitweise Operatoren

                                 {{0-1}}
********************************************************************************
Bitweise Operatoren verknüpfen Zahlen auf der Ebene einzelnen Bits, analog
anderen Programmiersprachen stellt C# foldende Operatoren zur Verfügung:

| ~  |invertiert jedes Bit                       |
| \| |verknüpft korrespondierende Bits mit ODER  |
| &  |verknüpft korrespondierende Bits mit UND   |
| ^  |verknüpft korrespondierende Bits mit XOR   |
| << |bitweise Verschiebung nach links           |
| >> |bitweise Verschiebung nach rechts          |

```csharp
using System;

namespace Rextester
{
    public class Program
    {
        public static void Main(string[] args)
        {
          int x = 21, y = 12;
           Console.WriteLine("dezimal:{0:D}, hexadezimal:{0:X}", x );
           Console.WriteLine("dezimal:{0:D}, hexadezimal:{0:X}", y);
           Console.WriteLine("dezimal:{0:D}, hexadezimal:{0:X}", x & y );
           Console.WriteLine("dezimal:{0:D}, hexadezimal:{0:X}", x | y);
           Console.WriteLine("dezimal:{0:D}, hexadezimal:{0:X}", x << 1);
           Console.WriteLine("dezimal:{0:D}, hexadezimal:{0:X}", y >> 2);
        }
    }
}
```
@Rextester.eval(@CSharp)

```text
20 = 0000 0000 0001 0101
12 = 0000 0000 0000 1100
 4 = 0000 0000 0000 0100
29 = 0000 0000 0001 1101
42 = 0000 0000 0010 1010
 3 = 0000 0000 0000 0011
```
********************************************************************************

## Beispiel der Woche ... Primzahlenbestimmung

Dieses Beispiel richtet sich insbesondere an diejenigen, die noch wenig
Erfahrung bei der Programmierung haben. Experimentieren Sie selbstständig
mit kleinen Programmieraufgaben wie diesen und lesen Sie fremde Beispiele
um sich auf die Übungen vorzubereiten.

```csharp   PrimeNumbers.cs
using System;

namespace Rextester
{
  public class Program
  {
    public static void Main(string[] args)
    {
      for (int number = 0; number < 100; number ++)
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
}
```
@Rextester.eval(@CSharp)

Einen guten Startpunkt bieten zum Beispiel die "1000 C# Examples" unter
https://www.sanfoundry.com/csharp-programming-examples/
