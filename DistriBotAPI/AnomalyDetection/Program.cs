using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace DistriBotAPI.AnomalyDetection
{
        internal class APIRequest
        {
            [JsonProperty(PropertyName = "data")]
            public string[,] Series { get; set; }

            [JsonProperty(PropertyName = "params")]
            public Dictionary<string, string> Parameters { get; set; }
        }

        class Program
        {
            // Example code to call Score API 
            public static void TestScoreAPI()
            {
                // 
                var api = "https://api.datamarket.azure.com/data.ashx/aml_labs/anomalydetection/v2/Score";
                var accountid = "acanabarro@tangocode.com";
                var accountKey = "bxAkzWYq8S2nijSLTcIBI4Wl+qoGnOKwm+5IW86vAmY"; // found under My Account -> Account Keys

                var apikey = Convert.ToBase64String(new UTF8Encoding().GetBytes(accountid + ":" + accountKey));

                var inputSeries = new string[,]
                                {
                            { "06/08/2016 11:10:00 AM", "9" },
                            { "06/08/2016 11:20:00 AM", "12" }
                                    // Add more data points here 
                                };

                var reqParams = new Dictionary<string, string>()
                            {
                                {"tspikedetector.sensitivity", "3"},
                                {"zspikedetector.sensitivity", "3"},
                                {"trenddetector.sensitivity","3"},
                                {"bileveldetector.sensitivity", "3"},
                                {"postprocess.tailRows", "1"}
                            };
                var request = new APIRequest
                {
                    Series = inputSeries,
                    Parameters = reqParams
                };

                using (var client = new WebClient())
                {
                    client.Headers[HttpRequestHeader.ContentType] = "application/json";
                    var authHeader = $"Basic {apikey}";
                    client.Headers.Add(HttpRequestHeader.Authorization, authHeader);

                    Console.WriteLine("Sending Request...\n " + JsonConvert.SerializeObject(request));
                    var jsonBytes = Encoding.Default.GetBytes(JsonConvert.SerializeObject(request));
                    var result = client.UploadData(new Uri(api), "POST", jsonBytes);

                    var adrecords = ScoreResult.Parse(Encoding.Default.GetString(result));

                    //foreach (var r in adrecords)
                    //    Console.WriteLine(r);
                    //Console.Read();

                }

            }

            // Example code to call ScoreWithSeasonality API
            public static void TestSeasonalityScoreAPI()
            {
                var api = "https://api.datamarket.azure.com/data.ashx/aml_labs/anomalydetection/v2/ScoreWithSeasonality";
            var accountid = "acanabarro@tangocode.com";
            var accountKey = "bxAkzWYq8S2nijSLTcIBI4Wl+qoGnOKwm+5IW86vAmY"; // found under My Account -> Account Keys
            var apikey = Convert.ToBase64String(new UTF8Encoding().GetBytes(accountid + ":" + accountKey));

                var inputSeries = new string[,]
                                {
                                {"06/08/2016 11:10:00 AM", "9" },
                                {"06/08/2016 11:20:00 AM", "12" }
                                    // Add more data points here 
                                };
                var reqParams = new Dictionary<string, string>()
                            {
                                { "preprocess.aggregationInterval","0" },
                                { "preprocess.aggregationFunc", "mean"},
                                {"preprocess.replaceMissing","lkv"},
                                {"postprocess.tailRows", "1"},
                                {"zspikedetector.sensitivity", "3"},
                                {"tspikedetector.sensitivity", "3"},
                                {"upleveldetector.sensitivity", "3.25"},
                                {"bileveldetector.sensitivity", "3.25"},
                                {"trenddetector.sensitivity", "3.25"},
                                {"detectors.historywindow", "500"},
                                {"seasonality.enable","true"},
                                {"seasonality.transform","deSeasonTrend"},
                                {"seasonality.numSeasonality", "1"}
                            };

                var request = new APIRequest
                {
                    Series = inputSeries,
                    Parameters = reqParams
                };

                using (var client = new WebClient())
                {
                    client.Headers[HttpRequestHeader.ContentType] = "application/json";
                    var authHeader = $"Basic {apikey}";
                    client.Headers.Add(HttpRequestHeader.Authorization, authHeader);

                    Console.WriteLine("Sending Request ...\n " + JsonConvert.SerializeObject(request));
                    var jsonBytes = Encoding.Default.GetBytes(JsonConvert.SerializeObject(request));
                    var result = client.UploadData(new Uri(api), "POST", jsonBytes);

                    var adrecords = ScoreResult.Parse(Encoding.Default.GetString(result), true);

                    //foreach (var r in adrecords)
                    //    Console.WriteLine(r);
                    //Console.Read();

                }

            }

        }
    
}