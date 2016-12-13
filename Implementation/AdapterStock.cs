using InterfacesDLL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using DTO;

namespace Implementation
{
    public class AdapterStock : IStock
    {
        public static HttpClient client = new HttpClient();

        public AdapterStock()
        {
            client.BaseAddress = new Uri("http://serviceslayerdiciembre.azurewebsites.net/");
            client.DefaultRequestHeaders.Accept.Clear();
        }
        public async Task<int> RemainingStock(int PrdId)
        {
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            string path = "api/Stock?prdId=" + PrdId;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                string s = await response.Content.ReadAsStringAsync();
                return Int32.Parse(s);
            }
            return 0;
        }

        public async Task<string> GetStockForAllProducts()
        {
            string path = "api/StockForAllProducts";
            HttpResponseMessage hrm = await client.GetAsync(path);
            if (hrm.IsSuccessStatusCode)
            {
                string s = await hrm.Content.ReadAsStringAsync();
                return s;
            }
            return "";
        }

        public async Task<string> ImportCatalogue()
        {
            string path = "api/ImportCatalogue";
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                string s = await response.Content.ReadAsStringAsync();
                return s;
            }
            return "";
        }

        public async Task<string> ImportClientCatalogue()
        {
            string path = "api/ImportClientCatalogue";
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                string s = await response.Content.ReadAsStringAsync();
                return s;
            }
            return "";
        }

        public async Task<string> UpdateStock(int prdId, int diff)
        {
            string path = "api/UpdateStock?prdId=" + prdId+ "&difference=" + diff;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                string s = await response.Content.ReadAsStringAsync();
                return s;
            }
            return "Error en la solicitud al sistema externo";
        }
    }
}
