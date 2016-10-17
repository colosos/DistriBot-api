using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DistriBotAPI.Models
{
    public class BaseProduct
    {
        public int Id { get; set; }

        public string Name { get; set; }
        
        public string Description { get; set; }

        public List<Tag> Tags { get; set; }

        public BaseProduct()
        {
        }

        public BaseProduct(string nmb, string dsc, List<Tag> tags)
        {
            Name = nmb;
            Description = dsc;
            Tags = tags;
        }
    }
}