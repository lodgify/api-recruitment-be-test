using ApiApplication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly ImdbStatus _imdbStatus;
        public StatusController(ImdbStatus imdbStatus)
        {
            _imdbStatus = imdbStatus;
        }

        [Authorize(Policy = "Read-only")]
        [HttpGet]
        public ActionResult<ImdbStatus> GetImdbStatus()
        {
            return Ok(_imdbStatus);
        }
    }
}
