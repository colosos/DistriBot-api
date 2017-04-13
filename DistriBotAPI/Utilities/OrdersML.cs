using DistriBotAPI.Contexts;
using DistriBotAPI.DataAccess;
using DistriBotAPI.Models;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;

namespace DistriBotAPI.Utilities
{
    public class OrdersML
    {
        static SortedDictionary<string, string> productos = new SortedDictionary<string, string>();
        static SortedDictionary<string, OrderFede> ordenes = new SortedDictionary<string, OrderFede>();
        static CRUDOrders co = new CRUDOrders();
        private static Context db = new Context();

        public static void Main()
        {
            using (TextFieldParser parser = new TextFieldParser(@"C:/Users/Nano/Documents/catalog.csv"))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(";");
                int count = 0;
                string[] fields = null;
                while (!parser.EndOfData)
                {
                    try
                    {
                        fields = parser.ReadFields();
                    }
                    catch
                    {
                    }
                    if (++count > 1)//TIENE TRUE PUESTO???
                    {
                        //Processing row

                        string asin = fields[0];
                        string name = fields[1];

                        if (!productos.ContainsKey(asin)) productos.Add(asin, name);
                    }
                }
            }
            using (TextFieldParser parser = new TextFieldParser(@"C:/Users/Nano/Documents/usage1.csv"))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                int count = 0;
                string[] fields = null;
                while (!parser.EndOfData)
                {
                    try
                    {
                        fields = parser.ReadFields();
                    }
                    catch
                    {
                    }
                    if (++count > 1)//TIENE TRUE PUESTO???
                    {
                        //Processing row

                        string InvoiceNo = fields[0];
                        string Quantity = fields[1];
                        string InvoiceDate = fields[2];
                        string UnitPrice = fields[3];
                        string CustomerID = fields[4];
                        string ASIN = fields[5];

                        if (productos.ContainsKey(ASIN))
                            if (ordenes.ContainsKey(InvoiceNo))
                            {
                                OrderFede orden = ordenes[InvoiceNo];

                                OrderFede.ItemFede item = new OrderFede.ItemFede();
                                item.Name = productos[ASIN];
                                item.Quantity = Int32.Parse(Quantity);
                                orden.ProductList.Add(item);
                                orden.Price = String.Format("{0:F2}", Double.Parse(orden.Price) + (Double.Parse(UnitPrice) * (double)item.Quantity));
                            }
                            else
                            {

                                OrderFede orden = new OrderFede();
                                orden.Id = InvoiceNo;
                                orden.CreationDate = InvoiceDate;
                                orden.Cliente = CustomerID;
                                OrderFede.ItemFede item = new OrderFede.ItemFede();
                                item.Name = productos[ASIN];
                                item.Quantity = Int32.Parse(Quantity);
                                orden.ProductList = new List<OrderFede.ItemFede>();
                                orden.ProductList.Add(item);
                                orden.Price = String.Format("{0:F2}", Double.Parse(UnitPrice) * (double)item.Quantity);

                                ordenes.Add(orden.Id, orden);
                            }
                    }
                }
            }
            int cont = -3010;
            foreach (var orden in ordenes)
            {
                if (cont < 0)
                {
                    cont++;
                    continue;
                }
                OrderFede orderF = orden.Value;
                Order order = new Order();
                string nombreActual = orderF.Cliente;
                int ind = Math.Abs(nombreActual.GetHashCode());
                //Order order = db.Orders.Include("Client").Where(ord => ord.Client.Name.Equals(orderF.Cliente)).First();
                int totalClientes = db.Clients.Count();

                int clienteElegido = (ind % totalClientes);
                int idCliente = db.Clients.OrderBy(cli=>cli.Id).Skip(clienteElegido).First().Id;
                order.Client = db.Clients.Find(idCliente);
                order.CreationDate = DateTime.Parse(orderF.CreationDate);
                order.ProductsList = new List<Item>();
                foreach (OrderFede.ItemFede i in orderF.ProductList)
                {
                    Product prod = db.Products.Where(prd => prd.Name.Equals(i.Name)).First();
                    order.ProductsList.Add(new Item(i.Quantity, prod));
                    order.Price += i.Quantity * prod.Price;
                }
                int totalVendedores = db.Salesmen.Count();
                int vendedorElegido = (ind % totalVendedores);
                int idVendedor = db.Salesmen.OrderBy(vnd => vnd.Id).Skip(vendedorElegido).First().Id;
                order.Salesman = db.Salesmen.Find(idVendedor);
                //order.Price = Double.Parse(orderF.Price);
                order.PlannedDeliveryDate = Orders.DeliverDay(order.Client.DeliverDay);
                cont++;
                db.Orders.Add(order);
                if (cont == 10)
                {
                    db.SaveChanges();
                    cont = 0;
                }
            }
            db.SaveChanges();

        }
    }
}