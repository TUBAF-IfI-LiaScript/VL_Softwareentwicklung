<!--

author:   Sebastian Zug & André Dietrich
email:    zug@ovgu.de   & andre.dietrich@ovgu.de
version:  0.0.1
language: de
narrator: Deutsch Female

import: https://raw.githubusercontent.com/LiaTemplates/Rextester/master/README.md
import: https://raw.githubusercontent.com/LiaTemplates/WebDev/master/README.md
-->

# Vorlesung Softwareentwicklung - 15 - Elemente der Entwicklungstoolchain

--------------------------------------------------------------------
Link auf die aktuelle Vorlesung im Versionsmanagementsystem GitHub

https://github.com/liaScript/CsharpCourse/blob/master/15_ToolsII.md

Die interaktive Form ist unter diese Link zu finden ->
[LiaScript Vorlesung 15](https://liascript.github.io/course/?https://raw.githubusercontent.com/liaScript/CsharpCourse/master/15_ToolsII.md#1)

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

*1. Hier stehen jetzt Ihre Fragen ...*

---------------------------------------------------------------------

## 0. Beispielprojekt

Die Anwendung der vorgestellten Tools wird im Beipielprojekt https://gitlab.com/Sebastian_Zug/CsharpCIExample
gezeigt. Dieses ist in zwei Teilprojekte aufgeteilt, einmal für das eigentliche Projekt und im zweiten Teil für das Testen des Projektes.

```
├── doc
│   └── html
│       └── ...                  <- Generierte Dokumentation
├── Doxyfile                     <- Konfigurationsfile für die Doku
├── Makefile                     <- Beispielhafte Konfiguration zum "Bauen"
├── README.md                    <- Beschreibung des Projektes
├── src                          <- Sourcecode der Anwendung
│   ├── Animals
│   │   ├── Cat.cs
│   │   ├── Dog.cs
│   │   ├── IPet.cs
│   │   └── Pet.cs
|   ├── Animals.csproj           <- .NET Projektdatei
│   ├── bin
│   │   └── Debug
│   │       └── ...
│   ├── obj
│   │   └── ...
│   └── Program.cs               <- "Main" Klasse
├── test                         <- Unit-Tests für die Anwendung
│   └── AnimalsTests
│       ├── AnimalsTests.csproj  <- .NET Projektdatei
│       ├── bin
│       │   └── Debug
│       │       └── ...
│       ├── obj
│       │   ├──  ...
│       └── PetTest.cs
└── text.txt
```

![CsharpCIExample](http://www.plantuml.com/plantuml/png/VP51Ry8m38Nl-HK-WYRGpXmGi6aIXurf8EriKW-YfacLf4Eiwt-VcpLQeHEtsE_bytEoy05Tg0ejVAvCYGuBa1gzNuRWrBuAj9RMRDKNmRqugen0AYSr2L6YpF50i3IgGErh4Q_1AuLQ04oWfF221SrWoo1puDuTYnSzI4q_KWPCJnb7fSQz3mvbQ627Ej_PpScG6uqlc5jDL4R0qLREps1r3ZI6sQTLcMY4I0pupRVk0Wx3r8umsnXzRg9yDT57Kmli5dt0o64dd_x9VuEH48_sq27uT1CnM3nkJkZaaSidwpX3Aw-irSnl1j-p8n_wVzJLANzloXk5oXd_JFR4nFYAAUBGzMxjHtB5iOo6wNGUdram62QJxpcq1T__Ar82xinv_bM3hM919giA_GC0)<!-- width="60%" -->


## 1. Versionsmanagement

Nehmen wir an, Sie arbeiten in Ihrem Robotikteam gemeinsam an einer Aufgabenstellung. Ein Kommilitone möchte im Simulator den Navigationsalgorithmus testen, während ein anderes Teammitglied den Code die Dokumentation verfasst. Beide greifen auf die gleiche Codebasis zurück. Wie organisieren Sie das?

Welche Probleme treten auf?

1. Ich weiß nicht welche die aktuelle Version einer Datei ist.
2. Es existieren plötzlich mehrere Varianten einer Datei mit Änderungen an unterschiedlichen Codezeilen.
3. Ich kann den Code nicht kompilieren, weil einzelne Dateien fehlen.
4. Ich kann eine ältere Version der Software nicht finden - "Gestern hat es noch funktioniert".
5. Meine Änderungen wurden von einem Mitstreiter einfach überschrieben.

Während bei der kollaborativen Bearbeitung alle diese Fragen relevant sind,
treten die Probleme 1, 2, 4 auch in individuellen Projekten auf!

### Lösungsansatz

Eine Versionsverwaltung ist ein System, das zur Erfassung von Änderungen an Dokumenten oder Dateien verwendet wird. Alle Versionen werden in einem Archiv mit Zeitstempel und Benutzerkennung gesichert und können später wiederhergestellt werden. Versionsverwaltungssysteme werden typischerweise in der Softwareentwicklung eingesetzt, um Quelltexte zu verwalten.

Ein Beispiel ist das Versionsmanagementsystem von Wikipedia. Jede Änderung eines Artikels wird dokumentiert. Alle Versionen bilden eine Kette, in der die letzte Version als gültige angezeigt wird. Im Unterschied zu Softwareprojekten sind keine Varianten vorgesehen. Entsprechend der Angaben kann nachvollzogen werden: wer wann was geändert hat. Damit ist bei Bedarf eine Rückkehr zu früheren Version möglich.

![VersionsmanagementWikipedia](/img/15_Tools/VersionenVonVersionsverwaltung.png)<!-- width="100%" -->

Hauptaufgaben:

+ Protokollierungen der Änderungen: Es kann jederzeit nachvollzogen werden, wer wann was geändert hat.
+ Wiederherstellung von alten Ständen einzelner Dateien: Somit können versehentliche Änderungen jederzeit wieder rückgängig gemacht werden.
+ Archivierung der einzelnen Stände eines Projektes: Dadurch ist es jederzeit möglich, auf alle Versionen zuzugreifen.
+ Koordinierung des gemeinsamen Zugriffs von mehreren Entwicklern auf die Dateien.
+ Gleichzeitige Entwicklung mehrerer Entwicklungszweige (engl. Branch) eines Projektes, was nicht mit der Abspaltung eines anderen Projekts (engl. Fork) verwechselt werden darf.

### Strategien zur Konfliktvermeidung

**Herausforderung**

Das Beispiel entstammt dem Buch Version Control with Subversion [Subversion](#7)

Zwei Nutzer (Harry und Sally) arbeiten am gleichen Dokument (A), das auf einem
zentralen Server liegt:

+ Beide führen verschiedene Änderungen an ihren lokalen Versionendes Dokuments durch.
+ Die lokalen Versionen werden nacheinander in das Repository geschrieben.
+ Sally überschreibt dadurch eventuell Änderungenvon Harry.

Die zeitliche Abfolge der Schreibzugriffe bestimmt welche Variante des Dokuments A überlebt.

![ProblemKollaborativesArbeiten](/img/15_Tools/HarrySally_svn.png)<!-- width="60%" --> [](#7)

Was sind denn überhaupt Versionen, worin muss sich eine Dokument unterscheiden,
um als neuere Instanz angesehen zu werden?


**Lösung I - Exklusives Bearbeiten**

Bei der pessimistische Versionsverwaltung (*Lock Modify Unlock*) werden einzelne Dateien vor einer Änderung durch den Benutzer gesperrt und nach Abschluss der Änderung wieder freigegeben werden. Während sie gesperrt sind, verhindert das System Änderungen durch andere Benutzer. Der Vorteil dieses Konzeptes ist, dass kein Zusammenführen von Versionen erforderlich ist, da nur immer ein Entwickler eine Datei ändern kann.

![ProblemKollaborativesArbeiten](/img/15_Tools/HarrySally_svnII.png)<!-- width="60%" --> [](#7)

Welche Aspekte sehen Sie an dieser Lösung kritisch?

1. Administrative Probleme ... Gesperrte Dokumente werden vergessen zu entsperren.
2. Unnötige Sequentialisierung der Arbeit ... Wenn zwei Nutzer ein Dokument an verschiedenen Stellen ändern möchten, könnten sie dies auch gleichzeitig tun.
3. Keine Abbildung von übergreifenden Abhängigkeiten ... Zwei Nutzer arbeiten getrennt auf den Dokumenten A und B. Was passiert, wenn A von B abhängig ist? A und B passen nicht mehr zusammen. Die Nutzer müssen dieses Problem diskutieren.

**Lösung II - Kollaboratives Arbeiten mit Mischen**

Optimistische Versionsverwaltungen (*Copy Modify Merge*) versuchen die die Schwächen der pessimistischen Versionsverwaltung zu beheben, in dem siegleichzeitige Änderungen durch mehrere Benutzer an einer Datei zu lassen und anschließend diese Änderungen automatisch oder manuell zusammen führen (Merge).

![ProblemKollaborativesArbeiten](/img/15_Tools/HarrySally_svnMerge.png)<!-- width="60%" --> [](#7)

Ablauf:

+ Harry und Sally kopierendas das Dokument A in ihre lokalen Ordner.
+ Beide arbeiten unabhängig daran und erzeugen die Versionen A' und A''
+ Sally schreibt als Erste das Dokument in das Repository zurück.
+ Harry kann das Dokument nun nicht mehr zurückschreiben, seine Version ist veraltet
+ Harry vergleicht seine lokale Version mit der aktuellen Version im Repository und mischt die Änderungen von Sally mit seinen Anpassungen
+ Die neue (gemischte) Version A\* wird zurückgeschrieben.
+ Sally muss eine neue Leseoperation realisieren, da Ihre lokale Version veraltet ist.

Welche Konsequenzen ergeben sich daraus?

Das Dokument liegt in zeitgleich in n-Versionen vor, die ggf. überlappende Änderungen umfassen.

### Mischen von Dokumenten

In der Praxis wird zwischen zwei Szenarien unterschieden:

1. Mischen unabhäniger Dokumente (2-Wege-Mischen) - Ziel ist die Erzeugung eines neuen Dokumentes, dass die gemeinsamen Komponenten und individuelle Teilmengen vereint.

2. Mischen von Dokumenten mit gemeinsamer Ursprung (3-Wege-Mischen) - Ziel ist die Integration möglichst aller Änderungen der neuen Dokumente in eine weiterentwickelte Version der des Ursprungsdokumentes

> Ein Paar von Änderung aus D1 bzw. D2 gegenüber einen Ausgangsdokument D0 kann unverträglich sein, wenn die Abbildung beiden Änderungen in einem gemeinsamen Dokument nicht möglich ist. In diesem Fall spricht man von einem Konflikt.

Bei einem Konflikt muss eine der beiden ̈Änderungen weggelassen werden. Die Entscheidung darüber kann anhand von zwei vorgehensweisen realisiert werden:

1. Nicht-interaktives Mischen: Es wird zunächst ein Mischergebnis erzeugt, das beide Änderungen umfasst. Über eine entsprechend Semantik werden die notwendigerweise dublizierten Stellen hervorgehoben. Ein Vorteil dieser Vorgehensweise ist, dass eine beliebige weitergehende Editierung zur Konfliktauflosung möglich ist.
2. Interaktives Mischen: Ein Entwickler wird unmittelbar in den Mischprozess eingebunden und um "Schritt-für-Schritt" Entscheidungen gebeten. Denkbare Entscheidungen dabei sind:

    + Übernahme der Änderung gemäß D1 oder D2,

    + Übernahme keiner Änderung,


    + Übernahme von modifizierten Änderung

### Revisionen

Bislang haben wir lediglich einzelne Dateien betrachtet. Logischerweise muss ein übergreifender Ansatz auf Ordnerstrukturen integrieren.

![ProblemKollaborativesArbeiten](/img/15_Tools/Versionsverlauf.png)<!-- width="70%" --> [](#7)

### Formen der Versionsverwaltung

**Lokale Versionsverwaltung**
Bei der lokalen Versionsverwaltung wird oft nur eine einzige Datei versioniert, diese Variante wurde mit Werkzeugen wie SCCS und RCS umgesetzt. Sie findet auch heute noch Verwendung in Büroanwendungen, die Versionen eines Dokumentes in der Datei des Dokuments selbst speichern (Word).

**Zentrale Versionsverwaltung**
Diese Art ist als Client-Server-System aufgebaut, sodass der Zugriff auf ein Repository auch über Netzwerk erfolgen kann. Durch eine Rechteverwaltung wird dafür gesorgt, dass nur berechtigte Personen neue Versionen in das Archiv legen können. Die Versionsgeschichte ist hierbei nur im Repository vorhanden.

Dieses Konzept wurde vom Open-Source-Projekt Concurrent Versions System (CVS) populär gemacht, mit Subversion (SVN) neu implementiert und von vielen kommerziellen Anbietern verwendet.

<!--
style="width: 100%; max-width: 560px; display: block; margin-left: auto; margin-right: auto;"
-->
````````````
                                +-----------------+
                                | V 21.09         |        
                              +-----------------+ |
                              | V 21.10         | |                                                
              Zentrales     +-----------------+ | |                                                       
              Repository    | V 21.11         | | |          
                            |                 | |-+
                            |                 | |
                            |                 |-+  
                            |                 |                        
                            +-----------------+                                                          
                                    |
          +-------------------------+--------------------------+
          |                         |                          |
  +-----------------+       +-----------------+       +-----------------+          
  | V 21.11         |       | V 21.11         |       | V 21.11         |           
  | ABCD            |       | GEFH            |       | IKLM            |           
  |                 |       |                 |       |                 |                         
  +-----------------+       +-----------------+       +-----------------+
    User 1                    User 2                    User 3
    Lokale Kopien
````````````


**Verteilte Versionsverwaltung**
Die verteilte Versionsverwaltung (DVCS, distributed VCS) verwendet kein zentrales Repository mehr. Jeder, der an dem verwalteten Projekt arbeitet, hat sein eigenes Repository und kann dieses mit jedem beliebigen anderen Repository abgleichen. Die Versionsgeschichte ist dadurch genauso verteilt. Änderungen können lokal verfolgt werden, ohne eine Verbindung zu einem Server aufbauen zu müssen.

<!--
style="width: 100%; max-width: 560px; display: block; margin-left: auto; margin-right: auto;"
-->
````````````
                                +-----------------+
                                | V 21.09         |        
                              +-----------------+ |
                              | V 21.10         | |                                                
              Zentrales     +-----------------+ | |                                                       
              Repository    | V 21.11         | | |          
                            |                 | |-+
                            |                 | |
                            |                 |-+  
                            |                 |                        
                            +-----------------+        
                                    |
          +-------------------------+--------------------------+
          |                         |                          |
    +-----------------+      +-----------------+         +-----------------+
    | V 21.09         |      | V 21.09         |         | V 21.09         |
  +-----------------+ |    +-----------------+ |       +-----------------+ |
  | V 21.10         | |    | V 21.10         | |       | V 21.10         | |
+-----------------+ | |  +-----------------+ | |     +-----------------+ | |
| V 21.11         | | |  | V 21.11         | | |     | V 21.11         | | |
|                 | |-+  |                 | |-+   +-----------------+ | |-+
|                 | |    |                 | |     | V 21.12         | | |
|                 |-+    |                 |-+     |                 | |-+
|                 |      |                 |       |                 | |
+-----------------+      +-----------------+       |                 |-+
         |                        |                +-----------------+
         |                        |                          |
         |                        |                          |
+-----------------+       +-----------------+         +-----------------+
| V 21.11         |       | V 21.09         |         | V 21.12         |
| ABCD            |       | GEFH            |         | IKLM            |
|                 |       |                 |         |                 |
+-----------------+       +-----------------+         +-----------------+
  User 1                    User 2                      User 3

````````````

| Zentrale Versionsverwaltung                                       | Verteilte Versionsverwaltung                                |
| ----------------------------------------------------------------- | ----------------------------------------------------------- |
| Historie liegt nur auf dem Server                                 | Historie ist den lokalen Repositiories bekannt              |
| Zentrales Repository als Verbindungselement                       | n gleichberechtigte Repositories                            |
| Konflikte bei Manipulation eines Dokumentes durch mehrere Autoren | Existenz paralleler  Versionen eines Dokumentes abgesichert |
| Sequenz von Versionen                                             | gerichteter azyklischer Graph                               |


###git

**Basisbefehle**

Git ist eine freie Software zur verteilten Versionsverwaltung von Dateien, die durch Linus Torvalds initiiert wurde.

![ProblemKollaborativesArbeiten](/img/15_Tools/gitProcessing.png)<!-- height="400px" --> [max](#7)

| Befehl       | Vorgang                                                                      |
| ------------ | ---------------------------------------------------------------------------- |
| git clone    | Erzeugung eines lokalen Repositories                                         |
| git add      | Markiert eine Datei für das "Staging"                                        |
| git commit   | Übergabe aller Stage-Dokumente an das lokale Repository                      |
| git push     | Synchronisation des lokalen mit einem Remote Repository                      |
| git fetch    | Übernahme der Versionen vom Remote Repository                                |
| git checkout | Wechsel zwischen Branches oder Zugriff auf Version des Projektes             |
| git merge    | Mischt die Änderungen des lokalen Repositories mit der aktullen Arbeitskopie |

Wie sieht ein üblicher Ablauf bei der Bearbeitung eines Projektes aus?

```console
▶ git clone https://gitlab.com/Sebastian_Zug/CsharpCIExample.git
Cloning into 'CsharpCIExample'...
remote: Enumerating objects: 95, done.
remote: Counting objects: 100% (95/95), done.
remote: Compressing objects: 100% (72/72), done.
remote: Total 95 (delta 40), reused 46 (delta 19)
Unpacking objects: 100% (95/95), done.
▶ cd CsharpCIExample
▶ ls
Doxyfile  Makefile  README.md  src  test
▶ cat README.md
# CsharpCIExample

Zeigt die Features von GitLab CI im Kontext eines C#-Projektes. Dabei sollen
insbesondere die automatisierten Test und Buildkonzepte vorgestellt werden.
```

Hier fügen wir nun einen ergänzenden Inhalt in README.md ein.

```console
▶ git status
On branch master
Your branch is ahead of 'origin/master' by 1 commit.
  (use "git push" to publish your local commits)

Changes not staged for commit:
        modified:   ../README.md
▶ git add README.md
▶ git commit -m "Adds additional information to README"
▶ git push
```

Um also danach auf eine bestimmte Version zurückzukehren können Sie mittels
`checkout` in der Historie suchen.

```console
% git checkout 9         
Completing recent commit object name
9bfb686  -- [9bfb686] Replaces wrong path reference (50 minutes ago)
9757c47  -- [9757c47] Replaces cd.. by referencing project files directly (55 minutes ago)
99861ba  -- [HEAD~17] Includiert erweiterte before_script Anweisungen (5 days ago)
```

**Arbeiten mit Branches**

Die Organisation von Versionen in unterschiedlichen Branches ist ein zentrales
Element der Arbeit mit git. Branches sind Verzweigungen des Codes, die bestimmte
Entwicklungsziele kapseln.

![GitWorkFlow](/img/15_Tools/Gitflow-Workflow.png)<!-- height="300px" --> [seibert](#7)

Der größte Nachteil bei der Arbeit mit nur einem Branch liegt darin, dass bei einem
defekten Master(-Branch) die Arbeit sämtlicher Beteiligter unterbrochen wird. Branches
schaffen einen eignen (temporären) Raum für die Entwicklung neuer Features, ohne
die Stabilität des Gesamtsystems zu gefährden. Gleichzeitig haben die Entwickler den gesamten Verlauf eines Projekts in strukturierter Art zur Hand.

Nehmen wir an, dass Sie einen neuen Branch "feature_x" anlegen wollen, um eine
zusätzliche Klasse "newClass.cs" zu realisieren.

```
▶git checkout -b feature_x
▶touch newFile.txt
▶git add newClass.cs
▶git commit -m "Adds a new fancy feature"
▶git checkout master
▶git merge feature_x
```

## Continuous Integration

Das Ziel der kontinuierlichen Integration ist die Steigerung der
Softwarequalität. Typische Aktionen sind das Übersetzen und Linken der
Anwendungsteile, prinzipiell können aber auch beliebige andere Operationen zur
Erzeugung abgeleiteter Informationen durchgeführt werden. Üblicherweise wird
dafür nicht nur das Gesamtsystem neu gebaut, sondern es werden auch
automatisierte Tests durchgeführt und Softwaremetriken zur Messung der
Softwarequalität erstellt. Der gesamte Vorgang wird automatisch ausgelöst durch
Einchecken in die Versionsverwaltung.

**Vorteile CI**

+ Integrations-Probleme werden laufend entdeckt und behoben (gefixt) – nicht erst kurz vor einem Meilenstein.
+ zwingende Realisierung der Unittests
+ ständige Verfügbarkeit eines lauffähigen Standes für Demo-, Test- oder Vertriebszwecke.
+ sofortige Reaktion des Systems auf das Einchecken eines fehlerhaften oder unvollständigen Codes „erzieht“ die Entwickler
+ erzwingt kürzeren Checkin-Intervallen - Der Merge-Aufwand wird immer größer, je länger man mit der Integration wartet.

| Vorteile                                                  | Nachteile                                                                                                    |
| --------------------------------------------------------- | ------------------------------------------------------------------------------------------------------------ |
| Permanente Qualitätssicherung                             | Umstellung von gewohnten Prozessen                                                                           |
| Genaue Aufzeichnung von Änderungen und deren Auswirkungen | Benötigt zusätzliche Server und Umgebungen                                                                   |
| Testsausführung auf allen Integrationsebenen              | Es kann zu Wartezeiten kommen, wenn mehrere Entwickler annähernd gleichzeitig ihren Code integrieren möchten |

## Anhang

**Referenzen**

[subversion]  Brian W. Fitzpatrick, Ben Collins-Sussman, C. Michael Pilato, Version Control with Subversion, 2nd Edition, O'Reilly Media

[max] Macs & Moritz, Sei (k)ein Schwachkopf: Versionsverwaltung mit Git unter Mac OS X, https://macs-moritz.com/sei-kein-schwachkopf-versionsverwaltung-mit-git-unter-mac-os-x

[seibert] Martin Seibert, Git-Workflows: Der Gitflow-Workflow (Teil 1), https://blog.seibert-media.net/blog/2014/03/31/git-workflows-der-gitflow-workflow-teil-1/

**Autoren**

Sebastian Zug, André Dietrich
