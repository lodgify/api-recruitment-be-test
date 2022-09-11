using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiApplication.Resources;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiApplication.Controllers
{
    [Route("/status")]
    public class StatusController : ApiBaseController
    {
        private IImdbPageStatus imdb;

        public StatusController(IMapper mapper, IImdbPageStatus imdb) : base(mapper)
        {
            this.imdb = imdb;
        }

        // GET: api/values
        [HttpGet]
        public IActionResult Get()
        {
            var result = imdb.Status;
            return this.StatusCode(StatusCodes.Status200OK, result);
        }
    }
}

