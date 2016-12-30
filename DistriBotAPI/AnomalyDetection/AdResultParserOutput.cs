using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace DistriBotAPI.AnomalyDetection
{
        public class ADResult
        {
            [DataMember]
            public string table { get; set; }

            public List<AnomalyRecordOutput> GetADResults()
            {
                var rowDelim = ";";
                var colDelim = ",";
                var rows = table.Split(new string[] { rowDelim }, StringSplitOptions.RemoveEmptyEntries);

                List<AnomalyRecordOutput> series = new List<AnomalyRecordOutput>();
                for (int i = 0; i < rows.Length; i++)
                {
                    var row = rows[i].Replace("\"", "").Trim();
                    if (i == 0 || row.Length == 0)
                    {
                        continue; // ignore headers and empty rows
                    }

                    var cols = row.Split(new string[] { colDelim }, StringSplitOptions.RemoveEmptyEntries);
                    series.Add(AnomalyRecordOutput.Parse(cols));
                }
                return series;
            }
        }

        public class AnomalyRecordOutput
        {
            public DateTime Time { get; set; }
            public double Data { get; set; }
            public int Spike1 { get; set; }
            public int Spike2 { get; set; }
            public double LevelScore { get; set; }
            public int LevelAlert { get; set; }
            public double TrendScore { get; set; }
            public int TrendAlert { get; set; }

            public AnomalyRecordOutput() { }
            public static AnomalyRecordOutput Parse(string[] values)
            {
                if (values.Length < 8)
                    throw new ArgumentException("Anomaly Record expects 8 values.");
            AnomalyRecordOutput ret = new AnomalyRecordOutput();
            ret.Data = double.Parse(values[1]);
                    ret.Spike1 = int.Parse(values[2]);
            ret.Spike2 = int.Parse(values[3]);
            ret.LevelScore = double.Parse(values[4]);
            ret.LevelAlert = int.Parse(values[5]);
            ret.TrendScore = double.Parse(values[6]);
            ret.TrendAlert = int.Parse(values[7]);
            string s = values[0];
            ret.Time = DateTime.ParseExact(s, "M/d/yyyy h:mm:ss tt", System.Globalization.CultureInfo.InvariantCulture);

            return ret;
            }

            public override string ToString()
            {
                return Time + ", " + Data + ", " + Spike1 + ", " + Spike2 + ", " +
                    LevelScore + ", " + LevelAlert + ", " + TrendScore + ", " + TrendAlert;
            }
        }
    }