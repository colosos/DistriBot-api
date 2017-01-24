using System;
using System.Collections.Generic;
using System.Data.Services.Client;
using System.Linq;
using System.Net;
using System.Text;

namespace DistriBotAPI.AnomalyDetection
{
    public class ProgramOutput
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("THIS USAGE WILL SOON BE DEPRECATED! SEE ADMARKETPLACEAPI PROJECT FOR LATEST.");
            var url = "https://api.datamarket.azure.com/data.ashx/aml_labs/anomalydetection/v1/Score"; // marketplace
            var acitionUri = new Uri(url);

            var key = "bxAkzWYq8S2nijSLTcIBI4Wl+qoGnOKwm+5IW86vAmY"; // market place primary account key 

            var cred = new NetworkCredential("acanabarro@tangocode.com", key); // your Microsoft live Id here 
            var cache = new CredentialCache();
            cache.Add(acitionUri, "Basic", cred);

            DataServiceContext ctx = new DataServiceContext(acitionUri);
            ctx.Credentials = cache;

            // Your time series data. Format: datetime1=val1;datetime2=val2;.... 
            var series = "9/21/2014 11:05:00 AM=3;9/21/2014 11:10:00 AM=9.09;9/21/2014 11:15:00 AM=0;";

            // Example request 
            var query = ctx.Execute<ADResult>(acitionUri, "POST", true,
                            new BodyOperationParameter("data", series),
                            new BodyOperationParameter("params", "SpikeDetector.TukeyThresh=3; SpikeDetector.ZscoreThresh=3") // default configuration of spike detectors
                            );
            var resultTable = query.FirstOrDefault();
            var results = resultTable.GetADResults();
            //foreach (var row in results)
            //{
            //    Console.WriteLine(row);
            //}
            //Console.Read();
        }
    }
}