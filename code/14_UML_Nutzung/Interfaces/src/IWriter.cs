using System;

// Interface definition
public interface IWriter
{
    void WriteToFile(string data);
}
 
// Abstract class definiton
public abstract class FileBase
{
    protected string name;
    public void SetName(string name = "DataFile.txt")
    {
        this.name = name;
    }
}
 
// XmlFileWriter - "implements" IWriter and "inherits" from FileBase
public class XmlFileWriter: FileBase, IWriter
{
    public void WriteToFile(string data)
    {
        Console.WriteLine($"Writing {data} as xml to {name}");
    }
}
 
public class JsonFileWriter: FileBase, IWriter
{
    public void WriteToFile(string data)
    {
        Console.WriteLine($"Writing {data} as json to {name}");    
    }
}