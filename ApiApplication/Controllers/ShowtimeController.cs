using AutoMapper;
using Cinema.Business.Abstract;
using Cinema.Entities.Concrete;
using Cinema.Entities.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiApplication.Controllers
{

    public class ShowtimeController : BaseApiController
    {
        IShowtimeService _showtimeService;
        private readonly IMapper _mapper;
        public ShowtimeController(IShowtimeService showtimeService, IMapper mapper)
        {
            _showtimeService = showtimeService;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(Policy = "ReadPolicy")]
        [Route("v1")]
        public async Task<IActionResult> GetShowtimes(string? movieTitle = null, DateTime? date = null)
        {
            // Getting showtimes including the auditorium id.
            var result = await _showtimeService.GetShowtimes(movieTitle, date);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpPost]
        [Authorize(Policy = "WritePolicy")]
        [Route("v1")]
        public async Task<IActionResult> CreateShowtime([FromBody] ShowtimeDto showtime)
        {
            // Creates new showtime and relates to the specified auditorium.
            var result = await _showtimeService.AddAsync(showtime);
            if (result.Success)
            {
                return Ok();
            }
            return BadRequest(result.Message);
        }

        [HttpPut]
        [Authorize(Policy = "WritePolicy")]
        [Route("v1")]
        public async Task<IActionResult> UpdateShowtime(int id, [FromBody] ShowtimeDto showtime)
        {
            // Updates the showtime information also updates the auditorium if necessary.
            var result = await _showtimeService.UpdateAsync(id, showtime);
            if (result.Success)
            {
                return Ok();
            }
            return BadRequest(result.Message);
        }

        [HttpDelete]
        [Authorize(Policy = "WritePolicy")]
        [Route("v1")]
        public async Task<IActionResult> DeleteShowtime(int id)
        {
            // Removes showtime from the auditorium and deletes it.
            var result = await _showtimeService.DeleteAsync(id);
            if (result.Success)
            {
                return Ok();
            }
            return BadRequest(result.Message);
        }

        [HttpPatch]
        [Authorize(Policy = "WritePolicy")]
        [Route("v1")]
        public IActionResult Error()
        {
            // Mocking internal server error.
            throw new AccessViolationException("Violation Exception while accessing the resource.");

        }

    }
}
