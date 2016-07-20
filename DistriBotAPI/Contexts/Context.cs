using DistriBotAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DistriBotAPI.Contexts
{
    public class Context : DbContext
    {
        public DbSet<Salesman> Salesmen { get; set; }
    }
}