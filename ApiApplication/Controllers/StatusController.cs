using ApiApplication.Dtos;
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
        public StatusController()
        {

        }

        public ActionResult<IMDBStatus> Get()
        {
            var data = IMDBStatus.Instance();
            return Ok(data);

        }
    }
}
