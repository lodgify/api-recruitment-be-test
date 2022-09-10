using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiApplication.Resources;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiApplication.Controllers
{
    [Route("/imdb")]
    public class StatusController : ApiBaseController
    {
        public StatusController(IMapper mapper) : base(mapper)
        {
        }

        // GET: api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return null;
            //return new string[] { "value1", "value2" };
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

