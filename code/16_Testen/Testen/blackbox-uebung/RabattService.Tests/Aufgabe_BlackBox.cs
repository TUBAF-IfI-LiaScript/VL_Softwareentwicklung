namespace RabattService.Tests;

using Xunit;

// ===========================================================================
//  ÜBUNG: BLACK-BOX-TESTING
// ===========================================================================
//
//  Sie kennen NUR die Spezifikation (siehe Aufgabenblatt / Doku in Rabatt.cs):
//
//     Bestellwert (Euro)        Rabatt
//     -----------------------------------
//        0,00 –   99,99          0 %
//      100,00 –  499,99          5 %
//      500,00 – 1999,99         10 %
//     ab 2000,00                15 %
//
//     negativer Bestellwert  ->  ArgumentOutOfRangeException
//
//  Den Quelltext von Rabatt.RabattInProzent(...) betrachten wir als
//  Black Box - wir testen ausschließlich gegen die Tabelle oben.
//
//  Vorgehen:
//   1) ÄQUIVALENZKLASSEN bilden: je Rabattstufe ein "typischer" Repräsentant
//      plus die ungültige Klasse (negativer Wert).
//   2) GRENZWERTANALYSE: an jedem Übergang die beiden direkt benachbarten
//      Werte testen (z. B. 99,99 und 100,00).
//
//  Führen Sie die Tests mit  `dotnet test`  aus. Mindestens einer wird
//  fehlschlagen - finden Sie heraus, an welcher Klassengrenze der Fehler
//  in der Implementierung steckt!
// ===========================================================================

public class Aufgabe_BlackBox
{
    // ----- 1. Äquivalenzklassen: ein Repräsentant je Stufe -----------------

    [Theory]
    [InlineData(50.00, 0)]      // Klasse "0 %"
    // TODO: je eine Zeile für die Klassen 5 %, 10 % und 15 % ergänzen
    // [InlineData(   ?,  5)]
    // [InlineData(   ?, 10)]
    // [InlineData(   ?, 15)]
    public void Rabatt_je_Aequivalenzklasse(decimal bestellwert, int erwartet)
    {
        var rabatt = Rabatt.RabattInProzent(bestellwert);
        Assert.Equal(erwartet, rabatt);
    }

    // ----- 2. Grenzwertanalyse: Werte direkt an den Übergängen -------------
    //
    //  Tipp: An jeder Grenze g testen Sie den größten Wert der unteren Klasse
    //  und den kleinsten Wert der oberen Klasse (z. B. 99.99 und 100.00).

    [Theory]
    [InlineData(99.99, 0)]
    [InlineData(100.00, 5)]
    // TODO: die Grenzen 500 und 2000 analog ergänzen
    // [InlineData( 499.99,  5)]
    // [InlineData( 500.00, 10)]
    // [InlineData(1999.99, 10)]
    // [InlineData(2000.00, 15)]
    public void Rabatt_an_den_Grenzen(decimal bestellwert, int erwartet)
    {
        var rabatt = Rabatt.RabattInProzent(bestellwert);
        Assert.Equal(erwartet, rabatt);
    }

    // ----- 3. Ungültige Eingabe (ungültige Äquivalenzklasse) ---------------

    [Fact]
    public void NegativerBestellwert_wirftException()
    {
        // TODO: prüfen, dass eine ArgumentOutOfRangeException geworfen wird.
        // Assert.Throws<...>( () => Rabatt.RabattInProzent(-1m) );
    }
}
