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
    public class BaseProductsController : ApiController
    {
        private Context db = new Context();

        // GET: api/BaseProducts
        public IQueryable<BaseProduct> GetBaseProducts()
        {
            return db.BaseProducts;
        }

        // GET: api/BaseProducts/5
        [ResponseType(typeof(BaseProduct))]
        public async Task<IHttpActionResult> GetBaseProduct(int id)
        {
            BaseProduct baseProduct = await db.BaseProducts.FindAsync(id);
            if (baseProduct == null)
            {
                return NotFound();
            }

            return Ok(baseProduct);
        }

        // PUT: api/BaseProducts/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutBaseProduct(int id, BaseProduct baseProduct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != baseProduct.Id)
            {
                return BadRequest();
            }

            db.Entry(baseProduct).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BaseProductExists(id))
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

        // POST: api/BaseProducts
        [ResponseType(typeof(BaseProduct))]
        public async Task<IHttpActionResult> PostBaseProduct(BaseProduct baseProduct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.BaseProducts.Add(baseProduct);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = baseProduct.Id }, baseProduct);
        }

        // DELETE: api/BaseProducts/5
        [ResponseType(typeof(BaseProduct))]
        public async Task<IHttpActionResult> DeleteBaseProduct(int id)
        {
            BaseProduct baseProduct = await db.BaseProducts.FindAsync(id);
            if (baseProduct == null)
            {
                return NotFound();
            }

            db.BaseProducts.Remove(baseProduct);
            await db.SaveChangesAsync();

            return Ok(baseProduct);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BaseProductExists(int id)
        {
            return db.BaseProducts.Count(e => e.Id == id) > 0;
        }
    }
}