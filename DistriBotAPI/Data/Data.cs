using DistriBotAPI.Authentication;
using DistriBotAPI.Contexts;
using DistriBotAPI.DataAccess;
using DistriBotAPI.Models;
using DistriBotAPI.Utilities;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DistriBotAPI.Data
{
    public class Data
    {
        public Data()
        {
            _repo = new AuthRepository();
        }
        private static CRUDClients clients = new CRUDClients();
        private static CRUDProducts prods = new CRUDProducts();
        private static CRUDSalesmen salesmen = new CRUDSalesmen();
        private static CRUDDeliveryMen deliverymen = new CRUDDeliveryMen();
        private static CRUDSupervisors supervisors = new CRUDSupervisors();
        private static CRUDOrders ords = new CRUDOrders();
        private static CRUDManagers managers = new CRUDManagers();
        private static CRUDRoutes routes = new CRUDRoutes();
        private static CRUDParms parms = new CRUDParms();
        private static Context db = new Context();
        private static AuthRepository _repo = new AuthRepository();

        public static void VaciarBD()
        {
            db.Products.RemoveRange(db.Products);
            db.Items.RemoveRange(db.Items);
            db.Orders.RemoveRange(db.Orders);
            db.Salesmen.RemoveRange(db.Salesmen);
            db.Clients.RemoveRange(db.Clients);
            db.DeliveryMen.RemoveRange(db.DeliveryMen);
            db.Routes.RemoveRange(db.Routes);
            db.Supervisors.RemoveRange(db.Supervisors);
            db.Managers.RemoveRange(db.Managers);
            db.Parms.RemoveRange(db.Parms);
            foreach (var user in db.Users)
            {
                db.Users.Remove(user);
            }
            db.SaveChanges();
        } 
        public static void LlenarBD()
        {
            InsertarClientes();
            InsertarProductos();
            InsertarVendedores();
            InsertarRepartidores();
            InsertarSupervisores();
            InsertarGerentes();
            InsertarVentas();
            InsertarRutas();
            InsertarParametros();
        }
        public static void InsertarParametros()
        {
            parms.CreateParm(new Parm("AUTOMATIC_ROUTE", "Indicates if the routes for the system will be computed automatically or manually set by the supervisor", 0));
        }
        public static void InsertarClientes()
        {
            clients.CreateClient(Clientes.Client1());
            clients.CreateClient(Clientes.Client2());
            clients.CreateClient(Clientes.Client3());
            clients.CreateClient(Clientes.Client4());
            clients.CreateClient(Clientes.Client5());
            clients.CreateClient(Clientes.Client6());
            clients.CreateClient(Clientes.Client7());
            clients.CreateClient(Clientes.Client8());
            clients.CreateClient(Clientes.Client9());
            clients.CreateClient(Clientes.Client10());
        }
        public static void InsertarProductos()
        {
            Product p1 = new Product("Manzana", "Son mas frescas por la manana", "Frutas", 10, "KG", 1);
            prods.CreateProduct(p1);

            Product p2 = new Product("Banana", "Ecuatorianas de gran tamanio", "Frutas", 25, "KG", 1);
            prods.CreateProduct(p2);

            Product p3 = new Product("Pera", "Muy jugosas", "Frutas", 30, "KG", 1);
            prods.CreateProduct(p3);

            Product p4 = new Product("Ciruela", "Las mejores de la ciudad", "Frutas", 46, "KG", 1);
            prods.CreateProduct(p4);

            Product p5 = new Product("Kiwi", "Ideales para movilizar el intestino", "Frutas", 98, "KG", 1);
            prods.CreateProduct(p5);

            Product p6 = new Product("Anana", "De todos los tamaños y sabores", "Frutas", 70, "KG", 1);
            prods.CreateProduct(p6);

            Product p7 = new Product("Sandia", "Enormes", "Frutas", 11, "KG", 1);
            prods.CreateProduct(p7);

            Product p8 = new Product("Cebolla", "Para no parar de llorar", "Verduras", 23, "KG", 1);
            prods.CreateProduct(p8);

            Product p9 = new Product("Palta", "Verdes y maduras", "Frutas", 33, "KG", 1);
            prods.CreateProduct(p9);

            Product p10 = new Product("Paleta de ping pong", "Deportes", "Koreanas, la mejor calidad", 83, "Unit", 1);
            prods.CreateProduct(p10);

            Product p11 = new Product("Pelota de ping pong", "Deportes", "Firmes y duraderas", 20, "Unit", 1);
            prods.CreateProduct(p11);

            Product p12 = new Product("Mesa de ping pong", "Deportes", "Ideal para todas las edades", 2400, "Unit", 1);
            prods.CreateProduct(p12);
        }
        public static void InsertarVendedores()
        {
            Salesman s1 = Usuarios.Vendedor1();
            InsertarUsuario(s1.UserName, s1.Password);
            salesmen.CreateSalesmen(s1);

            Salesman s2 = Usuarios.Vendedor2();
            InsertarUsuario(s2.UserName, s2.Password);
            salesmen.CreateSalesmen(s2);

            Salesman s3 = Usuarios.Vendedor3();
            InsertarUsuario(s3.UserName, s3.Password);
            salesmen.CreateSalesmen(s3);

            Salesman s4 = Usuarios.Vendedor4();
            InsertarUsuario(s4.UserName, s4.Password);
            salesmen.CreateSalesmen(s4);

            Salesman s5 = Usuarios.Vendedor5();
            InsertarUsuario(s5.UserName, s5.Password);
            salesmen.CreateSalesmen(s5);

            Salesman s6 = Usuarios.Vendedor6();
            InsertarUsuario(s6.UserName, s6.Password);
            salesmen.CreateSalesmen(s6);
        }
        public static void InsertarRepartidores()
        {
            DeliveryMan dm1 = Usuarios.Repartidor1();
            InsertarUsuario(dm1.UserName, dm1.Password);
            deliverymen.CreateDeliveryMan(dm1);

            DeliveryMan dm2 = Usuarios.Repartidor2();
            InsertarUsuario(dm2.UserName, dm2.Password);
            deliverymen.CreateDeliveryMan(dm2);

            DeliveryMan dm3 = Usuarios.Repartidor3();
            InsertarUsuario(dm3.UserName, dm3.Password);
            deliverymen.CreateDeliveryMan(dm3);

            DeliveryMan dm4 = Usuarios.Repartidor4();
            InsertarUsuario(dm4.UserName, dm4.Password);
            deliverymen.CreateDeliveryMan(dm4);

            DeliveryMan dm5 = Usuarios.Repartidor5();
            InsertarUsuario(dm5.UserName, dm5.Password);
            deliverymen.CreateDeliveryMan(dm5);

            DeliveryMan dm6 = Usuarios.Repartidor6();
            InsertarUsuario(dm6.UserName, dm6.Password);
            deliverymen.CreateDeliveryMan(dm6);
        }
        public static void InsertarSupervisores()
        {
            Supervisor s1 = Usuarios.Supervisor1();
            InsertarUsuario(s1.UserName, s1.Password);
            supervisors.CreateSupervisor(s1);

            Supervisor s2 = Usuarios.Supervisor2();
            InsertarUsuario(s2.UserName, s2.Password);
            supervisors.CreateSupervisor(s2);

            Supervisor s3 = Usuarios.Supervisor3();
            InsertarUsuario(s3.UserName, s3.Password);
            supervisors.CreateSupervisor(s3);

            Supervisor s4 = Usuarios.Supervisor4();
            InsertarUsuario(s4.UserName, s4.Password);
            supervisors.CreateSupervisor(s4);

            Supervisor s5 = Usuarios.Supervisor5();
            InsertarUsuario(s5.UserName, s5.Password);
            supervisors.CreateSupervisor(s5);

            Supervisor s6 = Usuarios.Supervisor6();
            InsertarUsuario(s6.UserName, s6.Password);
            supervisors.CreateSupervisor(s6);
        }
        public static void InsertarGerentes()
        {
            Manager g1 = Usuarios.Gerente1();
            InsertarUsuario(g1.UserName, g1.Password);
            managers.CreateManager(g1);

            Manager g2 = Usuarios.Gerente2();
            InsertarUsuario(g2.UserName, g2.Password);
            managers.CreateManager(g2);

            Manager g3 = Usuarios.Gerente3();
            InsertarUsuario(g3.UserName, g3.Password);
            managers.CreateManager(g3);

            Manager g4 = Usuarios.Gerente4();
            InsertarUsuario(g4.UserName, g4.Password);
            managers.CreateManager(g4);

            Manager g5 = Usuarios.Gerente5();
            InsertarUsuario(g5.UserName, g5.Password);
            managers.CreateManager(g5);

            Manager g6 = Usuarios.Gerente6();
            InsertarUsuario(g6.UserName, g6.Password);
            managers.CreateManager(g6);
        }
        public static void InsertarVentas()
        {
            //Client1
            ords.CreateOrder(new Order(Clientes.Client1(), DateTime.Now.AddDays(-24), Carritos.Carrito2(), Carritos.ValorCarrito2(), Usuarios.Vendedor5()));
            ords.CreateOrder(new Order(Clientes.Client1(), DateTime.Now.AddDays(-17), Carritos.Carrito3(), Carritos.ValorCarrito3(), Usuarios.Vendedor6()));
            ords.CreateOrder(new Order(Clientes.Client1(), DateTime.Now.AddDays(-10), Carritos.Carrito1(), Carritos.ValorCarrito1(), Usuarios.Vendedor1()));
            ords.CreateOrder(new Order(Clientes.Client1(), DateTime.Now.AddDays(-3), Carritos.Carrito2(), Carritos.ValorCarrito2(), Usuarios.Vendedor3()));
            ords.CreateOrder(new Order(Clientes.Client1(), DateTime.Now, Carritos.Carrito3(), Carritos.ValorCarrito3(), Usuarios.Vendedor2()));

            //Client2
            ords.CreateOrder(new Order(Clientes.Client2(), DateTime.Now.AddDays(-23), Carritos.Carrito3(), Carritos.ValorCarrito3(), Usuarios.Vendedor1()));
            ords.CreateOrder(new Order(Clientes.Client2(), DateTime.Now.AddDays(-16), Carritos.Carrito1(), Carritos.ValorCarrito1(), Usuarios.Vendedor1()));
            ords.CreateOrder(new Order(Clientes.Client2(), DateTime.Now.AddDays(-9), Carritos.Carrito1(), Carritos.ValorCarrito1(), Usuarios.Vendedor5()));
            ords.CreateOrder(new Order(Clientes.Client2(), DateTime.Now.AddDays(-2), Carritos.Carrito2(), Carritos.ValorCarrito2(), Usuarios.Vendedor4()));
            ords.CreateOrder(new Order(Clientes.Client2(), DateTime.Now, Carritos.Carrito3(), Carritos.ValorCarrito3(), Usuarios.Vendedor2()));

            //Client3
            ords.CreateOrder(new Order(Clientes.Client3(), DateTime.Now.AddDays(-22), Carritos.Carrito1(), Carritos.ValorCarrito1(), Usuarios.Vendedor2()));
            ords.CreateOrder(new Order(Clientes.Client3(), DateTime.Now.AddDays(-15), Carritos.Carrito2(), Carritos.ValorCarrito2(), Usuarios.Vendedor3()));
            ords.CreateOrder(new Order(Clientes.Client3(), DateTime.Now.AddDays(-8), Carritos.Carrito2(), Carritos.ValorCarrito2(), Usuarios.Vendedor4()));
            ords.CreateOrder(new Order(Clientes.Client3(), DateTime.Now.AddDays(-1), Carritos.Carrito1(), Carritos.ValorCarrito1(), Usuarios.Vendedor6()));
            ords.CreateOrder(new Order(Clientes.Client3(), DateTime.Now, Carritos.Carrito3(), Carritos.ValorCarrito3(), Usuarios.Vendedor5()));
        }
        public static void InsertarRutas()
        {
            routes.CreateRoute(new Route("Colossus", Usuarios.Repartidor1(), Clientes.ListClients1(), DayOfWeek.Tuesday));
            routes.CreateRoute(new Route("Empresas", Usuarios.Repartidor2(), Clientes.ListClients2(), DayOfWeek.Monday));
            routes.CreateRoute(new Route("Facultades", Usuarios.Repartidor3(), Clientes.ListClients3(), DayOfWeek.Friday));
        }
        public async static void InsertarUsuario(string user, string pass) { 
        if (Roles.GetRole(user).Equals("none"))
        {
                try { 
                    IdentityResult result = await _repo.RegisterUser(user,pass);
                }
                catch(Exception e)
                {
                    string msg = e.Message;
                }
        }
        }
    }
}