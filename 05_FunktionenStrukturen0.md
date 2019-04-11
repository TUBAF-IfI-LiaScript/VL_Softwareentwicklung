<!--

author:   Sebastian Zug & André Dietrich
email:    zug@ovgu.de   & andre.dietrich@ovgu.de
version:  0.0.1
language: de
narrator: Deutsch Female

import: https://raw.githubusercontent.com/liaScript/rextester_template/master/README.md

-->

# Vorlesung Softwareentwicklung - 4 - Funktionen und Strukturen

**Fragen an die heutige Veranstaltung ...**

*

---------------------------------------------------------------------
Link auf die aktuelle Vorlesung im Versionsmanagementsystem GitHub

https://github.com/liaScript/CsharpCourse/blob/master/04_FunktionenStrukturen.md

Die interaktive Form ist unter diese Link zu finden ->
[LiaScript Vorlesung 1](https://liascript.github.io/course/?https://raw.githubusercontent.com/liaScript/CsharpCourse/master/04_FunktionenStrukturen.md#1)

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

*1. Welche Default Values nehmen Referenztypen, numerische, char und bool Variablen an?*

| Typ                       | Default Wert |
| ------------------------- | ------------ |
| Referenztypen             | null         |
| numerische und enum Typen | 0            |
| char                      | '\0'         |
| bool                      | false        |

--------------------------------------------------------------------

*2. Hier stehen jetzt Ihre Fragen ...*

---------------------------------------------------------------------
## Ergänzung - Anwendung der switch Anweisung

Identifizieren Sie das auftreten des Musters *a{c}df* in einem Signalverlauf!

<!--
style="width: 90%; max-width: 560px; display: block; margin-left: auto; margin-right: auto;"
-->
````
                  .-  a   -. .-- c  --. .-  d   -.
                  |        | |        | |        |
                  |        v |        v |        v
            .----.-.       .-.        .-.       .-. --.
            *   ( A )     ( B )      ( C )     ( D )  *
            '--->'-'       '-'       ^'-'|      '-'<--.
                  ^         |        |   |       |
                  |--- * ---.        . c .       |
                  .-------------- f -------------.
````

```csharp   
using System;

namespace Rextester
{
  public class Program
  {
    enum states {A, B, C, D};
    public static void Main(string[] args)
    {
      string inputs;

      states state = states.A;
      Console.WriteLine("Geben Sie die Eingabefolge für die State-Machine vor: ");
      inputs = Console.ReadLine();
      foreach(char sign in inputs){
        Console.Write("{0} -> {1} ", state, sign);
        switch (state){
          case states.A:
            if (sign == 'a') state = states.B;
            else state = states.A;
            break;
          case states.B:
            if (sign == 'c') state = states.C;
            else state = states.A;
            break;
          case states.C:
            if (sign == 'd') state = states.D;
            else if (sign != 'c') state = states.A;
            break;
          case states.D:
            if (sign == 'f') state = states.A;
            break;
        }
        Console.WriteLine("-> {0}", state);
      }
    }
  }
}
```
``` bash stdin
abaccdaafab
```
@Rextester._eval_(@uid,@CSharp,true,`@input(1)`)


## 1. Funktionen in C#

Im Grunde ist die separate von Operationen, ohne die Einbettung in entsprechende
Klassen nur beschränkt zielführend. In C# können Funktionen und Prozeduren nur
als Methoden innerhalb von Klassen angelegt werden. Allerdings lassen sich
insbesondere die Konzepte der Parameterübergaben auch ohne dass zuvor die
OO-Konzepte besprochen wurden, erläutern.

C# kennt *benannte* und *anonyme* Methoden, in diesem Abschnitt wird nur auf
die benannten Methoden eingegangen. Prozeduren sind Funktionen ohne Rückgabewert,
sie werden entsprechend als `void` deklariert.

<!-- --{{1}}-- Idee des Codefragments:
  * Bedeutung von void static
  * static void Calc(float p)            Überladen von Funktionen
-->
```csharp   
using System;

namespace Rextester
{
  public class Program
  {
    static void Calc(int p)               // Funktions / Methodendefinition
    {
      p = p + 1;
      Console.WriteLine(p);
    }

    public static void Main(string[] args)
    {
      Calc(5f);                           // Funktions / Methodenaufruf
    }
  }
}
```
@Rextester.eval(@CSharp)

### Verkürzte Darstellung

Methoden können in Kurzform in einer einzigen Zeile angegeben werden. Dafür nutzt
C# die Syntax von Lambda Ausdrücken die für anonyme Funktionen verwendet werden.

```csharp  
public class Program
{
  static void Print(int p)  => Console.WriteLine(p);    // Prozedur
  static int Calc(int p)    => p+1;                     // Funktion

  public static void Main(string[] args){
    int p = 6, result;
    result = Calc(p);  
    print(result);
  }
}
```

### Übergeben von Parametern

Ohne weitere Refrenzparameter werden Variablen an Funktionen bei

* Wertetypen (Basistypen, Enumerationen, structs, Tupel) mittels *pass-by-value*
* Referenztypen (Klassen, Interfaces, Arrays, Delegates) mittels *pass-by-value*

an eine Funktion übergeben.

<!-- --{{1}}-- Idee des Codefragments:
  * Ersetzen der integer Variablen p durch ein Array
          int [] p = new int [] {6};
  * Einführen von ref
-->
```csharp   
using System;

namespace Rextester
{
  public class Program
  {
    static void Calc(int p)  
    {
      p = p + 1;
      Console.WriteLine(p);
    }

    public static void Main(string[] args){
      int p = 6;
      Calc(p);  
      Console.WriteLine(p);                
    }
  }
}
```
@Rextester.eval(@CSharp)

Welche Lösungen sind möglich um einen referenzierten Zugriff zu ermöglichen?

**Ansatz 0 - Globale Variablen**

... sind in C# als isoliertes Konzept nicht implementiert, können aber als
statische Klasse realisiert werden.

**Ansatz 1 - Rückgabe des angepassten Wertes**
(unüblich und auf einen Wert beschränkt)

```csharp   
static int Calc( int p)  
{
  // operationen über P
  return p;
}

public static void Main(string[] args){
  ...
  int p = 5;
  p = Calc(ref p);  
  ...
}
```
**Ansatz 2 - Übergabe als Referenz**

Bei der Angabe des `ref`-Attributes wird statt der Variablen in jedem Fall die
Adresse übergeben. Es ist aber lediglich ein Attribut der Parameterübergabe und
kann isoliert nicht genutzt werden, um die Adresse einer Variablen zu bestimmen
(vgl C: `int a=5; int *b=&a`).
`ref` kann auch auf Referenzdatentypen angewendet werden, der Compiler löscht es entsprechend.

Vorteil: auf beliebig viele Parameter ausweisbar, keine Synchronisation der
Variablennamen zwischen Übergabeparameter und Rückgabewert notwendig.

```csharp   
static void Calc(ref int p)  
{
  // operationen über P
}

public static void Main(string[] args){
  ...
  int p = 5;
  Calc(ref p);  
  ...
}
```

**Ansatz 3 - Übergabe als out-Referenz**

`out` erlaubt die Übernahme von Rückgabewerten aus der aufgerufenen Methode.

```csharp   
using System;

namespace Rextester
{
  public class Program
  {
    static void Calc(int p, out int output)
    {
      output = p + 1;
    }

    public static void Main(string[] args){
      int p = 6, r;
      Calc(p, out r);  
      Console.WriteLine(r);                
    }
  }
}
```
@Rextester.eval(@CSharp)

Interessant wird dieses Konzept durch die in C# 7.0 eingeführte Möglichkeit,
dass die Deklaration beim Aufruf selbst erfolgt. Im Zusammenhang mit impliziten
Variablendeklarationen kann man dann typunabhängig Rückgabewerte aus Funktionen
entgegennehmen.

Zudem sollte für eine sehr umfangreiches Set von Rückgabewerten geprüft werden,
ob diese wirklich alle benötig werden. Mit dem *discard* Platzhalter `out _` werden unnötige
Deklarationen eingespart.

```csharp  
static void SuperComplexMethod(out  string result, out int countA, out int countB){}
SuperComplexMethod(out _, out_, out int count);
```

### Parameterlisten

C# erlaubt es Methoden zu definieren, die eine variable Zahl von Parametern
haben. Dabei wird der letzte Parameter als Array deklariert, so dass die  
Informationen dann systematisch zu evaluieren sind. Dafür wird der `params`
Modifikator eingefügt.

```csharp    variableParameters.cs
using System;

namespace Parameters
{
  public class Program
  {
    static void Add(out int sum, params int [] list)
    {
      sum = 0;
      foreach(int i in list) sum+=i;
    }

    public static void Main(string[] args){
      Add(out int sum, 3, 3, 5 , 6);
      Console.WriteLine(sum);
    }
  }
}
```

Letztendlich wird damit eine Funktionalität realisiert, wie sie für
`Main(string[] args)` obligatorisch ist.

### Benannte und optionale Argumente

Funktionsdeklarationen können mit Default-Werten spezifiziert werden. Dadurch
wird auf der einen Seite Flexibilität über ein breites Interface garantiert,
auf der anderen aber lästige Tipparbeit vermieden. Der Code bleibt damit
übersichtlich.

```csharp   
static void Sort(string [] s, int from, int to, bool ascending, bool ignoreCases){}

static void Sort(string [] s, int from=0, int to=-1, bool ascending = true, bool ignoreCases= false){}
```

Die *default*-Werte müssen aber der Reihenfolge nach "abgearbeitet" werden.
eine partielle Auswahl bestimmter Werte ist nicht möglich.

```csharp   
string [] s = {'Rotkäpchen', 'Hänsel', 'Gretel', 'Hexe'};
//Aufruf     // implizit
Sort(s);     // from=0, to=-1, ascending = true, ignoreCases= false
Sort(s, 3);  // to=-1, ascending = true, ignoreCases= false
```

Darüber hinaus lässt sich die Reihenfolge der Parameter aber auch auflösen. Der
Variablenname wird dann explizit angegeben `,variablenname:Wert, `.

<!-- --{{1}}-- Idee des Beispiels:
          PrintDate(1, year:2019, month:12);  
-->
```csharp   
using System;

namespace Rextester
{
  public class Program
  {
    static void PrintDate(int day=1111, int month=2222, int year=3333 ){
      Console.WriteLine("Day {0} Month {1} Year {2}", day, month, year);
    }

    public static void Main(string[] args){
      PrintDate();           
    }
  }
}
```
@Rextester.eval(@CSharp)

### Überladen von Funktionen

Innerhalb der Konzepte von C# ist es explizit vorgesehen, dass Methoden gleichen
Namens auftreten, wenn diese sich in ihren Parametern unterscheiden:

* Anzahl der Parameter
* Parametertypen
* Paramterattribute (ref, out)

Ein bereits mehrfach genutztes Beispiel dafür ist die `System.Write`-Methode,
die unabhängig vom Typ der übergebenen Variable eine entsprechende Ausgabe
realisiert. Die Dokumentation unter [Link](https://docs.microsoft.com/de-de/dotnet/api/system.console.write?view=netframework-4.7.2) kennt 18 verschiedene
Parametersets für diese Methode. Der Vorteil liegt darin, dass der Nutzer sich nicht
18 verschiedene Methoden zu merken muss.

```csharp   
using System;

namespace Rextester
{
  public class Program
  {
    public static void Main(string[] args){
      double value = 1d;                                   // originär unterstützt
      DateTime date = new DateTime(2008, 5, 1, 8, 30, 52); // auf ToString() angewiesen
      Console.WriteLine(date);
      }
   }
}
```
@Rextester.eval(@CSharp)

## 2 Structs

                                         {{0-1}}
*******************************************************************************

Ausgangspunkt für die weiteren Überlegungen ist die Konfiguration von `structs`
in C.


```cpp         struct.c
#include <stdio.h>
#include <stdlib.h>

typedef struct RectangleStructure{
    double length;
    double width;
    double area;
} Rectangle;

int main(int argc, char const *argv[])
{
    Rectangle rect1, rect2, rect3;
    // Initialisierung:
    rect1.length = 2.5; rect1.width = 5;
    rect2.length = 4; rect2.width = 8;
    rect3.length = 1.5; rect3.width = 2.1;
    // Berechnung:
    rect1.area = rect1.length * rect1.width;
    rect2.area = rect2.length * rect2.width;
    rect3.area = rect3.length * rect3.width;
    // Ausgabe:
    printf("Rectangle 1 has an area of %f\n",rect1.area);
    printf("Rectangle 2 has an area of %f\n",rect2.area);
    printf("Rectangle 3 has an area of %f\n",rect3.area);
    return 0;
}
```
@Rextester.C

**Was fehlt uns?**

*******************************************************************************

                                       {{1-2}}
*******************************************************************************

Richtig, ein Set zugehöriger Funktionen!

```cpp    structAndFunctions.c
#include <stdio.h>
#include <stdlib.h>

typedef struct RectangleStructure{
    double length;
    double width;
    double area;
} Rectangle;

void initializeRectangle(Rectangle *actualRect, double length,
                         double width){
 actualRect->length = length;
 actualRect->width = width;
}

void computeArea(Rectangle *actualRect){
 actualRect->area = actualRect->length * actualRect->width;
}

void printRectangleArea(Rectangle *actualRect){
 printf("The Rectangle has an area of %f\n",actualRect->area);
}

int main(int argc, char const *argv[])
{
    Rectangle rect1;
    // Initialisierung:
    initializeRectangle(&rect1, 30, 40);
    computeArea(&rect1);
    printRectangleArea(&rect1);
    return 0;
}
```
@Rextester.C

C sieht keine Möglichkeit vor Funktion und Daten "dichter" zusammenzubringen, wenn man von Funktionspointern im `struct` absieht.

*******************************************************************************

                                 {{2-3}}
*******************************************************************************

Und in C#?

https://docs.microsoft.com/de-de/dotnet/csharp/programming-guide/classes-and-structs/using-structs

*******************************************************************************



## 3. Beispiel der Woche ...



## Anhang

**Referenzen**

[MSDoku] C# Dokumentation, "Pattern Matching",  [Link](https://docs.microsoft.com/en-us/dotnet/csharp/pattern-matching)

[WikiMonteCarlo]  ZUM-Wiki, "Monte Carlo Simulation" Autor "Springob", [Link](https://de.wikipedia.org/wiki/Monte-Carlo-Simulation#/media/File:Pi_statistisch.png)

**Autoren**

Sebastian Zug, André Dietrich
