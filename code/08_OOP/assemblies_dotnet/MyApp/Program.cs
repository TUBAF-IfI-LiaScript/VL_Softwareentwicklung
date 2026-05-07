using System;
using Farm;

namespace Programm
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // 'FarmFacade' ist 'public' -> aus dieser Assembly nutzbar.
            var farm = new FarmFacade();
            farm.Register("Kitty");
            farm.Register("Wally", "Wuff");
            farm.Register("Berta", "Muuh");

            Console.WriteLine(farm.MorningCall());

            // -----------------------------------------------------------------
            // EXPERIMENT: Auskommentieren, um den Compilerfehler zu sehen.
            //
            //     var cat = new Animal("Kitty");
            //     ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
            //     CS0122: 'Farm.Animal' is inaccessible due to its protection level
            //
            // 'Animal' ist 'internal' -> aus MyApp.dll heraus unsichtbar,
            //                            obwohl der Quellcode in MyClass/Farmland.cs steht
            //                            und problemlos lesbar ist.
            //
            // Aufgabe: Ändern Sie in Farmland.cs 'internal struct Animal' auf
            // 'public struct Animal'. Beobachten Sie, wie der obige Aufruf
            // dann ohne Fehler kompiliert.
            // -----------------------------------------------------------------
        }
    }
}
