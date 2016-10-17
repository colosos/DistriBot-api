using DistriBotAPI.Contexts;
using DistriBotAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DistriBotAPI.DataAccess
{
    public class CRUDRoutes
    {
        private Context db = new Context();

        public void CreateRoute(Route route)
        {
            db.Routes.Add(route);
            db.SaveChanges();
        }

        public CRUDRoutes()
        {

        }
    }
}