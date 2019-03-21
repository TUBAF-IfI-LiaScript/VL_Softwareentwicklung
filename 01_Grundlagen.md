<!--

author:   Sebastian Zug & André Dietrich
email:    zug@ovgu.de   & andre.dietrich@ovgu.de
version:  0.0.1
language: de
narrator: Deutsch Female

import: https://raw.githubusercontent.com/liaScript/rextester_template/master/README.md

-->

# Vorlesung Softwareentwicklung - 1 - Grundlagen

**Fragen an die heutige Veranstaltung ...**

*

---------------------------------------------------------------------
Link auf die aktuelle Vorlesung im Versionsmanagementsystem GitHub

https://github.com/liaScript/CsharpCourse/blob/master/01_Grundlagen.md

Die interaktive Form ist unter diese Link zu finden ->
[LiaScript Vorlesung 1](https://liascript.github.io/course/?https://raw.githubusercontent.com/liaScript/CsharpCourse/master/01_Grundlagen.md#1)

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

<!--
style="width: 80%; max-width: 460px; display: block; margin-left: auto; margin-right: auto;"
-->
````
                  .- 100s -. .-- 2s --. .- 100s -.
                  |        | |        | |        |
                  |        v |        v |        v
                 .-.       .-.        .-.       .-.
 Ampelzustände  ( 0 )     ( 1 )      ( 2 )     ( 3 )
                 '-'       '-'        '-'       '-'
                  ^                              |
                  |                              |
                  .------------- 2s -------------.

                 RED  RED/YELLOW     GREEN     YELLOW
````




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
