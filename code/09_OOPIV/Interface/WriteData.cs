using System;

public class WriteData
{
    private IWriter _Writer;
 
    public WriteData(IWriter Writer)
    {
        _Writer = Writer;
    }
 
    public void Write(string data)
    {
        _Writer.WriteToFile(data);
    }
}