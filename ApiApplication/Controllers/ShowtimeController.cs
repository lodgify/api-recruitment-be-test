using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiApplication.Database;
using ApiApplication.Resources;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiApplication.Controllers
{
    [Route("/showtime")]
    public class ShowtimeController : ApiBaseController
    {
        private IShowtimesRepository showTimeRepository;

        public ShowtimeController(IMapper mapper, IShowtimesRepository showTimeRepository) : base(mapper)
        {
            this.showTimeRepository = showTimeRepository;
        }

        // GET: api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            //var shows = await showTimeRepository.GetCollection();
            //var result = this.mapper.Map<IEnumerable<ShowTime>>(shows);
            //return Enumerable.Empty<ShowTime>();
            var shows = await showTimeRepository.GetCollection();
            var result = this.mapper.Map<IEnumerable<ShowTime>>(shows);
            return this.StatusCode(StatusCodes.Status200OK, result);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

