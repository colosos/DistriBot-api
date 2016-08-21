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

        public List<Tuple<Product, int>> ProductsList { get; set; }

        public Devolution()
        {

        }
    }
}