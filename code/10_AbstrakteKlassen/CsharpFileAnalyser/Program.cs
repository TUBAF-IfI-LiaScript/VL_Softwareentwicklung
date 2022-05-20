using System;
using System.IO;

public class CertificateEvaluator{
    private string fileName;
    public CertificateEvaluator(string fileName)
    {
        this.fileName = fileName;
    }
    public void RunEvaluation(string patter)
    {
        bool result = false;
        using (StreamReader file = File.OpenText(fileName))
        {
            string line = file.ReadLine();
            result = line.Contains(patter);
        }
        Console.Write("{0:-20} - ", fileName);
        if (result) Console.WriteLine($"references {patter}!");
        else Console.WriteLine("contains unknown certificate");
    }
}

public class RunCode
{
    public static void Main(string[] args)
    {
        string fileName = "./files/textfile_1.txt";
        const string pattern = "VL Softwareentwicklung";
        CertificateEvaluator CertProcessor = new CertificateEvaluator(fileName);
        CertProcessor.RunEvaluation(pattern);
    }
}