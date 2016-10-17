using DistriBotAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DistriBotAPI.Data
{
    public class Clientes
    {
        public static Client Client1() { 
            return new Client("Andres Canabarro", 0, 0, "Br. Espana 2873/302", "098657882", "andrescanabarro@gmail.com", -500);
        }
        public static Client Client2()
        {
            return new Client("Juan Pablo Mazza", 0, 0, "Cadiz 2870", "098123456", "jpmazza@gmail.com", 100);
        }
        public static Client Client3()
        {
            return new Client("Alejandro Monetti", 0, 0, "Dublin 2641", "098611000", "alemonetti@gmail.com", 0);
        }
        public static Client Client4()
        {
            return new Client("Federico Zaiter", 0, 0, "Vicente Rocafuerte", "098000111", "fzaiter@gmail.com", 500);
        }
    }
}