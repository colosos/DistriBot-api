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
using DistriBotAPI.Authentication;
using Microsoft.AspNet.Identity;

namespace DistriBotAPI.Controllers
{
    public class SalesmenController : ApiController
    {
        private Context db = new Context();
        private AuthRepository _repo = null;

        public SalesmenController()
        {
            _repo = new AuthRepository();
        }

        // GET: api/Salesmen
        public IQueryable<Salesman> GetSalesmen()
        {
            return db.Salesmen;
        }

        // GET: api/Salesmen/5
        [ResponseType(typeof(Salesman))]
        public async Task<IHttpActionResult> GetSalesman(int id)
        {
            Salesman salesman = await db.Salesmen.FindAsync(id);
            if (salesman == null)
            {
                return NotFound();
            }

            return Ok(salesman);
        }

        // PUT: api/Salesmen/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutSalesman(int id, Salesman salesman)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != salesman.Id)
            {
                return BadRequest();
            }

            db.Entry(salesman).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SalesmanExists(id))
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

        // POST: api/Salesmen
        [ResponseType(typeof(Salesman))]
        public async Task<IHttpActionResult> PostSalesman(Salesman salesman)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            IdentityResult result = await _repo.RegisterUser(salesman);
            db.Salesmen.Add(salesman);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = salesman.Id }, salesman);
        }

        // DELETE: api/Salesmen/5
        [ResponseType(typeof(Salesman))]
        public async Task<IHttpActionResult> DeleteSalesman(int id)
        {
            Salesman salesman = await db.Salesmen.FindAsync(id);
            if (salesman == null)
            {
                return NotFound();
            }

            db.Salesmen.Remove(salesman);
            await db.SaveChangesAsync();

            return Ok(salesman);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SalesmanExists(int id)
        {
            return db.Salesmen.Count(e => e.Id == id) > 0;
        }
    }
}