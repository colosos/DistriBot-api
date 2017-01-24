using DistriBotAPI.Utilities;
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

        public DateTime? DeliveredDate { get; set; }

        public DateTime PlannedDeliveryDate { get; set; }

        public List<Item> ProductsList { get; set; }

        public double Price { get; set; }

        public Salesman Salesman { get; set; }

        public Order()
        {

        }

        public Order(Client cli, DateTime creationDate, List<Item> prodsList, double price)
        {
            Client = cli;
            CreationDate = creationDate;
            PlannedDeliveryDate = Orders.DeliverDay(cli.DeliverDay);
            ProductsList = prodsList;
            Price = price;
           // Salesman = vend;
        }

        public Order(Client cli, DateTime creationDate, List<Item> prodsList, double price, Salesman vend)
        {
            Client = cli;
            CreationDate = creationDate;
            PlannedDeliveryDate = Orders.DeliverDay(cli.DeliverDay);
            ProductsList = prodsList;
            Price = price;
            Salesman = vend;
        }
    }
}