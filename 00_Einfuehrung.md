<!--

author:   Sebastian Zug & André Dietrich
email:    zug@ovgu.de   & andre.dietrich@ovgu.de
version:  0.0.1
language: de
narrator: Deutsch Female

import: https://raw.githubusercontent.com/liaScript/rextester_template/master/README.md

-->

# Vorlesung Softwareentwicklung - 0 - Einführung

**Fragen an die heutige Veranstaltung ...**

* ... wann geht es denn endlich los?

---------------------------------------------------------------------
Link auf die aktuelle Vorlesung im Versionsmanagementsystem GitHub

https://github.com/liaScript/CsharpCourse/blob/master/00_Einfuehrung.md

Die interaktive Form ist unter diese Link zu finden ->
[LiaScript Vorlesung 0](https://liascript.github.io/course/?https://raw.githubusercontent.com/liaScript/CsharpCourse/master/00_Einfuehrung.md#1)

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

## 1. Softwareentwicklung




> „Zielorientierte Bereitstellung und systematische Verwendung von Prinzipien, Methoden und Werkzeugen für die arbeitsteilige, ingenieurmäßige Entwicklung und Anwendung von umfangreichen Softwaresystemen.“
[Balzert, S. 36]


## 2. ... praktisch



## 3. --- Warum den C#

Erläuterung zur Sprachen

### Historie

### Konzepte

### Abgrenzung zu Java


## 4. Beispiel der Woche ... Hello World

```csharp    HelloWorld.cs
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

*A) LiaScript Umgebung*

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


*B) Mono Kommandozeile*

``` bash @output
▶ mcs helloWorld.cs

▶ ls
helloWorld.cs  helloWorld.exe

▶ mono helloWorld.exe
Hello, world!
```

Eine ausführliche Hilfe findet sich unter https://www.mono-project.com/docs/getting-started/mono-basics/
(Allerdings ist dort ein Typo passiert statt des Mono-Compilers wird der .NET
Compiler aufgerufen. Bitte genau hinschauen.)

*C) Monodeveloper*

![EventDrivenGUI](../Bilder/0_Motivation/MonoDeveloper.png)<!-- width="80%" -->


*D) .NET Kommandozeile*


*E) .NET Visual Code*


Evaluieren Sie auch den interaktiven Modus mit gsharp, csharp oder dem .NET
Interpreter unter Visual Studio.
