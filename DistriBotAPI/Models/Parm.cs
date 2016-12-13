using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DistriBotAPI.Models
{
    public class Parm
    {
        [Key]
        public string Id { get; set; }

        public string Description { get; set; }

        public int Value { get; set; }

        public Parm()
        {

        }

        public Parm(string id, string dsc, int value)
        {
            Id = id;
            Description = dsc;
            Value = value;
        }
    }
}