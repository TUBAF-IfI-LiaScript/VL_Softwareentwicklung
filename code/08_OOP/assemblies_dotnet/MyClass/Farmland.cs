using System;
using System.Collections.Generic;

namespace Farm
{
    // 'internal' (Standardwert für Top-Level-Typen) bedeutet:
    // 'Animal' ist nur INNERHALB dieser Assembly (MyClass.dll) sichtbar.
    // Aus MyApp gibt es keinen Zugriff -> Compilerfehler CS0122.
    internal struct Animal
    {
        public string name;
        public string sound;

        public Animal(string name, string sound = "Miau")
        {
            this.name = name;
            this.sound = sound;
            //aksjfölasfj
        }

        public override string ToString() => $"Mein Name ist {name}, {sound}!";
    }

    // 'public' bedeutet:
    // 'FarmFacade' darf von ANDEREN Assemblies (z.B. MyApp) genutzt werden.
    // Sie wirkt als öffentliche Schnittstelle und kann intern auf 'Animal'
    // zugreifen, weil sie zur gleichen Assembly gehört.
    public class FarmFacade
    {
        private List<Animal> animals = new List<Animal>();

        public void Register(string name, string sound = "Miau")
        {
            animals.Add(new Animal(name, sound));   // ok - dieselbe Assembly
        }

        public string MorningCall()
        {
            var lines = new List<string>();
            foreach (var a in animals)
                lines.Add(a.ToString());            // 'Animal' bleibt verborgen,
            return string.Join("\n", lines);        //  nur Text geht nach außen.
        }
    }
}
