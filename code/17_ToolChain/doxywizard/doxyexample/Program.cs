using System;

namespace doxyexample
{
  /// <summary>
  ///  KLasse mit dem Einsprungspunkt für die Main zu Testzwecken.
  /// </summary>
  public class Program
  {
  // Divides an integer by another and returns the result
  // Codebeispiel aus der Microsoft Dokumentation
  // siehe https://docs.microsoft.com/de-de/dotnet/csharp/codedoc

  /// <summary>
  /// Divides an integer <paramref name="a"/> by another integer <paramref name="b"/> and returns the result.
  /// </summary>
  /// <returns>
  /// The quotient of two integers.
  /// </returns>
  /// <example>
  /// <code>
  /// int c = Math.Divide(4, 5);
  /// if (c > 1)
  /// {
  ///     Console.WriteLine(c);
  /// }
  /// </code>
  /// </example>
  /// <exception cref="System.DivideByZeroException">Thrown when <paramref name="b"/> is equal to 0.</exception>
  /// See <see cref="Math.Divide(double, double)"/> to divide doubles.
  /// <seealso cref="Math.Add(int, int)"/>
  /// <seealso cref="Math.Subtract(int, int)"/>
  /// <seealso cref="Math.Multiply(int, int)"/>
  /// <param name="a">An integer dividend.</param>
  /// <param name="b">An integer divisor.</param>
  public static int intDivide(int a, int b)
  {
      return a / b;
  }
    /// <summary>
    ///  Main Funktion mit expemplarischer Berechung eines Divisionsergebnisses
    /// </summary>
    public static void Main(string[] args)
    {
      Console.WriteLine (intDivide(5,7));
    }
  }
}
