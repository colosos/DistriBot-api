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
        public List<Product> GetRecommendations([FromUri] int CliId)
        {
            List<Product> recommendedProducts = new List<Product>();
            List<Product> allProducts = db.Products.ToList();
            int total = allProducts.Count;
            bool[] includedProds = new bool[total];
            int cant = 0;
            int max = Math.Min(3, total);
            Random r = new Random();
            while (cant < max)
            {
                int sig = r.Next(total);
                if (!includedProds[sig])
                {
                    includedProds[sig] = true;
                    cant++;
                    recommendedProducts.Add(allProducts.ElementAt(sig));
                }
            }
            return recommendedProducts;
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
