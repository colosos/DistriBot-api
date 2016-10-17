using DistriBotAPI.Contexts;
using DistriBotAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DistriBotAPI.DataAccess
{
    public class CRUDOrders
    {
        private Context db = new Context();

        public void CreateOrder(Order order)
        {
            int idClient = -1;
            foreach (Client c in db.Clients)
            {
                if (c.Name.Equals(order.Client.Name))
                {
                    idClient = c.Id;
                    break;
                }
            }
            order.Client = db.Clients.Find(idClient);
            int idSalesman = -1;
            foreach (Salesman s in db.Salesmen)
            {
                if (s.UserName.Equals(order.Salesman.UserName))
                {
                    idSalesman = s.Id;
                    break;
                }
            }
            order.Salesman = db.Salesmen.Find(idSalesman);
            //double price = 0;
            foreach (Item i in order.ProductsList)
            {
                int idProd = -1;
                foreach(Product p in db.Products)
                {
                    if (p.Name.Equals(i.Product.Name))
                    {
                        idProd = p.Id;
                        break;
                    }
                }
                i.Product = db.Products.Find(idProd);
                //price += i.Quantity * i.Product.Price;
            }
            //double porcDif = (order.Price - price)/price*100;
            //if (porcDif < 10)
            //{
            db.Orders.Add(order);
            db.SaveChanges();
            //} 
        }

        public CRUDOrders()
        {

        }
    }
}