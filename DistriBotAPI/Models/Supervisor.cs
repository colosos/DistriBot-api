using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DistriBotAPI.Models
{
    public class Supervisor
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string UserName { get; set; }

        [NotMapped]
        public string Password { get; set; }

        [NotMapped]
        public string ConfirmPassword { get; set; }

        public Supervisor()
        {

        }

        public Supervisor(string nmb, string user, string pass, string confPass)
        {
            Name = nmb;
            UserName = user;
            Password = pass;
            ConfirmPassword = confPass;
        }
    }
}