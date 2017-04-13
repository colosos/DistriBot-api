using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DistriBotAPI.Models
{
    public class Anomaly
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }
       
        public double EstimatedSale { get; set; }
        
        public double RealSale { get; set; }

        public double Low { get; set; }

        public double High { get; set; }

        public double Diff { get; set; }

        public bool IsAnomaly { get; set; }

        public Anomaly()
        {
        }

    }
}