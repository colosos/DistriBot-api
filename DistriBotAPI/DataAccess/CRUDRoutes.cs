using DistriBotAPI.Contexts;
using DistriBotAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DistriBotAPI.DataAccess
{
    public class CRUDRoutes
    {
        private Context db = new Context();

        public void CreateRoute(Route route)
        {
            List<Client> listCli = new List<Client>();
            foreach(Client c in route.Clients)
            {
                int idCli = -1;
                foreach(Client aux in db.Clients)
                {
                    if (aux.Name == c.Name)
                    {
                        idCli = aux.Id;
                        break;
                    }
                }
                Client clientAux = db.Clients.Find(idCli);
                listCli.Add(clientAux);
            }
            route.Clients = listCli;
            int id = -1;
            foreach(DeliveryMan dm in db.DeliveryMen)
            {
                if (dm.UserName.Equals(route.Driver.UserName))
                {
                    id = dm.Id;
                    break;
                }
            }
            route.Driver = db.DeliveryMen.Find(id);
            db.Routes.Add(route);
            db.SaveChanges();
        }

        public CRUDRoutes()
        {

        }
    }
}