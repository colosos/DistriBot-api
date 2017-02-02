using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DistriBotAPI.Models
{
    public class OrderFede
    {
        public string Id { get; set; }
        public string Cliente { get; set; }
        public string CreationDate { get; set; }
        public List<ItemFede> ProductList { get; set; }
        public string Price { get; set; }

        public class ItemFede
        {
            public string Name { get; set; }
            public int Quantity { get; set; }
        }
    }
}