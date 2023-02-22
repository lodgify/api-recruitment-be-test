using ApiApplication.Auth;
using ApiApplication.DTOs.API;
using ApiApplication.Services;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ShowtimeController : Controller
    {
        private readonly IShowtimeService _service;
        private readonly IMapper _mapper;

        public ShowtimeController(IShowtimeService service, IMapper mapper)
        {
            _service = service ?? throw new ArgumentNullException(nameof(_service));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet("")]
        [Authorize(Roles = Constants.Roles.Read)]
        public async Task<ActionResult<IEnumerable<Showtime>>> GetAsync([FromQuery] DateTime date, [FromQuery] string movieTitle)
        {
            IEnumerable<ShowtimeEntity> entities = await _service.GetAsync(date, movieTitle);
            var result = _mapper.Map<IEnumerable<Showtime>>(entities);
            return Ok(result);
        }

        [HttpPost("")]
        [Authorize(Roles = Constants.Roles.Write)]
        public async Task<ActionResult<Showtime>> PostAsync([FromBody] Showtime showtime)
        {
            if (showtime?.Movie?.ImdbId == default)
            {
                return BadRequest("Movie.ImdbId property missing.");
            }

            var result = await _service.CreateAsync(showtime);
            return Ok(_mapper.Map<Showtime>(result));
        }

        [HttpPut("")]
        [Authorize(Roles = Constants.Roles.Write)]
        public async Task<ActionResult<Showtime>> Put([FromBody] Showtime showtime)
        {
            if ((await _service.GetByIdAsync(showtime.Id)) == null)
            {
                return NotFound($"Not found Showtime with provided id");
            }

            var result = await _service.UpdateAsync(showtime);
            return Ok(_mapper.Map<Showtime>(result));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = Constants.Roles.Write)]
        public async Task<ActionResult> Delete(int id)
        {
            if ((await _service.GetByIdAsync(id)) == null)
            {
                return NotFound($"Not found Showtime with provided id");
            }

            await _service.DeleteAsync(id);
            return Ok();
        }

        [HttpPatch("")]
        [Authorize(Roles = Constants.Roles.Write)]
        public ActionResult Patch()
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Test error handler");
        }
    }
}
