using DistriBotAPI.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DistriBotAPI.Utilities
{
    public class Roles
    {
        private static Context db = new Context();

        public static string GetRole(string userName)
        {
            if (IsDeliveryMen(userName))
                return "deliverymen";
            if (IsSalesmen(userName))
                return "salesmen";
            if (IsSupervisor(userName))
                return "supervisor";
            //Returns "none" allows a new user to be added to the DB as there is no user 
            //with that username
            return "none";
        }

        public static bool IsDeliveryMen(string userName)
        {
            return db.DeliveryMen.Where(dm => dm.UserName.Equals(userName)).Count() > 0;
        }

        public static bool IsSalesmen(string userName)
        {
            return db.Salesmen.Where(sm => sm.UserName.Equals(userName)).Count() > 0;
        }

        public static bool IsSupervisor(string userName)
        {
            return db.Supervisors.Where(sv => sv.UserName.Equals(userName)).Count() > 0;
        }
    }
}