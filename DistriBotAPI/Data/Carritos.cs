using DistriBotAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DistriBotAPI.Data
{
    public class Carritos
    {
        public static List<Item> Carrito1()
        {
            List<Item> items = new List<Item>();
            Product p1 = new Product("Manzana", "Son mas frescas por la manana", "Frutas",10, "KG", 1);
            Product p2 = new Product("Banana", "Ecuatorianas de gran tamanio", "Frutas", 25, "KG", 1);
            Product p3 = new Product("Pera", "Muy jugosas", "Frutas",30, "KG", 1);
            Item it1 = new Item(5, p1);
            Item it2 = new Item(2, p2);
            Item it3 = new Item(3, p3);
            items.Add(it1);
            items.Add(it2);
            items.Add(it3);
            return items;
        }

        public static List<Item> Carrito2()
        {
            List<Item> items = new List<Item>();
            Product p1 = new Product("Manzana", "Son mas frescas por la manana", "Frutas", 10, "KG", 1);
            Product p2 = new Product("Banana", "Ecuatorianas de gran tamanio", "Frutas", 25, "KG", 1);
            Product p3 = new Product("Pera", "Muy jugosas", "Frutas", 30, "KG", 1);
            Item it1 = new Item(1, p1);
            Item it2 = new Item(9, p2);
            Item it3 = new Item(1, p3);
            items.Add(it1);
            items.Add(it2);
            items.Add(it3);
            return items;
        }

        public static List<Item> Carrito3()
        {
            List<Item> items = new List<Item>();
            Product p1 = new Product("Manzana", "Son mas frescas por la manana", "Frutas", 10, "KG", 1);
            Product p2 = new Product("Banana", "Ecuatorianas de gran tamanio", "Frutas", 25, "KG", 1);
            Product p3 = new Product("Pera", "Muy jugosas", "Frutas", 30, "KG", 1);
            Item it1 = new Item(3, p1);
            Item it2 = new Item(3, p2);
            Item it3 = new Item(3, p3);
            items.Add(it1);
            items.Add(it2);
            items.Add(it3);
            return items;
        }

        public static double ValorCarrito1()
        {
            double ret = 0;
            foreach(Item i in Carritos.Carrito1())
            {
                ret += i.Quantity * i.Product.Price;
            }
            return ret;
        }

        public static double ValorCarrito2()
        {
            double ret = 0;
            foreach (Item i in Carritos.Carrito2())
            {
                ret += i.Quantity * i.Product.Price;
            }
            return ret;
        }

        public static double ValorCarrito3()
        {
            double ret = 0;
            foreach (Item i in Carritos.Carrito3())
            {
                ret += i.Quantity * i.Product.Price;
            }
            return ret;
        }
    }
}