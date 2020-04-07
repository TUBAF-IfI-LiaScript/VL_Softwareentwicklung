<!--

author:   Sebastian Zug & André Dietrich
email:    zug@ovgu.de   & andre.dietrich@ovgu.de
version:  0.0.1
language: de
narrator: Deutsch Female

import: https://raw.githubusercontent.com/liaScript/rextester_template/master/README.md
        https://raw.githubusercontent.com/liascript-templates/plantUML/master/README.md
        https://raw.githubusercontent.com/liaTemplates/DigiSim/master/README.md

script: https://cdn.jsdelivr.net/gh/liaTemplates/DigiSim/js/parser.min.js
        https://tilk.github.io/digitaljs/main.js
        https://s.plantuml.com/synchro2.min.js


-->

# Vorlesung Softwareentwicklung - 0 - Einführung

**TU Bergakademie Freiberg - Sommersemester 2020**

Link auf die aktuelle Vorlesung im Versionsmanagementsystem GitHub

https://github.com/SebastianZug/CsharpCourse/blob/SoSe2020/00_Einfuehrung.md

Die interaktive Form ist unter diese Link zu finden ->
[LiaScript Vorlesung 00](https://liascript.github.io/course/?https://raw.githubusercontent.com/SebastianZug/CsharpCourse/SoSe2020/00_Einfuehrung.md#1)

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
@Rextester.eval(@CSharp)

## 0 - Organisatorisches

                                    {{0-1}}
*******************************************************************************

**Zielstellung der Veranstaltung**

* Verstehen verschiedener Programmierparadigmen UNABHÄNGIG von der konkreten Programmiersprache
* Konzepte objektorientierter Programmierung (Beispiel von C#)
* praktische Einführung in die Werkzeuge der Softwareentwicklung
* Anwendung kooperativer Programmiertechniken bei der Arbeit


**Dozenten**

| Name             | Email                                      |
|:-----------------|:-------------------------------------------|
| Sebastian Zug    | sebastian.zug@informatik.tu-freiberg.de    |
| Galina Rudolf    | galina.rudolf@informatik.tu-freiberg.de    |
| Jonas Treumer    | jonas.treumer@informatik.tu-freiberg.de    |


*******************************************************************************


                                    {{1-2}}
*******************************************************************************

**Konzept der Veranstaltung**

Als Plattform für die Online Vorlesung nutzen wir eine lokale Instanz von BigBlueButton. Den zugehörigen Link finden Sie auf der OPAL Webseite des Kurses.

Die Vorlesung wird in der präsenzfreien Zeit auf folgendem zeitlichen Muster basieren:

<!--
style="width: 80%; min-width: 420px; max-width: 720px;"
-->
```ascii
               | Mittwoch   |     +-------------------------------------------+
vorangegangene | Donnerstag | <-- | Bis Donnerstag sin die Materialien        |
Woche          | Freitag    |     | der kommenden Woche auf github hinterlegt |
               | ...        |     +-------------------------------------------+
               |------------|
aktuelle       | Montag     |
Woche          | Dienstag   | <---- Einreichen der 7 Fragen mit Antworten
               | Mittwoch   | <-+   Rückmeldungen und Fragen via Issues
               |            |   |  +------------------------------------------+
                                +--| Online Vorlesung, die die Inhalte der    |
                                   | anhand Ihrer Rückmeldungen diskutiert    |
                                   +------------------------------------------+
```

Hierfür kombinieren wir:

* __asynchronen / synchrone Online-Vorlesungen__ - Im Rahmen der synchronen Vorlesungen werden die zentralen Elemente vorgestellt. Da es aber in der großen Runde an der Möglichkeit der Interaktion wie in einer echte Vorlesung fehlt, fokussieren wir uns auf den Termin am __Mittwoch um 7:30__.
* __offline Diskussionen und Fragen__ - Die Fragen zu den praktischen Lehrinhalten erörtern wir über das Softwareentwicklungstool github. Damit haben Sie die Möglichkeit Fragen von allgemeinem Interesse in einer großen Runde zu besprechen.
* __asynchronen Übungen / synchronen Übungen__ - Die Übungen bestehen aus selbständig zu bearbeitenden Aufgaben, wobei einzelne Lösungen in Videokonferenzen besprochen werden.

Wie bringen Sie sich ein:

* __Sieben Minuten für sieben Fragen__ ... Einbettung einer studentischen Zusammenfassung in jeden Foliensatz. Wir organisieren Sie in Gruppen von 2 Studenten, die zu Beginn jeder Veranstaltung die zentralen Aspekte der  vorangegangen Vorlesung komprimiert darstellt. __10 dieser Fragestellungen werden Eingang in die Klausur finden.__
* __Allgemeine Fragen/Antworten__ ... Dabei können Sie sich über github in die Diskussion einbringen.
* __Rückmeldungen/Verbesserungsvorschläge zu den Vorlesungsmaterialien__ ... *"Das versteht doch keine Mensch!"* ... dann korrigieren Sie uns. Alle Materialien sind Open Source. Senden Sie mir einen Pull-Request und werden Sie Mitautor.
* __Praktischen Übungsaufgaben__ ... Wir werden die Übungsaufgaben über die Plattform GitHub abwickeln und einzelne Aspekte dann in der Vorlesung/ Übungen besprechen.

Die Übungen beginnen in der Woche zum 20. April! Dafür werden wir Aufgaben vorbereiten, mit denen die Inhalte der Vorlesung vertieft werden. Wir motivieren Sie sich dafür ein Gruppen von 2 oder 3 Studierenden zu organisieren. Die genauen Modalitäten werden noch bekannt gegeben.

*******************************************************************************


                                    {{2-3}}
*******************************************************************************

**Zeitaufwand und Engagement**

Mit der Veranstaltung verdienen Sie sich 9CP. Eine Hochrechnung mit der von der
Kultusministerkonferenz vorgegebenen Formel 1CP = 30 Zeitstunden bedeutet,
dass Sie dem Fach im Mittel über dem Semester __270 Stunden__ widmen sollten ...
entsprechend bleibt neben den Vorlesungen und Übungen genügend Zeit für die
Vor- und Nachbereitung der
Lehrveranstaltungen, die eigenständige Lösung von Übungsaufgaben sowie die
Prüfungsvorbereitung.

*"Erzähle mir und ich vergesse. Zeige mir und ich erinnere. Lass es mich tun und ich verstehe."*

(Konfuzius, chin. Phiolsoph 551-479 v. Chr.)

**Wie können Sie zum Gelingen der Veranstaltung beitragen**

* Stellen Sie Fragen, seinen Sie kommunikativ!
* Organisieren Sie sich in Arbeitsgruppen!
* Experimentieren Sie mit verschiedenen Entwicklungsumgebung um "Ihren Editor"
  zu finden

*******************************************************************************


                                    {{3-4}}
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
* _Entwicklungsumgebung / Editor + Kompiler_ -> Empfehlung: Visual Studio Code mit C# Erweiterung und .net core [Tutorial](https://www.youtube.com/watch?v=rOzXt--TXLg)

**Seien Sie neugierig und probieren Sie verschiedene Tools und Editoren aus!**

*******************************************************************************

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

* Wichtigstes Element ist ein Previewer, der es Ihnen erlaubt "online" die Korrktheit der Eingaben zu prüfen
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

                                {{2-3}}
****************************************************************************

Allerdings vermisst Markdown trotz seiner Einfachheit einige Features, die
für die Anwendbarkeit in der (Informatik-)Lehre sprechen:

* Ausführbarer Code
* Möglichkeiten zur Visualisierung
* Quizze Tests und Aufgaben
* spezifische Tools für die Modellierung Simulation etc.

```
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
@plantUML.eval

Eine Reihe von Einführungsvideos findet sich unter [Youtube](https://www.youtube.com/channel/UCyiTe2GkW_u05HSdvUblGYg). Die Dokumentation von LiaScript ist hier [verlinkt](https://liascript.github.io/course/?https://raw.githubusercontent.com/liaScript/docs/master/README.md#1)

Für den [Atom](https://atom.io/)-Editor von GitHub sind zwei LiaScript Plugins verfügbar, die die Arbeit signifikant vereinfachen.

****************************************************************************

### Github

Link zum GitHub des Kurses

[https://github.com/SebastianZug/CsharpCourse](https://github.com/SebastianZug/CsharpCourse)


## Aufgaben

1. Legen Sie sich einen GitHub Account an (sofern dies noch nicht geschehen ist).
2. Installieren Sie einen Editor Ihrer Wahl auf Ihrem Rechner, mit dem Sie Markdown-Dateien komfortabel bearbeiten können.
3. Arbeiten Sie die Materialien für die nächste Vorlesung durch.
4. Recherchieren Sie weitere Softwarebugs. Dabei interessieren uns insbesondere solche, wo der konkrete Fehler direkt am Code nachvollzogen werden konnte. Halten Sie Ihre Erkenntnisse in dem dafür vorgesehenen Wiki fest.
