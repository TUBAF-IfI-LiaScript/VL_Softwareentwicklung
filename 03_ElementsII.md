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


## 1. Konstrukte der Programmsteuerung

if
switch
range
...

## 2. Ausgaben



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





## 1. Enumerationen

enumartationen
Arrays
Structs
strings


## 2. Arrays

Arrays sind potentiell multidimensionale Container beliebiger Daten, also auch
von Arrays und haben folgende Eigenschaften:

* Ein Array kann eindimensional, mehrdimensional oder verzweigt sein.
* Die Größe innerhalb der Dimensionen eines Arrays wird festgelegt, wenn die Arrayinstanz erstellt wird. Eine Anpassung zur Lebensdauer ist nicht vorgesehen.
* Numerische Arrayelemente sind standardmäßig auf 0 (null) festgelegt, Verweiselemente auf NULL.
* Arrays sind nullbasiert: Der Index eines Arrays mit n Elementen beginnt bei 0 und endet bei n-1.
* Arraytypen sind Referenztypen, die IEnumerable und IEnumerable<T> implementieren, können also mit `foreach` iteriert werden.

### Eindimensionale Arrays

```csharp    HelloWorld_rex.cs
using System;

namespace Rextester // This namespace is necessary for Rextester API !
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





## 4. Beispiel der Woche ... Hello World

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
