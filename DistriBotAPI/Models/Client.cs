using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DistriBotAPI.Models
{
    public class Client
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public DayOfWeek DeliverDay { get; set; }

        [JsonIgnore]
        public virtual List<Route> Routes {get;set;}
 
        public string EmailAddress { get; set; }

        public double CreditBalance { get; set; }

        public Client()
        {
            Routes = new List<Route>();
        }

        public Client(string nmb, double lat, double lon, string dir, string tel, DayOfWeek dw, string mail, double creditoB)
        {
            Name = nmb;
            Latitude = lat;
            Longitude = lon;
            Address = dir;
            Phone = tel;
            DeliverDay = dw;
            EmailAddress = mail;
            CreditBalance = creditoB;
            Routes = new List<Route>();
        }
    }
}