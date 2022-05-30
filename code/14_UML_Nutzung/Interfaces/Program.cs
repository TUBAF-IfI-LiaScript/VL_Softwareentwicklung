using System;

class Program
{
    static void Main(string[] args)
    {
        // Phase 1
        // Wir legen zwei Objekte an, die Daten unterschiedlichen
        // Formats in Dateien schreiben

        XmlFileWriter xml_fw = new XmlFileWriter ();
        xml_fw.SetName("DataFile.xml");
        xml_fw.WriteToFile("MessdatenMessdaten");

        JsonFileWriter json_fw = new JsonFileWriter();
        json_fw.WriteToFile("MessdatenMessdaten");

        // Phase 2 ("Closely coupled classes")
        // Wir wollen die Methoden erweitern und die Daten vor dem Schreiben
        // filtern. Die neue Klasse implementiert eine erweiterte Write Methode
        XmlCheckedWriter xml_dh = new XmlCheckedWriter(xml_fw);
        xml_dh.Write("DatenDatenDaten");
        xml_dh.Write("");
        // Was stört?
        // 1. Sobald wir "XmlFileWriter" anpassen, muss auch
        //    XmlCheckedWriter korrigiert werden
        // 2. Wir schreiben eine Klasse für xml, eine für json usw. :-(

        // Wir ersetzen die "feste Integration" durch ein
        // Interface. Jede Klasse die dieses Interface bedient, kann dann
        // eingebunden werden.
        CheckedWriter dh = new CheckedWriter(xml_fw);
        dh.Write("DiesUndJenes");

        // Ziel erreicht :-)
    }
}
