using DistriBotAPI.Contexts;
using DistriBotAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DistriBotAPI.DataAccess
{
    public class CRUDDevolutions
    {
        private Context db = new Context();

        public void CreateOrder(Devolution dev)
        {
            db.Devolutions.Add(dev);
            db.SaveChanges();
        }

        public CRUDDevolutions()
        {

        }
    }
}