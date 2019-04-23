<!--

author:   Sebastian Zug & André Dietrich
email:    zug@ovgu.de   & andre.dietrich@ovgu.de
version:  0.0.1
language: de
narrator: Deutsch Female

import: https://raw.githubusercontent.com/liaScript/rextester_template/master/README.md

-->

# Vorlesung Softwareentwicklung - 5 - Funktionen und Strukturen

--------------------------------------------------------------------
Link auf die aktuelle Vorlesung im Versionsmanagementsystem GitHub

https://github.com/liaScript/CsharpCourse/blob/master/05_FunktionenStrukturen.md

Die interaktive Form ist unter diese Link zu finden ->
[LiaScript Vorlesung 5](https://liascript.github.io/course/?https://raw.githubusercontent.com/liaScript/CsharpCourse/master/05_FunktionenStrukturen.md#1)

---------------------------------------------------------------------

**Wie weit sind wir schon gekommen?**

c# Schlüsselwörter:

| abstract    | as       | base     |`bool`      |`break`     |`byte`     |  
|`case`       |`catch`   |`char`    |`checked`   |`class`     | const     |
|`continue`   |`decimal` | default  | delegate   |`do`        |`double`   |
|`else`       |`enum`    | event    | explicit   | extern     |`false`    |
|`finally`    | fixed    |`float`   |`for`       |`foreach`   |`goto`     |
|`if`         | implicit | in       |`int`       | interface  | internal  |
| is          | lock     |`long`    |`namespace` | new        | null      |
| object      | operator | out      | override   | params     | private   |
| protected   | public   | readonly | ref        |`return`    |`sbyte`    |
| sealed      |`short`   | sizeof   | stackalloc |`static`    |`string`   |
| struct      |`switch`  | this     |`throw`     |`true`      |`try`      |
| typeof      |`uint`    |`ulong`   |`unchecked` | unsafe     |`ushort`   |
|`using`      | virtual  |`void`    | volatile   |`while`     |           |


Auf die Auführung der kontextabhängigen Schlüsselwörter wie `where` oder
`ascending` wurde hier verzichtet.

---

## Kontrollfragen

*1. Welche Default Values nehmen Referenztypen, numerische, char und bool
Variablen an?*

| Typ                       | Default Wert |
| ------------------------- | ------------ |
| Referenztypen             | null         |
| numerische und enum Typen | 0            |
| char                      | '\0'         |
| bool                      | false        |

--------------------------------------------------------------------

*2. Hier stehen jetzt Ihre Fragen ...*

---------------------------------------------------------------------
## Motivation

Identifizieren Sie das Auftreten des Musters *a{c}df* in einem Signalverlauf!

<!--
style="width: 90%; max-width: 560px; display: block; margin-left: auto; margin-right: auto;"
-->
````ascii
                  .-  a   -. .-- c  --. .-  d   -.
                  |        | |        | |        |
                  |        v |        v |        v
            .----.-.       .-.        .-.       .-. --.
            *   ( A )     ( B )      ( C )     ( D )  *
            '--->'-'       '-'       ^'-'\     '-'<--.
                  ^         |       /    |       |
                  |--- * ---╯      | .   -'        |
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

Welche anderen Lösungsansätze sind denkbar?

## 1. Funktionen in C#

Im Grunde ist die separate von Operationen, ohne die Einbettung in entsprechende
Klassen nur beschränkt zielführend. In C# können Funktionen und Prozeduren nur
als Methoden innerhalb von Klassen angelegt werden. Allerdings lassen sich
insbesondere die Konzepte der Parameterübergaben auch ohne dass zuvor die
OO-Konzepte besprochen wurden, erläutern.

C# kennt *benannte* und *anonyme* Methoden, in diesem Abschnitt wird nur auf
die benannten Methoden eingegangen. Prozeduren sind Funktionen ohne Rückgabewert,
sie werden entsprechend als `void` deklariert.

<!-- --{{0}}-- Idee des Codefragments:
  * Bedeutung von void static
  * static void Calc(float p)            Überladen von Funktionen
-->
```csharp                    Functions
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
C# die Syntax von Lambda Ausdrücken, die für anonyme Funktionen verwendet werden.

```csharp
public override string ToString() => $"{fname} {lname}".Trim();

// oder

public override string ToString()
{
   return $"{fname} {lname}".Trim();
}
```

Damit lassen sich einfache Funktionen sehr kompakt darstellen.

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

                                       {{0-2}}
********************************************************************************

Ohne weitere Refrenzparameter werden Variablen an Funktionen bei

* Wertetypen (Basistypen, Enumerationen, structs, Tupel) mittels *pass-by-value*
* Referenztypen (Klassen, Interfaces, Arrays, Delegates) mittels *pass-by-reference*

an eine Funktion übergeben.

<!-- --{{1}}-- Idee des Codebeispiels FunctionParameters:
Ersetzen Sie die integer Variable p durch ein Array der Größe 1 und beobachten Sie das veränderte Ergebnis. Nutzen Sie das Schlüsselwort ref um eine datentypunabhängige pass-by-reference Übergabe zu realisieren.
-->
```csharp          FunctionParameters
using System;

namespace Rextester
{
  public class Program
  {
    static void Calc(int p)  
    {
      p = p + 1;
      Console.WriteLine("Innerhalb von Calc {0}", p);
    }

    public static void Main(string[] args){
      int p = 6;
      Calc(p);  
      Console.WriteLine("Innerhalb von Main {0}", p);              
    }
  }
}
```
@Rextester.eval(@CSharp)

Welche Lösungen sind möglich den Zugriff einer Funktion auf eine übergebene
Variable generell sicherzustellen?

********************************************************************************

********************************************************************************

                                     {{2-3}}
********************************************************************************

**Ansatz 0 - Globale Variablen**

... sind in C# als isoliertes Konzept nicht implementiert, können aber als
statische Klassen realisiert werden.

```csharp          staticClassAsGlobalVariableContainer
using System;

namespace Rextester
{
  public static class Counter
  {
     public static int globalCounter = 0;
  }

  public class Program
  {
    static void IncrementsCounter(){
       Counter.globalCounter++;
    }
    public static void Main(string[] args){
      Console.WriteLine(Counter.globalCounter);   
      IncrementsCounter();
      Console.WriteLine(Counter.globalCounter);               
    }
  }
}
```
@Rextester.eval(@CSharp)

********************************************************************************

                                     {{3-4}}
********************************************************************************

**Ansatz 1 - Rückgabe des modifizierten Wertes**

```csharp   
static int Calc( int input)  
{
  // operationen über P
  int output = ... input
  return output;
}

public static void Main(string[] args){
  ...
  int p = 5;
  p = Calc(p);  
  ...
}
```

********************************************************************************

                                     {{4-5}}
********************************************************************************

**Ansatz 2 - Übergabe als Referenz**

Bei der Angabe des `ref`-Attributes wird statt der Variablen in jedem Fall die
Adresse übergeben. Es ist aber lediglich ein Attribut der Parameterübergabe und
kann isoliert nicht genutzt werden, um die Adresse einer Variablen zu bestimmen
(vgl C: `int a=5; int *b=&a`).
`ref` kann auch auf Referenzdatentypen angewendet werden, der Compiler löscht es entsprechend.

Vorteil: auf beliebig viele Parameter ausweisbar, keine Synchronisation der
Variablennamen zwischen Übergabeparameter und Rückgabewert notwendig.

```csharp         UsageOfRef
using System;

namespace Rextester
{
  public class Program
  {
    static void Calc(ref int p)
    {
       p = p * 2;
    }
    public static void Main(string[] args){
      int p = 1;
      Console.WriteLine(p);   
      Calc(ref p);
      Console.WriteLine(p);               
    }
  }
}
```
@Rextester.eval(@CSharp)


********************************************************************************

                                     {{5-6}}
********************************************************************************

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
ob diese wirklich alle benötig werden. Mit dem *discard* Platzhalter `out _` werden unnötige Deklarationen eingespart.

```csharp  
// Defintion
static void SuperComplexMethod(out string result,
                               out int countA,
                               out int countB)
{
  // super complex
}

// Aufruf der Methode
SuperComplexMethod(out _, out _, out int count);
```

********************************************************************************

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
      int sum = 0;
      Add(out sum, 3, 3, 5 , 6);
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
static void Sort(string [] s, int from, int to,
                 bool ascending, bool ignoreCases){}

static void Sort(string [] s,
                 int from = 0,
                 int to = -1,
                 bool ascending = true,
                 bool ignoreCases= false){}
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
      PrintDate(year:2019);           
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
realisiert.

## 2. Structs

                                         {{0-1}}
*******************************************************************************

Ein struct-Typ ist ein ein Werttyp, der in der Regel verwendet wird, um eine
Gruppe verwandter Variablen zusammenzufassen. Beispiele dafür können sein:

* karthesische Koordinaten (x, y, z)
* Merkmale eines Produktes (Größe, Name, Preis)
* Charakteristik einer Datei (Speicherort, Name, Größe, Rechteinformationen)

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
    printf("Rectangle 1 has an area of %5.1f\n",rect1.area);
    printf("Rectangle 2 has an area of %5.1f\n",rect2.area);
    printf("Rectangle 3 has an area of %5.1f\n",rect3.area);
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

C sieht keine Möglichkeit vor Funktion und Daten "dichter" zusammenzubringen,
wenn man von Funktionspointern im `struct` absieht.

*******************************************************************************

                                 {{2-3}}
*******************************************************************************

Und in C#? C# erweitert das Konzept in Richtung der Konzepte der objektorientierten
Programmierung und integriert Operationen über den Daten in das `struct`-Konzept.
Dabei können sie folgende Elemente umfassen:

* Felder und Konstanten
* Methoden
* Konstruktoren und Destruktoren
* Eigenschaften(Properties)
* Indexer
* Events
* überladene Operatoren
* geschachtelte Typen

```csharp        FirstStructExample
using System;

namespace Rextester
{
  public struct Animal
  {
    public string name;               // Felder / Konstanten
    public string sound;              //

    public void MakeNoise() {         // Methode
    	Console.WriteLine("{0} makes {1}", name, sound);
    }
  }

  public class Program
  {
    public static void Main(string[] args){
      Animal kitty;
      kitty.name = "Kitty";
      kitty.sound = "Miau";     
      kitty.MakeNoise();
    }
  }
}
```
@Rextester.eval(@CSharp)

*******************************************************************************

### Konstruktoren

                                 {{0-1}}
*******************************************************************************

```csharp
Animal kitty;
kitty.name = ... ;
kitty.sound = ...;
...
kitty.state = State.Hungry;

Animal wally;
wally.name = ... ;
wally.sound = ...;
...
wally.state = State.Sleeps;
```

Haben wir auch wirklich alle initialen Variablen gesetzt? Das Vorgehen
scheint doch sehr unübersichtlich und fehleranfällig!

*******************************************************************************

                                 {{1-2}}
*******************************************************************************

Konstruktoren sind spezielle Methoden für die Initalisierung eines Objektes. In
`structs` dürfen allerdings (im Unterschied zu Klassen) keine parameterlosen
Methoden sein. Der Compiler erzeugt diese automatisch, die Methode beschreibt
alle Felder mit den datentypspezifischen Nullwerten.

> *Anmerkung 1:* Konstruktoren sind Methoden und folglich steht das gesamte Spektrum
> der Variabilität bei deren Defintion zur Verfügung (Überladen, vordefinierte
> Variablen, Parameterlisten, usw.)

> *Anmerkung 2:* Um einen Konstruktor für die Intialisierung zu nutzen
> braucht es einen erweiterten Aufruf.


| Konstruktor                                  | Aufruf                                      |
| -------------------------------------------- | ------------------------------------------- |
| Aufruf des (impliziten) Standardkonstruktors | `Animal kitty = new Animal()`               |
| `public Animal(name, sound)`                 | `Animal kitty = new Animal("kitty","Miau")` |
| `public Animal(name, sound = "Miau")`        | `Animal kitty = new Animal("kitty")`        |


<!-- --{{1}}-- Idee des Beispiels:
       + Deklaration eines parameterlosen Konstruktors
       + Deklaration eines Konstruktors mit einzelnem Parameter
       + Überladen des Konstruktors
-->
```csharp                                      Constructors
using System;

namespace Rextester
{
  public struct Animal
  {
    public string name;   
    public string sound;  

    public void MakeNoise() {
    	Console.WriteLine("{0} makes {1}", name, sound);
    }
  }

  public class Program
  {
    public static void Main(string[] args){
      Animal cat = new Animal();
      cat.name = "Kitty";
      cat.sound = "Miau";
      cat.MakeNoise();
    }
  }
}
```
@Rextester.eval(@CSharp)

*******************************************************************************

### Embedded Structs

Eine innerhalb einer Struktur (oder Klasse) definierte Struktur (oder Klasse)
wird als geschachtelter Typ bezeichnet.

Unabhängig davon, ob der äußere Typ eine Klasse oder eine Struktur ist, lautet
die Standardeinstellung von geschachtelten Typen private. Sie sind nur über
ihren enthaltenden Typ zugänglich. Es können aber auch weitere
Zugriffsmodifizierer angeben werden.

```csharp                   NestedStructs                    
public structs Container
{
    public structs Nested
    {
        private Container parent;

        public Nested()
        {
        }
        public Nested(Container parent)
        {
            this.parent = parent;
        }
    }
}

// Aufruf mit
Container.Nested nest = new Container.Nested();
```

zum Beispiel vgl. [C# Programmierhandbuch](https://docs.microsoft.com/de-de/dotnet/csharp/programming-guide/classes-and-structs/nested-types)


### Sichtbarkeitsattribute

Struct und Klassen abstrahieren Daten und verbergen konkrete Realisierungen von
Programmteilen. Um zu steuern, welche Elemente eines Programms aus welchem
Kontext heraus sichtbar sind, werden diese mit Attributen versehen.

                                 {{0 - 1}}
*******************************************************************************


**Sichtbarkeit der Stuktur**

Strukturen, die innerhalb eines Namespace (mit anderen Worten, die nicht in anderen Klassen oder Strukturen geschachtelt sind) direkt deklariert werden, können entweder `private, ``public` oder `internal` sein.

| Bezeichner | Konsequenz                                            |
| ---------- | ----------------------------------------------------- |
| public     | Keine Einschränkungen für den Zugriff                 |
| internal   | Der Zugriff ist auf die aktuelle Assembly beschränkt. |
| private    | Der Zugriff kann nur aus dem Code der gleichen Struktur erfolgen.                                                      |

Wenn kein Modifizierer angegeben ist, wird standardmäßig `internal` verwendet.

<!--
style="width: 90%; max-width: 560px; display: block; margin-left: auto; margin-right: auto;"
-->
````ascii

            Programm.cs                       Farmland.cs
            +-----------------------+         +-------------------------+
            | class Program{        |    .->  | internal struct Animal{ |
            |   public void Main(){ |    |    |    ...                  |
            |      Animal Kitty;    | ---.    | }                       |
            |      ...              |         |                         |
            |      Farm Bullerbue;  | ----->  | public struct Farm{     |
            |   }                   |         |    ...                  |
            |}                      |         | }                       |
            +-----------------------+         +-------------------------+

Schritt 1                                     mcs -target:library Farmland.cs
Schritt 2   mcs -reference:Farmland.dll Programm.cs
````

Das struct "Animal" soll in einem anderen Assembly nicht aufrufbar sein. Wir
wollen die Implementierung kapseln und verbergen. Folglich generiert der entsprechende
Aufruf einen Compiler-Fehler. Zugehörige Dateien sind unter [GitHub](https://github.com/liaScript/CsharpCourse/tree/master/code/05_FunktionenStrukturen/DifferentAssemblies) zu finden.

Variante 1: Compilieren von Farmland und Programm in ein Assambly

```bash
mcs Programm.cs Farmland.cs
My name ist Kitty.
```


Variante 2: Compilieren von Farmland als externe Bibliothek

```bash
mcs -target:library Farmland.cs
mcs -reference:Farmland.dll Programm.cs
Programm.cs(8,13): error CS0122: `Farm.Animal' is inaccessible due to its protection level
Programm.cs(9,26): error CS0841: A local variable `cat' cannot be used before it is declared
```

*******************************************************************************


                                 {{1-2}}
*******************************************************************************

**Sichtbarkeit der Felder und Member einer Stuktur**

Für die Sichtbarkeit auf der Ebene der Felder und Member können für structs
drei Attribute verwendet werden `private`, `internal` und `public`. Standard
ist `private`.

```csharp                                      Attributes
using System;

namespace Rextester
{
  public struct Animal
  {
    public string name;   
    internal string sound;
    private byte age;       

    public Animal(string name, uint born, string sound = "Miau"){
      this.name = name;
      this.sound = sound;
      age = (byte) (2019 - born);
    }

    public void MakeNoise() {
    	Console.WriteLine("{0} ({1} years old) makes {2}", name, age, sound);
    }
  }

  public class Program
  {
    public static void Main(string[] args){
      Animal Wally = new Animal ("Wally", 2014, "Wau");
      Wally.MakeNoise();
      Wally.age = 5;
    }
  }
}
```
@Rextester.eval(@CSharp)


*******************************************************************************

### ... ja, aber ...

Welche Einschränkungen haben Structs gegenüber Klassen?

> `struct`s kennen das Konzept der Vererbung nicht! Eine Struktur kann nicht von einer
> anderen Struktur oder Klasse erben, und sie kann auch nicht die Basis einer Klasse
> sein.

https://docs.microsoft.com/de-de/dotnet/csharp/programming-guide/classes-and-structs/using-structs

Welche Konsequenzen hat das?

| Structs                                         | Klassen |
| ----------------------------------------------- | ------- |
| Werttyp (Variablen enthalten das Objektes)      | Referenzdatentyp         |
| Abgelegt auf dem Stack                          | Gespeichert auf dem Heap        |
| Unterstützen keine Vererbung                    |  Unterstützen Vererbung       |
| können Interfaces implementieren                | können Interfaces implementieren        |
| `internal` und `public` als Struct-Attribute | `private`, `internal` und `public` als Klassenattribute |
| `private`, `internal` und `public` als Feld und Member-Attribute| analog plus 3 weitere Attribute|
| keine parameterlosen Konstruktoren deklarierbar |         |


## 3. Beispiel der Woche ...

Im folgenden Beispiel werden Instanzen des Structs "Animal" von einem anderen
Struct "Farm" genutzt und dort in einer (generischen) Liste gespeichert.

```csharp                                      Usage
using System;
using System.Collections;
using System.Collections.Generic;

namespace Rextester
{
  public struct Animal
  {
    public string name;   
    public string sound;  

    public Animal(string name, string sound = "Miau"){
      this.name = name;
      this.sound = sound;
    }

    public void MakeNoise() {
    	Console.WriteLine("{0} makes {1}", name, sound);
    }
  }

  public struct Farm{
    public string adress;
    public List<Animal> animalList;

    public Farm(string adress) {
    	animalList = new List<Animal>();
    	this.adress = adress;
    }

    public void AddAnimal(Animal newanimal){
      animalList.Add(newanimal);
    }

    public void PrintAnimals(){
      foreach (Animal pet in animalList){
        pet.MakeNoise();
      }
    }
  }

  public class Program
  {
    public static void Main(string[] args){
      Animal Wally = new Animal ("Wally","Wau");
      Animal Kitty = new Animal ("Kitty","Miau");
      Farm myFarm = new Farm("Biobauernhof Freiberg");
      myFarm.AddAnimal(Wally);
      myFarm.AddAnimal(Kitty);
      myFarm.PrintAnimals();
    }
  }
}
```
@Rextester.eval(@CSharp)


## Anhang

**Referenzen**



**Autoren**

Sebastian Zug, André Dietrich
