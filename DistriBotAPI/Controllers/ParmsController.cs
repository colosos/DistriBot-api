using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using DistriBotAPI.Contexts;
using DistriBotAPI.Models;

namespace DistriBotAPI.Controllers
{
    public class ParmsController : ApiController
    {
        private Context db = new Context();

        // GET: api/Parms
        public IQueryable<Parm> GetParms()
        {
            return db.Parms;
        }

        // GET: api/Parms
        [HttpOptions]
        public IHttpActionResult GetOptions()
        {
            return Ok();
        }

        // GET: api/Parms/5
        [ResponseType(typeof(Parm))]
        public IHttpActionResult GetParm(string id)
        {
            Parm parm = db.Parms.Find(id);
            if (parm == null)
            {
                return NotFound();
            }

            return Ok(parm);
        }

        // PUT: api/Parms/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutParm(string id, int value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Parm parm = db.Parms.Find(id);
            parm.Value = value;

            try
            {
                db.SaveChanges();
                return Ok("Se ha establecido el parametro correctamente");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParmExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        // POST: api/Parms
        [ResponseType(typeof(Parm))]
        public IHttpActionResult PostParm(Parm parm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Parms.Add(parm);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ParmExists(parm.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = parm.Id }, parm);
        }

        // DELETE: api/Parms/5
        [ResponseType(typeof(Parm))]
        public IHttpActionResult DeleteParm(string id)
        {
            Parm parm = db.Parms.Find(id);
            if (parm == null)
            {
                return NotFound();
            }

            db.Parms.Remove(parm);
            db.SaveChanges();

            return Ok(parm);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ParmExists(string id)
        {
            return db.Parms.Count(e => e.Id == id) > 0;
        }
    }
}