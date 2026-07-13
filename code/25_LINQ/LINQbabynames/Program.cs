using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Microsoft.VisualBasic.FileIO;

class Program
{
    // Zaehler, der bei JEDEM Auswerten des where-Praedikats hochgezaehlt wird.
    // Damit machen wir sichtbar, wie oft die Query tatsaechlich durchlaufen wird.
    static int whereCallCount = 0;

    static void Main(string[] args)
    {
        var path = @"../babynames.csv";
        List<BabyName> namelist = new List<BabyName>();
        using (TextFieldParser csvReader = new TextFieldParser(path))
        {
            csvReader.CommentTokens = new string[] { "#" };
            csvReader.SetDelimiters(new string[] { "," });
            csvReader.HasFieldsEnclosedInQuotes = true;
            csvReader.ReadLine();

            while (!csvReader.EndOfData)
            {
                // Read current line fields, pointer moves to the next line.
                string[]? fields = csvReader.ReadFields();
                var newRecord = new BabyName(year_string: fields[0], name_string: fields[1],
                    percent_string: fields[2], gender_string: fields[3]);
                namelist.Add(newRecord);
            }
        }
        Console.WriteLine($"Eingelesen: {namelist.Count} Datensaetze (1880 - 2008)");

        // ----------------------------------------------------------------------
        // Dieselbe Frage - zweimal formuliert:
        // Was sind die zehn Namen von Jungen mit den hoechsten Anteilen im
        // gesamten Datenzeitraum?
        //
        // queryA nutzt die ABFRAGESYNTAX (from ... where ... orderby ... select),
        // queryB die dazu semantisch IDENTISCHE METHODENSYNTAX (.Where(...)
        // .OrderByDescending(...)). Der Compiler uebersetzt die Abfragesyntax
        // ohnehin in genau diese Methodenaufrufe. Beide filtern "boy" und liefern
        // dasselbe Ergebnis - sie unterscheiden sich NUR in der Schreibweise.
        //
        // Das where-Praedikat zaehlt ueber CountAnd(...) jeden Aufruf mit. So
        // koennen wir belegen, wie oft die Query wirklich ausgefuehrt wird.
        var queryA = from s in namelist
                     where CountAnd(s.gender == "boy")
                     orderby s.percent descending
                     select s;

        var queryB = namelist.Where(s => CountAnd(s.gender == "boy"))
                             .OrderByDescending(s => s.percent);

        // ======================================================================
        // VARIANTE A: Abfragesyntax + queryA.ElementAt(i) direkt in der Schleife
        // ----------------------------------------------------------------------
        // ACHTUNG: queryA ist eine VERZOEGERTE Abfrage - sie speichert nur die
        // Vorschrift. Jeder Aufruf von ElementAt(i) fuehrt sie KOMPLETT neu aus:
        // filtert alle 258.000 Zeilen und sortiert sie erneut. Zehn Durchlaeufe
        // = zehnmal die gesamte Arbeit.
        Console.WriteLine("\n=== Variante A: Abfragesyntax, ElementAt(i) in der Schleife ===");
        whereCallCount = 0;
        var swA = Stopwatch.StartNew();
        for (int i = 0; i < 10; i++)
        {
            var boys = queryA.ElementAt(i);
            Console.WriteLine("{0,2}. {1,-10} {2,4} {3:0.000}", i + 1, boys.name, boys.year, boys.percent);
        }
        swA.Stop();
        Console.WriteLine($"--> Zeit: {swA.ElapsedMilliseconds} ms, " +
                          $"where-Aufrufe: {whereCallCount:N0} (= {whereCallCount / namelist.Count}x durch alle Daten!)");

        // ======================================================================
        // VARIANTE B: Methodensyntax + die Query EINMAL materialisieren
        // ----------------------------------------------------------------------
        // queryB ist dieselbe Abfrage in Methodensyntax. ToList() ist ein
        // terminaler Operator: er fuehrt die Query genau einmal aus und legt das
        // Ergebnis in einer Liste ab. Der anschliessende Indexzugriff top[i] ist
        // ein billiger Array-Zugriff - keine erneute Auswertung mehr.
        Console.WriteLine("\n=== Variante B: Methodensyntax, einmal ToList(), dann Index ===");
        whereCallCount = 0;
        var swB = Stopwatch.StartNew();
        List<BabyName> top = queryB.ToList();          // <-- einmalige Auswertung
        for (int i = 0; i < 10; i++)
        {
            var boys = top[i];
            Console.WriteLine("{0,2}. {1,-10} {2,4} {3:0.000}", i + 1, boys.name, boys.year, boys.percent);
        }
        swB.Stop();
        Console.WriteLine($"--> Zeit: {swB.ElapsedMilliseconds} ms, " +
                          $"where-Aufrufe: {whereCallCount:N0} (= {whereCallCount / namelist.Count}x durch alle Daten)");

        // ----------------------------------------------------------------------
        Console.WriteLine("\nFazit 1 (Syntax): Abfrage- und Methodensyntax sind semantisch identisch -");
        Console.WriteLine("beide Varianten liefern exakt dieselbe Top-10-Liste.");
        Console.WriteLine("Fazit 2 (Ausfuehrung): A verrichtet die teure Filter- und Sortierarbeit");
        Console.WriteLine("zehnmal, B nur einmal. Verzoegerte Auswertung ist maechtig - man muss");
        Console.WriteLine("aber wissen, WANN eine Query laeuft.");
    }

    // Hilfsfunktion: reicht den Wahrheitswert des Praedikats durch und zaehlt
    // dabei jeden Aufruf. So wird jede Auswertung des where sichtbar.
    static bool CountAnd(bool predicate)
    {
        whereCallCount++;
        return predicate;
    }
}

class BabyName : IComparable
{
    // Readonly properties can only be set in the constructor.
    public int year { get; private set; }
    public string name { get; private set; }
    public double percent { get; private set; }
    public string gender { get; private set; }

    public BabyName(string year_string, string name_string,
                    string percent_string, string gender_string)
    {
        year = int.Parse(year_string);
        name = name_string;
        percent = double.Parse(percent_string);
        gender = gender_string;
    }

    public override string ToString()
    {
        return $"{this.year} - {this.name}";
    }

    public int CompareTo(object obj)
    {
        if (obj == null) return 1;
        BabyName otherName = obj as BabyName;
        return this.percent > otherName.percent ? 1 : -1;
    }
}
