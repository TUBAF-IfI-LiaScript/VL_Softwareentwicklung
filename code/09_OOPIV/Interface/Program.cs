using System;

class Program
{
    static void Main(string[] args)
    {
        // Phase 1
        // Wir legen zwei Objekte an, die Daten unerschiedlichen 
        // Formats in Dateien schreiben

        XmlFileWriter myXmlFileWriter = new XmlFileWriter ();
        myXmlFileWriter.SetName("DataFile.xml");
        myXmlFileWriter.WriteToFile("MessdatenMessdaten");

        IWriter Writer1 = new JsonFileWriter();
        //Writer1.SetName("DataFile1");  // keine Element des Interfaces!
        Writer1.WriteToFile("MessdatenMessdaten");

        // Phase 2 ("Closely coupled classes")
        // Wir wollen die Methoden abermals erweitern 
        // und die Daten vor dem Schreiben filtern
        // Dazu implementieren wir eine neue Klasse 
        WriteDataToXml xmlWriter = new WriteDataToXml(myXmlFileWriter);
        xmlWriter.Write("DatenDatenDaten");
        // Was stört? 
        // 1. Sobald wir "XmlFileWriter" anpassen, muss auch 
        //    FilteredDataToXML korrigiert werden 
        // 2. Wir schreiben eine Klasse für xml, eine für json usw. :-(

        // Phase 2 ("Losely coupled classes")
        // Wir ersetzen die "feste Integration" durch ein
        // Interface. Jede Klasse die dieses Interface bedient, kann dann
        // eingebundne werden.
        WriteData xmlOutput = new WriteData(myXmlFileWriter);
        xmlOutput.Write("DiesUndJenes");
        
        JsonFileWriter myJsonFileWriter = new JsonFileWriter();
        myJsonFileWriter.SetName("Data.json");
        WriteData jsonOutput = new WriteData(myJsonFileWriter);
        jsonOutput.Write("UndNochWasAnderes");

        // Ziel erreicht :-)
    }
}
