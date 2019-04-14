using System;

namespace Rextester
{
  public class Program
  {
    public static void Main(string[] args)
    {
       int ivalue = 33;
       double fvalue = 43.1231;
       Console.WriteLine($"{ivalue * 10000,20}");
       Console.WriteLine($"{ivalue * 10000,20:E}");
    }
  }
}


//Console.WriteLine($"{fvalue,20:F2}");
//Console.WriteLine($"{fvalue,-20:F2}");

//Console.WriteLine($"{ivalue < 5 ? ivalue.ToString() : "invalid"}");
//Console.WriteLine($"{(fvalue < 1000 ? fvalue : fvalue/1000):f2} {fvalue < 1000 ? "Euro" : "kEuro"}");
