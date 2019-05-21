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

# Vorlesung Softwareentwicklung - 12 - Visual Studio P

--------------------------------------------------------------------
Link auf die aktuelle Vorlesung im Versionsmanagementsystem GitHub

https://github.com/liaScript/CsharpCourse/blob/master/13_Testen.md

Die interaktive Form ist unter diese Link zu finden ->
[LiaScript Vorlesung 13](https://liascript.github.io/course/?https://raw.githubusercontent.com/liaScript/CsharpCourse/master/13_Testen.md#1)

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

## 1. Motivation

+ Beim Kampfflugzeug F-16 brachte der Autopilot das Flugzeug in Rückenlage, wenn der Äquator überflogen wurde. Dies kam daher, dass man keine „negativen“ Breitengrade als Eingabedaten bedacht hatte. Dieser Fehler wurde sehr spät während der Entwicklung der F-16 anhand eines Simulators entdeckt und beseitigt.

+ 1999 verpasste die NASA-Sonde Mars Climate Orbiter den Landeanflug auf den Mars, weil die Programmierer unterschiedliche Maßsysteme verwendeten (ein Team verwendete das metrische und das andere das angloamerikanische) und beim Datenaustausch es so zu falschen Berechnungen kam. Eine Software wurde so programmiert, dass sie sich nicht an die vereinbarte Schnittstelle hielt, in der die metrische Einheit Newton × Sekunde festgelegt war. Die NASA verlor dadurch die Sonde

+ Zwischen 1985 und 1987 gab es mehrere Unfälle[8] mit dem medizinischen Bestrahlungsgerät Therac-25. Infolge einer Überdosis, die durch fehlerhafte Programmierung und fehlende Sicherungsmaßnahmen verursacht wurde, mussten Organe entfernt werden, und es verstarben drei Patienten.

+ Das Jahr-2000-Problem, auch als Millennium-Bug (zu deutsch „Millennium-Fehler“) oder Y2K-Bug bezeichnet, ist ein Computer­problem, das im Wesentlichen durch die Behandlung von Jahreszahlen als zweistellige Angabe innerhalb von Computersystemen entstanden ist.

**Welche Muster und Klassen erkennen Sie?**

Anforderungsfehler
Entwurfsfehler
Implementierungsfehler


**Welche Tests werden in das Projekt integriert?**

<!--
style="width: 100%; max-width: 560px; display: block; margin-left: auto; margin-right: auto;"
-->
````````````

         +------------------------------------------------------>   Zeit
         |
         |      Analyse                             Abnahmetest       KUNDE
         |          \                                   ^
         |           v                                 /          -.
         |        Grobentwurf                   Systemtests        |
         |             \                             ^             |
         |              v                           /              |
         |           Feinentwurf             Integrationstests     \  ENTWICKLER
 Detail- |                \                       ^                /
 grad    |                 v                     /                 |
         |             Implementierung  --> Modultests             |
         |                                                        -'
         v    
````````````

Der Modultest, auch Komponententest oder Unittest genannt, ist ein Test auf der Ebene der einzelnen Module der Software. Testgegenstand ist die Funktionalität innerhalb einzelner abgrenzbarer Teile der Software (Module, Programme oder Unterprogramme, Units oder Klassen). Testziel dieser häufig durch den Softwareentwickler selbst durchgeführten Tests ist der Nachweis der technischen Lauffähigkeit und korrekter fachlicher (Teil-) Ergebnisse.
Integrationstest

Der Integrationstest bzw. Interaktionstest testet die Zusammenarbeit voneinander abhängiger Komponenten. Der Testschwerpunkt liegt auf den Schnittstellen der beteiligten Komponenten und soll korrekte Ergebnisse über komplette Abläufe hinweg nachweisen.
Systemtest

Der Systemtest ist die Teststufe, bei der das gesamte System gegen die gesamten Anforderungen (funktionale und nicht-funktionale Anforderungen) getestet wird. Gewöhnlich findet der Test auf einer Testumgebung statt und wird mit Testdaten durchgeführt. Die Testumgebung soll die Produktivumgebung des Kunden simulieren, d. h. ihr möglichst ähnlich sein. In der Regel wird der Systemtest durch die realisierende Organisation durchgeführt.
Abnahmetest

Ein Abnahmetest, Verfahrenstest, Akzeptanztest oder auch User Acceptance Test (UAT) ist das Testen der gelieferten Software durch den Kunden. Der erfolgreiche Abschluss dieser Teststufe ist meist Voraussetzung für die rechtswirksame Übernahme der Software und deren Bezahlung. Dieser Test kann unter Umständen (z. B. bei neuen Anwendungen) bereits auf der Produktionsumgebung mit Kopien aus Echtdaten durchgeführt werden.



ALTER VON FEHLERN


Welcher Fehler ist der teuerste? Hier gibt es schaubilder, die die Kosten der Fehlerkorrektur beschreiben.

TODO: Bild suchen

## 2. Begriffe

### Definition



### Vollständiges Testen

Eine Klasse MyMathFunctions, die zwei Methoden implementiert, soll in Bezug
auf die beiden Methoden getestet werden.

```csharp
static class MyMathFunctions{
  //Fakultät der Zahl i
  public int fak(int i) {...}
  // Grösstergemeinsamer Teiler von i, j und k
  public int ggt(inti, intj, intk) {...}
}
```

Ein vollständiges Testen aller `int` Werte ($2^{31}$ bis $2^{31}-1$) bedeutet für die Funktion
`fak()` $2^{32}$ und für `ggt()` $2^{32} \cdot 2^{32} \cdot 2^{32}$ Kombinationen. Testen aller möglichen Eingaben ist damit nicht möglich. Für Variablen mit unbestimmtem Wertebereich (`string`) lässt sich nicht einmal die Menge der möglichen Kombinationen darstellen.


### Klassifikation Testmethoden

Herausforderung "Abdeckung"

**ManualTests**

**Unittests**

Before you write the code you should write the unit tests.


**Integrationstests**

**Systemtests**


Hinzu kommen noch die

**White Box vs. Black Box Testing**


**Statische Tests vs. Dynamsiche Tests**

https://de.wikipedia.org/wiki/Dynamisches_Software-Testverfahren#Zustandsbasierte_Testmethoden

### Metriken zur Testbewertung

> Black-Box-Testing ... Grundlage der Testfallentwicklung ist die Spezifikation
> des Moduls. Die Interna des Softwareelements sind nicht bekannt.

Die Güte der Testfälle ist definiert über die Abdeckung möglicher Kombinationen
der Eingangsparameter.

Für Black-Box-Testing existieren unterschiedliche Ausprägungen:

+ Äquivalenzklassenanalyse [Link](https://de.wikipedia.org/wiki/%C3%84quivalenzklassentest)
+ Grenzwertanalyse [Link](https://www.youtube.com/watch?v=GshMbff3mzw)
+ Ursache Wirkungsgraphen
+ Zustandsbasierte Testmethoden

Problematisch ist dabei, dass spezifische Lösungen, wie zum Beispiel in folgendem Fall

```csharp
static class MyMathFunctions{
  public int fak (int i){
    if ( i==1 ) return 0;
    elseif (i == 2) return 1;
    elseif  ... Ergebnisse für 3 und 4 ...
    elseif (i == 5) return 120;
    else return i * fak(i-1);  
  }
}
```

mit Testfällen `fak(5)==120`, `fak(6)==720` und `fak(10)==3628800` verborgen
bleiben. Die Fehler für `fak(1)` und `fak(2)` bleiben verborgen.

> White-Box-Testing ... beim „quelltextbasierten Testen“ sind die Interna des
> getesteten Softwareelements bekannt und werden zur Bestimmung der Testfälle
> verwendet

White-Box-Testing-Verfahrend zerlegen das Programm (statisch oder dynamisch)
entsprechend dem Kontrollfluß. Die Güte der Testfälle wird danach beurteilt,
wie groß der Anteil der abgedeckten Programmpfade ist. Die Bewertung kann dabei
anhand differenzierter Metriken erfolgen:

+ Anweisungsüberdeckung
+ Zweigüberdeckung
+ Pfadüberdeckung
+ ...

Anweisungsüberdeckung (auch $C_0$-Test genannt) zerlegt das Programm statisch in
seine Anweisungen und bestimmt den Anteil der in den Testfällen berücksichtigten
Anweisungen. Üblich ist eine Prüfung von 95%-100% aller Anweisungen durch als
$C-0$-Kriterium anzustreben:

$$
C_0 = \frac{Anzahl überdeckte Anzahl}{Gesamtanzahl der Anweisungen}
$$

```csharp
static class MyMathFunctions{
  public int fak (int i){                    // Anweisung
    if ( i==1 ) return 0;                    // 1
    elseif (i == 2) return 1;                // 2
    elseif  ... Ergebnisse für 3 und 4 ...   // 3 - 4
    elseif (i == 5) return 120;              // 5
    else return i * fak(i-1);                // 6
  }
}
```

Der oben genannten Black-Box-Test $i = {5, 6, 10}$ adressierte lediglich 2
der Anweisungen und generiert damit ein $C_0 = \frac{2}{6} = 0.33$. Mit dem Wissen
um die Codestruktur, kann der White-Box-Test sehr schnell den Nachweis erbringen,
dass das gezeigte Black-Box-Vorgehen nur unzureichend die Qualität des Codes
abprüft.

```csharp
static class MyMathFunctions{
  public int fak (int i){                    // Anweisung
    int [] facArray = new int [10];          // 1
    facArray[0] = 1;                         // 2
    facArray[1] = 1;        
    ...
    facArray[9] = 1;                         // 9
    // besser:
    // int [] facArray = new int[] { 1, 3, 5, 7, 9 };
    if ( i<10 ) return fakArray[i];          // 10 + 11
    else return i * fak(i-1);                // 12
  }
}
```

Mit dem Testfall $i = 1$ lassen sich hingegen vermeindlich $11/12 = 0.91$ der
Anweisungen abdecken, die Fehleinschätzung ist aber offensichtlich. Gleichwohl
sind die fest hinterlegten Werte aus Erfahrung heraus auch besonders anfällig
für Copy-&-Paste-Fehler.


**Reviews**  

statische Testmethode
Code aber auch UML Dastellungen

### Ablauf beim Testen


<!--
style="width: 100%; max-width: 560px; display: block; margin-left: auto; margin-right: auto;"
-->
````````````
                   +------------------------------------------------+
                   |                                                |
             +-----+-----+      +-----------+      +------------+   |    +------------+
          +->| Testfälle |-+ +->| Testdaten |-+ +->| Ergebnisse |-+ | +->| Protokolle |
          |  +-----------+ | |  +-----------+ | |  +------------+ | | |  +------------+
          |                | |                | |                 | | |                 
          |                | |                | |                 | | |                 
          |                | |                | |                 | | |                 
          |                v |                v |                 v v |               
  .---------------.  .---------------.  .--------------.  .---------------.           
  | Entwerfen der |->| Erstellen der |->| Testaus-     |->| Vergleich der |      
  | Testfälle     |  | Testdaten     |  | führung      |  | Ergebnisse    |         
  .---------------.  .---------------.  .--------------.  .---------------.      

````````````
[Somm01]




## 3. Und jetzt konkret!

... wie setzen wir das Ganze unter C# um? Welche Bibliotheken existieren dafür?


## 5. Beispiel der Woche



## Anhang

**Referenzen**

[Somm01]  Ian Sommerville: Software Engineering, Pearson Education, 6. Auflage, 2001

**Autoren**

Sebastian Zug, André Dietrich
