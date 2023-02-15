using CinemaApplication.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiApplication.Controllers
{
    /// <summary>
    /// Exposes operations for showtimes
    /// </summary>
    public class ShowtimeController : CinemaBaseApiController
    {
        /// <summary>
        /// Get all available showtimes.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<IEnumerable<ShowtimeDto>> Get()
        {
            return Ok(new List<ShowtimeDto>
            {
                new ShowtimeDto
                {
                     Id= 1,
                     StartDate = DateTime.UtcNow.AddDays(1),
                     EndDate = DateTime.UtcNow.AddDays(5),
                     AudithoriumId = 1,
                     Movie= new MovieDto
                     {
                         Title = "Go"
                     },
                    Schedule = "some schedule"
                }
            });
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
        public ActionResult<Task> Delete(int id)
        {
            return Ok(Task.CompletedTask);
        }
    }
}
