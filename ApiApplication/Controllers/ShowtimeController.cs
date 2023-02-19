using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiApplication.Database;
using ApiApplication.Database.Entities;
using ApiApplication.Resources;
using ApiApplication.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Policy = "Read")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await this.showtimeService.GetShowtimes();
            return this.StatusCode(StatusCodes.Status200OK, result);
        }

        // POST /showtime
        [Authorize(Policy = "Write")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ShowTime showTime)
        {
            var result = await showtimeService.CreateShowtime(showTime);
            return this.StatusCode(StatusCodes.Status201Created, result);
        }

        // PUT /showtime/5
        [Authorize(Policy = "Write")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ShowTime showTime)
        {
            var result = await showtimeService.UpdateShowtime(id, showTime);
            return this.StatusCode(StatusCodes.Status200OK, result);
        }

        // DELETE /showtime/5
        [Authorize(Policy = "Write")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await showtimeService.DeleteShowtime(id);
            return this.StatusCode(StatusCodes.Status200OK);
        }

        // DELETE /patch
        [Authorize(Policy = "Write")]
        [HttpPatch()]
        public async Task<IActionResult> Patch()
        {
            throw new Exception("Controller error for testing purposes");
        }
    }
}

