using System;
using System.IO;

namespace Rextester
{
  public class Program
  {
    static void WriteMeasurementsToFile(int [] values, string filename)
    {
      try
      {
        using (var sw = new StreamWriter(filename))
        {
          for (int i = 0; i<3; i++)
            sw.WriteLine(values[i]);
        }
      }
      // Dateifehler level 1 - Falscher Ordner
      catch (DirectoryNotFoundException ex)
      {
        Console.WriteLine(ex);
      }
      catch (FileNotFoundException ex)
      {
        Console.WriteLine(ex);
      }
      // Dateifehler level 2 - Abstrakter Lese/Schreib-Fehler
      // Ausnahme der StreamWriter.WriteLine Methode
      catch (IOException ex)
      {
        Console.WriteLine(ex);
      }
      // Datenfehler
      catch (System.IndexOutOfRangeException ex)
      {
        Console.WriteLine(ex.Message);
        Console.WriteLine(ex.StackTrace);
        //throw new System.ArgumentOutOfRangeException("index parameter is out of range.", ex);
      }
    }

    static void Main(string[] args)
    {
      int []values = {1,2,3,4,5};
      string filename = @"./data/FileDoesNotExist";
      filename = null;
      try{
        WriteMeasurementsToFile(values, filename);
      }
      catch{
        Console.WriteLine("Oha, der Fehler wird bis ganz nach oben durchgereicht.");
      }
      finally{
        Console.WriteLine("Sichern der superwichtigen Daten auf einer alterniven Ausgabe!");
        foreach(int i in values) Console.Write("{0} ",i);
        Console.WriteLine();
      }
    }
  }
}
