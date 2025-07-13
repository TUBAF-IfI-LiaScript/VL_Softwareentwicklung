<!--

author:   Sebastian Zug, Galina Rudolf & André Dietrich
email:    sebastian.zug@informatik.tu-freiberg.de
version:  1.0.7
language: de
narrator: Deutsch Female
comment:  Klassifikation von UML Diagrammtypen, Anwendungsfall Diagramm, Aktivitätsdiagramm, Sequenzdiagramm, Klassendiagramm, Objektdiagramm, UML Tools
tags:      
logo:     
title: Modellierung von Software II

import: https://github.com/liascript/CodeRunner
        https://raw.githubusercontent.com/liascript-templates/plantUML/master/README.md

import: https://raw.githubusercontent.com/TUBAF-IfI-LiaScript/VL_Softwareentwicklung/master/config.md

-->

[![LiaScript](https://raw.githubusercontent.com/LiaScript/LiaScript/master/badges/course.svg)](https://liascript.github.io/course/?https://github.com/TUBAF-IfI-LiaScript/VL_Softwareentwicklung/blob/master/16_UML_ModellierungII.md)

# Modellierung von Software

| Parameter                | Kursinformationen                                                                                  |
| ------------------------ | -------------------------------------------------------------------------------------------------- |
| **Veranstaltung:**       | `Vorlesung Softwareentwicklung`                                                                    |
| **Teil:**                | `14/27`                                                                                            |
| **Semester**             | @config.semester                                                                                   |
| **Hochschule:**          | @config.university                                                                                 |
| **Inhalte:**             | @comment                                                                                           |
| **Link auf den GitHub:** | https://github.com/TUBAF-IfI-LiaScript/VL_Softwareentwicklung/blob/master/16_UML_ModellierungII.md |
| **Autoren**              | @author                                                                                            |

![](https://media.giphy.com/media/26tn33aiTi1jkl6H6/source.gif)

---------------------------------------------------------------------

## Warum die Formalisierung?

**Lastenheft und Pflichtenheft**:

1. Das Lastenheft beschreibt alle Anforderungen und Wünsche des Auftraggebers an ein zukünftiges System,  u.a. *funktionale Anforderungen*: was soll das System tun? (Features, Anwendungsfälle). 

   Beispiel aus dem Lastenheft (was? — Kundensicht): _Der Webshop soll es Endkunden ermöglichen, Produkte aus dem Bereich Haushaltswaren online zu bestellen. Ziel ist es, einen nutzerfreundlichen, performanten und responsiven Online-Shop bereitzustellen, der eine einfache Produktsuche, einen sicheren Bestellprozess sowie gängige Zahlungsmethoden (z. B. PayPal, Kreditkarte) unterstützt._

2. Das Pflichtenheft beschreibt, wie die Anforderungen des Lastenhefts umgesetzt werden sollen, d.h. es enthält detaillierte Spezifikationen und Entwürfe für die Realisierung des Systems und enthält ebenfalls einen Abschnitt zu funktionalen Anforderungen.

   Beispiel aus dem Pflichtenheft (wie? — Entwicklersicht): _Der Webshop wird mit dem Framework Django umgesetzt und verwendet eine PostgreSQL-Datenbank. Die Produktsuche wird über ein Volltext-Suchfeld mit Autovervollständigung realisiert. Der Bestellprozess besteht aus folgenden Schritten:_

    - Warenkorb anzeigen und bearbeiten
    - Benutzer-Login oder Gastbestellung
    - Eingabe der Lieferadresse
    - Auswahl der Zahlungsart (Stripe-Integration für Kreditkarte und PayPal)
    - Bestellübersicht mit Bestätigungsfunktion
    - Automatischer E-Mail-Versand der Bestellbestätigung


```text @plantUML.png
@startuml
actor Auftraggeber
actor Auftragnehmer

== Initiale Anforderungen ==
Auftraggeber -> Auftragnehmer : übermittelt Lastenheft
note right of Auftraggeber
Lastenheft: beschreibt Ziel, Zweck, Anforderungen
aus Sicht des Auftraggebers
end note

== Analyse und Rückfragen ==
Auftragnehmer -> Auftraggeber : stellt Verständnisfragen
Auftraggeber -> Auftragnehmer : klärt offene Punkte

== Erstellung Pflichtenheft ==
Auftragnehmer -> Auftragnehmer : erstellt Pflichtenheft
note right of Auftragnehmer
Pflichtenheft: technische Umsetzung
basierend auf Lastenheft
end note
Auftragnehmer -> Auftraggeber : übergibt Pflichtenheft

== Abstimmung ==
Auftraggeber -> Auftragnehmer : gibt Rückmeldung
alt Änderungen notwendig?
    Auftragnehmer -> Auftragnehmer : überarbeitet Pflichtenheft
    Auftragnehmer -> Auftraggeber : übermittelt aktualisiertes Pflichtenheft
end

== Freigabe ==
Auftraggeber -> Auftragnehmer : gibt Pflichtenheft frei
note right of Auftraggeber
Nach Freigabe beginnt die Umsetzung
end note

@enduml
```

> **Merke:** Das Lastenheft beschreibt die Anforderungen aus Sicht des Auftraggebers, während das Pflichtenheft die technische Umsetzung dieser Anforderungen aus Sicht des Auftragnehmers beschreibt.

> **Merke:** UML-Diagramme sind ein wichtiges Hilfsmittel, um die Anforderungen und das Design eines Systems zu visualisieren und zu kommunizieren. Sie helfen dabei, komplexe Zusammenhänge verständlich darzustellen und dienen als Grundlage für die Implementierung.

## UML Diagrammtypen

![OOPGeschichte](https://upload.wikimedia.org/wikipedia/commons/thumb/d/da/UML-Diagrammhierarchie.svg/1200px-UML-Diagrammhierarchie.svg.png "Autor `Stkl`- derivative work: File: UML-Diagrammhierarchie.png: Sae1962, CC BY-SA 4.0, https://upload.wikimedia.org/wikipedia/commons/thumb/d/da/UML-Diagrammhierarchie.svg/1200px-UML-Diagrammhierarchie.svg.png")

> [OMG-Spezifikation](https://www.omg.org/spec/UML/2.5.1/PDF)

Im folgenden werden wir uns aus den beiden Hauptkategorien jeweils folgende Diagrammtypen genauer anschauen:

+ Verhaltensdiagramme

  + Anwendungsfall Diagramm
  + Aktivitätsdiagramm
  + Sequenzdiagramm

+ Strukturdiagramme

  + Klassendiagramm
  + Objektdiagramm

### Anwendungsfall Diagramm

> Das Anwendungsfalldiagramm (Use-Case Diagramm) abstrahiert das erwartete
> Verhalten eines Systems und wird dafür eingesetzt, die Anforderungen an ein
> System zu spezifizieren.

Ein Anwendungsfalldiagramm ist eine grafische Darstellung der *funktionalen Anforderungen* eines Systems. Es zeigt die verschiedenen Anwendungsfälle (Use Cases), Akteure und deren Interaktionen.

> Achtung: Ein Anwendungsfalldiagramm stellt keine Ablaufbeschreibung dar! Diese kann stattdessen mit einem Aktivitäts-, einem Sequenz- oder einem Kommunikationsdiagramm dargestellt werden.

**Basiskonzepte**

Elemente:

+ Systemgrenzen werden durch Rechtecke gekennzeichnet.
+ Akteure werden als „Strichmännchen“ dargestellt, dies können sowohl Personen (Kunden, Administratoren) als auch technische Systeme sein (manchmal auch ein Bandsymbol verwendet). Sie ordnen den Symbolen Rollen zu.
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



```text @plantUML.png
@startuml
left to right direction

actor Kunde

usecase "Produkte anzeigen" as UC1
usecase "In den Warenkorb legen" as UC2
usecase "Bestellung abschließen" as UC3
usecase "Zahlung durchführen" as UC4
usecase "Gutschein einlösen" as UC5
usecase "Rabattinformationen anzeigen" as UC6

' Basis-Interaktionen
Kunde -- UC1
Kunde -- UC2
Kunde -- UC3

' include: Immer bei Bestellung
UC3 ..> UC4 : <<include>>

' extend: Optionaler Gutschein
UC3 <.. UC5 : <<extend>>\n[Bedingung: Gutschein vorhanden]

' extend: Optional Rabattinfo
UC1 <.. UC6 : <<extend>>\n[Bedingung: Premiumkunde]

' Detaillierte Bedingungen als Notizen
note right of UC5
Bedingung für Erweiterung:
Nur wenn ein gültiger Gutschein
im Kundenkonto oder Warenkorb vorhanden ist
end note

note right of UC6
Bedingung für Erweiterung:
Nur wenn der Kunde eingeloggt
und als Premiumkunde markiert ist
end note

@enduml
```

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

> Halten Sie komplexe `<extend>` Bedingungen in separaten Dokumenten fest.

### Aktivitätsdiagramm

> Aktivitätsdiagramme stellen die Vernetzung von elementaren Aktionen und deren Verbindungen mit Kontroll- und Datenflüssen grafisch dar.


      {{0-1}}
*******************************

**Aktivitätsmodellierung in UML1**

| <img src="https://www.plantuml.com/plantuml/svg/JOynRW9134LxdyAY8bTOYX3gz3Gq1lxkM3Fn8i-CLk8iANCJBXOJjaXdVV_tfB-lJRprhqBqTn4vRf16pCE7DyqeNFibmNR_8pM-mlXaHqaEoxEVkM2ArihpahI0jqTeWuDNyFsDdew0dG-uIohTffFnylX9vKcFisSQ7jzd-0AjyNrbB9EeqN0Gor2xzyXXLtxrDv-A4IvMBybrR66KNbVfPXVJvXlHFe1O-Wi0"> | <img src="https://www.plantuml.com/plantuml/png/JOz1JiD034NtFeKbDic60oIBDX8tgA3hQVlRCPgwmUE0LCrTs706Bf2BCQe8PUdlxoU_TVPWFfqJbUSCAtIRoJ0YE7MRVJJ83a_1eJt9aPlD2Db76BzVKbgrx17AZKAq9Uw6AP_23hoqJZP_pv_e2Ic3czTGIrmU1dLvcx2DuYZ3uInQQjwz_1tO7T4JtxdNAJ_-sq0FSbnUxvPS-ry1_eYImCqMuZ3mJIFFNtx5ggsgbv7M5L7rVm00">|
|Aktivitätsdiagramme.plantUML | ActivityUser.plantUML |

Elemente UML 1.x: 

+ Aktivitätszustände (Activity States)
+ Übergänge (Transitions) 
+ Startzustand (Initial State)
+ Endzustand (Final State) 
+ Entscheidungsknoten (Decision Nodes)
+ Synchronisationsbalken
+ Aktivitätspartitionen.

Bis UML 1.x waren Aktivitätsdiagramme eine Mischung aus Zustandsdiagramm, Petrinetz und Ereignisdiagramm, was zu theoretischen und praktischen Problemen führte.

*******************************

      {{1-2}}
*******************************

**Erweiterung des Konzeptes in UML2**

| Bereich                           | UML 1.x                                               | UML 2.x                                                                                     |
| --------------------------------- | ----------------------------------------------------- | ------------------------------------------------------------------------------------------- |
| **Strukturmodell**                | einfaches Kontrollflussmodell (ähnlich Flussdiagramm) | basiert auf **aktivitätsbasiertem Zustandsautomaten** mit Tokens (ähnlich **Petri-Netzen**) |
| **Objektfluss**                   | rudimentär                                            | **explizite Darstellung von Datenflüssen** zwischen Aktionen möglich                        |
| **Pins**                          | nicht vorhanden                                       | **Input- und Output-Pins** ermöglichen feingranulare Kontrolle über Daten                   |
| **Partitions (Swimlanes)**        | einfach                                               | weiterhin vorhanden, aber besser formalisiert                                               |
| **Unteraktivitäten**              | eingeschränkt                                         | **Call Behavior Action** für Wiederverwendung anderer Aktivitäten                           |
| **Nebenläufigkeit (Concurrency)** | schwer modellierbar                                   | durch **Fork/Join-Nodes** sauber formalisiert                                               |
| **Signale und Ereignisse**        | kaum genutzt                                          | erweiterte Unterstützung für **Signal Send/Receive Actions**                                |
| **Exception Handling**            | nicht vorhanden                                       | neu eingeführt mit **Interruptible Regions** und **Exception Handlers**                     |
| **Zustandsübergänge**             | implizit                                              | explizite Modellierung möglich                                                              |


> "Was früher Aktivitäten waren sind heute Aktionen."

UML2 strukturiert das Konzept der Aktivitätsmodellierung neu und führt als übergeordnete
Gliederungsebene Aktivitäten ein, die Aktionen, Objektknoten sowie Kontrollelemente der
Ablaufsteuerung und verbindende Kanten umfasst. Die Grundidee ist dabei, dass neben dem
Kontrollfluss auch der Objektfluss modelliert wird.

+ Aktivitäten definieren Strukturierungselemente für Aktionen, die durch Ein- und Ausgangsparameter, Bedingungen, zugehörige Aktionen und Objekte sowie einen Bezeichner gekennzeichnet sind.

<!--
style="width: 60%; max-width: 860px; display: block; margin-left: auto; margin-right: auto;"
-->
````ascii

     .---------------------------------------------------.
     | Flächenberechnung Rechteck     ≪ precondition ≫   |
     |                                   Höhe ≥ 0        |
 +---+----+                              Breite ≥ 0      |
 | Höhe   |----ᗒ                                         |
 +---+----+                                          +---+----+
     |                                          ----ᗒ| Fläche |
 +---+----+                                          +---+----+
 | Breite |----ᗒ                                         |
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
     | Einladung verschicken |  -----ᗒ | Getränke einkaufen      |
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
     | Aktion      |------------------ᗒ | Aktion      |
     .-------------.                    .-------------.

 ---------------------------------------------------------------

             Ausgabe-                 Eingabe-
              pin                      pin
                       Objektfluss
    .-------------._                   _.-------------.
    | Aktion      |_|----------------ᗒ|_| Aktion      |
    .-------------.                     .-------------.


    .-------------.   +-------------+     .-------------.
    | Aktion      |---| Objekt      |---ᗒ | Aktion      |
    .-------------.   +-------------+     .-------------.
````


+ Signale und Ereignisse sind die Schnittstellen für das Auslösen einer Aktion

<!--
style="width: 90%; max-width: 860px; display: block; margin-left: auto; margin-right: auto;"
-->
````ascii

 +--------------.       .----------------+       .------------------------.
 |    Sende      \       \  Erster Gast  |------ᗒ| Vorbereitungen beenden |
 |    Signal     /       /  eingetroffen |       .------------------------.
 +--------------.       .----------------+
````


**Beispiel**

![Aktivitätsdiagramme](./img/14_UML_II/ActivityDiagram.png "Beispiel eines Aktivitätsdiagramms - Wikimedia, Autor Gubaer, UML2 Aktivitätsdiagramm,  https://commons.wikimedia.org/wiki/File:Uml-Activity-Beispiel2.svg")

**Anwendung**

+ Verfeinerung von Anwendungsfällen (aus den Use Case Diagrammen)
+ Darstellung von Abläufen mit fachlichen Ausführungsbedingungen
+ Darstellung für Aktionen im Fehlerfall oder Ausnahmesituationen

> Das nachfolgende Video zeigt die Erstellung von Aktivitätsdiagrammen und arbeitet dabei die Unterschiede von UML 1.x und UML 2.x heraus.

!?[Link](https://www.youtube.com/watch?v=VaKCZOhVJkQ)

*******************************

### Sequenzdiagramm

> Sequenzdiagramme beschreiben den Austausch von Nachrichten zwischen Objekten mittels Lebenslinien.

Ein Sequenzdiagramms besteht aus einem Kopf- und einem Inhaltsbereich. Von jedem Kommunikationspartner geht eine **Lebenslinie** (gestrichelt) aus. 

Eine Nachricht wird in einem Sequenzdiagramm durch einen Pfeil dargestellt, wobei der Name der Nachricht über den Pfeil geschrieben wird. Nachrichten können sein:

+ Operationsaufrufe einer Klasse
+ Ergebnisse einer Operation, Antwortsnachricht (gestrichelte Linien)
+ Signale
+ Interaktionen mit den Nutzern
+ das Setzen einer Variablen

Für synchrone und asynchrone Operationsaufrufe (Nachrichten) gibt es verschiedene Notationsvarianten.
**Synchrone Nachrichten** werden mit einer gefüllten Pfeilspitze, **asynchrone Nachrichten** mit einer offenen Pfeilspitze gezeichnet.

Die schmalen Rechtecke, die auf den Lebenslinien liegen, sind **Aktivierungsbalken**, die den Focus of Control anzeigen, also jenen Bereich, in dem ein Objekt über den Kontrollfluss verfügt, und aktiv an Interaktionen beteiligt ist.

**Beispiel**

```text @plantUML.png

@startuml
actor Kunde
participant "Webbrowser" as Browser
participant "Webshop-Frontend" as Frontend
participant "Webshop-Backend" as Backend
participant "Zahlungsdienst" as Payment

Kunde -> Browser : Produktseite aufrufen
activate Browser
Browser -> Frontend : HTTP GET /produkt/123
activate Frontend
Frontend -> Backend : Produktdaten abrufen
activate Backend
Backend --> Frontend : Produktdetails
deactivate Backend
Frontend --> Browser : HTML mit Produktinformationen
deactivate Frontend
deactivate Browser

Kunde -> Browser : In den Warenkorb legen
activate Browser
Browser -> Frontend : HTTP POST /warenkorb
activate Frontend
Frontend -> Backend : Warenkorb aktualisieren
activate Backend
Backend --> Frontend : OK
deactivate Backend
Frontend --> Browser : Rückmeldung „Produkt im Warenkorb“
deactivate Frontend
deactivate Browser

Kunde -> Browser : Bestellung auslösen
activate Browser
Browser -> Frontend : HTTP POST /checkout
activate Frontend
Frontend -> Backend : Bestellung erstellen
activate Backend
Backend -> Payment : Zahldaten prüfen
activate Payment
Payment --> Backend : Zahlung bestätigt
deactivate Payment
Backend --> Frontend : Bestellung abgeschlossen
deactivate Backend
Frontend --> Browser : Bestellbestätigung
deactivate Frontend
deactivate Browser

Browser -> Kunde : Anzeige „Vielen Dank für Ihre Bestellung!“
@enduml
```


**Bestandteile**

| Name                   | Beschreibung                                                                                                          |
| ---------------------- | --------------------------------------------------------------------------------------------------------------------- |
| Objekt                 | Dient zur Darstellung eines Objekts (:Klasse) im Kopfbereich.                                                         |
| Nachrichtensequenzen   | Modelliert den Informationsfluss zwischen den Objekten                                                                |
| Aktivitätsbalken       | Repräsentiert die Zeit, die ein Objekt zum Abschließen einer Aufgabe benötigt.                                        |
| Akteur                | Repräsentiert einen Nutzer (Strichmännchen)                                                                            |
| Lebenslinien-Symbol    | Stellt durch die Ausdehnung nach unten den Zeitverlauf dar.                                                           |
| Fragmente              | Kapseln Sequenzen in Szenarien, wie optionalen Interaktionen (opt), Alternativen (alt), Schleifen (Loop), Parallelität (par)  |
| Interaktionsreferenzen | Binden Submodelle und deren Ergebnisse ein  (ref)                                                                     |



```text @plantUML.png
@startuml
actor User
participant System
participant ServiceA
participant ServiceB

User -> System : Anfrage starten
activate System

System -> ServiceA : Task A starten
activate ServiceA

System -> ServiceB : Task B starten
activate ServiceB

' Beide Services arbeiten parallel
ServiceA --> System : Task A Ergebnis
deactivate ServiceA

ServiceB --> System : Task B Ergebnis
deactivate ServiceB

' System wartet auf beide Ergebnisse
System -> User : Ergebnis liefern
deactivate System
@enduml
```

**Beispiel**

| <img src="https://www.plantuml.com/plantuml/png/ZPCn3jim34LtdUBt00NQ8Ok15idGR0K2z0IApTX2PCgGw0w6ZzQj5wiYrqOP1sYM26B8pwTF-fZ4HXbxRuPykjeDbWLdsqMAAdG9taqkYGgUxg4BNYZ9K_Pfb63C8eEFNxVg2Z_gpbjkYRrZHxWzKjZOMjnFAxHstkvRMpFFdm_pRDP0LlsRNC5oPxtiPJO0HR1cL-5k6jftQkXPPXzvFDO0hs0k55DWwnqnXlZxrutO0FismBCeiBmH1lWlEV2A7TYpaa-aMSjuglB_H6CxZKaiD-_IbjcY79fl4to7RnKYReonNfHl7xuWt14QYflc4dljULPAGgubXhSfvMFa1hqJfgudrWxeodEqBO5jtU7aopOlH64Gw1Arx5M-vLSuYielbRFCyrFUMQZul-s_KqeZjoXVePOgAKfatOaTowfofSnEektNKeSzZXhXjzZIbLs2vQaugfHKOJ7gc7_6tly1"> |
|Alkoholkontrolle.plantUML |

**Anwendung**

+ Verfeinerung von Anwendungsfällen (aus den Use Case Diagrammen)
+ Darstellung von Kommunikation zwischen Systemkomponenten
+ Darstellung der Steuerung des Programmflusses und die
+ Darstellung der Behandlung von Ausnahmen 

**Sequenzdiagramm vs. Aktivitätsdiagramm**

+ Beide können zur Beschreibung von Use Cases und einzelnen Methoden einer Klasse verwendet werden
+ bieten unterschiedliche Perspektiven und Betrachtungsweisen auf diese Elemente:
+ Aktivität: Visualisierung der Ablaufschritte, Darstellung/Dokumentation von komplexen Geschäftsprozessen oder Systemabläufen
+ Sequenz: Darstellung von Interaktionen zwischen Objekten, einschließlich Aufrufen von Methoden einer Klasse, betonen die zeitliche Abfolge der Kommunikation

### Klassendiagramme

> Ein Klassendiagramm ist eine grafischen Darstellung (Modellierung) von Klassen, Schnittstellen sowie deren Beziehungen.

**Beispiel**

Nehmen wir an, sie planen die Software für ein Online-Handel System. Es soll
sowohl verschieden Nutzertypen (*Customer* und *Administrator*) als auch die Objekt *ShoppingCart* und *Order* umfassen.

```text @plantUML.png
@startuml
left to right direction
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
Klasse (fett gedruckt, abstrakte und Interfaces evtl. kursiv) tragen oder zusätzlich auch Attribute, Operationen und
Eigenschaften spezifiziert haben.  Oberhalb des Klassennamens können
Schlüsselwörter in _Guillemets_ ('<< >>') und unterhalb des Klassennamens in
geschweiften Klammern zusätzliche Eigenschaften (wie {abstrakt}) stehen.
Mit Schlüsselwörtern können zusätzliche Informationen oder Meta-Eigenschaften zur Standardsemantik der Elemente hinzugefügt werden. Sie bieten eine Möglichkeit, benutzerdefinierte Modellierungskonzepte hinzuzufügen oder vorhandene Konzepte zu präzisieren.

Elemente der Darstellung :

| Eigenschaften | Bedeutung                                                                         |
| ------------- | --------------------------------------------------------------------------------- |
| Attribute     | Beschreiben die Struktur der Objekte: die Bestandteile und darin enthaltene Daten |
| Operationen   | Beschreiben das Verhalten der Objekte (Methoden)                                  |
| Zusicherungen | Bedingungen, Voraussetzungen und Regeln, die die Objekte erfüllen müssen          |
| Beziehungen   | Beziehungen einer Klasse zu anderen Klassen                                       |

Wenn die Klasse keine Eigenschaften oder Operationen besitzt, können die entspechenden Abschnitte wegfallen.

![OOPGeschichte](./img/14_UML_II/ClassTypes.png "UML Klassendiagramme und deren Attribute - adaptiert aus [^WikiUMLClass]")


********************************************************************************

                                  {{1-2}}
********************************************************************************

**Objekte vs. Klassen**

| Klassendiagramm                                                                                                                                                                                                                                                                                                                                   | Beispielhaftes Objektdiagramm                                                                          |
| ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- | ------------------------------------------------------------------------------------------ |
| <img src="https://www.plantuml.com/plantuml/png/TP2_JWCn3CRtF8Lr4r1LWXKn5C7I1OPxWjGnhKNYd1o7WJ_lJhXhInYOelm-Vpz7sHJpQZL5vyeho-JL9y3eIxatKzvLeorc6VcTuDeTE10WAqPoRfw2Xrei9rBuS01h49y8Rc5iNTdF96Rqnbbwke924fura5vSpdSb97wZ4_ewwHiukuEt7_TZEvPTDUr86zcAqPvq5c6M5zPqr6BCXNhVKzRauwzr1O7Bv0qFVn-oT8wbSIBaVrjjRadkFjVhVa6FUuKx_zbmCq0BXwi1bcH34cgAtm00" width="228"> | <img src="https://www.plantuml.com/plantuml/png/TSwnJiCm4CRntKznojvWEss7gaIL8h4HaF84RdmK8pj6b_DKyMPil1WnY10ARDRdltwwmyY6E0KlF3umS0HofqS6wmXxTd4G8XwVfYSK-rGtQUGOa7PmZuoqlqfr0MQ4vYm64n2kFtUuVNlr7adus9kCJ1ytXEw8T18lFyGsKZ3-Zcn6LQNqUaPRTHRMS5OBfKkvARNKiX132XCqhHzvzlu7rzKIrD-Wbd-1ERyui7l6tisWtcotizSj3wnAMLrwkSvS0wjL3Qs27SJieFsQzA-fMrO776mA_Xq0" width="300">  |
_ Gegenüberstellung motiviert nach _What is Object Diagram?_, https://www.visual-paradigm.com/guide/uml-unified-modeling-language/what-is-object-diagram/, Autor unbekannt_


**Objektdiagramm** stellt die Instanzen von Klassen und ihre Beziehungen zueinander zu einem bestimmten Zeitpunkt dar basierend auf einem Klassendiagramm
(ein „Schnappschuss“ der Objekte innerhalb eines Systems) und enthält als Elemente: Objekte (Instanzen der Klasse), Links (Instanzen von Assoziationen), Attribute (Werte von Eigenschaften von Objekten).

Anwendung des Objektdiagramms:

+ Um den Zustand eines Systems zu einem bestimmten Zeitpunkt darzustellen bzw. während der Laufzeit zu visualisieren.
+ Um Testfälle und Szenarien zu beschreiben.



********************************************************************************

                                  {{2}}
********************************************************************************

> **Merke:** Vermeiden Sie bei der Benennung von Klassen, Attributen, Operationen usw. sprachspezifische Zeichen

Modellierung in UML

```text @plantUML.png
@startuml
skinparam classAttributeIconSize 0

class Zähler{
  +i: int = 12345
}
@enduml
```

Ausführbarer Code in Python 2

```python KlasseMitUmlaut.py
class Zaehler:
    """A simple example class"""
    i = 12345

A = Zaehler()
print A.i
```
@LIA.evalWithDebug(`["main.py"]`, `python2.7 -m compileall .`, `python2.7 main.pyc`)


Ausführbarer Code in C++ 20

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

```text @plantUML.png
@startuml
skinparam classAttributeIconSize 0
hide circle
class Car{
  - speed: double
  - setSpeed(double speed)
  + GetSpeed() : double
}

class TrafficOperator{
  - Vehicles: Car[]
  + MonitorAllCars()
}

TrafficOperator "1" *-- "0:.. "Car : "greift zu auf"

note top of Car : speed als Property,\npublic get Methode, \nprivate set Methode
note top of TrafficOperator : Das Objekt kann auf die \nGeschwindigkeiten aller Fahrzeuge\n zurückgreifen, sie aber nicht verändern.
@enduml
```

**protected**

Die abgeleitete Klassen Bus und PassagerCar erben von Car und übernehmen damit deren Methoden. Die Zahl
der Sitze wird beispielsweise mit ihrem Initialisierungswert von 5 auf 40 gesetzt.
Zudem muss die Methode `StopAtStation` auch auf die Geschwindigkeit zurückgreifen können.

```text @plantUML.png
@startuml
skinparam classAttributeIconSize 0
hide circle
abstract class Car{
  - speed: double
  - numberOfSeats
  # setSpeed(double speed)
  + getSpeed() : double
}

class PassengerCar{
  - numberOfSeats: int = 5
}

class Bus{
  - numberOfSeats: int = 40
  + CheckTickets()
  + StopAtStation()
}

class TrafficOperator{
  - Vehicle: Car[]
  + monitorAllCars()
}

Car <|-- Bus : "erbt von"
Car <|-- PassengerCar : "erbt von"
TrafficOperator "1" *-- "0:.. "PassengerCar : "greift zu auf"
TrafficOperator "1" *-- "0:.. "Bus : "greift zu auf"
@enduml
```

**internal**

Ein Member vom Typ `internal` einer Basisklasse kann von jedem Typ innerhalb seiner enthaltenden Assembly aus zugegriffen werden.


```text @plantUML.png
@startuml
skinparam classAttributeIconSize 0
hide circle
package RoadTraffic <<Folder>> {
  class Vehicle{
    - speed: double
    ~ setSpeed(double speed)
    + getSpeed(): double
  }
}

class Junction{
  - cars: Traffic::Vehicles[]
  + SimulateJam()
}

class Airplane{
  - heigth : double
  + Fly()
}

Junction "1" -- "0:.. "Vehicle : "greift zu auf"
Airplane -|> Vehicle
@enduml
```

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
    Das "/" bedeutet, dass es sich um ein abgeleitetes Attribut handelt.
+ *Name* ... des Attributes, Leer und Sonderzeichen sollten weggelassen werden, um zu vermeiden, dass Sie bei der Implementierung Probleme generieren.

+ *Typ* ... UML verwendet zwar einige vordefinierte Typen (Integer, Real, String, Boolean, UnlimitedNatural) beinhaltet aber keine Einschränkungen zu deren Wertebereich!

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
| `subsets`   | definiert die zugelassene Belegung als Untermenge eines anderen Attributs     |
| `redefines` | überschreiben eines ererbten Attributes                                       |
| `ordered`   | Inhalte eines Attributes treten in geordneter Reihenfolge ohne Duplikate auf  |
| `bag`       | Attribute dürfen ungeordnet und mit Duplikaten versehen enthalten sein        |
| `sequence`  | legt fest, dass der Inhalt sortiert ist, Duplikate sind erlaubt               |
| `composite` | starke Abhängigkeitsbeziehungen                                               |

Daraus ergeben sich UML-korrekte Darstellungen

| Attributdeklaration              | Korrekt | Bemerkung                                                                          |
| -------------------------------- | ------- | ---------------------------------------------------------------------------------- |
| `public zähler:int `             | ja      | Umlaute sind nicht verboten                                                        |
| `/ alter`                        | ja      | Datentypen müssen nicht zwingend angegeben werden                                  |
| `privat adressen: String [1..*]` | ja      | Menge der Zeichenketten                                                            |
| `protected bruder: Person`       | ja      | Datentyp kann neben den Basistypen jede andere Klasse oder eine Schnittstelle sein |
| String                           | nein    | Name des Attributes fehlt                                                          |
| privat, public name: String      | nein    | Fehler wegen mehrfachen Zugriffsattributen                                         |

![Protected](https://www.plantuml.com/plantuml/png/LP1FImCn4CNl-HIFUXHhrzO_18HQyE2v7WJna4q66yncbcGMr6Nzx4Pj3RqDFtxlmRnr5fDaw8BARpxs9faEDfYSxqIIRmUX1yjnwpy92xNpZc1zie7KhuJxTzFrWKO5c4EWqV1H2ZcjxEB0nYn6l9tG3stm5XgEoqMunNBUh8fnLbDBZPOva8c5drI-qiWxJeA2log-rDYMCxwMijaZPPbXJ6GSn_0nALJnIcFpl9ZFcvVHSjTpza97mlL_qrxZv2YH-v7EgtQZ9hKVOdCqmTFVkvDQKtHbt6y0 "Korrespondierendes UML-Klassendiagramm [AttributeExample.plantUML](https://github.com/liaScript/CsharpCourse/blob/master/code/10_UML/Graphs/AttributeExample.plantUML)")

```csharp    AttributeExample
using System;

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
  Object attribute8{
    get{return wert * 10;}
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


![Protected](https://www.plantuml.com/plantuml/png/HO_1IiH044JlynK5NhAAYzdLIq1O5G-UzoWUnfb66oSzeTC1DSHlTpQ1pQtegbT57thpQaERNF_5qddrBKBoVVzWflm-6Bs4B4V-9TouJuw4m1eIkJc4vs_VTeb61-0AOoap3XDoHrfKbhhQdAphzRAhM33lr0rO3FUusongLF8nI_gPJip5okmBamTRi1qQFCsjVslYjZhx-mP7KvOFlDRRxUKRngoHbE9KPXxTv0uaiJpy3m00 "Korrespondierendes UML-Klassendiagramm")

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

  protected void operation3 (ref C param2)
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

![Protected](https://www.plantuml.com/plantuml/png/NP1D3e9038NtFKMNke0J48BHZOc91p0kIgbnp30aCtNXZtSN5K9nsUzxRvffbPIYNbiFPzS8ieli1O7gf95OaJsbXAjXtBcaanlfklDUM5qNm0MLU28M_4InABOZA4iZfyPV17wrPvxFTzg2aNOrJCnaisp-a1q67IFTlWw6puu07u2uho_2UZYYU6abw8QKUfpSNHPBU44beUdFzmO0 "Darstellung von Schnittstellen in UML-Klassendiagrammen")

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
| Generalisierung                       | <img src="https://www.plantuml.com/plantuml/png/LOv12W8n40Jlyuh-m7vW3x9NRu8l2CcO7KmcC9diHVsxgoZehMcgw5ww1_DH2wsI654i5gR25uStcEJLaSGukZIbM4BlEB7rfZS2D5IUSNOJpGHipVwQnrSmsO9VXoP-MNWd7RMopJYgSsRROVhXC4ttmsU95jQqNtm2" width="660"> | gerichtete Beziehung zwischen einer generelleren und einer spezielleren Klasse (Vererbung) |
| Assoziationen (ohne Anpassung)        | <img src="https://www.plantuml.com/plantuml/png/NSon3S9038NXtbEy1LWW1Kf1Hya4nvr9B3csvFFHeEoE0550-_H_R-vP8iUcj4fZL8cgJCgtH3f2bZNH9BYck15LBRtZx9R-8C3AnXCk6M8B0Rreymad7rTbsh_riCQl6dUFFm4hTVtPTgBtFGx7ZwIPhOrDdm00"width="160">          | beschreiben die Verknüpfung allgemein                                                      |
| Assoziation (Komposition/Aggregation) | <img src="https://upload.wikimedia.org/wikipedia/commons/thumb/f/f8/Komposition_Aggregation.svg/1920px-Komposition_Aggregation.svg.png"width="660">  | Bildet Beziehungen von einem Ganzen und seinen Teilen ab                                   |

## UML- Metamodell

Abstraktionseben der UML-Modellierung:

+ M0 - Instanzebene: repräsentiert die konkrete Ausführung des Systems und ist nicht direkt in der UML-Modellierung abgebildet, auf dieser Ebene befinden sich die tatsächlichen Instanzen von Objekten, die während der Laufzeit eines Systems existieren. 

+ M1 - Modellierungsebene: beinhaltet verschiedene Arten von UML-Diagrammen wie Klassendiagramme, Aktivitätsdiagramme, Zustandsdiagramme usw. für eigentliche Benutzermodelle. 

+ M2 - Metamodell-Ebene: beinhaltet die Modelle, die das System selbst beschreiben. Das UML-Metamodell ist ein Beispiel für ein Metamodell auf dieser Ebene. Es beschreibt die Struktur und Syntax der UML selbst und ermöglicht es, UML-Diagramme zu erstellen und zu interpretieren.

+ M3 - Metametamodell-Ebene: Diese Ebene beschreibt die Struktur und Semantik von Metamodellen auf der M2-Ebene. Metametamodelle definieren die Regeln und Konzepte, die verwendet werden, um Metamodelle zu erstellen. 

![Metamodel-Hierarchy](https://upload.wikimedia.org/wikipedia/commons/thumb/6/6f/MetamodelHierarchy_de.svg/1920px-MetamodelHierarchy_de.svg.png "Von Jens von Pilgrim - created by author, based on OMG: Unified Modeling Language: Infrastructure. Page 31, CC BY-SA 3.0, https://commons.wikimedia.org/w/index.php?curid=9914721")


## Verwendung von UML Tools

Verwendung von Klassendiagrammen

+ ... unter Umbrello (UML Diagramm Generierung / Code Generierung)

+ ... unter Microsoft Studio [Link](https://docs.microsoft.com/de-de/visualstudio/ide/class-designer/how-to-add-class-diagrams-to-projects?view=vs-2019))

![ClassDesigner](./img/14_UML_II/ClassDesigner.png)

!?[VisualStudio](https://www.youtube.com/watch?v=syYyQ7LlcPc)

+ ... unter Visual Studio Code mit PlantUML oder UMLet (siehe Codebeispiele zu dieser Vorlesung)

+ ... unter Visual Studio Code mit [PlantUmlClassDiagramGenerator](https://github.com/pierre3/PlantUmlClassDiagramGenerator)

## Aufgaben

- [ ] Experimentieren Sie mit [Umbrello](https://umbrello.kde.org/)
- [ ] Experimentieren Sie mit der automatischen Extraktion von UML Diagrammen für Ihre Computer-Simulation aus den Übungen
- [ ] Evaluieren Sie das Add-On "Class Designer" für die Visual Studio Umgebung
