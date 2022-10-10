using ApiApplication.Models;
using ApiApplication.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShowtimeController : ControllerBase
    {
        public readonly IShowtimeService showtimeService;
        public ShowtimeController(IShowtimeService showtimeService) {
            this.showtimeService= showtimeService;
        }

        // GET: api/<ShowtimeController>
        [HttpGet]
        public IEnumerable<Showtime> Get(string movie = "", DateTime? date = null)
        {
            var result = showtimeService.GetAll(movie, date);
            return result;
        }

        // POST api/<ShowtimeController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Showtime showtime)
        {
            var result = await showtimeService.Create(showtime);
             return StatusCode(201, result); 
        }

        // PUT api/<ShowtimeController>/5
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Showtime showtime)
        {
            var result = await showtimeService.Update(showtime);
            if (result == null) return NotFound();
            return StatusCode(202, result);
        }

        // DELETE api/<ShowtimeController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = showtimeService.Delete(id);
            if (result == null) return NotFound();
            return StatusCode(202, result);
        }

        [HttpPatch]
        public IActionResult Patch() 
        {
            return StatusCode(500);
        }
    }
}
