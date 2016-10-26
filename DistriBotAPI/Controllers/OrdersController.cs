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
using Implementation;

namespace DistriBotAPI.Controllers
{
    public class OrdersController : ApiController
    {
        private Context db = new Context();
        private Implementation.IFacturation billing = (Implementation.IFacturation) new FacturationImp();

        // GET: api/Orders
        //[Authorize]
        [Route("api/Orders")]
        public IQueryable<Order> GetOrders([FromUri] int desde, [FromUri] int cantidad)
        {
            if (cantidad == 0) return db.Orders.OrderBy(c => c.Id).Skip(desde - 1);
            return db.Orders.OrderBy(c => c.Id).Skip(desde - 1).Take(cantidad);
        }

        //[Authorize]
        [Route("api/missingOrders")]
        public IQueryable<Order> GetUndeliveredOrders()
        {
            return db.Orders.Where(o=>o.DeliveredDate == null).Include("Client").Include("Salesman");
        }

        //[Authorize]
        [Route("api/setDeliverDate")]
        public async Task<IHttpActionResult> SetDeliverDate([FromUri] int idOrder, [FromUri] DateTime date)
        {
            Order oldOrder = await db.Orders.Where(o => o.Id == idOrder).FirstAsync();
            oldOrder.PlannedDeliveryDate = date;
            db.SaveChanges();
            return Ok();
        }

        //[Authorize]
        [Route("api/neededProducts")]
        public List<Item> GetNeededProducts([FromUri] DateTime date)
        {
            List<Item> list = new List<Item>();
            foreach (Order o in db.Orders.Where(o => o.PlannedDeliveryDate <= date && o.DeliveredDate == null).Include("ProductsList").Include("ProductsList.Product"))
            {
                foreach(Item aux in o.ProductsList)
                {
                    bool encontro = false;
                    foreach(Item i in list)
                    {
                        
                        if(i.Product.Id == aux.Product.Id)
                        {
                            i.Quantity += aux.Quantity;
                            encontro = true;
                            break;
                        }
                    }
                    if (!encontro)
                    {
                        list.Add(aux);
                    }
                }
            }
            return list;
        }

        //[Authorize]
        [Route("api/deliverOrder")]
        [HttpPost]
        public async Task<IHttpActionResult> SetDeliveredFlag([FromUri] int idOrder, [FromUri] bool flag)
        {
            Order oldOrder = await db.Orders.Where(o => o.Id == idOrder).FirstAsync();
            if (flag) oldOrder.DeliveredDate = DateTime.Now;
            else oldOrder.DeliveredDate = null;
            db.SaveChanges();
            return Ok();
        }

        // GET: api/Orders/5
        [ResponseType(typeof(Order))]
        public async Task<IHttpActionResult> GetOrder(int id)
        {
            Order order = await db.Orders.Where(o => o.Id == id)
                .Include("Client")
                .Include("ProductsList")
                .Include("ProductsList.Product")
                .Include("Salesman")
                .Include("ProductsList.Product.BaseProduct").FirstAsync();
            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        // PUT: api/Orders/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutOrder(int id, Order order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != order.Id)
            {
                return BadRequest();
            }

            Order oldOrder = await db.Orders.Where(o => o.Id == id)
                .Include("Client")
                .Include("ProductsList")
                .Include("ProductsList.Product")
                .Include("ProductsList.Product.BaseProduct").FirstAsync();

            if (oldOrder.Client != null)
            {
                if (order.Client != null)
                {
                    if (oldOrder.Client.Id != order.Client.Id)
                    {
                        // Relationship change
                        // Attach assumes that newFoo.SubFoo is an existing entity
                        db.Clients.Attach(order.Client);
                        oldOrder.Client = order.Client;
                    }
                }
                else // relationship has been removed
                    oldOrder.Client = null;
            }
            else
            {
                if (order.Client != null) // relationship has been added
                {
                    // Attach assumes that newFoo.SubFoo is an existing entity
                    db.Clients.Attach(order.Client);
                    oldOrder.Client = order.Client;
                }
            }

            // Delete\from database that are not in the newFoo.SubFoo collection
            List<Item> borrarAux = new List<Item>();
            foreach (var dbItems in oldOrder.ProductsList)
                if (!order.ProductsList.Any(s => s.Id == dbItems.Id))
                    borrarAux.Add(dbItems);
            db.Items.RemoveRange(borrarAux);
            foreach (var newItem in order.ProductsList)
            {
                var dbItem = oldOrder.ProductsList.SingleOrDefault(s => s.Id == newItem.Id);
                if (dbItem != null)
                {
                    // Update items
                    db.Entry(dbItem).CurrentValues.SetValues(newItem);
                    if (dbItem.Product != null)
                    {
                        if (newItem.Product != null)
                        {
                            if (dbItem.Product.Id != newItem.Product.Id)
                            {
                                // Relationship change
                                // Attach assumes that newFoo.SubFoo is an existing entity
                                db.Products.Attach(newItem.Product);
                                dbItem.Product = newItem.Product;
                            }
                        }
                        else // relationship has been removed
                            dbItem.Product = null;
                    }
                    else
                    {
                        if (order.Client != null) // relationship has been added
                        {
                            // Attach assumes that newFoo.SubFoo is an existing entity
                            db.Clients.Attach(order.Client);
                            oldOrder.Client = order.Client;
                        }
                    }
                }
                else
                    db.Items.Add(newItem);
            }

            db.Entry(oldOrder).CurrentValues.SetValues(order);
            db.Entry(oldOrder).Collection("ProductsList").CurrentValue = order.ProductsList;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
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

        // POST: api/Orders
        //[ResponseType(typeof(Order))]
        [Route("api/Orders")]
        [HttpPost]
        public async Task<IHttpActionResult> PostOrder(Order order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            order.Client = db.Clients.Find(order.Client.Id);
            int idSalesman = -1;
            foreach (Salesman s in db.Salesmen)
            {
                if (s.UserName.Equals(order.Salesman.UserName))
                {
                    idSalesman = s.Id;
                    break;
                }
            }
            order.Salesman = db.Salesmen.Find(idSalesman);
            order.CreationDate = DateTime.Now;
            order.DeliveredDate = DateTime.Now;
            //double price = 0;
            foreach (Item i in order.ProductsList)
            {
                i.Product = db.Products.Find(i.Product.Id);
                //price += i.Quantity * i.Product.Price;
            }
            //double porcDif = (order.Price - price)/price*100;
            //if (porcDif < 10)
            //{
                db.Orders.Add(order);
                await db.SaveChangesAsync();
            //}
            return Ok();
            //return CreatedAtRoute("DefaultApi", new { id = order.Id }, order);
        }

        // DELETE: api/Orders/5
        [ResponseType(typeof(Order))]
        public async Task<IHttpActionResult> DeleteOrder(int id)
        {
            Order order = await db.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            db.Orders.Remove(order);
            await db.SaveChangesAsync();

            return Ok(order);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OrderExists(int id)
        {
            return db.Orders.Count(e => e.Id == id) > 0;
        }

        // GENERATES THE CORRESPONDING BILL
        [Route("api/Orders/bill")]
        public async Task<IHttpActionResult> GenerateBill([FromUri] int orderId)
        {
            Order order = await db.Orders.FindAsync(orderId);
            if (order == null)
            {
                return NotFound();
            }
            List<Tuple<string, int, double>> products = new List<Tuple<string, int, double>>();
            foreach(Item aux in order.ProductsList)
            {
                products.Add(new Tuple<string, int, double>(aux.Product.Name, aux.Quantity, aux.Product.Price));
            }
            billing.GenerateBill(products);
            return Ok();
        }
    }
}