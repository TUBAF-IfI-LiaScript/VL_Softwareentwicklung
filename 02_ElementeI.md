<!--

author:   Sebastian Zug & André Dietrich
email:    zug@ovgu.de   & andre.dietrich@ovgu.de
version:  0.0.1
language: de
narrator: Deutsch Female

import: https://raw.githubusercontent.com/liaScript/rextester_template/master/README.md

-->

# Vorlesung Softwareentwicklung - 2 - Elemente der Sprache

Link auf die aktuelle Vorlesung im Versionsmanagementsystem GitHub

https://github.com/liaScript/CsharpCourse/blob/master/02_ElementsI.md

Die interaktive Form ist unter diese Link zu finden ->
[LiaScript Vorlesung 2](https://liascript.github.io/course/?https://raw.githubusercontent.com/liaScript/CsharpCourse/master/02_ElementsI.md#1)

---------------------------------------------------------------------

**Wie weit sind wir schon gekommen?**

c# Schlüsselwörter:

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


---------------------------------------------------------------------

## Kontrollfragen

*1. Was bedeutet die Aussage, das C# ein einheitliches Typsystem habe?*

[( )] Es gibt lediglich einen Datentyp.
[(X)] Alle Typen erben von der Klasse `Object`
[( )] Alle Methoden können auf alle Typen angewandt werden.
---------------------------------------------------------------------

*2. Hier stehen jetzt Ihre Fragen ... *

---------------------------------------------------------------------

## 1. Symbole

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

## 2. Datentypen

Datentypen können sehr unterschiedlich strukturiert werden. Das nachfolgende
Schaubild realisiert dies auf 2 Ebenen (nach Mössenböck, Kompaktkurs C# 7 )

<!--
style="width: 80%; max-width: 760px; display: block; margin-left: auto; margin-right: auto;"
-->
````````````
                                       C# Typen
                                           ┃
                         ┏━━━━━━━━━━━━━━━━━┻━━━━━━━━━━━━━━━━━━┓
                     Werttypen                           Referenztypen
                         ┃                                    ┃
           ┏━━━━━━━━┳━━━━┻━━━━┳━━━━━━━┓         ┏━━━━━━━━┳━━━━┻━━━━┳━━━━━━━┓
       Vordefi-  Enumer-  Structs   Tupel   Klassen  Inter    Arrays  Delegates
   nierte Typen  ation                               -faces
           ┃      ╰─────────────────────────┬──────────────────────────────────╯
           ┃                                V
           ┃                    Benutzerdefinierte Typen
      ┏━━━━┻━━━━━━┳━━━━━━━━━━┓                   
 Character    Ganzzahl   Gleitkommazahl                       
                  ┃   
            ┏━━━━━┻━━━━━━┓
        Vorzeichen   Vorzeichenlos                             vgl. Mössenböck

````````````

|                  | Werttypen        | Referenztypen                              |
|:---------------- |:---------------- |:------------------------------------------ |
| Variable enthält | einen Wert       | eine Referenz                              |
| Speicherort      | Stack            | Heap                                       |
| Zuweisung        | kopiert den Wert | kopiert die Referenz                       |
| Speicher         | Größe der Daten  | Größe der Daten, Objekt-Metadata, Referenz |

<!--
style="width: 80%; max-width: 360px; display: block; margin-left: auto; margin-right: auto;"
-->
````````````
    struct Point
   ┏━━━━━━━━━━━━━━━━━┓
   ┃ X               ┃
   ┠─────────────────┨     P1 (Instanz des structs)
   ┃ Y               ┃
   ┗━━━━━━━━━━━━━━━━━┛


   Referenz P1          class Point
   ┏━━━━━━━━━━━┓      ┏━━━━━━━━━━━━━━━━━┓
   ┃ Reference ┠────> ┃ Object Metadata ┃
   ┗━━━━━━━━━━━┛      ┠─────────────────┨
                      ┃ X               ┃
                      ┠─────────────────┨
                      ┃ Y               ┃
                      ┗━━━━━━━━━━━━━━━━━┛  
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
| U      | decimal | decimal d = 1.0M | Compilerfehler bei fehlen des Suffixe |
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

### Exkurs: Gleitkommazahlen

Gleitkommazahlen? Wie funktioniert das eigentlich?

Welche Probleme treten ggf. auf?

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

### 3. Arithmetische Operatoren

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

## 4. Beispiel der Woche ... Primzahlenbestimmung

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

## Anhang

**Referenzen**

keine

**Autoren**

Sebastian Zug, André Dietrich
