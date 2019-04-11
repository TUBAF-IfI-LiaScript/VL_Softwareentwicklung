using System;

namespace Parameters
{
  public class Program
  {
    static void Add(out int sum, params int [] list)
    {
      sum = 0;
      foreach(int i in list) sum+=i;
    }

    public static void Main(string[] args){
      Add(out int sum, 3, 3, 5 , 6);
      Console.WriteLine(sum);
    }
  }
}
