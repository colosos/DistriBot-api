using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InterfacesDLL
{
        public interface IStock
        {
        Task<int> RemainingStock(int PrdId);
        Task<string> GetStockForAllProducts();
        Task<string> ImportCatalogue();
        Task<string> ImportClientCatalogue();
        Task<string> UpdateStock(int prdId, int diff);
    }
}
