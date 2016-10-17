using DistriBotAPI.Contexts;
using DistriBotAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DistriBotAPI.DataAccess
{
    public class CRUDSalesmen
    {
        private Context db = new Context();

        public void CreateSalesmen(Salesman ss)
        {
            db.Salesmen.Add(ss);
            db.SaveChanges();
        }

        public CRUDSalesmen()
        {

        }
    }
}