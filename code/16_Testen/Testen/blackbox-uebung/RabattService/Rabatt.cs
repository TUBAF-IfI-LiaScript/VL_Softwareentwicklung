namespace RabattService;

/// <summary>
/// Übungsobjekt für Black-Box-Testing.
///
/// Die SPEZIFIKATION (das, was die Studierenden kennen) lautet:
///
///   Berechne den prozentualen Mengenrabatt anhand des Bestellwerts:
///
///     Bestellwert (Euro)        Rabatt
///     -----------------------------------
///        0,00 –   99,99          0 %
///      100,00 –  499,99          5 %
///      500,00 – 1999,99         10 %
///     ab 2000,00                15 %
///
///   Ein negativer Bestellwert ist ungültig und führt zu einer
///   ArgumentOutOfRangeException.
///
/// Die INTERNA (der Code hier) kennen die Studierenden beim Black-Box-Test
/// NICHT. Sie leiten ihre Testfälle allein aus der Tabelle oben ab:
///   - eine Äquivalenzklasse je Rabattstufe (+ die ungültige Klasse < 0)
///   - die Grenzwerte an den Übergängen (99,99 / 100,00 ; 499,99 / 500,00 ; ...)
///
/// Achtung: In der Implementierung steckt ein typischer Off-by-one-Fehler
/// an einer Klassengrenze. Genau diesen soll die Grenzwertanalyse aufdecken.
/// </summary>
public static class Rabatt
{
    public static int RabattInProzent(decimal bestellwert)
    {
        if (bestellwert < 0)
            throw new ArgumentOutOfRangeException(
                nameof(bestellwert), "Der Bestellwert darf nicht negativ sein.");

        if (bestellwert < 100m) return 0;
        if (bestellwert < 500m) return 5;
        if (bestellwert <= 2000m) return 10;   // FEHLER: muss < 2000 sein!
                                               // dadurch erhält 2000,00 nur 10 % statt 15 %
        return 15;
    }
}
