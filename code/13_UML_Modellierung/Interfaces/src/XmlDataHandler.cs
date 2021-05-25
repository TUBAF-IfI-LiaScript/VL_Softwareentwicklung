using System;

public class XmlCheckedWriter
{
    private XmlFileWriter _xmlWriter;
 
    public XmlCheckedWriter(XmlFileWriter xmlWriter)
    {
        _xmlWriter = xmlWriter;
    }
 
    public void Write(string data)
    {
        if (!String.IsNullOrEmpty(data))
        {
            _xmlWriter.WriteToFile(data);
        }
        else
        {
            Console.WriteLine("Data set is empty, no write operation!");
        }
    }
}