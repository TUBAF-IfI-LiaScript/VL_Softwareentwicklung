using System;
namespace Calcualator
{
  class MainClass
  {
    static double Square(int num) => num * num;
    static double Reciprocal (int num) => 1f / num;

    static void Main(string[] args)
    {
      bool Error = false;
      double result = 0;
      int num = 0;
      if (args.Length == 2)
      {
        // Hier geht es weiter
        if (int.TryParse(args[1], out num)) {
          if (args[0]=="Square")
            result = Square(num);
          else if (args[0]=="Reciprocal")
            result = Reciprocal(num);
          else Error = true;
        }
        else Error = true;
      }
      else Error = true;

      if (Error)
      {
        Console.WriteLine("Please enter a function and a numeric argument.");
        Console.WriteLine("Usage: Square    <int> or\n       Reciprocal <int>");
      }
      else
      {
        Console.WriteLine("{0} Operation on {1} generates {2}", args[0], num, result );
      }
    }
  }
}
