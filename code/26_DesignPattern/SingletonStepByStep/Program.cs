using System;

namespace Rextester
{
  public class PrinterDriver{
    public void print(string text){
       Console.WriteLine("!PRINT {0}", text);
    }
  }

  public class Program {
    public static void Main(string[] args){
      PrinterDriver MyPrinter = new PrinterDriver();
      PrinterDriver FaultyPrinterInstance = new PrinterDriver();
      Console.WriteLine(MyPrinter.GetHashCode());
      Console.WriteLine(FaultyPrinterInstance.GetHashCode());
    }
  }
}