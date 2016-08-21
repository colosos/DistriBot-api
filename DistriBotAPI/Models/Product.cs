using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DistriBotAPI.Models
{
    public enum SaleUnit { KG, Quantity };
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }

        public SaleUnit Unit { get; set; }

        public double UnitQuantity { get; set; }

        public virtual BaseProduct BaseProduct { get; set; }
      
        public Product()
        {
        }
    }
}