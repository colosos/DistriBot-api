using DistriBotAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DistriBotAPI.Data
{
    public class Clientes
    {
        public static Client Client1() {
            return new Client("Andres Canabarro", 0, 0, "Br. Espana 2873/302", "098657882", DayOfWeek.Monday, "andrescanabarro@gmail.com", -500);
        }
        public static Client Client2()
        {
            return new Client("Juan Pablo Mazza", 0, 0, "Cadiz 2870", "098123456", DayOfWeek.Tuesday, "jpmazza@gmail.com", 100);
        }
        public static Client Client3()
        {
            return new Client("Alejandro Monetti", 0, 0, "Dublin 2641", "098611000", DayOfWeek.Thursday, "alemonetti@gmail.com", 0);
        }
        public static Client Client4()
        {
            return new Client("Federico Zaiter", 0, 0, "Vicente Rocafuerte", "098000111", DayOfWeek.Thursday, "fzaiter@gmail.com", 500);
        }
        public static List<Client> ListClients1()
        {
            List<Client> ret = new List<Client>();
            ret.Add(Clientes.Client1());
            ret.Add(Clientes.Client4());
            ret.Add(Clientes.Client3());
            ret.Add(Clientes.Client2());
            return ret;
        }
    }
}