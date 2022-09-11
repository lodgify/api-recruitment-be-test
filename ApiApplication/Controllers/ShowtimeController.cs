using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiApplication.Database;
using ApiApplication.Database.Entities;
using ApiApplication.Resources;
using ApiApplication.Services;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiApplication.Controllers
{
    [Route("/showtime")]
    public class ShowtimeController : ApiBaseController
    {
        private ShowtimeService showtimeService;

        public ShowtimeController(IMapper mapper, IShowtimesRepository showTimeRepository, IImdbRepository imdbRepository, ShowtimeService showtimeService) : base(mapper)
        {
            this.showtimeService = showtimeService;
        }

        // GET: /showtime
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await this.showtimeService.GetShowtimes();
            return this.StatusCode(StatusCodes.Status200OK, result);
        }

        //// GET /showtime/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST /showtime
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ShowTime showTime)
        {
            var result = await showtimeService.CreateShowtime(showTime);
            return this.StatusCode(StatusCodes.Status201Created, result);
        }

        // PUT /showtime/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ShowTime showTime)
        {
            var result = await showtimeService.UpdateShowtime(id, showTime);
            return this.StatusCode(StatusCodes.Status200OK, result);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

