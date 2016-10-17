using DistriBotAPI.Contexts;
using DistriBotAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DistriBotAPI.DataAccess
{
    public class CRUDClients
    {
        private Context db = new Context();

        public IQueryable<Client> GetClients()
        {
            return db.Clients;
        }

        public Client GetClient(int id)
        {
            return db.Clients.Find(id);
        }

        public void UpdateClient(Client newClient)
        {
            db.Entry(newClient).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void CreateClient(Client cli)
        {
            db.Clients.Add(cli);
            db.SaveChanges();
        }

        public void DeleteClient(Client cli)
        {
            db.Clients.Remove(cli);
            db.SaveChanges();
        }

        public CRUDClients()
        {

        }
    }
}