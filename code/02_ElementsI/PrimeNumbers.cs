using System;

namespace MyNamespace
{
  public class Program
  {
    public static void Main(string[] args)
    {
      for (int number = 0; number < 100; number ++)
      {
        bool prime = true;
        for (int i = 2; i <= number / 2; i++)
        {
          if(number % i == 0)
          {
            prime = false;
            break;
          }
        }
        if (prime == true) Console.Write("{0}, ", number);
      }
      Console.WriteLine("\nAus Maus!");
    }
  }
}
