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

## 1. Objektorientierung

Konzepte

## 2. Objektorientierung in C#


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
