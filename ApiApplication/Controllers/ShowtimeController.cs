using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using ApiApplication.Services.RemoteServices;

namespace ApiApplication.Controllers
{
    public class ShowtimeController : Controller
    {
        private readonly ILogger<ShowtimeController> _logger;
        private ImdbRemoteService _imdbRemoteService;

        public ShowtimeController(ImdbRemoteService imdbRemoteService)
        {
            _imdbRemoteService = imdbRemoteService;
        }
        [HttpGet]
        [Route("/api/v1/GetMovieInformation")]
        public async Task<IActionResult> GetMovieInformation(
            [FromQuery] string movie_id)
        {
            if (ModelState.IsValid)
            {
                var result = await _imdbRemoteService.GetMovieInformation(movie_id);
                return Ok(result);
            }
            return BadRequest();
        }
    }
}
