using ApiApplication.Auth;
using ApiApplication.Models;
using ApiApplication.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiApplication.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = CustomAuthenticationSchemeOptions.AuthenticationScheme)]
    [ApiController]
    public class ShowtimeController : ControllerBase
    {
        public readonly IShowtimeService showtimeService;
        public ShowtimeController(IShowtimeService showtimeService)
        {
            this.showtimeService = showtimeService;
        }

        // GET: api/<ShowtimeController>
        [HttpGet]
        [Authorize(Policy = "Read")]
        public IActionResult Get(string movie = "", DateTime? date = null)
        {
            var result = showtimeService.GetAll(movie, date);
            return Ok(result);
        }

        // POST api/<ShowtimeController>
        [HttpPost]
        [Authorize(Policy = "Write")]
        public async Task<IActionResult> Post([FromBody] Showtime showtime)
        {
            var result = await showtimeService.Create(showtime);
            return StatusCode(201, result);
        }

        // PUT api/<ShowtimeController>
        [HttpPut]
        [Authorize(Policy = "Write")]
        public async Task<IActionResult> Put([FromBody] Showtime showtime)
        {
            var result = await showtimeService.Update(showtime);
            if (result == null) return NotFound();
            return StatusCode(202, result);
        }

        // DELETE api/<ShowtimeController>/5
        [HttpDelete("{id}")]
        [Authorize(Policy = "Write")]
        public IActionResult Delete(int id)
        {
            var result = showtimeService.Delete(id);
            if (result == null) return NotFound();
            return StatusCode(202, result);
        }

        [HttpPatch]
        [Authorize(Policy = "Write")]
        public IActionResult Patch()
        {
            throw new Exception();
        }
    }
}
