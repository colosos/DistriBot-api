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

namespace DistriBotAPI.Controllers
{
    public class RoutesController : ApiController
    {
        private Context db = new Context();

        // GET: api/Routes
        public IQueryable<Route> GetRoutes()
        {
            return db.Routes.Include("Driver");
        }

        // GET: api/Routes/5
        [ResponseType(typeof(Route))]
        public IHttpActionResult GetRoute(int id)
        {
            Route route = db.Routes.Include("Driver").Include("Clients").Where(r => r.Id == id).First();
            if (route == null)
            {
                return NotFound();
            }

            return Ok(route);
        }

        //[Authorize]
        [Route("api/EvaluateRoute")]
        public IHttpActionResult EvaluateGivenRoute(List<Client> clients)
        {
            double res = 0;
            for(int i = 0; i < clients.Count-1; i++)
            {
                res += Distance(clients.ElementAt(i), clients.ElementAt(i + 1));
            }
            return Ok(res);
        }

        public static double Distance(Client c1, Client c2)
        {
            double difX = c1.Latitude - c2.Latitude;
            double difY = c1.Longitude - c2.Longitude;
            difX *= difX;
            difY *= difY;
            double dif = difX + difY;
            dif = Math.Sqrt(dif);
            return dif;
        }

        // PUT: api/Routes/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutRoute(int id, Route route)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != route.Id)
            {
                return BadRequest();
            }

            db.Entry(route).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RouteExists(id))
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

        // POST: api/Routes
        //[ResponseType(typeof(Route))]
        [HttpPost]
        public async Task<IHttpActionResult> PostRoute(Route route)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            route.Driver = db.DeliveryMen.Find(route.Driver.Id);
            List<Client> clients = new List<Client>();
            foreach(Client c in route.Clients)
            {
                clients.Add(db.Clients.Find(c.Id));
            }
            route.Clients = clients;
            db.Routes.Add(route);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = route.Id }, route);
        }

        // DELETE: api/Routes/5
        [ResponseType(typeof(Route))]
        public async Task<IHttpActionResult> DeleteRoute(int id)
        {
            Route route = await db.Routes.FindAsync(id);
            if (route == null)
            {
                return NotFound();
            }

            db.Routes.Remove(route);
            await db.SaveChangesAsync();

            return Ok(route);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RouteExists(int id)
        {
            return db.Routes.Count(e => e.Id == id) > 0;
        }
    }
}