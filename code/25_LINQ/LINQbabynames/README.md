# LINQ babynames — verzögerte vs. materialisierte Auswertung

Dieses Beispiel beantwortet zweimal dieselbe Frage:

> *Was sind die zehn Namen von Jungen mit den höchsten Anteilen im gesamten
> Datenzeitraum (1880–2008)?*

Interessant ist nicht das Ergebnis (beide Varianten liefern dieselbe Top-10),
sondern **wie oft die Abfrage dafür tatsächlich durch alle ~258.000 Datensätze
läuft**. Sichtbar gemacht wird das über den Zähler `whereCallCount`, der bei
jeder Auswertung des `where`-Prädikats (`CountAnd(...)`) hochgezählt wird.

## Die zwei Achsen

Das Beispiel variiert bewusst **zwei Dinge gleichzeitig**. Für das Verständnis
ist entscheidend, sie auseinanderzuhalten:

| Achse | Variante A | Variante B | laufzeitrelevant? |
|-------|------------|------------|-------------------|
| **Syntax** | Abfragesyntax (`from … where … orderby … select`) | Methodensyntax (`.Where(…).OrderByDescending(…)`) | **nein** |
| **Ausführung** | `queryA.ElementAt(i)` in der Schleife | einmal `queryB.ToList()`, dann Index | **ja** |

Der gemessene Unterschied kommt **allein aus der Ausführung**, nicht aus der
Syntax. Der Compiler übersetzt die Abfragesyntax ohnehin in exakt dieselben
Methodenaufrufe — beide Queries sind semantisch identisch.

## Warum 2.580.000 vs. 258.000?

**Variante A** ruft in der Schleife zehnmal `queryA.ElementAt(i)` auf. `queryA`
ist eine **verzögerte** Abfrage: Sie speichert nur die *Vorschrift*, kein
Ergebnis. Jeder `ElementAt(i)`-Aufruf fordert einen **frischen** Durchlauf an —
filtert alle 258.000 Zeilen und sortiert sie erneut — nur um dann das i-te
Element herauszugreifen:

```
10 Durchläufe × 258.000 Zeilen = 2.580.000 where-Aufrufe
```

**Variante B** ruft **einmal** `ToList()` auf. Das ist ein *terminaler*
Operator: Er führt die Query genau einmal aus und legt das Ergebnis in einer
Liste ab. Der anschließende `top[i]`-Zugriff ist ein billiger Array-Zugriff —
keine erneute Filterung:

```
1 Durchlauf × 258.000 Zeilen = 258.000 where-Aufrufe
```

## Der eigentliche Lerneffekt

- **Syntax ist Geschmackssache**, keine Performancefrage. Abfrage- und
  Methodensyntax kompilieren zum selben Code.
- **Verzögerte Auswertung ist mächtig, aber man muss wissen, *wann* eine Query
  läuft.** Der wiederholte Zugriff auf eine verzögerte Query (`ElementAt` in
  einer Schleife, mehrfaches `foreach`, `.Count()` + `.First()` …) führt sie
  jedes Mal komplett neu aus.
- **`ToList()` / `ToArray()` materialisieren** das Ergebnis einmal und machen
  Folgezugriffe billig.

### Fußnote zu `OrderByDescending`

`OrderByDescending` ist ein **blockierender** Operator: Bevor das erste Element
geliefert werden kann, muss die *gesamte* Eingabe gelesen und sortiert werden.
Deshalb hilft es bei dieser Query auch nicht, statt `ElementAt(i)` den
`IEnumerator` zu speichern und weiterzuschieben — bereits der erste
`MoveNext()` zahlt die vollen 258.000 Filter-Aufrufe plus die komplette
Sortierung. „Iterator speichern und auslesen" landet also beim selben Wert wie
`ToList()` (258.000). Der Mehraufwand in Variante A entsteht einzig dadurch,
dass `ElementAt(i)` den Durchlauf **nicht** wiederverwendet, sondern jedes Mal
von vorne beginnt.

## Ausführen

```bash
dotnet run
```

Die Konsolenausgabe zeigt für beide Varianten die Top-10, die gemessene Zeit
und die Zahl der `where`-Aufrufe.
