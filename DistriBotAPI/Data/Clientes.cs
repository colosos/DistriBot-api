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
            return new Client("Andres Canabarro", -34.914668, -56.150695, "Br. Espana 2873/302", "098657882", DayOfWeek.Tuesday, "andrescanabarro@gmail.com", -500);
        }
        public static Client Client2()
        {
            return new Client("Juan Pablo Mazza", -34.876634, -56.159731, "Cadiz 2870", "099729734", DayOfWeek.Tuesday, "jpmazza@gmail.com", 100);
        }
        public static Client Client3()
        {
            return new Client("Alejandro Monetti", -34.882042, -56.074454, "Dublin 2641", "099970198", DayOfWeek.Tuesday, "alemonetti@gmail.com", 0);
        }
        public static Client Client4()
        {
            return new Client("Federico Zaiter", -34.892703, -56.083378, "Vicente Rocafuerte", "099888581", DayOfWeek.Tuesday, "fzaiter@gmail.com", 500);
        }
        public static Client Client5()
        {
            return new Client("Universidad ORT Uruguay (Campus Centro)", -34.903843, -56.190626, "Cuareim 1451", "29021505", DayOfWeek.Friday, "ort@gmail.com", 500);
        }
        public static Client Client6()
        {
            return new Client("Universidad ORT Uruguay (Campus Pocitos)", -34.912813, -56.156900, "Bvar. España 2633", "27071806", DayOfWeek.Friday, "ort@gmail.com", 500);
        }
        public static Client Client7()
        {
            return new Client("Burlesque", -34.907313, -56.136056, "Av. Dr. Luis Alberto de Herrera esq. Iturriaga", "26232808", DayOfWeek.Friday, "burlesque@gmail.com", -4500);
        }
        public static Client Client8()
        {
            return new Client("TopTier labs, Inc", -34.906796, -56.185265, "San José 1406", "+1 917-862-4945", DayOfWeek.Monday, "toptier@gmail.com", 8500);
        }
        public static Client Client9()
        {
            return new Client("AstroPay LLP", -34.902972, -56.134567, "Luis Bonavita 1294", "", DayOfWeek.Monday, "astropay@gmail.com", 1500);
        }
        public static Client Client10()
        {
            return new Client("TangoCode", -34.908567, -56.163167, "Br. Artigas 1118", "", DayOfWeek.Monday, "tangocode@gmail.com", 6700);
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

        public static List<Client> ListClients2()
        {
            List<Client> ret = new List<Client>();
            ret.Add(Clientes.Client8());
            ret.Add(Clientes.Client9());
            ret.Add(Clientes.Client10());
            return ret;
        }

        public static List<Client> ListClients3()
        {
            List<Client> ret = new List<Client>();
            ret.Add(Clientes.Client7());
            ret.Add(Clientes.Client6());
            ret.Add(Clientes.Client5());
            return ret;
        }
    }
}