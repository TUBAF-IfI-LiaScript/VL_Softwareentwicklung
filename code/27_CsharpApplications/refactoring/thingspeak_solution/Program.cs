using System.Web;
using System.Text;
using System.Net;
using System.Collections.Generic;

    public class Entry
    {
        public string fieldname;
        public string content;
        public double? value;
        public Entry (int index, string contentname)
        {
            this.fieldname = $"field{index}";
            this.content = contentname;
        }
    }

    public class Message
    {
        private List<Entry> entryList = new();
        public List<Entry> EntryList
        {
            get { return entryList; }  
        }
        public string channelName;
        public bool successfulSent;
        public Message(string channelName, string [] contentNames){
            this.channelName = channelName;
            int index = 0;
            foreach (var name in contentNames)
            {
                entryList.Add(new Entry(index, name));
                index++;
            }
        }

        public void SetValue(string contentName, double value)
        {
            foreach (var item in this.entryList)
            {
                if (item.content == contentName)
                {
                    item.value = value;
                    this.successfulSent = false;
                }
            }
        }

        public void ResetSuccessfulTransmittedValues()
        {
            if (successfulSent == true)
            {
                foreach (var item in this.entryList)
                {
                    item.value = null;
                }
            }
        }

        public override string ToString()
        {
            string output = $"{channelName} \n----------------\n";
            foreach (var item in entryList)
            {
                output += $"{item.fieldname} / {item.content} - {item.value}\n";
            }
            return output;
        }
    }

    public class ThingSpeak
    {
        private const string _url = "http://api.thingspeak.com/";
        private string _APIKey;
        public ThingSpeak(string APIKey)
        {
            _APIKey = APIKey;
        }

        public int SendDataToThingSpeak(Message msg)
        {
            StringBuilder sbQS = new StringBuilder();
            sbQS.Append(_url + "update?key=" + _APIKey);

            foreach (var entry in msg.EntryList)
            {
                if (entry.value != null)
                          sbQS.Append( $"&{entry.fieldname}=" +
                                       HttpUtility.UrlEncode(entry.value.ToString()));
            }
            Console.WriteLine(sbQS.ToString());

            Int16 TSResponse = Convert.ToInt16(PostToThingSpeak(sbQS.ToString()));
            
            if (TSResponse > 0) msg.successfulSent = true;
            msg.ResetSuccessfulTransmittedValues();
            return TSResponse;

        }
 
        private string PostToThingSpeak(string QueryString)
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

    class Program {         
        static void Main(string[] args)
        {
            var ts = new ThingSpeak("38KVQHK38XDKOBJY");
            string[] content_names = {"Value X", "Value Y"};
            Message msg = new (".net_TestChannel", content_names);
            msg.SetValue("Value X", 220);
            msg.SetValue("Value Y", 174);
            Console.WriteLine(msg);
            ts.SendDataToThingSpeak(msg);
            Console.WriteLine(msg);
        }
    }
