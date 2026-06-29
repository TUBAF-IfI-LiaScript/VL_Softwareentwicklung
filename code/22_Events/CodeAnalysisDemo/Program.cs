using System;

// Diese Klasse enthält ABSICHTLICH kleine Schwächen, damit Compiler und
// Code-Analyse (Analyzer) etwas zu melden haben. Bauen Sie das Projekt mit
//   dotnet build
// und vergleichen Sie die Ausgabe, wenn Sie die Analyzer-Schalter in der
// CodeAnalysisDemo.csproj aus- bzw. einkommentieren.

public class Publisher
{
    public event EventHandler Changed;

    private int counter;            // wird nie verwendet -> CS0169 / CA1823

    // Eine Methode, die nichts anderes tut, als ein Event auszulösen.
    // Der Analyzer schlägt vor, das stattdessen als Event auszudrücken -> CA1030
    public void Raise()
    {
        Changed?.Invoke(this, EventArgs.Empty);
    }
}

public class Program
{
    public static void Main()
    {
        var p = new Publisher();
        p.Changed += (s, e) => Console.WriteLine("Event empfangen");
        p.Raise();
    }
}
