using System;
using System.Threading;

namespace Program
{
    class Printer{
      char ch;
      int sleepTime;

      public Printer(char c, int t){
        ch = c;
        sleepTime = t;
      }

      public void Print(){
        for (int i = 0; i<10;  i++){
          Console.Write(ch);
          Thread.Sleep(sleepTime);
        }
      }
    }

    class Program {
        public static void printThreadProperties(Thread currentThread){
          Console.WriteLine("{0} - {1} - {2}", currentThread.Name,
                                               currentThread.Priority,
                                               currentThread.IsBackground);
        }

        public static void Main(string[] args){
            Thread MainThread = Thread.CurrentThread;
            MainThread.Name = "MainThread";
            printThreadProperties(MainThread);
            Printer a = new Printer ('a', 10);
            Printer b = new Printer ('b', 50);
            Printer c = new Printer ('c', 70);

            Thread PrinterA = new Thread(new ThreadStart(a.Print));
            PrinterA.Name = "PrinterA  ";
            PrinterA.IsBackground = true;
            Thread PrinterB = new Thread(new ThreadStart(b.Print));
            PrinterB.Name = "PrinterB  ";
            printThreadProperties(PrinterA);
            printThreadProperties(PrinterB);
            PrinterA.Start();
            PrinterB.Start();
            c.Print();
            Console.WriteLine("Application finished");
        }
    }
}
