using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.VisualBasic.FileIO;

class Program
{
    static void Main(string[] args)
    {
        var path = @"../babynames.csv";
        List<BabyName> namelist = new List<BabyName>();
        using (TextFieldParser csvReader = new TextFieldParser(path))
        {
            csvReader.CommentTokens = new string[] { "#" };
            csvReader.SetDelimiters(new string[] { "," });
            csvReader.HasFieldsEnclosedInQuotes = true;
            csvReader.ReadLine();

            while (!csvReader.EndOfData)
            {
                // Read current line fields, pointer moves to the next line.
                string[]? fields = csvReader.ReadFields();
                var newRecord = new BabyName(year_string: fields[0], name_string: fields[1], 
                    percent_string: fields[2], gender_string: fields[3]);
                namelist.Add(newRecord);
            }
        }

       // Angabe mit LINQ Semantik
       // Was sind die zehn Namen von Jungen mit den höchsten Anteilen dem gesamten Datenzeitraum?
       var query = from s in namelist
                   where s.gender == "boy"
                   orderby s.percent descending
                   select s;

        // Beispiel mit Methoden Syntax
        // Was sind die zehn Namen von Mädchen mit den höchsten Anteilen dem gesamten Datenzeitraum?
        var query2 = namelist.Where(x => x.gender == "girl")
                             .OrderByDescending(x => x.percent);

       Console.WriteLine("\n\nTop 10 Jungen Namen              Top 10 Mädchen Namen");
       Console.WriteLine("----------------------           ----------------------");
       for (int i = 0; i < 10; i++){
         var boys = query.ElementAt(i);
         var girls = query2.ElementAt(i); 
         Console.Write("{0,-6} - {1,-10} {2:0.000}", boys.year, boys.name, boys.percent);
         Console.WriteLine("        {0,-6} - {1,-10} {2:0.000}", girls.year, girls.name, girls.percent);
       }
    }
}

class BabyName: IComparable
{
    // Readonly properties can only be set in the constructor.
    public int year { get; private set; }
    public string name { get; private set; }
    public double percent { get; private set; }
    public string gender { get; private set; }

    public BabyName(string year_string, string name_string, 
                    string percent_string, string gender_string)
    {
        year = int.Parse(year_string);
        name = name_string;
        percent = double.Parse(percent_string);
        gender = gender_string;
    }

    public override string ToString(){
	 	  return $"{this.year} - {this.name}";
	 }

    public int CompareTo(object obj){
        if (obj == null) return 1;
        BabyName otherName = obj as BabyName;
        return this.percent>otherName.percent ? 1 : -1;
  }
}