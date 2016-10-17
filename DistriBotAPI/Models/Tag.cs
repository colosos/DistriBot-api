using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DistriBotAPI.Models
{
    public class Tag
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Tag()
        {
        }

        public Tag(string nmb)
        {
            Name = nmb;
        }
    }
}