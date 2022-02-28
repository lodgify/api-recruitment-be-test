using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using ApiApplication.Services.RemoteServices;
using ApiApplication.Services.ShowtimeServices;
using ApiApplication.Database;

namespace ApiApplication.Controllers
{
    public class ShowtimeController : Controller
    {
        private readonly ILogger<ShowtimeController> _logger;
        private readonly ImdbRemoteService _imdbRemoteService;
        private IShowtimesRepository _showtimesRepository;
        private IShowtimeService _showtimeService;

        public ShowtimeController(ImdbRemoteService imdbRemoteService,
            IShowtimeService showtimeService,
            IShowtimesRepository showtimesRepository)
        {
            _imdbRemoteService = imdbRemoteService;
            _showtimesRepository = showtimesRepository;
            _showtimeService = showtimeService;
        }
        [HttpGet]
        [Route("/api/v1/get_movie_information")]
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

        [HttpGet]
        [Route("/")]
        public async Task<IActionResult> GetShowTimes()
        {
            if (ModelState.IsValid)
            {
                var result = _showtimeService.GetCollection();
                return Ok(result);
            }
            return BadRequest();
        }

        [HttpGet]
        [Route("/test")]
        public async Task<IActionResult> GetShowTimesTest()
        {
            if (ModelState.IsValid)
            {
                var result = _showtimesRepository.GetCollection();
                return Ok(result);
            }
            return BadRequest();
        }
    }
}
