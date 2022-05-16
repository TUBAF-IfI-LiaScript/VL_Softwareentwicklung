<!--

author:   Sebastian Zug, Galina Rudolf, André Dietrich, `fjangfaragesh`, `KoKoKotlin` & `Lina`
email:    sebastian.zug@informatik.tu-freiberg.de
version:  1.0.1
language: de
narrator: Deutsch Female

import: https://github.com/LiaTemplates/Pyodide
        https://github.com/liascript/CodeRunner
        https://raw.githubusercontent.com/liaTemplates/tau-prolog/master/README.md

icon: https://upload.wikimedia.org/wikipedia/commons/d/de/Logo_TU_Bergakademie_Freiberg.svg
-->

[![LiaScript](https://raw.githubusercontent.com/LiaScript/LiaScript/master/badges/course.svg)](https://liascript.github.io/course/?https://github.com/TUBAF-IfI-LiaScript/VL_Softwareentwicklung/blob/master/02_DotNet.md)

# .NET und Einordnung der Sprache C#

| Parameter                | Kursinformationen                                                                                                                                                                    |
| ------------------------ | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ |
| **Veranstaltung:**       | `Vorlesung Softwareentwicklung`                                                                                                                                                      |
| **Semester**             | `Sommersemester 2022`                                                                                                                                                                |
| **Hochschule:**          | `Technische Universität Freiberg`                                                                                                                                                    |
| **Inhalte:**             | `Basiskonzepte von C# und dotnet `                                                                                            |
| **Link auf den GitHub:** | [https://github.com/TUBAF-IfI-LiaScript/VL_Softwareentwicklung/blob/master/02_DotNet.md](https://github.com/TUBAF-IfI-LiaScript/VL_Softwareentwicklung/blob/master/02_DotNet.md) |
| **Autoren**              | @author                                                                                                                                                                              |

![](https://media.giphy.com/media/26tn33aiTi1jkl6H6/source.gif)

---------------------------------------------------------------------

## Einschub: Programmierparadigmen

Ein Programmierparadigma bezeichnet die gedankliche, konzeptionelle Grundstruktur
die der Darstellung des Problems in Code zugrunde liegt.

Das Programmierparadigma:

* beschreibt den fundamentalen Programmierstil bzw. Eigenschaften von Programmiersprachen
* unterscheidet sich durch die Repräsentation der statischen und dynamischen Programmelemente
* beruht auf Sprache, aber auch auf individuellem Stil.

<!--
style="width: 100%; max-width: 560px; display: block; margin-left: auto; margin-right: auto;"
-->
```ascii

                         Programmierparadigmen
                                  |
                .-----------------+-----------------.
                |                                   |
    Imperative Programmierung         Deklarative Programmierung
                |                                   |
      .------------------.               .----------+---------.
      |                  |               |                    |
  Prozedural    Objektorientiert    Funktional             Logisch             .

```


                                      {{1-2}}
*******************************************************************************

+ __Imperative Programmierung__ - Quellcode besteht aus einer Folge von Befehlen die in einer festen Reihenfolge abgearbeitet werden.

  + __Prozedurale Programmierung__ - Zerlegung von Programmen in überschaubare Teile, die durch eine definierte Schnittstelle aufrufbar sind (Kernkonzepte: Funktion, Prozedur, Routine, Unterprogramm)
  + __Objektorientierte Programmierung__ - Kapselung von Daten und Funktionen in einem Konzept

*******************************************************************************

                                       {{2-3}}
*******************************************************************************

+ __Deklarative Programmierung__ - es wird kein Lösungsweg implementiert, sondern nur angegeben, was gesucht ist.

  + __Funktionale Sprachen__ - Abbildung der Algorithmen auf funktionale Darstellungen
  + __Logische Sprachen__ -  Ableitung einer Lösung aus einer Menge von Fakten, Generierung einer Auswahl von Daten


```prolog    Prolog.pro
% Prolog Text mit Fakten
mann(adam).
mann(tobias).
mann(frank).
frau(eva).
frau(daniela).
frau(ulrike).
vater(adam,tobias).
vater(tobias,frank).
vater(tobias,ulrike).
mutter(eva,tobias).
mutter(daniela,frank).
mutter(daniela,ulrike).

grossvater(X,Y) :-
     vater(X,Z),
     vater(Z,Y).
```
@Tau.program(Prolog.pro)

```prolog Query
grossvater(adam,frank).
```
@Tau.query(Prolog.pro)

*******************************************************************************


                                      {{3-4}}
*******************************************************************************

* **Weiter Konzepte** ... keine spezifische Zuordenbarkeit

  + **Strukturierte  Programmierung** ... Verzicht bzw. Einschränkung des `Goto` Statements zugunsten von Kontrollstrukturen (Kernkonzepte: Verzweigungen,
     Schleifen)
  + **Nebenläufig**, **Reflektiv**, **Generisch**, ...
  + **Aspektorientierte Programmierung**,


******************************************************************************

                                    {{4-5}}
*******************************************************************************

Viele Sprachen unterstützen verschiedene Elemente der Paradigmen, bzw. entwickeln
sich in dieser Richtung weiter.

| Sprache | imperativ                    | deklarativ |
| ------- | ---------------------------- | ---------- |
| Pascal  | prozedural                   |            |
| C       | prozedural                   |            |
| Ada     | objektorientiert             |            |
| Java    | objektorientiert             |            |
| Python  | objektorientiert,            | funktional |
| C#      | prozedural, objektorientiert | funktional |
| C++     | prozedural, objektorientiert | funktional |
| Haskell |                              | funktional |
| Prolog  |                              | Logisch    |
| SQL     |                              | Logisch    |

Viele Paradigmen in einer Sprache am Beispiel eines Python Programmes ...
Berechnen Sie die Summe der Ziffern eines Arrays.

```python    MultiParadigmen.py
my_list = range(0,10)

# imperative
result = 0
for x in my_list:
    result += x
print("Result in imperative style      :" + str(result))

# procedural
result = 0
def do_add(list_of_numbers):
    result = 0
    for x in my_list:
        result += x
    return result
print("Result in procedural style      :" + str(do_add(my_list)))

# object oriented
class MyClass(object):
    def __init__(self, any_list):
        self.any_list = any_list
        self.sum = 0
    def do_add(self):
      self.sum = sum(self.any_list)
create_sum = MyClass(my_list)
create_sum.do_add()
print("Result in object oriented style :" + str(create_sum.sum))

# functional
import functools
result = functools.reduce(lambda x, y: x + y, my_list)
print("Result in functional style      :" + str(result))

```
@Pyodide.eval

*******************************************************************************

                                  {{5}}
*******************************************************************************


**"Das ist ja alles gut und schön, aber ich ich bin C Programmierer!"**

> **Anti-Pattern "Golden Hammer"**:
> *If all you have is a hammer, everything looks like a nail.*

Lösungsansätze:
* Individuell - Hinterfragen des Vorgehens und der Intuition, bewusste Weiterentwicklung des eigenen Horizontes (ohne auf jeden Zug aufzuspringen)
* im Team - Teilen Sie Ihre Erfahrungen im Team / der Community, besetzen Sie Teams mit Mitarbeitern unterschiedlichen Backgrounds (Technical Diversity)

Weitere Diskussion unter: [https://sourcemaking.com/antipatterns/golden-hammer](https://sourcemaking.com/antipatterns/golden-hammer)

*******************************************************************************

## Warum also C#?

C# wurde unter dem Codenamen *Cool* entwickelt, vor der Veröffentlichung aber
umbenannt. Der Name C Sharp leitet sich vom Zeichen Kreuz (#, englisch sharp)
der Notenschrift ab, was dort für eine Erhöhung des Grundtons um einen Halbton
steht. C sharp ist also der englische Begriff für den Ton *cis* (siehe
Anspielung auf C++)

C#

+ ist eine moderne und nur in überschaubarem Maße durch die eigene Entwicklung "verschandelte" Sprache
+ enthält Elemente vieler verschiedener Paradigmen
+ ist plattformunabhängig
+ bietet eine breite Sammlung von Bibliotheken
+ integriert Bibliotheken und Konzepte für die GUI-Programmierung
+ kann mit anderen Sprachen über .NET interagieren
+ unterstützt Multi-Processing problemlos
+ ist typsicher
+ ...

### Historie der Sprache C#

<!--data-type="none"-->
| Jahr | Version .NET     | Version C# | Ergänzungen                                                                                                                                                 |
| ---- | ---------------- | ---------- | ----------------------------------------------------------------------------------------------------------------------------------------------------------- |
| 2002 | 1.0              | 1.0        |                                                                                                                                                             |
| 2006 | 3.0              | 2.0        | Generics, Anonyme Methoden, Iteratoren, Private setters, Delegates                                                                                          |
| 2007 | 3.5              | 3.0        | Implizit typisierte Variablen, Objekt- und Collection-Initialisierer, Automatisch implementierte Properties, LINQ, Lambda Expressions                       |
| 2010 | 4.0              | 4.0        | Dynamisches Binding, Benannte und optionale Argumente, Generische Co- und Kontravarianz                                                                     |
| 2012 | 4.5              | 5.0        | Asynchrone Methoden                                                                                                                                         |
| 2015 | 4.6              | 6.0        | Exception Filters, Indizierte Membervariablen und Elementinitialisierer, Mehrzeilige String-Ausdrücke, Implementierung von Methoden mittels Lambda-Ausdruck |
| 2017 | 4.6.2/ .NET Core | 7.0        | Mustervergleiche (Pattern matching),  Binärliterale 0b..., Tupel                                                                                            |
| 2019 | .NET Core 3      | 8.0        | Standardimplementierungen in Schnittstellen, Switch Expressions, statische lokale Funktionen, Index-Operatoren für Teilmengen                               |
| 2020 | .NET 5.0         | 9.0        | Datensatztypen (Records),   Eigenschafteninitialisierung, Anweisungen außerhalb von Klassen, Verbesserungen beim Pattern Matching                           |
| 2021 | .NET 6.0         | 10.0       | Erforderliche Eigenschaften, Null-Parameter-Prüfung, globale Using-Statements                                                                               |

Die Sprache selbst ist unmittelbar mit der Ausführungsumgebung, dem .NET Konzept verbunden und war ursprünglich stark auf Windows Applikationen zugeschnitten.

### Konzepte und Einbettung

                                        {{0-2}}
********************************************************************************


.NET ist ein Sammelbegriff für mehrere von Microsoft/Dritten herausgegebene Software-Plattformen, die der Entwicklung und Ausführung von Anwendungsprogrammen dienen. Dabei erlebt die Plattform einen permanenten Wandel. Die Bedeutung der einzelnen Teile und Technologien, die .NET umfasst, hat sich im Laufe der Zeit gewandelt. Stand November 2020 spielen folgende Frameworks eine herausgehobene Rolle in der Praxis:

+ das nur unter Windows unterstützte klassische .NET Framework, das mit der Version 4.8 in der letzten Version vorliegt.
+ das als dessen Nachfolger positionierte, auf verschiedenen Plattformen unterstützte Framework .NET 5 (bei dem auch einige Techniken gekündigt wurden). Es wurde mehrere Jahre parallel unter der Bezeichnung .NET Core entwickelt.
+ die Plattform Mono und darauf basierende Techniken (von Microsoft meist als Xamarin bezeichnet). Diese unterstützt seit längerem .NET auf verschiedenen Plattformen (in der Vergangenheit jedoch oft unvollständig implementiert).

    --{{0}}--
Mono ist eine alternative, ursprünglich unabhängige Implementierung von Microsofts .NET Standards. Sie ermöglicht die Entwicklung von plattformunabhängiger Software auf den Standards der Common Language Infrastructure und der Programmiersprache C#. Entstanden ist das Mono-Projekt 2001 unter Führung von Miguel de Icaza von der Firma Ximian, die 2003 von Novell aufgekauft wurde. Die Entwickler wurden 2011 in eine neue Firma namens Xamarin übernommen, die im Jahr 2016 eine Microsoft-Tochtergesellschaft wurde. In der Folge wurde Microsoft Hauptsponsor des Projektes.

Mono "hinkt" als Open Source Projekt der eigentlichen Entwicklung etwas nach.

********************************************************************************

                                        {{1-2}}
********************************************************************************

```ascii
       .NET Core 3.1
       Teile von .NET Framework
       Teile von Mono
    +  neue Features
    ------------------                                                                                  .
       .NET 5.0
```

Der Artikel in [heise](https://www.heise.de/developer/meldung/NET-5-Zweite-Preview-bringt-viel-Feinschliff-4696009.html) fasst diesen Status gut zusammen.

Zum Vergleich sei auf eine Darstellung der **vor dem Erscheinen von .NET 5** verwiesen:

| Betriebssystem           | .NET Framework | .NET Core | Xamarin |
| ------------------------ | -------------- | --------- | ------- |
| Windows 7/8              | X              | X         |         |
| Windows 10 Desktop       | X              | X         |         |
| Windows 10 Mobile Geräte |                |           | X       |
| Linux                    |                | X         |         |
| macOS                    |                | X         |         |
| iOS                      |                |           | X       |

Einen guten Überblick über das historische Nebeneinander gibt das Video von Tim Corey (Achtung, die Darstellung greift den Stand von 2017 auf!) [Link](https://www.youtube.com/watch?v=Ph_jSGq6vIA)

> .NET 6 ist als LTS-Version im November 2021 erscheinen und wird für 3 Jahre unterstützt.

********************************************************************************

                                      {{2-3}}
********************************************************************************

Ziel des .NET-Ökosystems ist die Erhöhung der Anwendungskompatibilität zwischen verschiedenen Systemen und Plattformen. Programme, die das .NET Framework verwenden, werden in der Regel so ausgeliefert, dass benötigte Komponenten des Frameworks automatisch mit installiert werden.

<!--
style="width: 100%; max-width: 560px; display: block; margin-left: auto; margin-right: auto;"
-->
```ascii
 +--------------------------------------------------+
 |               .NET Anwendungen                   |
 +--------------------------------------------------+

 +--------------------------------------------------+
 |               Klassenbibliothek                  |
 +--------------------------------------------------+

 +--------------------------------------------------+
 |    Common Language Runtime (Laufzeitumgebung)    |
 +--------------------------------------------------+  

 +--------------------------------------------------+
 |     Betriebssystem (Windows, Linux, macOS)       |
 +--------------------------------------------------+                         .
```

* die Laufzeitumgebung (CLR) implementiert die Ausführungsplattform des .NET Codes. Sie umfasst die Sicherheitsmechanismen, Versionierung, automatische Speicherbereinigung und vor allem die Entkopplung der Programmausführung vom Betriebssystem.

* die Klassenbibliothek gliedern sich intern in Basisklassen und eigenen Bibliotheken für verschiedene Anwendungstypen:


  * ASP.NET ... ist ein Web Application Framework, mit dem sich dynamische Webseiten, Webanwendungen und Webservices entwickeln lassen.
  * Windows Forms/ WPF ... ist ein GUI-Toolkit des Microsoft .NET Frameworks. Es ermöglicht die Erstellung grafischer Benutzeroberflächen (GUIs) für Windows.
  * ...

    --{{2}}--
Die Open Source Community stand dem .NET Konzept kritisch gegenüber, da eine "unklare Lage" im Hinblick auf die Lizenzen bestand. Aufgrund der Gefahr durch Patentklagen seitens Microsoft warnte Richard Stallman davor Mono in die Standardkonfiguration von Linuxdistributionen aufzunehme. Ab 2013 änderte Microsoft aber seine Strategie und veröffentlichte den Quellcode von .NET komplett als Open Source unter einer MIT-Lizenz bzw. Apache-2.0-Lizenz.


********************************************************************************

                                   {{3-5}}
********************************************************************************

*Compilierung unter C* (zur Erinnerung und zum Vergleich)

<!--
style="width: 100%; max-width: 560px; display: block; margin-left: auto; margin-right: auto;"
-->
```ascii
Sourccode (.c, .cpp, .h)             |
                                     v
                                Preprocessing  Schritt 1: Präprozessor (cpp)
Erweiterer Sourcecode                |
                                     v
                                Compilation    Schritt 2: Compiler (gcc, g++)
Assembler Code (.s)                  |
                                     v
                                Assemblieren   Schritt 3: Assembler (as)
Maschinencode (.o, .obj)             |
                                     v
Statische Libs (.lib, .a)  ---->  Linken       Schritt 4: Linker (ld)
                                     |
Ausführbarer Maschinencode           v                                         .
```

********************************************************************************

                                   {{4-5}}
********************************************************************************

*Build-Prozess mit C#*

<!--
style="width: 100%; max-width: 560px; display: block; margin-left: auto; margin-right: auto;"
-->
```ascii
                 +--------+     +--------+      +--------+
Source Code      | VB     |     | C++    |      | C#     |
                 +--------+     +--------+      +--------+
                 |Compiler|     |Compiler|      |Compiler|--------------+
                 .--------.     .--------.      .--------.              |
                     |              |               |                   |
                     v              v               v                   |
Common           +--------+     +--------+      +--------+              |
Intermediate     |Assembly|     |Assembly|      |Assembly|              |
Language         +--------+     +--------+      +--------+              |
                     |              |               |                   |
                     v              v               v                   |
                 +---------------------------------------+              |
                 | Common Language Runtime JIT Compiler  |              |
                 +---------------------------------------+              |
                     |              |               |                   |
                     v              v               v                   |
              .- - - - - - - - - - - - - - - - - - - - - - .            v
              !  +--------+     +--------+      +--------+ !       +---------+
              !  |Managed |     |Managed |      |Managed | !       |Unmanaged|
              !  | Code   |     | Code   |      | Code   | !       | Code    |
              !  +--------+     +--------+      +--------+ !       +---------+
              ! Common Language Runtime Services           !
              .- - - - - - - - - - - - - - - - - - - - - - .

               +-------------------------------------------------------------+
               |            Operating System Services                        |
               +-------------------------------------------------------------+
```

Die spezifischen Compiler der einzelnen .NET Sprachen (C#. Visual Basic, F#) bilden den Quellcode
auf einen Zwischencode ab. Die Common Language Infrastructure (CLI) ist eine von ISO und ECMA
standardisierte offene Spezifikation (technischer Standard), die ausführbaren
Code und eine Laufzeitumgebung beschreibt.

*Was passiert unter der Haube der CLR?*

Für die *Managed Code Execution* stellt die CLR ein entsprechendes Set von Komponenten
bereit:

* Class Loader ... Einlesen der Assemblies in die CLR Ausführungsumgebung unter Beachtung der Sicherheits-, Versions-, Typinformationen usw.
* Just-in-Time Compiler ... Abbildung der CIL auf den ausführbaren Maschinencode
* Code Execution und Debugging
* Garbage Collection ... der GC ist für die Bereinigung von Referenz-Objekten auf dem Heap verantwortlich und wird von der CLR zu nicht-deterministischen Zeitpunkten gestartet.


********************************************************************************

                                   {{5}}
********************************************************************************

```cil    CLI
.assembly HalloWelt { }
.assembly extern mscorlib { }
.method public static void Main() cil managed
{
    .entrypoint
    .maxstack 1
    ldstr "Hallo Welt!"
    call void [mscorlib]System.Console::WriteLine(string)
    ret
}
```

Ein Assembly umfasst:
* das Assemblymanifest, das die Assemblymetadaten enthält.
* die Typmetadaten.
* den CIL-Code
* Links auf mögliche Ressourcen.

Ein Assembly bildet:

* **bildet eine Sicherheitsgrenze** - Eine Assembly ist die Einheit, bei der Berechtigungen angefordert und erteilt werden.
* **bildet eine Typgrenze** - Die Identität jedes Typs enthält den Namen der Assembly, in der dieser sich befindet. Wenn der Typ `MyType` in den Gültigkeitsbereich einer Assembly geladen wird, ist dieser nicht derselbe wie der Typ `MyType`, der in den Gültigkeitsbereich einer anderen Assembly geladen wurde.
* **bildet eine Versionsgrenze** - Die Assembly ist die kleinste, in verschiedenen Versionen verwendbare Einheit in der Common Language Runtime. Alle Typen und Ressourcen in derselben Assembly bilden eine Einheit mit derselben Version.
* **bildet eine Bereitstellungseinheit** - Beim Starten einer Anwendung müssen nur die von der Anwendung zu Beginn aufgerufenen Assemblys vorhanden sein. Andere Assemblys, z. B. Lokalisierungsressourcen oder Assemblys mit Hilfsklassen, können bei Bedarf abgerufen werden. Dadurch ist die Anwendung beim ersten Herunterladen einfach und schlank.

********************************************************************************

### Abgrenzung zu Java

vgl. Vortrag von Mössenböck ([link](https://www.dcl.hpi.uni-potsdam.de/teaching/componentVl05/slides/Net_VL2_01_Java-CS-Moessenboeck.pdf))

<!--data-type="none"-->
|                  | Java                                        | C#                                         |
| ---------------- | ------------------------------------------- | ------------------------------------------ |
| Veröffentlichung | 1995                                        | 2001                                       |
| Plattform        | (Java) Unix/Linux,  Windows, MacOS, Android | (.NET) Windows, Linux, Android, iOS, MacOS |
| VM               | Java-VM                                     | CLR                                        |
| Zwischencode     | Java-Bytecode                               | CIL                                        |
| JIT              | per Methoden                                | per Methode / gesamt                       |
| Komponenten      | Beans                                       | Assemblies                                 |
| Versionierung    | nein                                        | ja                                         |
| Leitidee         | Eine Sprache auf vielen Plattformen         | Viele Sprachen auf vielen Plattform        |


## Es wird konkret ... Hello World

Die organisatorischen Schlüsselkonzepte in C# sind: **Programme**, **Namespaces**,
**Typen**, **Member** und **Assemblys**. C#-Programme bestehen aus mindestens einer
Quelldatei, von denen mindestens eine `Main` als einen Methodennamen hat.
Programme deklarieren Typen, die Member enthalten, und können in Namespaces
organisiert werden.

Wenn C#-Programme kompiliert werden, werden sie physisch in Assemblys verpackt.
Assemblys haben unter Windows Betriebssystemen die Erweiterung .exe oder
.dll, je nachdem, ob sie Anwendungen oder Bibliotheken implementieren.

<!--
style="width: 100%; max-width: 560px; display: block; margin-left: auto; margin-right: auto;"
-->
```ascii
                                     C# Plattformen
                                           |
             .-----------------------------+------------------------.
             |                             |                        |  
             |                             |                        |
   A) Vorlesungsmaterialien          B) Webseiten               C) Lokal
          interaktiv                       |
             |                             |
    .--------+---------.         .---------+---------.
    |                  |         |                   |
  mono (C#7)    dotnet (C#10)    |                   |
                                 |                   |
                          dotnetfiddle.net        repl.it
                             bis C#10               C#8                        .
```


*A) Vorlesungsmaterialien - LiaScript Umgebung*

Die LiaScript basierte Komplierung und Ausführung kann wie bereits erläutert auf der Basis von mono und dem dotnet Framework umgesetzt werden.

```csharp    Mono.cs
using System;

public class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, world!");
    }
}
```
@LIA.eval(`["main.cs"]`, `mono main.cs`, `mono main.exe`)


```csharp  Coderunner.cs9
using System;

Console.WriteLine("Hello, world!");
```
```xml   -myproject.csproj
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net5.0</TargetFramework>
  </PropertyGroup>
</Project>
```
@LIA.eval(`["Program.cs", "project.csproj"]`, `dotnet build -nologo`, `dotnet run -nologo`)


*B) Repl.it*

https://replit.com/

*C) .NET Kommandozeile*

Das .NET Core Framework kann unter [.NET](https://dotnet.microsoft.com/download/linux-package-manager/rhel/sdk-current)
für verschiedene Betriebssystem heruntergeladen werden. Das SDK umfasst sowohl die Bibliotheken, Laufzeitumgebung und Tools. An dieser Stelle sei nur auf die `dotnet` Tools verwiesen, die anderen Werkzeuge werden zu einem späteren Zeitpunk eingeführt.

``` bash
> dotnet new console
> dotnet build
> dotnet run
```

Aus dem Generieren eines neuen Konsolenprojektes ergibt sich ein beeindruckender
Baum von Projektdateien.

``` bash
> dotnet new console

> tree
.
├── bin
│   └── Debug
│       └── net5.0
│           ├── ref
│           │   └── visual_studio_code.dll
│           ├── visual_studio_code
│           ├── visual_studio_code.deps.json
│           ├── visual_studio_code.dll
│           ├── visual_studio_code.pdb
│           ├── visual_studio_code.runtimeconfig.dev.json
│           └── visual_studio_code.runtimeconfig.json
├── obj
│   ├── Debug
│   │   └── net5.0
│   │       ├── apphost
│   │       ├── ref
│   │       │   └── visual_studio_code.dll
│   │       ├── visual_studio_code.AssemblyInfo.cs
│   │       ├── visual_studio_code.AssemblyInfoInputs.cache
│   │       ├── visual_studio_code.assets.cache
│   │       ├── visual_studio_code.csprojAssemblyReference.cache
│   │       ├── visual_studio_code.csproj.CoreCompileInputs.cache
│   │       ├── visual_studio_code.csproj.FileListAbsolute.txt
│   │       ├── visual_studio_code.dll
│   │       ├── visual_studio_code.GeneratedMSBuildEditorConfig.editorconfig
│   │       ├── visual_studio_code.genruntimeconfig.cache
│   │       └── visual_studio_code.pdb
│   ├── project.assets.json
│   ├── project.nuget.cache
│   ├── visual_studio_code.csproj.nuget.dgspec.json
│   ├── visual_studio_code.csproj.nuget.g.props
│   └── visual_studio_code.csproj.nuget.g.targets
├── Program.cs
└── visual_studio_code.csproj
```

*C) .NET Visual Code*

Alternativ können Sie auch die Microsoft Visual Studio oder Visual Code Suite nutzen.
Diese kann man zum Beispiel auf unser gerade erstelltes Projekt anwenden

https://code.visualstudio.com/docs/languages/csharp


## Aufgaben

- [ ] Installieren Sie das .NET 6 auf Ihrem Rechner und erfreuen Sie sich an einem ersten "Hello World"
- [ ] Testen Sie mit einem Kommilitonen die Features von repl.it! Arbeiten Sie probeweise an einem gemeinsamen Dokument.
- [ ] Legen Sie sich einen GitHub-Account an.

<!--START_SKIP_IN_PDF-->

## Quizze - Vorwissenstest

Was wird als Methode/Funktion bezeichnet?

- [(X)] Codeblock, welcher durch Aufruf seines Bezeichners ausgeführt wird
- [( )] Ein beliebiger Programmabschnitt zwischen zwei geschweiften Klammern
- [( )] Die Berechnung eines Wertes infolge der Auswertung eines Ausdrucks

Welcher der folgenden Schlüsselwörter ist keine Bezeichnung für eine Schleife:

[[  while
|   do while
|   (goto)
|   for
]]

Vervollständige die folgenden Entscheidungs-Statements:

``` c
*A* (Variable)
{
  1:
    //CODE
    break;
  2:
    //CODE
    break;
  3:
    //CODE
    break;
}
```
A:

[[switch]]

```
*B* (Bedingung)
{ //CODE }
*C*
{ //CODE }
```

B:

[[if]]

C:

[[else]]

<!--END_SKIP_IN_PDF-->
