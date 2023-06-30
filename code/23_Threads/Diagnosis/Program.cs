using System.Text;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Exporters.Csv;

BenchmarkRunner.Run<StringsBenchmark>();

[InProcess()]
[MemoryDiagnoser]
[MinColumn, MaxColumn]
[CsvMeasurementsExporter, HtmlExporter, MarkdownExporterAttribute.GitHub]
[GcServer(true)]
public class StringsBenchmark
{
    [Params(10,100,1000)]
    public int Iterations;
    private const string TestString = "Hello TU Bergakademie Freiberg!";

    [Benchmark]
    public void StringConcat()
    {
        string result = string.Empty;
        for (int i = 0; i < Iterations; i++)
        {
            result += TestString;
        }
    }

    [Benchmark]
    public void StringBuilder()
    {
        var sb = new StringBuilder();
        for (int i = 0; i < Iterations; i++)
        {
            sb.Append(TestString);
        }
    }
}
