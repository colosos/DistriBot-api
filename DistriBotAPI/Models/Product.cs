﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace DistriBotAPI.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Category { get; set; }

        public double Price { get; set; }

        [DefaultValue("KG")]
        public string Unit { get; set; }

        public double UnitQuantity { get; set; }

        public virtual BaseProduct BaseProduct { get; set; }

        public Product()
        {
        }

        public Product(string nmb, string dsc, string category, double price, string unit, double unitQty, BaseProduct bp)
        {
            Name = nmb;
            Description = dsc;
            Category = category;
            Price = price;
            Unit = unit;
            UnitQuantity = unitQty;
            BaseProduct = bp;
        }
    }
}