using DistriBotAPI.Contexts;
using DistriBotAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DistriBotAPI.DataAccess
{
    public class CRUDManagers
    {
        private Context db = new Context();

        public void CreateManager(Manager man)
        {
            db.Managers.Add(man);
            db.SaveChanges();
        }

        public CRUDManagers()
        {

        }
    }
}