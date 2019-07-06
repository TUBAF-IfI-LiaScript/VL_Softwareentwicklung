<!--

author:   Sebastian Zug & André Dietrich
email:    zug@ovgu.de   & andre.dietrich@ovgu.de
version:  0.0.1
language: de
narrator: Deutsch Female

import: https://raw.githubusercontent.com/LiaTemplates/Rextester/master/README.md
import: https://raw.githubusercontent.com/LiaTemplates/WebDev/master/README.md
-->

# Vorlesung Softwareentwicklung - 26 - Design Patterns

--------------------------------------------------------------------
Link auf die aktuelle Vorlesung im Versionsmanagementsystem GitHub

https://github.com/liaScript/CsharpCourse/blob/master/26_DesignPattern.md

Die interaktive Form ist unter diese Link zu finden ->
[LiaScript Vorlesung 26](https://liascript.github.io/course/?https://raw.githubusercontent.com/liaScript/CsharpCourse/master/26_DesignPattern.md#1)

---------------------------------------------------------------------

**Wie weit sind wir schon gekommen?**

c# Schlüsselwörter:

| abstract    | as       | base     |`bool`      |`break`     |`byte`     |
|`case`       |`catch`   | char     |`checked`   |`class`     | const     |
|`continue`   |`decimal` | default  | delegate   |`do`        |`double`   |
|`else`       |`enum`    | event    | explicit   | extern     |`false`    |
|`finally`    | fixed    |`float`   |`for`       |`foreach`   |`goto`     |
|`if`         | implicit | in       |`int`       | interface  |`internal` |
| is          | lock     |`long`    |`namespace` |`new`       | null      |
| object      | operator |`out`     | override   |`params`    |`private`  |
| protected   |`public`  | readonly |`ref`       |`return`    |`sbyte`    |
| sealed      |`short`   | sizeof   | stackalloc |`static`    |`string`   |
|`struct`     |`switch`  |`this`    |`throw`     |`true`      |`try`      |
| typeof      |`uint`    |`ulong`   |`unchecked` | unsafe     |`ushort`   |
|`using`      | virtual  |`void`    | volatile   |`while`     |           |


Auf die Auführung der kontextabhängigen Schlüsselwörter wie `where` oder
`ascending` wurde hier verzichtet.

---


## Kontrollfragen

*1. afdasdf*


---------------------------------------------------------------------

## Wiederholung

Boxing unboxing



## Design Pattern


### Singleton Pattern

Das Singleton  ist ein in der Softwareentwicklung eingesetztes Entwurfsmuster und gehört zur Kategorie der Erzeugungsmuster. Es stellt sicher, dass von einer Klasse genau ein Objekt existiert. Dieses Singleton ist darüber hinaus üblicherweise global verfügbar. Es soll sicher gestellt werden, dass ein
Resourcenzugriff kanalisiert wird.

Ausgangspunkt der Überlegungen ist die Implementierung einer Klasse PrinterDriver. Über die entsprechenden Hashcodes kann gezeigt werden, dass es
sich um unterschiedliche Instanzen der Klasse handelt.

Welche Möglichkeiten sehen Sie diese Implementierung anzupassen, so dass das Singleton-Pattern realisiert wird?

```csharp    SingletonPatternStart
using System;

namespace Rextester
{
  public class PrinterDriver{
    public void print(string text){
       Console.WriteLine("!PRINT {0}", text);
    }
  }

  public class Program {
    public static void Main(string[] args){
      PrinterDriver MyPrinter = new PrinterDriver();
      PrinterDriver FaultyPrinterInstance = new PrinterDriver();
      Console.WriteLine(MyPrinter.GetHashCode());
      Console.WriteLine(FaultyPrinterInstance.GetHashCode());
    }
  }
}
```
@Rextester.eval(@CSharp)


## Anhang

**Referenzen**


**Autoren**

Sebastian Zug, André Dietrich
