# Code-Analyse zum Anfassen (Vorlesung 22 - Events)

Dieses Mini-Projekt gehört zu **Hinweis 2** der Vorlesung *Events*:

> *Evaluieren Sie die Hinweise der Code Analyse sorgfältig, entwerfen Sie ggf.
> eigene Regeln.*

Im LiaScript-CodeRunner werden reine **Warnungen** bei erfolgreichem Build nicht
angezeigt (nur die Programmausgabe bzw. echte Fehler). Wer die Meldungen der
Code-Analyse selbst sehen will, baut dieses Projekt deshalb lokal.

## Schritt 1 - so wie es ist bauen

```bash
dotnet build
```

Erwartete Ausgabe (gekürzt):

```text
Program.cs(13,17): warning CS0169: The field 'Publisher.counter' is never used
    1 Warning(s)
    0 Error(s)
```

Der **Compiler** sieht nur "technisch Unbenutztes".

## Schritt 2 - die Code-Analyse einschalten

Kommentieren Sie in `CodeAnalysisDemo.csproj` die beiden Zeilen ein:

```xml
<EnableNETAnalyzers>true</EnableNETAnalyzers>
<AnalysisLevel>latest-all</AnalysisLevel>
```

und bauen Sie erneut:

```bash
dotnet build
```

Jetzt kommen zusätzlich **Analyzer-Regeln** (`CAxxxx`) hinzu, u.a.:

| Regel    | Aussage                                                                    |
| -------- | -------------------------------------------------------------------------- |
| `CA1823` | dieselbe Beobachtung wie CS0169, als Qualitätsregel formuliert             |
| `CA1030` | "Erwäge, `Raise` als Event auszudrücken" - ein event-spezifischer Hinweis  |

(Daneben erscheinen "strukturelle" Hinweise wie `CA1050`/`CA1515`, die zum
Namespace und zur Sichtbarkeit raten - im Kurskontext zweitrangig.)

> **Beobachtung:** Die Code-Analyse bewertet nicht nur technische Fehler,
> sondern auch **Entwurfsentscheidungen** (`CA1030` betrifft direkt unser
> Event-Thema).

## Schritt 3 - Warnungen zu Fehlern machen

Damit eine Regel beim Lernen nicht übersehen wird, lässt sie sich zum Fehler
hochstufen (so wird es auch in Vorlesung 10 demonstriert):

```bash
dotnet build -warnaserror:CS0169          # nur diese eine Regel
dotnet build -warnaserror                 # alle Warnungen
```

## Schritt 4 - eigene Regeln entwerfen (`.editorconfig`)

Über eine `.editorconfig` lassen sich einzelne Regeln gezielt verschärfen,
abschwächen oder abschalten - projektweit und ohne die `.csproj` zu ändern.

In diesem Ordner liegt eine fertige Beispielkonfiguration als
**`editorconfig.beispiel`**. Benennen Sie sie zum Ausprobieren in `.editorconfig`
um (nur dann wird sie wirksam) und bauen Sie mit aktivierten Schaltern (Schritt 2)
neu:

```bash
mv editorconfig.beispiel .editorconfig
dotnet build
```

Sie werden beobachten:

+ `CA1030` erscheint nicht mehr als Warnung (auf `suggestion` herabgestuft),
+ `CA1823` (unbenutztes Feld) wird zum **Fehler** und lässt den Build scheitern.

Alternativ direkt im Quelltext - für eine einzelne Stelle:

```csharp
#pragma warning disable CA1030
public void Raise() { ... }
#pragma warning restore CA1030
```
