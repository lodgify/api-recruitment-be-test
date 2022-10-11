using ApiApplication.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly IImdbApiStatusService statusService;
        public StatusController(IImdbApiStatusService statusService)
        {
            this.statusService = statusService;
        }
        // GET: api/<StatusController>
        [HttpGet]
        public IActionResult Get()
        {
            var result = statusService.imdbStatus;
            return Ok(result);
        }
    }
}
