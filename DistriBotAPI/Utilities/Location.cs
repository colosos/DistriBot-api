using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DistriBotAPI.Models;

namespace DistriBotAPI.Utilities
{
    public class Location
    {
        public static double Distance(double lat1, double lon1, double lat2, double lon2)
        {
            double distX = Math.Pow(lat1-lat2, 2);
            double distY = Math.Pow(lon1-lon2, 2);
            return Math.Sqrt(distX + distY);
        }
    }
}