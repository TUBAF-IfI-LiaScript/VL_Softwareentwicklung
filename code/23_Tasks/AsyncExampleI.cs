// Example found at http://openbook.rheinwerk-verlag.de/visual_csharp_2012/1997_15_004.html

using System;
using System.Threading;
using System.Threading.Tasks;

namespace Rextester
{
    class Program {
      public static void Main(string[] args){
        DoSomethingAsync();
        for (int i = 0; i < 1000; i++)
          Console.Write(".");
        Console.ReadLine();
      }
      static async void DoSomethingAsync()
      {
        Console.Write("Start");
        Console.Write(await TestAsync());
      }

      static async Task<string> TestAsync()
      {
        await Task.Delay(20);
        return "Fertig";
      }
    }
}
