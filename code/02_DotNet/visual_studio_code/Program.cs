using System;

namespace visual_studio_code
{
    class Program
    {
        static void Main(string[] args)
        {
            string text = "Hello World";
            Console.WriteLine(text);

            Console.WriteLine($"parameter count = {args.Length}");

            for (int i = 0; i < args.Length; i++)
            {
                Console.WriteLine($"Arg[{i}] = [{args[i]}]");
            }
        }
    }
}
