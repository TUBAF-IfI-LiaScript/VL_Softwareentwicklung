using System;

public class WriteDataToXml
{
    // eigentlich wollen wir an dieser Stelle richtig 
    // variabel sein und Instanzen von beliebigen 
    // Writern hinterlegen können
    private XmlFileWriter _xmlWriter;
 
    public WriteDataToXml(XmlFileWriter xmlWriter)
    {
        _xmlWriter = xmlWriter;
    }
 
    public void Write(string data)
    {
        _xmlWriter.WriteToFile(data);
    }
}