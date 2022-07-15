using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Text;
using System.Net;
using System.IO;

public class ThingSpeak
{
    private const string _url = "http://api.thingspeak.com/";
    private const string _APIKey = "YOUR_KEY_HERE";
    public static Boolean SendDataToThingSpeak(string field1, string field2, string field3, string field4, string field5, string field6, string field7, string field8, out Int16 TSResponse)
    {
        StringBuilder sbQS = new StringBuilder();
        // Build the querystring
        sbQS.Append(_url + "update?key=" + _APIKey);
        if (field1 != null) sbQS.Append("&field1=" + HttpUtility.UrlEncode(field1));
        if (field2 != null) sbQS.Append("&field2=" + HttpUtility.UrlEncode(field2));
        if (field3 != null) sbQS.Append("&field3=" + HttpUtility.UrlEncode(field3));
        if (field4 != null) sbQS.Append("&field4=" + HttpUtility.UrlEncode(field4));
        if (field5 != null) sbQS.Append("&field5=" + HttpUtility.UrlEncode(field5));
        if (field6 != null) sbQS.Append("&field6=" + HttpUtility.UrlEncode(field6));
        if (field7 != null) sbQS.Append("&field7=" + HttpUtility.UrlEncode(field7));
        if (field8 != null) sbQS.Append("&field8=" + HttpUtility.UrlEncode(field8));
        // The response will be a "0" if there is an error or the entry_id if > 0
        TSResponse = Convert.ToInt16(PostToThingSpeak(sbQS.ToString()));
        if (TSResponse > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private static string PostToThingSpeak(string QueryString)
    {
        StringBuilder sbResponse = new StringBuilder();
        byte[] buf = new byte[8192];
        // Hit the URL with the querystring and put the response in webResponse
        HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(QueryString);
        HttpWebResponse webResponse = (HttpWebResponse)myRequest.GetResponse();
        try
        {
            Stream myResponse = webResponse.GetResponseStream();
            int count = 0;
            // Read the response buffer and return
            do
            {
                count = myResponse.Read(buf, 0, buf.Length);
                if (count != 0)
                {
                    sbResponse.Append(Encoding.ASCII.GetString(buf, 0, count));
                }
            }
            while (count > 0);
            return sbResponse.ToString();
        }
        catch (WebException ex)
        {
            return "0";
        }
    }
}

// ToDo
// + Absichern des Passwortes 
// + Datenstruktur für die Speicherung 
// + Schreibmechanismus zum Server 
// + "Reset" der Daten
// + Aktualisierung der dotnet WebRequest API
// + Exception handling 
// + ...

class Program {         
     static void Main(string[] args)
     {
        Console.WriteLine("Los geht's");
        // var ts = new ThingSpeak("38KVQHK38XDKOBJY");
        // string[] content_names = {"Value X", "Value Y"};
        // Message msg = new (".net_TestChannel", content_names);
        // msg.SetValue("Value X", 220);
        // msg.SetValue("Value Y", 174);
        // Console.WriteLine(msg);
        // ts.SendDataToThingSpeak(msg);
        // Console.WriteLine(msg);
     }
 }
