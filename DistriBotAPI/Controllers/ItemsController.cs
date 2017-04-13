using DistriBotAPI.Contexts;
using DistriBotAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Newtonsoft.Json;

namespace DistriBotAPI.Controllers
{
    public class ItemsController : ApiController
    {
        private Context db = new Context();

        // GET: api/Items
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Items/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Items
        [ResponseType(typeof(Item))]
        public async Task<IHttpActionResult> PostItem(Item item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            item.Product = db.Products.Find(item.Product.Id);
            db.Items.Add(item);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = item.Id }, item);
        }

        //[Authorize]
        [HttpGet]
        [Route("api/getRecommendations")]
        public async Task<List<Product>> GetRecommendations([FromUri] int CliId)
        {
            List<Product> recommendedProducts = new List<Product>();
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "bafc7bc42b2641b7ab001d7eb2f0da9e");
                client.BaseAddress = new Uri("https://westus.api.cognitive.microsoft.com/");

                string path = "recommendations/v4.0/models/d0b95e2d-6894-489a-ad56-6763a0b31182/recommend/user?userId=" + CliId.ToString() + "&numberOfResults=3";
                string response = await client.GetStringAsync(path);

                dynamic stuff = JsonConvert.DeserializeObject(response);
                foreach(dynamic recomItem in stuff.recommendedItems)
                {
                    foreach(dynamic item in recomItem.items)
                    {
                        string name = item.name;
                        int id = item.id;
                        Product prd = db.Products.Find(id);
                        recommendedProducts.Add(prd);
                    }
                }
                
            }
            return recommendedProducts;


            //List<Product> recommendedProducts = new List<Product>();
            //List<Product> allProducts = db.Products.ToList();
            //int total = allProducts.Count;
            //bool[] includedProds = new bool[total];
            //int cant = 0;
            //int max = Math.Min(3, total);
            //Random r = new Random();
            //while (cant < max)
            //{
            //    int sig = r.Next(total);
            //    if (!includedProds[sig])
            //    {
            //        includedProds[sig] = true;
            //        cant++;
            //        recommendedProducts.Add(allProducts.ElementAt(sig));
            //    }
            //}
            //return recommendedProducts;
        }

        // PUT: api/Items/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Items/5
        public void Delete(int id)
        {
        }
    }
}
