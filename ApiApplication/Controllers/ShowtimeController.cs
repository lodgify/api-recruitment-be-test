using ApiApplication.Models;
using ApiApplication.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

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
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ShowtimeController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ShowtimeController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
