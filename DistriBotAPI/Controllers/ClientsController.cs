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
using DistriBotAPI.Utilities;
using DistriBotAPI.DataAccess;
using InterfacesDLL;
using Microsoft.WindowsAzure.Storage;
using System.Reflection;
using Newtonsoft.Json;
using System.Configuration;
using Microsoft.WindowsAzure.Storage.Blob;

namespace DistriBotAPI.Controllers
{
    public class ClientsController : ApiController
    {
        private Context db = new Context();
        private CRUDClients cc = new CRUDClients();
        private IFinance finance = null;
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

        //= (IFinance) new AdapterFinance();

        // GET: api/Clients
        //[Authorize]
        public IQueryable<Client> GetClients([FromUri] int desde, [FromUri] int cantidad)
        {
            if (cantidad == 0) return cc.GetClients().OrderBy(c => c.Id).Skip(desde - 1);
            return cc.GetClients().OrderBy(c => c.Id).Skip(desde - 1).Take(cantidad);
        }

        // GET: api/Clients/5
        [ResponseType(typeof(Client))]
        public IHttpActionResult GetClient(int id)
        {
            Client client = cc.GetClient(id);
            if (client == null)
            {
                return NotFound();
            }

            return Ok(client);
        }

        // PUT: api/Clients/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutClient(int id, Client client)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != client.Id)
            {
                return BadRequest();
            }
            cc.UpdateClient(client);
            //db.Entry(client).State = EntityState.Modified;

            //try
            //{
            //    await db.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!ClientExists(id))
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Clients
        [ResponseType(typeof(void))]
        public IHttpActionResult PostClient(Client client)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            cc.CreateClient(client);
            return CreatedAtRoute("DefaultApi", new { id = client.Id }, client);
        }

        // DELETE: api/Clients/5
        [ResponseType(typeof(Client))]
        public IHttpActionResult DeleteClient(int id)
        {
            Client client = cc.GetClient(id);
            if (client == null)
            {
                return NotFound();
            }
            cc.DeleteClient(client);
            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ClientExists(int id)
        {
            return cc.GetClients().Count(e => e.Id == id) > 0;
        }

        // FIND THE CLIENT WHICH IS CLOSEST TO THE GIVEN COORDINATES
        [Route("api/Clients/nearest")]
        [HttpGet]
        [ResponseType(typeof(Client))]
        public IHttpActionResult DetectClient([FromUri] double lat, [FromUri] double lon)
        {
            if (cc.GetClients().Count() == 0)
            {
                return BadRequest();
            }
            double minDist = Double.MaxValue;
            Client closestClient = null;
            foreach (Client aux in GetClients(1,0))
            {
                double dist = Location.Distance(lat, lon, aux.Latitude, aux.Longitude);
                if (dist < minDist)
                {
                    minDist = dist;
                    closestClient = aux;
                }
            }
            return Ok(closestClient);
        }

        // RETURNS THE FINAL BALANCE OF A GIVEN CLIENT
        [Route("api/Clients/balance")]
        public IHttpActionResult GetBalance([FromUri] int cliId)
        {
            int balance = finance.ReturnBalance(cliId, DateTime.Now);      
            return Ok(balance);
        }


        // GET: api/Products/UpdateCatalogue
        [HttpGet]
        [Route("api/Clients/UpdateCatalogue")]
        public async Task<IHttpActionResult> UpdateCatalogue()
        {
            InicializarStock();
            string s = await stock.ImportClientCatalogue();
            List<Client> list = JsonConvert.DeserializeObject<List<Client>>(s);
            int cliYaExistentes = 0;
            int cliAgregados = 0;
            foreach (Client cli in list)
            {
                if (db.Clients.Where(c => c.Name.Equals(cli.Name)).Count() == 0)
                {
                    db.Clients.Add(cli);
                    cliAgregados++;
                }
                else
                {
                    cliYaExistentes++;
                }
            }
            db.SaveChanges();
            return Ok("Se agregaron " + cliAgregados + " clientes y se ignoraron " + cliYaExistentes + " que ya estaban en el catalogo de la distribuidora");
        }
    }
}