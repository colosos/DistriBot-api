using DistriBotAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DistriBotAPI.Data
{
    public static class Usuarios
    {
        public static Salesman Vendedor1()
        {
            string user = "vjc";
            string pass = "123456";
            return new Salesman("Juan Carlos", user, pass, pass);
        }
        public static Salesman Vendedor2()
        {
            string user = "vjm";
            string pass = "123456";
            return new Salesman("Juan Manuel", user, pass, pass);
        }
        public static Salesman Vendedor3()
        {
            string user = "vale";
            string pass = "123456";
            return new Salesman("Alejandro", user, pass, pass);
        }
        public static Salesman Vendedor4()
        {
            string user = "vpedro";
            string pass = "123456";
            return new Salesman("Pedro", user, pass, pass);
        }
        public static Salesman Vendedor5()
        {
            string user = "vjuan";
            string pass = "123456";
            return new Salesman("Juan", user, pass, pass);
        }
        public static Salesman Vendedor6()
        {
            string user = "vgonzalo";
            string pass = "123456";
            return new Salesman("Gonzalo", user, pass, pass);
        }
        public static DeliveryMan Repartidor1()
        {
            string user = "rjc";
            string pass = "123456";
            return new DeliveryMan("Juan Carlos", user, pass, pass);
        }
        public static DeliveryMan Repartidor2()
        {
            string user = "rjm";
            string pass = "123456";
            return new DeliveryMan("Juan Manuel", user, pass, pass);
        }
        public static DeliveryMan Repartidor3()
        {
            string user = "rale";
            string pass = "123456";
            return new DeliveryMan("Alejandro", user, pass, pass);
        }
        public static DeliveryMan Repartidor4()
        {
            string user = "rpedro";
            string pass = "123456";
            return new DeliveryMan("Pedro", user, pass, pass);
        }
        public static DeliveryMan Repartidor5()
        {
            string user = "rjuan";
            string pass = "123456";
            return new DeliveryMan("Juan", user, pass, pass);
        }
        public static DeliveryMan Repartidor6()
        {
            string user = "rgonzalo";
            string pass = "123456";
            return new DeliveryMan("Gonzalo", user, pass, pass);
        }
        public static Supervisor Supervisor1()
        {
            string user = "sjc";
            string pass = "123456";
            return new Supervisor("Juan Carlos", user, pass, pass);
        }
        public static Supervisor Supervisor2()
        {
            string user = "sjm";
            string pass = "123456";
            return new Supervisor("Juan Manuel", user, pass, pass);
        }
        public static Supervisor Supervisor3()
        {
            string user = "sale";
            string pass = "123456";
            return new Supervisor("Alejandro", user, pass, pass);
        }
        public static Supervisor Supervisor4()
        {
            string user = "spedro";
            string pass = "123456";
            return new Supervisor("Pedro", user, pass, pass);
        }
        public static Supervisor Supervisor5()
        {
            string user = "sjuan";
            string pass = "123456";
            return new Supervisor("Juan", user, pass, pass);
        }
        public static Supervisor Supervisor6()
        {
            string user = "sgonzalo";
            string pass = "123456";
            return new Supervisor("Gonzalo", user, pass, pass);
        }
        public static Manager Gerente1()
        {
            string user = "ger1";
            string pass = "123456";
            return new Manager("Gerente General", user, pass, pass);
        }
        public static Manager Gerente2()
        {
            string user = "ger2";
            string pass = "123456";
            return new Manager("Gerente Comercial", user, pass, pass);
        }
        public static Manager Gerente3()
        {
            string user = "ger3";
            string pass = "123456";
            return new Manager("Gerente Financiero", user, pass, pass);
        }
        public static Manager Gerente4()
        {
            string user = "mpedro";
            string pass = "123456";
            return new Manager("Pedro", user, pass, pass);
        }
        public static Manager Gerente5()
        {
            string user = "mjuan";
            string pass = "123456";
            return new Manager("Juan", user, pass, pass);
        }
        public static Manager Gerente6()
        {
            string user = "mgonzalo";
            string pass = "123456";
            return new Manager("Gonzalo", user, pass, pass);
        }
    }
}