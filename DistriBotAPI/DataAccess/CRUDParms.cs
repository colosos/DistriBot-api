using DistriBotAPI.Contexts;
using DistriBotAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DistriBotAPI.DataAccess
{
    public class CRUDParms
    {
        private Context db = new Context();

        public void CreateParm(Parm parm)
        {
            db.Parms.Add(parm);
            db.SaveChanges();
        }
    }
}