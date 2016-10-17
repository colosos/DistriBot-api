using DistriBotAPI.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace DistriBotAPI.Controllers
{
    public class TestController : ApiController
    {

        [Route("api/upload")]
        [HttpGet]
        public async Task<IHttpActionResult> UpdateImages()
        {
            Images.UploadAllFiles();
            Images.DownloadAllFiles();
            return Ok();
        }

        [Route("api/resetClients")]
        [HttpGet]
        public async Task<IHttpActionResult> ResetClients()
        {
            Data.Data.VaciarBD();
            Data.Data.LlenarBD();
            return Ok();
        }
    }
}
