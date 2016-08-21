﻿using System;
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
using Microsoft.AspNet.Identity;
using DistriBotAPI.Authentication;
using System.Web.Security;

namespace DistriBotAPI.Controllers
{
    public class DeliveryMenController : ApiController
    {
        private Context db = new Context();
        private AuthRepository _repo = null;

        public DeliveryMenController()
        {
            _repo = new AuthRepository();
        }

        // GET: api/DeliveryMen
        public IQueryable<DeliveryMan> GetDeliveryMen()
        {
            return db.DeliveryMen;
        }

        // GET: api/DeliveryMen/5
        [ResponseType(typeof(DeliveryMan))]
        public async Task<IHttpActionResult> GetDeliveryMan(int id)
        {
            DeliveryMan deliveryMan = await db.DeliveryMen.FindAsync(id);
            if (deliveryMan == null)
            {
                return NotFound();
            }

            return Ok(deliveryMan);
        }

        // PUT: api/DeliveryMen/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutDeliveryMan(int id, DeliveryMan deliveryMan)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != deliveryMan.Id)
            {
                return BadRequest();
            }

            db.Entry(deliveryMan).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeliveryManExists(id))
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

        // POST: api/DeliveryMen
        [ResponseType(typeof(DeliveryMan))]
        public async Task<IHttpActionResult> PostDeliveryMan(DeliveryMan deliveryMan)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            IdentityResult result = await _repo.RegisterUser(deliveryMan.UserName, deliveryMan.Password);
            db.DeliveryMen.Add(deliveryMan);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = deliveryMan.Id }, deliveryMan);
        }

        // DELETE: api/DeliveryMen/5
        [ResponseType(typeof(DeliveryMan))]
        public async Task<IHttpActionResult> DeleteDeliveryMan(int id)
        {
            DeliveryMan deliveryMan = await db.DeliveryMen.FindAsync(id);
            if (deliveryMan == null)
            {
                return NotFound();
            }

            db.DeliveryMen.Remove(deliveryMan);
            await db.SaveChangesAsync();

            return Ok(deliveryMan);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DeliveryManExists(int id)
        {
            return db.DeliveryMen.Count(e => e.Id == id) > 0;
        }
    }
}