using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DistriBotAPI.Utilities
{
    public class Orders
    {
        public static DateTime DeliverDay(DayOfWeek dw)
        {
            DayOfWeek dw1 = DateTime.Now.DayOfWeek;
            int dif = -1;
            if (dw >= dw1) dif = dw - dw1;
            else dif = dw + 7 - dw1;
            return DateTime.Now.AddDays(dif).Date;
        }
    }
}