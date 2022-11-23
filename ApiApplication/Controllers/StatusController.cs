using ApiApplication.Services.IMDB.Status;
using Microsoft.AspNetCore.Mvc;

namespace ApiApplication.Controllers
{
    [Route("api/status")]
    [ApiController]
    public class StatusController : BaseApiController
    {
        private readonly ImdbStatusService _imdbStatusService;

        public StatusController(ImdbStatusService imdbStatusService)
        {
            _imdbStatusService = imdbStatusService;
        }

        [HttpGet]
        public IActionResult GetStatus()
        {
            var response = _imdbStatusService.ImdbStatus;

            return Ok(response);
        }
    }
}
