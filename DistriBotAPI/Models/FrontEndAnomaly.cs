using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DistriBotAPI.Models
{
    public class FrontEndAnomaly
    {
        public int Id { get; set; }

        public bool IsPositive { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public double Diff { get; set; }

        public FrontEndAnomaly()
        {

        }

        public FrontEndAnomaly(bool isPositive, DateTime start, DateTime end, double diff)
        {
            IsPositive = isPositive;
            StartDate = start;
            EndDate = end;
            Diff = diff;
        }
    } 
}