<!--

author:   Sebastian Zug, Galina Rudolf & André Dietrich
email:    sebastian.zug@informatik.tu-freiberg.de
version:  1.0.2
language: de
narrator: Deutsch Female

import: https://github.com/liascript/CodeRunner
        https://raw.githubusercontent.com/liascript-templates/plantUML/master/README.md
        https://raw.githubusercontent.com/liaTemplates/ExplainGit/master/README.md

icon: https://upload.wikimedia.org/wikipedia/commons/d/de/Logo_TU_Bergakademie_Freiberg.svg
-->

[![LiaScript](https://raw.githubusercontent.com/LiaScript/LiaScript/master/badges/course.svg)](https://liascript.github.io/course/?https://github.com/TUBAF-IfI-LiaScript/VL_Softwareentwicklung/blob/master/13_UML_Modellierung.md)

# Modellierung von Software

| Parameter                | Kursinformationen                                                                                                                                                                          |
| ------------------------ | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ |
| **Veranstaltung:**       | `Vorlesung Softwareentwicklung`                                                                                                                                                            |
| **Semester**             | `Sommersemester 2021`                                                                                                                                                                      |
| **Hochschule:**          | `Technische Universität Freiberg`                                                                                                                                                          |
| **Inhalte:**             | `Motivation der Modellierung von Software`                                                                                                                                |
| **Link auf den GitHub:** | [https://github.com/TUBAF-IfI-LiaScript/VL_Softwareentwicklung/blob/master/13_UML_Modellierung.md](https://github.com/TUBAF-IfI-LiaScript/VL_Softwareentwicklung/blob/master/13_UML_Modellierung.md) |
| **Autoren**              | @author                                                                                                                                                                                    |

![](https://media.giphy.com/media/26tn33aiTi1jkl6H6/source.gif)

---------------------------------------------------------------------

## Neues aus Github

Die erste Aufgabe unter Zuhilfenahme von git / GitHub ist angelaufen. Setzen Sie sich in dieser Woche, soweit das noch nicht geschehen ist, intensiv damit auseinander! Die Techniken sind von zentraler Bedeutung für die weiteren Aufgabenblätter.


Anmerkungen:

+ Versehen Sie Ihre Commits mit aussagekräftigen Bezeichnungen -> [Anleitung](https://t3n.de/news/schreibt-richtig-gute-1214910/)
+ Beenden Sie das Projekt mit der Veröffentlichung eines Release!

> Tragen Sie bitte Ihre Fragebogenschlüssel in die Datei team.config ein. Dies hilft bei der wissenschaftlichen Aufbereitung der Daten ungemein.

## Motivation des Modellierungsgedankens

Um gedanklich wieder in die C# Entwicklung einzutauchen, finden Sie in dem Ordner [code](https://github.com/TUBAF-IfI-LiaScript/VL_Softwareentwicklung/tree/master/code/13_UML_Modellierung) zwei Beispiele für die:

+ Nutzung abstrakter Klassen
+ Verwendung von Interfaces

Überlegen Sie sich alternative Lösungsansätze mit Vor- und Nachteilen für die beschriebenen Implementierungen.

## Prinzipien des (objektorientierten) Softwareentwurfs

> **Merke:** Software lebt!

+ Prinzipien zum Entwurf von Systemen
+ Prinzipien zum Entwurf einzelner Klassen
+ Prinzipien zum Entwurf miteinander kooperierender Klassen

[Robert C. Martin](https://de.wikipedia.org/wiki/Robert_Cecil_Martin)
fasste eine wichtige Gruppe von Prinzipien zur Erzeugung wartbarer und
erweiterbarer Software unter dem Begriff "SOLID" zusammen [^UncleBob]. Robert
C. Martin erklärte diese Prinzipien zu den wichtigsten Entwurfsprinzipien. Die
SOLID-Prinzipien bestehen aus:

* **S** ingle Responsibility Prinzip
* **O** pen-Closed Prinzip
* **L** iskovsches Substitutionsprinzip
* **I** nterface Segregation Prinzip
* **D** ependency Inversion Prinzip

Die folgende Darstellung basiert auf den Referenzen [^Just]. Eine
sehr gute, an einem Beispiel vorangetrieben Erläuterung ist unter [^Krämer] zu finden.

[^Krämer]: Andre Krämer, "SOLID - Die 5 Prinzipien für objektorientiertes Softwaredesign", [Link](https://www.informatik-aktuell.de/entwicklung/methoden/solid-die-5-prinzipien-fuer-objektorientiertes-softwaredesign.html)

[^Just]: Markus Just, IT Designers Gruppe, "Entwurfsprinzipien", Foliensatz Fachhochschule Esslingen [Link](http://www.it-designers-gruppe.de/fileadmin/Inhalte/Studentenportal/Die_SOLID-Prinzipien__Folien___1_.pdf)

[^UncleBob]: Robert C. Martin, Webseite "The principles of OOD", http://www.butunclebob.com/ArticleS.UncleBob.PrinciplesOfOod

### Prinzip einer einzigen Verantwortung (Single-Responsibility-Prinzip SRP)

In der objektorientierten Programmierung sagt das SRP aus, dass jede Klasse nur
eine fest definierte Aufgabe zu erfüllen hat. In einer Klasse sollten lediglich
Funktionen vorhanden sein, die direkt zur Erfüllung dieser Aufgabe beitragen.

> “There should never be more than one reason for a class to change. [^UncleBob]

+  Verantwortlichkeit = Grund für eine Änderung (multiple Veränderungen == multiple Verantwortlichkeiten)

```csharp
public class Employee
{
  public Money calculatePay() ...
  public void save() ...
  public String reportHours() ...
}
```

* Mehrere Verantwortlichkeiten innerhalb eines Software-Moduls führen zu zerbrechlichem Design, da Wechselwirkungen bei den Verantwortlichkeit nicht ausgeschlossen werden können


```csharp                       SpaceStation
public class SpaceStation{
  public initialize() ...
  public void run_sensors() ...
  public void show_sensors() ...
  public void load_supplies(type, quantity) ...
  public void use_supplies(type, quantity) ...
  public void report_supplies () ...
  public void load_fuel(quantity) ...
  public void report_fuel() ...
  public void activate_thrusters() ...
}
```

Eine mögliche separaten Realisierung findet sich unter [Link](https://medium.com/@severinperez/writing-flexible-code-with-the-single-responsibility-principle-b71c4f3f883f)

> **Merke:** Vermeiden Sie "God"-Objekte, die alles wissen.


**Verallgemeinerung**

Eine Verallgemeinerung des SRP stellt Curly’s Law [CodingHorror](https://blog.codinghorror.com/curlys-law-do-one-thing/) dar, welches das Konzept
"methods should do one thing" bis "single source of truth" zusammenfasst und
auf alle Aspekte eines Softwareentwurfs anwendet. Dazu gehören
nicht nur Klassen, sondern unter anderem auch Funktionen und Variablen.

```csharp
var numbers = new [] { 5,8,4,3,1 };
numbers = numbers.OrderBy(i => i);

var numbers = new [] { 5,8,4,3,1 };
var orderedNumbers = numbers.OrderBy(i => i);
```

Da die Variable `numbers` zuerst die unsortierten Zahlen repräsentiert und später
die sortierten Zahlen, wird Curly’s Law verletzt. Dies lässt sich auflösen,
indem eine zusätzliche Variable eingeführt wird.

### Open-Closed Prinzip

Bertrand Meyer beschreibt das Open-Closed-Prinzip durch:
*Module sollten sowohl offen (für Erweiterungen) als auch verschlossen (für Modifikationen) sein.* [^Meyer]

Eine Erweiterung im Sinne des Open-Closed-Prinzips ist beispielsweise die
Vererbung. Diese verändert das vorhandene Verhalten der Einheit nicht, erweitert
aber die Einheit um zusätzliche Funktionen oder Daten. Überschriebene Methoden
verändern auch nicht das Verhalten der Basisklasse, sondern nur das der
abgeleiteten Klasse.

Gegenbeispiel: Ausgangspunkt ist eine Klasse `Employee`, die für unterschiedliche
Angestelltentypen um verschiedenen Algorithmen zur Bonusberechnung versehen werden soll.
Intuitiv ist der Ansatz ein weiteres Feld einzufügen, dass den Typ des Angestellten
erfasst und dazu eine entsprechende Verzweigung zu realisieren ... ein Verstoß gegen das OCP, der sich über eine Vererbungshierachie deutlich wartungsfreundlicher realisieren lässt!

```csharp                                      Iniitalisation
using System;

public class Employee
{
  public string Name {set; get;}
  public int ID {set; get;}

  public Employee(int id, string name){
     this.ID = id; this.Name = name;
  }

  public decimal CalculateBonus(decimal salary){
    return salary * 0.1M;
  }
}

public class Program
{
  public static void Main(string[] args){
    Employee Bernhard = new Employee(1, "Bernhard");
    Console.WriteLine($"Bonus = {Bernhard.CalculateBonus(11234)}");
  }
}
```
@LIA.eval(`["main.cs"]`, `mono main.cs`, `mono main.exe`)

Achtung: Die Einbettung der `CalculateBonus()` Methode in die jeweiligen `Employee` Klassen ist selbst fragwürdig! Dadurch wird eine Funktion an verschiedenen Stellen realisiert, so dass pro Klasse unterschiedliche "Zwecke" umgesetzt werden. Damit liegt ein Verstoß gegen die Idee des SRP vor.

[^Meyer]: Bertrand Meyer, "Object Oriented Software Construction" Prentice Hall, 1988,

###  Liskovsche Substitutionsprinzip (LSP)

> Das Liskovsche Substitutionsprinzip (LSP) oder Ersetzbarkeitsprinzip besagt, dass ein Programm, das Objekte einer Basisklasse T verwendet, auch mit Objekten der davon abgeleiteten Klasse S korrekt funktionieren muss, ohne dabei das Programm zu verändern:
> *"Sei $q(x)$ eine beweisbare Eigenschaft von Objekten $x$ des Typs $T$. Dann soll $q(y)$ für Objekte $y$ des Typs $S$ wahr sein, wobei $S$bein Untertyp von $T$ ist.“* [^Liskov]

Beispiel: Grafische Darstellung von verschiedenen Primitiven

![Liskov](https://www.plantuml.com/plantuml/png/SoWkIImgAStDuN8lIapBB4xEI2rspKdDJSqhKR2fqTLL24fDpYX9JSx69U-QavDPK9oAIpeajQA42we68k9Tb9fPp8L5lPL2LMfcSaPUgeOcbqDgNWhGKG00)<!-- width="60%" -->

Entsprechend sollte eine Methode, die `GrafischesElement` verarbeitet, auch auf  `Ellipse` und `Kreis` anwendbar sein. Problematisch ist dabei allerdings deren unterschiedliches Verhalten. `Kreis` weist zwei gleich lange Halbachsen. Die zugehörigen Membervariablen sind nicht unabhängig von einander.

[^liskov]: Liskov, Barbara H., and Jeannette M. Wing. “A Behavioral Notion of Subtyping.” ACM Transactions on Programming Languages and Systems, vol. 16, no. 6, 1994, pp. 1811–41. doi:10.1145/197320.197383

### Interface Segregation Prinzip

Zu große Schnittstellen sollten in mehrere Schnittstellen aufgeteilt werden,
so dass die implementierende Klassen keine unnötigen Methoden umfasst.
Schnittstellen aufgeteilt werden, falls implementierende Klassen unnötige
Methoden haben müssen. Nach erfolgreicher Anwendung dieses Entwurfprinzips würde
ein Modul, das eine Schnittstelle benutzt, nur die Methoden implementieren
müssen, die es auch wirklich braucht.


```csharp
public interface IVehicle
{
    void Drive();
    void Fly();
}


public class MultiFunctionalCar : IVehicle
{
    public void Drive()
    {
        //actions to start driving car
        Console.WriteLine("Drive a multifunctional car");
    }

    public void Fly()
    {
        //actions to start flying
        Console.WriteLine("Fly a multifunctional car");
    }
}

public class Car : IVehicle
{
    public void Drive()
    {
        //actions to drive a car
        Console.WriteLine("Driving a car");
    }

    public void Fly()
    {
        throw new NotImplementedException();
    }
}
```

Lösung unter Beachtung des Interface Segregation Prinzip

```csharp
public interface ICar
{
    void Drive();
}

public interface IAirplane
{
    void Fly();
}

public class Car : ICar
{
    public void Drive()
    {
        //actions to drive a car
        Console.WriteLine("Driving a car");
    }
}


public class MultiFunctionalCar : ICar, IAirplane
{
    public void Drive()
    {
        //actions to start driving car
        Console.WriteLine("Drive a multifunctional car");
    }

    public void Fly()
    {
        //actions to start flying
        Console.WriteLine("Fly a multifunctional car");
    }
}
```

Man könnte jetzt sogar ein Highlevel Interface realisieren, dass beide Aspekte
integriert.

```csharp
public interface IMultiFunctionalVehicle : ICar, IAirplane
{
}

public class MultiFunctionalCar : IMultiFunctionalVehicle
{
}
```
**Vorteil**

+ übersichtlichere kleinere Schnittstellen, die flexibler kombiniert werden können
+ Klassen umfassen keine Methoden, die sie nicht benötigen

-> Das Prinzip der Schnittstellentrennung verbessert die Lesbarkeit und Wartbarkeit unseres Codes.


### Dependency Inversion Prinzip

> "High-level modules should not depend on low-level modules. Both should
> depend on abstractions. Abstractions should not depend upon details. Details
> should depend upon abstractions" [UncleBob]

Lösungsansatz für die Realisierung ist eine veränderte Sicht auf die
klassischerweise hierachische Struktur von Klassen.


<!--
style="width: 90%; max-width: 560px; display: block; margin-left: auto; margin-right: auto;"
-->
````ascii

  Traditionelle Sicht                 Objektorientierte Perspektive

  +-----------------------+           +-------------------------------+
  | Präsentation          |           |          Präsentation         |
  +-----------------------+           | Realisierung        Interface |
              |                       +-------------------------^-----+
              |                                                 |
              |                                                 |
              v                               +-----------------+
  +-----------------------+           +-------|-----------------------+
  | Anwendung             |           |       |  Anwendung            |
  +-----------------------+           | Realisierung        Interface |
              |                       +-------------------------^-----+
              |                                                 |
              |                                                 |
              v                               +-----------------+
  +-----------------------+           +-------|-----------------------+
  | Verarbeitung          |           |       |  Verarbeitung         |
  +-----------------------+           | Realisierung        Interface |
             |                        +-------------------------^-----+
             |                                                  |
             |                                                  |
             v                                +-----------------+
  +-----------------------+           +-------|-----------------------+
  | Daten                 |           |       |     Daten             |
  +-----------------------+           | Realisierung        Interface |
                                      +-------------------------------+
````


Das folgende Beispiel entstammt der Webseite
https://exceptionnotfound.net/simply-solid-the-dependency-inversion-principle/

Beachten Sie, dass die Benachrichtigungsklasse, eine übergeordnete Klasse, eine
Abhängigkeit sowohl von der E-Mail-Klasse als auch von der SMS-Klasse hat, bei
denen es sich um untergeordnete Klassen handelt. Mit anderen Worten, die
Benachrichtigung hängt von der konkreten Implementierung von E-Mail und SMS ab
und nicht von einer Abstraktion der Implementierung. Da DIP verlangt, dass
sowohl Klassen der höheren als auch der unteren Ebenen von Abstraktionen
abhängen, verstoßen wir derzeit gegen das Prinzip der Abhängigkeitsinversion.


```csharp
public class Email
{
    public string ToAddress { get; set; }
    public string Subject { get; set; }
    public string Content { get; set; }
    public void SendEmail()
    {
        //Send email
    }
}

public class SMS
{
    public string PhoneNumber { get; set; }
    public string Message { get; set; }
    public void SendSMS()
    {
        //Send sms
    }
}

public class Notification
{
    private Email _email;
    private SMS _sms;

    public Notification()
    {
        _email = new Email();
        _sms = new SMS();
    }

    public void Send()
    {
        _email.SendEmail();      // Abhängigkeit von Email
        _sms.SendSMS();          // Abhängigkeit von SMS
    }
}

```

Lösung

```csharp
// Schritt 1: Interface Definition
public interface IMessage
{
    void SendMessage();
}

// Schritt 2: Die niederwertigeren Klassen implmentieren das Interface
public class Email : IMessage
{
    public string ToAddress { get; set; }
    public string Subject { get; set; }
    public string Content { get; set; }
    public void SendMessage()
    {
        //Send email
    }
}

public class SMS : IMessage
{
    public string PhoneNumber { get; set; }
    public string Message { get; set; }
    public void SendMessage()
    {
        //Send sms
    }
}

// Schritt 3: Die höherwertige Klasse wird gegen das Interface implementiert
public class Notification
{
    private IMessage _message;

    public Notification(IMessage messages)
    {
        this._message = message;
    }
    public void Send()
    {
        message.SendMessage();
    }
}

// Variante für multiple Messages
//public class Notification
//{
//    private ICollection<IMessage> _messages;
//    public Notification(ICollection<IMessage> messages)
//    {
//        this._messages = messages;
//    }
//    public void Send()
//    {
//        foreach(var message in _messages)
//        {
//            messages.SendMessage();
//        }
//    }
//}
```

Beispiel aus https://exceptionnotfound.net/simply-solid-the-dependency-inversion-principle/


## Herausforderungen bei der Umsetzung der Prinzipien

                                       {{0-2}}
****************************************************************************

Kunde TU Freiberg: *Entwickeln Sie für mich ein webbasiertes System, mit dem Sie die Anmeldung und Bewertung von Prüfungsleistung erfassen.*

> Welche Fragen sollten Sie dem Kunden stellen, bevor Sie sich daran machen und munter Code schreiben?

****************************************************************************
                                        {{1-2}}
****************************************************************************

Betrachten Sie die Darstellung auf der [Webseite](https://www.programmwechsel.de/lustig/management/schaukel-baum.html). Welche hier scherzhaft beschriebenen Herausforderungen sehen Sie im Projekt?

Wie verzahnen wir den Entwicklungsprozess? Wie können wir sicherstellen,
dass am Ende die erwartete Anwendung realisiert wird?

****************************************************************************

                                         {{2-3}}
****************************************************************************
<!--
style="width: 100%; max-width: 560px; display: block; margin-left: auto; margin-right: auto;"
-->
````````````

         +------------------------------------------------------>   Zeit
         |
         |      Analyse                             Abnahmetest
         |          \                                   ^
         |           v                                 /
         |        Grobentwurf                   Systemtests
         |             \                             ^
         |              v                           /
         |           Feinentwurf             Integrationstests
 Detail- |                \                       ^
 grad    |                 v                     /
         |             Implementierung  --> Modultests
         |
         v
````````````

> Das V-Modell ist ein Vorgehensmodell, das den Softwareentwicklungsprozess in Phasen organisiert. Zusätzlich zu den Entwicklungsphasen definiert das V-Modell auch das Evaluationsphasen, in welchen den einzelnen Entwicklungsphasen Testphasen gegenüber gestellt werden.

vgl. zum Beispiel [Link](https://www.johner-institut.de/blog/iec-62304-medizinische-software/v-modell/)

Achtung: Das V-Modell ist nur eine Variante eines Vorgehensmodells, moderne
Entwicklungen stellen eher agile Methoden in den Vordergrund.

vgl. zum Beispiel [Link](https://entwickler.de/online/agile/agile-methoden-einfuehrung-209035.html)

****************************************************************************

## Unified Modeling Language

> Die Unified Modeling Language, kurz UML, dient zur Modellierung, Dokumentation, Spezifikation und Visualisierung komplexer Softwaresysteme unabhängig von deren Fach- und Realsierungsgebiet. Sie liefert die Notationselemente gleichermaßen für statische und dynamische Modelle zur Analyse, Design und Architektur und unterstützt insbesondere objektorientierte Vorgehensweisen. [^Jeckle]

UML ist heute die dominierende Sprache für die Softwaresystem-Modellierung. Der erste Kontakt zu UML besteht häufig darin, dass Diagramme in UML im Rahmen von Softwareprojekten zu erstellen, zu verstehen oder zu beurteilen sind:

+ Projektauftraggeber prüfen und bestätigen die Anforderungen an ein System, die Business Analysten in Anwendungsfalldiagrammen in UML festgehalten haben;
+ Softwareentwickler realisieren Arbeitsabläufe, die Wirtschaftsanalytiker in Aktivitätsdiagrammen beschrieben haben;
+ Systemingenieure implementieren, installieren und betreiben Softwaresysteme basierend auf einem Implementationsplan, der als Verteilungsdiagramm vorliegt.

UML ist dabei Bezeichner für die meisten bei einer Modellierung wichtigen Begriffe und legt mögliche Beziehungen zwischen diesen Begriffen fest. UML definiert weiter grafische Notationen für diese Begriffe und für Modelle statischer Strukturen und dynamischer Abläufe, die man mit diesen Begriffen formulieren kann.

> **Merke:**  Die grafische Notation ist jedoch nur ein Aspekt, der durch UML geregelt wird. UML legt in erster Linie fest, mit welchen Begriffen und welchen Beziehungen zwischen diesen Begriffen sogenannte Modelle spezifiziert werden.

Was ist UML nicht:

+ vollständige, eindeutige Abbildung aller Anwendungsfälle
+ keine Programmiersprache
+ keine rein formale Sprache
+ kein vollständiger Ersatz für textuelle Beschreibungen
+ keine Methode oder Vorgehensmodell

[^Jeckle]:  Mario Jeckle, Christine Rupp, Jürgen Hahn, Barbara Zengler, Stefan Queins, UML 2 glasklar, Hanser Verlag, 2004

### Geschichte

UML (aktuell UML 2.5) ist durch die Object Management Group (OMG) als auch die ISO (ISO/IEC 19505 für Version 2.4.1) genormt.

![OOPGeschichte](./img/13_UML/OO-historie.png "Darstellung der Historie von UML [^WikiUMLHist]")

[^WikiUMLHist]: https://commons.wikimedia.org/w/index.php?curid=7892951, Autor GuidoZockoll, Mitarbeiter der oose.de Dienstleistungen für Innovative Informatik GmbH - derivative work: File:OO-historie.svg : AxelScheithauer, oose.de Dienstleistungen für Innovative Informatik GmbH - derivative work: Chris828 (talk) - File:Objektorientieren methoden historie.png and File:OO-historie.svg, CC BY-SA 3.0

### UML Werkzeuge

* Tools zur Modellierung - Unterstützung des Erstellungsprozesses, Überwachung der Konformität zur graphischen Notation der UML

    *Herausforderungen:* Transformation und Datenaustausch zwischen unterschiedlichen Tools

* Quellcoderzeugung - Generierung von Sourcecode aus den Modellen

    *Herausforderungen:* Synchronisation der beiden Repräsentationen, Abbildung widersprüchlicher Aussagen aus verschiedenen Diagrammtypen

    (Beispiel mit Visual Studio folgt am Ende der Vorlesung.)

* Reverse Engineering / Dokumentation - UML-Werkzeug bilden Quelltext als Eingabe liest auf entsprechende UML-Diagramme und Modelldaten ab

    *Herausforderungen:* Abstraktionskonzept der Modelle führt zu verallgemeinernden Darstellungen, die ggf. Konzepte des Codes nicht reflektieren.

![OOPGeschichte](./img/13_UML/Doxygen.png "Nutzung einer adaptierten Variante in Doxygen [^WikiDoxygen]")


**Darstellung von UML im Rahmen dieser Vorlesung**

Die Vorlesungsunterlagen der Veranstaltung "Softwareentwicklung" setzen auf die domainspezifische Beschreibungssprache plantUML auf, die verschiedene Aspekte in einer

http://plantuml.com/de/

```text ClassDiagram
@startuml
class Car

Driver - Car : drives >
Car *- Wheel : have 4 >
Car -- Person : < owns

@enduml
```
@plantUML.eval(png)


```text GanttChart
@startuml
robust "Web Browser" as WB
concise "Web User" as WU

@0
WU is Idle
WB is Idle

@100
WU is Waiting
WB is Processing

@300
WB is Waiting
@enduml
```
@plantUML.eval(png)

[^WikiDoxygen]:  https://commons.wikimedia.org/w/index.php?curid=24966914, Doxygen-Beispielwebseite, Autor Der Messer - Eigenes Werk, CC BY-SA 3.0

### Diagramm-Typen

![OOPGeschichte](./img/13_UML/UML-Diagrammhierarchie.png "UML Diagramm-Typen [^WikiUMLDiagrammTypes]")

[^WikiUMLDiagrammTypes]: https://upload.wikimedia.org/wikipedia/commons/thumb/d/da/UML-Diagrammhierarchie.svg/1200px-UML-Diagrammhierarchie.svg.png, Autor "Stkl"- derivative work: File: UML-Diagrammhierarchie.png: Sae1962, CC BY-SA 4.0


**Strukturdiagramme**

| Diagrammtyp                  | Zentrale Frage                                                                                                           |
| ---------------------------- | ------------------------------------------------------------------------------------------------------------------------ |
| Klassendiagramm              | Welche Klassen bilden das Systemverhalten ab und in welcher Beziehung stehen diese?                                      |
| Paketdiagramm                | Wie kann ich mein Modell in Module strukturieren?                                                                        |
| Objektdiagramm               | Welche Instanzen bestehen zu einem bestimmten Zeitpunkt im System?                                                       |
| Kompositionsstrukturdiagramm | Welche Elemente sind Bestandteile einer Klasse, Komponente, eines Subsystems?                                            |
| Komponentendiagramm          | Wie lassen sich die Klassen zu wiederverwendbaren Komponenten zusammenfassen und wie werden deren Beziehungen definiert? |
| Verteilungsdiagramm          | Wie sieht das Einsatzumfeld des Systems aus?                                                                             |

**Verhaltensdiagramme**

| Diagrammtyp                    | Zentrale Frage                                                                                |
| ------------------------------ | --------------------------------------------------------------------------------------------- |
| Use-Case-Diagramm              | Was leistet mein System überhaupt? Welche Anwendungen müssen abgedeckt werden?                |
| Aktivitätsdiagramm             | Wie lassen sich die Stufen eines Prozesses beschreiben?                                       |
| Zustandsautomat                | Welche Abfolge von Zuständen wird für eine eine Sequenz von Einganginformationen realisiert   |
| Sequenzdiagramm                | Wer tauscht mit wem welche Informationen aus? Wie bedingen sich lokale Abläufe untereinander? |
| Kommunikationsdiagramm         | Wer tauscht mit wem welche Informationen aus?                                                 |
| Timing-Diagramm                | Wie hängen die Zustände verschiedener Akteure zeitlich voneinander ab?                        |
| Interaktionsübersichtsdiagramm | Wann läuft welche Interaktion ab?                                                             |


**Begrifflichkeiten**

Ein UML-Modell ergibt sich aus der Menge aller seiner Diagramme. Entsprechend
werden verschiedene Diagrammtypen genutzt um unterschiedliche Perspektiven auf
ein realweltliches Problem zu entwickeln.

![Modelle](https://www.plantuml.com/plantuml/png/JSj1oi9030NW_PmYpFA7ARG7-Ed2fLrwW3WJsq3IWIIzlwAYhXxlVRpP0oqEbIHq2uWEnkiMqDYe1lSzRTm8bFHAvgzIsQfGIbNG7V9bEPUbDnB9W0xFTVh54-DggFhbyNsU88yPIlb_v33yvO_EjBT3vGu0 "Modell vs. Diagramm")

## Aufgaben

- [ ] Bearbeiten Sie die Aufgabe 3 im GitHub Classroom
- [ ] Machen Sie sich mit den SOLID Prinzipien weiter vertraut:

!?[alt-text](https://www.youtube.com/watch?v=R1AimDBGtkY)
