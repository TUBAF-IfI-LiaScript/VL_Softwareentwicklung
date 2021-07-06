// Example found at http://openbook.rheinwerk-verlag.de/visual_csharp_2012/1997_15_004.html

using System;
using System.Threading.Tasks;
using System.Threading;

namespace MyAsyncExample
{
    class Program {
      public static void Main(string[] args){
        DoSomethingAsync();
        for (int i = 0; i < 100; i++){
          Console.Write(".");
          Thread.Sleep(1);
        }
      }
      static async void DoSomethingAsync()
      {
        Console.Write("Start");
        Console.Write(await TestAsync());
      }

      static async Task<string> TestAsync()
      {
        await Task.Delay(5);
        return "Fertig";
      }
    }
}
