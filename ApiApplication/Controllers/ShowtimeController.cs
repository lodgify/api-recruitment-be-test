using CinemaApplication.DTOs;
using CinemaApplication.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiApplication.Controllers
{
    /// <summary>
    /// Exposes operations for showtimes
    /// </summary>
    public class ShowtimeController : CinemaBaseApiController
    {
        private readonly IShowtimeService _showtimeService;

        public ShowtimeController(IShowtimeService showtimeService)
        {
            _showtimeService = showtimeService;
        }

        /// <summary>
        /// Get all available showtimes
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        //[Authorize(Policy = "Read")]
        public async Task<ActionResult<IEnumerable<ShowtimeDto>>> GetShowtimes()
            => Ok(await _showtimeService.GetAllAsync());

        /// <summary>
        /// Create new showtime
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        //[Authorize(Policy = "Write")]
        public async Task<ActionResult<IEnumerable<int>>> CreateShowtime(NewShowtimeDto showtime)
            => Ok(await _showtimeService.AddAsync(showtime));

        /// <summary>
        /// Updates existing showtime
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        //[Authorize(Policy = "Write")]
        public async Task<IActionResult> UpdateShowtime(ShowtimeDto showtime)
        {
            await _showtimeService.UpdateAsync(showtime);

            return Ok();
        }

        /// <summary>
        /// Deletes a showtime
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{id}")]
        //[Authorize(Policy = "Write")]
        public async Task<IActionResult> DeleteShowtime(int id)
        {
            await _showtimeService.DeleteAsync(id);

            return Ok();
        }
    }
}
