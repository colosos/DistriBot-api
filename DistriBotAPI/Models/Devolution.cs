using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DistriBotAPI.Models
{
    public class Devolution
    {
        public int Id { get; set; }

        public Client Client { get; set; }

        public DateTime RequestDate { get; set; }

        public DateTime DevolutionDate { get; set; }

        public List<Item> ProductsList { get; set; }

        public Devolution()
        {

        }

        public Devolution(Client cli, DateTime reqDate, DateTime devDate, List<Item> prodsList)
        {
            Client = cli;
            RequestDate = reqDate;
            DevolutionDate = devDate;
            ProductsList = prodsList;
        }
    }
}