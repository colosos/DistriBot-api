using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using DistriBotAPI.Contexts;
using DistriBotAPI.Models;
using System.Web.Security;
using InterfacesDLL;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Reflection;
using System.Configuration;
using DTO;
using Newtonsoft.Json;

namespace DistriBotAPI.Controllers
{
    public class ProductsController : ApiController
    {
        private Context db = new Context();
        private IStock stock = null;

        public void InicializarStock()
        {
            if (stock != null) return;
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConfigurationManager.AppSettings["StorageConnectionString"]);
            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            // Retrieve a reference to a container. 
            CloudBlobContainer container = blobClient.GetContainerReference("dlls-stock");
            // blob reference, you can use what ever name you want
            var blobReference = container.GetBlobReferenceFromServer("Implementation.dll");
            var assemblyBytes = new byte[blobReference.Properties.Length];
            blobReference.DownloadToByteArray(assemblyBytes, 0);
            var assembly = Assembly.Load(assemblyBytes);
            var tipo = assembly.GetType("Implementation.AdapterStock");
            stock = Activator.CreateInstance(tipo) as IStock;
        }

        //GET: api/Products
        // [Authorize]
        public IQueryable<Product> GetProducts([FromUri] int desde, [FromUri] int cantidad)
        {
            return db.Products.OrderBy(c => c.Id).Skip(desde - 1).Take(cantidad);
        }

        // GET: api/Products/5
        [ResponseType(typeof(Product))]
        public async Task<IHttpActionResult> GetProduct(int id)
        {
            Product product = await db.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        // GET: api/Products/UpdateCatalogue
        [HttpGet]
        [Route("api/Products/UpdateCatalogue")]
        public async Task<IHttpActionResult> UpdateCatalogue()
        {
            InicializarStock();
            string s = await stock.ImportCatalogue();
            List<ProductDTO> list = JsonConvert.DeserializeObject<List<ProductDTO>>(s);
            int prdYaExistentes = 0;
            int prdAgregados = 0;
            foreach(ProductDTO prd in list)
            {
                if (db.Products.Where(p => p.Name.Equals(prd.Name)).Count() == 0)
                {
                    Product aux = JsonConvert.DeserializeObject<Product>(JsonConvert.SerializeObject(prd));
                    db.Products.Add(aux);
                    prdAgregados++;
                }
                else
                {
                    prdYaExistentes++;
                }
            }
            db.SaveChanges();
            return Ok("Se agregaron "+prdAgregados+" productos y se ignoraron "+prdYaExistentes+" que ya estaban en el catalogo de la distribuidora");
        }

        // GET: api/Products/CheckStock
        [HttpGet]
        [Route("api/Products/CheckStock")]
        public async Task<IHttpActionResult> QueryStock()
        {
            InicializarStock();
            string s = await stock.GetStockForAllProducts();
            List<ProductStock> list = JsonConvert.DeserializeObject<List<ProductStock>>(s);
            return Ok(list);
        }


        // PUT: api/Products/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutProduct(int id, Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != product.Id)
            {
                return BadRequest();
            }

            db.Entry(product).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Products
        [ResponseType(typeof(Product))]
        public async Task<IHttpActionResult> PostProduct(Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Products.Add(product);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = product.Id }, product);
        }

        // DELETE: api/Products/5
        [ResponseType(typeof(Product))]
        public async Task<IHttpActionResult> DeleteProduct(int id)
        {
            Product product = await db.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            db.Products.Remove(product);
            await db.SaveChangesAsync();

            return Ok(product);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductExists(int id)
        {
            return db.Products.Count(e => e.Id == id) > 0;
        }
    }
}