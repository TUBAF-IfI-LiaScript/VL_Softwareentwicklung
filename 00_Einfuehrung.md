<!--

author:   Sebastian Zug & André Dietrich
email:    zug@ovgu.de   & andre.dietrich@ovgu.de
version:  0.0.1
language: de
narrator: Deutsch Female

import: https://raw.githubusercontent.com/liascript-templates/plantUML/master/README.md
        https://raw.githubusercontent.com/liaTemplates/DigiSim/master/README.md
        https://github.com/liascript/CodeRunner

icon: https://upload.wikimedia.org/wikipedia/commons/d/de/Logo_TU_Bergakademie_Freiberg.svg
-->

# Einführung

**TU Bergakademie Freiberg - Sommersemester 2021**

Link auf die aktuelle Vorlesung im Versionsmanagementsystem GitHub

[https://github.com/TUBAF-IfI-LiaScript/VL_Softwareentwicklung/blob/master/00_Einfuehrung.md](https://github.com/TUBAF-IfI-LiaScript/VL_Softwareentwicklung/blob/master/00_Einfuehrung.md)

Die interaktive Form ist unter diese Link zu finden ->
[LiaScript Vorlesung 00](https://liascript.github.io/course/?https://github.com/TUBAF-IfI-LiaScript/VL_Softwareentwicklung/blob/master/00_Einfuehrung.md)

---------------------------------------------------------------------

```csharp    HelloWorld.cs
using System;

namespace Rextester
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Glück auf!");
        }
    }
}
```
@LIA.eval(`["main.cs"]`, `mono main.cs`, `mono main.exe`)

## Zielstellung der Veranstaltung

                                   {{0-1}}
*******************************************************************************

Was steht im Modulhandbuch über diesen Kurs?

**Qualifikationsziele /Kompetenzen:**

_Studierende sollen_

+ _die Konzepte objektorientierten und interaktiven Programmierung verstehen,_
+ _die Syntax und Semantik einer objektorientierten Programmiersprache beherrschen um Probleme kollaborativ bei verteilter Verantwortlichkeit von Klassen von einem Computer lösen lassen,_
+ _in der Lage sein, interaktive Windowsprogramme unter Verwendung einer objektorientierten Klassenbibliothek zu erstellen._

[Auszug aus dem Modulhandbuch 2020]

*******************************************************************************

                                    {{1-2}}
*******************************************************************************

**Zielstellung der Veranstaltung**

| Genereller Anspruch                                                                             | Spezifischer Anspruch                                                              |
| ----------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------- |
| + Verstehen verschiedener Programmierparadigmen UNABHÄNGIG von der konkreten Programmiersprache | + Objektorientierte (und funktionale) Programmierung am Beispiel von C# |
| + praktische Einführung in die methodische Softwareentwicklung                                  | + Arbeit mit ausgewählten UML Diagrammen und Entwurfsmustern                                                                                   |
| + Grundlagen der kooperativ/kollaborative Programmierung und Projektentwicklung                                                   | + Verwendung von Projektmanagementtools und einer Versionsverwaltung für den Softwareentwicklungsprozess                                                                                  |

> **Merke:** "Wir lernen effizient guten Code zu schreiben."

*******************************************************************************

                                  {{2-3}}
*******************************************************************************

**Und wozu brauche ich das?**

Anhand der Veranstaltung entwickeln Sie ein "Gefühl" für guten und schlechten
Code und hinterfragen den Softwareentwicklungsprozess.

**Beispiel 1**

Mariner 1 ging beim Start am 22. Juli 1962 durch ein fehlerhaftes Steuerprogramm verloren, als die Trägerrakete vom Kurs abkam und 293 Sekunden nach dem Start gesprengt werden musste. Ein Entwickler hatte einen Überstrich in der handgeschriebenen Spezifikation eines Programms zur Steuerung des Antriebs übersehen und dadurch statt geglätteter Messwerte Rohdaten verwendet, was zu einer fehlerhaften und potenziell gefährlichen Fehlsteuerung des Antriebs führte.

> **Potentieller Lösungsansatz**: Testen & Dokumentation

**Beispiel 2**

Das Erfassungssystem für die Autobahngebühren für Lastkraftwagen sollte ursprünglich zum 31. August 2003 gestartet werden. Nachdem die organisatorischen und technischen Mängel offensichtlich geworden waren, erfolgte eine mehrfache Restrukturierung. Seit 1. Januar 2006 läuft das System, mit einer Verzögerung von über zwei Jahren, mit der vollen Funktionalität.

Eine Baustelle waren die On-Board-Units (OBU), diese wurden zunächst nicht in ausreichender Stückzahl geliefert und eingebaut werden, da Schwierigkeiten mit der komplexen Software der Geräte bestanden. Die On-Board-Units

+ reagierten nicht auf Eingaben
+ ließen sich nicht ausschalten
+ schalteten sich grundlos aus
+ zeigten unterschiedliche Mauthöhen auf identischen Strecken an
+ wiesen Autobahnstrecken fehlerhaft als mautfrei/mautpflichtig aus

> **Potentieller Lösungsansatz**: Testen auf Integrationsebene, Projektkoordination

*******************************************************************************

## Organisatorisches

                                  {{0-1}}
*******************************************************************************

**Dozenten**

| Name            | Email                                                  |
|:--------------- |:------------------------------------------------------ |
| Sebastian Zug   | sebastian.zug@informatik.tu-freiberg.de                |
| Galina Rudolf   | galina.rudolf@informatik.tu-freiberg.de                |
| Jonas Treumer   | jonas.treumer@informatik.tu-freiberg.de                |
| Nico Sonack     | nico.sonack@student.tu-freiberg.de                     |
| Baldur Paulwitz | baldur-heinrich-eckart.paulwitz@student.tu-freiberg.de |

*******************************************************************************

                                  {{1-2}}
*******************************************************************************

**Ablauf**

Jetzt wird es etwas komplizierter ... die Veranstaltung kombiniert nämlich zwei Vorlesungen:

<!--data-type="none"-->
|                 | _Softwareentwicklung (SEW)_            | _Einführung in die Softwareentwicklung (EiS)_ |
| --------------- | -------------------------------- | --------------------------------------- |
| Hörerkreis      | Fakultät 1 + interessierte Hörer | Fakultät 4 - Studiengang Engineering    |
| Leistungspunkte | 9                                | 6                                       |
| Vorlesungen     | 28 (2 Feiertage)                 | 15 (bis 31. Mai 2021)                                     |
| Übungen         | ab Mai 2 x wöchentlich           | ab Mai 8 Übungen (nominell 14 tägig)    |
| Prüfungsform    | Klausur oder Projekt             | maschinenbauspezifisches Software-Projekt (im Wintersemester 2021/22)                                        |

> **Ermunterung an unsere EiS-Hörer**: Nehmen Sie an der ganzen Vorlesungsreihe teil. Den Einstieg haben Sie ja schon gelegt ...

*******************************************************************************

                                  {{2-3}}
*******************************************************************************

**Struktur der Vorlesungen**

<!--data-type="none"-->
| Woche | Tag       | Inhalt der Vorlesung                              | Bemerkung            |
|:----- | --------- |:------------------------------------------------- | -------------------- |
| 1     | 5. April  | _Ostermontag_                                     |                      |
|       | 6. April  | Organisation, Einführung von GitHub und LiaScript |                      |
| 2     | 12. April | Softwareentwicklung als Prozess                   |                      |
|       | 13. April | Konzepte von Dotnet und C#                        |                      |
| 3     | 19. April | Elemente der Sprache C# (Datentypen)              |                      |
|       | 20. April | Elemente der Sprache C# (Forts. Datentypen)       |                      |
| 4     | 26. April | Elemente der Sprache C# (Ein-/Ausgaben)           |                      |
|       | 27. April | Programmfluss und Funktionen                      |                      |
| 5     | 3. Mai    | Strukturen / Konzepte der OOP                     | Beginn Übungen       |
|       | 4. Mai    | Säulen Objektorientierter Programmierung          |                      |
| 6     | 10. Mai   | Klassenelemente in C#  / Vererbung                |                      |
|       | 11. Mai   | Klassenelemente in C#  / Vererbung                |                      |
| 7     | 17. Mai   | Versionsmanagement im Softwareentwicklungsprozess |                      |
|       | 18. Mai   | Git und Continuous integration in GitHub          |                      |
| 8     | 24. Mai   | _Pfingstmontag_                                   |                      |
|       | 25. Mai   | Interfaces                                        |                      |
| 9     | 31. Mai   | UML Konzepte                                      | Ende EiS-Vorlesungen |
|       | 1. Juni   | ausgewählten UML Diagrammtypen                    |                      |
| 10    | 7. Juni   | UML Anwendungsbeispiel                            |                      |
|       | 8. Juni   | Testen                                            |                      |
| 11    | 14. Juni  | Dokumentation                                     |                      |
|       | 15. Juni  | Build Toolchains und ihr Einsatz                  |                      |
| 12    | 21. Juni  | Generics                                          |                      |
|       | 22. Juni  | Container                                         |                      |
| 13    | 28. Juni  | Delegaten                                         | ?                    |
|       | 29. Juni  | Events                                            | ?                    |
| 14    | 5. Juli   | Threadkonzepte in C#                              | ?                    |
|       | 6. Juli   | Taskmodell                                        | ?                    |
| 15    | 12. Juli  | Language Integrated Query                         | ?                    |
|       | 13. Juli  |                                                   | ?                    |

> **Frage:** Wie steht es um Ihr Hintergrundwissen?

*******************************************************************************

                                  {{3-4}}
*******************************************************************************

**Durchführung der Vorlesungen**

Als Plattform für die Online Vorlesung nutzen wir eine lokale Instanz von BigBlueButton. Alle Vorlesungen werden aufgezeichnet und stehen im Nachgang zur Verfügung. Den zugehörigen Link finden Sie auf der OPAL Webseite des Kurses.

+ Montags, 14:00 - 15:30
+ Dienstags, 7:30 - 9:00

Die Materialien der Vorlesung sind als Open-Educational-Ressources konzipiert und stehen unter Github bereit.

> Wie können Sie sich einbringen?
> * __Allgemeine theoretische Fragen/Antworten__ ... Dabei können Sie sich über github/ das Opal-Forum in die Diskussion einbringen.
> * __Rückmeldungen/Verbesserungsvorschläge zu den Vorlesungsmaterialien__ ... *"Das versteht doch keine Mensch! Ich würde vorschlagen ..."* ... dann korrigieren Sie uns. Alle Materialien sind Open Source. Senden Sie mir einen Pull-Request und werden Sie Mitautor.

*******************************************************************************

                                  {{4-5}}
*******************************************************************************

**Durchführung der Übungen**

Die Übungen bestehen aus selbständig zu bearbeitenden Aufgaben, wobei einzelne Lösungen in Videokonferenzen besprochen werden. Wir werden die Übungsaufgaben über die Plattform GitHub abwickeln.

Die Übungen beginnen in der Woche zum 3. Mai! Dafür werden wir Aufgaben vorbereiten, mit denen die Inhalte der Vorlesung vertieft werden. Wir motivieren Sie sich dafür ein Gruppen von 2 Studierenden zu organisieren.

Wie bringen Sie sich ein:

> Wie können Sie sich einbringen?
> * __Allgemeine praktische Fragen/Antworten__ ... in den genannten Foren bzw. in den Übungsveranstaltungen
> * __Eigene Lösungen__ ... Präsentation der Implementierungen in den Übungen
> * __Individuelle Fragen__ ... an die Übungsleiter per Mail oder in einer individuellen Session

<!--data-type="none"-->
| Index | C#     | GitHub | Teamarbeit | Inhalte / Teilaufgaben                                                     | Woche |
| ----- | ------ | ------ | ---------- | -------------------------------------------------------------------------- | ----- |
| 1     | Basics | nein   | nein       | Toolchain, Entwicklungsfluss, Datentypen, Fehler, Ausdrücke, Programmfluss | 5-6   |
| 2     | -      | ja     | ja         | Github am Beispiel von Markdown                                            | 7-8   |
| 3     | OOP    | ja     | ja         | Einführungsbeispiel OOP,                                                   | 9     |
|       |        |        |            | *Anwendungsbeispiel:* Computersimulation                                   |       |
| 4     | OOP    | ja     | ja         | Vererbung, virtuelle Methoden, Indexer, Überladene Operatoren,             | 10-11 |
|       |        |        |            | *Anwendungsbeispiel:*  Smartphone (Entwurf mit UML)                        |       |
| 5     | OOP    | ja     | ja         | Vererbung, abstract, virtuell, Generics                                    | 12-13 |
|       |        |        |            | *Anwendungsbeispiel:* Zoo                                                  |       |
| 6     | OOP    | ja     | ja         | Genererische Collections, Delegaten, Events                                | 14-15 |
|       |        |        |            | *Anwendungsbeispiel:* ???????                                              |       |


<!-- style="display: block; margin-left: auto; margin-right: auto; max-width: 815px;" -->
```    ascii

                   .-.       .-.*   
 Einführung C#    ( 1 )     ( 2 ) Einführung Git/ GitHub      
                   '-'       '-'  Teamarbeit
                     \       /
                      \     /
                       v   v                   
                        .-.*
                       ( 3 )  Einführung Objektorientierte Programmierung
                        '-'     
                         |     
                         v     
                        .-.*
  Alle Teilnehmer      ( 4 )  Basiselemente Objektorientierte Programmierung
                        '-'       
 ::::::::::::::::::::::::|:::::::::::::::::::::::::::::::::::::::::::         
                         v
  Informatiker          .-.*
  Mathematiker         ( 5 )  Erweiterte OOP Konzepte, Generics
                        '-'   Entwurfsmuster  
                         |     
                         v     
                        .-.*
                       ( 6 )  Collections, Delegaten, Events
                        '-'       
```

Obwohl Einmütigkeit dazu besteht, dass kooperative Arbeit für Ingenieure Grundlage der täglichen Arbeitswelt ist, bleibt die Wissensvermittlung im Rahmen der Ausbildung nahezu aus:

_Lösen Sie folgende Aufgabe mit Ihrem Team: ... _

> **Spezifisches Ziel:** Wir wollen Sie für die Konzepte und Werkzeuge der kollaborativen Arbeit bei der Softwareentwicklung "sensibilisieren".

+ Wer definiert die Feature, die unsere Lösung ausmachen?
+ Wie behalten wir bei synchronen Codeänderungen der Überblick?
+ Welchen Status hat die Erfüllung der Aufgabe X erreicht?
+ Wie können wir sicherstellen, dass Code in jedem Fall kompiliert und Grundfunktionalitäten korrekt ausführt?
+ ...

*******************************************************************************

                                  {{5-6}}
*******************************************************************************

**Prüfungen**

+ Softwareentwicklung - Konventionelle Klausur / Programmieraufgabe in Zweier-Team
+ Einführung in die Softwareentwicklung - Teamprojekt und Projektpräsentationen

> **In der Klausur werden neben den Programmierfähigkeiten und dem konzeptionellen Verständnis auch die Werkzeuge der Softwareentwicklung adressiert!**

![Klausurergebnisse](./img/00_Einleitung/Klausur2020.png)



*******************************************************************************

                                    {{6-7}}
*******************************************************************************

**Zeitaufwand und Engagement**

Mit der Veranstaltung Softwareentwicklung verdienen Sie sich 9CP. Eine Hochrechnung mit der von der
Kultusministerkonferenz vorgegebenen Formel 1CP = 30 Zeitstunden bedeutet,
dass Sie dem Fach im Mittel über dem Semester __270 Stunden__ widmen sollten ...
entsprechend bleibt neben den Vorlesungen und Übungen genügend Zeit für die
Vor- und Nachbereitung der
Lehrveranstaltungen, die eigenständige Lösung von Übungsaufgaben sowie die
Prüfungsvorbereitung.

*"Erzähle mir und ich vergesse. Zeige mir und ich erinnere. Lass es mich tun und ich verstehe."*

(Konfuzius, chin. Phiolsoph 551-479 v. Chr.)

**Wie können Sie zum Gelingen der Veranstaltung beitragen**

+ Stellen Sie Fragen, seinen Sie kommunikativ!
+ Geben Sie uns Rückmeldungen in Bezug auf die Geschwindigkeit, Erklärmuster, etc.
+ Organisieren Sie sich in *interdisziplinären* Arbeitsgruppen!
+ Lösen Sie sich von vermeindlichen Grundwahrheiten *"in Python wäre ich drei mal schneller gewesen"*, *"VIM ... mehr Editor braucht kein Mensch!"*!

*******************************************************************************

                                    {{7-8}}
*******************************************************************************

**Literaturhinweise**

Literaturhinweise werden zu verschiedenen Themen als Links oder Referenzen
in die Unterlagen integriert.

Es existiert eine Vielzahl kommerzielle Angebote, die aber einzelne Aspekte
in freien Tutorial vorstellen. In der Regel gibt es keinen geschlossenen Kurs
sondern erfordert eine individuelle Suche nach spezifischen Inhalten.

| Medium         | Bemerkung                                                       | Links                                                                              |
| -------------- | --------------------------------------------------------------- | ---------------------------------------------------------------------------------- |
| Online Kurse   | Leitfaden von Microsoft für C# aber auch die Werkzeuge          | [Link](https://docs.microsoft.com/de-de/dotnet/csharp/)                            |
|                | C# Tutorial for Beginners: Learn in 7 Days                      | [Link](https://www.guru99.com/c-sharp-tutorial.html)                               |
|                | Programmierkonzepte von C#                                      | [Link](https://docs.microsoft.com/de-de/dotnet/csharp/programming-guide/concepts/) |
| Videotutorials | Umfangreicher C# Kurs mit guten konzeptionellen Anmerkungen     | [Link](https://www.youtube.com/watch?v=M3lqkuZQBcM&t=2160s)                        |
|                | Einsteigerkurs als Ausgangspunkt für eine Tutorienreihe         | [Link](https://www.youtube.com/watch?v=gfkTfcpWqAY&t=151s)                         |
| Bücher         | J. Albahari, B. Albahari, "C# 7.0 in a Nutshell", O'Reilly 2017 |                                                                                    |
|                | H. Mössenböck, "Kompaktkurs C# 7", dpunkt.verlag                                                                |                                                                                    |
*******************************************************************************

## Werkzeuge der Veranstaltung

Was sind die zentralen Tools unserer Veranstaltung?

* _Vorlesungstool_ -> BigBlueButton [Introduction](https://www.youtube.com/watch?v=uYYnryIM0Uw)
* _Entwicklungsplattform [GitHub](https://github.com/)_
* _Beschreibungssprache für Lerninhalte_ [LiaScript](https://liascript.github.io/)

### Markdown und LiaScript

                                 {{0-1}}
****************************************************************************

Markdown ist eine Auszeichnungssprache für die Gliederung und Formatierung von Texten und anderen Daten. Analog zu HTML oder LaTex werden die Eigenschaften und Organisation von Textelementen (Zeichen, Wörtern, Absätzen) beschrieben. Dazu werden entsprechende "Schlüsselworte", die sogenannten Tags verwandt.

Markdown wurde von John Gruber und Aaron Swartz mit dem Ziel entworfen, die Komplexität der Darstellung so weit zu reduzieren, dass schon der Code sehr einfach lesbar ist. Als Auszeichnungselemente werden entsprechend möglichst kompakte Darstellungen genutzt.

```markdown HelloWorld.md
# Überschrift

__eine__ Betonung __in kursiver Umgebung__

* Punkt 1
* Punkt 2

Und noch eine Zeile mit einer mathematischen Notation $a=cos(b)$!
```

----------------------------------------------------------------------------<h1>Überschrift</h1>
<i>eine <em>Betonung</em> in kursiver Umgebung</i>
<ul>
<li>Punkt 1</li>
<li>Punkt 2</li>
</ul>
Und noch eine Zeile mit einer mathematischen Notation $a=cos(b)$!

----------------------------------------------------------------------------

Eine gute Einführung zu Markdown finden Sie zum Beispiel unter

* [MarkdownGuide](https://www.markdownguide.org/getting-started/)
* [GitHubMarkdownIntro](https://guides.github.com/features/mastering-markdown/)

Mit einem entsprechenden Editor und einigen Paketen macht das Ganze dann auch Spaß

* Wichtigstes Element ist ein Previewer, der es Ihnen erlaubt "online" die Korrektheit der Eingaben zu prüfen
* Tools zur Unterstützung komplexerer Eingaben wie zum Beispiel der Tabellen (zum Beispiel für Atom mit [markdown-table-editor](https://atom.io/packages/markdown-table-editor))
* Visualisierungsmethoden, die schon bei der Eingabe unterstützen
* Rechtschreibprüfung (!)

****************************************************************************


                                 {{1-2}}
****************************************************************************

Eine vergleichbare Ausgabe unter LaTex hätte einen deutlich größeren Overhead, gleichzeitig eröffnet das Textsatzsystem (über einzubindende Pakete) aber auch ein wesentlich größeres Spektrum an Möglichkeiten und Features (automatisch erzeugte Numerierungen, komplexe Tabellen, Diagramme), die Markdown nicht umsetzen kann.

```latex  latexHelloWorld.tex
\documentclass[12pt]{article}
\usepackage[latin1]{inputenc}
\begin{document}
  \section{Überschrift}
  \textit{eine \emph{Betonung} in kursiver Umgebung}
  \begin{itemize}
    \item Punkt 1
    \item Punkt 2
  \end{itemize}
  Und noch eine Zeile mit einer mathematischen Notation $a=cos(b)$!
\end{document}
```

Das Ergebnis sieht dann wie folgt aus

![pdflatexScreenshoot](./img/00_Einleitung/LatexScreenshot.png)<!-- width="70%" -->

****************************************************************************

                                {{2-6}}
****************************************************************************

Allerdings vermisst Markdown trotz seiner Einfachheit einige Features, die
für die Anwendbarkeit in der (Informatik-)Lehre sprechen:

* Ausführbarer Code
* Möglichkeiten zur Visualisierung
* Quizze Tests und Aufgaben
* spezifische Tools für die Modellierung Simulation etc.

****************************************************************************

                                {{3-4}}
****************************************************************************

Beispiele:

**Generierung von UML Diagrammen mit Hilfe von PlantUML**

```code Example.plantUML
@startuml

abstract class AbstractList
abstract AbstractCollection
interface List
interface Collection

List <|-- AbstractList
Collection <|-- AbstractCollection

Collection <|- List
AbstractCollection <|- AbstractList
AbstractList <|-- ArrayList

class ArrayList {
  Object[] elementData
  size()
}

enum TimeUnit {
  DAYS
  HOURS
  MINUTES
}

annotation SuppressWarnings

@enduml
```
@plantUML.eval(png)

****************************************************************************

                                {{4-5}}
****************************************************************************

Beispiele:

**Ausführbarer C# Code**

Wichtig für uns sind die ausführbaren Code-Blöcke, die ich in der Vorlesung nutze, um Beispielimplementierungen zu evaluieren. Dabei werden zwei Formen unterschieden:

**C# 9 mit dotnet Unterstützung**

```csharp  -Coderunner.cs9
using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

int n;
Console.Write("Number of primes: ");
n = int.Parse(Console.ReadLine());

ArrayList primes = new ArrayList();
primes.Add(2);

for(int i = 3; primes.Count < n; i++) {
	bool isPrime = true;
	foreach(int num in primes) isPrime &= i % num != 0;
	if(isPrime) primes.Add(i);
}

Console.Write("Primes: ");
foreach(int prime in primes) Console.Write($" {prime}");
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

**C# 8 mit mono**

```csharp    -HelloWorld.cs
using System;

namespace Rextester
{
  public class Program
  {
    public static void Main(string[] args)
    {
        Console.WriteLine("Glück auf!");
    }
  }
}
```
@LIA.eval(`["main.cs"]`, `mono main.cs`, `mono main.exe`)

> **Frage:** Welche Unterschiede sehen Sie zwischen C#8 und C#9 Code schon jetzt?

****************************************************************************


                           {{5-6}}
****************************************************************************

Eine Reihe von Einführungsvideos findet sich unter [Youtube](https://www.youtube.com/channel/UCyiTe2GkW_u05HSdvUblGYg). Die Dokumentation von LiaScript ist hier [verlinkt](https://liascript.github.io/course/?https://raw.githubusercontent.com/liaScript/docs/master/README.md#1)

Für den [Atom](https://atom.io/)-Editor von GitHub sind zwei LiaScript Plugins verfügbar, die die Arbeit signifikant vereinfachen.

****************************************************************************

### Github

Der Kurs selbst wird als "Projekt" entwickelt. Neben den einzelnen Vorlesungen finden Sie dort auch ein Wiki, Issues und die aggregierten Inhalte als automatisch generiertes Skript.

Link zum GitHub des Kurses

[https://github.com/TUBAF-IfI-LiaScript/VL_Softwareentwicklung](https://github.com/TUBAF-IfI-LiaScript/VL_Softwareentwicklung)

> **Hinweis:** Mit den Features von GitHub machen wir uns nach und nach vertraut.

> **Hinweis:** Natürlich bestehen neben Github auch alternative Umsetzungen für das Projektmanagement wie das Open Source Projekt GitLab oder weitere kommerzielle Tools BitBucket, Google Cloud Source Repositories etc.

### Entwicklungsumgebungen

* Visual Studio
* Visual Studio Code mit C# Erweiterung und .net core [Tutorial](https://www.youtube.com/watch?v=rOzXt--TXLg)
* Atom ...
* VIM ...

**Seien Sie neugierig und probieren Sie verschiedene Tools und Editoren aus!**

## Aufgaben

1. Legen Sie sich einen GitHub Account an (sofern dies noch nicht geschehen ist).
2. Installieren Sie einen Editor Ihrer Wahl auf Ihrem Rechner, mit dem Sie Markdown-Dateien komfortabel bearbeiten können.
3. Recherchieren Sie weitere Softwarebugs. Dabei interessieren uns insbesondere solche, wo der konkrete Fehler direkt am Code nachvollzogen werden konnte. Halten Sie Ihre Erkenntnisse in dem dafür vorgesehenen Wiki fest.
4. Fügen Sie im Wiki einen Link zu Ihrem Editor ein!
