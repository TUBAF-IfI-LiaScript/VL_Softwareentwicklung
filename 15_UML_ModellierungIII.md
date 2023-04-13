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

[![LiaScript](https://raw.githubusercontent.com/LiaScript/LiaScript/master/badges/course.svg)](https://liascript.github.io/course/?https://github.com/TUBAF-IfI-LiaScript/VL_Softwareentwicklung/blob/master/15_UML_ModellierungIII.md)

# Modellierung von Software

| Parameter                | Kursinformationen                                                                                   |
| ------------------------ | --------------------------------------------------------------------------------------------------- |
| **Veranstaltung:**       | `Vorlesung Softwareentwicklung`                                                                     |
| **Teil:**                | `15/27`                                                                                             |
| **Semester**             | @config.semester                                                                                    |
| **Hochschule:**          | @config.university                                                                                  |
| **Inhalte:**             | @comment                                                                                            |
| **Link auf den GitHub:** | https://github.com/TUBAF-IfI-LiaScript/VL_Softwareentwicklung/blob/master/15_UML_ModellierungIII.md |
| **Autoren**              | @author                                                                                             |

![](https://media.giphy.com/media/26tn33aiTi1jkl6H6/source.gif)

---------------------------------------------------------------------

## Beispielszenario UML-Modellierung

Das folgende Beispiel entspricht der Analyse, die auf der Webseite [Link](http://www.highscore.de/uml/titelseite.html) von Boris Schäling beschrieben wird (CC BY-NC-ND 3.0 DE)[^UMLTutorial].

Hier sind auch weitergehende Informationen und Designdiskussionen zu finden.  Die entsprechenden
Texte sind jeweils in kursiver Schrift gehalten, Ergänzungen für die Einbettung
in dieser Vorlesung entsprechend hervorgehoben. Ein zusätzliches Durcharbeiten der Originalunterlagen wird empfohlen.

Der Anforderungskatalog an den beispielhaft zur realisierenden Onlineshop gliedert sich wie folgt:

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
+ Systemparameter

[^UMLTutorial]: Boris Schäling, "Der moderne Softwareentwicklungsprozess mit UML, Kapitel 3: Das Aktivitätsdiagramm" http://www.highscore.de/uml/titelseite.html

### Use-Case Diagramm

**Basisabläufe**

![UseCaseOnlineShopII](https://www.plantuml.com/plantuml/png/TOmnJWCn44Lxd-ANKEGE8QAZeA2Zcsp-sjREZbVsM0WGj-4OTBaO2oiXWKWtUk_DtpUNwdtMe1mTNb1pJ8vJhXmy5mjjohP8bHcB39D4FlYp4bzQB2g335ugxhgT6C8FnhrfDhPK5hm6005RWzqELpVLyqI5zFPqVazAk_xjhGsfvj71R977l_vHadQBRQQdih9_Hsx5QHZF7nK7lcmV5_OER4xz3kJOkE8r26zXlvvzray0)

Welche Elemente unserer Anforderungsliste werden in Bezug auf Anwendungsfälle
mit dieser  Darstellung nicht abgedeckt?

**Verfeinerung des Anwendungsfalldiagramms**

+ systeminterne Prüfung der Anschriften
+ Abbildung von Alternativen und unterschiedlichen Konsequenzen bei der Wahl  der Zahlungsmethode

![UseCaseOnlineShopII](https://www.plantuml.com/plantuml/png/RP11Jnin48NlyokUSaeEh0IzHQi80b6Hsa8bsWlgnUnETgquuyWUgos8VzEVmIr_h7RIXWNbPhzvCthVEuzgWgQr8yTBXNe4s_MArWPkr7gXkBAoCS6iiJ7DodHyfxyTeuoDT8x9DEe3FYLfcUWWurQS5Owzt-29WF55KBjY1vFYp-sVth5CO29Iv4iJ-NHdaTZqmUOSRANZXxUMCnJFbL4MBBSl0ND-DBrBqk6e_mEXF9lynFt5zUVlDn_llYo-NTzUBOfhTBzVZclMthUCVwag0avE25ZyKfPI0RvwAne9c1jPiPN7r8s8y1gigcR3z9kmVLcMCDEwjjAurF9iHW1XNrE-VzWTQkCvHsMtGp8FaRCiaoDBfqFj7HeHYLV6wD9BQqlyAPvUSwFZ7QS-LpzaZnWFYDCXOEdevep6x-vFK5TL7c2AkkQotsQs_C_g-7_p4w9dEi-Zj7P_0G00)

Was fehlt noch?

**Anwendungsfälle des Distributors**

![UseCaseOnlineShopIII](https://www.plantuml.com/plantuml/png/TOz1IyD048Nl-olUiHvIi4SffTRMYmU5uCNRDfcaGpUpONQMek8_y-OVCubG2-9juBtlySqwcLVBNN3foD9xzHrwNnrzIwMz1e9IPLxQG2vGOx08vVPWg7bCE8hpbKN5bfCfx_DmEtU6y2Y1XOho47kyEs2s8SdsHYg-8q6M6ay-fLDK0x6qj2UvWazuBdTtj-NkUcGUXXf9CRKtb8n2gBsHe8ud2T4F8IwXnG31uT89HN6U_3TPm3cSQvZFLvYzv5QvQTAUbAg3SPkbHQzVdyqeUeFbagKAkQRLwfTT_B3RIP8xA4ye2UwZTjumhh5Dd_oTSHzkJLBdBdm3)

Welche Zusammenhänge konnten wir bisher nicht abbilden?

### Aktivitätsdiagramme

Die Aktivitäten rund um die Eingabe der Anschrift lassen sich im einfachsten
Fall als Sequenz Eingaben verstehen. Unter UML 1 könnte man das folgendermaßen
darstellen:

| Variante I | Variante II |
| ---------- | ----------- |
|    ![UseCaseOnlineShopIII](https://www.plantuml.com/plantuml/png/PSzD2i8m40NWVKuHkimHTDFk54G5mVtOFfj2CfN9547nB6wyIIzcK3V1vNppmxmwAObECHX1gyHzWAsSQpYrNeQpMWpSumIsQ-Ugk_cCcuLjMD31qfIkyyi7y3BZSHIc2FujVN5BhV_NWmhDJnIsojTuYX5Iy7vC6Z3eZNme6ZlHejuGTyL7ISUBUPWQtEM4Rm00)<!-- width="100%" -->            | ![UseCaseOnlineShopIII](https://www.plantuml.com/plantuml/png/SoWkIImgAStDuIfAJIv9p4lFILLGqj9op2jEpYZAJ2jHICtCIqzDIirJyFJKqbAgvWAhvqhBByfMuB9ISCmhIar9LKWiBIu_l2GZ9pNFcai1B9UOdfYP1r8Hbb-KbvYRcWSp25E5Ns9o1W4bmIL5YOVdf-9MeAUdPysLcfS24GOwCh-0gvRB0IW2z1e0)<!-- width="100%" -->           |

UML 2 für eine stärkere Differenzierung in die Aktivitätsdiagramme ein. Was sind
die Hauptmerkmale?

Leider Unterstützt *plantUML* die Features diese Feature noch nicht, so dass hier
auf eine Grafik des Originaltutorials zurückgegriffen wird:

![ActivityDiagram](./img/15_UML_III/aktivitaet_zugriffsberechtigungueberpruefen.gif "Aktivitätsdiagramm aus dem [^UMLTutorial]")

### Klassendiagramme

Konzentrieren wir uns zunächst auf einzelne Aspekte der Modellierung, um darauf aufbauend die

das gesamte Diagramm zu entwerfen.

**Ausgangspunkt Angebot / Artikel**

*Angebote* sind Sammlungen von *Artikeln* die in einer Kategorie (Sportgerät, Kleidung, usw.) liegen. Wie fassen wir also die Datenanforderungen aus unserer Spezifikation zusammen?

Assoziationen und Eigenschaften sind äquivalent in der Darstellung, entsprechend repräsentieren die folgenden beiden Diagramme eine analoge Aussage.

| Variante I | Variante II |
| ---------- | ----------- |
|    ![ClassDiagramm](https://www.plantuml.com/plantuml/png/DOsnQYin44Nx-OfX-pNljIg27TAu2G53UgtsMXuiZS9er2JapzBbnnAMR3Vd39Tpx1xDHotKxw9lqMBbL6Bl9tUJPJZEgUb5ti7_gE3gx8rDyirv5KDoQKetMS59B1KGRMi-QSRIV2TpkQDy4FEyWuvQOO7FwiYmxVKSAm9Vt4Jp9buCqWC_jR4KREOLTlt-oakqm8-j5KIbJwA_7a__9vxd1_uzmk4mxJ4wGjTHomy0)<!-- width="100%" -->            | ![ClassDiagramm](https://www.plantuml.com/plantuml/png/NSknIWD14CRnVfvYk5Aue6qAoObD6X42DcAnT_TbCsHtLcPd6yKti_F5XEDI9Dt7_FwpgnxCMqvKpwAVmKBcAOLQ1tUJiJdsKz63V87lAU7ex8LDuibv5iFaKfHECeCdiIc1QFdcGIF6uj_4tFDWBcUaxMMyFsmsTn_qSudNCz4wkEyVcVVgYB0jTzole2rdM7VT7Tn4uvAzX8mRVIvjJ5ZqrI3rXfzAIGYw-1li-FSRns0TxQ1po-aV)<!-- width="100%" -->           |

**Integration des Warenkorbes**

Der Warenkorb bildet die Menge der bestellten Artikel ab, entsprechend wird hier eine 1:n-Beziehung zwischen der Klasse Warenkorb und der Klasse `BestellteArtikel` definiert. `BestellteArtikel` erbt dabei von Artikel und erweitert die Felder um den Eintrag Anzahl.

Zudem bilden wir in der Klasse Warenkorb unsere Anforderung nach einer Bestellumfang gebundenen Versandkostenhöhe ab. Es wird davon ausgegangen, dass der Kunde hier eine Landes- und Versandart und Zahlungsmethodenunabhängige Lösung wünscht. Daher besitzt die Klasse Warenkorb neben der Assoziation zur Klasse Artikel eine statische Eigenschaft KostenfreiAb (diese ist unterstrichen). Die Membervariable gibt einen Euro-Betrag an, ab dem für die Lieferung der bestellten Ware keine Verpackungs- und Versandkosten in Rechnung gestellt werden.

![](https://www.plantuml.com/plantuml/png/bP3FIiD048Vl-nH3JlunHQz1f2rUYY12GS-JP9eCtSpAxCH3QzxCcozcMHArr8kdtPdzVNmxCu-6QdtdNToolA9Y1vN761TcocLljAg2h7b7SEqyRGmiW7BJ6jIiL1a7SItN11LhvScvHCD26Yg3JPKQRydl7K0-5T9t7Ma1Ap5gIDFJ8tPKmDgKfKcD9oME1To7llIKEi-acy-FfiG3kWp14ueRHisIef7tHiS-sM7hVqHJ-WMLP1kqJB5zD3Ik1dW8qKWsIhmeVvalMnPYESC_Vty5U6mvB-0jS3s2sCV_WeDpRbmYP3SPvFaT72x5R9PD6tJZ2RVlUNuolFkUBx7PHNOQcPFKVUU_0000)<!-- width="100%" -->

Die Klasse Bestellung umfasst zwei private Eigenschaften vom Typ Anschrift: Lieferanschrift und Rechnungsanschrift. Dabei gibt es genau eine Rechnungsanschrift, eine Lieferanschrift ist aber nicht zwingend ist. Ist keine Lieferanschrift angegeben, wird der Warenkorb an die Rechnungsanschrift ausgeliefert.

Bestellung fasst jeweils ein Objekt vom Typ Warenkorb und eines vom Typ Anschrift zusammen. Anhand der Komposition wird deutlich, dass eine Bestellung ohne die anderen beiden Klassen nicht existieren kann.

Die Klasse Anschrift enthält zahlreiche private Eigenschaften vom Typ `string`. Bis auf die Telefonnummer sind alle Angaben Pflicht. Hinzu kommt ein Member vom Objekttyp Land, das neben der nationalen Zuordnung auch die Höhe der Versand- und Verpackungskosten umfasst.

![](https://www.plantuml.com/plantuml/png/RP6nRi9044Jx-ueLPIGZqhg8C4eJ1P4eHAI8KfpFQtpoiOVsraqGtvAFoOzbiS08Azrvni_ZcPidiIpTBfZKUjfRjZjmmQPKYB0lEy4d5sdjln5c9c0j817ORnk1oZCwyP5Cuoi4vza5DAQduSCoKXkvF1Y0xA0fujqHdcCIf9hH5sKEZx4h5JcUcGMg6K97snvQUgoHBINNiAybX-9o_9nDf_TV0pVeFLOGOuKvhFN_QNjIDf79xcwabNNDMDEJ_VsvaLvOBez3sj9IrGV1QlG1s20dTTk-v53lxEojQxLQ-iTzHrqsqciTl6iqSH5ZG4iQQQwRm5ts0DTTnyum0kGmWUmDjU0fRjXl0kRwJgR4gOYP8rLw-ty0)<!-- width="80%" -->

**Zahlungsmethoden**


Das Anforderungsset des Kunden beschreibt 3 Zahlungsmethoden für die Abrechnung der Bestellung: Bankeinzug, Nachnahme, Vorauskasse. Lediglich im Falle des Bankeinzuges müssen weitere Daten erhoben werden. Bitte beachten Sie, das die Basisklasse Zahlungsmethode als abstrakte Klasse definiert wurde, um zu vermeiden, dass davon (sinnlose) Instanzen gebildet werden können.

![](https://www.plantuml.com/plantuml/png/TOxFIWCn4CRlUOe1RyAMUYqYRHMaABqKF2WUPhFJPjXa8fF9fTety-QBwLfgfT1RliptvpSjXcerHLTwbZTKJE0ZbZ8pKswhqS9dMV6MOEmYhGqiW_8c67Iiv8spkC0TWMVraPpxJSESYb6CLJRl3g1vP5gJeXGVbDSsWTdX-J8UZQvUtSvXMqpHstuUdZ4CwPB8GkveQBc0Usg_FeDEu2xNDfBR_SsXzCIohVkvXooMfQP4FzwLASjc-5xCPyl_UhwuFSYXoXsXuRAvWLC6TwBX-gDfO8a-28PqvloK5MlfiPGpXYEwcv9qDSLl)<!-- width="60%" -->

**Und nun alles zusammen**

Was bisher fehlte, war der Distributor, der, wie in unserem Use-Case Diagramm modelliert, per Benutzername und Kennwort auf den Online-Shop zugreifen möchte, um Bestell- und Kundendaten herunterzuladen. Die Klasse Distributor ist entsprechend mit den Klassen Bestellung und Land verknüpft. Diese Assoziationen drücken aus, dass der Distributor auf beliebig viele Bestellungen zugreifen kann und für mindestens ein Land verantwortlich ist, in das er Bestellungen auszuliefern hat. Die Klasse Format umfasst die Methoden zur Generierung der Ausgabeformate für die Daten, da davon ausgegangen wird, die dass die weitere Bearbeitung mit alternativen Programmen realisiert wird.

![](https://www.plantuml.com/plantuml/png/RP51IyD048Nl-ok67WjRz2fIcYA281uK5BnDDcDtoMPMPcOlfJzUIsW3IQ_lUxmtZzcfeLWTijC-yIyA3X0ogZPcajfgz18AVwGZmPsBgIC8IK8cvx1L4mmssk4ROwvyq84ibexSgBuYTyxjIOto6SFQRm6-K8Zx8ksSDAmXIlenU80L-7SAaS-XUD5Nu2QFzzkneE43jMKK6h7KKpu7t6yswzMbmkX27hrVNZ1XVxg6pC-RRtp341dZGF_DfmK6zra4g_QeUjlXziWz9JxMmst-b7JyGf7hscdPd3uN6T0MyZJ2xOYxEkG_)<!-- width="100%" -->

[^UMLTutorial]: Boris Schäling, "Der moderne Softwareentwicklungsprozess mit UML, Kapitel 3: Das Aktivitätsdiagramm" http://www.highscore.de/uml/titelseite.html
