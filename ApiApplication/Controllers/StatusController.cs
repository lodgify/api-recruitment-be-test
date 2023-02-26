using ApiApplication.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StatusController : ControllerBase
    {
        private readonly IImdbService _imdbService;
        public StatusController(IImdbService imdbService) 
        {
            _imdbService = imdbService;
        }

        [HttpGet]
        [Authorize(Policy = "ApiKeyReadPolicy")]
        public IActionResult Get()
        {
            var showTimes = _imdbService.GetStatus();

            return Ok(showTimes);
        }
    }
}
