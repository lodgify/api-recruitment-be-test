using ApiApplication.Services;
using ApiApplication.Services.Implementors;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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
