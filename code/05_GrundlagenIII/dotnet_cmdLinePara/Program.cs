using System;

public class Program
{
  static int Main(string[] args)
  {
    System.Console.WriteLine($"How many arguments are given? - {args.Length}");
    foreach (string argument in args)
    {
      System.Console.WriteLine(argument);
    }
    return 0;
  }
}