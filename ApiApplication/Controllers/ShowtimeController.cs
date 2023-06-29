using ApiApplication.Database.Entities;
using ApiApplication.Helpers;
using ApiApplication.Models;
using ApiApplication.Services.Implementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShowtimeController : ControllerBase
    {
        private readonly IServiceShowtime _serviceShowtime;
        public ShowtimeController(IServiceShowtime serviceShowtime)
        {
            _serviceShowtime = serviceShowtime;
        }

        [Authorize(Policy = "Read-Only")]
        [HttpGet]
        public IEnumerable<ShowtimeEntity> GetAllShowtimes(
            [FromQuery(Name = "date")] DateTime? date,
            [FromQuery(Name = "title")] string movieTitle)
        {
            return _serviceShowtime.GetAll(date, movieTitle);
        }


        [Authorize(Policy = "Write")]
        [HttpPost]
        public async Task<ActionResult<ShowtimeEntity>> AddShowTime([FromBody] Showtime showtime)
        {
            string errorMessage;
            if (!ValidatorsHelper.ShowtimeResource(showtime, out errorMessage))
                throw new Exception(errorMessage);

            var result = await _serviceShowtime.Add(showtime);

            if (result is not null)
                return CreatedAtAction(nameof(ShowtimeController.AddShowTime), new { id = result.Id }, result);

            return BadRequest("Movie not found");
        }

        [Authorize(Policy = "Write")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateShowTime(int id, [FromBody] Showtime showtime)
        {
            string errorMessage;
            if (!ValidatorsHelper.ShowtimeResource(showtime, out errorMessage))
                throw new Exception(errorMessage);

            var existingShowtime = _serviceShowtime.GetExisting(id);
            var updatedShowtimeEntity = await _serviceShowtime.Update(existingShowtime, showtime);

            if (updatedShowtimeEntity is not null)
                return Ok(updatedShowtimeEntity);
            else
                return NotFound();

        }

        [Authorize(Policy = "Write")]
        [HttpDelete("{id}")]
        public IActionResult RemoveShowTime(int id)
        {
            var deletedShowtime = _serviceShowtime.Remove(id);

            if (deletedShowtime is not null)
                return NoContent();
            else
                return NotFound();
        }

        [Authorize(Policy = "Write")]
        [HttpPatch()]
        public IActionResult Patch()
        {
            throw new Exception("An error occurred during the call of this method.");
        }
    }
}
