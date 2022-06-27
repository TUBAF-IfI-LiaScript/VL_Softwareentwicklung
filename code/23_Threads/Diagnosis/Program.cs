using System;
using System.Threading;
using System.Diagnostics;
using static System.Diagnostics.Process;

public static class Recorder {

    static Stopwatch timer = new Stopwatch();
    static long bytesPhysicalBefore =  0;
    static long bytesVirtualBefore =  0;

    public static void Start(){
        GC.Collect();
        GC.WaitForPendingFinalizers();
        GC.Collect();

        bytesPhysicalBefore = GetCurrentProcess().WorkingSet64;
        bytesVirtualBefore = GetCurrentProcess().VirtualMemorySize64;
        timer.Restart();
    }

    public static void Stop(){
        timer.Stop();
        long bytesPhysicalAfter = GetCurrentProcess().WorkingSet64;
        long bytesVirtualAfter = GetCurrentProcess().VirtualMemorySize64;
        Console.WriteLine("{0:N0} physical bytes used.", bytesPhysicalAfter-bytesPhysicalBefore);
        Console.WriteLine("{0:N0} virtual bytes used.", bytesVirtualAfter-bytesVirtualBefore);
        Console.WriteLine("{0:N0} total milliseconds ellapsed.", timer.ElapsedMilliseconds );
    }
}

public class Program{
    public static void Main(string[] args){
        int [] numbers = Enumerable.Range(1, 50_000).ToArray();

        // Variante 1
        Console.WriteLine("Generating String with +");
        Recorder.Start();
        string s = "";
        for (int i=0; i < numbers.Length; i++){
            s+= numbers[i] + ", "; 
        }
        Recorder.Stop();

        // Variante 2
        Console.WriteLine("\nUsing String Builder");
        Recorder.Start();
        var builder = new System.Text.StringBuilder();
        for (int i=0; i < numbers.Length; i++){
            builder.Append(numbers[i]);
            builder.Append(", ");
        }
        Recorder.Stop();        
    }
}