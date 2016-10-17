using DistriBotAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DistriBotAPI.Data
{
    public static class Usuarios
    {
      public static Salesman Vendedor1() { 
        string user = "jc";
        string pass = "123456";
        return new Salesman("Juan Carlos", user, pass, pass);
        }
        public static Salesman Vendedor2()
        {
            string user = "jm";
            string pass = "123456";
            return new Salesman("Juan Manuel", user, pass, pass);
        }
        public static Salesman Vendedor3()
        {
            string user = "ale";
            string pass = "123456";
            return new Salesman("Alejandro", user, pass, pass);
        }
        public static DeliveryMan Repartidor1()
        {
            string user = "jc";
        string pass = "123456";
        return new DeliveryMan("Juan Carlos", user, pass, pass);
        }
        public static DeliveryMan Repartidor2()
        {
            string user = "jm";
            string pass = "123456";
            return new DeliveryMan("Juan Manuel", user, pass, pass);
    }
        public static DeliveryMan Repartidor3()
        {
            string user = "ale";
            string pass = "123456";
            return new DeliveryMan("Alejandro", user, pass, pass);
}
        public static Supervisor Supervisor1()
        {
            string user = "jc";
        string pass = "123456";
        return new Supervisor("Juan Carlos", user, pass, pass);
        }
        public static Supervisor Supervisor2()
        {
            string user = "jm";
        string pass = "123456";
            return new Supervisor("Juan Manuel", user, pass, pass);
        }
        public static Supervisor Supervisor3()
        {
            string user = "ale";
    string pass = "123456";
            return new Supervisor("Alejandro", user, pass, pass);
        }
    }
}