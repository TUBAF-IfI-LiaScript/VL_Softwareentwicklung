<!--

author:   Sebastian Zug, Galina Rudolf & André Dietrich
email:    sebastian.zug@informatik.tu-freiberg.de
version:  1.0.4
language: de
narrator: Deutsch Female
comment:  Fallbeispiel UML Modellierung
tags:      
logo:     
title: Modellierung von Software III

import: https://github.com/liascript/CodeRunner
        https://raw.githubusercontent.com/liascript-templates/plantUML/master/README.md
        https://raw.githubusercontent.com/liaTemplates/ExplainGit/master/README.md

import: https://raw.githubusercontent.com/TUBAF-IfI-LiaScript/VL_Softwareentwicklung/master/config.md

-->

[![LiaScript](https://raw.githubusercontent.com/LiaScript/LiaScript/master/badges/course.svg)](https://liascript.github.io/course/?https://github.com/TUBAF-IfI-LiaScript/VL_Softwareentwicklung/blob/master/17_UML_ModellierungIII.md)

# Modellierung von Software

| Parameter                | Kursinformationen                                                                                   |
| ------------------------ | --------------------------------------------------------------------------------------------------- |
| **Veranstaltung:**       | `Vorlesung Softwareentwicklung`                                                                     |
| **Teil:**                | `15/27`                                                                                             |
| **Semester**             | @config.semester                                                                                    |
| **Hochschule:**          | @config.university                                                                                  |
| **Inhalte:**             | @comment                                                                                            |
| **Link auf den GitHub:** | https://github.com/TUBAF-IfI-LiaScript/VL_Softwareentwicklung/blob/master/17_UML_ModellierungIII.md |
| **Autoren**              | @author                                                                                             |

![](https://media.giphy.com/media/26tn33aiTi1jkl6H6/source.gif)

---------------------------------------------------------------------

## Beispielszenario UML-Modellierung

Das folgende Beispiel entspricht der Analyse, die auf der Webseite [Link](http://www.highscore.de/uml/titelseite.html) von Boris Schäling beschrieben wird (CC BY-NC-ND 3.0 DE)[^UMLTutorial].

Hier sind auch weitergehende Informationen und Designdiskussionen zu finden.  Die entsprechenden
Texte sind jeweils in kursiver Schrift gehalten, Ergänzungen für die Einbettung
in dieser Vorlesung entsprechend hervorgehoben. Ein zusätzliches Durcharbeiten der Originalunterlagen wird empfohlen.

Der Anforderungskatalog an den beispielhaft zu realisierenden Onlineshop gliedert sich wie folgt:

+ *Alle im Online-Shop erhältlichen Artikel besitzen eine eindeutige Artikelnummer, einen Namen und einen Preis. Außerdem sollen Artikel aus dem Angebot entfernt werden können, ohne ihre Daten aus dem System zu löschen.*

+ *Während des Bestellvorgangs geben Kunden eine Rechnungs- und Lieferanschrift ein. Grundsätzlich wird zwischen diesen beiden Anschriften nicht unterschieden. Wenn ein Kunde es verlangt, kann er jedoch unterschiedliche Adressen für Rechnung und Lieferung angeben.*

+ *Die Rechnungs- und Lieferanschrift bestehen aus der Anrede, die Kunden aus einer Liste wählen, dem Vor- und Nachnamen, der Straße, Hausnummer, Postleitzahl und dem Ort. Da Bestellungen europaweit ausgeliefert werden, wählen Kunden außerdem ihr Land aus einer Liste aus. Für die Zustellung über einen Paketdienst ins Ausland ist es notwendig, dass Kunden auch ihre Telefonnummer angeben. Darüber hinaus müssen sie ihre Email-Adresse angeben, an die am Ende des Bestellvorgangs eine Bestätigungsmail geschickt wird.*

+ *Kunden dürfen wählen, auf welche Weise sie ihre Bestellung bezahlen möchten. Als Zahlungsmethoden stehen Nachnahme, Bankeinzug und Vorauskasse zur Verfügung. Je nach gewählter Zahlungsmethode fallen zusätzliche Kosten an. Außerdem muss beachtet werden, dass je nach Land, in das die Bestellung ausgeliefert werden soll, unterschiedlich hohe Kosten für Zahlungsmethoden anfallen: Zum Beispiel ist die Nachnahmegebühr im Inland geringer als die Nachnahmegebühr bei einem Versand ins Ausland.*

+ *Was für Kosten für Zahlungsmethoden gilt, trifft auch auf Verpackungs- und Versandkosten zu: Diese fallen je nach Land unterschiedlich hoch aus.*

+ *Es dürfen nur Bestellungen aus Ländern entgegengenommen werden, bei denen die Kosten von mindestens einer Zahlungsmethode bekannt sind. Ist also für ein Land nicht bekannt, was ein Versand per Nachnahme, Bankeinzug oder Vorauskasse an Kosten verursacht, darf für dieses Land keine Bestellung entgegengenommen werden. Dieses Land darf also nicht bei Eingabe einer Lieferanschrift ausgewählt werden können.*

+ *Es dürfen für eine Bestellung in ein Land nur die Zahlungsmethoden angeboten werden, für die die Kosten der Zahlungsmethoden für dieses Land bekannt sind. Ist zum Beispiel nicht bekannt, was ein Versand per Nachnahme nach Italien kosten würde, darf die Nachnahme als Zahlungsmethode für Bestellungen aus Italien natürlich nicht angeboten werden. Sind lediglich die Kosten für eine Zahlungsmethode bekannt, hat der Kunde keine Wahl und wird automatisch zu dieser Zahlungsmethode weitergeleitet.*

+ *Wenn der Wert einer Bestellung eine festgelegte Grenze überschreitet, werden keine Verpackungs- und Versandkosten in Rechnung gestellt.*

+ *Kundendaten müssen bei der Eingabe auf Vollständigkeit und Richtigkeit überprüft werden, soweit dies technisch möglich ist. Fehlen Daten oder sind Daten falsch angegeben, müssen verständliche Fehlermeldungen ausgegeben werden, die gleichzeitig erklären, wie der Fehler behoben werden kann.*

+ *In vielen Ländern, in die geliefert wird, gibt es einen Distributor, an den Bestellungen im Online-Shop weitergeleitet werden. Das muss auf sicherem Weg geschehen, da mit Kunden- und Bestelldaten natürlich verantwortungsvoll umgegangen wird. Gleichzeitig soll diese Weitergabe jedoch automatisiert sein, um den Aufwand zur Auslieferung von Bestellungen niedrig zu halten. Da diese Distributoren mit unterschiedlichen Softwaresystemen arbeiten, müssen Kunden- und Bestelldaten im jeweils richtigen Format weitergegeben werden.*

+ *Da es nicht für jedes Land, in das geliefert wird, einen dort ansässigen Distributor gibt, sind einige Distributoren für mehrere Länder verantwortlich.*

Nach welchen Aspekten lassen die Anforderungen des Kunden strukturieren:

+ Datenformate und -inhalte "Die Rechnungsadresse besteht aus Anrede ..."
+ Verhalten
     - Kundensicht - "Kunden dürfen wählen"
     - Betreibersicht - "Diese [Versandkosten] fallen ja nach Land unterschiedlich hoch aus"
+ Systemparameter - "Kunden- und Bestelldaten im jeweils richtigen Format weitergegeben" (Distributorspezifisches Exportformat)

[^UMLTutorial]: Boris Schäling, "Der moderne Softwareentwicklungsprozess mit UML, Kapitel 3: Das Aktivitätsdiagramm" http://www.highscore.de/uml/titelseite.html

**Womit beginnen?**

### Use-Case Diagramm

**Verwendung:**

- Anwendungsfalldiagramm visualisiert/spezifiziert funktionale Anforderungen an das Gesamtsystem aus der Sicht der Nutzer.
- Weitere Aufgabe des Anwendungsfalldiagramms ist Dokumentation.
- Anwendungsfalldiagramm bildet außerdem Basis für die Verständigung von Entwicklern, Endanwendern und Fachleuten für den Anwendungsbereich.

**Was wird dargestellt:**

- Benutzer eines Systems (Akteure)
- Anwendungsabläufe (Interaktionen von System und Nutzern)
- Zuerst Sunny-Day-Szenario

**Was wird nicht dargestellt:**

- Keine internen Abläufe, nur welche aus Sicht des Kunden
- Use-Case Diagramm zeigt keine Implementierung

**Was muss man dabei beachten:**

- Aktive Formulierungen (Subjekt, Prädikat, Objekt) nutzen
- Exakte, kurze Formulierung (kein sollte/müsste) verwenden

**Basisabläufe**

![UseCaseOnlineShopII](https://www.plantuml.com/plantuml/png/TOmnJWCn44Lxd-ANKEGE8QAZeA2Zcsp-sjREZbVsM0WGj-4OTBaO2oiXWKWtUk_DtpUNwdtMe1mTNb1pJ8vJhXmy5mjjohP8bHcB39D4FlYp4bzQB2g335ugxhgT6C8FnhrfDhPK5hm6005RWzqELpVLyqI5zFPqVazAk_xjhGsfvj71R977l_vHadQBRQQdih9_Hsx5QHZF7nK7lcmV5_OER4xz3kJOkE8r26zXlvvzray0)

Welche Elemente unserer Anforderungsliste werden in Bezug auf Anwendungsfälle
mit dieser  Darstellung nicht abgedeckt?

Gibt es weitere allgemeine Use-Cases u.a. welche mit einer indirekten Nutzerbeteiligung bzw. welche, die Unregelmäßigkeiten berücksichtigen (`include`, `extend`)

**Verfeinerung des Anwendungsfalldiagramms**

+ systeminterne Prüfung der Anschriften
+ Abbildung von Alternativen und unterschiedlichen Konsequenzen bei der Wahl  der Zahlungsmethode

![UseCaseOnlineShopII](https://www.plantuml.com/plantuml/png/RP11Jnin48NlyokUSaeEh0IzHQi80b6Hsa8bsWlgnUnETgquuyWUgos8VzEVmIr_h7RIXWNbPhzvCthVEuzgWgQr8yTBXNe4s_MArWPkr7gXkBAoCS6iiJ7DodHyfxyTeuoDT8x9DEe3FYLfcUWWurQS5Owzt-29WF55KBjY1vFYp-sVth5CO29Iv4iJ-NHdaTZqmUOSRANZXxUMCnJFbL4MBBSl0ND-DBrBqk6e_mEXF9lynFt5zUVlDn_llYo-NTzUBOfhTBzVZclMthUCVwag0avE25ZyKfPI0RvwAne9c1jPiPN7r8s8y1gigcR3z9kmVLcMCDEwjjAurF9iHW1XNrE-VzWTQkCvHsMtGp8FaRCiaoDBfqFj7HeHYLV6wD9BQqlyAPvUSwFZ7QS-LpzaZnWFYDCXOEdevep6x-vFK5TL7c2AkkQotsQs_C_g-7_p4w9dEi-Zj7P_0G00)

Was fehlt noch?

**Anwendungsfälle des Distributors**

![UseCaseOnlineShopIII](https://www.plantuml.com/plantuml/png/ZP6nJWCn343tV8L76DeFGAYgK65bPqiJ9sV9vrIE8mR4dtNhZv6xfLUXaswBu-VunJvMysfjPCUO3Ao0KXuC8Ya6eoBE1oiA9wgcT6xzxAQCbym8dy8aM8To-N6jOl0LuUchPQMKwXaxE1Zb9XbkrnvUz7PDACztzfs4IyuFTszO3PcZDnI8vRiJFZuGfpb5OtUT6_JWe-Ze3QQUP7FzLB711LxtIuqN80dS8hfL3zWVjYlSt_jrYnd2RdTO_-a_SZcd8qhi-_e3)

- Welche Zusammenhänge konnten wir bisher nicht abbilden?
- Wie kann man die Abläufe weiter verfeinern?

### Aktivitätsdiagramm vs. Sequenzdiagramm

| Kriterium                | Aktivitätsdiagramm                        | Sequenzdiagramm                             |
|--------------------------|-------------------------------------------|---------------------------------------------|
| **Fokus**                | Ablauf von Aktivitäten (Logik)            | Nachrichtenfluss zwischen Objekten          |
| **Perspektive**          | Workflow-orientiert                       | Interaktionsorientiert                      |
| **Verwendung bei Use Cases** | Visualisierung kompletter Abläufe     | Ausarbeitung spezifischer Szenarien         |
| **Vorteil**              | Übersicht & Verzweigungen                 | Kommunikation & zeitliche Ordnung           |


### Aktivitätsdiagramme

Die Aktivitäten rund um die Eingabe der Anschrift lassen sich im einfachsten
Fall als Sequenz Eingaben verstehen. Unter UML 1 könnte man das folgendermaßen
darstellen:

| Variante I | Variante II |
| ---------- | ----------- |
|    ![UseCaseOnlineShopIII](https://www.plantuml.com/plantuml/png/PSzD2i8m40NWVKuHkimHTDFk54G5mVtOFfj2CfN9547nB6wyIIzcK3V1vNppmxmwAObECHX1gyHzWAsSQpYrNeQpMWpSumIsQ-Ugk_cCcuLjMD31qfIkyyi7y3BZSHIc2FujVN5BhV_NWmhDJnIsojTuYX5Iy7vC6Z3eZNme6ZlHejuGTyL7ISUBUPWQtEM4Rm00)<!-- width="100%" -->            | ![UseCaseOnlineShopIII](https://www.plantuml.com/plantuml/png/SoWkIImgAStDuIfAJIv9p4lFILLGqj9op2jEpYZAJ2jHICtCIqzDIirJyFJKqbAgvWAhvqhBByfMuB9ISCmhIar9LKWiBIu_l2GZ9pNFcai1B9UOdfYP1r8Hbb-KbvYRcWSp25E5Ns9o1W4bmIL5YOVdf-9MeAUdPysLcfS24GOwCh-0gvRB0IW2z1e0)<!-- width="100%" -->           |

UML 2 führt eine stärkere Differenzierung in die Aktivitätsdiagramme ein. Was sind
die Hauptmerkmale?

Leider Unterstützt *plantUML* die Features diese Feature noch nicht, so dass hier
auf eine Grafik des Originaltutorials zurückgegriffen wird:

![ActivityDiagram](./img/15_UML_III/aktivitaet_zugriffsberechtigungueberpruefen.gif "Aktivitätsdiagramm aus dem [^UMLTutorial]")

### Klassendiagramme

Das **Klassendiagramm** ist eines der zentralen Strukturdiagramme der UML und bildet das **Rückgrat der statischen Modellierung** in objektorientierten Systemen. 

Konzentrieren wir uns zunächst auf einzelne Aspekte der Modellierung, um darauf aufbauend das gesamte Diagramm zu entwerfen.

**Ausgangspunkt Angebot / Artikel**

*Angebote* sind Sammlungen von *Artikeln* die in einer Kategorie (Sportgerät, Kleidung, usw.) liegen. Wie fassen wir also die Datenanforderungen aus unserer Spezifikation zusammen?

Assoziationen und Eigenschaften sind äquivalent in der Darstellung, entsprechend repräsentieren die folgenden beiden Diagramme eine analoge Aussage.

| Variante I | Variante II |
| ---------- | ----------- |
|    ![ClassDiagramm](https://www.plantuml.com/plantuml/png/DOsnQYin44Nx-OfX-pNljIg27TAu2G53UgtsMXuiZS9er2JapzBbnnAMR3Vd39Tpx1xDHotKxw9lqMBbL6Bl9tUJPJZEgUb5ti7_gE3gx8rDyirv5KDoQKetMS59B1KGRMi-QSRIV2TpkQDy4FEyWuvQOO7FwiYmxVKSAm9Vt4Jp9buCqWC_jR4KREOLTlt-oakqm8-j5KIbJwA_7a__9vxd1_uzmk4mxJ4wGjTHomy0)<!-- width="100%" -->            | ![ClassDiagramm](https://www.plantuml.com/plantuml/png/NSknIWD14CRnVfvYk5Aue6qAoObD6X42DcAnT_TbCsHtLcPd6yKti_F5XEDI9Dt7_FwpgnxCMqvKpwAVmKBcAOLQ1tUJiJdsKz63V87lAU7ex8LDuibv5iFaKfHECeCdiIc1QFdcGIF6uj_4tFDWBcUaxMMyFsmsTn_qSudNCz4wkEyVcVVgYB0jTzole2rdM7VT7Tn4uvAzX8mRVIvjJ5ZqrI3rXfzAIGYw-1li-FSRns0TxQ1po-aV)<!-- width="100%" -->           |

![ClassDiagramm](https://www.plantuml.com/plantuml/png/VO-zYiCm381tFON9T3yuSAk78pbkkUKeBNHtOiKrjQqYArr2twnRNwo1UkXKJH_euqFgYbYMEKM8E8aIKXpyGPGBZAC4oZ16MuhgiyU1P06rpcVlNCyIZXZVJfPDvNVpGnJHveUqnKHdVEbTQhqk5j2LyfmIildBWXuPzBzDQ7R28NlG6yPGp2_DGqJGU-JfTlM3vQTiFLSrNuqYhRzNQDkFfjQXm-pMd-y0)<!-- width="45%" -->

**Integration des Warenkorbes**

Der Warenkorb bildet die Menge der bestellten Artikel ab, entsprechend wird hier eine 1:n-Beziehung zwischen der Klasse Warenkorb und der Klasse `BestellteArtikel` definiert. `BestellteArtikel` erbt dabei von Artikel und erweitert die Felder um den Eintrag Anzahl.

Zudem bilden wir in der Klasse Warenkorb unsere Anforderung nach einer Bestellumfang gebundenen Versandkostenhöhe ab. Es wird davon ausgegangen, dass der Kunde hier eine Landes- und Versandart und Zahlungsmethodenunabhängige Lösung wünscht. Daher besitzt die Klasse Warenkorb neben der Assoziation zur Klasse Artikel eine statische Eigenschaft KostenfreiAb (diese ist unterstrichen). Die Membervariable gibt einen Euro-Betrag an, ab dem für die Lieferung der bestellten Ware keine Verpackungs- und Versandkosten in Rechnung gestellt werden.

![](https://www.plantuml.com/plantuml/png/bP3FIiD048Vl-nH3JlunHQz1f2rUYY12GS-JP9eCtSpAxCH3QzxCcozcMHArr8kdtPdzVNmxCu-6QdtdNToolA9Y1vN761TcocLljAg2h7b7SEqyRGmiW7BJ6jIiL1a7SItN11LhvScvHCD26Yg3JPKQRydl7K0-5T9t7Ma1Ap5gIDFJ8tPKmDgKfKcD9oME1To7llIKEi-acy-FfiG3kWp14ueRHisIef7tHiS-sM7hVqHJ-WMLP1kqJB5zD3Ik1dW8qKWsIhmeVvalMnPYESC_Vty5U6mvB-0jS3s2sCV_WeDpRbmYP3SPvFaT72x5R9PD6tJZ2RVlUNuolFkUBx7PHNOQcPFKVUU_0000)<!-- width="100%" -->

![](https://www.plantuml.com/plantuml/png/bO-_JiD038TtFuNLYJ_S2qG8DOW58R4oEudRM7Lyai_7GCcxW-1Q2NNYjhFzVVvvMu8ywgAWl0kCZ2xxEN0Ivp4a6y2eL0hs7d9WFNqVkGIh1hVw4V3xfTu-QDRksOBtxB12Q-FzPqeNK7EzaRCTiWzDSYn18UEABxa4syvP-g5xodLG_fcudSLOx8f-DQzrMTXFI--G_eIuxjwpJ6UEQlaNjmASys5EwG4tDvlBIxPiKrtq2m00)<!-- width="70%" -->

Die Klasse Bestellung umfasst zwei private Eigenschaften vom Typ Anschrift: Lieferanschrift und Rechnungsanschrift. Dabei gibt es genau eine Rechnungsanschrift, eine Lieferanschrift ist aber nicht zwingend ist. Ist keine Lieferanschrift angegeben, wird der Warenkorb an die Rechnungsanschrift ausgeliefert.

Bestellung fasst jeweils ein Objekt vom Typ Warenkorb und eines vom Typ Anschrift zusammen. Anhand der Komposition wird deutlich, dass eine Bestellung ohne die anderen beiden Klassen nicht existieren kann.

Die Klasse Anschrift enthält zahlreiche private Eigenschaften vom Typ `string`. Bis auf die Telefonnummer sind alle Angaben Pflicht. Hinzu kommt ein Member vom Objekttyp Land, das neben der nationalen Zuordnung auch die Höhe der Versand- und Verpackungskosten umfasst.

![](https://www.plantuml.com/plantuml/png/RP6nRi9044Jx-ueLPIGZqhg8C4eJ1P4eHAI8KfpFQtpoiOVsraqGtvAFoOzbiS08Azrvni_ZcPidiIpTBfZKUjfRjZjmmQPKYB0lEy4d5sdjln5c9c0j817ORnk1oZCwyP5Cuoi4vza5DAQduSCoKXkvF1Y0xA0fujqHdcCIf9hH5sKEZx4h5JcUcGMg6K97snvQUgoHBINNiAybX-9o_9nDf_TV0pVeFLOGOuKvhFN_QNjIDf79xcwabNNDMDEJ_VsvaLvOBez3sj9IrGV1QlG1s20dTTk-v53lxEojQxLQ-iTzHrqsqciTl6iqSH5ZG4iQQQwRm5ts0DTTnyum0kGmWUmDjU0fRjXl0kRwJgR4gOYP8rLw-ty0)<!-- width="80%" -->

**Zahlungsmethoden**

Das Anforderungsset des Kunden beschreibt 3 Zahlungsmethoden für die Abrechnung der Bestellung: Bankeinzug, Nachnahme, Vorauskasse. Lediglich im Falle des Bankeinzuges müssen weitere Daten erhoben werden. Bitte beachten Sie, das die Basisklasse Zahlungsmethode als abstrakte Klasse definiert wurde, um zu vermeiden, dass davon (sinnlose) Instanzen gebildet werden können.

![](https://www.plantuml.com/plantuml/png/TOwnIiH048RxUOe1Eo5WBI9tKP0WN7DWOJVPZDabkxCoEzkS-cPslfY9edSSfFpd_-RdsnGZjPeYb2d8AoabT95AsPffwAjnvxBimu7n2YA_65f63QCt78Aoiv05V1WONL0N6U3d6riknZ5M6O7wOahEyBTv9h-SIsBpUU0tGBp01-w_FhrUutskVMJu-DyXcJZ8eOuBs3nciImr9PxP_MmeutBksktkDBVTtLzSfk1eWvVDBPuxm5d6c_UcmHqvB-JJIlsKbMgPPiaLuAWE6vQXflWD)<!-- width="60%" -->

**Und nun alles zusammen**

Was bisher fehlte, war der Distributor, der, wie in unserem Use-Case Diagramm modelliert, per Benutzername und Kennwort auf den Online-Shop zugreifen möchte, um Bestell- und Kundendaten herunterzuladen. Die Klasse Distributor ist entsprechend mit den Klassen Bestellung und Land verknüpft. Diese Assoziationen drücken aus, dass der Distributor auf beliebig viele Bestellungen zugreifen kann und für mindestens ein Land verantwortlich ist, in das er Bestellungen auszuliefern hat. Die Klasse Format umfasst die Methoden zur Generierung der Ausgabeformate für die Daten, da davon ausgegangen wird, die dass die weitere Bearbeitung mit alternativen Programmen realisiert wird.

![](https://www.plantuml.com/plantuml/png/bLB1RjGm4BtxAwmzGS6Lk4O8TLSL0b4j5KX5uZP99ecrpbWQZpcqxBVm8zpwOymgJaftjOVcz2RptlCRZw-I04iUWmdOYPLesVU3sDOpDk8ZcIP0IdPDFTPHveh5xp0y65SGvN54hZwJO8zit1P6hBuBU-gDksNqgRgEkGvet1roz_Ythu6hJOm1WSo-s6um2OOWkFXR_ToQxc48ixcoIzk-_-tSYQXu_G16saMkYyb34X3VxEsNg7pg6FswdaCT66y6J-Zf5KmsssSJlRNmBCJhwbVqxQA3d5wasvjodtRrRcMZSnyKN5vUhEmycZ7B0AMlpPDHpRLFmhxE4jakBWinKt_2-1Xv13adl05gpPDwEdfDgHdOTrBP6uyTCim__gTU756_OJEGDj8Jkwnc8UbgTBcCBIguYqmm7j0MLAcG5EiRWiS_gEWJv3GzCorkOvA0NWvtg-LN5htJnNVASZJBzFEd2hH88yeGzLoeap0qOgTen-sZUdfn9T-PhiwXINVgTeQm0TgXf_lScvDuozBD5LARn_0F)<!-- width="100%" -->

**Wie viele Klassendiagramme soll man erstellen?**

- Typischerweise wird **ein Übersichtsdiagramm** erstellt, das die wichtigsten Klassen und Beziehungen zeigt. Es verzichtet oft auf vollständige Methodensignaturen oder sekundäre Attribute, um die Komplexität gering zu halten.
- Zusätzlich sind **mehrere Detaildiagramme** evtl. sinnvoll, z. B. für einzelne Subsysteme oder Module, spezifische Use Cases, technische Komponenten.
- Ziel ist die **Balance zwischen Übersichtlichkeit und Detailtiefe**.

**Was ist die richtige Vorgehensweise: von Detaildiagrammen zum Übersichtsdiagramm oder umgekehrt?**

Abhängig vom Entwicklungsstand und Kontext:

1. **Top-down (vom Übersichtsdiagramm zu Detaildiagrammen):**
- Häufig in der frühen Analyse- oder Entwurfsphase
- Zuerst grobe Struktur, dann schrittweise Verfeinerung
- Gut geeignet bei unklaren Anforderungen oder großer Systemkomplexität

2. **Bottom-up (von detaillierten Klassen zu einem Übersichtsdiagramm):**
- Praktisch bei bereits existierendem Code oder wiederverwendbaren Komponenten
- Zuerst detaillierte Modellierung einzelner Bereiche, später Zusammenführung
- Nützlich bei agilen oder inkrementellen Projekten

**Wie kann es weiter gehen**

### Sequenzdiagramme

Sequenzdiagramme (und Aktivitätsdiagramme) sind gut geeignet zur Darstellung von Methodenabläufen.

In einem Sequenzdiagramm: 

- Wird zeitliche Abfolge explizit dargestellt.
- Es wird genau gezeigt, welche Objekte (Instanzen) an der Ausführung beteiligt sind. Interaktionen über mehrere Objekte hinweg werden damit deutlich.
- Methodenaufrufe und Rückgaben werden unterschiedlich dagestellt.
- Sequenzdiagramme können sowohl synchrone als auch asynchrone Nachrichten zeigen.
- Entwickler können anhand des Sequenzdiagramms den Aufbau und Ablauf einer Methode schnell erfassen, insbesondere bei komplexer Geschäftslogik (Unterstützung bei der Implementierung).

Aber: 
Für kontrollflussorientierte, parallele Abläufe oder frühe Anforderungsanalysen (keine Klassenstruktur vorhanden) eignen sich besser Aktivitätsdiagramme –  Sequenzdiagramme fokussieren stärker auf Objektinteraktionen und Nachrichtenaustausch.

| GebuehrenBerechnen | Klassendiagramm |
| ---------- | ----------- |
|    ![SequenzDiagramm](https://www.plantuml.com/plantuml/png/RSkn2i8m48VnlKyHRiyB197QnKLmTulDNwzmkKhkylwYYrGwVl3xHlFSl5SPA5rL5eNn-g8GZu7I17E8T7rIIE6CUmStw8I6cHKwRCdkuxVCdx8AvxLWxpcfpToMXzFtk6GjV-mGuyQ57noXfLTv0m00)<!-- width="100%" -->            | ![ClassDiagramm](https://www.plantuml.com/plantuml/png/HK-nhiCm2Dpz5OpljSeFPAZIg5ANTkdOpR59R8bWoSPJrNylLXI50T1nTy1Gn6QfAnd2MK0IvBW40HypEec9pOX5aAZo0gUkky2LYmivpjB32rrW7HIxOyQHBGKEpSU0nlcEKyMGaKVCJOJyzz_VKfq8DyA2yhQ1cjaJ9IIFIjYHTKFCTxC_Jb3tZYpx_g79j3NIEfMselm1sUjpFm00)<!-- width="100%" -->           |

[^UMLTutorial]: Boris Schäling, "Der moderne Softwareentwicklungsprozess mit UML, Kapitel 3: Das Aktivitätsdiagramm" http://www.highscore.de/uml/titelseite.html
