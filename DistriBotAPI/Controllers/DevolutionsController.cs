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
    public class DevolutionsController : ApiController
    {
        private Context db = new Context();

        // GET: api/Devolutions
        public IQueryable<Devolution> GetDevolutions()
        {
            return db.Devolutions;
        }

        // GET: api/Devolutions/5
        [ResponseType(typeof(Devolution))]
        public async Task<IHttpActionResult> GetDevolution(int id)
        {
            Devolution devolution = await db.Devolutions.FindAsync(id);
            if (devolution == null)
            {
                return NotFound();
            }

            return Ok(devolution);
        }

        // PUT: api/Devolutions/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutDevolution(int id, Devolution devolution)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != devolution.Id)
            {
                return BadRequest();
            }

            db.Entry(devolution).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DevolutionExists(id))
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

        // POST: api/Devolutions
        [ResponseType(typeof(Devolution))]
        public async Task<IHttpActionResult> PostDevolution(Devolution devolution)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Devolutions.Add(devolution);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = devolution.Id }, devolution);
        }

        // DELETE: api/Devolutions/5
        [ResponseType(typeof(Devolution))]
        public async Task<IHttpActionResult> DeleteDevolution(int id)
        {
            Devolution devolution = await db.Devolutions.FindAsync(id);
            if (devolution == null)
            {
                return NotFound();
            }

            db.Devolutions.Remove(devolution);
            await db.SaveChangesAsync();

            return Ok(devolution);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DevolutionExists(int id)
        {
            return db.Devolutions.Count(e => e.Id == id) > 0;
        }
    }
}