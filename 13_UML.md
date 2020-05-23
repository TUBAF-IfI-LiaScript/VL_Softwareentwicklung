<!--

author:   Sebastian Zug & André Dietrich
email:    Sebastian.Zug@informatik.tu-freiberg.de & andre.dietrich@informatik.tu-freiberg.de
version:  0.0.1
language: de
narrator: Deutsch Female

import: https://raw.githubusercontent.com/liaScript/rextester_template/master/README.md
        https://raw.githubusercontent.com/liascript-templates/plantUML/master/README.md

-->

# Softwareentwicklung - 13 - Modellierung einer Software

**TU Bergakademie Freiberg - Sommersemester 2020**

Link auf die aktuelle Vorlesung im Versionsmanagementsystem GitHub

[https://github.com/SebastianZug/CsharpCourse/blob/SoSe2020/13_UML.md](https://github.com/SebastianZug/CsharpCourse/blob/SoSe2020/13_UML.md)

Die interaktive Form ist unter diese Link zu finden ->
[LiaScript Vorlesung 13](https://liascript.github.io/course/?https://raw.githubusercontent.com/SebastianZug/CsharpCourse/SoSe2020/13_UML.md#1)

---------------------------------------------------------------------

## 7 Fragen in 7 Minuten

**1. Jetzt sind Sie dran ...**

**2. Jetzt sind Sie dran ...**

**3. Jetzt sind Sie dran ...**

**4. Jetzt sind Sie dran ...**

**5. Jetzt sind Sie dran ...**

**6. Jetzt sind Sie dran ...**

**7. Jetzt sind Sie dran ...**


## Prinzipien des (objektorientierten) Softwareentwurfs

> **Merke:** Software lebt!

+ Prinzipien zum Entwurf von Systemen
+ Prinzipien zum Entwurf einzelner Klassen
+ Prinzipien zum Entwurf miteinander kooperierender Klassen

Robert C. Martin [Link](https://de.wikipedia.org/wiki/Robert_Cecil_Martin)
fasste eine wichtige Gruppe von Prinzipien zur Erzeugung wartbarer und
erweiterbarer Software unter dem Begriff "SOLID" zusammen [Martin](#10). Robert
C. Martin erklärte diese Prinzipien zu den wichtigsten Entwurfsprinzipien. Die
SOLID-Prinzipien bestehen aus:

* Single Responsibility Prinzip
* Open-Closed Prinzip
* Liskovsches Substitutionsprinzip
* Interface Segregation Prinzip
* Dependency Inversion Prinzip

Die folgende Darstellung basiert auf den Referenzen [Just](#10) [](#10). Eine
sehr gute, an einem Beispiel vorangetrieben Erläuterung ist unter [Krämer](#10)


### Prinzip einer einzigen Verantwortung (Single-Responsibility-Prinzip SRP)

In der objektorientierten Programmierung sagt das SRP aus, dass jede Klasse nur
eine fest definierte Aufgabe zu erfüllen hat. In einer Klasse sollten lediglich
Funktionen vorhanden sein, die direkt zur Erfüllung dieser Aufgabe beitragen.

> “There should never be more than one reason for a class to change. [Robert C. Martin]

+  Verantwortlichkeit = Grund für eine Änderung (multiple Veränderungen == multiple Verantwortlichkeiten)

```csharp
class Employee
{
  public Money calculatePay()
  public void save()
  public String reportHours()
}
```

* Mehrere Verantwortlichkeiten innerhalb eines Software-Moduls führen zu zerbrechlichem Design, da Wechselwirkungen bei den Verantwortlichkeit nicht ausgeschlossen werden können


```java                       SpaceStation
class SpaceStation{}
  public initialize()
  public void run_sensors()
  public void load_supplies(type, quantity)
  public void use_supplies(type, quantity)
  public void report_supplies ()
  public void load_fuel(quantity)
  public void report_fuel()
  public void activate_thrusters()
}
```

Eine mögliche Realisierung zum Beispiel findet sich unter https://medium.com/@severinperez/writing-flexible-code-with-the-single-responsibility-principle-b71c4f3f883f

> **Merke:** Vermeiden Sie "God"-Objekte


**Verallgemeinerung [WikiSRP]**

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

Da die Variable `numbers` zuerst die unsortierten Zahlen repräsentiert und nachher
die sortierten Zahlen, wird Curly’s Law verletzt. Dies lässt sich auflösen,
indem eine zusätzliche Variable eingeführt wird.

### Open-Closed Prinzip

Bertrand Meyer beschreibt das Open-Closed-Prinzip durch:
*Module sollten sowohl offen (für Erweiterungen) als auch verschlossen (für Modifikationen) sein.* [[Meyer](#11)]

Eine Erweiterung im Sinne des Open-Closed-Prinzips ist beispielsweise die
Vererbung. Diese verändert das vorhandene Verhalten der Einheit nicht, erweitert
aber die Einheit um zusätzliche Funktionen oder Daten. Überschriebene Methoden
verändern auch nicht das Verhalten der Basisklasse, sondern nur das der
abgeleiteten Klasse.

Beispiel: Etablierung einer `ObservableList` auf der Basis der generischen .NET
`List` Klasse:

```csharp
class List<T>{
  public bool Contains (T item);
  public void Sort (Comparison<T> comparison);
}

class ObservableList<T> : List<T>
{
  public event Action CollectionChanged;
}
```


###  Liskovsche Substitutionsprinzip (LSP)

> Das Liskovsche Substitutionsprinzip (LSP) oder Ersetzbarkeitsprinzip besagt, dass ein Programm, das Objekte einer Basisklasse T verwendet, auch mit Objekten der davon abgeleiteten Klasse S korrekt funktionieren muss, ohne dabei das Programm zu verändern:

*"Sei $q(x)$ eine beweisbare Eigenschaft von Objekten $x$ des Typs $T$. Dann soll $q(y)$ für Objekte $y$ des Typs $S$ wahr sein, wobei $S$bein Untertyp von $T$ ist.“* [2]

Beispiel: Grafische Darstellung von verschiedenen Primitiven

![Liskov](http://www.plantuml.com/plantuml/png/SoWkIImgAStDuN8lIapBB4xEI2rspKdDJSqhKR2fqTLL24fDpYX9JSx69U-QavDPK9oAIpeajQA42we68k9Tb9fPp8L5lPL2LMfcSaPUgeOcbqDgNWhGKG00)<!-- width="60%" -->

Beispiel: Vögel / Flugunfähgige Vögel


### Interface Segregation Prinzip

Zu große Schnittstellen sollten in mehrere Schnittstellen aufgeteilt werden,
so dass die implementierende Klassen keine unnötigen Methoden umfasst.
Schnittstellen aufgeteilt werden, falls implementierende Klassen unnötige
Methoden haben müssen. Nach erfolgreicher Anwendung dieses Entwurfprinzips würde
ein Modul, das eine Schnittstelle benutzt, nur die Methoden implementieren
müssen, die es auch wirklich braucht.

                                     {{1-2}}
*******************************************************************************

Was sind eigentlich Interfaces oder Schnittstellen?

Eine Schnittstelle enthält Definitionen für eine Gruppe von zugehörigen
Funktionalitäten, die von einer Klasse oder einer Struktur implementiert werden
können.

Eine Schnittstelle gibt an, welche Methoden vorhanden sind oder vorhanden sein
müssen. Zusätzlich zu dieser syntaktischen Definition sollte ein so genannter
Kontrakt über die Bedeutung (Semantik) definiert werden.

https://docs.microsoft.com/de-de/dotnet/csharp/programming-guide/interfaces/


*******************************************************************************

                                      {{2-3}}
*******************************************************************************

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

```
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

*******************************************************************************

### Dependency Inversion Prinzip

> "High-level modules should not depend on low-level modules. Both should
> depend on abstractions. Abstractions should not depend upon details. Details
> should depend upon abstractions" [Martin]

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
    private ICollection<IMessage> _messages;
    public Notification(ICollection<IMessage> messages)
    {
        this._messages = messages;
    }
    public void Send()
    {
        foreach(var message in _messages)
        {
            message.SendMessage();
        }
    }
}
```

Beispiel aus https://exceptionnotfound.net/simply-solid-the-dependency-inversion-principle/


## Herausforderungen bei der Umsetzung der Prinzipien

                                       {{0}}
****************************************************************************

Kunde TU Freiberg: *Entwickeln Sie für mich ein webbasiertes System, mit dem Sie die Anmeldung und Bewertung von Prüfungsleistung erfassen.*

Welche Fragen sollten Sie dem Kunden stellen, bevor Sie sich daran machen und munter Code schreiben?

****************************************************************************
                                        {{1}}
****************************************************************************
![OOPGeschichte](/img/13_UML/cartoon-projekte.png)<!-- width="80%" --> [^1] Possel


[^1]Heiko Possel, https://www.programmwechsel.de/lustig/management/schaukel-baum.html

Wie verzahnen wir den Entwicklungsprozess? Wie können wir sicherstellen,
dass am Ende die erwartete Anwendung realisiert wird?

****************************************************************************

                                         {{2}}
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

> Das V-Modell ist ein Vorgehensmodell, das ähnlich dem Wasserfallmodell, den Softwareentwicklungsprozess in Phasen  organisiert. Zusätzlich zu den Entwicklungsphasen definiert das V-Modell auch das Evaluationsphasen, in welchen den einzelnen Entwicklungsphasen Testphasen gegenüber gestellt werden.

vgl. zum Beispiel [Link](https://www.johner-institut.de/blog/iec-62304-medizinische-software/v-modell/)

Achtung: Das V-Modell ist nur eine Variante eines Vorgehensmodells, moderne
Entwicklungen stellen eher agile Methoden in den Vordergrund.

vgl. zum Beispiel [Link](https://entwickler.de/online/agile/agile-methoden-einfuehrung-209035.html)

****************************************************************************

## Unified Modeling Language

> Die Unified Modeling Language, kurz UML, dient zur Modellierung, Dokumentation , Spezifikation und Visualisierung komplexer Softwaresysteme unabhängig von deren Fach- und Realsierungsgebiet. Sie liefert die Notationselemente gleichermaßen fpr statische und dynamische Modelle zur Analyse, Design und Architektur und unterstützt insbesondere objektorientierte Vorgehensweisen. [Jeckle]

UML ist heute die dominierende Sprache für die Softwaresystem-Modellierung. Der erste Kontakt zu UML besteht häufig darin, dass Diagramme in UML im Rahmen von Softwareprojekten zu erstellen, zu verstehen oder zu beurteilen sind:

+ Projektauftraggeber und Fachvertreter prüfen und bestätigen zum Beispiel Anforderungen an ein System, die Wirtschaftsanalytiker bzw. Business Analysten in Anwendungsfalldiagrammen in UML festgehalten haben;
+ Softwareentwickler realisieren Arbeitsabläufe, die Wirtschaftsanalytiker bzw. Business Analysten in Zusammenarbeit mit Fachvertretern in Aktivitätsdiagrammen beschrieben haben;
+ Systemingenieure implementieren, installieren und betreiben Softwaresysteme basierend auf einem Implementationsplan, der als Verteilungsdiagramm vorliegt.

UML dabei Bezeichner für die meisten bei einer Modellierung wichtigen Begriffe und legt mögliche Beziehungen zwischen diesen Begriffen fest. UML definiert weiter grafische Notationen für diese Begriffe und für Modelle statischer Strukturen und dynamischer Abläufe, die man mit diesen Begriffen formulieren kann.

> **Merke:**  Die grafische Notation ist jedoch nur ein Aspekt, der durch UML geregelt wird. UML legt in erster Linie fest, mit welchen Begriffen und welchen Beziehungen zwischen diesen Begriffen sogenannte Modelle spezifiziert werden.

Was ist UML nicht:

+ vollständige, eindeutige Abbildung aller Anwendungsfälle
+ keine Programmiersprache
+ keine rein formale Sprache
+ kein vollständiger Ersatz für textuelle Beschreibungen
+ keine Methode oder Vorgehensmodell [Jeckle]

### Geschichte

UML (aktuell UML 2.5) ist durch die Object Management Group (OMG) als auch die ISO (ISO/IEC 19505 für Version 2.4.1) genormt.

![OOPGeschichte](/img/13_UML/OO-historie.png)<!-- width="70%" --> [^2] WikiUMLHistory

[^2] https://commons.wikimedia.org/w/index.php?curid=7892951, Autor GuidoZockoll, Mitarbeiter der oose.de Dienstleistungen für Innovative Informatik GmbH -
derivative work: File:OO-historie.svg : AxelScheithauer, oose.de Dienstleistungen für Innovative Informatik GmbH -
derivative work: Chris828 (talk) - File:Objektorientieren methoden historie.png and File:OO-historie.svg, CC BY-SA 3.0

### UML Werkzeuge

* Tools zur Modellierung - Unterstützung des Erstellungsprozesses, Überwachung der Konformität zur graphischen Notation der UML

    *Herausforderungen:* Transformation und Datenaustausch zwischen unterschiedlichen Tools

* Quellcoderzeugung - Generierung von Sourcecode aus den Modellen

    *Herausforderungen:* Synchronisation der beiden Repräsentationen, Abbildung wiedersprüchlicher Aussagen aus verschiedenen Diagrammtypen

    (Beispiel mit Visual Studio folgt am Ende der Vorlesung.)

* Reverse Engineering / Dokumentation - UML-Werkzeug bilden Quelltext als Eingabe liest auf entsprechende UML-Diagramme und Modelldaten ab

    *Herausforderungen:* Abstraktionskonzept der Modelle führt zu verallgemeinernden Darstellungen, die ggf. Konzepte des Codes nicht reflektieren.

![OOPGeschichte](/img/13_UML/Doxygen.png)<!-- width="80%" --> [^2] WikiDoxygen

[^3]  https://commons.wikimedia.org/w/index.php?curid=24966914, Doxygen-Beispielwebseite, Autor Der Messer - Eigenes Werk, CC BY-SA 3.0


**Darstellung von UML im Rahmen dieser Vorlesung**

Die Vorlesungsunterlagen der Veranstaltung "Softwareentwicklung" setzen auf die domainspezifische Beschreibungssprache plantUML auf, die verschiedene Aspekte in einer

http://plantuml.com/de/

```ascii PlantUMLClasses
@startuml
class Car

Driver - Car : drives >
Car *- Wheel : have 4 >
Car -- Person : < owns

@enduml
```
@plantUML.eval


```ascii PlantUMLTimings
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
@plantUML.eval

### Diagramm-Typen

![OOPGeschichte](/img/13_UML/UML-Diagrammhierarchie.png)<!-- width="90%" --> [^5] WikiUMLDiagrammTypes

[^5] https://upload.wikimedia.org/wikipedia/commons/thumb/d/da/UML-Diagrammhierarchie.svg/1200px-UML-Diagrammhierarchie.svg.png, Autor "Stkl"- derivative work: File: UML-Diagrammhierarchie.png: Sae1962, CC BY-SA 4.0

| Diagrammtyp                  | Zentrale Frage                                                                                                           |
| ---------------------------- | ------------------------------------------------------------------------------------------------------------------------ |
| Klassendiagramm              | Welche Klassen bilden das Systemverhalten ab und in welcher Beziehung stehen diese?                                      |
| Paketdiagramm                | Wie kann ich mein Modell in Module strukturieren?                                                                        |
| Objektdiagramm               | Welche Instanzen bestehen zu einem bestimmten Zeitpunkt im System?                                                       |
| Kompositionsstrukturdiagramm | Welche Elemente sind Bestandteile einer Klasse, Komponente, eines Subsystems?                                            |
| Komponentendiagramm          | Wie lassen sich die Klassen zu wiederverwendbaren Komponenten zusammenfassen und wie werden deren Beziehungen definiert? |
| Verteilungsdiagramm          | Wie sieht das Einsatzumfeld des Systems aus?                                                                             |

| Diagrammtyp                    | Zentrale Frage                                                                                |
| ------------------------------ | --------------------------------------------------------------------------------------------- |
| Use-Case-Diagramm              | Was leistet mein System überhaupt? Welche Anwendungen müssen abgedeckt werden?                |
| Aktivitätsdiagramm             | Wie lassen sich die Stufen eines Prozesses beschreiben?                                       |
| Zustandsautomat                | Welche Abfolge von Zuständen wird für eine eine Sequenz von Einganginformationen realisiert   |
| Sequenzdiagramm                | Wer tauscht mit wem welche Informationen aus? Wie bedingen sich lokale Abläufe untereinander? |
| Kommunikationsdiagramm         | Wer tauscht mit wem welche Informationen aus?                                                 |
| Timing-Diagramm                | Wie hängen die Zustände verschiedener Akteure zeitlich voneinander ab?                        |
| Interaktionsübersichtsdiagramm | Wann läuft welche Interaktion ab?                                                             |

[Jeckle]

**Begrifflichkeiten**

Ein UML-Modell ergibt sich aus der Menge aller seiner Diagramme. Entsprechend
werden verschiedene Diagrammtypen genutzt um unterschiedliche Perspektiven auf
ein realweltliches Problem zu entwickeln.

![Modelle](http://www.plantuml.com/plantuml/png/JSj1oi9030NW_PmYpFA7ARG7-Ed2fLrwW3WJsq3IWIIzlwAYhXxlVRpP0oqEbIHq2uWEnkiMqDYe1lSzRTm8bFHAvgzIsQfGIbNG7V9bEPUbDnB9W0xFTVh54-DggFhbyNsU88yPIlb_v33yvO_EjBT3vGu0)<!-- width="40%" --> [ModelvsDiagramm.plantUML]

## Anhang

**Referenzen**

[Martin] Robert Cecil Martin "Clean Code: Refactoring, Patterns, Testen und Techniken für sauberen Code" mitp-Verlag, 2009, ISBN 978-0-13-235088-4.

[Jeckle]  Mario Jeckle, Christine Rupp, Jürgen Hahn, Barbara Zengler, Stefan Queins, UML 2 glasklar, Hanser Verlag, 2004

[StackOverFlow] http://oi43.tinypic.com/2s7dr8y.jpg, StackOverFlow Forum, Autor unbekannt

[WikiInheri]  Wikipedia, "Vererbung", Autor "cactus26", [Link](https://de.wikipedia.org/wiki/Vererbung_&28Programmierung&29\#Kovarianz_und_Kontravarianz)

[Ghosh] Debasish Ghosh "Functional and Reactive Domain Modeling". Manning, 2016,

[Just] Markus Just, IT Designers Gruppe, "Entwurfsprinzipien", Foliensatz Fachhochschule Esslingen [Link](http://www.it-designers-gruppe.de/fileadmin/Inhalte/Studentenportal/Die_SOLID-Prinzipien__Folien___1_.pdf)

[UncleBob] Robert C. Martin, Webseite "The principles of OOD", http://www.butunclebob.com/ArticleS.UncleBob.PrinciplesOfOod

[WikiSRP] Wikipedia, "Single-Responsibility-Prinzip", [Link](https://de.wikipedia.org/wiki/Single-Responsibility-Prinzip)

[Meyer] Bertrand Meyer, "Object Oriented Software Construction" Prentice Hall, 1988,

[Krämer] Andre Krämer, "SOLID - Die 5 Prinzipien für objektorientiertes Softwaredesign", [Link](https://www.informatik-aktuell.de/entwicklung/methoden/solid-die-5-prinzipien-fuer-objektorientiertes-softwaredesign.html)
