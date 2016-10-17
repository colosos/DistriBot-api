using DistriBotAPI.Contexts;
using DistriBotAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DistriBotAPI.DataAccess
{
    public class CRUDItems
    {
        private Context db = new Context();

        public void CreateItem(Item item)
        {
            db.Items.Add(item);
            db.SaveChanges();
        }

        public CRUDItems()
        {

        }
    }
}