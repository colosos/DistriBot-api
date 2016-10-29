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
        private static Context db = new Context();
        private static AuthRepository _repo = new AuthRepository();

        public static void VaciarBD()
        {
            //db.BaseProducts.RemoveRange(db.BaseProducts);
            //db.Products.RemoveRange(db.Products);
            //db.Items.RemoveRange(db.Items);
            //db.Orders.RemoveRange(db.Orders);
            //db.Salesmen.RemoveRange(db.Salesmen);
            //db.Clients.RemoveRange(db.Clients);
            //db.DeliveryMen.RemoveRange(db.DeliveryMen);
            //db.Devolutions.RemoveRange(db.Devolutions);
            //db.Routes.RemoveRange(db.Routes);
            //db.Supervisors.RemoveRange(db.Supervisors);
            //db.Tags.RemoveRange(db.Tags);
            //db.Managers.RemoveRange(db.Managers);
            //foreach (var user in db.Users)
            //{
            //    db.Users.Remove(user);   
            //}
            //db.SaveChanges();
        } 
        public static void LlenarBD()
        {
            //InsertarClientes();
            InsertarProductos();
            //InsertarVendedores();
            //InsertarRepartidores();
            //InsertarSupervisores();
            //InsertarGerentes();
            //InsertarVentas();
            //InsertarRutas();
        }
        public static void InsertarClientes()
        {
            clients.CreateClient(Clientes.Client1());
            clients.CreateClient(Clientes.Client2());
            clients.CreateClient(Clientes.Client3());
            clients.CreateClient(Clientes.Client4()); ;
        }
        public static void InsertarProductos()
        {
            //Product p1 = new Product("Manzana", "Son mas frescas por la manana", 10, "KG", 1, null);
            //prods.CreateProduct(p1);

            //Product p2 = new Product("Banana", "Ecuatorianas de gran tamanio", 25, "KG", 1, null);
            //prods.CreateProduct(p2);

            //Product p3 = new Product("Pera", "Muy jugosas", 30, "KG", 1, null);
            //prods.CreateProduct(p3);

            Product p4 = new Product("Ciruela", "Las mejores de la ciudad", 46, "KG", 1, null);
            prods.CreateProduct(p4);

            Product p5 = new Product("Kiwi", "Ideales para movilizar el intestino", 98, "KG", 1, null);
            prods.CreateProduct(p5);

            Product p6 = new Product("Ananá", "De todos los tamaños y sabores", 70, "KG", 1, null);
            prods.CreateProduct(p6);

            Product p7 = new Product("Sandía", "Enormes", 11, "KG", 1, null);
            prods.CreateProduct(p7);

            Product p8 = new Product("Cebolla", "Para no parar de llorar", 23, "KG", 1, null);
            prods.CreateProduct(p8);

            Product p9 = new Product("Palta", "Verdes y maduras", 33, "KG", 1, null);
            prods.CreateProduct(p9);
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
        }
        public static void InsertarVentas()
        {
            ords.CreateOrder(new Order(Clientes.Client1(), DateTime.Now.AddDays(-10), DateTime.Now, Carritos.Carrito1(), 1000, Usuarios.Vendedor1()));
            ords.CreateOrder(new Order(Clientes.Client2(), DateTime.Now.AddDays(-5), DateTime.Now, Carritos.Carrito2(), 1200, Usuarios.Vendedor2()));
            ords.CreateOrder(new Order(Clientes.Client3(), DateTime.Now.AddDays(-2), DateTime.Now, Carritos.Carrito3(), 400, Usuarios.Vendedor3()));
        }
        public static void InsertarRutas()
        {
            routes.CreateRoute(new Route("Maldonado", Usuarios.Repartidor1(), Clientes.ListClients1(), DayOfWeek.Monday));
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