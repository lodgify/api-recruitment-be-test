using ApiApplication.Database;
using ApiApplication.Database.Entities;
using ApiApplication.Dtos;
using ApiApplication.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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

        private readonly IShowtimesRepository _repository;
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClient;
        private readonly IMapper _mapper;
        private readonly IIMDBHttpClientManager _imdbHttpClientManager;

        public ShowtimeController(IShowtimesRepository repository,
            IHttpClientFactory httpclient,
            IMapper mapper,
            IIMDBHttpClientManager imdbHttpClientManager
            )
        {
            _repository = repository;
            _httpClient = httpclient;
            _mapper = mapper;
            _imdbHttpClientManager = imdbHttpClientManager;

        }

        [HttpGet]
        [Authorize(Policy = "ReadOnlyToken")]
        public ActionResult<IEnumerable<ShowTimeDTO>> Get([FromQuery] ShowTimeCriteriaDTO criteria)
        {

            if (!criteria.ShowTime.HasValue && string.IsNullOrEmpty(criteria.MovieTitle))
            {
                return Ok(_mapper.Map<IEnumerable<ShowtimeEntity>, IEnumerable<ShowTimeDTO>>(_repository.GetCollection()));
            }

            var moviesSchedulesEntities = _repository.GetCollection(x =>

           (string.IsNullOrEmpty(criteria.MovieTitle) || x.Movie.Title.Equals(criteria.MovieTitle))
           &&
           (!criteria.ShowTime.HasValue || (criteria.ShowTime.Value.Date >= x.StartDate.Date && criteria.ShowTime.Value.Date <= x.EndDate.Date)));

            IEnumerable<ShowTimeDTO> _movieShowTimes = _mapper.Map<IEnumerable<ShowtimeEntity>, IEnumerable<ShowTimeDTO>>(moviesSchedulesEntities);

            if (!_movieShowTimes.Any())
                return NotFound();

            return Ok(_movieShowTimes);

        }

        [HttpPost]
        public async Task<ActionResult<ShowTimeDTO>> Post([FromBody] ShowTimeDTO showTimeDTO)
        {

            JObject jObject = await _imdbHttpClientManager.GetIMDBJObject(showTimeDTO.Movie.ImdbId);

            ShowtimeEntity showTimeEntity = _mapper.Map<ShowTimeDTO, ShowtimeEntity>(showTimeDTO);

            showTimeEntity.Movie.Title = jObject["title"]?.ToString();

            showTimeEntity.Movie.ReleaseDate = Convert.ToDateTime(jObject["releaseDate"]);

            showTimeEntity.Movie.Stars = jObject["stars"]?.ToString();

            var dbShowTime = _repository.Add(showTimeEntity);

            return _mapper.Map<ShowtimeEntity, ShowTimeDTO>(dbShowTime);
        }


        [HttpPut]
        public async Task<ActionResult<ShowTimeDTO>> Put([FromBody] ShowTimeDTO showTimeDTO)
        {
            JObject jObject = null;

            ShowtimeEntity showTimeEntity = _mapper.Map<ShowTimeDTO, ShowtimeEntity>(showTimeDTO);

            if (showTimeDTO.Movie != null)
            {
                jObject = await _imdbHttpClientManager.GetIMDBJObject(showTimeDTO.Movie.ImdbId);

                showTimeEntity.Movie.Title = jObject["title"]?.ToString();

                showTimeEntity.Movie.ReleaseDate = Convert.ToDateTime(jObject["releaseDate"]);

                showTimeEntity.Movie.Stars = jObject["stars"]?.ToString();
            }

            var result = _repository.Update(showTimeEntity);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult<ShowTimeDTO> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid ShowTime id");
            }

            var showTimeDTO = _repository.Delete(id);

            if (showTimeDTO == null)
                return NotFound($"ShowTime not found with id = {id}");

            return Ok(showTimeDTO);

        }

    }
}
