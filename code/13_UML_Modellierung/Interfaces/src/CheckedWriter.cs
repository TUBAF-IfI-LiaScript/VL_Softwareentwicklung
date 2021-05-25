using System;

public class CheckedWriter
{
    private IWriter _Writer;
 
    public CheckedWriter(IWriter Writer)
    {
        _Writer = Writer;
    }
 
    public void Write(string data)
    {
        if (!String.IsNullOrEmpty(data))
        {
            _Writer.WriteToFile(data);
        }
        else
        {
            Console.WriteLine("Data set is empty, no write operation!");
        }
    }
}