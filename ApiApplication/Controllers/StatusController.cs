using ApiApplication.Dtos;
using ApiApplication.Worker;
using IMDbApiLib;
using IMDbApiLib.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ApiApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IMDBStatus> Get()
        {           

            var data = IMDBStatus.Instance();
            return Ok(data);

        }
    }
}
