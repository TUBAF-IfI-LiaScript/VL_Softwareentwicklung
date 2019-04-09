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

### Benannte und optionale Argumente



## 3. Beispiel der Woche ...






## Anhang

**Referenzen**

[MSDoku] C# Dokumentation, "Pattern Matching",  [Link](https://docs.microsoft.com/en-us/dotnet/csharp/pattern-matching)

[WikiMonteCarlo]  ZUM-Wiki, "Monte Carlo Simulation" Autor "Springob", [Link](https://de.wikipedia.org/wiki/Monte-Carlo-Simulation#/media/File:Pi_statistisch.png)

**Autoren**

Sebastian Zug, André Dietrich
