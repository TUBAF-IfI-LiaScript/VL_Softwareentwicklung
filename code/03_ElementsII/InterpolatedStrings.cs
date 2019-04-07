using System;

namespace MyNamespace
{
  public class Program
  {
    public static void Main(string[] args)
    {
      double a = 5.0, b = 3;
      // Anstatt auf Indizes zu bauen, wie in anderen Sprachen
      Console.WriteLine("Das Ergebnis von {0} + {1} = {2}", a, b, a+b);
      // Unmittelbare Einbettung
      Console.WriteLine($"Das Ergebnis von {a} + {b} = {a + b}");
      Console.WriteLine($"Offenbar ist {a > b ? a : b} der größere Wert!");
      Console.WriteLine($@"\t Produktes {a * b, 7:F3} \n\tDifferenz {a + b, 7:F3}");
      Console.WriteLine("\nAus Maus!");
    }
  }
}
