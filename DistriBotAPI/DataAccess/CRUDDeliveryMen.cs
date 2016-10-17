using DistriBotAPI.Contexts;
using DistriBotAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DistriBotAPI.DataAccess
{
    public class CRUDDeliveryMen
    {
        private Context db = new Context();

        public void CreateDeliveryMan(DeliveryMan dMan)
        {
            db.DeliveryMen.Add(dMan);
            db.SaveChanges();
        }

        public CRUDDeliveryMen()
        {

        }
    }
}