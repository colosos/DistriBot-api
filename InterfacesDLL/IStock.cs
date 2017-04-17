using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InterfacesDLL
{
        public interface IStock
        {
        //Returns the actual stock for all the products in the catalogue
        Task<string> GetStockForAllProducts();

        //Returns the actual stock for the given product
        Task<int> RemainingStock(int PrdId);

        //Updates the actual stock for the given product
        Task<string> UpdateStock(int prdId, int diff);

        //Updates the product's catalogue adding the products which are not in DistriBot yet
        Task<string> ImportCatalogue();

        Task<string> ImportClientCatalogue();
    }
}
