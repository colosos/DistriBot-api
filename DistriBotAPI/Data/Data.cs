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
        private static Context db = new Context();
        private static AuthRepository _repo = null;

        public static void VaciarBD()
        {
            db.BaseProducts.RemoveRange(db.BaseProducts);
            db.Products.RemoveRange(db.Products);
            db.Items.RemoveRange(db.Items);
            db.Orders.RemoveRange(db.Orders);
            db.Salesmen.RemoveRange(db.Salesmen);
            db.Clients.RemoveRange(db.Clients);
            db.DeliveryMen.RemoveRange(db.DeliveryMen);
            db.Devolutions.RemoveRange(db.Devolutions);
            db.Routes.RemoveRange(db.Routes);
            db.Supervisors.RemoveRange(db.Supervisors);
            db.Tags.RemoveRange(db.Tags);
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
            InsertarVentas();
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
            Product p1 = new Product("Manzana", "Son mas frescas por la manana", 10, "KG", 1, null);
            prods.CreateProduct(p1);

            Product p2 = new Product("Banana", "Ecuatorianas de gran tamanio", 25, "KG", 1, null);
            prods.CreateProduct(p2);

            Product p3 = new Product("Pera", "Muy jugosas", 30, "KG", 1, null);
            prods.CreateProduct(p3);
        }
        public static void InsertarVendedores()
        {
            Salesman s1 = Usuarios.Vendedor1();
            salesmen.CreateSalesmen(s1);
            InsertarUsuario(s1.UserName, s1.Password);

            Salesman s2 = Usuarios.Vendedor2();
            salesmen.CreateSalesmen(s2);
            InsertarUsuario(s2.UserName, s2.Password);

            Salesman s3 = Usuarios.Vendedor3();
            salesmen.CreateSalesmen(s3);
            InsertarUsuario(s3.UserName, s3.Password);
        }
        public static void InsertarRepartidores()
        {
            DeliveryMan dm1 = Usuarios.Repartidor1();
            deliverymen.CreateDeliveryMan(dm1);
            InsertarUsuario(dm1.UserName, dm1.Password);

            DeliveryMan dm2 = Usuarios.Repartidor2();
            deliverymen.CreateDeliveryMan(dm2);
            InsertarUsuario(dm2.UserName, dm2.Password);

            DeliveryMan dm3 = Usuarios.Repartidor3();
            deliverymen.CreateDeliveryMan(dm3);
            InsertarUsuario(dm3.UserName, dm3.Password);
        }
        public static void InsertarSupervisores()
        {
            Supervisor s1 = Usuarios.Supervisor1();
            supervisors.CreateSupervisor(s1);
            InsertarUsuario(s1.UserName, s1.Password);

            Supervisor s2 = Usuarios.Supervisor2();
            supervisors.CreateSupervisor(s2);
            InsertarUsuario(s2.UserName, s2.Password);

            Supervisor s3 = Usuarios.Supervisor3();
            supervisors.CreateSupervisor(s3);
            InsertarUsuario(s3.UserName, s3.Password);
        }
        public static void InsertarVentas()
        {
            ords.CreateOrder(new Order(Clientes.Client1(), DateTime.Now.AddDays(-10), DateTime.Now, Carritos.Carrito1(), 1000, Usuarios.Vendedor1()));
            ords.CreateOrder(new Order(Clientes.Client2(), DateTime.Now.AddDays(-5), DateTime.Now, Carritos.Carrito2(), 1200, Usuarios.Vendedor2()));
            ords.CreateOrder(new Order(Clientes.Client3(), DateTime.Now.AddDays(-2), DateTime.Now, Carritos.Carrito3(), 400, Usuarios.Vendedor3()));
        }
        public async static void InsertarUsuario(string user, string pass) { 
        if (Roles.GetRole(user).Equals("none"))
        { 
            IdentityResult result = await _repo.RegisterUser(user,pass);
        }
        }
    }
}