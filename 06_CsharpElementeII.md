<!--

author:   Sebastian Zug & André Dietrich
email:    Sebastian.Zug@informatik.tu-freiberg.de & andre.dietrich@informatik.tu-freiberg.de
version:  0.0.1
language: de
narrator: Deutsch Female

import: https://raw.githubusercontent.com/liaScript/rextester_template/master/README.md

-->

# Softwareentwicklung - 6 - Elemente der Sprache C# II

**TU Bergakademie Freiberg - Sommersemester 2020**

Link auf die aktuelle Vorlesung im Versionsmanagementsystem GitHub

[https://github.com/SebastianZug/CsharpCourse/blob/SoSe2020/06_CsharpElementeII.md](https://github.com/SebastianZug/CsharpCourse/blob/SoSe2020/06_CsharpElementeII.md)

Die interaktive Form ist unter diese Link zu finden ->
[LiaScript Vorlesung 06](https://liascript.github.io/course/?https://raw.githubusercontent.com/SebastianZug/CsharpCourse/SoSe2020/06_CsharpElementeII.md#1)

---------------------------------------------------------------------


## 7 Fragen in 7 Minuten

**1. Jetzt sind Sie dran ...**

**2. Jetzt sind Sie dran ...**

**3. Jetzt sind Sie dran ...**

**4. Jetzt sind Sie dran ...**

**5. Jetzt sind Sie dran ...**

**6. Jetzt sind Sie dran ...**

**7. Jetzt sind Sie dran ...**


## Datentypen und Operatoren (Fortsetzung)

Aufbauend auf den Inhalten der Vorlesung 5 setzen wir unseren Weg durch die Datentypen und Operatoren unter C# fort.

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


### Boolscher Datentyp und Operatoren


                        {{0-2}}
**********************************************************************


Der `bool`-Typ umfasst die logischen Werte `true` and `false`. Diese sind durch keine cast-Operatoren in numerische Werte und umgekehrt wandelbar!

```csharp                BoolOperation.cs
using System;

namespace Rextester
{
    public class Program
    {
        public static void Main(string[] args)
        {
            bool x = true;
            Console.WriteLine(x);
            Console.WriteLine(!x);
            Console.WriteLine(x == true);      // Rückgabe eines "neuen" bool Wertes

            int y = 1;
            //Console.WriteLine(x == y);       // Funkioniert nicht

            // Lösungsansatz I bool -> int
            int bool2int = x ? 1 : 0;
            Console.WriteLine(bool2int);

            // Lösungsansatz II
            bool2int = Convert.ToInt32(x);
            Console.WriteLine(bool2int);

            Console.WriteLine(bool2int == y);  // Funktiontiert
        }
    }
}
```
@Rextester.eval(@CSharp)

Welchen Vorteil/Nachteil sehen Sie zwischen den beiden Lösungsansätzen?

**********************************************************************

                        {{1-2}}
**********************************************************************

Die Vergleichsoperatoren `==` und `!=` testen auf Gleichheit oder Ungleichheit
für jeden Typ und geben in jedem Fall einen `bool` Wert zurück. Dabei muss
unterschieden werden zwischen Referenztypen und Wertetypen.

<!-- --{{1}}-- Idee des Codefragments:
    * Einführung eines weiteren Objektes, dass auf student2 zeigt,
      anschließend Ausführung der Vergleichsoperation
-->
```csharp                    Equality.cs
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

> Merke: Für Referenztypen evaluiert `==` die Addressen der Objekte, für Wertetype die spezifischen Daten. (Es sei denn, Sie haben den Operator überladen.)

Die Gleichheits- und Vergleichsoperationen  `==`, `!=`, `>=`, `>` usw. sind auf
alle  numerischen Typen anwendbar.

**********************************************************************

                        {{2}}
**********************************************************************

Logische Operatoren `&`, `&&`, `|`, `||` und `!` ermöglichen die Verknüpfung von Boolschen Aussagen. Dabei wird zwischen "nicht-konditionalen" und "konditionalen" Operatoren
unterschieden. Die erste Gruppe kann auf alle Basisdatentypen angewandt werden
die letztgenannte nur auf `bool`.

| Operation | numerische Typen | boolsche Variablen                |
| --------- | ---------------- | --------------------------------- |
| `&`       | bitweises UND (Ergebnis ist ein numerischer Wert!)   | nicht-konditionaler UND Operator |
| `&&`      |                  | konditonaler UND Operator         |


<!-- --{{1}}-- Idee des Codefragments:
    * Wechsel zu && -> Fehlermeldung
-->
```csharp                   BooleanOperations.cs
using System;

namespace Rextester
{
    public class Program
    {
        public static void Main(string[] args)
        {
            int a =  6; // 0110
            int b = 10; // 1010

            Console.WriteLine((a & b).GetType());
            Console.WriteLine(Convert.ToString(a & b, 2).PadLeft(8,'0'));

            // Console.WriteLine(a && b);
        }
    }
}
```
@Rextester.eval(@CSharp)

Konditional und Nicht-Konditional, was heißt das? Erstgenannte optimieren die Auswertung. So berücksichtigt der AND-Operator `&&` den rechten Operanden gar nicht, wenn der linke Operand bereits ein `false` ergibt.

```csharp
bool a=true, b=true, c=false;
Console.WriteLine(a || (b && c)); // short-circuit evaluation

// alternativ
Console.WriteLine(a | (b & c));   // keine short-circuit evaluation
```

**********************************************************************

### Char / String Datentypen und Operatoren

                                   {{0-1}}
********************************************************************************

**Type char**

Der `char` Datentyp repräsentiert Unicode Zeichen (vgl. [Link](https://de.wikipedia.org/wiki/Unicode)) mit einer Breite von 2 Byte.

```csharp
char oneChar = 'A';
char secondChar = '\n';
char thirdChar = (char) 65;  // Referenz auf ASCII Tabelle
```

Die Eingabe erfolgt entsprechend den Konzepten von C mit einfachen Anführungszeichen. Doppelte Anführungsstricht implizieren `String`-Variablen!

```csharp            FancyCharacters.cs
using System;
namespace Rextester
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var myChar = 'A';
            var myString = "A";
            Console.WriteLine(myChar.GetType());
            Console.WriteLine(myString.GetType());
        }
    }
}
```
@Rextester.eval(@CSharp)

Neben der unmittelbaren Eingabe über die Buchstaben und Zeichen können auch Unicodezeichen referenziert werden. Daneben ist mit einer entsprechenden Konvertierung auch eine Zahlen-basierte Eingabe möglich.

| char | Bedeutung                   | Wert   |
| ---- | --------------------------- | ------ |
| \\'  | Einzelnes Anführungszeichen | 0x0027 |
| \\\  | Backslash                   | 0x0022 |
| \\0  | Null                        | 0x0000 |
| \\n  | Neue Zeilenenden            | 0x000A |
| \\t  | Tabulator                   | 0x0009 |


Mit `\u` oder `\x` lassen sich zudem alle Unicode Zeichen mit einem
4-elementigen Hexadezimalen Code abrufen. Achtung: C# ist dabei etwas kleinlich, `0x027` ist keine gültige Eingabe.

```csharp            FancyCharacters.cs
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
* Arraytypen sind Referenztypen, die `IEnumerable` und `IEnumerable<T>` implementieren, können also mit `foreach` iteriert werden.

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
style="width: 100%; max-width: 560px; display: block; margin-left: auto; margin-right: auto;"
-->
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
                    |[1]|       |[0],[0]│[0],[0]|
                    +---+       +-------+-------+-------+
                    |[2]|       |[0],[0]│[0],[1]│[0],[1]|
                    +---+       +-------+-------+-------+                      .
```

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

## Umgang mit Variablen

                                    {{0-1}}
********************************************************************************

**Implizit typisierte Variablen**

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
```csharp    UsageVar.cs
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

********************************************************************************

                                 {{1-2}}
*******************************************************************************

**Nullable - Leere Variablen**


Ein "leer-lassen" ist nur für Referenzdatentypen möglich, Wertedatentypen können nicht uninitialisiert bleiben (Compilerfehler)


<!-- --{{0}}-- Idee des Codefragments:
    * Der Ausgangszustand generiert einen Fehler
    * Initalisierung mit string text = null
    * Evaluation von int i = null;
-->
```csharp                                      Initialisation.cs
using System;

namespace Rextester
{
  public class Program
  {
    public static void Main(string[] args){
      string text;
      // int i = null;

      try{
        Console.WriteLine("Der Inhalt von text ist ->{0}<-", text);
      }
      catch (Exception e)
      {
        throw new Exception(e.ToString());
      }
    }
  }
}
```
@Rextester.eval(@CSharp)

Aus der Definition heraus kann zum beispiel eine `int` Variable nur einen Wert zwischen int.MinValue und int.MaxValue annehmen. Eine `null` ist nicht vorgesehen und eine `0` gehört zum "normalen" Wertebereich.

Um gleichermaßen "nicht-besetzte" Werte-Variablen zu ermöglichen integriert C#
das Konzept der sogenannte null-fähige Typen (*nullable types*) ein. Dazu wird
dem Typnamen ein  Fragezeichen angehängt. Damit ist es möglich diesen auch den
Wert `null` zuzuweisen bzw. der Compiler realisiert dies.

<!-- --{{0}}-- Idee des Codefragments:
    * einfache Variable ist mit null initialisierbar
    * Standardkonstruktor realisiert korrekte null Initialisierung
-->
```csharp                                      Iniitalisation
using System;

namespace Rextester
{
  //public struct Person{
  //  string name;
  //  int? alter;
  //}

  public class Program
  {
    public static void Main(string[] args){
      int? i = null;
      if (i == null) Console.WriteLine("Die Variable hat keinen Wert!");
      else Console.WriteLine("Der Wert der Variablen ist {0}", i);
    }
  }
}
```
@Rextester.eval(@CSharp)

Jeder Typ? wird vom Compiler dazu in einen generischen Typ `Nullable<Typ>`
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

********************************************************************************
