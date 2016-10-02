using DistriBotAPI.Authentication;
using DistriBotAPI.Contexts;
using DistriBotAPI.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace DistriBotAPI.Controllers
{
    public class SupervisorsController : ApiController
    {
        private Context db = new Context();
        private AuthRepository _repo = null;

        public SupervisorsController()
        {
            _repo = new AuthRepository();
        }

        // GET: api/Supervisors
        public IEnumerable<Supervisor> GetSupervisors()
        {
            using (var ctx = new Context())
            {
                return ctx.Supervisors.ToList();
            }
        }

        // GET: api/Supervisors/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Supervisors
        [ResponseType(typeof(Supervisor))]
        [Route("api/Supervisors")]
        public async Task<IHttpActionResult> PostSupervisor(Supervisor supervisor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (Utilities.Roles.GetRole(supervisor.UserName).Equals("none"))
            {
                IdentityResult result = await _repo.RegisterUser(supervisor.UserName, supervisor.Password);
                db.Supervisors.Add(supervisor);
                await db.SaveChangesAsync();
                return CreatedAtRoute("DefaultApi", new { id = supervisor.Id }, supervisor);
            }
            else
                return BadRequest();
        }

        // PUT: api/Supervisors/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Supervisors/5
        public void Delete(int id)
        {
        }
    }
}
