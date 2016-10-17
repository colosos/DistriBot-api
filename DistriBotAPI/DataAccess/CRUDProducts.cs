using DistriBotAPI.Contexts;
using DistriBotAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DistriBotAPI.DataAccess
{
    public class CRUDProducts
    {
        private Context db = new Context();

        public void CreateProduct(Product prod)
        {
            db.Products.Add(prod);
            db.SaveChanges();
        }

        public CRUDProducts()
        {

        }
    }
}