using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace sklepik.Services
{
    public class CurrencyService
    {
        public class Rate
        {
            public string no { get; set; }
            public string effectiveDate { get; set; }
            public string mid { get; set; }
        }

        public class NBPapi
        {
            public string table { get; set; }
            public string currency { get; set; }
            public string code { get; set; }
            public List<Rate> rates { get; set; }

        }


        public static decimal wylicz(string kurs)
        {
            if(kurs=="pln")
            {
                return 1;
            }
            WebRequest request = HttpWebRequest.Create(
                "http://api.nbp.pl/api/exchangerates/rates/A/" + kurs + "/?format=json");

            WebResponse response = request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());

            string jsonik = reader.ReadToEnd();

            NBPapi nbpapi = Newtonsoft.Json.JsonConvert.DeserializeObject<NBPapi>(jsonik);
            string usdCurr = nbpapi.rates[0].mid.ToString();

            return decimal.Parse(usdCurr);
        }

    }
}
