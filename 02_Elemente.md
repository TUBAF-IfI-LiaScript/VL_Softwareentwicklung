<!--

author:   Sebastian Zug & André Dietrich
email:    zug@ovgu.de   & andre.dietrich@ovgu.de
version:  0.0.1
language: de
narrator: Deutsch Female

import: https://raw.githubusercontent.com/liaScript/rextester_template/master/README.md

-->

# Vorlesung Softwareentwicklung - 1 - Elemente der Sprache

**Fragen an die heutige Veranstaltung ...**

*

---------------------------------------------------------------------
Link auf die aktuelle Vorlesung im Versionsmanagementsystem GitHub

https://github.com/liaScript/CsharpCourse/blob/master/02_Elemente.md

Die interaktive Form ist unter diese Link zu finden ->
[LiaScript Vorlesung 2](https://liascript.github.io/course/?https://raw.githubusercontent.com/liaScript/CsharpCourse/master/02_Elemente.md#1)

---------------------------------------------------------------------

**Wie weit sind wir schon gekommen?**

c# Schlüsselwörter:

|abstract    |as       |base     |bool      |break      |byte      |  
|case        |catch    |char     |checked   |class      |const     |
|continue    |decimal  |default  |delegate  |do         |double    |
|else        |enum     |event    |explicit  |extern     |false     |
|finally     |fixed    |float    |for       |foreach    |goto      |
|if          |implicit |in       |int       |interface  |internal  |
|is          |lock     |long     |namespace |new        |null      |
|object      |operator |out      |override  |params     |private   |
|protected   |public   |readonly |ref       |return     |sbyte     |
|sealed      |short    |sizeof   |stackalloc|static     |string    |
|struct      |switch   |this     |throw     |true       |try       |
|typeof      |uint     |ulong    |unchecked |unsafe     |ushort    |
|using       |using    |static   |virtual   |void       |volatile  |
|while       |         |         |          |           |          |

Auf die Auführung der kontextabhängigen Schlüsselwörter wie `where` oder
`ascending` wurde hier verzichtet.

---

## 1. Symbole

C# Programme umfassen Schlüsselwörter, Namen, Zahlen, Zeichen, Zeichenketten, Operatoren und Kommentare. Leerzeichen oder Einrückungen sind irrelevant.

 1. Schlüsselwörter

   ... C# umfasst 77 Schlüsselwörter (C# 7.0), die nicht als Namen verwendet
   werden dürfen. Ein vorangestelltes `@` ermöglicht Ausnahmen.

```csharp
var
if
operator
@class // class als Name !
```

 2. Kommentare

  ... C# unterscheidet zwischen *single-line* und *multi-line* Kommentaren.
  Diese können mit XML-Tags versehen werden, um die automatische Generation
  einer Dokumentation zu unterstützen. Darauf wird zu einem späteren Zeitpunkt
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
           System.Console.WriteLine(Δ);  
           /* Wenn man etwas mehr Platz braucht */
         }
     }
}
```
@Rextester.eval(@CSharp)

 3. Namen

   ... umfassen Buchstaben, Ziffern oder `_`. Das erste Zeichen eines Namens
   muss ein Buchstabe (des Unicode-Zeichensatzes) oder ein `_` sein. Der
   C# Compiler ist *case sensitive*

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

 4. Zahlen

 ... Zahlenwerte können als vorzeichenbehaftet und vorzeichenlose Zahlenwerte,
 als Ganzzahl oder gebrochene Zahl übergeben werden. C# deckt dafür verschiedene
 Darstellungen ab.

 Auf die unterschiedlichen Formate wird im Anschluss eingegangen.

 5. Zeichenketten

 ... analog zu C werden konstante Zeichen mit einfachen Hochkommas `'A'`, `'b'` und Zeichenkettenkonstanten `"Bergakademie Freiberg"`
 mit doppelten Hochkommas festgehalten. Es dürfen beliebige Zeichen bis auf die
 jeweiligen Hochkommas  
 oder das `\` als Escape-Zeichen (wenn diese nicht mit dem Escape Zeichen kombiniert sind) eingeschlossen sein.

| Escape Zeichen | Bedeutung                            |
|:---------------|:-------------------------------------|
| `\'`           | `'`                                  |         

ERGÄNZEN

Um zum Beispiel den Text `file "c:\text.txt"` darzustellen müssen wir

```csharp    Print .cs
using System;

namespace Rextester
{
     public class Program
     {
         public static void Main(string[] args)
         {
             Console.WriteLine("file \"c:\\text.txt\"");
             //alternative Schreibweise mit @
         }
     }
}
```
@Rextester.eval(@CSharp)

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

 1.  Wertyp vs. Referenztypen

|                  | Werttypen        | Referenztypen        |
|:-----------------|:-----------------|:---------------------|
| Variable enthält | einen Wert       | eine Referenz        |
| Speicherort      | Stack            | Heap                 |
| Zuweisung        | kopiert den Wert | kopiert die Referenz |
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

<!-- --{{1}}-- wechsel von struct auf class, erläuterung der AUsgaben, Erklärung der
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
            Point p1 = new Point()
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


2. Benutzerorientierte vs. vordefinierte Typen

| Kategorie | Type                        | Suffix | Name    | .NET Typ  | Bit |
| --------- | --------------------------- | ------ | ------- | ------- | --- |
| Numerisch | Ganzzahl vorzeichenbehaftet |        | sbyte   | SByte   | 8   |
|           |                             |        | short   | Int16   | 16  |
|           |                             |        | int     | Int32   | 32  |
|           |                             | `L`    | long    | Int64   | 64  |
|           | Ganzzahl ohne Vorzeichen    |        | byte    | Byte    | 8   |
|           |                             |        | ushort  | UInt16  | 16  |
|           |                             | `U`    | uint    | UInt32  | 32  |
|           |                             | `UL`   | ulong   | UInt64  | 64  |
|           | Gleitkommazahl              | `F`    | float   | Single  | 32  |
|           |                             | `D`    | double  | Double  | 64  |
|           |                             | `M`    | decimal | Decimal | 128 |

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

<!-- --{{1}}-- Illustration des Gebrauchs der Suffixe, Einführung von var -->


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

## Exkurs: Gleitkommazahlen

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

         double fnumber = 123456789012.34567;   
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

## 3. Arithmetische Operatoren

Die arithmetischen Operatoren `+`, `-`, `*`, `/`, `%` sind für alle numerischen
Typen mit Ausnahme der 8 und 16-Bit breiten Typen als Addition, Subtraktion,
Multiplikation, Division und Modulo.

Für die Addition und Subtraktion kann dies als Inkrement und Dekrement  



## 4. Beispiel der Woche ... Hello World

```csharp    HelloWorld_rex.cs
using System;

namespace Rextester // This namespace is necessary for Rextester API !
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello, world!");
        }
    }
}
```
@Rextester.eval(@CSharp)
