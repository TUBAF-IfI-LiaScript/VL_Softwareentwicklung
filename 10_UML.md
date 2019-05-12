<!--

author:   Sebastian Zug & André Dietrich
email:    zug@ovgu.de   & andre.dietrich@ovgu.de
version:  0.0.1
language: de
narrator: Deutsch Female

import: https://raw.githubusercontent.com/liaScript/rextester_template/master/README.md
import: https://raw.githubusercontent.com/liaScript/WebDev_template/master/README.md
import: https://raw.githubusercontent.com/liascript-templates/plantUML/master/README.md
-->

# Vorlesung Softwareentwicklung - 7 - UML und OOP

--------------------------------------------------------------------
Link auf die aktuelle Vorlesung im Versionsmanagementsystem GitHub

https://github.com/liaScript/CsharpCourse/blob/master/10_UML.md

Die interaktive Form ist unter diese Link zu finden ->
[LiaScript Vorlesung 10](https://liascript.github.io/course/?https://raw.githubusercontent.com/liaScript/CsharpCourse/master/10_UML.md#1)

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

*1. Welche Sichtbarkeitsattribute können struct-Memberfunktionen in ihrer Sichtbarkeit spezifizieren und was bedeuten sie?*

--------------------------------------------------------------------

*2. Hier stehen jetzt Ihre Fragen ...*

---------------------------------------------------------------------

## 1. Unified Modeling Language

Die Unified Modeling Language, kurz UML, ist eine grafische Modellierungssprache zur Spezifikation, Konstruktion und Dokumentation von Software-Teilen und anderen Systemen.
Im Sinne einer Sprache definiert UML dabei Bezeichner für die meisten bei einer Modellierung wichtigen Begriffe und legt mögliche Beziehungen zwischen diesen Begriffen fest. UML definiert weiter grafische Notationen für diese Begriffe und für Modelle statischer Strukturen und dynamischer Abläufe, die man mit diesen Begriffen formulieren kann.

UML ist heute die dominierende Sprache für die Softwaresystem-Modellierung. Der erste Kontakt zu UML besteht häufig darin, dass Diagramme in UML im Rahmen von Softwareprojekten zu erstellen, zu verstehen oder zu beurteilen sind:

    Projektauftraggeber und Fachvertreter prüfen und bestätigen zum Beispiel Anforderungen an ein System, die Wirtschaftsanalytiker bzw. Business Analysten in Anwendungsfalldiagrammen in UML festgehalten haben;
    Softwareentwickler realisieren Arbeitsabläufe, die Wirtschaftsanalytiker bzw. Business Analysten in Zusammenarbeit mit Fachvertretern in Aktivitätsdiagrammen beschrieben haben;
    Systemingenieure implementieren, installieren und betreiben Softwaresysteme basierend auf einem Implementationsplan, der als Verteilungsdiagramm vorliegt.


UML (aktuell UML 2.5) ist durch die Object Management Group (OMG) als auch die ISO (ISO/IEC 19505 für Version 2.4.1) genormt.

> Merke:  Die grafische Notation ist jedoch nur ein Aspekt, der durch UML geregelt wird. UML legt in erster Linie fest, mit welchen Begriffen und welchen Beziehungen zwischen diesen Begriffen sogenannte Modelle spezifiziert werden.

### Geschichte

![OOPGeschichte](/img/10_UML/OO-historie.png)<!-- width="70%" --> [WikiUMLHistory](#7)


### UML Werkzeuge

* Tools zur Modellierung - Unterstützung des Erstellungsprozesses, Überwachung der Konformität zur graphischen Notation der UML

    *Herausforderungen:* Transformation und Datenaustausch zwischen unterschiedlichen Tools

* Quellcoderzeugung - Generierung von Sourcecode aus den Modellen

    *Herausforderungen:* Synchronisation der beiden Repräsentationen, Abbildung wiedersprüchlicher Aussagen aus verschiedenen Diagrammtypen

    (Beispiel mit Visual Studio folgt am Ende der Vorlesung.)

* Reverse Engineering / Dokumentation - UML-Werkzeug bilden Quelltext als Eingabe liest auf entsprechende UML-Diagramme und Modelldaten ab

    *Herausforderungen:* Abstraktionskonzept der Modelle führt zu verallgemeinernden Darstellungen, die ggf. Konzepte des Codes nicht reflektieren.

![OOPGeschichte](/img/10_UML/Doxygen.png)<!-- width="80%" --> [WikiDoxygen](#7)


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


## 2. Arten von Diagrammen

![OOPGeschichte](/img/10_UML/UML-Diagrammhierarchie.png)<!-- width="90%" --> [WikiUMLHistory](#7)

Darstellung von Diagrammtypen

## 3. Klassendiagramme

> Ein Klassendiagramm ist eine grafischen Darstellung (Modellierung) von Klassen, Schnittstellen sowie deren Beziehungen.

**Beispiel**

Nehmen wir an, sie planen die Software für ein Online-Handel System. Es soll
sowohl verschieden Nutzertypen (*Customer* und *Administrator*) als auch die Objekt *ShoppingCart* und *Order* umfassen.

```ascii PlantUMLTimings
@startuml
skinparam classAttributeIconSize 0
abstract class User{
  -userId: string
  -password: string
  -email: string
  -loginStatus: string
  +verifyLogin():bool
  +login()
}

class Customer{
  -customerName: string
  +register()
  +updateProfile()
}

class Administrator{
  -adminName: string
  +updateCatlog(): bool
}

class ShoppingCart{
  -cartId: int
  +addCartItem()
  +updateQuantity()
  +checkOut()
}

class Order{
  -orderId: int
  -customerId: int
  -shippingId: int
  -dateCreated: date
  -dateShipped: date
  -status: string
  +updateQuantity()
  +checkOut()
}

class ShippingInfo{
  -shipingId: int
  -shipingType: string
  +updateShipingInfo()
}

User <|-- Customer
User <|-- Administrator
Customer "1" *-- "0..*" ShoppingCart
Customer "1" *-- "0..*" Order
Order "1" *-- "1" ShippingInfo

@enduml
```
@plantUML.eval

### Klasse

Klassen werden durch Rechtecke dargestellt, die entweder nur den Namen der
Klasse (fett gedruckt) tragen oder zusätzlich auch Attribute, Operationen und
Eigenschaften spezifiziert haben.  Oberhalb des Klassennamens können
Schlüsselwörter in Guillemets und unterhalb des Klassennamens in
geschweiften Klammern zusätzliche Eigenschaften (wie {abstrakt}) stehen.

Elemente der Darstellung :

| Eigenschaften | Bedeutung                                                                    |
| ------------- | ---------------------------------------------------------------------------- |
| Attribute     | beschreiben die Struktur der Objekte: Bestandteile und darin enthalten Daten |
| Operationen   | Beschreiben das Verhalten der Objekte (Methoden)                             |
| Zusicherungen | Bedingungen, Voraussetzungen und Regeln, die die Objekte erfüllen müssen     |
| Beziehungen   | Beziehungen einer Klasse zu anderen Klassen                                  |

Wenn die Klasse keine Eigenschaften oder Operationen besitzt, kann die unterste
horizontale Linie entfallen.

![OOPGeschichte](/img/10_UML/ClassTypes.png)<!-- width="70%" --> [WikiUMLHistory](#7)

## Blau

**Sichtbarkeitsattribute**

<!--
style="width: 100%; display: block; margin-left: auto; margin-right: auto;"
-->
````ascii

| Zugriffsmodifizierer | Innerhalb eines Assemblys       || Außerhalb eines Assemblys      |
|                      | Vererbung      | Instanzierung  || Vererbung     | Instanzierung  |                
| -------------------- | -------------- | -------------- || ------------- | -------------- |
| `public`             | ja             | ja             || ja            | ja             |
| `private`            | nein           | nein           || nein          | nein           |
| `protected`          | ja             | nein           || ja            | nein           |
| `internal`           | ja             | ja             || nein          | nein           |
| `ìnternal protected` | ja             | ja             || ja            | nein           |

````

**public, private**

Die Sichtbarkeitsattribute `public` und `private`

![OOPGeschichte](http://www.plantuml.com/plantuml/svg/NP71pjCm48JlVefHJgregtBjgHK8n04LwKTSA0V7sIHBN3jQRueeucruZF_M5yD92eXUPsRdiyRxf5PqE7kJEWwz5Nk6ypQbWwfmEIYzSp4yyPMmDPE1LrP-662Dr1DLErHnA3tD2kdJA2vcRKuii_K2pHzhYRydFyszygFOkcPtx4cindlz9shPUTgDmCzVff8qvCI54nsypsfQBCUEXsiKBmkiroYskyq6HSvbOj48SQsu3h13NHWJeX8qzeZr8px7Nzk4f5RPuLX-fKxHsH36KrHCE8Mtb5pxZKF5JKUib3tlIV36jdAbeQ5q2XacjsVNJMWAAoGcs984WLshk93SVeMA96p-Uyxqudcxt9hmGM9FejypTeK-ByTkd1FlITjOqGhPw8KlLWdfdsxCdaALl_Ot)<!-- width="70%" --> [PublicPrivate.plantUML]()


```ascii PlantUMLClasses

```
@plantUML.eval




**Beziehungen**

```
[Sichtbarkeit] [/] name [: Typ] [ Multiplizität ] [= Vorgabewert] [{eigenschaftswert*}]
```

**Beziehungen**




**Abgrenzung**

**Aktive Klassen**
Die UML unterscheidet zwischen aktiven und passiven Klassen. Während die Trigger
passiver Klassen von außen kommen, sind aktive Klassen dadurch gekennzeichnet,
dass Sie ein „Eigenleben führen“. Unmittelbar wenn eine Instanz einer aktiven Klasse angelegt wird, startet ein für die Klasse spezifiziertes Verhalten. Dieses Verhalten läuft weiter, bis es explizit gestoppt oder bis das Objekt zerstört wird.


**Objekte vs. Klassen**

![OOPGeschichte](/img/10_UML/ObjectVsClass.png)<!-- width="90%" --> [StackOverFlow](#7)





### Schnittstelle



### Darstellung und Bedeutung


**Unterschiede zur UML 1.4**

1. Ab der Version 2.0 der UML können Klassen innere Klassen besitzen.

2. Im Gegensatz zur UML 1.x gibt es in der UML2 kein Modellelement Attribut mehr. Attribute einer Klasse werden neu als Eigenschaften (engl. property) modelliert. Dieser Ansatz wurde eingeführt, damit das Metamodell der UML2 sowohl Attribute einer Klasse als auch Enden von Assoziationen einheitlich als Eigenschaften modellieren kann.

3. Neben Eigenschaften und Operationen kann eine Klasse in UML2 neu auch über Ports und Signalempfänger als Merkmal verfügen.



## 4. Beispiele der Woche ...



## Anhang

**Referenzen**

[WikiUMLHistory] https://commons.wikimedia.org/w/index.php?curid=7892951, Autor GuidoZockoll, Mitarbeiter der oose.de Dienstleistungen für Innovative Informatik GmbH -
derivative work: File:OO-historie.svg : AxelScheithauer, oose.de Dienstleistungen für Innovative Informatik GmbH -
derivative work: Chris828 (talk) - File:Objektorientieren methoden historie.png and File:OO-historie.svg, CC BY-SA 3.0

[WikiUMLDiagrammTypes] https://upload.wikimedia.org/wikipedia/commons/thumb/d/da/UML-Diagrammhierarchie.svg/1200px-UML-Diagrammhierarchie.svg.png, Autor "Stkl"- derivative work: File: UML-Diagrammhierarchie.png: Sae1962, CC BY-SA 4.0

[WikiDoxygen] https://commons.wikimedia.org/w/index.php?curid=24966914, Doxygen-Beispielwebseite, Autor Der Messer - Eigenes Werk, CC BY-SA 3.0

[StackOverFlow] http://oi43.tinypic.com/2s7dr8y.jpg, StackOverFlow Forum, Autor unbekannt


**Autoren**

Sebastian Zug, André Dietrich
