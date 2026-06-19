# Black-Box-Testing — Live-Übung

Begleitübung zum Foliensatz 19 (Abschnitt *Black-Box-Testing / Spezifikationsorientiert*).
Die Studierenden leiten Testfälle **allein aus der Spezifikation** ab —
ohne den Quelltext des Übungsobjekts zu kennen.

## Übungsobjekt

`RabattService/Rabatt.cs` berechnet einen Mengenrabatt nach Bestellwert:

| Bestellwert (Euro) | Rabatt |
| ------------------ | ------ |
| 0,00 – 99,99       | 0 %    |
| 100,00 – 499,99    | 5 %    |
| 500,00 – 1999,99   | 10 %   |
| ab 2000,00         | 15 %   |
| negativer Wert     | `ArgumentOutOfRangeException` |

In der Implementierung steckt ein **Off-by-one-Fehler an der 2000-Grenze**
(`<= 2000m` statt `< 2000m`). Er ist *nicht* über die Äquivalenzklassen-
Repräsentanten sichtbar, sondern **nur** über die **Grenzwertanalyse** — genau
das ist die Pointe der Übung.

## Ablauf in der Vorlesung

1. **Spezifikation zeigen** (Tabelle oben), Quelltext geschlossen lassen.
2. Gemeinsam **Äquivalenzklassen** bilden (4 gültige + 1 ungültige) und die
   `[InlineData]`-Zeilen in `Rabatt_je_Aequivalenzklasse` ausfüllen.
   → alle grün. *„Sind wir fertig?“*
3. **Grenzwertanalyse** ergänzen (`Rabatt_an_den_Grenzen`): je Grenze die zwei
   benachbarten Werte. Beim Wert `2000.00` wird der Test **rot**.
4. Erst **jetzt** den Quelltext öffnen → der Black-Box-Test hat den Fehler
   gefunden, ohne die Interna zu kennen.

## Befehle

```bash
# Tests ausführen (Aufgabenstand)
dotnet test
```

Lösung vorführen:

```bash
# Musterlösung aktivieren, Aufgabe vorübergehend ausblenden
mv RabattService.Tests/Aufgabe_BlackBox.cs   RabattService.Tests/Aufgabe_BlackBox.cs.txt
mv RabattService.Tests/Loesung_BlackBox.cs.txt RabattService.Tests/Loesung_BlackBox.cs
dotnet test          # -> 1 Test schlägt fehl: (2000.00 -> 15 %)

# danach zurücksetzen
mv RabattService.Tests/Loesung_BlackBox.cs     RabattService.Tests/Loesung_BlackBox.cs.txt
mv RabattService.Tests/Aufgabe_BlackBox.cs.txt RabattService.Tests/Aufgabe_BlackBox.cs
```

> Bonus: Nach dem Fix (`< 2000m`) sind alle Tests grün — schöner Anknüpfungspunkt
> für „Test schreiben → Fehler reproduzieren → Fix → Test grün“ (vgl. TDD).
