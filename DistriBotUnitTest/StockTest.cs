using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.WindowsAzure.Storage;
using System.Reflection;
using System.Configuration;
using Microsoft.WindowsAzure.Storage.Blob;
using InterfacesDLL;
using DistriBotAPI.Controllers;
using DistriBotAPI.Models;
using System.Collections.Generic;

namespace DistriBotUnitTest
{
    [TestClass]
    public class StockTest
    {
        private IStock stock = null;
        
        private static void InicializarStock()
        {

        }

        [TestMethod]
        public async void TestDecreaseStock()
        {
            InicializarStock();
            int stockActual = await stock.RemainingStock(5);
            string s = await stock.UpdateStock(5, -5);
            int stockFinal = await stock.RemainingStock(5)+5;
            Assert.AreEqual(stockActual, stockFinal);
        }

        [TestMethod]
        public async void TestIncreaseStock()
        {
            InicializarStock();
            int stockActual = await stock.RemainingStock(5);
            string s = await stock.UpdateStock(5, +5);
            int stockFinal = await stock.RemainingStock(5) - 5;
            Assert.AreEqual(stockActual, stockFinal);
        }
    }
}
