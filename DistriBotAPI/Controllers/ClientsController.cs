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
using DistriBotAPI.Interfaces;
using Implementation;

namespace DistriBotAPI.Controllers
{
    public class ClientsController : ApiController
    {
        private Context db = new Context();
        private Implementation.IFinance finance = (Implementation.IFinance) new FinanceImp();

        // GET: api/Clients
        [Authorize]
        public IQueryable<Client> GetClients([FromUri] int desde, [FromUri] int cantidad)
        {
            if (cantidad == 0) return db.Clients.OrderBy(c => c.Id).Skip(desde - 1);
            return db.Clients.OrderBy(c => c.Id).Skip(desde - 1).Take(cantidad);
        }

        // GET: api/Clients/5
        [ResponseType(typeof(Client))]
        public async Task<IHttpActionResult> GetClient(int id)
        {
            Client client = await db.Clients.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }

            return Ok(client);
        }

        // PUT: api/Clients/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutClient(int id, Client client)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != client.Id)
            {
                return BadRequest();
            }

            db.Entry(client).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientExists(id))
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

        // POST: api/Clients
        [ResponseType(typeof(Client))]
        public async Task<IHttpActionResult> PostClient(Client client)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Clients.Add(client);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = client.Id }, client);
        }

        // DELETE: api/Clients/5
        [ResponseType(typeof(Client))]
        public async Task<IHttpActionResult> DeleteClient(int id)
        {
            Client client = await db.Clients.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }

            db.Clients.Remove(client);
            await db.SaveChangesAsync();

            return Ok(client);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ClientExists(int id)
        {
            return db.Clients.Count(e => e.Id == id) > 0;
        }

        // FIND THE CLIENT WHICH IS CLOSEST TO THE GIVEN COORDINATES
        [Route("api/Clients/nearest")]
        [HttpGet]
        [ResponseType(typeof(Client))]
        public async Task<IHttpActionResult> DetectClient([FromUri] double lat, [FromUri] double lon)
        {
            if (db.Clients.Count() == 0)
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
        public async Task<IHttpActionResult> GetBalance([FromUri] int cliId)
        {
            int balance = finance.ReturnBalance(cliId, DateTime.Now);      
            return Ok(balance);
        }
    }
}