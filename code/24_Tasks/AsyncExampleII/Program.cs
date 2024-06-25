using System;
using System.Net.Http;
using System.Threading.Tasks;

public class Program
{
    public static Task Main()
    {   Console.WriteLine("Beispiel mit Download");
        int n=await DownloadFileAsync();
        Console.WriteLine("Zurück in Main()");
        Console.WriteLine(n);
        Console.WriteLine("Download abgeschlossen!");
    }

    public static async Task<int> DownloadFileAsync()
    {
        using (var httpClient = new HttpClient())
        {
            Console.WriteLine("Starte den Download...");
            var url = "https://github.com/TUBAF-IfI-LiaScript/VL_Softwareentwicklung/blob/master/24_Tasks.md";
            var response = await httpClient.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            //Console.WriteLine("Datei heruntergeladen: " + content);
            return content.Length;
        }
    }
}
