using InterfacesDLL;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Implementation
{
    public class AdapterStock : IStock
    {
        public static HttpClient client = new HttpClient();

        public AdapterStock()
        {
            client.BaseAddress = new Uri("http://serviceslayer20161120085520.azurewebsites.net/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }
        public async Task<int> RemainingStock(int PrdId)
        {
            string path = "api/Stock?prdId=" + PrdId;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                string s = await response.Content.ReadAsStringAsync();
                return Int32.Parse(s);
            }
            return 0;
        }
    }
}
