using System;
using System.Threading.Tasks;
using ApiApplication.Database;
using ApiApplication.Mappings;
using ApiApplication.Models.Showtime;
using Microsoft.AspNetCore.Mvc;

namespace ApiApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShowtimeController : ControllerBase
    {
        private readonly IShowtimesRepository _showtimesRepository;

        public ShowtimeController(IShowtimesRepository showtimesRepository)
        {
            _showtimesRepository = showtimesRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllShowtimes([FromQuery] GetAllShowtimesRequest request)
        {
            var showtimes = await _showtimesRepository.GetByShowtimeRequestAsync(request);

            return Ok(showtimes.MapToResponse());
        }
    }
}