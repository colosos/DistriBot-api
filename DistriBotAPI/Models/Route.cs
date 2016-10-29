using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DistriBotAPI.Models
{
    public class Route
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public DeliveryMan Driver { get; set; }

        public List<Client> Clients { get; set; }

        public DayOfWeek DayOfWeek { get; set; }

        public Route()
        {

        }
        public Route(string dsc, DeliveryMan driver, List<Client> clis, DayOfWeek dw)
        {
            Description = dsc;
            Driver = driver;
            Clients = clis;
            DayOfWeek = dw;
        }
    }
}