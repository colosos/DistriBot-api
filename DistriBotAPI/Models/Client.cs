﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DistriBotAPI.Models
{
    public class Client
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }
 
        public string EmailAddress { get; set; }

        public double CreditBalance { get; set; }

        public Client()
        {

        }
    }
}