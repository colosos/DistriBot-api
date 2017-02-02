using DistriBotAPI.AnomalyDetection;
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
        [Route("api/anomaly")]
        [HttpGet]
        public IHttpActionResult AnomalyDetection()
        {
            Program.TestScoreAPI();
            //Program.TestSeasonalityScoreAPI();
            ProgramOutput.Main(new string[10]);
            return Ok();
        }

        [Route("api/upload")]
        [HttpGet]
        public IHttpActionResult UpdateImages()
        {
            Images.UploadAllFiles();
            //Images.DownloadAllFiles();
            return Ok();
        }

        [Route("api/resetClients")]
        [HttpGet]
        public IHttpActionResult ResetClients()
        {
            Data.Data.VaciarBD();
            Data.Data.LlenarBD();
            return Ok();
        }

        [Route("api/deliverDay")]
        [HttpGet]
        public IHttpActionResult DeliverDay()
        {
            return Ok(Orders.DeliverDay(DayOfWeek.Thursday));
        }

        [Route("api/loadOrders")]
        [HttpGet]
        public IHttpActionResult LoadOrdersForML()
        {
            OrdersML.Main();
            return Ok();
        }
    }
}
