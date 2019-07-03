using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Globalization;

namespace BabyNames
{
    class BabyNames
    {
        public decimal year;
        public string name;
        public decimal percentage;
        public bool girl;
        public static BabyNames FromCsv(string csvLine)
        {
            string[] values = csvLine.Split(',');
            BabyNames BabyNamesEntry = new BabyNames();
            BabyNamesEntry.year = Convert.ToDecimal(values[0]);
            BabyNamesEntry.name = values[1];
            BabyNamesEntry.percentage = decimal.Parse(values[2], NumberStyles.Float);
            if (values[3].Trim('"') == "boy") BabyNamesEntry.girl = false;
            else BabyNamesEntry.girl=true;
            return BabyNamesEntry;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Einlesen gestartet ... ");
            List<BabyNames> BabyNamesList = File.ReadAllLines("baby-names.csv")
                                           .Skip(1)
                                           .Select(v => BabyNames.FromCsv(v))
                                           .ToList();
            Console.WriteLine(" beendet!");
            Console.WriteLine("{0} Datensätze gelesen.", BabyNamesList.Count);

            var query = from BabyNames baby in BabyNamesList
                         orderby baby.percentage descending
                         group baby by baby.girl;

            foreach (var group in query){
                if (group.First().girl==true) Console.WriteLine("Mädchennamen");
                else Console.WriteLine("Jungennamen");
                foreach (BabyNames name in group.Take(5)){
                    Console.WriteLine(" {0,-10} ({1}) {2}", name.name, name.year, name.percentage);
                }
            }   
        }
    }
}







// Wie waren die jeweilig am höchsten vergebenen Namen in den verschiedenen Jahren

// var query2 = from BabyNames baby in BabyNamesList
//              where baby.girl == true
//              group baby by baby.year;       
// foreach (var group in query2){
//     Console.Write(group.Key);
//     foreach (BabyNames name in group.Take(1)){
//         Console.WriteLine(" {0,-10} ({1})", name.name, name.percentage);
//     }
// } 
