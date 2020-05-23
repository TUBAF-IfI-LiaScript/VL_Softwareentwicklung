<!--

author:   Sebastian Zug & André Dietrich
email:    Sebastian.Zug@informatik.tu-freiberg.de & andre.dietrich@informatik.tu-freiberg.de
version:  0.0.1
language: de
narrator: Deutsch Female

import: https://raw.githubusercontent.com/liaScript/rextester_template/master/README.md
        https://raw.githubusercontent.com/liascript-templates/plantUML/master/README.md

-->

# Softwareentwicklung - 14 - Modellierung einer Software

**TU Bergakademie Freiberg - Sommersemester 2020**

Link auf die aktuelle Vorlesung im Versionsmanagementsystem GitHub

[https://github.com/SebastianZug/CsharpCourse/blob/SoSe2020/14_UML_II.md](https://github.com/SebastianZug/CsharpCourse/blob/SoSe2020/14_UML_II.md)

Die interaktive Form ist unter diese Link zu finden ->
[LiaScript Vorlesung 14](https://liascript.github.io/course/?https://raw.githubusercontent.com/SebastianZug/CsharpCourse/SoSe2020/14_UML_II.md#1)

---------------------------------------------------------------------

## 7 Fragen in 7 Minuten

**1. Jetzt sind Sie dran ...**

**2. Jetzt sind Sie dran ...**

**3. Jetzt sind Sie dran ...**

**4. Jetzt sind Sie dran ...**

**5. Jetzt sind Sie dran ...**

**6. Jetzt sind Sie dran ...**

**7. Jetzt sind Sie dran ...**


## UML Diagrammtypen

![OOPGeschichte](/img/13_UML/UML-Diagrammhierarchie.png)<!-- width="90%" --> [^5] WikiUMLDiagrammTypes

### Use-Case Diagramm

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
+ Anwendungsfälle werden in Ellipsen dargestellt. Sie müssen beschrieben werden (z. B. in einem Kommentar oder einer eigenen Datei).
+ Beziehungen zwischen Akteuren und Anwendungsfällen müssen durch Linien gekennzeichnet werden.

![Modelle](http://www.plantuml.com/plantuml/png/RL11Rkf03DtFAInMi63n-HUWeAverGLIkpQ9IKPndCZZgGHLRzDZTCV5EY4f6gIk_6I_Pp-_TJ1KYoqxfgE1TQ2-gWrAhrIOxyI5nakFYYtqM3HOqTvEJ32CKIecXuLr2hievI_Urrt_Z9AuEdMU1ko3kPiCNeIzK4XK-700ymSrtn33WO8HCya2C40CjCL0_nmaoYQDK4eeenPrY4LzJrev66t0SlbdRtv5KgAHmEKhOPL5JiYkpTzGIP9LEdG6_P6f6gubKlxTP8gOerHmZYsyaeR1utkd1rBoDgbk2NowB8JP1gK9fs3Kml7ohV2uNUvGasWsFBQ_JbPkghd5VCaO9MpeZ3MFspBvVpVLExbRavInvHy0)<!-- width="70%" --> [UseCase.plantUML]

**Verfeinerung**

Use-Case Diagrammer erlaube die Abstraktion von Elementen auf der Basis von Generalisierungen. So können Akteure von einander erben und redundante Beschreibungen von Verhalten über `<<extend>>` oder `<<include>>` (unter bestimmten Bedingungen) erweitert werden.

![PartyUCD](http://www.plantuml.com/plantuml/png/bPB1IWCn48RlUOfXJtfeeJtAiXH4Un2AXzQJbsnst8J6ISaa82rz6v_1Ks_xOgQnk2jLgBT_P-QR-VatCmxIX_XgXLJ1UPXB3WFPOet-zFtehDcY4SAZ2f9QYw0qghMB0NOYCYh92AlR2KY6Gen60jy24Xj7i7660ZyJBAJIh7IiOUaDhP76XHYmqylOE-OlykWHHXLSBILjaQxxmee2UYrj65QJkO6UzjFpxfq3WZOa8zVOMZGP5DyXvB7UWVCD-hnvsBz7avICBxRxuCoVGXjsQnyt0Mf4QlB86GNZyT-Mu0RAqf2oiS6g4hyl_Sy6GIuD5MNvv7oDZQ4sKbjLa0BVFNG7RLbfBsaDBMviTqZxVTWN81aM4t6UI3fzR2mcIUJ0ZzVopspIiGk0wGzWBsP9fFVv2G00)<!-- width="70%" --> [UseCase.plantUML]

Beispiel motiviert aus dem Beispiel von Jeckle Seite 240 (Mario Jeckle, Christine Rupp, Jürgen Hahn, Barbara Zengler, Stefan Queins, UML 2 glasklar, Hanser Verlag, 2004)

|                | <img src="https://raw.githubusercontent.com/liaScript/CsharpCourse/master/img/10_UML/UseCaseInclude.png" height="42">                             | <img src="https://raw.githubusercontent.com/liaScript/CsharpCourse/master/img/10_UML/UseCaseExtend.png" height="42">         |
| -------------- | ------------------------------------------------ | ------------------------------------------------ |
|                | `<<include>>` Beziehung                          | `<<extend>>` Beziehung                           |
| Bedeutung      | Ablauf von A schließt den Ablauf von B immer ein | Ablauf von A kann optional um B erweitert werden |
| Anwendung      | Hierachische Zerlegung                           | Abbildung von Sonderfällen                       |
| Abhängigkeiten | A muss B bei der Modellierung berücksichtigen    | Unabhängige Modellierung möglich                 |


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

### Aktivitätsdiagramm

> Aktivitätsdiagramme stellen die Vernetzung von elementaren Aktionen und deren
> Verbindungen mit Kontroll- und Datenflüssen grafisch dar.

**Aktivitätsmodellierung in UML1**

| <img src="http://www.plantuml.com/plantuml/svg/JOynRW9134LxdyAY8bTOYX3gz3Gq1lxkM3Fn8i-CLk8iANCJBXOJjaXdVV_tfB-lJRprhqBqTn4vRf16pCE7DyqeNFibmNR_8pM-mlXaHqaEoxEVkM2ArihpahI0jqTeWuDNyFsDdew0dG-uIohTffFnylX9vKcFisSQ7jzd-0AjyNrbB9EeqN0Gor2xzyXXLtxrDv-A4IvMBybrR66KNbVfPXVJvXlHFe1O-Wi0"> | <img src="http://www.plantuml.com/plantuml/png/JOz1JiD034NtFeKbDic60oIBDX8tgA3hQVlRCPgwmUE0LCrTs706Bf2BCQe8PUdlxoU_TVPWFfqJbUSCAtIRoJ0YE7MRVJJ83a_1eJt9aPlD2Db76BzVKbgrx17AZKAq9Uw6AP_23hoqJZP_pv_e2Ic3czTGIrmU1dLvcx2DuYZ3uInQQjwz_1tO7T4JtxdNAJ_-sq0FSbnUxvPS-ry1_eYImCqMuZ3mJIFFNtx5ggsgbv7M5L7rVm00">|
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

![Aktivitätsdiagramme](/img/14_UML_II/ActivityDiagram.png)<!-- width="70%" --> [WikiActivityDiagram]

Beispiels auf Anwendungsfall [Link](https://www.youtube.com/watch?v=VaKCZOhVJkQ)

**Anwendungsfälle**

+ Verfeinerung von Anwendungsfällen (aus den Use Case Diagrammen)
+ Darstellung von Abläufen mit fachlichen Ausführungsbedingungen
+ Darstellung für Aktionen im Fehlerfall oder Ausnahmesituationen

### Sequenzdiagramm

> Sequenzdiagramme beschreiben den Austausch von Nachrichten zwischen Objekten mittels Lebenslinien.

Ein Sequenzdiagramms besteht aus einem Kopf- und einem Inhaltsbereich. Das Schlüsselwort im Kopfbereich ist bei einem Sequenzdiagramm `sd`. Von jedem Kommunikationspartner geht eine Lebenslinie (gestrichelt) aus. Es sind zwei synchrone Operationsaufrufe, erkennbar an den Pfeilen mit ausgefüllter Pfeilspitze, dargestellt. Notationsvarianten für synchrone und asynchrone Nachrichten

Eine Nachricht wird in einem Sequenzdiagramm durch einen Pfeil dargestellt, wobei der Name der Nachricht über den Pfeil geschrieben wird. Nachrichten können:

+ Operationsaufrufe einer Klasse sein
+ Ergebnisse einer Operation
+ Signale
+ Interaktionen mit dem Nutzern
+ das Setzen einer Variablen

Synchrone Nachrichten werden mit einer gefüllten Pfeilspitze, asynchrone Nachrichten mit einer offenen Pfeilspitze gezeichnet.

Die schmalen Rechtecke, die auf den Lebenslinien liegen, sind Aktivierungsbalken, die den Focus of Control anzeigen, also jenen Bereich, in dem ein Objekt über den Kontrollfluss verfügt, und aktiv an Interaktionen beteiligt ist.


**Beispiel**

![Aktivitätsdiagramme](/img/14_UML_II/SequenzCheckEmail.png)<!-- width="60%" --> [WikiSequenceDiagram](#7)

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

| <img src="http://www.plantuml.com/plantuml/png/ZPCn3jim34LtdUBt00NQ8Ok15idGR0K2z0IApTX2PCgGw0w6ZzQj5wiYrqOP1sYM26B8pwTF-fZ4HXbxRuPykjeDbWLdsqMAAdG9taqkYGgUxg4BNYZ9K_Pfb63C8eEFNxVg2Z_gpbjkYRrZHxWzKjZOMjnFAxHstkvRMpFFdm_pRDP0LlsRNC5oPxtiPJO0HR1cL-5k6jftQkXPPXzvFDO0hs0k55DWwnqnXlZxrutO0FismBCeiBmH1lWlEV2A7TYpaa-aMSjuglB_H6CxZKaiD-_IbjcY79fl4to7RnKYReonNfHl7xuWt14QYflc4dljULPAGgubXhSfvMFa1hqJfgudrWxeodEqBO5jtU7aopOlH64Gw1Arx5M-vLSuYielbRFCyrFUMQZul-s_KqeZjoXVePOgAKfatOaTowfofSnEektNKeSzZXhXjzZIbLs2vQaugfHKOJ7gc7_6tly1"> |
|Alkoholkontrolle.plantUML |

### Klassendiagramme

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

#### Klassen

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

![OOPGeschichte](/img/14_UML_II/ClassTypes.png)<!-- width="70%" --> [WikiUMLHistory](#7)

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

![PublicPrivate](http://www.plantuml.com/plantuml/png/NP51pjCm48NtFiLJLd-XhNwsMRK2KR6eYbJ4Xh9mx4aonBKZUr944RVX6EnwCPoaWEZspJVVFCkR93hAS7OgTUnx7VKPnkcKjYAHgq7edGd-o5V2ishP4Wn7uqXD45xh-5q1AwIUo9QmOQWSpGd9SGoVvcmc5ddq4hi_emN-hlnGT-M7gEkQpQ6dg2NSzH-fvNmrbUF5Jvydopvupi3MkPobfyMeU5X6yQh0YzKAnNEvNgF884eKJIIk1TS1UgWBfNmGWeGUeSQCJ6MXNSBx6B9ClYrFliz5sQ0XmPwa3PQMo6daYnP2-XV_9toiLEADJZXKNwWJTDhxiGGiKtRjA9dsAtlBJKSib6VEKSHRtSOh3GsTV6uTRxzCDxs1_1A92RhAa6VJ2YuKRp-zfUZNQaFUvg_z3G00)<!-- width="60%" --> [PublicPrivate.plantUML](https://github.com/liaScript/CsharpCourse/blob/master/code/10_UML/Graphs/publicPrivate.plantUML)

**protected**

Die abgeleitete Klassen Bus und PassagerCar erben von Car und übernehmen damit deren Methoden. Die Zahl
der Sitze wird beispielsweise mit ihrem Initialisierungswert von 5 auf 40 gesetzt.
Zudem muss die Methode `StopAtStation` auch auf die Geschwindigkeit zurückgreifen können.

![Protected](http://www.plantuml.com/plantuml/png/XKy_Jm8n5D_lK_nI5z10CD7beWd8v8J94HVZqEkzWuPUUsbV7L2_kuKgXXjqQPhV_pSFB3t7pXHXfsqllUoWZ0nXmUnr7PaUbRELtXDcnLOt1AMzCbJ8Eh2NYhCQI-dV2s22q1Cr9HeNQqD7nCQk9hzgAv8S4dA1G5mTPAEiofvnegwm-Q76E8Ly5aKkUKeFsGtvt3RCBg4junwtluw76FuGtioEdSijgTrQgnrn6EKP5Rj-mHLBriucx1Iuzh9jjLhrv2MxxnNFjDNAK8cqw-Nr6D0vgnE_C2PX8IUa7-u-9fF3AfGGv6l6cxFYn9nTU2uP54DS2rmcXvYLqod4qBZnf5l6FaB6zbzt7ZGqpSasiJDV)<!-- width="60%" --> [protected.plantUML](https://github.com/liaScript/CsharpCourse/blob/master/code/10_UML/Graphs/protected.plantUML)

**internal**

Ein Member vom Typ `protected internal` einer Basisklasse kann von jedem Typ innerhalb seiner enthaltenden Assembly aus zugegriffen werden.

![Protected](http://www.plantuml.com/plantuml/png/JOwzRkf044RxVOfFha38YDjQ2463T2b3b2PACUoExH7hjRK_HKZ8iqTcRONspCmvim-HV4oTLU4gRY1F7RIb40unUhcaoFzrxyvoOsnKAuQXnMlBQY1zfORnqfDvzLJNeh7T7djhsEzs-5B87hnnAzho280IOM0s5KoVBfOVx0U1utd4Yqpppl8nNA6PXykdgxkwAvNzf-HqbDwDWHAQVAWmFLHLKpoyVwZHTPOkMOfyecwn_5CSn0-M76T5ozB45a-r5OxsCn_CDHJ_2fGbYasrNgEOEgXGD9wbZhWbKAeBDRjHVkymhQazEvCw-mi0)<!-- width="50%" --> [internal.plantUML](https://github.com/liaScript/CsharpCourse/blob/master/code/10_UML/Graphs/internal.plantUML)

Der UML Standard kennt nur + Public, - Private, # Protected und ~ Internal. Das
C# spezifische `internal protected` ist als weitere Differenzierungsmöglichlichkeit nicht
vorgesehen.

**Aktive Klassen**
Die UML unterscheidet zwischen aktiven und passiven Klassen. Während die Trigger
passiver Klassen von außen kommen, sind aktive Klassen dadurch gekennzeichnet,
dass Sie ein „Eigenleben führen“. Unmittelbar wenn eine Instanz einer aktiven Klasse angelegt wird, startet ein für die Klasse spezifiziertes Verhalten. Dieses Verhalten läuft weiter, bis es explizit gestoppt oder bis das Objekt zerstört wird.


**Objekte vs. Klassen**

![OOPGeschichte](/img/14_UML_II/ObjectVsClass.png)<!-- width="90%" --> [VisualParadigm](#7)

**Attribute**

Im einfachsten Fall wird ein Attribut (unter C# sprechen wir von Feldern) durch
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

```python
class Zähler:
    """A simple example class"""
    i = 12345

    def count(self):
        self.i += 1

A = Zähler()
A.count()
print(A.i)
```
@Rextester.eval(@Python)

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
| `public int zähler`              | ja      | Umlaute sind nicht verboten                                                        |
| `/ alter`                        | ja      | Datentypen müssen nicht zwingend angegeben werden                                  |
| `privat adressen: String [1..*]` | ja      | Menge der Zeichenketten                                                            |
| `protected bruder Person`        | ja      | Datentyp kann neben den Basistypen jede andere Klasse oder eine Schnittstelle sein |
| String                           | nein    | Name des Attributes fehlt                                                          |
| privat, public name: String      | nein    | Fehler wegen mehrfachen Zugriffsattributen                                         |

![Protected](http://www.plantuml.com/plantuml/png/LP3DIWCn58NtUOf3tIXYdR7rfm6b5bouhZjnaKakDfX9XkGEgCFuxCRICNHty_6T0pdhn2fotpgHFgplL5Gjj5CfFJ97s_HCpphuhVqcL69d3K7Rg1s9lOIdJzLsZWO1g2bGIrZFcPmNTZMn8R5YgqTqVUEiXeG9-IBSOp6lbzcuAAc5H1E28-KpFobyAP5s8J8o_YduMcARJ_-UosOlb7Y68P8omRklKQ28DnARrse-hzy67Tek9Cjq9Dp-Dnsq40r5Cd_IPT7kn2WEGsoE9bZjltiwlLTuG5sTPNjDtkInVm40)<!-- width="70%" --> [AttributeExample.plantUML](https://github.com/liaScript/CsharpCourse/blob/master/code/10_UML/Graphs/AttributeExample.plantUML)

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


**Operationen**

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


![Protected](http://www.plantuml.com/plantuml/png/HO_1IiH044JlynK5NhAAYzdLIq1O5G-UzoWUnfb66oSzeTC1DSHlTpQ1pQtegbT57thpQaERNF_5qddrBKBoVVzWflm-6Bs4B4V-9TouJuw4m1eIkJc4vs_VTeb61-0AOoap3XDoHrfKbhhQdAphzRAhM33lr0rO3FUusongLF8nI_gPJip5okmBamTRi1qQFCsjVslYjZhx-mP7KvOFlDRRxUKRngoHbE9KPXxTv0uaiJpy3m00)<!-- width="50%" --> [OperationsExample.plantUML](https://github.com/liaScript/CsharpCourse/blob/master/code/10_UML/Graphs/OperationsExample.plantUML)

```csharp    OperationsExample
using System;

namespace Rextester
{
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
}
```

#### Schnittstellen

Eine Schnittstelle wird ähnlich wie eine Klasse mit einem Rechteck dargestellt, zur Unterscheidung aber mit dem Schlüsselwort `interface` gekennzeichnet.

![Protected](http://www.plantuml.com/plantuml/png/NP1D3e9038NtFKMNke0J48BHZOc91p0kIgbnp30aCtNXZtSN5K9nsUzxRvffbPIYNbiFPzS8ieli1O7gf95OaJsbXAjXtBcaanlfklDUM5qNm0MLU28M_4InABOZA4iZfyPV17wrPvxFTzg2aNOrJCnaisp-a1q67IFTlWw6puu07u2uho_2UZYYU6abw8QKUfpSNHPBU44beUdFzmO0)<!-- width="30%" --> [PublicPrivate.plantUML](https://github.com/liaScript/CsharpCourse/blob/master/code/10_UML/Graphs/OperationsExample.plantUML)

Eine alternative Darstellung erfolgt in der LolliPop Notation, die die grafische Darstellung etwas entkoppelt.

![Loolipop](http://www.plantuml.com/plantuml/png/ZP0n3e9044NxESMKK7W22Q7GmaHZOMaibkK3gykIx0wDnhDu62_cOg8anC9fNzwRoHH1b9UXizIQ2goDrnPCnWbyhJJuq7iny8Aj2GBEiiq7vVcDE0wCgmSqS9oie-TLmqYNRsHx1DtEoPr8MnK2hvJ0bSfTU2pjopEq74yCYmvE8bN47CmLIJf9EpmVrIZ-9ytkJzB5jFON_EQ92hWgVkO5)<!-- width="80%" --> [lollipop.plantUML](https://github.com/liaScript/CsharpCourse/blob/master/code/10_UML/Graphs/lollipop.plantUML)


```csharp    OperationsExample
using System;

namespace Rextester
{

  interface Sortierliste{
    void einfuegen (Eintrag e);
    void loeschen (Eintrag e);
  }

  class Datenbank : SortierteListe
  {
    void einfuegen (Eintrag e) {//Implementierung};
    void loeschen (Eintrag e) {//Implementierung};
  }
}
```


#### Beziehungen

Die Möglichkeiten der Verknüpfung zwischen Klassen und Interfaces lassen sich wie folgt gliedern:

| Beziehung                             | Darstellung                                                                                                                                          | Bedeutung                                                                                  |
| ------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------------------------- | ------------------------------------------------------------------------------------------ |
| Generalisierung                       | <img src="http://www.plantuml.com/plantuml/png/LOv12W8n40Jlyuh-m7vW3x9NRu8l2CcO7KmcC9diHVsxgoZehMcgw5ww1_DH2wsI654i5gR25uStcEJLaSGukZIbM4BlEB7rfZS2D5IUSNOJpGHipVwQnrSmsO9VXoP-MNWd7RMopJYgSsRROVhXC4ttmsU95jQqNtm2" width="228"> | gerichtete Beziehung zwischen einer generelleren und einer spezielleren Klasse (Vererbung) |
| Assoziationen (ohne Anpassung)        | <img src="http://www.plantuml.com/plantuml/png/NOun3i9030Hxls8_a0-aIa57oGkEEvDOScp9vz2W_9q0ee1oLpEhdNgMo6rLMgGngaHL9kLR8XqXyv9e4bnJt8YgbjvnTaj_461bvH6N3B7vYBreyn4dtwxAj7_fpHg_QDmz_WnOgSjFjnEzvp4C7vCcjdOSV080"width="228">          | beschreiben die Verknüpfung allgemein                                                      |
| Assoziationen (navigierbar)  | <img src="http://www.plantuml.com/plantuml/png/1S713SGW30J0lwjm0xHmuZf82n5iWYM39RFqdvcwUIVFT1dMeSsoESJnK1cQzWvgjqIZnHAva3kMblBJ_s58nUfd-WS0" width="128">                 | beschreiben die Verknüpfung allgemein                                                                                           |
| Assoziation (Komposition/Aggregation) | <img src="https://upload.wikimedia.org/wikipedia/commons/thumb/f/f8/Komposition_Aggregation.svg/1920px-Komposition_Aggregation.svg.png"width="228">  | Bildet Beziehungen von einem Ganzen und seinen Teilen ab                                   |

## Anhang

**Referenzen**

[WikiUMLHistory] https://commons.wikimedia.org/w/index.php?curid=7892951, Autor GuidoZockoll, Mitarbeiter der oose.de Dienstleistungen für Innovative Informatik GmbH -
derivative work: File:OO-historie.svg : AxelScheithauer, oose.de Dienstleistungen für Innovative Informatik GmbH -
derivative work: Chris828 (talk) - File:Objektorientieren methoden historie.png and File:OO-historie.svg, CC BY-SA 3.0

[WikiUMLDiagrammTypes] https://upload.wikimedia.org/wikipedia/commons/thumb/d/da/UML-Diagrammhierarchie.svg/1200px-UML-Diagrammhierarchie.svg.png, Autor "Stkl"- derivative work: File: UML-Diagrammhierarchie.png: Sae1962, CC BY-SA 4.0

[WikiDoxygen] https://commons.wikimedia.org/w/index.php?curid=24966914, Doxygen-Beispielwebseite, Autor Der Messer - Eigenes Werk, CC BY-SA 3.0

[VisualParadigm] What is Object Diagram?, https://www.visual-paradigm.com/guide/uml-unified-modeling-language/what-is-object-diagram/, Autor unbekannt
