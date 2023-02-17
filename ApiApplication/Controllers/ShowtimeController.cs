using CinemaApplication.DAL.Repositories;
using CinemaApplication.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
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
        private readonly IShowtimesRepository showtimesRepository;

        public ShowtimeController(IShowtimesRepository showtimesRepository)
        {
            this.showtimesRepository = showtimesRepository;
        }

        /// <summary>
        /// Get all available showtimes.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShowtimeDto>>> Get()
        {
            var showTimes = await showtimesRepository.GetAllAsync();
            return Ok(showTimes.Select(s => new ShowtimeDto
            {
                Id = s.Id,
                StartDate = s.StartDate,
                EndDate = s.EndDate,
                Schedule = String.Join(",", s.Schedule),
                AudithoriumId = s.AuditoriumId,
                Movie = new MovieDto
                {
                    Title = s.Movie.Title,
                    ImdbId = s.Movie.ImdbId,
                    ReleaseDate = s.Movie.ReleaseDate,
                    Starts = s.Movie.Stars
                }
            }));
        }

        /// <summary>
        /// Create new showtime
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<IEnumerable<int>> Post()
        {
            return Ok(new List<int> { 1, 4 });
        }

        /// <summary>
        /// Updates existing showtime
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        public ActionResult<IEnumerable<int>> Update()
        {
            return Ok(new List<int> { 1, 4 });
        }

        /// <summary>
        /// Deletes a showtime
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var showtimes = await showtimesRepository.GetAllAsync(s => s.Id == id);

            await showtimesRepository.DeleteAsync(showtimes.Single());

            return Ok();
        }
    }
}
