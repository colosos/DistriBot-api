using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DistriBotAPI.Models
{
    public class Order
    {
        public int Id { get; set; }

        public Client Client { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime DeliveredDate { get; set; }

        public List<Tuple<Product, int>> ProductsList { get; set; }

        public double Price { get; set; }

        public Order()
        {

        }
    }
}