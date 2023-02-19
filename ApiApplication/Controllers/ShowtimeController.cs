using CinemaApplication.DAL.Repositories;
using CinemaApplication.DTOs;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IShowtimesRepository _showtimeRepository;

        public ShowtimeController(IShowtimesRepository showtimeRepository)
        {
            this._showtimeRepository = showtimeRepository;
        }

        /// <summary>
        /// Get all available showtimes
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        //[Authorize(Policy = "Read")]
        public async Task<ActionResult<IEnumerable<ShowtimeDto>>> GetShowtimes()
        {
            var showTimes = await _showtimeRepository.GetAllAsync();
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
        //[Authorize(Policy = "Write")]
        public async Task<ActionResult<IEnumerable<int>>> CreateShowtime(NewShowtimeDto showtime)
        {
            var newShowtime = await _showtimeRepository.AddAsync(new CinemaApplication.DAL.Models.ShowtimeEntity
            {
                AuditoriumId = showtime.AudithoriumId,
                StartDate = showtime.StartDate,
                EndDate = showtime.EndDate,
                Schedule = showtime.Schedule,
                Movie = new CinemaApplication.DAL.Models.MovieEntity
                {
                    ImdbId = showtime.Movie.ImdbId,
                    Title = showtime.Movie.Title,
                    ReleaseDate = showtime.Movie.ReleaseDate
                }
            });

            return Ok(newShowtime);
        }

        /// <summary>
        /// Updates existing showtime
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        //[Authorize(Policy = "Write")]
        public ActionResult<IEnumerable<int>> UpdateShowtime()
        {
            return Ok(new List<int> { 1, 4 });
        }

        /// <summary>
        /// Deletes a showtime
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{id}")]
        //[Authorize(Policy = "Write")]
        public async Task<ActionResult> DeleteShowtime(int id)
        {
            var showtimes = await _showtimeRepository.GetAllAsync(s => s.Id == id);

            await _showtimeRepository.DeleteAsync(showtimes.Single());

            return Ok();
        }
    }
}
