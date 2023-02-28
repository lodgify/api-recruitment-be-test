using Lodgify.Cinema.Domain.Contract;
using Microsoft.AspNetCore.Mvc;

namespace ApiApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : Controller
    {
        private readonly IImdbStatus _imdbStatus;

        public StatusController(IImdbStatus imdbStatus)
        {
            _imdbStatus = imdbStatus;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new { IsUp = _imdbStatus.IsUp });
        }
    }
}