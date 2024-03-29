﻿using DistriBotAPI.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DistriBotAPI.Contexts
{
    public class Context : IdentityDbContext<IdentityUser>
    {
        public DbSet<Salesman> Salesmen { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<DeliveryMan> DeliveryMen { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Route> Routes { get; set; }
        public DbSet<Supervisor> Supervisors { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Parm> Parms { get; set; }

        public Context() : base("Context")
        {
            this.Configuration.ProxyCreationEnabled = false;
            this.Configuration.LazyLoadingEnabled = false;
        }
    }
}
