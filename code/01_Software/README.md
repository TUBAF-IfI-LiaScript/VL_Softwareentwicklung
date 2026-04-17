# Codebeispiel "AllesFalsch" – Bugs und überarbeitete Lösung

Dieser Ordner begleitet den Abschnitt *Qualitätsmängel erkennen* aus [01_Software.md](../../01_Software.md).

Die Lösung hat Nico Seidler (SoSe 2026) beigesteuert. Die überarbeitete Version korrigiert die beiden echten Bugs der Ursprungsfassung, beseitigt einige Qualitätsmängel (z. B. fehlende Fehlerprüfungen, ungenutzte Header) und verbessert die Wartbarkeit durch bessere Strukturierung und sprechende Bezeichner. Dennoch bleiben einige Schwächen (z. B. algorithmische Komplexität, Endlosschleife bei Fehleingaben) bestehen, um Diskussionsanlässe zu bieten.

| Datei                        | Inhalt                                                                 |
| ---------------------------- | ---------------------------------------------------------------------- |
| [withBugs.c](withBugs.c)     | Ursprungsfassung mit zahlreichen Qualitätsmängeln und zwei echten Bugs |
| [improved.c](improved.c)     | Überarbeitete Lösung (studentischer Beitrag, SoSe 2026)                |
| [numbers.txt](numbers.txt)   | Beispiel-Eingabedatei                                                  |

## Übersetzen und Ausführen

```bash
gcc -Wall -Wextra -O2 improved.c -o improved
./improved
# Eingabe des Pfads, z. B.: numbers.txt
```

## Stärken der überarbeiteten Lösung

Die Lösung adressiert systematisch die in der ISO-25010-Tabelle aufgelisteten Qualitätsmerkmale:

- **Funktionale Eignung** — Beide Bugs des Originals sind korrigiert:
  - äußere Sortierschleife startet bei `i = 0` (vorher fälschlich `i = 2` → Element 0 und 1 wurden nicht einbezogen),
  - innere Schleife läuft bis `j < n - i - 1` (vorher `j = l` → Zugriff hinter das Array-Ende).
- **Zuverlässigkeit** — `fopen` wird auf `NULL` geprüft, `fclose` und `free` werden konsequent aufgerufen, auch auf den Fehlerpfaden (z. B. `realloc`-Fail).
- **Leistungseffizienz (partiell)** — Sortierung *in-place* über Zeigerübergabe, keine Array-Kopie.
- **Wartbarkeit** — BubbleSort ist in eine eigene Funktion mit Doxygen-Kopf ausgelagert; sprechende Bezeichner (`pfad`, `daten`, `kapazitaet`, `anzahl`).
- **Sicherheit** — `scanf("%255s", …)` mit expliziter Längenbegrenzung → kein Buffer Overflow beim Einlesen des Pfads.
- **Flexibilität** — Dateipfad wird zur Laufzeit abgefragt, Datenarray wächst dynamisch über `realloc`.
- **Robuster `realloc`-Umgang** — Rückgabewert wird in einem Zwischenpointer (`optimiert`) aufgefangen, damit `daten` bei Allokationsfehler nicht verloren geht.

## Offene Punkte / Diskussionsanlässe

Nicht alle Schwächen des Originals sind vollständig behoben. Die folgenden Punkte eignen sich gut für die Plenumsdiskussion:

1. **Endlosschleife bei wiederholter Fehleingabe**
   Die `while (datei == NULL)`-Schleife besitzt kein Abbruchkriterium. Bei permanent falschen Eingaben läuft das Programm unbegrenzt.
   *Verbesserung:* Maximale Anzahl Versuche oder Abbruch bei `EOF`.

2. **Algorithmische Komplexität weiterhin O(n²)**
   Der Mangel "quadratischer Aufwand der Sortierung" aus der Qualitätstabelle wird nicht behoben. 
   *Verbesserung:* `swapped`-Flag für Early-Exit bei sortierten Eingaben, oder Verwendung von `qsort` aus `<stdlib.h>`.

3. **Magic Numbers**
   Die Werte `256` (Pfadlänge) und `10` (Startkapazität) stehen im Code ohne Benennung.
   *Verbesserung:* `#define` bzw. `PATH_MAX` aus `<limits.h>`.

4. **Keine Validierung der Datei-Inhalte**
   `fscanf` bricht beim ersten Nicht-Integer still ab; nachfolgende gültige Zahlen werden ignoriert, ohne Warnung.
   *Verbesserung:* Ursache des Abbruchs prüfen (`feof` vs. Parse-Fehler) und melden.

5. **Sonderfall leere Datei**
   Bei `anzahl == 0` wird trotzdem "Sortierte Ergebnisse:" ausgegeben.
   *Verbesserung:* explizite Meldung "Keine Daten eingelesen".

6. **Pfade mit Leerzeichen**
   `scanf("%255s", …)` liest nur bis zum ersten Whitespace.
   *Verbesserung:* `fgets` + Newline-Trimming.

## Bezug zur Vorlesung

Die Lösung zeigt exemplarisch, dass *Bug-Fix* und *Qualitätsverbesserung* zwei unterschiedliche Dimensionen sind: Die funktionalen Fehler sind behoben, einige nicht-funktionale Mängel (insbesondere Leistungseffizienz und vollständige Eingabevalidierung) bleiben jedoch bestehen. Genau diese Trennung ist ein zentrales Motiv des Abschnitts.
