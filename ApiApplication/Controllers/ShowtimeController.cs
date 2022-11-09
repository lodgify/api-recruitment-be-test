using ApiApplication.Database;
using ApiApplication.Database.Entities;
using ApiApplication.Dtos;
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
        private readonly IShowtimesRepository _repository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClient;
        public ShowtimeController(IShowtimesRepository repository
            , IMapper mapper
            , IConfiguration configuration
            , IHttpClientFactory httpClientFactory
            )
        {
            _repository = repository;
            _mapper = mapper;
            _configuration = configuration;
            _httpClient = httpClientFactory;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ShowTimeDTO>> Get([FromQuery] ShowTimeCriteriaDTO filter)
        {
            if (!filter.ShowTime.HasValue && string.IsNullOrEmpty(filter.MovieTitle))
            {
                return Ok(_mapper.Map<IEnumerable<ShowtimeEntity>, IEnumerable<ShowTimeDTO>>(_repository.GetCollection()));
            }

            var moviesSchedulesEntities = _repository.GetCollection(x =>

            (string.IsNullOrEmpty(filter.MovieTitle) || x.Movie.Title.Equals(filter.MovieTitle))
            &&
            (filter.ShowTime.Value.Date >= x.StartDate.Date && filter.ShowTime.Value.Date <= x.EndDate.Date)

            );

            if (!moviesSchedulesEntities.Any())
                return NotFound();

            IEnumerable<ShowTimeDTO> _movieShowTimes = _mapper.Map<IEnumerable<ShowtimeEntity>, IEnumerable<ShowTimeDTO>>(moviesSchedulesEntities);

            return Ok(_movieShowTimes);

            #region another possible way to implement client of GetCollection
            //var moviesSchedules = _repository.GetCollection(query =>
            //  {
            //      if (string.IsNullOrEmpty(filter.MovieTitle))
            //      {
            //          query.Where(x => x.Movie.Title.Equals(filter.MovieTitle));
            //      }

            //      if (filter.ShowTime.HasValue)
            //      {
            //          query.Where(x => x.StartDate.Equals(filter.ShowTime));
            //      }

            //      return query;

            //  }); 
            #endregion
        }

        public async Task<ActionResult> Post([FromBody] ShowTimeDTO showTime)
        {
            var restClient = _httpClient.CreateClient("IMDBClient");

            string url = $"/Title/{_configuration.GetValue<string>("IMDBApiKey")}/{showTime.Movie.ImdbId}";

            HttpResponseMessage response = await restClient.GetAsync(url);

            ShowtimeEntity showTimeEntity = _mapper.Map<ShowTimeDTO, ShowtimeEntity>(showTime);

            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();

                JObject jObject = JsonConvert.DeserializeObject<JObject>(json);

                showTimeEntity.Movie.Title = jObject["title"]?.ToString();

                showTimeEntity.Movie.ReleaseDate = Convert.ToDateTime(jObject["releaseDate"]);

                showTimeEntity.Movie.Stars = jObject["stars"]?.ToString();
            }
            _repository.Add(showTimeEntity);

            return CreatedAtAction("", new { showTimeEntity.Id });
        }

    }
}
