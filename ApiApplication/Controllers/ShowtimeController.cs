using ApiApplication.Database;
using ApiApplication.Database.Entities;
using ApiApplication.Dtos;
using ApiApplication.Services;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Threading.Tasks;

namespace ApiApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShowtimeController : ControllerBase
    {

        private readonly IMapper _mapper;
        private readonly IShowTimeService _service;

        public ShowtimeController(
              IShowTimeService service
            , IMapper mapper)
        {
            _service = service;
            _mapper = mapper;


        }

        [HttpGet]
        public ActionResult<IEnumerable<ShowTimeDTO>> Get([FromQuery] ShowTimeCriteriaDTO filter)
        {
            if (!filter.ShowTime.HasValue && string.IsNullOrEmpty(filter.MovieTitle))
            {
                return Ok(_mapper.Map<IEnumerable<ShowtimeEntity>, IEnumerable<ShowTimeDTO>>(_service.GetCollection()));
            }

            var moviesSchedule = _service.GetCollection(filter);

            if (!moviesSchedule.Any())
                return NotFound();


            return Ok(moviesSchedule);

        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ShowTimeDTO showTime)
        {
            return Ok(await _service.Create(showTime));
        }

    }
}
