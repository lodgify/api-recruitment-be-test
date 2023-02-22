using ApiApplication.Extensions;
using CinemaApplication.Core.Exceptions;
using CinemaApplication.DTOs;
using CinemaApplication.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiApplication.Controllers
{
    /// <summary>
    /// Exposes operations for showtime management
    /// </summary>
    public class ShowtimeController : CinemaBaseApiController
    {
        private readonly IShowtimeService _showtimeService;

        /// <summary>
        /// Constructor
        /// </summary>
        public ShowtimeController(IShowtimeService showtimeService)
        {
            _showtimeService = showtimeService;
        }

        /// <summary>
        /// Get all available showtimes
        /// </summary>
        /// <returns>Collection of showtime metadata</returns>
        [HttpGet]
        [Authorize(Policy = "Read")]
        public async Task<ActionResult<IEnumerable<ShowtimeDto>>> GetShowtimes([FromQuery] ShowtimeQuery query)
            => (await _showtimeService.GetAllAsync(query)).ToActionResult();

        /// <summary>
        /// Create a new showtime
        /// </summary>
        /// <returns>Id of newly added showtime</returns>
        [HttpPost]
        [Authorize(Policy = "Write")]
        public async Task<ActionResult<int>> CreateShowtime(NewShowtimeDto showtime)
            => (await _showtimeService.AddAsync(showtime)).ToActionResult();

        /// <summary>
        /// Update existing showtime
        /// </summary>
        [HttpPut]
        [Authorize(Policy = "Write")]
        public async Task<IActionResult> UpdateShowtime(ShowtimeDto showtime)
            => (await _showtimeService.UpdateAsync(showtime)).ToActionResult();

        /// <summary>
        /// Delete existing showtime
        /// </summary>
        [HttpDelete("{id}")]
        [Authorize(Policy = "Write")]
        public async Task<IActionResult> DeleteShowtime(int id)
            => (await _showtimeService.DeleteAsync(id)).ToActionResult();

        /// <summary>
        /// Partial update of a showtime
        /// </summary>
        [HttpPatch]
        [Authorize(Policy = "Write")]
        public async Task<IActionResult> PatchShowtime(ShowtimeDto _)
        {
            throw new ShowtimeException("Partial update has thrown unhandled exception.");
        }
    }
}
