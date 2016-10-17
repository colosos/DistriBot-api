using DistriBotAPI.Contexts;
using DistriBotAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DistriBotAPI.DataAccess
{
    public class CRUDSupervisors
    {
        private Context db = new Context();

        public void CreateSupervisor(Supervisor sup)
        {
            db.Supervisors.Add(sup);
            db.SaveChanges();
        }

        public CRUDSupervisors()
        {

        }
    }
}