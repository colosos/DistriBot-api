using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DistriBotAPI.Models
{
    public class Item
    {
        public int Id { get; set; }

        public int Quantity { get; set; }

        public Product Product { get; set; }

        public Item()
        {

        }

    }
}