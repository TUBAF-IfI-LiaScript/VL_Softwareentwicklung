<!--

author:   Sebastian Zug, Galina Rudolf & André Dietrich
email:    sebastian.zug@informatik.tu-freiberg.de
version:  1.0.0
language: de
narrator: Deutsch Female

import: https://github.com/liascript/CodeRunner
        https://raw.githubusercontent.com/liascript-templates/plantUML/master/README.md
        https://github.com/LiaTemplates/Pyodide

icon: https://upload.wikimedia.org/wikipedia/commons/d/de/Logo_TU_Bergakademie_Freiberg.svg
-->

[![LiaScript](https://raw.githubusercontent.com/LiaScript/LiaScript/master/badges/course.svg)](https://liascript.github.io/course/?https://github.com/TUBAF-IfI-LiaScript/VL_Softwareentwicklung/blob/master/14_UML_ModellierungII.md)

# Modellierung von Software

| Parameter                | Kursinformationen                                                                                                                                                                          |
| ------------------------ | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ |
| **Veranstaltung:**       | `Vorlesung Softwareentwicklung`                                                                                                                                                            |
| **Semester**             | `Sommersemester 2021`                                                                                                                                                                      |
| **Hochschule:**          | `Technische Universität Freiberg`                                                                                                                                                          |
| **Inhalte:**             | `Ausgewählte UML Diagrammtypen`                                                                                                                                |
| **Link auf den GitHub:** | [https://github.com/TUBAF-IfI-LiaScript/VL_Softwareentwicklung/blob/master/14_UML_ModellierungII.md](https://github.com/TUBAF-IfI-LiaScript/VL_Softwareentwicklung/blob/master/14_UML_ModellierungII.md) |
| **Autoren**              | @author                                                                                                                                                                                    |

![](https://media.giphy.com/media/26tn33aiTi1jkl6H6/source.gif)

---------------------------------------------------------------------

## Neues aus Github

**Wann wird gearbeitet?**      

<!--
data-type="heatmap"
data-title="Commit Anzahl"
data-transpose="true"
data-ylabel = "Wochentage (0 = Montag, 7 = Sonntag)"
data-xlabel = "Stunden des Tages"
data-show
-->
| Stunde |   0 |   1 |   2 |   6 |   7 |   8 |   9 |  10 |  11 |  12 |  13 |  14 |  15 |  16 |  17 |  18 |  19 |  20 |  21 |  22 |  23 |
| ------:| ---:| ---:| ---:| ---:| ---:| ---:| ---:| ---:| ---:| ---:| ---:| ---:| ---:| ---:| ---:| ---:| ---:| ---:| ---:| ---:| ---:|
|      1 | nan | nan | nan | nan | nan | nan | nan |   2 | nan |   3 |   2 | nan | nan | nan |   1 | nan | nan | nan | nan | nan | nan |
|      2 | nan | nan | nan | nan |   2 | nan | nan | nan |   2 |   8 | nan |  10 |  24 |   3 | nan | nan | nan | nan | nan | nan | nan |
|      3 | nan | nan | nan | nan | nan | nan | nan |   1 |   4 |   6 |   7 |  25 |  22 | nan | nan | nan | nan | nan |   1 | nan |   2 |
|      4 |   2 | nan | nan |   2 |   2 | nan |   5 |   8 |   7 | nan | nan |  29 |  16 |  14 |  12 | nan |   2 |   1 | nan | nan | nan |
|      5 | nan | nan |   1 | nan | nan |   2 | nan |  12 |  10 |   3 |   1 |   2 |   2 | nan |   4 |   3 | nan | nan | nan | nan | nan |
|      6 | nan | nan | nan | nan | nan |   1 | nan | nan | nan |   1 |   2 |   4 |   7 |   7 |   2 | nan | nan |   4 |   1 | nan | nan |
|      7 | nan | nan | nan | nan | nan | nan | nan |   2 |   4 |   3 |   8 |   5 |   4 |   2 |   4 | nan | nan | nan | nan | nan | nan |


**Was ist ein guter Commit?**

Das folgende Diagramm zeigt die Commits pro Aufgabe

<!--
data-type="heatmap"
data-title="Commit Anzahl"
data-ylabel = "Team ID"
data-xlabel = "Task"
data-show
-->
| Task    |   0 |   1 |   2 |   3 |   4 |   5 |   6 |   7 |   8 |   9 |  10 |  11 |  12 |  13 |  14 |  15 |  16 |
| ---:| ---:| ---:| ---:| ---:| ---:| ---:| ---:| ---:| ---:| ---:| ---:| ---:| ---:| ---:| ---:| ---:| ---:|
|   3 |   7 |  15 |  12 |   8 |  18 |  10 |   5 |   8 |  45 |  21 |   5 |   8 |  18 |   8 |   6 |   6 |   3 |
|   4 | nan | nan | nan |  15 |  13 |  18 |   3 |   5 |   5 |   9 |   4 |   3 |  19 |   2 |   3 |  24 | nan |

... und wie viele unterschiedliche Dateien wurden dabei editiert?

<!--
data-type="heatmap"
data-title="Editierte Dateien"
data-ylabel = "Team ID"
data-xlabel = "Task"
data-show
-->
|   Task |   0 |   1 |   2 |   3 |   4 |   5 |   6 |   7 |   8 |   9 |   10 |   11 |   12 |   13 |   14 |   15 |   16 |
|-------:|----:|----:|----:|----:|----:|----:|----:|----:|----:|----:|-----:|-----:|-----:|-----:|-----:|-----:|-----:|
|      3 |   5 |   3 |   2 |  15 |   4 |   5 |   2 |   6 |   3 |  16 |    2 |    2 |    7 |    2 |    2 |    4 |    2 |
|      4 | nan | nan | nan |  26 |  11 |   7 |   5 |   6 |   8 |   6 |    5 |    7 |    7 |    1 |    2 |    6 |  nan |


## UML Diagrammtypen

![OOPGeschichte](./img/13_UML/UML-Diagrammhierarchie.png)<!-- width="90%" --> [^WikiUMLDiagrammTypes]

[^WikiUMLDiagrammTypes]: https://upload.wikimedia.org/wikipedia/commons/thumb/d/da/UML-Diagrammhierarchie.svg/1200px-UML-Diagrammhierarchie.svg.png, Autor "Stkl"- derivative work: File: UML-Diagrammhierarchie.png: Sae1962, CC BY-SA 4.0

Im folgenden werden wir uns aus den beiden Hauptkategorien jeweils folgende Diagrammtypen genauer anschauen:

+ Verhaltensdiagramme

  + Anwendungsfall Diagramm
  + Aktivitätsdiagramm
  + Sequenzdiagramm

+ Strukturdiagramm

  + Klassendiagramm
  + Objektdiagramm

### Anwendungsfall Diagramm

> Das Anwendungsfalldiagramm (Use-Case Diagramm) abstrahiert das erwartete
> Verhalten eines Systems und wird dafür eingesetzt, die Anforderungen an ein
> System zu spezifizieren.

Ein Anwendungsfalldiagramm stellt keine
Ablaufbeschreibung dar! Diese kann stattdessen mit einem Aktivitäts-, einem
Sequenz- oder einem Kollaborationsdiagramm (ab UML 2.x Kommunikationsdiagramm)
dargestellt werden.

**Basiskonzepte**

Elemente:

+ Systemgrenzen werden durch Rechtecke gekennzeichnet.
+ Akteure werden als „Strichmännchen“ dargestellt, dies können sowohl Personen (Kunden, Administratoren) als auch technische Systeme sein (manchmal auch ein Bandsymbol verwendet). Sie ordnen den Symbolen Rollen zu
+ Anwendungsfälle werden in Ellipsen dargestellt. Üblich ist die Kombination aus Verb und ein Substantiv `Kundendaten Ändern`.
+ Beziehungen zwischen Akteuren und Anwendungsfällen müssen durch Linien gekennzeichnet werden. Man unterscheidet "Association", "Include", "Extend" und "Generalization".

![Modelle](https://www.plantuml.com/plantuml/png/RL11Rkf03DtFAInMi63n-HUWeAverGLIkpQ9IKPndCZZgGHLRzDZTCV5EY4f6gIk_6I_Pp-_TJ1KYoqxfgE1TQ2-gWrAhrIOxyI5nakFYYtqM3HOqTvEJ32CKIecXuLr2hievI_Urrt_Z9AuEdMU1ko3kPiCNeIzK4XK-700ymSrtn33WO8HCya2C40CjCL0_nmaoYQDK4eeenPrY4LzJrev66t0SlbdRtv5KgAHmEKhOPL5JiYkpTzGIP9LEdG6_P6f6gubKlxTP8gOerHmZYsyaeR1utkd1rBoDgbk2NowB8JP1gK9fs3Kml7ohV2uNUvGasWsFBQ_JbPkghd5VCaO9MpeZ3MFspBvVpVLExbRavInvHy0 "Einfaches Anwendungsfalldiagramm für einen Online-Versand")

**Verfeinerung**

Use-Case Diagramme erlauben die Abstraktion von Elementen auf der Basis von Generalisierungen. So können Akteure von einander erben und redundante Beschreibungen von Verhalten über `<<extend>>` oder `<<include>>` (unter bestimmten Bedingungen) erweitert werden.


|                | <img src="https://raw.githubusercontent.com/TUBAF-IfI-LiaScript/VL_Softwareentwicklung/b4a1739500b5a58e729c148a070343fce8faaf45/img/10_UML/UseCaseInclude.png" height="42">                           | <img src="https://raw.githubusercontent.com/TUBAF-IfI-LiaScript/VL_Softwareentwicklung/b4a1739500b5a58e729c148a070343fce8faaf45/img/10_UML/UseCaseExtend.png" height="42">         |
| -------------- | ------------------------------------------------ | ------------------------------------------------ |
|                | `<<include>>` Beziehung                          | `<<extend>>` Beziehung                           |
| Bedeutung      | Ablauf von A schließt den Ablauf von B immer ein | Ablauf von A kann optional um B erweitert werden |
| Anwendung      | Hierachische Zerlegung                           | Abbildung von Sonderfällen                       |
| Abhängigkeiten | A muss B bei der Modellierung berücksichtigen    | Unabhängige Modellierung möglich                 |



![PartyUCD](https://www.plantuml.com/plantuml/png/bLAnQiCm4Dtr5OUdPaWW6OinWT9cA8GEIQSkidorXTLJa4vG4l8t-OPEsVnZbPrGkqqBwVQiU-_TtJlFFKl7uSq8gTWwM4ZF0rXYZkxy_MIDtg8Mmg5YIQL1K1MgsWQ6dGWUbVG8wxifI0-9M22xva0rM5UW8p4U2tnd9AyjmSPgOlz2i9IgygeAEQoSfXffaeY1r-paTUyzqhL6aNooQHU0SGOtAqbRf57_i2P6Lqd3mzSu-G3FUlQYENX6g3J9u7ECY-jEABuIwcZz1AT1Hp_g3wWZak7LD1--qvyJ7LPp5pOCEI8L7UL21PF9toF1BQIf9cL2WLacVZVzZuCW6qb9cZvRbsEXgGnKPXd4m7UFpR4oSM4dgOOIDwOvHd_7-wGzsd8gBXE9hTl2SXf15tZRhJjc4PjT8PXz4-p5F8Buytu2 "Party als Anwendungsfalldiagramm - Beispiel motiviert aus dem Beispiel von [^Jeckle] Seite 240")


**Anwendungsfälle**

+ Darstellung der wichtigsten Systemfunktionen
+ Austausch mit dem Anwender und dem Management auf der Basis logischer, handhabbarer Teile
+ Dokumentation des Systemüberblicks und der Außenschnittstellen
+ Indentifikation von Anwendungsfällen

**Vermeiden Sie ...**

+ ... eine zu detaillierte Beschreibung von Operationen und Funktionen
+ ... nicht funktionale Anforderungen mit einem Use-Case abbilden zu wollen
+ ... Use-Case Analysen aus Entwicklersicht durchzuführen
+ ... zu viele Use-Cases in einem Diagramm abzubilden (max. 10)

[^Jeckle]: Mario Jeckle, Christine Rupp, Jürgen Hahn, Barbara Zengler, Stefan Queins, UML 2 glasklar, Hanser Verlag, 2004

### Aktivitätsdiagramm

> Aktivitätsdiagramme stellen die Vernetzung von elementaren Aktionen und deren
> Verbindungen mit Kontroll- und Datenflüssen grafisch dar.

**Aktivitätsmodellierung in UML1**

| <img src="https://www.plantuml.com/plantuml/svg/JOynRW9134LxdyAY8bTOYX3gz3Gq1lxkM3Fn8i-CLk8iANCJBXOJjaXdVV_tfB-lJRprhqBqTn4vRf16pCE7DyqeNFibmNR_8pM-mlXaHqaEoxEVkM2ArihpahI0jqTeWuDNyFsDdew0dG-uIohTffFnylX9vKcFisSQ7jzd-0AjyNrbB9EeqN0Gor2xzyXXLtxrDv-A4IvMBybrR66KNbVfPXVJvXlHFe1O-Wi0"> | <img src="https://www.plantuml.com/plantuml/png/JOz1JiD034NtFeKbDic60oIBDX8tgA3hQVlRCPgwmUE0LCrTs706Bf2BCQe8PUdlxoU_TVPWFfqJbUSCAtIRoJ0YE7MRVJJ83a_1eJt9aPlD2Db76BzVKbgrx17AZKAq9Uw6AP_23hoqJZP_pv_e2Ic3czTGIrmU1dLvcx2DuYZ3uInQQjwz_1tO7T4JtxdNAJ_-sq0FSbnUxvPS-ry1_eYImCqMuZ3mJIFFNtx5ggsgbv7M5L7rVm00">|
|Aktivitätsdiagramme.plantUML | ActivityUser.plantUML |

Bis UML 1.x waren Aktivitätsdiagramme eine Mischung aus Zustandsdiagramm,
Petrinetz und Ereignisdiagramm, was zu theoretischen und praktischen Problemen
führte.

**Erweiterung des Konzeptes in UML2**

> "Was früher Aktivitäten waren sind heute Aktionen."

UML2 strukturiert das Konzept der Aktivitätsmodellierung neu und führt als übergeordnete
Gliederungsebene Aktivitäten ein, die Aktionen, Objektknoten sowie Kontrollelemente der
Ablaufsteuerung und verbindende Kanten umfasst. Die Grundidee ist dabei, dass neben dem
Kontrollfluss auch der Objektfluss modelliert wird.

+ Aktivitäten definieren Strukturierungselemente für Aktionen, die durch Ein- und Ausgangsparameter, Bedingungen, zugehörige Aktionen und Objekte sowie einen
Bezeichner gekennzeichnet sind.

<!--
style="width: 80%; max-width: 860px; display: block; margin-left: auto; margin-right: auto;"
-->
````ascii

     .---------------------------------------------------.
     | Flächenberechnung Rechteck     ≪ precondition ≫   |
     |                                   Höhe ≥ 0        |
 +---+----+                              Breite ≥ 0      |
 | Höhe   |----˃                                         |
 +---+----+                                          +---+----+
     |                                          ----˃| Fläche |
 +---+----+                                          +---+----+
 | Breite |----˃                                         |
 +---+----+                                              |
     |                                                   |
     .---------------------------------------------------.
````


+ Aktionen stehen für den Aufruf eines Verhaltens oder die Bearbeitung von Daten, die innerhalb einer Aktivität nicht weiter zerlegt wird.

<!--
style="width: 80%; max-width: 860px; display: block; margin-left: auto; margin-right: auto;"
-->
````ascii

 +--------------------------+
 |   ≪ localPrecondition ≫  |\
 |                          +-+
 |  Papier vorhanden          |
 +----------------------------+
               \
                \
     .-----------------------.         .-------------------------.
     | Einladung verschicken |  ----˃  | Getränke einkaufen      |
     .-----------------------.         .-------------------------.
                  \
                   \
         +----------------------------+
         | ≪ localPostcondition ≫     |\
         |                            +-+
         |  Hälfte der Gäste angenommen |
         +------------------------------+

````

+ Objekte repräsentieren Daten und Werte, die innerhalb der Aktivität manipuliert werden. Damit ergibt sich ein nebeneinander von Kontroll- und Objektfluss.

<!--
style="width: 80%; max-width: 860px; display: block; margin-left: auto; margin-right: auto;"
-->
````ascii
                      Kontrollfluss
     .-------------.                    .-------------.
     | Aktion      |------------------> | Aktion      |
     .-------------.                    .-------------.

 ---------------------------------------------------------------

             Ausgabe-                 Eingabe-
              pin                      pin
                       Objektfluss
    .-------------._                   _.-------------.
    | Aktion      |_|----------------˃|_| Aktion      |
    .-------------.                     .-------------.


    .-------------.   +-------------+     .-------------.
    | Aktion      |---| Objekt      |---˃ | Aktion      |
    .-------------.   +-------------+     .-------------.
````


+ Signale und Ereignisse sind die Schnittstellen für das Auslösen einer Aktion

<!--
style="width: 90%; max-width: 860px; display: block; margin-left: auto; margin-right: auto;"
-->
````ascii

 +--------------.       .----------------+       .------------------------.
 |    Sende      \       \  Erster Gast  |------˃| Vorbereitungen beenden |
 |    Signal     /       /  eingetroffen |       .------------------------.
 +--------------.       .----------------+
````

**Beispiel**

![Aktivitätsdiagramme](./img/14_UML_II/ActivityDiagram.png "Beispiel eines Anwendungsfall Diagramms [^WikiActivityDiagram]")

Beispiels auf Anwendungsfall [Link](https://www.youtube.com/watch?v=VaKCZOhVJkQ)

**Anwendungsfälle**

+ Verfeinerung von Anwendungsfällen (aus den Use Case Diagrammen)
+ Darstellung von Abläufen mit fachlichen Ausführungsbedingungen
+ Darstellung für Aktionen im Fehlerfall oder Ausnahmesituationen

[^WikiActivityDiagram]: Wikimedia, Autor Gubaer, UML2 Aktivitätsdiagramm,  https://commons.wikimedia.org/wiki/File:Uml-Activity-Beispiel2.svg

### Sequenzdiagramm

> Sequenzdiagramme beschreiben den Austausch von Nachrichten zwischen Objekten mittels Lebenslinien.

Ein Sequenzdiagramms besteht aus einem Kopf- und einem Inhaltsbereich. Von jedem Kommunikationspartner geht eine Lebenslinie (gestrichelt) aus. Es sind zwei synchrone Operationsaufrufe, erkennbar an den Pfeilen mit ausgefüllter Pfeilspitze, dargestellt. Notationsvarianten für synchrone und asynchrone Nachrichten

Eine Nachricht wird in einem Sequenzdiagramm durch einen Pfeil dargestellt, wobei der Name der Nachricht über den Pfeil geschrieben wird. Nachrichten können:

+ Operationsaufrufe einer Klasse sein
+ Ergebnisse einer Operation
+ Signale
+ Interaktionen mit dem Nutzern
+ das Setzen einer Variablen

Synchrone Nachrichten werden mit einer gefüllten Pfeilspitze, asynchrone Nachrichten mit einer offenen Pfeilspitze gezeichnet.

Die schmalen Rechtecke, die auf den Lebenslinien liegen, sind Aktivierungsbalken, die den Focus of Control anzeigen, also jenen Bereich, in dem ein Objekt über den Kontrollfluss verfügt, und aktiv an Interaktionen beteiligt ist.


**Beispiel**

![Aktivitätsdiagramme](./img/14_UML_II/SequenzCheckEmail.png "Sequenzdiagramm der Interaktion zwischen Emailclient und Server [^WikiSequenceDiagram]")

**Bestandteile**

| Name                   | Beschreibung                                                                                                          |
| ---------------------- | --------------------------------------------------------------------------------------------------------------------- |
| Objekt                 | Dient zur Darstellung einer Klasse oder eines Objekts im Kopfbereich.                                                 |
| Nachrichtensequenzen   | Modelliert den Informationsfluss zwischen den Objekten                                                                |
| Aktivitätsbalken       | Repräsentiert die Zeit, die ein Objekt zum Abschließen einer Aufgabe benötigt.                                        |
| Paket                  | Strukturiert das Sequenzdiagramm                                                                                      |
| Lebenslinien-Symbol    | Stellt durch die Ausdehnung nach unten den Zeitverlauf dar.                                                           |
| Fragmente              | Kapseln Sequenzen in „Wenn-dann“-Szenarien, optionalen Interaktionen, Schleifen, etc.                                 |
| Alternativen-Symbol    | Stellt eine Auswahl zwischen zwei oder mehr Nachrichtensequenzen (die sich in der Regel gegenseitig ausschließen) dar |
| Interaktionsreferenzen | Binden Submodelle und deren Ergebnisse ein  deren                                                                     |

**Beispiel**

| <img src="https://www.plantuml.com/plantuml/png/ZPCn3jim34LtdUBt00NQ8Ok15idGR0K2z0IApTX2PCgGw0w6ZzQj5wiYrqOP1sYM26B8pwTF-fZ4HXbxRuPykjeDbWLdsqMAAdG9taqkYGgUxg4BNYZ9K_Pfb63C8eEFNxVg2Z_gpbjkYRrZHxWzKjZOMjnFAxHstkvRMpFFdm_pRDP0LlsRNC5oPxtiPJO0HR1cL-5k6jftQkXPPXzvFDO0hs0k55DWwnqnXlZxrutO0FismBCeiBmH1lWlEV2A7TYpaa-aMSjuglB_H6CxZKaiD-_IbjcY79fl4to7RnKYReonNfHl7xuWt14QYflc4dljULPAGgubXhSfvMFa1hqJfgudrWxeodEqBO5jtU7aopOlH64Gw1Arx5M-vLSuYielbRFCyrFUMQZul-s_KqeZjoXVePOgAKfatOaTowfofSnEektNKeSzZXhXjzZIbLs2vQaugfHKOJ7gc7_6tly1"> |
|Alkoholkontrolle.plantUML |

[^WikiSequenceDiagram]: Wikimedia, Autor Coupling_loss_graph.svg, UML of message sequence, https://commons.wikimedia.org/wiki/File:CheckEmail.svg

### Klassendiagramme

> Ein Klassendiagramm ist eine grafischen Darstellung (Modellierung) von Klassen, Schnittstellen sowie deren Beziehungen.

**Beispiel**

Nehmen wir an, sie planen die Software für ein Online-Handel System. Es soll
sowohl verschieden Nutzertypen (*Customer* und *Administrator*) als auch die Objekt *ShoppingCart* und *Order* umfassen.

```text @plantUML.png
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

#### Klassen

                        {{0-2}}
********************************************************************************

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

![OOPGeschichte](./img/14_UML_II/ClassTypes.png "UML Klassendiagramme und deren Attribute - adaptiert aus [^WikiUMLClass]")


********************************************************************************

                                  {{1-2}}
********************************************************************************

**Objekte vs. Klassen**

| Klassendiagramm                                                                                                                                                                                                                                                                                                                                   | Beispielhaftes Objektdiagramm                                                                          |
| ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- | ------------------------------------------------------------------------------------------ |
| <img src="https://www.plantuml.com/plantuml/png/TP2_JWCn3CRtF8Lr4r1LWXKn5C7I1OPxWjGnhKNYd1o7WJ_lJhXhInYOelm-Vpz7sHJpQZL5vyeho-JL9y3eIxatKzvLeorc6VcTuDeTE10WAqPoRfw2Xrei9rBuS01h49y8Rc5iNTdF96Rqnbbwke924fura5vSpdSb97wZ4_ewwHiukuEt7_TZEvPTDUr86zcAqPvq5c6M5zPqr6BCXNhVKzRauwzr1O7Bv0qFVn-oT8wbSIBaVrjjRadkFjVhVa6FUuKx_zbmCq0BXwi1bcH34cgAtm00" width="228"> | <img src="https://www.plantuml.com/plantuml/png/TSwnJiCm4CRntKznojvWEss7gaIL8h4HaF84RdmK8pj6b_DKyMPil1WnY10ARDRdltwwmyY6E0KlF3umS0HofqS6wmXxTd4G8XwVfYSK-rGtQUGOa7PmZuoqlqfr0MQ4vYm64n2kFtUuVNlr7adus9kCJ1ytXEw8T18lFyGsKZ3-Zcn6LQNqUaPRTHRMS5OBfKkvARNKiX132XCqhHzvzlu7rzKIrD-Wbd-1ERyui7l6tisWtcotizSj3wnAMLrwkSvS0wjL3Qs27SJieFsQzA-fMrO776mA_Xq0" width="300">  |

Darstellung motiviert nach _What is Object Diagram?_, https://www.visual-paradigm.com/guide/uml-unified-modeling-language/what-is-object-diagram/, Autor unbekannt


********************************************************************************

                                  {{2}}
********************************************************************************

> **Merke:** Vermeiden Sie bei der Benennung von Klassen, Attributen, Operationen usw. sprachspezifische Zeichen

```python KlasseMitUmlaut.py
class Zähler:
    """A simple example class"""
    i = 12345

A = Zähler()
print(A.i)
```
@LIA.eval(`["main.py"]`, `python -m compileall .`, `python main.pyc`)


```c KlasseMitUmlaut.cpp
#include <iostream>

class Zähler{
  public:
   int i = 12345;
};   

int main()
{
  Zähler A = Zähler();
  std::cout << A.i;
  return 0;
}
```
@LIA.eval(`["main.cpp"]`, `g++ main.cpp -o a.out`, `./a.out`)


********************************************************************************

                                  {{3}}
********************************************************************************

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

Die Sichtbarkeitsattribute `public` und `private` sind unabhängig vom Vererbungs-, Instanzierungs- oder Paketstatus einer Klasse. Im Beispiel kannn der TrafficOperator nicht
auf die Geschwindigkeiten der Instanzen von Car zurückgreifen.

![PublicPrivate](https://www.plantuml.com/plantuml/png/NP51pjCm48NtFiLJLd-XhNwsMRK2KR6eYbJ4Xh9mx4aonBKZUr944RVX6EnwCPoaWEZspJVVFCkR93hAS7OgTUnx7VKPnkcKjYAHgq7edGd-o5V2ishP4Wn7uqXD45xh-5q1AwIUo9QmOQWSpGd9SGoVvcmc5ddq4hi_emN-hlnGT-M7gEkQpQ6dg2NSzH-fvNmrbUF5Jvydopvupi3MkPobfyMeU5X6yQh0YzKAnNEvNgF884eKJIIk1TS1UgWBfNmGWeGUeSQCJ6MXNSBx6B9ClYrFliz5sQ0XmPwa3PQMo6daYnP2-XV_9toiLEADJZXKNwWJTDhxiGGiKtRjA9dsAtlBJKSib6VEKSHRtSOh3GsTV6uTRxzCDxs1_1A92RhAa6VJ2YuKRp-zfUZNQaFUvg_z3G00 "Private member [PublicPrivate.plantUML](https://github.com/liaScript/CsharpCourse/blob/master/code/10_UML/Graphs/publicPrivate.plantUML)")

**protected**

Die abgeleitete Klassen Bus und PassagerCar erben von Car und übernehmen damit deren Methoden. Die Zahl
der Sitze wird beispielsweise mit ihrem Initialisierungswert von 5 auf 40 gesetzt.
Zudem muss die Methode `StopAtStation` auch auf die Geschwindigkeit zurückgreifen können.

![Protected](https://www.plantuml.com/plantuml/png/XKy_Jm8n5D_lK_nI5z10CD7beWd8v8J94HVZqEkzWuPUUsbV7L2_kuKgXXjqQPhV_pSFB3t7pXHXfsqllUoWZ0nXmUnr7PaUbRELtXDcnLOt1AMzCbJ8Eh2NYhCQI-dV2s22q1Cr9HeNQqD7nCQk9hzgAv8S4dA1G5mTPAEiofvnegwm-Q76E8Ly5aKkUKeFsGtvt3RCBg4junwtluw76FuGtioEdSijgTrQgnrn6EKP5Rj-mHLBriucx1Iuzh9jjLhrv2MxxnNFjDNAK8cqw-Nr6D0vgnE_C2PX8IUa7-u-9fF3AfGGv6l6cxFYn9nTU2uP54DS2rmcXvYLqod4qBZnf5l6FaB6zbzt7ZGqpSasiJDV "Protected Member[protected.plantUML](https://github.com/liaScript/CsharpCourse/blob/master/code/10_UML/Graphs/protected.plantUML)")

**internal**

Ein Member vom Typ `protected internal` einer Basisklasse kann von jedem Typ innerhalb seiner enthaltenden Assembly aus zugegriffen werden.

![Protected](https://www.plantuml.com/plantuml/png/JOwzRkf044RxVOfFha38YDjQ2463T2b3b2PACUoExH7hjRK_HKZ8iqTcRONspCmvim-HV4oTLU4gRY1F7RIb40unUhcaoFzrxyvoOsnKAuQXnMlBQY1zfORnqfDvzLJNeh7T7djhsEzs-5B87hnnAzho280IOM0s5KoVBfOVx0U1utd4Yqpppl8nNA6PXykdgxkwAvNzf-HqbDwDWHAQVAWmFLHLKpoyVwZHTPOkMOfyecwn_5CSn0-M76T5ozB45a-r5OxsCn_CDHJ_2fGbYasrNgEOEgXGD9wbZhWbKAeBDRjHVkymhQazEvCw-mi0 "Internal Member [internal.plantUML](https://github.com/liaScript/CsharpCourse/blob/master/code/10_UML/Graphs/internal.plantUML)")

> **Merke: ** Der UML Standard kennt nur `+ public`, `- private`, `# protected` und `~ internal`. Das C# spezifische `internal protected` ist als weitere Differenzierungsmöglichlichkeit nicht vorgesehen.

********************************************************************************

                                  {{4}}
********************************************************************************

**Attribute**

> **Merke:** In der C# Welt sprechen wir bei Attributen von Membervariablen und Feldern.

Im einfachsten Fall wird ein Attribut durch
einen Namen repräsentiert, der in der Klasse eindeutig sein muss - Die Klasse
bildet damit den Namensraum der enthaltenen Attribute.

Entsprechend der Spezifikation sind folgende Elemente eines Attributes
definierbar:

```
[Sichtbarkeit] [/] Name [: Typ] [ Multiplizität ] [= Vorgabewert] [{Eigenschaftswert}]
```

+ *Sichtbarkeit* ... vgl. vorheriger Absatz
    Das "/" bedeutet, dass es sich um ein abgeleitetes Attribut handelt, dessen
    Daten von anderen Attributen abhängt
+ *Name* ... des Attributes, Leer und Sonderzeichen sollten weggelassen werden, um zu vermeiden, dass Sie bei der Implementierung Probleme generieren.

+ *Typ* ... UML verwendet zwar einige vordefinierte Typen (Integer, String, Boolean) beinhaltet aber keine Einschränkungen zu deren Wertebereich!

+ *Multiplizität* ... die Zahlenwerte in der rechteckigen Klammer legen eine Ober- und Untergrenze der Anzahl (Kardinalitäten) von Instanzen eines Datentyps fest.

| Beispiel | Bedeutung                                                                |
| -------- | ------------------------------------------------------------------------ |
| `0..1`   | optionales Attribut, das aber höchstens in einer Instanz zugeordnet wird |
| `1..1`   | zwingendes Attribut                                                      |
| `0..n`   | optionales Attribute mit beliebiger Anzahl                               |
| `1..*`   | zwingend mit beliebiger Anzahl größer Null                               |
| `n..m`   | allgemein beschränkte Anzahl größer 0                                    |

+ *Vorgabewerte* ... definieren die automatische Festlegung des Attributes auf einen bestimmten Wert

+ *Eigenschaftswerte* ... bestimmen die besondere Charakteristik des Attributes

| Eigenschaft | Bedeutung                                                                     |
| ----------- | ----------------------------------------------------------------------------- |
| `readOnly`  | unveränderlicher Wert                                                         |
| `subsets`   | definiert die zugelassen Belegung als Untermenge eines anderen Attributs      |
| `redefines` | überschreiben eines ererbten Attributes                                       |
| `ordered`   | Inhaltes eines Attributes treten in geordneter Reihenfolge ohne Dublikate auf |
| `bag`       | Attribute dürfen ungeordnet und mit Dublikaten versehen enthalten sein        |
| `sequence`  | legt fest, dass der Inhalt sortiert, aber ohne Dublikate ist                  |
| `composite` |                                                                               |

Daraus ergeben sich UML-korrekte Darstellungen

| Attributdeklaration              | Korrekt | Bemerkung                                                                          |
| -------------------------------- | ------- | ---------------------------------------------------------------------------------- |
| `public zähler:int `             | ja      | Umlaute sind nicht verboten                                                        |
| `/ alter`                        | ja      | Datentypen müssen nicht zwingend angegeben werden                                  |
| `privat adressen: String [1..*]` | ja      | Menge der Zeichenketten                                                            |
| `protected bruder Person`        | ja      | Datentyp kann neben den Basistypen jede andere Klasse oder eine Schnittstelle sein |
| String                           | nein    | Name des Attributes fehlt                                                          |
| privat, public name: String      | nein    | Fehler wegen mehrfachen Zugriffsattributen                                         |

![Protected](https://www.plantuml.com/plantuml/png/LP1FImCn4CNl-HIFUXHhrzO_18HQyE2v7WJna4q66yncbcGMr6Nzx4Pj3RqDFtxlmRnr5fDaw8BARpxs9faEDfYSxqIIRmUX1yjnwpy92xNpZc1zie7KhuJxTzFrWKO5c4EWqV1H2ZcjxEB0nYn6l9tG3stm5XgEoqMunNBUh8fnLbDBZPOva8c5drI-qiWxJeA2log-rDYMCxwMijaZPPbXJ6GSn_0nALJnIcFpl9ZFcvVHSjTpza97mlL_qrxZv2YH-v7EgtQZ9hKVOdCqmTFVkvDQKtHbt6y0 "Korrespondierendes UML-Klassendiagramm [AttributeExample.plantUML](https://github.com/liaScript/CsharpCourse/blob/master/code/10_UML/Graphs/AttributeExample.plantUML)")

```csharp    AttributeExample
using System;

namespace Rextester
{
  class Example
  {
    int attribute1;
    public int attribute2;
    public static double pi = 3.14;
    private bool attribute3;
    protected short attribute4;
    internal const string attribute5 = "Test";
    B attribute6;
    System.Collections.Speciallized.StringCollection attribute7;
    private int wert;
    Object attribute8{
      get{return wert * 10;}
    }
  }
}
```

********************************************************************************

                                  {{5}}
********************************************************************************

**Operationen**

> **Merke:** In der C# Welt sprechen wir bei Operationen von Methoden.

Operationen werden durch  mindestens ihren Nahmen sowie wahlfrei weitere Angaben
definiert. Folgende Aspekte können entsprechend der UML Spezifikation beschrieben werden:

```
[Sichtbarkeit] Name (Parameterliste) [: Rückgabetyp] [{Eigenschaftswert}]
```

Dabei ist die Parameterliste durch folgende Elemente definiert:

```
[Übergaberichtung] Name [: Typ] [Multiplizität] [= Vorgabewert][{Eigenschaftswert}]
```

+ *Sichbarkeit* ... analog Attribute
+ *Name* ... analog Attribute
+ *Parameterliste* ... Aufzählung der durch die aufrufende Methode übergebenden Parameter, die im folgenden nicht benannten Elemente folgend den Vorgaben, die bereits für die Attribute erfasst wurden:

    + *Übergaberichtung* ... Spezifiziert die Form des Zugriffes (`in` = nur lesender Zugriff, `out` = nur schreibend (reiner Rückgabewert), `inout` = lesender und schreibender Zugriff)

  + *Vorgabewert* ... default-Wert einer Übergabevariablen

+ *Rückgabetyp* ... Datentyp oder Klasse, der nach der Operationsausführung zurückgegeben wird.

+ *Eigenschaftswert* ... Angaben zu besonderen Charakteristika der Operation


![Protected](https://www.plantuml.com/plantuml/png/HO_1IiH044JlynK5NhAAYzdLIq1O5G-UzoWUnfb66oSzeTC1DSHlTpQ1pQtegbT57thpQaERNF_5qddrBKBoVVzWflm-6Bs4B4V-9TouJuw4m1eIkJc4vs_VTeb61-0AOoap3XDoHrfKbhhQdAphzRAhM33lr0rO3FUusongLF8nI_gPJip5okmBamTRi1qQFCsjVslYjZhx-mP7KvOFlDRRxUKRngoHbE9KPXxTv0uaiJpy3m00 "Korrespondierendes UML-Klassendiagramm [OperationsExample.plantUML](https://github.com/liaScript/CsharpCourse/blob/master/code/10_UML/Graphs/OperationsExample.plantUML)")

```csharp    OperationsExample
using System;

class Example
{
  public static void operation1(){
    // Implementierung
  }

  private int operation2 (int param1 = 5)
  {
    // Implementierung
    return value;
  }

  protected void operation3 (ref C param3)
  {
    // Implementierung
    param3 = ...
  }

  internal B operation4 (out StringCollection param3)
  {
    // Implementierung
    return value;
  }
}
```

********************************************************************************

[^WikiUMLClass]: https://de.wikipedia.org/wiki/Klassendiagramm#/media/Datei:UmlCd_Klasse-3.svg, Autor Gubaer, CC BY-SA 3.0

#### Schnittstellen

Eine Schnittstelle wird ähnlich wie eine Klasse mit einem Rechteck dargestellt, zur Unterscheidung aber mit dem Schlüsselwort `interface` gekennzeichnet.

![Protected](https://www.plantuml.com/plantuml/png/NP1D3e9038NtFKMNke0J48BHZOc91p0kIgbnp30aCtNXZtSN5K9nsUzxRvffbPIYNbiFPzS8ieli1O7gf95OaJsbXAjXtBcaanlfklDUM5qNm0MLU28M_4InABOZA4iZfyPV17wrPvxFTzg2aNOrJCnaisp-a1q67IFTlWw6puu07u2uho_2UZYYU6abw8QKUfpSNHPBU44beUdFzmO0 "Darstellung von Schnittstellen in UML-Klassendiagrammen [PublicPrivate.plantUML](https://github.com/liaScript/CsharpCourse/blob/master/code/10_UML/Graphs/OperationsExample.plantUML)")

Eine alternative Darstellung erfolgt in der LolliPop Notation, die die grafische Darstellung etwas entkoppelt.

![Loolipop](https://www.plantuml.com/plantuml/png/ZP0n3e9044NxESMKK7W22Q7GmaHZOMaibkK3gykIx0wDnhDu62_cOg8anC9fNzwRoHH1b9UXizIQ2goDrnPCnWbyhJJuq7iny8Aj2GBEiiq7vVcDE0wCgmSqS9oie-TLmqYNRsHx1DtEoPr8MnK2hvJ0bSfTU2pjopEq74yCYmvE8bN47CmLIJf9EpmVrIZ-9ytkJzB5jFON_EQ92hWgVkO5 "Steigerung der Lesbarkeit durch die Verwendung von Lollipop Symbolen [lollipop.plantUML](https://github.com/liaScript/CsharpCourse/blob/master/code/10_UML/Graphs/lollipop.plantUML)")


```csharp    OperationsExample
using System;

interface Sortierliste{
  void einfuegen (Eintrag e);
  void loeschen (Eintrag e);
}

class Datenbank : SortierteListe
{
  void einfuegen (Eintrag e) {//Implementierung};
  void loeschen (Eintrag e) {//Implementierung};
}
```

#### Beziehungen

Die Möglichkeiten der Verknüpfung zwischen Klassen und Interfaces lassen sich wie folgt gliedern:

| Beziehung                             | Darstellung                                                                                                                                          | Bedeutung                                                                                  |
| ------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------------------------- | ------------------------------------------------------------------------------------------ |
| Generalisierung                       | <img src="https://www.plantuml.com/plantuml/png/LOv12W8n40Jlyuh-m7vW3x9NRu8l2CcO7KmcC9diHVsxgoZehMcgw5ww1_DH2wsI654i5gR25uStcEJLaSGukZIbM4BlEB7rfZS2D5IUSNOJpGHipVwQnrSmsO9VXoP-MNWd7RMopJYgSsRROVhXC4ttmsU95jQqNtm2" width="228"> | gerichtete Beziehung zwischen einer generelleren und einer spezielleren Klasse (Vererbung) |
| Assoziationen (ohne Anpassung)        | <img src="https://www.plantuml.com/plantuml/png/NOun3i9030Hxls8_a0-aIa57oGkEEvDOScp9vz2W_9q0ee1oLpEhdNgMo6rLMgGngaHL9kLR8XqXyv9e4bnJt8YgbjvnTaj_461bvH6N3B7vYBreyn4dtwxAj7_fpHg_QDmz_WnOgSjFjnEzvp4C7vCcjdOSV080"width="228">          | beschreiben die Verknüpfung allgemein                                                      |
| Assoziation (Komposition/Aggregation) | <img src="https://upload.wikimedia.org/wikipedia/commons/thumb/f/f8/Komposition_Aggregation.svg/1920px-Komposition_Aggregation.svg.png"width="228">  | Bildet Beziehungen von einem Ganzen und seinen Teilen ab                                   |


## Aufgaben

- [ ] Experimentieren Sie mit der automatischen Extraktion von UML Diagrammen für Ihre Computer-Simulation aus den Übungen
- [ ] Evaluieren Sie das Add-On "Class Designer" für die Visual Studio Umgebung

![ClassDesigner](./img/14_UML_II/ClassDesigner.png)

https://docs.microsoft.com/de-de/visualstudio/ide/class-designer/how-to-add-class-diagrams-to-projects?view=vs-2019
