using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using System.Linq;

namespace DistriBotAPI.AnomalyDetection
{

        public class ScoreResult
        {
            [JsonProperty("ADOutput")]
            public string ADOutput { get; set; }

            [JsonProperty("odata.metadata")]
            public string Metadata { get; set; }

            public ADResponse ParsedOutput { get; set; }

            public static List<AnomalyRecord> Parse(String response, bool isSeasonal = false)
            {
                var result = JsonConvert.DeserializeObject<ScoreResult>(response);

                result.ParsedOutput = JsonConvert.DeserializeObject<ADResponse>(result.ADOutput);

                List<AnomalyRecord> series = result.ParsedOutput.Values
                        .Select(r => AnomalyRecord.Parse(r, isSeasonal))
                        .ToList();
                return series;
            }
        }

        public class ADResponse
        {
            [DataMember]
            public string[] ColumnNames { get; set; }

            [DataMember]
            public string[] ColumnTypes { get; set; }

            [DataMember]
            public string[][] Values { get; set; }
        }

        public class AnomalyRecord
        {
            public DateTime Time { get; set; }
            public double Data { get; set; }
            public double ProcessedData { get; set; }
            public int Spike1 { get; set; }
            public int Spike2 { get; set; }
            public double LevelScore1 { get; set; }
            public bool LevelAlert1 { get; set; }
            public double LevelScore2 { get; set; }
            public bool LevelAlert2 { get; set; }
            public double TrendScore { get; set; }
            public bool TrendAlert { get; set; }


            public static AnomalyRecord Parse(string[] values, bool isSeasonal)
            {
                if (values.Length < 8)
                    throw new ArgumentException("Anomaly Record expects 8 values.");
                if (!isSeasonal)
                    // Return the AD record from Score API 
                    return new AnomalyRecord()
                    {

                        Time = DateTime.Parse(values[0]),
                        Data = double.Parse(values[1]),
                        ProcessedData = double.Parse(values[1]),
                        Spike1 = int.Parse(values[2]),
                        Spike2 = int.Parse(values[3]),
                        LevelScore1 = double.Parse(values[4]),
                        LevelAlert1 = int.Parse(values[5]) == 1,
                        TrendScore = double.Parse(values[6]),
                        TrendAlert = int.Parse(values[7]) == 1,
                    };
                else
                    // Return the AD record from ScoreWithSeasonality API 
                    return new AnomalyRecord()
                    {

                        Time = DateTime.Parse(values[0]),
                        Data = double.Parse(values[1]),
                        ProcessedData = double.Parse(values[2]),
                        Spike1 = int.Parse(values[3]),
                        Spike2 = int.Parse(values[4]),
                        LevelScore1 = double.Parse(values[5]),
                        LevelAlert1 = int.Parse(values[6]) == 1,
                        LevelScore2 = double.Parse(values[7]),
                        LevelAlert2 = double.Parse(values[8]) == 1,
                        TrendScore = double.Parse(values[9]),
                        TrendAlert = int.Parse(values[10]) == 1,
                    };

            }

            public override string ToString()
            {
                return Time + ", " + Data + ", " + ProcessedData + "," +
                    Spike1 + ", " + Spike2 + ", " +
                    LevelScore1 + ", " + LevelAlert1 + ", " +
                    LevelScore2 + ", " + LevelAlert2 + ", " +
                    TrendScore + ", " + TrendAlert;
            }

}
}